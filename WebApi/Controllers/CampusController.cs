using Library.Models;
using Library.Models.Dtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApi.IRepository;

namespace WebApi.Controllers
{
    public class CampusController : ApiBaseController
    {
        private readonly ICampusRepository _campusRepository;
        private readonly ILogHistoryRepository _logHistoryRepository;

        public CampusController(ICampusRepository campusRepository, ILogHistoryRepository logHistoryRepository)
        {
            _campusRepository = campusRepository;
            _logHistoryRepository = logHistoryRepository;
        }

        [HttpGet("GetCampus")]
        public async Task<IActionResult> GetCampus()
        {
            var data = await this._campusRepository.GetCampus();

            return Ok(data);
        }

        [HttpPost("AddCampus")]
        public async Task<IActionResult> AddCampus([FromBody] Campus req)
        {
            var data = await this._campusRepository.AddCampus(req);
            await _logHistoryRepository.LogAsync($"Add campus {req.CampusName}", JwtToken);

            return Ok(data);
        }

        [HttpPut("UpdateCampus")]
        public async Task<IActionResult> UpdateCampus([FromBody] Campus req)
        {
            var data = await this._campusRepository.UpdateCampus(req);
            await _logHistoryRepository.LogAsync($"Update campus {req.CampusName}", JwtToken);

            return Ok(data);
        }

        [HttpGet("GetCampusById/{CampusId}")]
        public async Task<IActionResult> GetCampusById(int CampusId)
        {
            var data = await this._campusRepository.GetCampusById(CampusId);

            return Ok(data);
        }

        [HttpDelete("DeleteCampus/{CampusId}")]
        public async Task<IActionResult> DelteCampus(int CampusId)
        {
            var data = await this._campusRepository.DeleteCampus(CampusId);
            await _logHistoryRepository.LogAsync($"Remove a campus", JwtToken);

            return Ok(data);
        }
    }
}
