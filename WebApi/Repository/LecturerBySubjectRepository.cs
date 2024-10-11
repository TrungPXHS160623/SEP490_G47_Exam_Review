using Library.Models;
using Library.Response;
using Microsoft.EntityFrameworkCore;
using WebApi.IRepository;

namespace WebApi.Repository
{
	public class LecturerBySubjectRepository : ILecturerBySubjectRepository
	{
		private readonly QuizManagementContext _context;

		public LecturerBySubjectRepository(QuizManagementContext context)
		{
			_context = context;
		}

		public async Task<IEnumerable<LecturerBySubjectResponse>> GetLecturersBySubjectAndCampus(int subjectId, int campusId)
		{
			var lecturers = await _context.CampusUserSubjects
		.Where(cus => cus.SubjectId == subjectId && cus.CampusId == campusId && cus.IsLecturer)
		.Select(cus => new LecturerBySubjectResponse
		{
			Lecturer = cus.User.Mail, // Assuming User entity has a Mail property
			SubjectName = cus.Subject.SubjectName,		
			Campus = cus.Campus.CampusName,
			ExamCodes = cus.Subject.Exams.Select(e => e.ExamCode).ToList(),
		})
		.ToListAsync();

			return lecturers;
		}
	}
}
