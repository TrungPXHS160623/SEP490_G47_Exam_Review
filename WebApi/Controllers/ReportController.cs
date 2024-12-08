using Library.Response;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApi.IRepository;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReportController : ApiBaseController
    {
        private readonly ILogHistoryRepository _logHistoryRepository;
        private readonly IReportRepository reportRepository;

        public ReportController(IReportRepository reportRepository, ILogHistoryRepository logHistoryRepository)
        {
            this.reportRepository = reportRepository;
            _logHistoryRepository = logHistoryRepository;
        }

        [HttpPost("SaveReport/{isSubmit:bool}")]
        [AllowAnonymous]
        public async Task<IActionResult> CreateReport([FromBody] LectureExamResponse reportRequest, bool isSubmit)
        {
            var data = await reportRepository.AddEditReport(reportRequest, isSubmit);

            if(isSubmit)
            {
                await _logHistoryRepository.LogAsync($"Submit report with exam code : {reportRequest.ExamCode}", JwtToken);
            }
            return Ok(data);
        }

        [HttpPost("UploadReportWithFiles/{isSubmit:bool}")]
        [AllowAnonymous]
        public async Task<IActionResult> UploadReportWithFiles([FromForm] LectureExamResponseFinal reportRequest, bool isSubmit)
        {
            // Kiểm tra nếu báo cáo là null hoặc không có file
            if (reportRequest == null ||
                reportRequest.ReportList == null ||
                !reportRequest.ReportList.Any() ||
                reportRequest.ReportList.Any(r => r.Files == null || !r.Files.Any()))
            {
                return BadRequest("Invalid report data.");
            }

            // Gọi phương thức UploadReportWithFiles từ service hoặc repository
            var response = await reportRepository.UploadReportWithFiles(reportRequest, isSubmit);

            if (response.IsSuccessful)
            {
                return Ok(response);
            }

            return BadRequest(response.Message);
        }



    }
}
