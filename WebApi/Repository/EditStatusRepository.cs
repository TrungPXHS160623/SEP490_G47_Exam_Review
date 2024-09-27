using Library.Common;
using Library.Models;
using Library.Request;
using Library.Response;
using Microsoft.EntityFrameworkCore;
using WebApi.IRepository;

namespace WebApi.Repository
{
	public class EditStatusRepository : IEditStatusRepository
	{
		private readonly QuizManagementContext dbContext;

		public EditStatusRepository(QuizManagementContext dbContext)
		{
			this.dbContext = dbContext;
		}

		public async Task<ResultResponse<StatusRequest>> EditStatus(int examID, string newStatus)
		{
			var exam = await dbContext.Exams
			.Include(e => e.ExamStatus)
			.Include(e => e.Subject)  // Include the Subject entity
			.FirstOrDefaultAsync(e => e.ExamId == examID);

			if (exam == null)
			{
				return new ResultResponse<StatusRequest>
				{
					IsSuccessful = false,
					Message = "Exam not found."
				};
			}

			var status = await dbContext.ExamStatuses.FirstOrDefaultAsync(s => s.StatusContent == newStatus);
			if (status == null)
			{
				return new ResultResponse<StatusRequest>
				{
					IsSuccessful = false,
					Message = "Provided status does not exist."
				};
			}

			exam.ExamStatusId = status.ExamStatusId;
			await dbContext.SaveChangesAsync();

			var response = new StatusRequest
			{
				ExamId = exam.ExamId,
				ExamCode = exam.ExamCode,
				ExamDuration = exam.ExamDuration,
				ExamType = exam.ExamType,
				SubjectName = exam.Subject.SubjectName,
				ExamStatus = status.StatusContent,
				StartDate = exam.StartDate,
				EndDate = exam.EndDate
			};

			return new ResultResponse<StatusRequest>
			{
				IsSuccessful = true,
				Items = new List<StatusRequest> { response }
			};
		}
	}
}
