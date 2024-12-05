using Library.Common;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApi.IRepository;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GenerateExcelController : ControllerBase
    {
        private readonly IGenerateExcelRepository _generateExcelRepository;

        public GenerateExcelController(IGenerateExcelRepository generateExcelRepository)
        {
            _generateExcelRepository = generateExcelRepository;
        }

        // Endpoint xuất tất cả các exams
        [AllowAnonymous]
        [HttpGet("export-all")]
        public IActionResult ExportAllToExcel()
        {
            try
            {
                var excelData = _generateExcelRepository.GenerateExcel();

                return File(excelData, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "ExamData_All.xlsx");
            }
            catch (Exception ex)
            {
                return BadRequest(new ResultResponse<byte[]>
                {
                    IsSuccessful = false,
                    Message = ex.Message
                });
            }
        }
        [AllowAnonymous]
        [HttpGet("exportTime")]
        public IActionResult GenerateExcelTime()
        {
            try
            {
                var excelData = _generateExcelRepository.GenerateExcelTime();

                return File(excelData, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "ExportTime_All.xlsx");
            }
            catch (Exception ex)
            {
                return BadRequest(new ResultResponse<byte[]>
                {
                    IsSuccessful = false,
                    Message = ex.Message
                });
            }
        }
        // Endpoint xuất exams theo trạng thái bằng statusId
        [AllowAnonymous]
        [HttpGet("status/{userid}")]
        public IActionResult ExportToExcelByStatus(int userid)
        {
            try
            {
                var excelData = _generateExcelRepository.GenerateExcelByStatus(userid);

                return File(excelData, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", $"ExportData.xlsx");
            }
            catch (Exception ex)
            {
                return BadRequest(new ResultResponse<byte[]>
                {
                    IsSuccessful = false,
                    Message = ex.Message
                });
            }
        }
    }
}
