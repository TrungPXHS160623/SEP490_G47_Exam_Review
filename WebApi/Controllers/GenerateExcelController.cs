using Library.Common;
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

        [HttpGet("export")]
        public IActionResult ExportToExcel()
        {
            try
            {
                var excelData = _generateExcelRepository.GenerateExcel();

                return File(excelData, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "ExamData.xlsx");
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
		[HttpGet("export/{statusId}")]
		public IActionResult ExportToExcelByStatus(int statusId)
		{
			try
			{
				var excelData = _generateExcelRepository.GenerateExcelByStatus(statusId);

				return File(excelData, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", $"ExamData_Status_{statusId}.xlsx");
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
