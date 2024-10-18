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
						.ThenInclude(ia => ia.Reports)
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
					var instructorAssignments = exam.InstructorAssignments;

					// Create a string containing all reports
					var allReports = instructorAssignments
						.SelectMany(ia => ia.Reports)
						.Select(r => $"Comment: {r.ReportContent}\nSolution: {r.QuestionSolutionDetail}")
						.ToList();

					string combinedReport = allReports.Any() ? string.Join("\n\n", allReports) : "N/A";

					// Set lecturer (assuming you want to get the first one)
					var assignedInstructor = instructorAssignments.FirstOrDefault();
					worksheet.Cells[row, 2].Value = assignedInstructor?.AssignedUser?.Mail ?? "N/A"; // Lecturer

					worksheet.Cells[row, 3].Value = exam.Subject.SubjectCode; // Subject
					worksheet.Cells[row, 4].Value = exam.ExamDuration; // Exam batch
					worksheet.Cells[row, 5].Value = exam.ExamType; // Exam type
					worksheet.Cells[row, 6].Value = exam.EstimatedTimeTest?.ToString("dd-MM-yyyy"); // Planned test date
					worksheet.Cells[row, 7].Value = exam.ExamCode; // Exam code
					worksheet.Cells[row, 8].Value = exam.Campus.CampusName; // Campus
					worksheet.Cells[row, 9].Value = exam.Subject.SubjectName; // Subject name

					// Get the details of the reports
					var allReportContents = instructorAssignments
						.SelectMany(ia => ia.Reports)
						.Select(r => r.ReportContent)
						.ToList();

					string combinedReportContents = allReportContents.Any() ? string.Join("\n\n", allReportContents) : "N/A";
					worksheet.Cells[row, 10].Value = combinedReportContents;

					var allSolutions = instructorAssignments
						.SelectMany(ia => ia.Reports)
						.Select(r => r.QuestionSolutionDetail)
						.ToList();

					string combinedSolutions = allSolutions.Any() ? string.Join("\n\n", allSolutions) : "N/A";
					worksheet.Cells[row, 11].Value = combinedSolutions;

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

					// Enable text wrapping for the comments and solutions columns
					worksheet.Cells[row, 10].Style.WrapText = true;
					worksheet.Cells[row, 11].Style.WrapText = true;

					// Left align the text in all columns and center vertically
					for (int col = 1; col <= 13; col++)
					{
						worksheet.Cells[row, col].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Left;
						worksheet.Cells[row, col].Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Center;
					}

					row++;
				}

				// Left align the header row and center vertically
				for (int col = 1; col <= 13; col++)
				{
					worksheet.Cells[1, col].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Left;
					worksheet.Cells[1, col].Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Center;
				}

				worksheet.Cells[1, 1, row - 1, 13].AutoFitColumns(); // Auto-fit all columns

				// Convert the package to a byte array
				return package.GetAsByteArray();
			}
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

				// Fetch data from exams based on statusId
				var exams = _context.Exams
					.Include(e => e.Campus)
					.Include(e => e.Subject)
					.Include(e => e.Creater)
					.Include(e => e.ExamStatus)
					.Include(e => e.InstructorAssignments)
						.ThenInclude(ia => ia.AssignedUser)
					.Include(e => e.InstructorAssignments)
						.ThenInclude(ia => ia.Reports)
					.Where(e => e.ExamStatusId == statusId) // Lọc theo statusId
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
					var instructorAssignments = exam.InstructorAssignments;

					// Tạo chuỗi chứa tất cả các report
					var allReports = instructorAssignments
						.SelectMany(ia => ia.Reports)
						.Select(r => $"Comment: {r.ReportContent}\nSolution: {r.QuestionSolutionDetail}")
						.ToList();

					string combinedReport = allReports.Any() ? string.Join("\n\n", allReports) : "N/A";

					// Set lecturer (assuming bạn muốn lấy người đầu tiên)
					var assignedInstructor = instructorAssignments.FirstOrDefault();
					worksheet.Cells[row, 2].Value = assignedInstructor?.AssignedUser?.Mail ?? "N/A"; // Lecturer

					worksheet.Cells[row, 3].Value = exam.Subject.SubjectCode; // Subject
					worksheet.Cells[row, 4].Value = exam.ExamDuration; // Exam batch
					worksheet.Cells[row, 5].Value = exam.ExamType; // Exam type
					worksheet.Cells[row, 6].Value = exam.EstimatedTimeTest?.ToString("dd-MM-yyyy"); // Planned test date
					worksheet.Cells[row, 7].Value = exam.ExamCode; // Exam code
					worksheet.Cells[row, 8].Value = exam.Campus.CampusName; // Campus
					worksheet.Cells[row, 9].Value = exam.Subject.SubjectName; // Subject name

					// Chỉ lấy các report chứa report
					var allReportContents = instructorAssignments
						.SelectMany(ia => ia.Reports)
						.Select(r => r.ReportContent)
						.ToList();

					string combinedReportContents = allReportContents.Any() ? string.Join("\n\n", allReportContents) : "N/A";
					worksheet.Cells[row, 10].Value = combinedReportContents;

					var allSolutions = instructorAssignments
						.SelectMany(ia => ia.Reports)
						.Select(r => r.QuestionSolutionDetail)
						.ToList();

					string combinedSolutions = allSolutions.Any() ? string.Join("\n\n", allSolutions) : "N/A";
					worksheet.Cells[row, 11].Value = combinedSolutions;

					// Set modification date only if status is "Complete"
					if (exam.ExamStatus?.StatusContent == "Complete")
					{
						worksheet.Cells[row, 12].Value = exam.ExamStatus.UpdateDate?.ToString("dd-MM-yyyy") ?? "N/A";
					}
					else
					{
						worksheet.Cells[row, 12].Value = "N/A"; // Set to "N/A" nếu không hoàn thành
					}

					// Set the status
					worksheet.Cells[row, 13].Value = exam.ExamStatus?.StatusContent ?? "N/A";

					// Enable text wrapping for the comments and solutions columns
					worksheet.Cells[row, 10].Style.WrapText = true;
					worksheet.Cells[row, 11].Style.WrapText = true;

					// Left align the text in all columns and center vertically
					for (int col = 1; col <= 13; col++)
					{
						worksheet.Cells[row, col].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Left;
						worksheet.Cells[row, col].Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Center;
					}

					row++;
				}

				// Left align the header row and center vertically
				for (int col = 1; col <= 13; col++)
				{
					worksheet.Cells[1, col].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Left;
					worksheet.Cells[1, col].Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Center;
				}

				worksheet.Cells[1, 1, row - 1, 13].AutoFitColumns();

				// Convert the package to a byte array
				return package.GetAsByteArray();
			}
		}

	}
}
