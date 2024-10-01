using Library.Request;
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
        [HttpPost("create-reports/by-lecturer")]
        [AllowAnonymous]
        public async Task<IActionResult> CreateReport([FromBody] ReportRequest reportRequest)
        {
            var data = await reportRepository.CreateReport(reportRequest);
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
