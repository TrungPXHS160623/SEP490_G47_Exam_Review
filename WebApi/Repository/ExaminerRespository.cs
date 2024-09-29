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
                .Where(e => e.CampusId == examiner.CampusId)
                 .Select(e => new
                 {
                     Exam = e,
                     HeadOfDepartment = dbContext.CampusUserSubjects
                        .Where(cus => cus.SubjectId == e.SubjectId && cus.CampusId == e.CampusId)
                        .Select(cus => cus.User)
                        .FirstOrDefault()
                 });
            var exams = await examsQuery
                .Select(e => new ExamByCampusResponse
                {
                    ExamId = e.Exam.ExamId,
                    ExamCode = e.Exam.ExamCode,
                    StatusContent =e.Exam.ExamStatus.StatusContent,
                    HeadOfDepartment = e.HeadOfDepartment != null ? e.HeadOfDepartment.Mail : "Unknown",
                    EstimatedTimeTest = e.Exam.EstimatedTimeTest,
                    StartDate = e.Exam.StartDate,
                    EndDate = e.Exam.EndDate
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
                .Where(e => e.ExamId == examID)
                .Select(e => new
                {
                    Exam = e,
                    HeadOfDepartment = dbContext.CampusUserSubjects
                        .Where(cus => cus.SubjectId == e.SubjectId && cus.CampusId == e.CampusId)
                        .Select(cus => cus.User)
                        .FirstOrDefault()
                });

            var exams = await examsQuery
                .Select(e => new ExamDetailResponse
                {
                    ExamId = e.Exam.ExamId,
                    ExamCode = e.Exam.ExamCode,
                    ExamDuration = e.Exam.ExamDuration,
                    ExamType = e.Exam.ExamType,
                    SubjectName = e.Exam.Subject.SubjectName,
                    ExamCreater = e.Exam.Creater.Mail,
                    HeadOfDepartment = e.HeadOfDepartment != null ? e.HeadOfDepartment.Mail : "Unknown",
                    ExamStatus = e.Exam.ExamStatus.StatusContent,
                    EstimatedTimeTest = e.Exam.EstimatedTimeTest,
                    StartDate = e.Exam.StartDate,
                    EndDate = e.Exam.EndDate
                })
                .ToListAsync();

            return new ResultResponse<ExamDetailResponse>
            {
                IsSuccessful = true,
                Items = exams
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
