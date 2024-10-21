using ExcelDataReader;
using Library.Common;
using Library.Models;
using Library.Request;
using Library.Response;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using WebApi.IRepository;
using static MudBlazor.Colors;

namespace WebApi.Repository
{
    public class SubjectRepository : ISubjectRepository
    {
        private readonly QuizManagementContext DBcontext;

        public SubjectRepository(QuizManagementContext DBcontext)
        {
            this.DBcontext = DBcontext;
        }

        public async Task<RequestResponse> AddSubject(Subject req)
        {
            try
            {
                var data = await this.DBcontext.Subjects.FirstOrDefaultAsync(x => x.SubjectCode.Equals(req.SubjectCode) && x.IsDeleted != true);

                if (data == null)
                {
                    await this.DBcontext.Subjects.AddAsync(req);

                    await this.DBcontext.SaveChangesAsync();

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
                var data = await this.DBcontext.Subjects.FirstOrDefaultAsync(x => x.SubjectId == subjectId && x.IsDeleted != true);

                if (data != null)
                {
                    data.IsDeleted = true;

                    await this.DBcontext.SaveChangesAsync();

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
                var data = await this.DBcontext.Subjects.FirstOrDefaultAsync(x => x.SubjectId == subjectId && x.IsDeleted != true);

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
                var data = await this.DBcontext.Subjects.Where(x => x.IsDeleted != true).ToListAsync();

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
                var data = await this.DBcontext.Subjects.FirstOrDefaultAsync(x => x.SubjectId == req.SubjectId && x.IsDeleted != true);

                if (data != null)
                {
                    if (data.SubjectCode != req.SubjectCode)
                    {
                        var existingSubjectWithSameCode = await this.DBcontext.Subjects
                            .AnyAsync(x => x.SubjectCode == req.SubjectCode && x.SubjectId != req.SubjectId && x.IsDeleted != true);

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

                    await this.DBcontext.SaveChangesAsync();

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
                                (cus != null && cus.UserId == userId && cus.IsLecturer == false && cus.CampusId == campusId)
                                // Điều kiện 2: Hoặc các môn chưa có chủ nhiệm tại campus của user X
                                || (cus == null || !this.DBcontext.CampusUserSubjects
                                    .Any(other => other.SubjectId == s.SubjectId && other.IsLecturer == false && other.CampusId == campusId))
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
    }
}
