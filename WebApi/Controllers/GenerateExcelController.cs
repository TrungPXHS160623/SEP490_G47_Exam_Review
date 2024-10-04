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
			var excelData = _generateExcelRepository.GenerateExcel();

			return File(excelData, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "ExamData.xlsx");
		}
	}
}
