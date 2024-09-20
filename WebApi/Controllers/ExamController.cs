using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApi.IRepository;

namespace WebApi.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class ExamController : ControllerBase
	{
		private readonly IExamRepository _examRepository;

		public ExamController(IExamRepository examRepository)
		{
			_examRepository = examRepository;
		}

		[HttpGet("info")]
		public async Task<IActionResult> GetExamInfo()
		{
			var examInfo = await _examRepository.GetExamInfoAsync();
			return Ok(examInfo);
		}
	}
}
