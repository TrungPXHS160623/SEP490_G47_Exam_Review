using Library.Enums;
using Library.Models;
using Library.Models.Dtos;
using Microsoft.EntityFrameworkCore;
using WebApi.IRepository;

namespace WebApi.Repository
{
    public class ExaminerRepository : IExaminerRepository
    {
        private readonly QuizManagementContext dbContext;

        public ExaminerRepository(QuizManagementContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<List<ExamDto>> GetExamsByCampusAsync(int examinerId, string subjectName = null)
        {
            // Find the examiner and their campus
            var examiner = await dbContext.Users
                .Include(u => u.Campus)
                .FirstOrDefaultAsync(u => u.UserId == examinerId && u.UserRole.RoleName == "Examiner");

            if (examiner == null)
            {
                return new List<ExamDto>(); // Return an empty list if examiner is not found
            }

            // Query to get exams based on the examiner's campus
            var examsQuery = dbContext.Exams
                .Include(e => e.Subject)
                .ThenInclude(s => s.Department)
                .Where(e => e.User.CampusId == examiner.CampusId); // Ensure this is CampusId

            // Filter exams by subject name if provided
            if (!string.IsNullOrEmpty(subjectName))
            {
                examsQuery = examsQuery.Where(e => e.Subject.SubjectName.Contains(subjectName));
            }

            // Map the result to a list of ExamDto
            var exams = await examsQuery
                .Select(e => new ExamDto
                {
                    ExamId = e.ExamId,
                    ExamCode = e.ExamCode,
                    ExamType = e.ExamType,
                    DepartmentName = e.Subject.Department.DepartmentName,
                    EstimatedTimeTest = e.EstimatedTimeTest
                })
                .ToListAsync();

            return exams;
        }
        public async Task<Exam> UpdateExamStatusAsync(int examId, ExamStatusEnum status)
        {
            var examStatus = await dbContext.Exams.FindAsync(examId);
            if (examStatus != null)
            {
                examStatus.ExamStatus = status;
                await dbContext.SaveChangesAsync();
            }
            return examStatus;
        }
    }
}
