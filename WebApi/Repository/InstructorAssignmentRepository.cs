using Library.Common;
using Library.Models;
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
                var data = await this.DBcontext.Exams.Where(x => x.ExamId == req.ExamId).ToListAsync();

                var addLecture = req.LectureList.Where(x => !data.Any(y => y.AssignedUserId == x.UserId)).ToList();

                if (addLecture.Any())
                {
                    foreach (var item in addLecture)
                    {
                        var newData = new Exam
                        {
                            AssignedUserId = item.UserId,
                            AssignmentDate = DateTime.Now,
                            ExamId = req.ExamId,
                            ExamStatusId = 3,
                            CreateDate = DateTime.Now,
                            UpdateDate = DateTime.Now
                        };

                        await this.DBcontext.Exams.AddAsync(newData);
                    }
                }


                var removeLecture = data.Where(x => !req.LectureList.Any(y => y.UserId == x.AssignedUserId)).ToList();

                if (removeLecture.Any())
                {
                    foreach (var item in removeLecture)
                    {
                        this.DBcontext.Exams.Remove(item);
                    }
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

                data.AssignmentDate = req.AssignmentDate;
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
