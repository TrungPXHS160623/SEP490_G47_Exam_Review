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
                /*chưa xong*/
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
