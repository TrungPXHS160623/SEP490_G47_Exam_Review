﻿using Library.Request;
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
        [HttpPost("SaveReport/{isSubmit:bool}")]
        [AllowAnonymous]
        public async Task<IActionResult> CreateReport([FromBody] LectureExamResponse reportRequest, bool isSubmit)
        {
            var data = await reportRepository.AddEditReport(reportRequest,isSubmit);
            return Ok(data);
        }

		[HttpPut("Edit-Report/{id}")]
		[AllowAnonymous]
		public async Task<IActionResult> EditReport(int id, [FromBody] ReportRequest reportRequest)
		{
			var data = await reportRepository.EditReportById(id, reportRequest);
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
