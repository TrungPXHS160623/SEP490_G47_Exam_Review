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
        [HttpGet("Get-All-Report")]
        [AllowAnonymous]
        public async Task<IActionResult> GetAllReport()
        {
            var data = await reportRepository.GetAllReport();
            return Ok(data);
        }
        [HttpPost("Create-Report")]
        [AllowAnonymous]
        public async Task<IActionResult> CreateReport([FromBody] ReportRequest reportRequest)
        {
            var data = await reportRepository.AddReport(reportRequest);
            return Ok(data);
        }

    }
}
