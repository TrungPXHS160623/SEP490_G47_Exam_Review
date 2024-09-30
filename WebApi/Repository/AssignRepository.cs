using Library.Common;
using Library.Models;
using Library.Request;
using Library.Response;
using Microsoft.EntityFrameworkCore;
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

        public async Task<RequestResponse> AddAssignToLecturer(AssignRequest assignRequest)
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
                /*
                // Kiểm tra trạng thái của kỳ thi (chỉ cho phép phân công nếu trạng thái hợp lệ)
                if (exam.ExamStatusId == null || exam.ExamStatusId == 3 || exam.ExamStatusId == 4 || exam.ExamStatusId == 5 || exam.ExamStatusId == 6)
                {
                    return new RequestResponse
                    {
                        IsSuccessful = false,
                        Message = "Exam is not in a valid state for assignment!"
                    };
                }
                */
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

                /*
                // Kiểm tra ngày kỳ thi, không cho phép phân công sau khi kỳ thi đã kết thúc
                if (exam.EndDate != null && DateTime.Now > exam.EndDate)
                {
                    return new RequestResponse
                    {
                        IsSuccessful = false,
                        Message = "Cannot assign lecturer after the exam has ended!"
                    };
                }
                */
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

        public async Task<ResultResponse<AssignResponse>> GetAssignmentsByCampusId(int campusID)
        {
            try
            {
                var data = (from a in this.dbContext.InstructorAssignments
                                // Tham chiếu bảng Exams (Kỳ thi)
                            join e in this.dbContext.Exams on a.ExamId equals e.ExamId
                            // Tham chiếu bảng ExamStatuses (Trạng thái kỳ thi) 
                            join es in this.dbContext.ExamStatuses on e.ExamStatusId equals es.ExamStatusId into esJoin
                            from es in esJoin.DefaultIfEmpty()
                                // Tham chiếu bảng Campuses (Cơ sở)
                            join c in this.dbContext.Campuses on e.CampusId equals c.CampusId
                            // Tham chiếu bảng Subjects (Môn học)
                            join s in this.dbContext.Subjects on e.SubjectId equals s.SubjectId
                            // Tham chiếu bảng CampusUserSubjects 
                            join cus in this.dbContext.CampusUserSubjects on new { e.SubjectId, e.CampusId } equals new { cus.SubjectId, cus.CampusId } into cusJoin
                            from cus in cusJoin.DefaultIfEmpty()
                                // Tham chiếu bảng Users để lấy thông tin trưởng bộ môn 
                            join uHead in this.dbContext.Users on cus.UserId equals uHead.UserId into uHeadJoin
                            from uHead in uHeadJoin.DefaultIfEmpty()
                                // Tham chiếu bảng Users để lấy thông tin khảo thí 
                            join uCreater in this.dbContext.Users on e.CreaterId equals uCreater.UserId into uCreaterJoin
                            from uCreater in uCreaterJoin.DefaultIfEmpty()
                                // Tham chiếu bảng Users để lấy thông tin giảng viên được phân công 
                            join lReceived in this.dbContext.Users on a.AssignedUserId equals lReceived.UserId into lReceivedJoin
                            from lReceived in lReceivedJoin.DefaultIfEmpty()
                            where c.CampusId == campusID
                            select new AssignResponse
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

                return new ResultResponse<AssignResponse>
                {
                    IsSuccessful = true,
                    Items = data
                };
            }
            catch (Exception ex)
            {
                return new ResultResponse<AssignResponse>
                {
                    IsSuccessful = false,
                    Message = ex.Message,
                };
            }
        }

        public async Task<ResultResponse<AssignResponse>> GetAssignmentsByExamId(int examID)
        {
            try
            {
                var data = (from a in this.dbContext.InstructorAssignments
                                // Tham chiếu bảng Exams (Kỳ thi)
                            join e in this.dbContext.Exams on a.ExamId equals e.ExamId
                            // Tham chiếu bảng ExamStatuses (Trạng thái kỳ thi) 
                            join es in this.dbContext.ExamStatuses on e.ExamStatusId equals es.ExamStatusId into esJoin
                            from es in esJoin.DefaultIfEmpty()
                                // Tham chiếu bảng Campuses (Cơ sở)
                            join c in this.dbContext.Campuses on e.CampusId equals c.CampusId
                            // Tham chiếu bảng Subjects (Môn học)
                            join s in this.dbContext.Subjects on e.SubjectId equals s.SubjectId
                            // Tham chiếu bảng CampusUserSubjects 
                            join cus in this.dbContext.CampusUserSubjects on new { e.SubjectId, e.CampusId } equals new { cus.SubjectId, cus.CampusId } into cusJoin
                            from cus in cusJoin.DefaultIfEmpty()
                                // Tham chiếu bảng Users để lấy thông tin trưởng bộ môn 
                            join uHead in this.dbContext.Users on cus.UserId equals uHead.UserId into uHeadJoin
                            from uHead in uHeadJoin.DefaultIfEmpty()
                                // Tham chiếu bảng Users để lấy thông tin khảo thí 
                            join uCreater in this.dbContext.Users on e.CreaterId equals uCreater.UserId into uCreaterJoin
                            from uCreater in uCreaterJoin.DefaultIfEmpty()
                                // Tham chiếu bảng Users để lấy thông tin giảng viên được phân công 
                            join lReceived in this.dbContext.Users on a.AssignedUserId equals lReceived.UserId into lReceivedJoin
                            from lReceived in lReceivedJoin.DefaultIfEmpty()
                            where e.ExamId == examID
                            select new AssignResponse
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

                return new ResultResponse<AssignResponse>
                {
                    IsSuccessful = true,
                    Items = data
                };
            }
            catch (Exception ex)
            {
                return new ResultResponse<AssignResponse>
                {
                    IsSuccessful = false,
                    Message = ex.Message,
                };
            }
        }

        public async Task<ResultResponse<AssignResponse>> GetAssignmentsByHeadId(int HeadID)
        {
            try
            {
                var data = (from a in this.dbContext.InstructorAssignments
                                // Tham chiếu bảng Exams (Kỳ thi)
                            join e in this.dbContext.Exams on a.ExamId equals e.ExamId
                            // Tham chiếu bảng ExamStatuses (Trạng thái kỳ thi) 
                            join es in this.dbContext.ExamStatuses on e.ExamStatusId equals es.ExamStatusId into esJoin
                            from es in esJoin.DefaultIfEmpty()
                                // Tham chiếu bảng Campuses (Cơ sở)
                            join c in this.dbContext.Campuses on e.CampusId equals c.CampusId
                            // Tham chiếu bảng Subjects (Môn học)
                            join s in this.dbContext.Subjects on e.SubjectId equals s.SubjectId
                            // Tham chiếu bảng CampusUserSubjects 
                            join cus in this.dbContext.CampusUserSubjects on new { e.SubjectId, e.CampusId } equals new { cus.SubjectId, cus.CampusId } into cusJoin
                            from cus in cusJoin.DefaultIfEmpty()
                                // Tham chiếu bảng Users để lấy thông tin trưởng bộ môn 
                            join uHead in this.dbContext.Users on cus.UserId equals uHead.UserId into uHeadJoin
                            from uHead in uHeadJoin.DefaultIfEmpty()
                                // Tham chiếu bảng Users để lấy thông tin khảo thí 
                            join uCreater in this.dbContext.Users on e.CreaterId equals uCreater.UserId into uCreaterJoin
                            from uCreater in uCreaterJoin.DefaultIfEmpty()
                                // Tham chiếu bảng Users để lấy thông tin giảng viên được phân công 
                            join lReceived in this.dbContext.Users on a.AssignedUserId equals lReceived.UserId into lReceivedJoin
                            from lReceived in lReceivedJoin.DefaultIfEmpty()
                            where uHead.UserId == HeadID
                            select new AssignResponse
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

                return new ResultResponse<AssignResponse>
                {
                    IsSuccessful = true,
                    Items = data
                };
            }
            catch (Exception ex)
            {
                return new ResultResponse<AssignResponse>
                {
                    IsSuccessful = false,
                    Message = ex.Message,
                };
            }
        }

        public async Task<ResultResponse<AssignResponse>> GetAssignmentsByLecturerId(int lecturerID)
        {
            try
            {
                var data = (from a in this.dbContext.InstructorAssignments
                                // Tham chiếu bảng Exams (Kỳ thi)
                            join e in this.dbContext.Exams on a.ExamId equals e.ExamId
                            // Tham chiếu bảng ExamStatuses (Trạng thái kỳ thi) 
                            join es in this.dbContext.ExamStatuses on e.ExamStatusId equals es.ExamStatusId into esJoin
                            from es in esJoin.DefaultIfEmpty()
                                // Tham chiếu bảng Campuses (Cơ sở)
                            join c in this.dbContext.Campuses on e.CampusId equals c.CampusId
                            // Tham chiếu bảng Subjects (Môn học)
                            join s in this.dbContext.Subjects on e.SubjectId equals s.SubjectId
                            // Tham chiếu bảng CampusUserSubjects 
                            join cus in this.dbContext.CampusUserSubjects on new { e.SubjectId, e.CampusId } equals new { cus.SubjectId, cus.CampusId } into cusJoin
                            from cus in cusJoin.DefaultIfEmpty()
                                // Tham chiếu bảng Users để lấy thông tin trưởng bộ môn 
                            join uHead in this.dbContext.Users on cus.UserId equals uHead.UserId into uHeadJoin
                            from uHead in uHeadJoin.DefaultIfEmpty()
                                // Tham chiếu bảng Users để lấy thông tin khảo thí 
                            join uCreater in this.dbContext.Users on e.CreaterId equals uCreater.UserId into uCreaterJoin
                            from uCreater in uCreaterJoin.DefaultIfEmpty()
                                // Tham chiếu bảng Users để lấy thông tin giảng viên được phân công 
                            join lReceived in this.dbContext.Users on a.AssignedUserId equals lReceived.UserId into lReceivedJoin
                            from lReceived in lReceivedJoin.DefaultIfEmpty()
                            where lReceived.UserId == lecturerID
                            select new AssignResponse
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

                return new ResultResponse<AssignResponse>
                {
                    IsSuccessful = true,
                    Items = data
                };
            }
            catch (Exception ex)
            {
                return new ResultResponse<AssignResponse>
                {
                    IsSuccessful = false,
                    Message = ex.Message,
                };
            }
        }

        public async Task<ResultResponse<AssignResponse>> ListAssignmentsToLecturersByHead()
        {
            try
            {
                var data = (from a in this.dbContext.InstructorAssignments
                                // Tham chiếu bảng Exams (Kỳ thi)
                            join e in this.dbContext.Exams on a.ExamId equals e.ExamId
                            // Tham chiếu bảng ExamStatuses (Trạng thái kỳ thi) 
                            join es in this.dbContext.ExamStatuses on e.ExamStatusId equals es.ExamStatusId into esJoin
                            from es in esJoin.DefaultIfEmpty()
                                // Tham chiếu bảng Campuses (Cơ sở)
                            join c in this.dbContext.Campuses on e.CampusId equals c.CampusId
                            // Tham chiếu bảng Subjects (Môn học)
                            join s in this.dbContext.Subjects on e.SubjectId equals s.SubjectId
                            // Tham chiếu bảng CampusUserSubjects 
                            join cus in this.dbContext.CampusUserSubjects on new { e.SubjectId, e.CampusId } equals new { cus.SubjectId, cus.CampusId } into cusJoin
                            from cus in cusJoin.DefaultIfEmpty()
                                // Tham chiếu bảng Users để lấy thông tin trưởng bộ môn 
                            join uHead in this.dbContext.Users on cus.UserId equals uHead.UserId into uHeadJoin
                            from uHead in uHeadJoin.DefaultIfEmpty()
                                // Tham chiếu bảng Users để lấy thông tin khảo thí 
                            join uCreater in this.dbContext.Users on e.CreaterId equals uCreater.UserId into uCreaterJoin
                            from uCreater in uCreaterJoin.DefaultIfEmpty()
                                // Tham chiếu bảng Users để lấy thông tin giảng viên được phân công 
                            join lReceived in this.dbContext.Users on a.AssignedUserId equals lReceived.UserId into lReceivedJoin
                            from lReceived in lReceivedJoin.DefaultIfEmpty()

                            select new AssignResponse
                            {
                                // Gán các thuộc tính kết quả từ các bảng
                                ExamCode = e.ExamCode,
                                ExamDuration = e.ExamDuration,
                                ExemType = e.ExamType,
                                SubjectName = s.SubjectName,
                                CampusName = c.CampusName,
                                ExaminorMail = uCreater.Mail, // Mail của người tạo đề
                                HeadOfDepartmentMail = uHead.Mail, // Mail của trưởng bộ môn
                                LecturorMail = lReceived.Mail,    // Mail của giảng viên
                                ExamStatus = es.StatusContent,
                                EstimatedTimeTest = e.EstimatedTimeTest,
                                AssignmentDate = a.AssignmentDate

                            }).ToList();

                return new ResultResponse<AssignResponse>
                {
                    IsSuccessful = true,
                    Items = data
                };
            }
            catch (Exception ex)
            {
                return new ResultResponse<AssignResponse>
                {
                    IsSuccessful = false,
                    Message = ex.Message,
                };
            }
        }
    }
}



