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

		// Implementation of GetExamAssignment
		public async Task<ResultResponse<ExamAssignResponse>> GetExamAssign(int examID)
		{
			var exam = await dbContext.Exams
				.Include(e => e.Subject)
				.Include(e => e.ExamStatus)
				.FirstOrDefaultAsync(e => e.ExamId == examID);

			if (exam == null)
			{
				return new ResultResponse<ExamAssignResponse>
				{
					IsSuccessful = false,
					Message = "Exam not found."
				};
			}

			var response = new ExamAssignResponse
			{
				ExamId = exam.ExamId,
				ExamCode = exam.ExamCode,
				ExamDuration = exam.ExamDuration,
				ExamType = exam.ExamType,
				SubjectName = exam.Subject.SubjectName,
				ExamStatus = exam.ExamStatus.StatusContent,
				StartDate = exam.StartDate,
				EndDate = exam.EndDate
			};

			return new ResultResponse<ExamAssignResponse>
			{
				IsSuccessful = true,
				Items = new List<ExamAssignResponse> { response }
			};
		}

		public async Task<ResultResponse<ExamAssignResponse>> GetAndEditExamAssign(int examID, string newStatus)
		{
			
			var exam = await dbContext.Exams
				.Include(e => e.Subject)
				.Include(e => e.ExamStatus)
				.FirstOrDefaultAsync(e => e.ExamId == examID);

			if (exam == null)
			{
				return new ResultResponse<ExamAssignResponse>
				{
					IsSuccessful = false,
					Message = "Exam not found."
				};
			}

		
			var status = await dbContext.ExamStatuses.FirstOrDefaultAsync(s => s.StatusContent == newStatus);
			if (status == null)
			{
				return new ResultResponse<ExamAssignResponse>
				{
					IsSuccessful = false,
					Message = "Provided status does not exist."
				};
			}

			
			exam.ExamStatusId = status.ExamStatusId;

	
			await dbContext.SaveChangesAsync();

			
			var response = new ExamAssignResponse
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

			return new ResultResponse<ExamAssignResponse>
			{
				IsSuccessful = true,
				Items = new List<ExamAssignResponse> { response }
			};
		}


	}
}