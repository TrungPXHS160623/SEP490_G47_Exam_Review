using Library.Common;
using Library.Models;
using Library.Request;
using Library.Response;
using Microsoft.EntityFrameworkCore;
using WebApi.IRepository;

namespace WebApi.Repository
{
    public class InstructorAssignmentRepository : IInstructorAssignmentRepository
    {
        private readonly QuizManagementContext DBcontext;

        public InstructorAssignmentRepository()
        {
        }

        public InstructorAssignmentRepository(QuizManagementContext DBcontext)
        {
            this.DBcontext = DBcontext;
        }
        public async Task<RequestResponse> AssignExamToLecture(LeaderExamResponse req)
        {
            try
            {
                var data = await this.DBcontext.Exams.Where(x => x.ExamId == req.ExamId).FirstOrDefaultAsync();

                if (data != null)
                {
                    data.AssignedUserId = req.AssignedLectureId;
                    data.ExamStatusId = 3;
                }
                else
                {
                    return new RequestResponse
                    {
                        IsSuccessful = false,
                        Message = "Exam not found",
                    };
                }

                await this.DBcontext.SaveChangesAsync();

                return new RequestResponse
                {
                    IsSuccessful = true,
                    Message = "Assign Exam Successfully!"
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
        public async Task<RequestResponse> AssignSubjectToLecture(AddLecturerSubjectRequest req)
        {
            try
            {
                // Lấy danh sách các đối tượng có cùng SubjectId và CampusId
                var campusId = await this.DBcontext.CampusUserSubjects
                    .Where(x => x.SubjectId == req.SubjectId && x.UserId == req.UserId)
                    .Select(x => x.CampusId)
                    .FirstOrDefaultAsync();

                if (campusId == 0)
                {
                    return new RequestResponse
                    {
                        IsSuccessful = false,
                        Message = "Invalid CampusId for the provided UserId and SubjectId.",
                    };
                }

                // Cập nhật IsSelect cho tất cả các đối tượng liên quan
                var relatedSubjects = await this.DBcontext.CampusUserSubjects
                    .Where(x => x.SubjectId == req.SubjectId && x.CampusId == campusId)
                    .ToListAsync();

                foreach (var item in relatedSubjects)
                {
                    item.IsSelect = item.UserId == req.UserId; // Đặt true cho UserId truyền vào, false cho những UserId khác
                }

                await this.DBcontext.SaveChangesAsync();

                return new RequestResponse
                {
                    IsSuccessful = true,
                    Message = "Assigned Subject to Lecture successfully!"
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

        public async Task<RequestResponse> SetAssignDate(LectureExamResponse req)
        {
            try
            {
                var data = await this.DBcontext.Exams.FirstOrDefaultAsync(x => x.ExamId == req.ExamId);

                if (data == null)
                {
                    return new RequestResponse
                    {
                        IsSuccessful = false,
                        Message = "Exam not found!"
                    };
                }

                data.EstimatedTimeTest = req.EstimatedTimeTest;
                data.ExamStatusId = 4;

                await this.DBcontext.SaveChangesAsync();

                return new RequestResponse
                {
                    IsSuccessful = true,
                    Message = "Assign date save successfully!"
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
