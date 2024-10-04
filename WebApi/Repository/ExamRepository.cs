using Azure;
using ExcelDataReader;
using Library.Common;
using Library.Models;
using Library.Models.Dtos;
using Library.Request;
using Library.Response;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Runtime.CompilerServices;
using WebApi.IRepository;

public class ExamRepository : IExamRepository
{
    private readonly QuizManagementContext _context;

    public ExamRepository(QuizManagementContext context)
    {
        _context = context;
    }

    public async Task<RequestResponse> ChangeStatusExam(List<TestDepartmentExamResponse> exam)
    {
        try
        {
            var examIds = exam.Select(x => x.ExamId).ToList();

            var examsToUpdate = await this._context.Exams
                .Where(x => examIds.Contains(x.ExamId))
                .ToListAsync();

            if (examsToUpdate.Count != examIds.Count)
            {
                return new RequestResponse
                {
                    IsSuccessful = false,
                    Message = "Error! Some exams do not exist.",
                };
            }

            foreach (var e in examsToUpdate)
            {
                e.ExamStatusId = 2;
                e.UpdateDate = DateTime.Now;
            }

            await this._context.SaveChangesAsync();

            return new RequestResponse
            {
                IsSuccessful = true,
                Message = "Update Exam Status Successfully",
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

    public async Task<RequestResponse> CreateExam(ExamCreateRequest exam)
    {
        try
        {
            var e = await this._context.Exams.FirstOrDefaultAsync(x => x.ExamCode == exam.ExamCode);

            if (e == null)
            {
                var newExam = new Exam
                {
                    ExamCode = exam.ExamCode,
                    CampusId = exam.CampusId,
                    CreateDate = DateTime.Now,
                    CreaterId = exam.CreaterId.Value,
                    EndDate = exam.EndDate,
                    StartDate = exam.StartDate,
                    EstimatedTimeTest = exam.EstimatedTimeTest,
                    ExamDuration = exam.ExamDuration,
                    ExamStatusId = 1,
                    ExamType = exam.ExamType,
                    SubjectId = exam.SubjectId,
                    UpdateDate = DateTime.Now,
                };

                await this._context.AddAsync(newExam);

                await this._context.SaveChangesAsync();
            }
            else
            {
                return new RequestResponse
                {
                    IsSuccessful = false,
                    Message = "Exam code is exist!",
                };
            }

            return new RequestResponse
            {
                IsSuccessful = true,
                Message = "Create exam successfully",
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

    public async Task<ResultResponse<TestDepartmentExamResponse>> GetExamById(int examId)
    {
        try
        {
            var data = (from ex in _context.Exams
                        join su in _context.Subjects on ex.SubjectId equals su.SubjectId
                        join ca in _context.Campuses on ex.CampusId equals ca.CampusId
                        join cus in _context.CampusUserSubjects
                            on new { ex.SubjectId, ex.CampusId } equals new { cus.SubjectId, cus.CampusId } into cusGroup
                        from cus in cusGroup.DefaultIfEmpty() // LEFT JOIN
                        join u1 in _context.Users on cus.UserId equals u1.UserId into u1Group
                        from u1 in u1Group.DefaultIfEmpty() // LEFT JOIN
                        join u2 in _context.Users on ex.CreaterId equals u2.UserId
                        join st in _context.ExamStatuses on ex.ExamStatusId equals st.ExamStatusId
                        where ex.ExamId == examId
                        select new TestDepartmentExamResponse
                        {
                            CreaterId = u2.UserId,
                            CreaterName = u2.Mail,
                            EndDate = ex.EndDate,
                            StartDate = ex.StartDate,
                            ExamCode = ex.ExamCode,
                            SubjectCode = su.SubjectCode,
                            CampusId = ca.CampusId,
                            CampusName = ca.CampusName,
                            EstimatedTimeTest = ex.EstimatedTimeTest,
                            ExamDuration = ex.ExamDuration,
                            ExamId = ex.ExamId,
                            ExamStatusContent = st.StatusContent,
                            ExamStatusId = st.ExamStatusId,
                            ExamType = ex.ExamType,
                            HeadDepartmentId = u1.UserId,
                            HeadDepartmentName = u1.Mail,
                            SubjectId = su.SubjectId,
                            SubjectName = su.SubjectName,
                            UpdateDate = ex.UpdateDate,
                        }).FirstOrDefault();

            return new ResultResponse<TestDepartmentExamResponse>
            {
                IsSuccessful = true,
                Item = data,
            };
        }
        catch (Exception ex)
        {
            return new ResultResponse<TestDepartmentExamResponse>
            {
                IsSuccessful = false,
                Message = ex.Message,
            };
        }
    }

    public async Task<IEnumerable<ExamInfoDto>> GetExamInfoAsync()
    {
        var examInfo = await _context.InstructorAssignments
            .Include(ia => ia.Exam) // Kết nối với Exam
            .Include(ia => ia.Exam.Subject) // Kết nối với Subject từ Exam
            .Include(ia => ia.Exam.ExamStatus) // Kết nối với ExamStatuses
                                               //.Include(ia => ia.AssignedToNavigation) // Kết nối với giảng viên
            .Select(ia => new ExamInfoDto
            {
                //DepartmentName = ia.Exam.Subject.Department.DepartmentName, // Tên Chuyên Ngành
                SubjectName = ia.Exam.Subject.SubjectName, // Tên Môn Học
                ExamCode = ia.Exam.ExamCode, // Mã Bài Thi
                Status = ia.Exam.ExamStatus.StatusContent, // Trạng Thái
                                                           //InstructorName = ia.AssignedToNavigation.Mail // Tên Giảng Viên
            })
            .ToListAsync();

        return examInfo;
    }

    public async Task<ResultResponse<TestDepartmentExamResponse>> GetExamList(ExamSearchRequest req)
      {
          try
          {
            var data = (from ex in _context.Exams
                        join su in _context.Subjects on ex.SubjectId equals su.SubjectId
                        join ca in _context.Campuses on ex.CampusId equals ca.CampusId
                        join cus in _context.CampusUserSubjects
                            on new { ex.SubjectId, ex.CampusId } equals new { cus.SubjectId, cus.CampusId } into cusGroup
                        from cus in cusGroup.DefaultIfEmpty() // LEFT JOIN
                        join u1 in _context.Users on cus.UserId equals u1.UserId into u1Group
                        from u1 in u1Group.DefaultIfEmpty() // LEFT JOIN
                        join st in _context.ExamStatuses on ex.ExamStatusId equals st.ExamStatusId
                        where (req.StatusId == null || ex.ExamStatusId == req.StatusId)
                              && (string.IsNullOrEmpty(req.ExamCode) || ex.ExamCode.Contains(req.ExamCode))
                        select new TestDepartmentExamResponse
                        {
                            EndDate = ex.EndDate,
                            ExamId = ex.ExamId,
                            StartDate = ex.StartDate,
                            ExamCode = ex.ExamCode,
                            CampusName = ca.CampusName,
                            EstimatedTimeTest = ex.EstimatedTimeTest,
                            ExamStatusContent = st.StatusContent,
                            ExamStatusId = st.ExamStatusId,
                            HeadDepartmentName = u1.Mail,
                            HeadDepartmentId = u1.UserId,
                            UpdateDate = ex.UpdateDate
                        }).ToList();

            return new ResultResponse<TestDepartmentExamResponse>
              {
                  IsSuccessful = true,
                  Items = data.OrderByDescending(x => x.UpdateDate).ToList(),
              };
          }
          catch (Exception ex)
          {
              return new ResultResponse<TestDepartmentExamResponse>
              {
                  IsSuccessful = false,
                  Message = ex.Message,
              };
          }
      }

    public async Task<RequestResponse> UpdateExam(TestDepartmentExamResponse exam)
    {
        try
        {
            var e = await this._context.Exams.FirstOrDefaultAsync(x => x.ExamId == exam.ExamId);

            if (e == null)
            {
                return new RequestResponse
                {
                    IsSuccessful = false,
                    Message = "Exam not exist!",
                };
            }
            else
            {
                e.EndDate = exam.EndDate;
                e.StartDate = exam.StartDate;
                e.ExamCode = exam.ExamCode;
                e.CampusId = exam.CampusId.Value;
                e.ExamDuration = exam.ExamDuration;
                e.ExamType = exam.ExamType;
                e.UpdateDate = DateTime.Now;

                await this._context.SaveChangesAsync();

                return new RequestResponse
                {
                    IsSuccessful = true,
                    Message = "Update Successfully",
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
    public async Task<RequestResponse> ImportExamsFromCsv(List<ExamImportRequest> examImportDtos)
    {
        throw new NotImplementedException();
    }


    public async Task<RequestResponse> ImportExamsFromExcel(IFormFile file)
    {
        var response = new RequestResponse();
        var errors = new List<string>();
        var examsToAdd = new List<Exam>();

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

            // Thiết lập thư mục lưu file upload
            var uploadsFolder = $"{Directory.GetCurrentDirectory()}\\Uploads";
            if (!Directory.Exists(uploadsFolder))
            {
                Directory.CreateDirectory(uploadsFolder);
            }

            // Đường dẫn file (thay đổi từ file.Name thành file.FileName để đảm bảo an toàn)
            var filePath = Path.Combine(uploadsFolder, Path.GetFileName(file.FileName)); // Sử dụng Path.GetFileName

            // Ghi file lên server
            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(stream); // Sử dụng CopyToAsync để ghi file một cách bất đồng bộ
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

                            // Sử dụng DTO để lưu dữ liệu từ Excel
                            var examImportRequest = new ExamImportRequest
                            {
                                ExamCode = reader.GetValue(0)?.ToString(),
                                ExamDuration = reader.GetValue(1)?.ToString(),
                                ExamType = reader.GetValue(2)?.ToString(),
                                CampusName = reader.GetValue(3)?.ToString(),
                                SubjectCode = reader.GetValue(4)?.ToString(),
                                CreaterName = reader.GetValue(5)?.ToString(),
                                StatusContent = reader.GetValue(6)?.ToString(),
                                EstimatedTimeTest = DateTime.TryParse(reader.GetValue(7)?.ToString(), out DateTime estimatedTime) ? estimatedTime : (DateTime?)null,
                                StartDate = DateTime.TryParse(reader.GetValue(8)?.ToString(), out DateTime startDate) ? startDate : (DateTime?)null,
                                EndDate = DateTime.TryParse(reader.GetValue(9)?.ToString(), out DateTime endDate) ? endDate : (DateTime?)null
                            };

                            // Kiểm tra tính hợp lệ của dữ liệu từ DTO
                            var errorMessages = new List<string>();

                            if (string.IsNullOrEmpty(examImportRequest.ExamCode))
                                errorMessages.Add("ExamCode không được để trống.");

                            if (string.IsNullOrEmpty(examImportRequest.ExamDuration))
                                errorMessages.Add("ExamDuration không được để trống.");

                            if (string.IsNullOrEmpty(examImportRequest.ExamType))
                                errorMessages.Add("ExamType không được để trống.");

                            var campus = await _context.Campuses.FirstOrDefaultAsync(c => c.CampusName == examImportRequest.CampusName);
                            var subject = await _context.Subjects.FirstOrDefaultAsync(s => s.SubjectCode == examImportRequest.SubjectCode);
                            var creator = await _context.Users.FirstOrDefaultAsync(u => u.Mail == examImportRequest.CreaterName);

                            if (campus == null)
                                errorMessages.Add($"Campus với tên là  {examImportRequest.CampusName} không tồn tại.");
                            if (subject == null)
                                errorMessages.Add($"Subject với mã môn là {examImportRequest.SubjectCode} không tồn tại.");
                            if (creator == null)
                                errorMessages.Add($"Creator với mail là  {examImportRequest.CreaterName} không tồn tại.");

                            var existingExam = await _context.Exams.FirstOrDefaultAsync(e => e.ExamCode == examImportRequest.ExamCode);
                            if (existingExam != null)
                                errorMessages.Add($"Exam với mã {examImportRequest.ExamCode} đã tồn tại.");

                            if (errorMessages.Any())
                            {
                                errors.Add($"Lỗi với ExamCode {examImportRequest.ExamCode} : {string.Join(", ", errorMessages)}");
                                continue;
                            }

                            // Nếu không có lỗi, ánh xạ từ DTO sang model Exam
                            var exam = new Exam
                            {
                                ExamCode = examImportRequest.ExamCode,
                                ExamDuration = examImportRequest.ExamDuration,
                                ExamType = examImportRequest.ExamType,
                                CampusId = campus.CampusId,
                                SubjectId = subject.SubjectId,
                                CreaterId = creator.UserId,
                                ExamStatusId = null, 
                                EstimatedTimeTest = examImportRequest.EstimatedTimeTest,
                                StartDate = examImportRequest.StartDate,
                                EndDate = examImportRequest.EndDate
                            };

                            examsToAdd.Add(exam);
                        }
                    } while (reader.NextResult());
                }
            }

            // Lưu các exam hợp lệ
            if (examsToAdd.Any())
            {
                await _context.Exams.AddRangeAsync(examsToAdd);
                await _context.SaveChangesAsync();
            }
            else
            {
                // Thêm phản hồi nếu không có dữ liệu hợp lệ
                response.IsSuccessful = false;
                response.Message = "Không có dữ liệu hợp lệ để nhập.";
            }

            if (errors.Any())
            {
                response.IsSuccessful = false;
                response.Message = $"Còn tồn tại các lỗi sau: {string.Join("; ", errors)}";
            }
            else
            {
                response.IsSuccessful = true;
                response.Message = "Import exams from Excel successfully.";
            }
        }
        catch (Exception ex)
        {
            response.IsSuccessful = false;
            response.Message = $"Có lỗi xảy ra: {ex.Message}";
        }

        return response;
    }

    public Task<ResultResponse<ExamExportResponse>> ExportExamsToCsv()
    {
        throw new NotImplementedException();
    }

    public Task<ResultResponse<ExamExportResponse>> ExportExamsToExcel()
    {
        throw new NotImplementedException();
    }
    
    
    
    
}
