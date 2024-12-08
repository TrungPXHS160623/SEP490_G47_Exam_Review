using Library.Request;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApi.IRepository;

namespace WebApi.Controllers
{
    public class FacultyController : ApiBaseController
    {
        private readonly IFacultyRepository _facultyRepository;
        private readonly ILogHistoryRepository _logHistoryRepository;

        public FacultyController(IFacultyRepository facultyRepository, ILogHistoryRepository logHistoryRepository)
        {
            _facultyRepository = facultyRepository;
            _logHistoryRepository = logHistoryRepository;
        }

        [HttpGet("GetFaculties")]
        public async Task<IActionResult> GetFaculty()
        {
            var data = await this._facultyRepository.GetFaculties();

            return Ok(data);
        }
        [HttpDelete("DeleteFaculties/{facultyID}")]
        public async Task<IActionResult> DeleteFaculties(int facultyID)
        {
            var data = await this._facultyRepository.DeleteFaculties(facultyID);
            await _logHistoryRepository.LogAsync($"Delete a department", JwtToken);

            return Ok(data);
        }
        [HttpGet("GetHeadFaculties/{userId}")]
        public async Task<IActionResult> GetHeadFaculty(int userId)
        {
            var data = await this._facultyRepository.GetHeadFaculties(userId);

            return Ok(data);
        }
        [HttpGet("GetFacutiByUserId/{UserID}")]
        public async Task<IActionResult> GetFacutiByUserId(int UserID)
        {
            var data = await this._facultyRepository.GetFacutiesByUserID(UserID);

            return Ok(data);
        }
        [HttpGet("GetFacutyByRole/{roleId}/{userId}/{campusId}")]
        [AllowAnonymous]
        public async Task<IActionResult> GetFacutyByRole(int roleId, int userId, int campusId)
        {
            var data = await this._facultyRepository.GetFacutyByRole(roleId, userId, campusId);

            return Ok(data);
        }
        [HttpGet("GetFacutyById/{facutyID}")]
        [AllowAnonymous]
        public async Task<IActionResult> GetFacutyById(int facutyID)
        {
            var data = await _facultyRepository.GetFacutyByIdAsync(facutyID);
            return Ok(data);
        }
        [HttpPut("CreateFacuty")]
        [AllowAnonymous]
        public async Task<IActionResult> CreateFacuty([FromBody] FacutyRequest semesterRequest)
        {
            var data = await _facultyRepository.CreateFacutyAsync(semesterRequest);
            await _logHistoryRepository.LogAsync($"Create department {semesterRequest.FacultyName}", JwtToken);

            return Ok(data);
        }
        [HttpPut("UpdateFacuty")]
        [AllowAnonymous]
        public async Task<IActionResult> UpdateFacuty([FromBody] FacutyRequest semesterRequest)
        {
            var data = await _facultyRepository.UpdateFacutyAsync(semesterRequest);
            await _logHistoryRepository.LogAsync($"Update department {semesterRequest.FacultyName}", JwtToken);

            return Ok(data);
        }
    }
}
