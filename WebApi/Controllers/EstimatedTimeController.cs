using Library.Request;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApi.IRepository;

namespace WebApi.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class EstimatedTimeController : ControllerBase
	{
		private readonly IEstimatedTimeRepository estimatedTimeRepository;

		public EstimatedTimeController(IEstimatedTimeRepository estimatedTimeRepository)
		{
			this.estimatedTimeRepository = estimatedTimeRepository;
		}
		[HttpGet("Get-EstimatedTimeTest/{id}")]
		[AllowAnonymous]
		public async Task<IActionResult> GetEstimatedTimeTest(int id)
		{
			try
			{
				var result = await estimatedTimeRepository.GetEstimatedTimeTest(id);
				return Ok(result); 
			}
			catch (Exception ex)
			{
				return BadRequest(new { Message = ex.Message });
			}
		}
		[HttpPost("Add-EstimatedTimeTest/{id}")]
		[AllowAnonymous]
		public async Task<IActionResult> AddEstimatedTimeTest(int id, [FromBody] EstimatedTimeRequest request)
		{
			try
			{
				var result = await estimatedTimeRepository.AddEstimatedTimeTest(id, request);
				return Ok(result);  
			}
			catch (Exception ex)
			{
				return BadRequest(new { Message = ex.Message });
			}
		}

		[HttpPut("Update-EstimatedTimeTest/{id}")]
		[AllowAnonymous]
		public async Task<IActionResult> UpdateEstimatedTimeTest(int id, [FromBody] EstimatedTimeRequest request)
		{
			try
			{
				var result = await estimatedTimeRepository.UpdateEstimatedTimeTest(id, request);
				return Ok(result);  
			}
			catch (Exception ex)
			{
				return BadRequest(new { Message = ex.Message });
			}
		}
	}
}
