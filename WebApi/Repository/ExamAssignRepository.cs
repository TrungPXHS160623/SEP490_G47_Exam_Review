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

			try
			{
				
				var exams = await (from e in dbContext.Exams
								   join s in dbContext.Subjects on e.SubjectId equals s.SubjectId
								   join cu in dbContext.CampusUserSubjects on s.SubjectId equals cu.SubjectId
								   join u in dbContext.Users on cu.UserId equals u.UserId
								   join es in dbContext.ExamStatuses on e.ExamStatusId equals es.ExamStatusId
								   where e.ExamStatusId == statusInProgress
										 && (u.UserId == userId || cu.UserId == userId) 
								   select new ExamAssignResponse
								   {
									   ExamId = e.ExamId,
									   ExamCode = e.ExamCode,
									   ExamDuration = e.ExamDuration,
									   ExamType = e.ExamType,
									   SubjectName = s.SubjectName,
									   ExamStatus = es.StatusContent,
									   ExamStatusId =es.ExamStatusId,
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
			catch (Exception ex)
			{
				// Log the exception or handle it accordingly
				return new ResultResponse<ExamAssignResponse>
				{
					IsSuccessful = false,
					Message = $"An error occurred: {ex.Message}"
				};
			}
		}
	}
}
