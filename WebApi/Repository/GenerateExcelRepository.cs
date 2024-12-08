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
        public byte[] GenerateExcelTime()
        {
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
            using (ExcelPackage package = new ExcelPackage())
            {
                ExcelWorksheet worksheet = package.Workbook.Worksheets.Add("Users");

                // Đặt tiêu đề
                worksheet.Cells[1, 1].Value = "STT";
                worksheet.Cells[1, 2].Value = "Giảng viên";
                worksheet.Cells[1, 3].Value = "Email";
                worksheet.Cells[1, 4].Value = "Số bài được giao";
                worksheet.Cells[1, 5].Value = "Tổng thời gian";

                // Lấy kỳ hiện tại
                var currentSemester = _context.Semesters
                    .FirstOrDefault(s => s.StartDate <= DateTime.Now && s.EndDate >= DateTime.Now);
                if (currentSemester == null)
                {
                    throw new Exception("Không tìm thấy kỳ hiện tại.");
                }

                // Lấy dữ liệu từ bảng Users và Exams
                var usersData = _context.Users
                    .Where(u => _context.Exams.Any(e => e.AssignedUserId == u.UserId && e.SemesterId == currentSemester.SemesterId))
                    .Select(u => new
                    {
                        u.UserId,
                        u.FullName,
                        u.Mail,
                        AssignedExamCount = _context.Exams.Count(e => e.AssignedUserId == u.UserId && e.SemesterId == currentSemester.SemesterId),
                        TotalDuration = _context.Exams
                            .Where(e => e.AssignedUserId == u.UserId && e.SemesterId == currentSemester.SemesterId)
                            .Sum(e => e.ExamDuration)
                    })
                    .ToList();

                int row = 2;
                int index = 1;

                foreach (var user in usersData)
                {
                    worksheet.Cells[row, 1].Value = index++;
                    worksheet.Cells[row, 2].Value = user.FullName;
                    worksheet.Cells[row, 3].Value = user.Mail;
                    worksheet.Cells[row, 4].Value = user.AssignedExamCount;
                    worksheet.Cells[row, 5].Value = user.TotalDuration;

                    // Căn lề cho các cột
                    for (int col = 1; col <= 5; col++)
                    {
                        worksheet.Cells[row, col].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Left;
                        worksheet.Cells[row, col].Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Center;
                    }

                    row++;
                }

                // Căn lề cho hàng tiêu đề
                for (int col = 1; col <= 5; col++)
                {
                    worksheet.Cells[1, col].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Left;
                    worksheet.Cells[1, col].Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Center;
                }

                worksheet.Cells[1, 1, row - 1, 5].AutoFitColumns(); // Tự động điều chỉnh chiều rộng cột

                // Chuyển đổi gói thành mảng byte
                return package.GetAsByteArray();
            }

        }

        public byte[] GenerateExcel()
        {
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

            using (ExcelPackage package = new ExcelPackage())
            {
                ExcelWorksheet worksheet = package.Workbook.Worksheets.Add("Exams");

                // Đặt tiêu đề
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
                var currentSemester = _context.Semesters
                .FirstOrDefault(s => s.StartDate <= DateTime.Now && s.EndDate >= DateTime.Now);
                if (currentSemester == null)
                {
                    throw new Exception("Không tìm thấy kỳ hiện tại.");
                }
                // Lấy dữ liệu từ bảng Exams và các bảng liên quan
                var exams = _context.Exams
                    .Include(e => e.Campus)
                    .Include(e => e.Subject)
                    .Include(e => e.Creater)
                    .Include(e => e.ExamStatus)
                    .Include(e => e.Reports)
                     .Where(e => e.SemesterId == currentSemester.SemesterId)// Truy vấn trực tiếp các báo cáo
                    .ToList();
                int row = 2;
                int index = 1;
                // Điền dữ liệu
                foreach (var exam in exams)
                {
                    worksheet.Cells[row, 1].Value = index++;
                    var assignedUser = _context.Users
                    .FirstOrDefault(u => u.UserId == exam.AssignedUserId);
                    worksheet.Cells[row, 2].Value = assignedUser?.Mail ?? "N/A";
                    worksheet.Cells[row, 3].Value = exam.Subject.SubjectCode; // Môn thi
                    worksheet.Cells[row, 4].Value = exam.ExamDuration; // Đợt thi
                    worksheet.Cells[row, 5].Value = exam.ExamType; // Hình thức thi
                    worksheet.Cells[row, 6].Value = exam.EstimatedTimeTest?.ToString("dd-MM-yyyy"); // Ngày dự kiến test đề
                    worksheet.Cells[row, 7].Value = exam.ExamCode; // Mã thi
                    worksheet.Cells[row, 8].Value = exam.Campus.CampusName; // Cơ sở
                    worksheet.Cells[row, 9].Value = exam.Subject.SubjectName; // Tên môn

                    // Lấy thông tin chi tiết báo cáo và phương án khắc phục từ bảng Reports
                    var reports = exam.Reports.ToList();

                    string combinedReportContents = reports.Any()
                        ? string.Join("\n\n", reports.Select(r => $"Comment: {r.ReportContent}"))
                        : "N/A";
                    worksheet.Cells[row, 10].Value = combinedReportContents;

                    string combinedSolutions = reports.Any()
                        ? string.Join("\n\n", reports.Select(r => $"Solution: {r.QuestionSolutionDetail}"))
                        : "N/A";
                    worksheet.Cells[row, 11].Value = combinedSolutions;

                    // Đặt ngày sửa chỉ nếu trạng thái là "Complete"
                    worksheet.Cells[row, 12].Value = exam.ExamStatus?.StatusContent == "Complete"
                        ? exam.ExamStatus.UpdateDate?.ToString("dd-MM-yyyy") ?? "N/A"
                        : "N/A"; // Đặt thành "N/A" nếu không hoàn thành

                    // Đặt trạng thái
                    worksheet.Cells[row, 13].Value = exam.ExamStatus?.StatusContent ?? "N/A";

                    // Kích hoạt ngắt dòng cho các cột báo cáo và giải pháp
                    worksheet.Cells[row, 10].Style.WrapText = true;
                    worksheet.Cells[row, 11].Style.WrapText = true;

                    // Căn lề cho tất cả các cột
                    for (int col = 1; col <= 13; col++)
                    {
                        worksheet.Cells[row, col].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Left;
                        worksheet.Cells[row, col].Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Center;
                    }

                    row++;
                }

                // Căn lề cho hàng tiêu đề
                for (int col = 1; col <= 13; col++)
                {
                    worksheet.Cells[1, col].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Left;
                    worksheet.Cells[1, col].Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Center;
                }

                worksheet.Cells[1, 1, row - 1, 13].AutoFitColumns(); // Tự động điều chỉnh chiều rộng cột

                // Chuyển đổi gói thành mảng byte
                return package.GetAsByteArray();
            }
        }

        public byte[] GenerateExcelByStatus(int userId)
        {
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

            using (ExcelPackage package = new ExcelPackage())
            {
                ExcelWorksheet worksheet = package.Workbook.Worksheets.Add("Exams");

                // Thiết lập tiêu đề cho các cột
                worksheet.Cells[1, 1].Value = "STT";
                worksheet.Cells[1, 2].Value = "Giảng viên";
                worksheet.Cells[1, 3].Value = "Môn thi";
                worksheet.Cells[1, 4].Value = "Đợt thi";
                worksheet.Cells[1, 5].Value = "Hình thức thi";
                worksheet.Cells[1, 6].Value = "Mã Đề Thi";
                worksheet.Cells[1, 7].Value = "Trạng thái Đề";
                worksheet.Cells[1, 8].Value = "Chi tiết câu hỏi lỗi";
                worksheet.Cells[1, 9].Value = "Phương án khắc phục lỗi";

                var exams = _context.Exams
                    .Include(e => e.Campus)
                    .Include(e => e.Subject)
                    .Include(e => e.Creater)
                    .Include(e => e.ExamStatus)
                    .Where(e =>
                        _context.CampusUserFaculties.Any(mcf =>
                            mcf.UserId == userId &&
                            mcf.CampusId == e.CampusId &&
                            mcf.FacultyId == e.Subject.FacultyId) ||
                        _context.CampusUserSubjects.Any(cus =>
                            cus.UserId == userId &&
                            cus.CampusId == e.CampusId &&
                            cus.SubjectId == e.SubjectId) &&
                        (e.ExamStatusId == 6 || e.ExamStatusId == 5)) // Điều kiện trạng thái
                    .OrderByDescending(e => e.Reports.Any()) // Sắp xếp theo việc có báo cáo hay không
                    .ToList();

                int row = 2;
                int index = 1;

                // Điền dữ liệu vào bảng
                foreach (var exam in exams)
                {
                    worksheet.Cells[row, 1].Value = index++; // Số thứ tự

                    var assignedUser = _context.Users
                      .FirstOrDefault(u => u.UserId == exam.AssignedUserId);
                    worksheet.Cells[row, 2].Value = assignedUser?.Mail ?? "N/A";
                    // Điền các thông tin khác về kỳ thi
                    worksheet.Cells[row, 3].Value = exam.Subject.SubjectCode; // Môn thi
                    worksheet.Cells[row, 4].Value = exam.ExamDuration; // Đợt thi
                    worksheet.Cells[row, 5].Value = exam.ExamType; // Hình thức thi
                    worksheet.Cells[row, 6].Value = exam.ExamCode;
                    worksheet.Cells[row, 7].Value = exam.ExamStatus?.StatusContent ?? "N/A";
                    // Lấy chi tiết câu hỏi lỗi và phương án khắc phục từ bảng Reports
                    var reports = _context.Reports
                        .Where(r => r.ExamId == exam.ExamId)
                        .Select(r => new
                        {
                            Content = r.ReportContent,
                            Solution = r.QuestionSolutionDetail
                        })
                        .ToList();

                    worksheet.Cells[row, 8].Value = reports.Any()
                        ? string.Join("\n\n", reports.Select(r => $"Comment: {r.Content}"))
                        : "N/A";

                    worksheet.Cells[row, 9].Value = reports.Any()
                        ? string.Join("\n\n", reports.Select(r => $"Solution: {r.Solution}"))
                        : "N/A";

                    for (int col = 1; col <= 9; col++)
                    {
                        worksheet.Cells[row, col].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Left;
                        worksheet.Cells[row, col].Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Center;
                    }

                    row++;
                }

                // Canh lề cho hàng tiêu đề
                for (int col = 1; col <= 9; col++)
                {
                    worksheet.Cells[1, col].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Left;
                    worksheet.Cells[1, col].Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Center;
                }

                worksheet.Cells[1, 1, row - 1, 9].AutoFitColumns();

                // Chuyển đổi gói thành mảng byte
                return package.GetAsByteArray();
            }
        }



    }
}
