using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApi.IRepository;

namespace WebApi.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class ExamAssignController : ControllerBase
	{
		private readonly IExamAssignRepository examAssignmentRepository;
		public ExamAssignController(
   IExamAssignRepository examAssignmentRepository, IMapper mapper)
		{
			this.examAssignmentRepository = examAssignmentRepository;
		}


		[HttpGet("getExamAssign/{id:int}")]
		[AllowAnonymous]
		public async Task<IActionResult> GetExamAssign(int id)
		{
			var data = await this.examAssignmentRepository.GetExamAssign(id);

			return Ok(data);
		}

		[HttpPut("editExamStatus/{id:int}")]
		[AllowAnonymous]
		public async Task<IActionResult> EditExamStatus(int id, [FromBody] string newStatus)
		{
			var result = await this.examAssignmentRepository.GetAndEditExamAssign(id, newStatus);
			return Ok(result);
		}

	}
}
