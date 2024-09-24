using Library.Models.Dtos;
using Library.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApi.IRepository;

public class ExamRepository : IExamRepository
{
	private readonly QuizManagementContext _context;

	public ExamRepository(QuizManagementContext context)
	{
		_context = context;
	}

	public async Task<IEnumerable<ExamInfoDto>> GetExamInfoAsync()
	{
		var examInfo = await _context.InstructorAssignments
			.Include(ia => ia.Exam) // Kết nối với Exam
			.Include(ia => ia.Exam.Subject) // Kết nối với Subject từ Exam
			.Include(ia => ia.Exam.ExamStatus) // Kết nối với ExamStatuses
			.Include(ia => ia.AssignedToNavigation) // Kết nối với giảng viên
			.Select(ia => new ExamInfoDto
			{
				DepartmentName = ia.Exam.Subject.Department.DepartmentName, // Tên Chuyên Ngành
				SubjectName = ia.Exam.Subject.SubjectName, // Tên Môn Học
				ExamCode = ia.Exam.ExamCode, // Mã Bài Thi
				Status = ia.Exam.ExamStatus.StatusContent, // Trạng Thái
				InstructorName = ia.AssignedToNavigation.Mail // Tên Giảng Viên
			})
			.ToListAsync();

		return examInfo;
	}
}
