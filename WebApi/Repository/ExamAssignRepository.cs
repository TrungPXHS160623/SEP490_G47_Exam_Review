using Library.Common;
using Library.Models;
using Library.Response;
using Microsoft.EntityFrameworkCore;
using WebApi.IRepository;

namespace WebApi.Repository
{

    public class ExamAssignRepository : IExamAssignRepository
    {
        private readonly QuizManagementContext dbContext;

        public ExamAssignRepository(QuizManagementContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<ResultResponse<ExamAssignResponse>> GetExamAssignByHeadId(int userId)
        {
            var statusInProgress = 2;

            // Query to get exams in progress for the specified Head of Department
            var exams = await dbContext.Exams
                .Include(e => e.Subject) // Include the Subject
                    .ThenInclude(s => s.CampusUserSubjects) // Include CampusUserSubjects to get Head of Department
                .Include(e => e.ExamStatus) // Include Exam Status
                .Where(e => e.ExamStatusId == statusInProgress)
                .Select(e => new ExamAssignResponse
                {
                    ExamId = e.ExamId,
                    ExamCode = e.ExamCode,
                    ExamDuration = e.ExamDuration,
                    ExamType = e.ExamType,
                    SubjectName = e.Subject.SubjectName,
                    ExamStatusId = statusInProgress,
                    ExamStatus = e.ExamStatus.StatusContent,
                    HeadOfDepartment = e.Subject.CampusUserSubjects
                        .Where(cus => cus.UserId == userId) // Filter by Head of Department ID
                        .Select(cus => cus.User.Mail) // Select the mail of the Head of Department
                        .FirstOrDefault(), // Get the first matching record
                    StartDate = e.StartDate,
                    EndDate = e.EndDate
                })
                .ToListAsync();

            if (exams == null || !exams.Any())
            {
                return new ResultResponse<ExamAssignResponse>
                {
                    IsSuccessful = false,
                    Message = "No exams found with the specified status."
                };
            }

            return new ResultResponse<ExamAssignResponse>
            {
                IsSuccessful = true,
                Items = exams
            };
        }
    }
}

