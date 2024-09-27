using Library.Common;
using Library.Models;
using Library.Request;
using Library.Response;
using Microsoft.EntityFrameworkCore;
using WebApi.IRepository;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi.Repository
{

	public class ExamAssignRepository : IExamAssignRepository
	{
		private readonly QuizManagementContext dbContext;

		public ExamAssignRepository(QuizManagementContext dbContext)
		{
			this.dbContext = dbContext;
		}

		public async Task<ResultResponse<ExamAssignResponse>> GetExamsInProgressByHeadDepartmentIdAsync(int userId)
		{
			var statusInProgress = 2;

			// Truy vấn LINQ để lấy kỳ thi dựa trên điều kiện đã cho
			var exams = await (from e in dbContext.Exams
							   join s in dbContext.Subjects on e.SubjectId equals s.SubjectId
							   join u in dbContext.Users on s.HeadOfDepartmentId  equals u.UserId
							   where e.ExamStatusId == statusInProgress && u.UserId == userId
							   select new ExamAssignResponse
							   {
								   ExamId = e.ExamId,
								   ExamCode = e.ExamCode,
								   ExamDuration = e.ExamDuration,
								   ExamType = e.ExamType,
								   SubjectName = s.SubjectName,
								   ExamStatusId = statusInProgress,
								   ExamStatus = e.ExamStatus.StatusContent,
								   HeadOfDepartment = u.Mail, 
								   StartDate = e.StartDate,
								   EndDate = e.EndDate
							   }).ToListAsync();

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

