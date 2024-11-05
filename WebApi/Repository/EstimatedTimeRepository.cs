using Library.Models;
using Library.Request;
using Library.Response;
using Microsoft.EntityFrameworkCore;
using WebApi.IRepository;

namespace WebApi.Repository
{
    public class EstimatedTimeRepository : IEstimatedTimeRepository
    {
        private readonly QuizManagementContext dbContext;

        public EstimatedTimeRepository(QuizManagementContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<EstimatedTimeResponse> GetEstimatedTimeTest(int instructorAssignmentId)
        {
            try
            {
                var instructorAssignment = await dbContext.Exams
                    .Include(x => x.Subject)
                    .FirstOrDefaultAsync(x => x.ExamId == instructorAssignmentId);

                if (instructorAssignment == null)
                {
                    throw new Exception("Instructor Assignment not found.");
                }


                return new EstimatedTimeResponse
                {
                    ExamCode = instructorAssignment.ExamCode,
                    SubjectName = instructorAssignment.Subject.SubjectName,
                    EstimatedTimeTest = instructorAssignment.EstimatedTimeTest
                };
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public async Task<EstimatedTimeResponse> AddEstimatedTimeTest(int instructorAssignmentId, EstimatedTimeRequest request)
        {
            try
            {
                var instructorAssignment = await dbContext.Exams
                    .Include(x => x.Subject)
                    .FirstOrDefaultAsync(x => x.ExamId == instructorAssignmentId);

                if (instructorAssignment == null)
                {
                    throw new Exception("Instructor Assignment not found.");
                }


                instructorAssignment.EstimatedTimeTest = request.EstimatedTimeTest;
                instructorAssignment.CreateDate = DateTime.Now;

                dbContext.Exams.Update(instructorAssignment);
                await dbContext.SaveChangesAsync();


                return new EstimatedTimeResponse
                {
                    ExamCode = instructorAssignment.ExamCode,
                    SubjectName = instructorAssignment.Subject.SubjectName,
                    EstimatedTimeTest = instructorAssignment.EstimatedTimeTest
                };
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<EstimatedTimeResponse> UpdateEstimatedTimeTest(int instructorAssignmentId, EstimatedTimeRequest request)
        {
            try
            {
                var instructorAssignment = await dbContext.Exams
                    .Include(x => x.Subject)
                    .FirstOrDefaultAsync(x => x.ExamId == instructorAssignmentId);

                if (instructorAssignment == null)
                {
                    throw new Exception("Instructor Assignment not found.");
                }


                instructorAssignment.EstimatedTimeTest = request.EstimatedTimeTest;
                instructorAssignment.UpdateDate = DateTime.Now;

                dbContext.Exams.Update(instructorAssignment);
                await dbContext.SaveChangesAsync();


                return new EstimatedTimeResponse
                {
                    ExamCode = instructorAssignment.ExamCode,
                    SubjectName = instructorAssignment.Subject.SubjectName,
                    EstimatedTimeTest = instructorAssignment.EstimatedTimeTest
                };
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
