using Library.Common;
using Library.Models;
using Library.Request;
using Library.Response;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query.SqlExpressions;
using WebApi.IRepository;

namespace WebApi.Repository
{
    public class AssignRepository : IAssignRepository
    {
        private readonly QuizManagementContext dbContext;

        public AssignRepository(QuizManagementContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public async Task<RequestResponse> AddAssign(AssignRequest assignRequest)
        {
            try
            {
                // Kiểm tra xem Exam có tồn tại không
                var exam = await dbContext.Exams.FirstOrDefaultAsync(e => e.ExamId == assignRequest.ExamId);
                if (exam == null)
                {
                    return new RequestResponse
                    {
                        IsSuccessful = false,
                        Message = "Exam does not exist!"
                    };
                }

                // Kiểm tra trạng thái của kỳ thi (chỉ cho phép phân công nếu trạng thái hợp lệ)
                if (exam.ExamStatusId == null || exam.ExamStatusId == 3 || exam.ExamStatusId == 4 || exam.ExamStatusId == 5 || exam.ExamStatusId == 6) 
                {
                    return new RequestResponse
                    {
                        IsSuccessful = false,
                        Message = "Exam is not in a valid state for assignment!"
                    };
                }
                // Kiểm tra xem AssignedUserId có tồn tại và là giảng viên không
                var user = await dbContext.Users.FirstOrDefaultAsync(u => u.UserId == assignRequest.AssignedUserId);
                if (user == null)
                {
                    return new RequestResponse
                    {
                        IsSuccessful = false,
                        Message = "Assigned user does not exist!"
                    };
                }

                if (user.RoleId != 3) // 3 là RoleId cho giảng viên
                {
                    return new RequestResponse
                    {
                        IsSuccessful = false,
                        Message = "Assigned user is not a lecturer!"
                    };
                }

                // Kiểm tra xem người dùng có hoạt động hay không
                if (!user.IsActive)
                {
                    return new RequestResponse
                    {
                        IsSuccessful = false,
                        Message = "Assigned user is not active!"
                    };
                }
                // Kiểm tra xem đã có phân công cho giảng viên này cho kỳ thi này chưa
                var existingAssignment = await dbContext.InstructorAssignments
                    .FirstOrDefaultAsync(x => x.ExamId == assignRequest.ExamId && x.AssignedUserId == assignRequest.AssignedUserId);

                if (existingAssignment != null)
                {
                    return new RequestResponse
                    {
                        IsSuccessful = false,
                        Message = "This lecturer has already been assigned to this exam!"
                    };
                }

                // Kiểm tra ngày kỳ thi, không cho phép phân công sau khi kỳ thi đã kết thúc
                if (exam.EndDate != null && DateTime.Now > exam.EndDate)
                {
                    return new RequestResponse
                    {
                        IsSuccessful = false,
                        Message = "Cannot assign lecturer after the exam has ended!"
                    };
                }

                var NewAssign = new InstructorAssignment
                {
                    ExamId = assignRequest.ExamId,
                    AssignedUserId = assignRequest.AssignedUserId,
                    AssignmentDate = DateTime.Now,
                    CreateDate = DateTime.Now,
                };
                await dbContext.InstructorAssignments.AddAsync(NewAssign);
                await dbContext.SaveChangesAsync();
                return new RequestResponse
                {
                    IsSuccessful = true,
                    Message = "Create Assign successfully!"
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
        public async Task<ResultResponse<AssignResponce>> GetAllAssign()
        {
            try
            {
                var data = (from a in this.dbContext.InstructorAssignments
                            join e in this.dbContext.Exams on a.ExamId equals e.ExamId
                            join es in this.dbContext.ExamStatuses on e.ExamStatusId equals es.ExamStatusId into examStatusJoin
                            from es in examStatusJoin.DefaultIfEmpty()
                            join s in this.dbContext.Subjects on e.SubjectId equals s.SubjectId 
                            join uCreater in this.dbContext.Users on e.CreaterId equals uCreater.UserId into createrJoin
                            from uCreater in createrJoin.DefaultIfEmpty()
                            join uHead in this.dbContext.Users on s.HeadOfDepartmentId equals uHead.UserId into headJoin
                            from uHead in headJoin.DefaultIfEmpty()
                            join lReceived in this.dbContext.Users on a.AssignedUserId equals lReceived.UserId into lecturorJoin
                            from lReceived in lecturorJoin.DefaultIfEmpty()
                            join c in this.dbContext.Campuses on e.CampusId equals c.CampusId into campusesJoin
                            from c in campusesJoin.DefaultIfEmpty()
                            select new AssignResponce
                            {
                                // Gán các thuộc tính kết quả từ các bảng
                                ExamCode = e.ExamCode,
                                ExamDuration = e.ExamDuration,
                                ExemType = e.ExamType,
                                SubjectName = s.SubjectName,
                                CampusName = c.CampusName,
                                ExaminorMail = uCreater.Mail, // Mail của người tạo đề
                                HeadOfDepartmentMail = uHead.Mail, // Mail của trưởng bộ môn
                                LecturorMail = lReceived.Mail,    //Mail của giảng viên
                                ExamStatus = es.StatusContent,
                                EstimatedTimeTest = e.EstimatedTimeTest,
                                AssignmentDate = a.AssignmentDate

                            }).ToList();

                return new ResultResponse<AssignResponce>
                {
                    IsSuccessful = true,
                    Items = data
                };
            }
            catch (Exception ex)
            {
                return new ResultResponse<AssignResponce>
                {
                    IsSuccessful = false,
                    Message = ex.Message,
                };
            }

        }
        public async Task<ResultResponse<AssignResponce>> GetAllAssignByCampusId(int id)
        {
            try
            {
                var data = (from a in this.dbContext.InstructorAssignments
                            join e in this.dbContext.Exams on a.ExamId equals e.ExamId
                            join es in this.dbContext.ExamStatuses on e.ExamStatusId equals es.ExamStatusId into examStatusJoin
                            from es in examStatusJoin.DefaultIfEmpty()
                            join s in this.dbContext.Subjects on e.SubjectId equals s.SubjectId
                            join uCreater in this.dbContext.Users on e.CreaterId equals uCreater.UserId into createrJoin
                            from uCreater in createrJoin.DefaultIfEmpty()
                            join uHead in this.dbContext.Users on s.HeadOfDepartmentId equals uHead.UserId into headJoin
                            from uHead in headJoin.DefaultIfEmpty()
                            join lReceived in this.dbContext.Users on a.AssignedUserId equals lReceived.UserId into lecturorJoin
                            from lReceived in lecturorJoin.DefaultIfEmpty()
                            join c in this.dbContext.Campuses on e.CampusId equals c.CampusId into campusesJoin
                            from c in campusesJoin.DefaultIfEmpty()
                            where c.CampusId == id
                            select new AssignResponce
                            {
                                // Gán các thuộc tính kết quả từ các bảng
                                ExamCode = e.ExamCode,
                                ExamDuration = e.ExamDuration,
                                ExemType = e.ExamType,
                                SubjectName = s.SubjectName,
                                CampusName = c.CampusName,
                                ExaminorMail = uCreater.Mail, // Mail của người tạo đề
                                HeadOfDepartmentMail = uHead.Mail, // Mail của trưởng bộ môn
                                LecturorMail = lReceived.Mail,    //Mail của giảng viên
                                ExamStatus = es.StatusContent,
                                EstimatedTimeTest = e.EstimatedTimeTest,
                                AssignmentDate = a.AssignmentDate

                            }).ToList();

                return new ResultResponse<AssignResponce>
                {
                    IsSuccessful = true,
                    Items = data
                };
            }
            catch (Exception ex)
            {
                return new ResultResponse<AssignResponce>
                {
                    IsSuccessful = false,
                    Message = ex.Message,
                };
            }
        }

        public async Task<ResultResponse<AssignResponce>> GetAllAssignByExamId(int id)
        {
            try
            {
                var data = (from a in this.dbContext.InstructorAssignments
                            join e in this.dbContext.Exams on a.ExamId equals e.ExamId
                            join es in this.dbContext.ExamStatuses on e.ExamStatusId equals es.ExamStatusId into examStatusJoin
                            from es in examStatusJoin.DefaultIfEmpty()
                            join s in this.dbContext.Subjects on e.SubjectId equals s.SubjectId
                            join uCreater in this.dbContext.Users on e.CreaterId equals uCreater.UserId into createrJoin
                            from uCreater in createrJoin.DefaultIfEmpty()
                            join uHead in this.dbContext.Users on s.HeadOfDepartmentId equals uHead.UserId into headJoin
                            from uHead in headJoin.DefaultIfEmpty()
                            join lReceived in this.dbContext.Users on a.AssignedUserId equals lReceived.UserId into lecturorJoin
                            from lReceived in lecturorJoin.DefaultIfEmpty()
                            join c in this.dbContext.Campuses on e.CampusId equals c.CampusId into campusesJoin
                            from c in campusesJoin.DefaultIfEmpty()
                            where e.ExamId == id
                            select new AssignResponce
                            {
                                // Gán các thuộc tính kết quả từ các bảng
                                ExamCode = e.ExamCode,
                                ExamDuration = e.ExamDuration,
                                ExemType = e.ExamType,
                                SubjectName = s.SubjectName,
                                CampusName = c.CampusName,
                                ExaminorMail = uCreater.Mail, // Mail của người tạo đề
                                HeadOfDepartmentMail = uHead.Mail, // Mail của trưởng bộ môn
                                LecturorMail = lReceived.Mail,    //Mail của giảng viên
                                ExamStatus = es.StatusContent,
                                EstimatedTimeTest = e.EstimatedTimeTest,
                                AssignmentDate = a.AssignmentDate

                            }).ToList();

                return new ResultResponse<AssignResponce>
                {
                    IsSuccessful = true,
                    Items = data
                };
            }
            catch (Exception ex)
            {
                return new ResultResponse<AssignResponce>
                {
                    IsSuccessful = false,
                    Message = ex.Message,
                };
            }
        }

        public async Task<ResultResponse<AssignResponce>> GetAllAssignByHeadOfDepartmentId(int id)
        {
            try
            {
                var data = (from a in this.dbContext.InstructorAssignments
                            join e in this.dbContext.Exams on a.ExamId equals e.ExamId
                            join es in this.dbContext.ExamStatuses on e.ExamStatusId equals es.ExamStatusId into examStatusJoin
                            from es in examStatusJoin.DefaultIfEmpty()
                            join s in this.dbContext.Subjects on e.SubjectId equals s.SubjectId
                            join uCreater in this.dbContext.Users on e.CreaterId equals uCreater.UserId into createrJoin
                            from uCreater in createrJoin.DefaultIfEmpty()
                            join uHead in this.dbContext.Users on s.HeadOfDepartmentId equals uHead.UserId into headJoin
                            from uHead in headJoin.DefaultIfEmpty()
                            join lReceived in this.dbContext.Users on a.AssignedUserId equals lReceived.UserId into lecturorJoin
                            from lReceived in lecturorJoin.DefaultIfEmpty()
                            join c in this.dbContext.Campuses on e.CampusId equals c.CampusId into campusesJoin
                            from c in campusesJoin.DefaultIfEmpty()
                            where uHead.UserId == id
                            select new AssignResponce
                            {
                                // Gán các thuộc tính kết quả từ các bảng
                                ExamCode = e.ExamCode,
                                ExamDuration = e.ExamDuration,
                                ExemType = e.ExamType,
                                SubjectName = s.SubjectName,
                                CampusName = c.CampusName,
                                ExaminorMail = uCreater.Mail, // Mail của người tạo đề
                                HeadOfDepartmentMail = uHead.Mail, // Mail của trưởng bộ môn
                                LecturorMail = lReceived.Mail,    //Mail của giảng viên
                                ExamStatus = es.StatusContent,
                                EstimatedTimeTest = e.EstimatedTimeTest,
                                AssignmentDate = a.AssignmentDate

                            }).ToList();

                return new ResultResponse<AssignResponce>
                {
                    IsSuccessful = true,
                    Items = data
                };
            }
            catch (Exception ex)
            {
                return new ResultResponse<AssignResponce>
                {
                    IsSuccessful = false,
                    Message = ex.Message,
                };
            }
        }

        public async Task<ResultResponse<AssignResponce>> GetAllAssignByLecturorId(int id)
        {
            try
            {
                var data = (from a in this.dbContext.InstructorAssignments
                            join e in this.dbContext.Exams on a.ExamId equals e.ExamId
                            join es in this.dbContext.ExamStatuses on e.ExamStatusId equals es.ExamStatusId into examStatusJoin
                            from es in examStatusJoin.DefaultIfEmpty()
                            join s in this.dbContext.Subjects on e.SubjectId equals s.SubjectId
                            join uCreater in this.dbContext.Users on e.CreaterId equals uCreater.UserId into createrJoin
                            from uCreater in createrJoin.DefaultIfEmpty()
                            join uHead in this.dbContext.Users on s.HeadOfDepartmentId equals uHead.UserId into headJoin
                            from uHead in headJoin.DefaultIfEmpty()
                            join lReceived in this.dbContext.Users on a.AssignedUserId equals lReceived.UserId into lecturorJoin
                            from lReceived in lecturorJoin.DefaultIfEmpty()
                            join c in this.dbContext.Campuses on e.CampusId equals c.CampusId into campusesJoin
                            from c in campusesJoin.DefaultIfEmpty()
                            where lReceived.UserId == id
                            select new AssignResponce
                            {
                                // Gán các thuộc tính kết quả từ các bảng
                                ExamCode = e.ExamCode,
                                ExamDuration = e.ExamDuration,
                                ExemType = e.ExamType,
                                SubjectName = s.SubjectName,
                                CampusName = c.CampusName,
                                ExaminorMail = uCreater.Mail, // Mail của người tạo đề
                                HeadOfDepartmentMail = uHead.Mail, // Mail của trưởng bộ môn
                                LecturorMail = lReceived.Mail,    //Mail của giảng viên
                                ExamStatus = es.StatusContent,
                                EstimatedTimeTest = e.EstimatedTimeTest,
                                AssignmentDate = a.AssignmentDate

                            }).ToList();

                return new ResultResponse<AssignResponce>
                {
                    IsSuccessful = true,
                    Items = data
                };
            }
            catch (Exception ex)
            {
                return new ResultResponse<AssignResponce>
                {
                    IsSuccessful = false,
                    Message = ex.Message,
                };
            }
        }
    }
}
