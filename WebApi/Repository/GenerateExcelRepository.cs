using Library.Models;
using Microsoft.EntityFrameworkCore;
using OfficeOpenXml;
using WebApi.IRepository;

namespace WebApi.Repository
{
    public class GenerateExcelRepository : IGenerateExcelRepository
    {
        private readonly QuizManagementContext _context;

        public GenerateExcelRepository(QuizManagementContext context)
        {
            _context = context;
        }

        public byte[] GenerateExcel()
        {
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

            using (ExcelPackage package = new ExcelPackage())
            {
                ExcelWorksheet worksheet = package.Workbook.Worksheets.Add("Exams");

                // Set headers for all tables' data
                worksheet.Cells[1, 1].Value = "STT";
                worksheet.Cells[1, 2].Value = "Giảng viên";
                worksheet.Cells[1, 3].Value = "Môn thi";
                worksheet.Cells[1, 4].Value = "Đợt thi";
                worksheet.Cells[1, 5].Value = "Hình thức thi";
                worksheet.Cells[1, 6].Value = "Ngày dự kiến test đề";
                worksheet.Cells[1, 7].Value = "Exam code";
                worksheet.Cells[1, 8].Value = "Cơ sở";
                worksheet.Cells[1, 9].Value = "Tên môn";
                worksheet.Cells[1, 10].Value = "Chi tiết câu hỏi lỗi";
                worksheet.Cells[1, 11].Value = "Phương án khắc phục lỗi";
                worksheet.Cells[1, 12].Value = "Ngày sửa";
                worksheet.Cells[1, 13].Value = "Trạng Thái";

                // Fetch data from all relevant tables
                var exams = _context.Exams
            .Include(e => e.Campus)
                 .Include(e => e.Subject)
                 .Include(e => e.Creater)
                      .Include(e => e.ExamStatus)
                     .Include(e => e.InstructorAssignments)
                        .ThenInclude(ia => ia.AssignedUser)
               .Include(e => e.InstructorAssignments)
                     .ThenInclude(ia => ia.Reports) // Reports through InstructorAssignments
                  .ToList();


                // Sort the exams by whether they have a reviewer (Assigned Instructor)
                var sortedExams = exams
                    .OrderByDescending(e => e.InstructorAssignments.Any(ia => ia.AssignedUser != null))
                    .ToList();

                int row = 2;
                int index = 1;

                // Fill data
                foreach (var exam in sortedExams)
                {
                    worksheet.Cells[row, 1].Value = index++;
                    var assignedInstructor = exam.InstructorAssignments.FirstOrDefault();
                    worksheet.Cells[row, 2].Value = assignedInstructor?.AssignedUser?.Mail ?? "N/A"; // Lecturer
                    worksheet.Cells[row, 3].Value = exam.Subject.SubjectCode; // Subject
                    worksheet.Cells[row, 4].Value = exam.ExamDuration; // Exam batch
                    worksheet.Cells[row, 5].Value = exam.ExamType; // Exam type
                    worksheet.Cells[row, 6].Value = exam.EstimatedTimeTest?.ToString("dd-MM-yyyy"); // Planned test date
                    worksheet.Cells[row, 7].Value = exam.ExamCode; // Exam code
                    worksheet.Cells[row, 8].Value = exam.Campus.CampusName; // Campus
                    worksheet.Cells[row, 9].Value = exam.Subject.SubjectName; // Subject name

                    var instructorAssignment = exam.InstructorAssignments.FirstOrDefault();
                    var report = instructorAssignment?.Reports.FirstOrDefault();
                    worksheet.Cells[row, 10].Value = report?.ReportContent ?? "N/A"; // Report Review

                    var solution = report?.QuestionSolutionDetail;
                    worksheet.Cells[row, 11].Value = solution ?? "N/A"; // Solution Details
                    // Set modification date only if status is "Complete"
                    if (exam.ExamStatus?.StatusContent == "Complete")
                    {
                        worksheet.Cells[row, 12].Value = exam.ExamStatus.UpdateDate?.ToString("dd-MM-yyyy") ?? "N/A";
                    }
                    else
                    {
                        worksheet.Cells[row, 12].Value = "N/A"; // Set to "N/A" if not complete
                    }

                    // Set the status
                    worksheet.Cells[row, 13].Value = exam.ExamStatus?.StatusContent ?? "N/A";

                    row++;
                }

                worksheet.Cells[1, 1, row - 1, 13].AutoFitColumns();

                // Convert the package to a byte array
                return package.GetAsByteArray();
            }
            return null;
        }

		public byte[] GenerateExcelByStatus(int statusId)
		{
			ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

			using (ExcelPackage package = new ExcelPackage())
			{
				ExcelWorksheet worksheet = package.Workbook.Worksheets.Add("Exams");

				// Set headers for all tables' data
				worksheet.Cells[1, 1].Value = "STT";
				worksheet.Cells[1, 2].Value = "Giảng viên";
				worksheet.Cells[1, 3].Value = "Môn thi";
				worksheet.Cells[1, 4].Value = "Đợt thi";
				worksheet.Cells[1, 5].Value = "Hình thức thi";
				worksheet.Cells[1, 6].Value = "Ngày dự kiến test đề";
				worksheet.Cells[1, 7].Value = "Exam code";
				worksheet.Cells[1, 8].Value = "Cơ sở";
				worksheet.Cells[1, 9].Value = "Tên môn";
				worksheet.Cells[1, 10].Value = "Chi tiết câu hỏi lỗi";
				worksheet.Cells[1, 11].Value = "Phương án khắc phục lỗi";
				worksheet.Cells[1, 12].Value = "Ngày sửa";
				worksheet.Cells[1, 13].Value = "Trạng Thái";

				// Fetch data from exams based on status
				var exams = _context.Exams
					.Include(e => e.Campus)
					.Include(e => e.Subject)
					.Include(e => e.Creater)
					.Include(e => e.ExamStatus)
					.Include(e => e.InstructorAssignments)
						.ThenInclude(ia => ia.AssignedUser)
					.Include(e => e.InstructorAssignments)
						.ThenInclude(ia => ia.Reports)
					.Where(e => e.ExamStatusId == statusId) // Filter by statusId
					.ToList();

				// Sort the exams by whether they have a reviewer (Assigned Instructor)
				var sortedExams = exams
					.OrderByDescending(e => e.InstructorAssignments.Any(ia => ia.AssignedUser != null))
					.ToList();

				int row = 2;
				int index = 1;

				// Fill data
				foreach (var exam in sortedExams)
				{
					worksheet.Cells[row, 1].Value = index++;
					var assignedInstructor = exam.InstructorAssignments.FirstOrDefault();
					worksheet.Cells[row, 2].Value = assignedInstructor?.AssignedUser?.Mail ?? "N/A"; // Lecturer
					worksheet.Cells[row, 3].Value = exam.Subject.SubjectCode; // Subject
					worksheet.Cells[row, 4].Value = exam.ExamDuration; // Exam batch
					worksheet.Cells[row, 5].Value = exam.ExamType; // Exam type
					worksheet.Cells[row, 6].Value = exam.EstimatedTimeTest?.ToString("dd-MM-yyyy"); // Planned test date
					worksheet.Cells[row, 7].Value = exam.ExamCode; // Exam code
					worksheet.Cells[row, 8].Value = exam.Campus.CampusName; // Campus
					worksheet.Cells[row, 9].Value = exam.Subject.SubjectName; // Subject name

					var instructorAssignment = exam.InstructorAssignments.FirstOrDefault();
					var report = instructorAssignment?.Reports.FirstOrDefault();
					worksheet.Cells[row, 10].Value = report?.ReportContent ?? "N/A"; // Report Review

					var solution = report?.QuestionSolutionDetail;
					worksheet.Cells[row, 11].Value = solution ?? "N/A"; // Solution Details
																		// Set modification date only if status is "Complete"
					if (exam.ExamStatus?.StatusContent == "Complete")
					{
						worksheet.Cells[row, 12].Value = exam.ExamStatus.UpdateDate?.ToString("dd-MM-yyyy") ?? "N/A";
					}
					else
					{
						worksheet.Cells[row, 12].Value = "N/A"; // Set to "N/A" if not complete
					}

					// Set the status
					worksheet.Cells[row, 13].Value = exam.ExamStatus?.StatusContent ?? "N/A";

					row++;
				}

				worksheet.Cells[1, 1, row - 1, 13].AutoFitColumns();

				// Convert the package to a byte array
				return package.GetAsByteArray();
			}
		}

	}
}
