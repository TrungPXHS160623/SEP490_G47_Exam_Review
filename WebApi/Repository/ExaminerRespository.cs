using Library.Common;
using Library.Models;
using Library.Response;
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

        public async Task<ResultResponse<ExamByCampusResponse>> GetExamsByCampusAsync(int examinerId)
        {
            var examiner = await dbContext.Users
           .Include(u => u.Campus)
           .FirstOrDefaultAsync(u => u.UserId == examinerId && u.Role.RoleName == "Examiner");

            if (examiner == null)
            {
                return new ResultResponse<ExamByCampusResponse>
                {
                    IsSuccessful = false,
                    Message = "No data to fetch",
                };
            }
            // Query to get exams based on the examiner's campus
            var examsQuery = dbContext.Exams
                .Where(e => e.CampusId == examiner.CampusId); // Ensure this is CampusId
            // Map the result to a list of ExamDto
            var exams = await examsQuery
                .Select(e => new ExamByCampusResponse
                {
                    ExamId = e.ExamId,
                    ExamCode = e.ExamCode,
                    StatusContent =e.ExamStatus.StatusContent,
                    HeadOfDepartment = e.Subject.HeadOfDepartment.Mail,
                    EstimatedTimeTest = e.EstimatedTimeTest,
                    StartDate = e.StartDate,
                    EndDate = e.EndDate
                })
                .ToListAsync();
            return new ResultResponse<ExamByCampusResponse>
            {
                IsSuccessful = true,
                Items =exams
            };
        }

        public async Task<ResultResponse<ExamDetailResponse>> GetExamsDetail(int examID)
        {
            var examsQuery = dbContext.Exams
                .Where(e => e.ExamId == examID);
            var exams = await examsQuery
               .Select(e => new ExamDetailResponse
               {
                   ExamId = e.ExamId,
                   ExamCode = e.ExamCode,
                   ExamDuration= e.ExamDuration,
                   ExamType =e.ExamType,
                   SubjectName=e.Subject.SubjectName,
                   ExamCreater=e.Creater.Mail,
                   HeadOfDepartment = e.Subject.HeadOfDepartment.Mail,
                   ExamStatus =e.ExamStatus.StatusContent,
                   EstimatedTimeTest = e.EstimatedTimeTest,
                   StartDate = e.StartDate,
                   EndDate = e.EndDate
               })
               .ToListAsync();
            return new ResultResponse<ExamDetailResponse>
            {
                IsSuccessful = true,
                Items =exams
            };
        }

        //public async Task<IEnumerable<ExamAssignment>> GetAllExamAssignmentsAsync()
        //{
        //    return await dbContext.ExamAssignments
        //                      .Include(i => i.Exam)
        //                      .Include(i => i.AssignedBy)
        //                      .Include(i => i.AssignedTo)
        //                      .Include(i => i.AssignmentDate)
        //                      .Include(i => i.Status)
        //                      .ToListAsync();
        //}
        //public async Task<ExamAssignment?> GetExamAssignmentByIdAsync(int id)
        //{
        //    return await dbContext.ExamAssignments
        //                     .Include(i => i.Exam)
        //                     .Include(i => i.AssignedBy)
        //                     .Include(i => i.AssignedTo)
        //                     .Include(i => i.Status)
        //                     .FirstOrDefaultAsync(i => i.AssignmentId == id);
        //}

        //public async Task<IEnumerable<ExamAssignment>> GetExamAssignments(int instructorId)
        //{
        //    return await dbContext.ExamAssignments
        //      .Where(ia => ia.AssignedTo == instructorId)
        //      .Include(ia => ia.Exam)
        //      .ToListAsync();
        //}
        //async Task<ExamAssignment> IExaminerRepository.AssignInstructor(ExamAssignment instructorAssignment)
        //{
        //    dbContext.ExamAssignments.Add(instructorAssignment);
        //    await dbContext.SaveChangesAsync();
        //    return instructorAssignment;
        //}

        //public async Task<List<ExamDto>> GetExamsByCampusAsync(int examinerId, string subjectName = null)
        //{
        // Find the examiner and their campus

        //}

        public async Task<Exam> UpdateExamStatusAsync(int examId)
        {
            var examStatus = await dbContext.Exams.FindAsync(examId);
            if (examStatus != null)
            {
                var pendingReviewStatus = await dbContext.ExamStatuses
             .FirstOrDefaultAsync(es => es.StatusContent == "Pending Review");
            }
            return examStatus;
        }


    }
}
