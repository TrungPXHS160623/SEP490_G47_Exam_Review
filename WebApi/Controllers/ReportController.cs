using Library.Request;
using Library.Response;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApi.IRepository;
using WebApi.Repository;

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
        [HttpGet("get-reports/by-lecturerId/{id:int}")]
        [AllowAnonymous]
        public async Task<IActionResult> GetReportsByLecturerId(int id)
        {
            var data = await reportRepository.GetReportsByLecturerId(id);
            return Ok(data);
        }
        [HttpPost("SaveReport")]
        [AllowAnonymous]
        public async Task<IActionResult> CreateReport([FromBody] LectureExamResponse reportRequest)
        {
            var data = await reportRepository.AddEditReport(reportRequest);
            return Ok(data);
        }

		[HttpPut("Edit-Report/{id}")]
		[AllowAnonymous]
		public async Task<IActionResult> EditReport(int id, [FromBody] ReportRequest reportRequest)
		{
			var data = await reportRepository.EditReportById(id, reportRequest);
			return Ok(data);
		}


	}
}
