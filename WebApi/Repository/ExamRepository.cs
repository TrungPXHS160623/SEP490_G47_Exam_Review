using ExcelDataReader;
using Library.Common;
using Library.Models;
using Library.Models.Dtos;
using Library.Request;
using Library.Response;
using Microsoft.EntityFrameworkCore;
using System.Data;
using System.Globalization;
using System.Security.Claims;
using WebApi.IRepository;
using static Library.Response.CampusReportResponse;
using static Library.Response.CampusSubjectExamResponse;

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
            //Lấy tất cả các thằng mà đã đc mặc định nhận đề
            var selectedSubjects = await this._context.CampusUserSubjects
                .Where(x => x.IsSelect == true)
                .ToListAsync();

            foreach (var e in examsToUpdate)
            {
                var selectedSubject = selectedSubjects.FirstOrDefault(x => x.SubjectId == e.SubjectId);
                //Chuyển Sang trực tiếp cho thằnfg giangr viên mà đc mặc địjnh
                if (selectedSubject != null)
                {
                    e.AssignedUserId = selectedSubject.UserId;
                    e.ExamStatusId = 3;
                }
                //Chuyểnr đề sang cho cnbm
                else
                {
                    e.ExamStatusId = 2;
                }

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
                    ExamDate = exam.ExamDate,
                    ExamDuration = exam.ExamDuration,
                    TermDuration = exam.TermDuration,
                    SemesterId = exam.SemesterId,
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
                        join cus in _context.CampusUserFaculties
                            on new { ex.CampusId } equals new { cus.CampusId } into cusGroup
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
                        join u3 in _context.Users on ex.AssignedUserId equals u3.UserId into u3Group
                        from u3 in u3Group.DefaultIfEmpty() // LEFT JOIN
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
                            AssignedLectureId = u3.UserId,
                            AssignedLectureName = u3.Mail,
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

    public async Task<ResultResponse<LectureExamResponse>> GetLectureExamById(int examId)
    {
        try
        {
            var data = await (from ex in _context.Exams
                              join su in _context.Subjects on ex.SubjectId equals su.SubjectId
                              join ca in _context.Campuses on ex.CampusId equals ca.CampusId
                              join cus in _context.CampusUserFaculties
                                  on new { su.FacultyId, ex.CampusId } equals new { cus.FacultyId, cus.CampusId } into cusGroup
                              from cus in cusGroup.DefaultIfEmpty() // LEFT JOIN
                              join u1 in _context.Users on cus.UserId equals u1.UserId into u1Group
                              from u1 in u1Group.DefaultIfEmpty() // LEFT JOIN
                              join u2 in _context.Users on ex.CreaterId equals u2.UserId
                              join u3 in _context.Users on ex.AssignedUserId equals u3.UserId
                              where ex.ExamId == examId
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
                                  AssignmentUserName = u3.Mail,
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
                                                    ImageList = (from rf in _context.ReportFiles
                                                                 where rf.ReportId == rp.ReportId
                                                                 select new FileReponse
                                                                 {
                                                                     FileId = rf.FileId,
                                                                     FileData = rf.FilePath,
                                                                 }).ToList()
                                                }).ToList(),
                                  UpdateDate = ex.UpdateDate,
                              }).FirstOrDefaultAsync();


            var resutl = (from rp in _context.Reports
                          where rp.ExamId == data.ExamId
                          select new ReportResponse
                          {
                              ReportId = rp.ReportId,
                              QuestionNumber = rp.QuestionNumber,
                              QuestionSolutionDetail = rp.QuestionSolutionDetail,
                              ReportContent = rp.ReportContent,
                          }).ToList();
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
                        join cuf in _context.CampusUserFaculties
                            on new { su.FacultyId, ex.CampusId } equals new { cuf.FacultyId, cuf.CampusId } into cusGroup
                        from cuf in cusGroup.DefaultIfEmpty() // LEFT JOIN
                        join u1 in _context.Users on cuf.UserId equals u1.UserId into u1Group
                        from u1 in u1Group.DefaultIfEmpty() // LEFT JOIN
                        join st in _context.ExamStatuses on ex.ExamStatusId equals st.ExamStatusId
                        where (req.StatusId == null || ex.ExamStatusId == req.StatusId)
                              &&(req.SemesterId == null || sem.SemesterId == req.SemesterId)
                              && (string.IsNullOrEmpty(req.ExamCode) || ex.ExamCode.ToLower().Contains(req.ExamCode.ToLower()))
                              && ex.CampusId == req.CampusId
                        select new ExaminerExamResponse
                        {
                            SemseterName = sem.SemesterName,
                            EndDate = ex.EndDate,
                            ExamId = ex.ExamId,
                            StartDate = ex.StartDate,
                            ExamDate = ex.ExamDate,
                            EstimatedTimeTest = ex.EstimatedTimeTest,
                            ExamCode = ex.ExamCode,
                            CampusName = ca.CampusName,
                            ExamType = ex.ExamType,
                            ExamStatusContent = st.StatusContent,
                            ExamStatusId = st.ExamStatusId,
                            HeadDepartmentName = u1.Mail,
                            HeadDepartmentId = u1.UserId,
                            UpdateDate = ex.UpdateDate
                        }).Distinct().ToList();

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

            var data = await (from e in _context.Exams
                              join u1 in _context.Users on e.AssignedUserId equals u1.UserId into u1Join
                              from u1 in u1Join.DefaultIfEmpty()
                              join sj in _context.Subjects on e.SubjectId equals sj.SubjectId
                              join c in _context.Campuses on e.CampusId equals c.CampusId
                              join s in _context.Semesters on e.SemesterId equals s.SemesterId
                              join es in _context.ExamStatuses on e.ExamStatusId equals es.ExamStatusId
                              join cus in _context.CampusUserSubjects on sj.SubjectId equals cus.SubjectId
                              where (cus.UserId ==req.UserId)
                              && e.ExamStatusId != 1
                              && (req.StatusId == null || e.ExamStatusId == req.StatusId)
                              && (req.SemesterId == null || s.SemesterId == req.SemesterId)
                              && (string.IsNullOrEmpty(req.ExamCode) || e.ExamCode.ToLower().Contains(req.ExamCode.ToLower()))
                              select new LeaderExamResponse
                              {
                                  SemesterName = s.SemesterName,
                                  EndDate = e.EndDate,
                                  ExamId = e.ExamId,
                                  StartDate = e.StartDate,
                                  ExamCode = e.ExamCode,
                                  CampusName = c.CampusName,
                                  EstimatedTimeTest = e.EstimatedTimeTest,
                                  ExamStatusContent = es.StatusContent,
                                  ExamStatusId = es.ExamStatusId,
                                  AssignedLectureId = u1.UserId,
                                  AssignedLectureName = u1.Mail,
                                  UpdateDate = e.UpdateDate
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
    public async Task<ResultResponse<LeaderExamResponse>> GetAdminExamList(ExamSearchRequest req)
    {
        try
        {

            var data = await (from e in _context.Exams
                              join u1 in _context.Users on e.AssignedUserId equals u1.UserId into u1Join
                              from u1 in u1Join.DefaultIfEmpty()
                              join sj in _context.Subjects on e.SubjectId equals sj.SubjectId
                              join c in _context.Campuses on e.CampusId equals c.CampusId
                              join s in _context.Semesters on e.SemesterId equals s.SemesterId
                              join es in _context.ExamStatuses on e.ExamStatusId equals es.ExamStatusId
                              where e.ExamStatusId != 1
                              && (req.StatusId == null || e.ExamStatusId == req.StatusId)
                              && (req.SemesterId == null || s.SemesterId == req.SemesterId)
                              && (string.IsNullOrEmpty(req.ExamCode) || e.ExamCode.ToLower().Contains(req.ExamCode.ToLower()))
                              select new LeaderExamResponse
                              {
                                  SemesterName = s.SemesterName,
                                  EndDate = e.EndDate,
                                  ExamId = e.ExamId,
                                  StartDate = e.StartDate,
                                  ExamCode = e.ExamCode,
                                  CampusName = c.CampusName,
                                  EstimatedTimeTest = e.EstimatedTimeTest,
                                  ExamStatusContent = es.StatusContent,
                                  ExamStatusId = es.ExamStatusId,
                                  AssignedLectureId = u1.UserId,
                                  AssignedLectureName = u1.Mail,
                                  UpdateDate = e.UpdateDate
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

            var data = await (from e in _context.Exams
                              join sj in _context.Subjects on e.SubjectId equals sj.SubjectId
                              join f in _context.Faculties on sj.FacultyId equals f.FacultyId
                              join cuf in _context.CampusUserFaculties on f.FacultyId equals cuf.FacultyId
                              join u in _context.Users on cuf.UserId equals u.UserId
                              join c in _context.Campuses on e.CampusId equals c.CampusId
                              join s in _context.Semesters on e.SemesterId equals s.SemesterId
                              join es in _context.ExamStatuses on e.ExamStatusId equals es.ExamStatusId
                              join cus in _context.CampusUserSubjects on sj.SubjectId equals cus.SubjectId
                              where e.AssignedUserId == req.UserId
                              && e.ExamStatusId != 1 && e.ExamStatusId != 2
                              && (req.StatusId == null || e.ExamStatusId == req.StatusId)
                              && (req.SemesterId == null || s.SemesterId == req.SemesterId)
                              && (string.IsNullOrEmpty(req.ExamCode) || e.ExamCode.ToLower().Contains(req.ExamCode.ToLower()))
                              select new LectureExamResponse
                              {
                                  EndDate = e.EndDate,
                                  ExamId = e.ExamId,
                                  StartDate = e.StartDate,
                                  ExamCode = e.ExamCode,
                                  CampusName = c.CampusName,
                                  EstimatedTimeTest = e.EstimatedTimeTest,
                                  AssignStatusContent = e.ExamStatus.StatusContent,
                                  AssignStatusId = e.ExamStatusId,
                                  HeadDepartmentName = u.Mail,
                                  HeadDepartmentId = u.UserId,
                                  UpdateDate = e.UpdateDate
                              }).Distinct().ToListAsync();

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
    public async Task<ResultResponse<AccountClaims>> GetCurrentUserInfoAsync(ClaimsPrincipal currentUser)
    {
        // Lấy thông tin người dùng hiện tại từ Claims
        var userId = int.TryParse(currentUser.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value, out var id) ? id : 0;

        // Có được id của người dùng từ hệ thống thì liên kết tới database
        var myUser = await _context.Users.FirstOrDefaultAsync(u => u.UserId == userId);
        if (myUser == null)
        {
            return new ResultResponse<AccountClaims>
            {
                IsSuccessful = false,
                Message = "User not found."
            };
        }

        // Lấy RoleId và CampusId từ đối tượng người dùng
        var currentUserRoleId = myUser.RoleId;
        var currentUserCampusId = myUser.CampusId;

        // Tạo đối tượng AccountClaims
        var accountClaims = new AccountClaims
        {
            Id = userId,
            RoleId = currentUserRoleId,
            Email = myUser.Mail,
            FirstName = myUser.FullName,
            CampusId = currentUserCampusId
        };

        return new ResultResponse<AccountClaims>
        {
            IsSuccessful = true,
            Item = accountClaims
        };
    }
    public async Task<RequestResponse> ImportExamsFromExcel(IFormFile file, ClaimsPrincipal currentUser)
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

            //lấy id của khảo thí tạo đề
            var userInfoResponse = await GetCurrentUserInfoAsync(currentUser);
            if (!userInfoResponse.IsSuccessful)
            {
                return new ResultResponse<AccountClaims>
                {
                    IsSuccessful = false,
                    Message = "Failed to retrieve user information. Please try again later."
                };
            }
            var currentUserId = userInfoResponse.Item.Id;


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
                    // Chuyển đổi toàn bộ dữ liệu Excel thành DataSet
                    var dataSet = reader.AsDataSet();

                    foreach (DataTable table in dataSet.Tables)
                    {
                        // Kiểm tra nếu sheet không có dữ liệu (chỉ có header hoặc hoàn toàn trống)
                        if (table.Rows.Count <= 1)
                            continue;

                        var formats = new[]
                        {
                            "dd-MM-yyyy",
                            "d/M/yyyy",
                            "d/MM/yyyy",
                            "dd/MM/yyyy",
                            "MM-dd-yyyy",
                            "dd/MM/yyyy hh:mm:ss tt",
                            "d/MM/yyyy hh:mm:ss tt",
                            "dd/M/yyyy hh:mm:ss tt",
                            "d/M/yyyy hh:mm:ss tt"
                        };

                        for (int i = 1; i < table.Rows.Count; i++) // Bỏ qua header (dòng đầu tiên)
                        {
                            var row = table.Rows[i];
                            var errorMessages = new List<string>();

                            // Đọc và validate dữ liệu từ Excel
                            var examImportRequest = new ExamImportRequest
                            {
                                ExamCode = row[1]?.ToString(),
                                TermDuration = row[2]?.ToString(),
                                ExamType = row[3]?.ToString(),
                                CampusName = row[4]?.ToString(),
                                SubjectCode = row[5]?.ToString(),
                                ExamDuration = int.TryParse(row[6]?.ToString(), out var duration) ? (int?)duration : null,
                                SemesterName = row[9]?.ToString()
                            };

                            // Xử lý StartDate
                            var startDateString = row[7]?.ToString();
                            if (!string.IsNullOrEmpty(startDateString) &&
                                DateTime.TryParseExact(startDateString, formats, CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime parsedStartDate))
                            {
                                examImportRequest.StartDate = parsedStartDate;
                            }
                            else if (!string.IsNullOrEmpty(startDateString))
                            {
                                errorMessages.Add($"The StartDate '{startDateString}' is not valid.");
                            }

                            // Xử lý EndDate
                            var endDateString = row[8]?.ToString();
                            if (!string.IsNullOrEmpty(endDateString) &&
                                DateTime.TryParseExact(endDateString, formats, CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime parsedEndDate))
                            {
                                examImportRequest.EndDate = parsedEndDate;
                            }
                            else if (!string.IsNullOrEmpty(endDateString))
                            {
                                errorMessages.Add($"The EndDate '{endDateString}' is not valid.");
                            }

                            // Kiểm tra logic giữa StartDate và EndDate
                            if (examImportRequest.StartDate != null && examImportRequest.EndDate != null &&
                                examImportRequest.StartDate > examImportRequest.EndDate)
                            {
                                errorMessages.Add("StartDate must be earlier than EndDate.");
                            }

                            // Validate các trường không được để trống
                            if (string.IsNullOrEmpty(examImportRequest.ExamCode))
                                errorMessages.Add("ExamCode không được để trống.");
                            if (string.IsNullOrEmpty(examImportRequest.ExamType))
                                errorMessages.Add("ExamType không được để trống.");
                            if (string.IsNullOrEmpty(examImportRequest.TermDuration))
                                errorMessages.Add("TermDuration không được để trống.");
                            if (string.IsNullOrEmpty(examImportRequest.SemesterName))
                                errorMessages.Add("SemesterName không được để trống.");

                            // Kiểm tra trong database
                            var semester = await _context.Semesters.FirstOrDefaultAsync(s => s.SemesterName == examImportRequest.SemesterName);
                            var campus = await _context.Campuses.FirstOrDefaultAsync(c => c.CampusName == examImportRequest.CampusName);
                            var subject = await _context.Subjects.FirstOrDefaultAsync(s => s.SubjectCode == examImportRequest.SubjectCode);
                            if (campus == null)
                                errorMessages.Add($"Campus với tên là {examImportRequest.CampusName} không tồn tại.");
                            if (subject == null)
                                errorMessages.Add($"Subject với mã môn là {examImportRequest.SubjectCode} không tồn tại.");
                            if (semester == null)
                                errorMessages.Add($"Semester với tên là {examImportRequest.SemesterName} không tồn tại.");

                            // Kiểm tra trùng lặp mã ExamCode
                            string uniquekey = examImportRequest.ExamCode;
                            if (existingExamSet.Contains(uniquekey))
                            {
                                errorMessages.Add($"Duplicate entry for ExamCode '{examImportRequest.ExamCode}'.");
                            }
                            else
                            {
                                existingExamSet.Add(uniquekey);
                            }

                            var existingExam = await _context.Exams.FirstOrDefaultAsync(e => e.ExamCode == examImportRequest.ExamCode);
                            if (existingExam != null)
                            {
                                errorMessages.Add($"Exam với mã {examImportRequest.ExamCode} đã tồn tại.");
                            }

                            // Nếu có lỗi, thêm vào danh sách lỗi
                            if (errorMessages.Any())
                            {
                                errors.Add($"Lỗi với ExamCode {examImportRequest.ExamCode} : {string.Join(", ", errorMessages)}");
                                continue;
                            }

                            // Thêm vào danh sách examsToAdd nếu hợp lệ
                            var exam = new Exam
                            {
                                ExamCode = examImportRequest.ExamCode,
                                TermDuration = examImportRequest.TermDuration,
                                ExamType = examImportRequest.ExamType,
                                CampusId = campus.CampusId,
                                SubjectId = subject.SubjectId,
                                CreaterId = currentUserId,
                                ExamStatusId = 1,
                                IsReady = false,
                                GeneralFeedback = null,
                                ExamDuration = examImportRequest.ExamDuration,
                                StartDate = examImportRequest.StartDate,
                                EndDate = examImportRequest.EndDate,
                                SemesterId = semester.SemesterId,
                            };

                            examsToAdd.Add(exam);
                        }
                    }
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
                .Where(cus => cus.SubjectId == e.SubjectId && cus.CampusId == e.CampusId /*&& cus.IsLecturer == true*/)
                .Select(cus => cus.User.FullName))
        }).ToListAsync();

        return (results, results.Count);
    }

    public async Task<ResultResponse<CampusSubjectExamResponse>> GetExamByCampusAndSubject(UserRequest req)
    {
        try
        {
            var exams = await _context.Exams
                .AsNoTracking()
                .Where(e => e.CampusId == req.CampusId)
                .Include(e => e.Campus)
                .Include(e => e.Subject)
                .ThenInclude(e => e.Faculty)
                .ToListAsync();

            // Kiểm tra xem có bài thi nào được tìm thấy hay không
            if (!exams.Any())
            {
                return new ResultResponse<CampusSubjectExamResponse>
                {
                    IsSuccessful = false,
                    Message = "No exams found for the provided campus and subject."
                };
            }

            // Đếm tổng số bài thi
            var examCodeCount = exams.Count();

            // Đếm bài thi OK và bài thi có lỗi
            var ExamOk = exams.Where(e => e.ExamStatusId == 6).Count();
            var ExamError = exams.Where(e => e.ExamStatusId == 5).Count();

            // Lấy thông tin mã đề và thống kê theo khoa
            var Department = exams
                .GroupBy(e => e.Subject.Faculty.FacultyName)
                .Select(g => new CampusSubjectExamCodeResponse
                {
                    departmentName = g.Key,
                    totalExams = g.Count(),
                    ErrorCode = g.Count(e => e.ExamStatusId == 5),
                    OKCode = g.Count(e => e.ExamStatusId == 6)
                }).ToList();

            return new ResultResponse<CampusSubjectExamResponse>
            {
                IsSuccessful = true,
                Items = new List<CampusSubjectExamResponse>
                {
                    new CampusSubjectExamResponse
                    {
                        CampusName = exams.FirstOrDefault()?.Campus.CampusName, // Lấy tên campus từ bài thi đầu tiên
                        ExamCodeCount = examCodeCount,
                        ErrorCode = ExamError,
                        OKCode = ExamOk,
                        Departments = Department
                    }
                }
            };
        }
        catch (Exception ex)
        {
            // Log lỗi (nếu cần thiết)
            return new ResultResponse<CampusSubjectExamResponse>
            {
                IsSuccessful = false,
                Message = "An error occurred while fetching the exams. Please try again later."
            };
        }
    }

    public async Task<ResultResponse<CampusReportResponse>> GetCampusReport()
    {
        try
        {
            var exams = await _context.Exams
                .AsNoTracking()
                .Include(e => e.Campus)
                .Include(e => e.Subject)
                .ThenInclude(e => e.Faculty)
                .ToListAsync();

            if (!exams.Any())
            {
                return new ResultResponse<CampusReportResponse>
                {
                    IsSuccessful = false,
                    Message = "No exams found for the provided campus and subject."
                };
            }

            var examCodeCount = exams.Count();

            var ExamOk = exams.Where(e => e.ExamStatusId == 6).Count();
            var ExamError = exams.Where(e => e.ExamStatusId == 5).Count();

            var campusReports = exams
                .GroupBy(e => e.Campus.CampusName)
                .Select(g => new CampusReport
                {
                    CampusName = g.Key,
                    totalExams = g.Count(),
                    ErrorCode = g.Count(e => e.ExamStatusId == 5),
                    OKCode = g.Count(e => e.ExamStatusId == 6)
                }).ToList();

            return new ResultResponse<CampusReportResponse>
            {
                IsSuccessful = true,
                Items = new List<CampusReportResponse>
                {
                    new CampusReportResponse
                    {
                        ExamCodeCount = examCodeCount,
                        ErrorCode = ExamError,
                        OKCode = ExamOk,
                        Campus = campusReports
                    }
                }
            };
        }
        catch (Exception ex)
        {
            // Log lỗi (nếu cần thiết)
            return new ResultResponse<CampusReportResponse>
            {
                IsSuccessful = false,
                Message = "An error occurred while fetching the exams. Please try again later."
            };
        }
    }
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



    public async Task<ResultResponse<DepartmentReportResponse>> GetDepartmentReport(UserRequest req)
    {
        try
        {
            var exams = await _context.Exams
                .AsNoTracking()
                .Include(e => e.Campus)
                .Include(e => e.Subject)
                .ThenInclude(e => e.Faculty)
                .Include(e => e.Reports)
                .Include(e => e.ExamStatus) // Bao gồm cả trạng thái bài thi
                .Where(e => e.CampusId ==req.CampusId)
                .ToListAsync();

            if (!exams.Any())
            {
                return new ResultResponse<DepartmentReportResponse>
                {
                    IsSuccessful = false,
                    Message = "No exams found for the provided campus and subject."
                };
            }

            var examCodeCount = exams.Count();
            var examOk = exams.Count(e => e.ExamStatusId == 6);
            var examError = exams.Count(e => e.ExamStatusId == 5);

            // Tạo danh sách báo cáo chi tiết theo ExamCode
            var departmentDetails = exams
                .GroupBy(e => e.ExamCode)
                .Select(g => new DepartmentReportResponse.DepartmentReport
                {
                    ExamCode = g.Key,
                    Status = g.FirstOrDefault()?.ExamStatus?.StatusContent ?? " ",
                    issues = g
                            .Where(e => e.ExamStatusId == 5)
                           .SelectMany(e => e.Reports.Select(r => r.ReportContent ?? "No Issues Reported"))
                          .ToList()
                })
                                    .ToList()
                        .Select(report =>
                                    {
                                        if (!report.issues.Any())
                                        {
                                            report.issues.Add("No Issues");
                                        }
                                        return report;
                                    })
                                    .ToList();


            return new ResultResponse<DepartmentReportResponse>
            {
                IsSuccessful = true,
                Items = new List<DepartmentReportResponse>
            {
                new DepartmentReportResponse
                {
                    CampusName = exams.FirstOrDefault()?.Campus?.CampusName ?? "Unknown Campus",
                    DepartmentName = exams.FirstOrDefault()?.Subject?.Faculty?.FacultyName ?? "Unknown Department",
                    ExamCodeCount = examCodeCount,
                    ErrorCode = examError,
                    OKCode = examOk,
                    DepartmentDetail = departmentDetails
                }
            }
            };
        }
        catch (Exception ex)
        {
            // Log lỗi (nếu cần thiết)
            return new ResultResponse<DepartmentReportResponse>
            {
                IsSuccessful = false,
                Message = "An error occurred while fetching the exams. Please try again later."
            };
        }
    }

    public async Task<List<ExamRemindResponse>> SendReminderForUncorrectedExams()
    {
        try
        {
            var data = await (from ex in this._context.Exams
                              join u in this._context.Users on ex.HeadDepartmentId equals u.UserId
                              where ex.ExamStatusId == 5
                              && Math.Round((DateTime.Now - ex.UpdateDate.Value).TotalDays) == 3
                              select new ExamRemindResponse
                              {
                                  ExamCode = ex.ExamCode,
                                  Mail = u.Mail,
                              }).ToListAsync();

            return data;
        }
        catch (Exception ex)
        {
            return new List<ExamRemindResponse>();
        }
    }

    public async Task<List<ExamRemindResponse>> SendReminderForExamsWithoutScheduledDate()
    {
        try
        {
            var data = await(from ex in this._context.Exams
                             join u in this._context.Users on ex.AssignedUserId equals u.UserId
                             where ex.ExamStatusId == 3
                             && Math.Round((DateTime.Now - ex.UpdateDate.Value).TotalDays) == 3
                             select new ExamRemindResponse
                             {
                                 ExamCode = ex.ExamCode,
                                 Mail = u.Mail,
                             }).ToListAsync();

            return data;
        }
        catch (Exception ex)
        {
            return new List<ExamRemindResponse>();
        }
    }

    public async Task<List<ExamRemindResponse>> SendReminderForReviewDate()
    {
        try
        {
            var data = await(from ex in this._context.Exams
                             join u in this._context.Users on ex.AssignedUserId equals u.UserId
                             where ex.ExamStatusId == 3
                             && ex.AssignmentDate.Value.Date == DateTime.Now.Date
                             select new ExamRemindResponse
                             {
                                 ExamCode = ex.ExamCode,
                                 Mail = u.Mail,
                             }).ToListAsync();

            return data;
        }
        catch (Exception ex)
        {
            return new List<ExamRemindResponse>();
        }
    }

}
