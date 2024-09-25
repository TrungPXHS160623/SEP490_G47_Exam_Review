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

        [HttpGet("GetExamList")]
        public async Task<IActionResult> GetExamList()
        {
            var examInfo = await _examRepository.GetExamList();
            return Ok(examInfo);
        }

        [HttpGet("GetExamById/{examId}")]
        public async Task<IActionResult> GetExamById(int examId)
        {
            var examInfo = await _examRepository.GetExamById(examId);
            return Ok(examInfo);
        }
    }
}
