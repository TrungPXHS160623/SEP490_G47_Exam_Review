using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using WebApi.IRepository;

namespace WebApi.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class LecturerBySubjectController : ControllerBase
	{
		private readonly ILecturerBySubjectRepository _repository;

		public LecturerBySubjectController(ILecturerBySubjectRepository repository)
		{
			_repository = repository;
		}

		[HttpGet("LecturersBySubject")]
		public async Task<IActionResult> GetLecturers([Required] int subjectId, [Required] int campusId)
		{
			var lecturers = await _repository.GetLecturersBySubjectAndCampus(subjectId, campusId);
			return Ok(lecturers);
		}
	}
}
