﻿using ExcelDataReader;
using Library.Common;
using Library.Models;
using Library.Request;
using Library.Response;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Linq;
using System.Security.Claims;
using WebApi.IRepository;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace WebApi.Repository
{
    public class SubjectRepository : ISubjectRepository
    {
        private readonly QuizManagementContext DBcontext;
        private readonly ILogHistoryRepository logRepository;

        public SubjectRepository(QuizManagementContext DBcontext, ILogHistoryRepository logRepository)
        {
            this.DBcontext = DBcontext;
            this.logRepository = logRepository;
        }

        public async Task<RequestResponse> AddSubject(Subject req)
        {
            try
            {
                var data = await this.DBcontext.Subjects.FirstOrDefaultAsync(x => x.SubjectCode.Equals(req.SubjectCode));

                if (data == null)
                {
                    req.IsDeleted = false;

                    await this.DBcontext.Subjects.AddAsync(req);

                    await this.DBcontext.SaveChangesAsync();

                    await logRepository.LogAsync($"Add subject [{req.SubjectCode}] {req.SubjectName}");

                    return new RequestResponse
                    {
                        IsSuccessful = true,
                        Message = "Add Subject Successfully",
                    };
                }
                else
                {
                    return new RequestResponse
                    {
                        IsSuccessful = false,
                        Message = "Subject Code already exist",
                    };
                }
            }
            catch (Exception ex)
            {
                return new RequestResponse
                {
                    IsSuccessful = false,
                    Message = ex.Message,
                };
            }
        }

        public async Task<RequestResponse> DeleteSubject(int subjectId)
        {
            try
            {
                var data = await this.DBcontext.Subjects.FirstOrDefaultAsync(x => x.SubjectId == subjectId);

                if (data != null)
                {
                    this.DBcontext.Subjects.Remove(data);

                    await this.DBcontext.SaveChangesAsync();

                    await logRepository.LogAsync($"Delete subject [{data.SubjectCode}] {data.SubjectName}");

                    return new RequestResponse
                    {
                        IsSuccessful = true,
                        Message = "Delete Subject Successfully",
                    };
                }
                else
                {
                    return new RequestResponse
                    {
                        IsSuccessful = false,
                        Message = "Subject not exist",
                    };
                }
            }
            catch (DbUpdateException ex)
            {
                if (ex.InnerException is SqlException sqlEx && sqlEx.Message.Contains("REFERENCE constraint"))
                {
                    return new RequestResponse
                    {
                        IsSuccessful = false,
                        Message = "Cannot delete because there is some data connect to this"
                    };
                }
                else
                {
                    return new RequestResponse
                    {
                        IsSuccessful = false,
                        Message = ex.Message,
                    };
                }
            }
            catch (Exception ex)
            {
                return new RequestResponse
                {
                    IsSuccessful = false,
                    Message = ex.Message,
                };
            }
        }

        public async Task<ResultResponse<Subject>> GetSubjectById(int subjectId)
        {
            try
            {
                var data = await this.DBcontext.Subjects.FirstOrDefaultAsync(x => x.SubjectId == subjectId);

                return new ResultResponse<Subject>
                {
                    IsSuccessful = data != null ? true : false,
                    Message = data != null ? string.Empty : "Cannot found subject",
                    Item = data,
                };
            }
            catch (Exception ex)
            {
                return new ResultResponse<Subject>
                {
                    IsSuccessful = false,
                    Message = ex.Message,
                };
            }
        }

        public async Task<ResultResponse<Subject>> GetSubjects()
        {
            try
            {
                var data = await this.DBcontext.Subjects.ToListAsync();

                return new ResultResponse<Subject>
                {
                    IsSuccessful = data != null ? true : false,
                    Message = data != null ? string.Empty : "Cannot found subject",
                    Items = data,
                };
            }
            catch (Exception ex)
            {
                return new ResultResponse<Subject>
                {
                    IsSuccessful = false,
                    Message = ex.Message,
                };
            }
        }

        public async Task<RequestResponse> UpdateSubject(Subject req)
        {
            try
            {
                var data = await this.DBcontext.Subjects.FirstOrDefaultAsync(x => x.SubjectId == req.SubjectId);

                if (data != null)
                {
                    if (data.SubjectCode != req.SubjectCode)
                    {
                        var existingSubjectWithSameCode = await this.DBcontext.Subjects
                            .AnyAsync(x => x.SubjectCode == req.SubjectCode && x.SubjectId != req.SubjectId);

                        if (existingSubjectWithSameCode)
                        {
                            return new RequestResponse
                            {
                                IsSuccessful = false,
                                Message = "SubjectCode already exists.",
                            };
                        }
                    }

                    data.SubjectCode = req.SubjectCode;
                    data.SubjectName = req.SubjectName;
                    data.IsDeleted = req.IsDeleted;
                    data.FacultyId = req.FacultyId;
                    data.Faculty.FacultyName = req.Faculty.FacultyName;
                    await this.DBcontext.SaveChangesAsync();

                    await logRepository.LogAsync($"Update subject [{data.SubjectCode}] {data.SubjectName}");

                    return new RequestResponse
                    {
                        IsSuccessful = true,
                        Message = "Subject updated successfully",
                    };
                }
                else
                {
                    return new RequestResponse
                    {
                        IsSuccessful = false,
                        Message = "Subject Code already exist",
                    };
                }
            }
            catch (Exception ex)
            {
                return new RequestResponse
                {
                    IsSuccessful = false,
                    Message = ex.Message,
                };
            }
        }

        public async Task<ResultResponse<SubjectResponse>> GetSubjectByRole(int roleId, int userId, int campusId)
        {
            try
            {
                var data = (from s in this.DBcontext.Subjects
                            join cus in this.DBcontext.CampusUserSubjects on s.SubjectId equals cus.SubjectId into subjectJoin
                            from cus in subjectJoin.DefaultIfEmpty()
                            where
                            (roleId == 4 && (
                                // Điều kiện 1: Các môn mà UserId = X đang là chủ nhiệm tại campus của họ
                                (cus != null && cus.UserId == userId && cus.CampusId == campusId)
                                // Điều kiện 2: Hoặc các môn chưa có chủ nhiệm tại campus của user X
                                || (cus == null || !this.DBcontext.CampusUserSubjects
                                    .Any(other => other.SubjectId == s.SubjectId /*&& other.IsLecturer == false*/ && other.CampusId == campusId))
                            ))
                            // Nếu RoleId = 3, lấy tất cả môn học
                            || roleId == 3
                            select new SubjectResponse
                            {
                                SubjectId = s.SubjectId,
                                SubjectCode = s.SubjectCode,
                                SubjectName = s.SubjectName
                            }).Distinct().ToList();


                return new ResultResponse<SubjectResponse>
                {
                    IsSuccessful = true,
                    Items = data
                };
            }
            catch (Exception ex)
            {
                return new ResultResponse<SubjectResponse>
                {
                    IsSuccessful = false,
                    Message = ex.Message,
                };
            }
        }

        public async Task<RequestResponse> ImportSubjectsFromExcel(IFormFile file, ClaimsPrincipal currentUser)
        {
            // Khai báo biến
            var response = new RequestResponse();
            var errors = new List<string>();
            var subjectsToAdd = new List<Subject>();
            // Tạo một HashSet để theo dõi các bản ghi đã được thêm
            var existingSubjectSet = new HashSet<string>();

            try
            {
                // Đăng ký mã hóa hỗ trợ cho Excel
                System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);

                // Kiểm tra nếu file không tồn tại hoặc không có dữ liệu
                if (file == null || file.Length == 0)
                {
                    return new RequestResponse
                    {
                        IsSuccessful = false,
                        Message = "No file uploaded!",
                    };
                }

                // Lấy thông tin người dùng hiện tại từ Claims
                var userId = int.TryParse(currentUser.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value, out var id) ? id : 0;

                // Có được id của người dùng từ hệ thống thì liên kết tới database
                var myUser = await DBcontext.Users.FirstOrDefaultAsync(u => u.UserId == userId);
                if (myUser == null)
                {
                    return new RequestResponse
                    {
                        IsSuccessful = false,
                        Message = "User not found.",
                    };
                }

                // Lấy RoleId và CampusId từ đối tượng người dùng
                var roleId = myUser.RoleId;

                // Lấy RoleName dựa trên RoleId
                var currentUserRole = await DBcontext.UserRoles
                    .Where(r => r.RoleId == roleId)
                    .Select(r => r.RoleName)
                    .FirstOrDefaultAsync();

                if (string.IsNullOrEmpty(currentUserRole))
                {
                    return new RequestResponse
                    {
                        IsSuccessful = false,
                        Message = "User role not found.",
                    };
                }

                // Kiểm tra vai trò người dùng có phải là Admin hay không
                if (currentUserRole != "Admin")
                {
                    return new RequestResponse
                    {
                        IsSuccessful = false,
                        Message = "You do not have permission to import subjects.",
                    };
                }

                // Thiết lập thư mục lưu file upload
                var uploadsFolder = $"{Directory.GetCurrentDirectory()}\\Uploads\\Subjects";
                if (!Directory.Exists(uploadsFolder))
                {
                    Directory.CreateDirectory(uploadsFolder);
                }

                // Đường dẫn file
                var filePath = Path.Combine(uploadsFolder, Path.GetFileName(file.FileName));

                // Ghi file lên server
                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await file.CopyToAsync(stream);
                }

                using (var stream = System.IO.File.Open(filePath, FileMode.Open, FileAccess.Read))
                {
                    using (var reader = ExcelReaderFactory.CreateReader(stream))
                    {
                        bool isHeaderSkipped = false;

                        do
                        {
                            while (reader.Read())
                            {
                                if (!isHeaderSkipped)
                                {
                                    isHeaderSkipped = true;
                                    continue;
                                }

                                var subjectImportRequest = new SubjectImportRequest
                                {
                                    SubjectCode = reader.GetValue(0)?.ToString(),
                                    SubjectName = reader.GetValue(1)?.ToString(),
                                };

                                var errorMessages = new List<string>();

                                // Validate SubjectCode
                                if (string.IsNullOrEmpty(subjectImportRequest.SubjectCode) || subjectImportRequest.SubjectCode.Length > 10)
                                {
                                    errorMessages.Add("SubjectCode must not exceed 10 characters.");
                                }

                                // Validate SubjectName
                                if (string.IsNullOrEmpty(subjectImportRequest.SubjectName) || subjectImportRequest.SubjectName.Length > 100)
                                {
                                    errorMessages.Add("SubjectName must not exceed 100 characters.");
                                }

                                // Tạo khóa duy nhất cho mỗi môn học
                                string uniqueKey = subjectImportRequest.SubjectCode;
                                // Kiểm tra xem môn học đã tồn tại trong HashSet chưa
                                if (existingSubjectSet.Contains(uniqueKey))
                                {
                                    errorMessages.Add($"Duplicate entry for SubjectCode '{subjectImportRequest.SubjectCode}'.");
                                    continue; // Bỏ qua bản ghi trùng lặp
                                }

                                // Thêm vào HashSet nếu không trùng lặp
                                existingSubjectSet.Add(uniqueKey);
                                // Kiểm tra xem môn học đã tồn tại hay chưa
                                var existingSubject = await DBcontext.Subjects
                                    .FirstOrDefaultAsync(s => s.SubjectCode == subjectImportRequest.SubjectCode);

                                if (existingSubject != null)
                                {
                                    errorMessages.Add($"Subject with SubjectCode '{subjectImportRequest.SubjectCode}' already exists.");
                                    continue;
                                }

                                if (errorMessages.Any())
                                {
                                    errors.Add($"Error with SubjectCode '{subjectImportRequest.SubjectCode}': {string.Join(", ", errorMessages)}");
                                    continue;
                                }

                                // Tạo Subject nếu không có lỗi
                                var subject = new Subject
                                {
                                    SubjectCode = subjectImportRequest.SubjectCode,
                                    SubjectName = subjectImportRequest.SubjectName,
                                    IsDeleted = false,
                                    CreateDate = DateTime.Now,
                                };

                                subjectsToAdd.Add(subject);
                            }
                        } while (reader.NextResult());
                    }
                }

                // Lưu các subject hợp lệ
                if (subjectsToAdd.Any())
                {
                    await DBcontext.Subjects.AddRangeAsync(subjectsToAdd);
                    await DBcontext.SaveChangesAsync();
                }
                else
                {
                    // Thêm phản hồi nếu không có dữ liệu hợp lệ
                    response.IsSuccessful = false;
                    response.Message = "No valid data to import.";
                }

                if (errors.Any())
                {
                    response.IsSuccessful = false;
                    response.Message = $"There are the following errors: {string.Join("; ", errors)}";
                }
                else
                {
                    response.IsSuccessful = true;
                    response.Message = $"{subjectsToAdd.Count} subjects added successfully.";
                }
            }
            catch (Exception ex)
            {
                response.IsSuccessful = false;
                response.Message = $"An error occurred: {ex.Message}";
            }

            return response;
        }

        public async Task<ResultResponse<SubjectResponse>> GetSubjectList(SubjectRequest req)
        {
            try
            {
                var data = await (from sj in DBcontext.Subjects
                                  join f in DBcontext.Faculties on sj.FacultyId equals f.FacultyId
                                  where (req.SubjectCode.IsNullOrEmpty() || sj.SubjectCode.ToLower().Contains(req.SubjectCode.ToLower()))
                                  && (sj.FacultyId == req.FacultyId)
                                  select new SubjectResponse
                                  {
                                      SubjectId = sj.SubjectId,
                                      SubjectCode = sj.SubjectCode,
                                      SubjectName = sj.SubjectName,
                                      Faculty = f.FacultyName
                                  }).ToListAsync();

                return new ResultResponse<SubjectResponse>
                {
                    IsSuccessful = data != null ? true : false,
                    Message = data != null ? string.Empty : "Cannot found subject",
                    Items = data,
                };
            }
            catch (Exception ex)
            {
                return new ResultResponse<SubjectResponse>
                {
                    IsSuccessful = false,
                    Message = ex.Message,
                };
            }
        }

        public async Task<ResultResponse<SubjectResponse>> GetLectureSubjectList(int userId)
        {
            try
            {
                var data = await (from sj in DBcontext.Subjects
                                  join f in DBcontext.Faculties on sj.FacultyId equals f.FacultyId
                                  join cus in DBcontext.CampusUserSubjects on sj.SubjectId equals cus.SubjectId
                                  where cus.UserId == userId
                                  select new SubjectResponse
                                  {
                                      SubjectId = sj.SubjectId,
                                      SubjectCode = sj.SubjectCode,
                                      SubjectName = sj.SubjectName,
                                      Faculty = f.FacultyName
                                  }).ToListAsync();

                return new ResultResponse<SubjectResponse>
                {
                    IsSuccessful = true,
                    Items = data,
                };
            }
            catch (Exception ex)
            {
                return new ResultResponse<SubjectResponse>
                {
                    IsSuccessful = false,
                    Message = ex.Message,
                };
            }
        }

        public async Task<RequestResponse> LecturerSubjectModify(int userId, HashSet<SubjectResponse> req)
        {
            try
            {
                var existData = await this.DBcontext.CampusUserSubjects.Where(x => x.UserId == userId).Select(x => x.SubjectId).ToListAsync();

                var newData = req.Select(x => x.SubjectId).ToList();

                var user = await this.DBcontext.Users.Where(x => x.UserId == userId).FirstOrDefaultAsync();

                var toRemove = existData.Where(subjectId => !newData.Any(newId => newId == subjectId)).ToList();

                var toAdd = newData.Where(subjectId => !existData.Any(existingId => existingId == subjectId)).ToList();

                if (toRemove.Any())
                {
                    var dataToRemove = await this.DBcontext.CampusUserSubjects.Where(x => x.UserId == userId && toRemove.Contains(x.SubjectId)).ToListAsync();

                    DBcontext.CampusUserSubjects.RemoveRange(dataToRemove);
                }

                if (toAdd.Any())
                {

                    foreach (var item in toAdd)
                    {
                        var data = new CampusUserSubject
                        {
                            SubjectId = item,
                            UserId = userId,
                            CampusId = user.CampusId
                        };

                        await DBcontext.CampusUserSubjects.AddAsync(data);
                    }
                }
                await this.DBcontext.SaveChangesAsync();

                await logRepository.LogAsync($"Change teaching subject of lecture {user.Mail}");

                return new RequestResponse
                {
                    IsSuccessful = true,
                    Message = "Save Successfully!",
                };
            }
            catch (Exception ex)
            {
                return new RequestResponse
                {
                    IsSuccessful = false,
                    Message = ex.Message,
                };
            }
        }
    }
}
