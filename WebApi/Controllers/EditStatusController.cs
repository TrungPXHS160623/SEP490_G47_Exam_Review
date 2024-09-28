using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApi.IRepository;

namespace WebApi.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class EditStatusController : ControllerBase
	{
		private readonly IEditStatusRepository editStatusRepository;
		public EditStatusController(
   IEditStatusRepository editStatusRepository, IMapper mapper)
		{
			this.editStatusRepository = editStatusRepository;
		}	

		[HttpPut("editExamStatus/{id:int}")]
		[AllowAnonymous]
		public async Task<IActionResult> EditExamStatus(int id, [FromBody] string newStatus)
		{
			var result = await this.editStatusRepository.EditStatus(id, newStatus);
			return Ok(result);
		}

	}
}
