﻿using ExcelDataReader;
using Library.Common;
using Library.Models;
using Library.Models.Dtos;
using Library.Request;
using Library.Response;
using Microsoft.EntityFrameworkCore;
using WebApi.IRepository;

public class ExamRepository : IExamRepository
{
    private readonly QuizManagementContext _context;

    public ExamRepository(QuizManagementContext context)
    {
        _context = context;
    }

    public async Task<RequestResponse> ChangeStatusExam(List<ExaminerExamResponse> exam)
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

    public async Task<RequestResponse> ChangeStatusExamById(int examId, int statusId)
    {
        try
        {
            var exam = await this._context.Exams.FirstOrDefaultAsync(x => x.ExamId == examId);

            if (exam == null)
            {
                return new RequestResponse
                {
                    IsSuccessful = false,
                    Message = "Exam not exist!",
                };
            }

            exam.ExamStatusId = statusId;

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

    public async Task<ResultResponse<ExaminerExamResponse>> GetExamById(int examId)
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
                        select new ExaminerExamResponse
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

            return new ResultResponse<ExaminerExamResponse>
            {
                IsSuccessful = true,
                Item = data,
            };
        }
        catch (Exception ex)
        {
            return new ResultResponse<ExaminerExamResponse>
            {
                IsSuccessful = false,
                Message = ex.Message,
            };
        }
    }

    public async Task<ResultResponse<LeaderExamResponse>> GetLeaderExamById(int examId)
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
                        select new LeaderExamResponse
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
                            LectureList = (from ia in _context.Exams
                                           join u3 in _context.Users on ia.AssignedUserId equals u3.UserId
                                           where ia.ExamId == ex.ExamId
                                           select new UserResponse
                                           {
                                               UserId = u3.UserId,
                                               Email = u3.Mail
                                           }).ToList(),
                            UpdateDate = ex.UpdateDate,
                        }).FirstOrDefault();

            return new ResultResponse<LeaderExamResponse>
            {
                IsSuccessful = true,
                Item = data,
            };
        }
        catch (Exception ex)
        {
            return new ResultResponse<LeaderExamResponse>
            {
                IsSuccessful = false,
                Message = ex.Message,
            };
        }
    }

    public async Task<ResultResponse<LectureExamResponse>> GetLectureExamById(int examId, int userId)
    {
        try
        {
            var data = await (from ex in _context.Exams
                              join su in _context.Subjects on ex.SubjectId equals su.SubjectId
                              join ca in _context.Campuses on ex.CampusId equals ca.CampusId
                              join cus in _context.CampusUserSubjects
                                  on new { ex.SubjectId, ex.CampusId } equals new { cus.SubjectId, cus.CampusId } into cusGroup
                              from cus in cusGroup.DefaultIfEmpty() // LEFT JOIN
                              join u1 in _context.Users on cus.UserId equals u1.UserId into u1Group
                              from u1 in u1Group.DefaultIfEmpty() // LEFT JOIN
                              join u2 in _context.Users on ex.CreaterId equals u2.UserId
                              where ex.ExamId == examId && ex.AssignedUserId == userId
                              select new LectureExamResponse
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
                                  AssignStatusId = ex.ExamStatusId,
                                  AssignStatusContent = ex.ExamStatus.StatusContent,
                                  AssignmentDate = ex.AssignmentDate,
                                  ExamType = ex.ExamType,
                                  HeadDepartmentId = u1.UserId,
                                  HeadDepartmentName = u1.Mail,
                                  SubjectId = su.SubjectId,
                                  SubjectName = su.SubjectName,
                                  Summary = ex.GeneralFeedback,
                                  ReportList = (from rp in _context.Reports
                                                where rp.ExamId == ex.ExamId
                                                select new ReportResponse
                                                {
                                                    ReportId = rp.ReportId,
                                                    QuestionNumber = rp.QuestionNumber,
                                                    QuestionSolutionDetail = rp.QuestionSolutionDetail,
                                                    ReportContent = rp.ReportContent,
                                                }).ToList(),
                                  UpdateDate = ex.UpdateDate,
                              }).FirstOrDefaultAsync();

            return new ResultResponse<LectureExamResponse>
            {
                IsSuccessful = true,
                Item = data,
            };
        }
        catch (Exception ex)
        {
            return new ResultResponse<LectureExamResponse>
            {
                IsSuccessful = false,
                Message = ex.Message,
            };
        }
    }

    public async Task<IEnumerable<ExamInfoDto>> GetExamInfoAsync()
    {
        var examInfo = await _context.Exams
            .Include(ia => ia.Subject) // Kết nối với Subject từ Exam
            .Include(ia => ia.ExamStatus) // Kết nối với ExamStatuses
                                          //.Include(ia => ia.AssignedToNavigation) // Kết nối với giảng viên
            .Select(ia => new ExamInfoDto
            {
                //DepartmentName = ia.Exam.Subject.Department.DepartmentName, // Tên Chuyên Ngành
                SubjectName = ia.Subject.SubjectName, // Tên Môn Học
                ExamCode = ia.ExamCode, // Mã Bài Thi
                Status = ia.ExamStatus.StatusContent, // Trạng Thái
                                                      //InstructorName = ia.AssignedToNavigation.Mail // Tên Giảng Viên
            })
            .ToListAsync();

        return examInfo;
    }

    public async Task<ResultResponse<ExaminerExamResponse>> GetExamList(ExamSearchRequest req)
    {
        try
        {
            var data = (from ex in _context.Exams
                        join su in _context.Subjects on ex.SubjectId equals su.SubjectId
                        join ca in _context.Campuses on ex.CampusId equals ca.CampusId
                        join sem in _context.Semesters on ex.SemesterId equals sem.SemesterId
                        join cus in _context.CampusUserSubjects
                            on new { ex.SubjectId, ex.CampusId } equals new { cus.SubjectId, cus.CampusId } into cusGroup
                        from cus in cusGroup.DefaultIfEmpty() // LEFT JOIN
                        join u1 in _context.Users on cus.UserId equals u1.UserId into u1Group
                        from u1 in u1Group.DefaultIfEmpty() // LEFT JOIN
                        join st in _context.ExamStatuses on ex.ExamStatusId equals st.ExamStatusId
                        where (req.StatusId == null || ex.ExamStatusId == req.StatusId)
                        &&(req.SemesterId == null || sem.SemesterId == req.SemesterId)
                              && (string.IsNullOrEmpty(req.ExamCode) || ex.ExamCode.ToLower().Contains(req.ExamCode.ToLower()))
                              && cus.IsLecturer == false
                        select new ExaminerExamResponse
                        {
                            SemseterName = sem.SemesterName,
                            EndDate = ex.EndDate,
                            ExamId = ex.ExamId,
                            ExamDate = ex.ExamDate,
                            StartDate = ex.StartDate,
                            ExamCode = ex.ExamCode,
                            CampusName = ca.CampusName,
                            ExamType = ex.ExamType,
                            ExamStatusContent = st.StatusContent,
                            ExamStatusId = st.ExamStatusId,
                            HeadDepartmentName = u1.Mail,
                            HeadDepartmentId = u1.UserId,
                            UpdateDate = ex.UpdateDate
                        }).ToList();

            return new ResultResponse<ExaminerExamResponse>
            {
                IsSuccessful = true,
                Items = data.OrderByDescending(x => x.UpdateDate).ToList(),
            };
        }
        catch (Exception ex)
        {
            return new ResultResponse<ExaminerExamResponse>
            {
                IsSuccessful = false,
                Message = ex.Message,
            };
        }
    }

    public async Task<ResultResponse<LeaderExamResponse>> GetLeaderExamList(ExamSearchRequest req)
    {
        try
        {
            var data = await (from ex in _context.Exams
                              join su in _context.Subjects on ex.SubjectId equals su.SubjectId
                              join ca in _context.Campuses on ex.CampusId equals ca.CampusId
                              join sem in _context.Semesters on ex.SemesterId equals sem.SemesterId
                              join cus in _context.CampusUserSubjects
                                  on new { ex.SubjectId, ex.CampusId } equals new { cus.SubjectId, cus.CampusId } into cusGroup
                              from cus in cusGroup.DefaultIfEmpty() // LEFT JOIN
                              join u1 in _context.Users on cus.UserId equals u1.UserId into u1Group
                              from u1 in u1Group.DefaultIfEmpty() // LEFT JOIN
                              join st in _context.ExamStatuses on ex.ExamStatusId equals st.ExamStatusId
                              where ((req.StatusId == null && ex.ExamStatusId != 1) || ex.ExamStatusId == req.StatusId)
                                    && (string.IsNullOrEmpty(req.ExamCode) || ex.ExamCode.ToLower().Contains(req.ExamCode.ToLower()))
                                    && req.UserId == u1.UserId
                                    && cus.IsLecturer == false
                              select new LeaderExamResponse
                              {
                                  SemesterName = sem.SemesterName,
                                  EndDate = ex.EndDate,
                                  ExamId = ex.ExamId,
                                  StartDate = ex.StartDate,
                                  ExamDate = ex.ExamDate,
                                  ExamCode = ex.ExamCode,
                                  CampusName = ca.CampusName,
                                  EstimatedTimeTest = ex.EstimatedTimeTest,
                                  ExamStatusContent = st.StatusContent,
                                  ExamStatusId = st.ExamStatusId,
                                  HeadDepartmentName = u1.Mail,
                                  HeadDepartmentId = u1.UserId,
                                  UpdateDate = ex.UpdateDate
                              }).ToListAsync();

            return new ResultResponse<LeaderExamResponse>
            {
                IsSuccessful = true,
                Items = data.OrderByDescending(x => x.UpdateDate).ToList(),
            };
        }
        catch (Exception ex)
        {
            return new ResultResponse<LeaderExamResponse>
            {
                IsSuccessful = false,
                Message = ex.Message,
            };
        }
    }

    public async Task<ResultResponse<LectureExamResponse>> GetLectureExamList(ExamSearchRequest req)
    {
        try
        {
            var data = await (from ex in _context.Exams
                              join su in _context.Subjects on ex.SubjectId equals su.SubjectId
                              join ca in _context.Campuses on ex.CampusId equals ca.CampusId
                              join cus in _context.CampusUserSubjects
                                  on new { ex.SubjectId, ex.CampusId } equals new { cus.SubjectId, cus.CampusId } into cusGroup
                              from cus in cusGroup.DefaultIfEmpty() // LEFT JOIN
                              join u1 in _context.Users on cus.UserId equals u1.UserId into u1Group
                              from u1 in u1Group.DefaultIfEmpty() // LEFT JOIN
                              where ((req.StatusId == null && ex.ExamStatusId != 1 && ex.ExamStatusId != 2) || ex.ExamStatusId == req.StatusId)
                              && (string.IsNullOrEmpty(req.ExamCode) || ex.ExamCode.ToLower().Contains(req.ExamCode.ToLower()))
                              && cus.IsLecturer == false
                              select new LectureExamResponse
                              {
                                  EndDate = ex.EndDate,
                                  ExamId = ex.ExamId,
                                  StartDate = ex.StartDate,
                                  ExamCode = ex.ExamCode,
                                  CampusName = ca.CampusName,
                                  EstimatedTimeTest = ex.EstimatedTimeTest,
                                  AssignStatusContent = ex.ExamStatus.StatusContent,
                                  AssignStatusId = ex.ExamStatusId,
                                  HeadDepartmentName = u1.Mail,
                                  HeadDepartmentId = u1.UserId,
                                  UpdateDate = ex.UpdateDate
                              }).ToListAsync();

            return new ResultResponse<LectureExamResponse>
            {
                IsSuccessful = true,
                Items = data.OrderByDescending(x => x.UpdateDate).ToList(),
            };
        }
        catch (Exception ex)
        {
            return new ResultResponse<LectureExamResponse>
            {
                IsSuccessful = false,
                Message = ex.Message,
            };
        }
    }

    public async Task<RequestResponse> UpdateExam(ExaminerExamResponse exam)
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
        // Tạo một HashSet để theo dõi các bản ghi đã được thêm
        var existingExamSet = new HashSet<string>();

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
            var uploadsFolder = $"{Directory.GetCurrentDirectory()}\\Uploads\\Exams";
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
                                EstimatedTimeTest = DateTime.TryParse(reader.GetValue(6)?.ToString(), out DateTime estimatedTime) ? estimatedTime : (DateTime?)null,
                                StartDate = DateTime.TryParse(reader.GetValue(7)?.ToString(), out DateTime startDate) ? startDate : (DateTime?)null,
                                EndDate = DateTime.TryParse(reader.GetValue(8)?.ToString(), out DateTime endDate) ? endDate : (DateTime?)null
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
                            //tạo khoá duy nhất cho mỗi exam
                            string uniquekey = examImportRequest.ExamCode;
                            if (existingExamSet.Contains(uniquekey))
                            {
                                errorMessages.Add($"Duplicate entry for ExamCode '{examImportRequest.ExamCode}'.");
                                continue;
                            }
                            existingExamSet.Add(uniquekey);
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

	//exam by status
	// exam by status
	public async Task<(IEnumerable<ExamByStatusResponse> Exams, int Count)> GetExamsByStatus(int? statusId = null, int? campusId = null)
	{
		IQueryable<Exam> query = _context.Exams
			.Include(e => e.Campus)
			.Include(e => e.Subject); // Đảm bảo bao gồm môn học

		if (statusId.HasValue)
		{
			query = query.Where(e => e.ExamStatusId == statusId.Value);
		}

		if (campusId.HasValue)
		{
			query = query.Where(e => e.CampusId == campusId.Value);
		}

		var results = await query.Select(e => new ExamByStatusResponse
		{
			ExamStatus = e.ExamStatus.StatusContent,
			ExamCode = e.ExamCode,
			Campus = e.Campus.CampusName,			
			Lecturer = string.Join(", ", _context.CampusUserSubjects
				.Where(cus => cus.SubjectId == e.SubjectId && cus.CampusId == e.CampusId && cus.IsLecturer == true)
				.Select(cus => cus.User.FullName))
		}).ToListAsync();

		return (results, results.Count);
	}



	public Task<ResultResponse<ExamExportResponse>> ExportExamsToCsv()
    {
        throw new NotImplementedException();
    }

    public Task<ResultResponse<ExamExportResponse>> ExportExamsToExcel()
    {
        throw new NotImplementedException();
    }

    public async Task<ResultResponse<CampusSubjectExamCodeResponse>> GetExamByCampusAndSubject(int campusId, int subjectId)
    {
        try
        {
            var exams = await _context.Exams
            .AsNoTracking() // Tăng hiệu suất nếu không cần cập nhật đối tượng
            .Where(e => e.CampusId == campusId && e.SubjectId == subjectId)
            .Include(e => e.Campus)
            .Include(e => e.Subject)
            .ToListAsync();


            // Kiểm tra xem có bài thi nào được tìm thấy hay không
            if (!exams.Any())
            {
                return new ResultResponse<CampusSubjectExamCodeResponse>
                {
                    IsSuccessful = false,
                    Message = "No exams found for the provided campus and subject."
                };
            }

            // Chuyển đổi dữ liệu từ Exam sang CampusSubjectExamCodeResponse

            /////cách 1:
            //// Tính số lượng mã đề cho từng tổ hợp môn và cơ sở
            //var examCodeCount = exams.Count();

            //// Tạo danh sách các mã đề
            //var examCodes = exams.Select(e => new CampusSubjectExamCodeResponse
            //{
            //    ExamCode = e.ExamCode,
            //    SubjectName = e.Subject != null ? e.Subject.SubjectCode : "No Subject Name",
            //    CampusName = e.Campus != null ? e.Campus.CampusName : "No Campus Name"
            //}).ToList();
            ////kiểu return của cách 1
            //return new ResultResponse<CampusSubjectExamResponse>
            //{
            //    IsSuccessful = true,
            //    Items = new List<CampusSubjectExamResponse>  
            //    {
            //        new CampusSubjectExamResponse
            //        {
            //            ExamCodes = examCodes,
            //            ExamCodeCount = examCodeCount
            //        }
            //    }
            //};

            //cách 2:
            var response = exams
           .GroupBy(e => new { e.CampusId, e.SubjectId }) // Nhóm theo CampusId và SubjectId
           .Select(g =>
           {
               var firstExam = g.FirstOrDefault(); // Lưu trữ phần tử đầu tiên
               return new CampusSubjectExamCodeResponse
               {
                   ExamCode = string.Join(", ", g.Select(e => e.ExamCode)),  // Gộp tất cả mã đề
                   SubjectName = firstExam != null && firstExam.Subject != null ? firstExam.Subject.SubjectCode : "No Subject Name",  // Kiểm tra null cho tên môn học
                   CampusName = firstExam != null && firstExam.Campus != null ? firstExam.Campus.CampusName : "No Campus Name",   // Kiểm tra null cho tên cơ sở
                   ExamCodeCount = g.Count()  // Đếm số lượng bài thi trong nhóm
               };
           })
           .ToList();
            //kiểu return của cách 2
            return new ResultResponse<CampusSubjectExamCodeResponse>
            {
                IsSuccessful = true,
                Items = response
            };

        }
        catch (Exception ex)
        {
            // Log hoặc xử lý lỗi
            return new ResultResponse<CampusSubjectExamCodeResponse>
            {
                IsSuccessful = false,
                Message = "An error occurred while fetching the exams. Please try again later."
            };
        }
    }

    // Tìm Môn theo kì và name
    public async Task<List<ExamBySemesterResponse>> ExamBySemesterNameAndUserId(int semesterId, int userId)
    {
        // Retrieve the full name of the user first
        var user = await _context.Users
            .Where(u => u.UserId == userId)
            .Select(u => new { u.FullName, u.RoleId })
            .FirstOrDefaultAsync();

        if (user == null)
        {

            return new List<ExamBySemesterResponse>();
        }

        // muốn hiển thị tìm kiếm được cả InstructorAssignment cho lecturer thì bỏ if.
        if (user.RoleId != 4)
        {
            return new List<ExamBySemesterResponse>();
        }

        var examAssignments = await _context.Exams
            .Include(e => e.Semester)
            .Include(e => e.Subject)
       .Where(e => e.SemesterId == semesterId && e.AssignedUserId == userId)
            .Select(e => new ExamBySemesterResponse
            {
                ExamCode = e.ExamCode,
                SubjectName = e.Subject.SubjectName,
                FullName = user.FullName,
                SemesterName = e.Semester.SemesterName
            })
            .ToListAsync();

        return examAssignments;
    }

    public Task<ResultResponse<LeaderExamResponse>> GetRemindExamList()
    {
        throw new NotImplementedException();
    }
}
