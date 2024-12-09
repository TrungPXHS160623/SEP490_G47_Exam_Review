using Library.Request;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApi.IRepository;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SemesterController : ApiBaseController
    {
        private readonly ISemesterRepository semesterRepository;
        private readonly ILogHistoryRepository _logHistoryRepository;

        public SemesterController(ISemesterRepository semesterRepository, ILogHistoryRepository logHistoryRepository)
        {
            this.semesterRepository = semesterRepository;
            _logHistoryRepository = logHistoryRepository;
        }

        [HttpGet("GetAllSemesters")]
        [AllowAnonymous]
        public async Task<IActionResult> GetAllSemesters()
        {
            var data = await semesterRepository.GetSemestersAsync();
            return Ok(data);
        }
        [HttpGet("GetAllActiveSemesters")]
        [AllowAnonymous]
        public async Task<IActionResult> GetAllActiveSemesters()
        {
            var data = await semesterRepository.GetActiveSemestersAsync();
            return Ok(data);
        }
        [HttpGet("GetSemesterById/{semesterId}")]
        [AllowAnonymous]
        public async Task<IActionResult> GetSemesterById(int semesterId)
        {
            var data = await semesterRepository.GetSemesterByIdAsync(semesterId);
            return Ok(data);
        }
        [HttpPut("ToggleSemesterStatus/{semesterId}")]
        [AllowAnonymous]
        public async Task<IActionResult> ToggleSemesterStatus(int semesterId)
        {
            var data = await semesterRepository.ToggleSemesterStatusAsync(semesterId);
            return Ok(data);
        }
        [HttpPost("CreateSemester")]
        [AllowAnonymous]
        public async Task<IActionResult> CreateSemester([FromBody] SemesterRequest semesterRequest)
        {
            var data = await semesterRepository.CreateSemesterAsync(semesterRequest);
            await _logHistoryRepository.LogAsync($"Create semester {semesterRequest.SemesterName}", JwtToken);

            return Ok(data);
        }
        [HttpPut("UpdateSemesterAsync")]
        [AllowAnonymous]
        public async Task<IActionResult> UpdateSemester(SemesterRequest semesterRequest)
        {
            var data = await semesterRepository.UpdateSemesterAsync(semesterRequest);
            await _logHistoryRepository.LogAsync($"Update semester {semesterRequest.SemesterName}", JwtToken);
            return Ok(data);
        }
        [HttpDelete("DeleteSemesterAsync/{semesterId}")]
        [AllowAnonymous]
        public async Task<IActionResult> DeleteSemester(int semesterId)
        {
            var data = await semesterRepository.DeleteSemesterAsync(semesterId);
            await _logHistoryRepository.LogAsync($"Delete semester ", JwtToken);
            return Ok(data);
        }
    }
}
