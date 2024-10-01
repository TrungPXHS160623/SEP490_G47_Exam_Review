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
				var instructorAssignment = await dbContext.InstructorAssignments
					.Include(x => x.Exam)
					.ThenInclude(x => x.Subject)
					.FirstOrDefaultAsync(x => x.AssignmentId == instructorAssignmentId);

				if (instructorAssignment == null)
				{
					throw new Exception("Instructor Assignment not found.");
				}

				
				return new EstimatedTimeResponse
				{
					ExamCode = instructorAssignment.Exam.ExamCode,
					SubjectName = instructorAssignment.Exam.Subject.SubjectName,
					EstimatedTimeTest = instructorAssignment.Exam.EstimatedTimeTest
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
				var instructorAssignment = await dbContext.InstructorAssignments
					.Include(x => x.Exam)
					.ThenInclude(x => x.Subject)
					.FirstOrDefaultAsync(x => x.AssignmentId == instructorAssignmentId);

				if (instructorAssignment == null)
				{
					throw new Exception("Instructor Assignment not found.");
				}

				
				instructorAssignment.Exam.EstimatedTimeTest = request.EstimatedTimeTest;
				instructorAssignment.CreateDate = DateTime.Now;

				dbContext.InstructorAssignments.Update(instructorAssignment);
				await dbContext.SaveChangesAsync();

				
				return new EstimatedTimeResponse
				{
					ExamCode = instructorAssignment.Exam.ExamCode,
					SubjectName = instructorAssignment.Exam.Subject.SubjectName,
					EstimatedTimeTest = instructorAssignment.Exam.EstimatedTimeTest
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
				var instructorAssignment = await dbContext.InstructorAssignments
					.Include(x => x.Exam)
					.ThenInclude(x => x.Subject)
					.FirstOrDefaultAsync(x => x.AssignmentId == instructorAssignmentId);

				if (instructorAssignment == null)
				{
					throw new Exception("Instructor Assignment not found.");
				}

				
				instructorAssignment.Exam.EstimatedTimeTest = request.EstimatedTimeTest;
				instructorAssignment.UpdateDate = DateTime.Now;

				dbContext.InstructorAssignments.Update(instructorAssignment);
				await dbContext.SaveChangesAsync();

				
				return new EstimatedTimeResponse
				{
					ExamCode = instructorAssignment.Exam.ExamCode,
					SubjectName = instructorAssignment.Exam.Subject.SubjectName,
					EstimatedTimeTest = instructorAssignment.Exam.EstimatedTimeTest
				};
			}
			catch (Exception ex)
			{
				throw new Exception(ex.Message);
			}
		}
	}
}
