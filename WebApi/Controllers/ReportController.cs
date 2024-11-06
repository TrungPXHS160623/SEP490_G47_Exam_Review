using Library.Request;
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
        private readonly IReportRepository reportRepository;

        public ReportController(IReportRepository reportRepository)
        {
            this.reportRepository = reportRepository;
        }

        [HttpPost("SaveReport/{isSubmit:bool}")]
        [AllowAnonymous]
        public async Task<IActionResult> CreateReport([FromBody] LectureExamResponse reportRequest, bool isSubmit)
        {
            var data = await reportRepository.AddEditReport(reportRequest, isSubmit);
            return Ok(data);
        }

        [HttpGet("GetReportDuration/{assignmentId}")]
        [AllowAnonymous]
        public async Task<IActionResult> GetReportDuration(int assignmentId)
        {
            var data = await reportRepository.GetReportDuration(assignmentId);
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
