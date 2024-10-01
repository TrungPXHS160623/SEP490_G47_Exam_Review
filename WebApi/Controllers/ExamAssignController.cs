using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using WebApi.IRepository;
using Library.Request;
using Library.Response;

namespace WebApi.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class ExamAssignController : ControllerBase
	{
		private readonly IExamAssignRepository examAssignRepository;
		private readonly IMapper mapper;

		public ExamAssignController(IExamAssignRepository examAssignRepository, IMapper mapper)
		{
			this.examAssignRepository = examAssignRepository;
			this.mapper = mapper;
		}

		[HttpGet("in-progress/{userId}")]
		public async Task<IActionResult> GetExamsInProgress(int userId)
		{
			var result = await examAssignRepository.GetExamAssignByHeadId(userId);
			if (!result.IsSuccessful)
			{
				return NotFound(result.Message);
			}
			return Ok(result.Items);
		}
	}
}
