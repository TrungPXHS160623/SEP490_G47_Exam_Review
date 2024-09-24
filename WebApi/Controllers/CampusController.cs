using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApi.IRepository;

namespace WebApi.Controllers
{
    public class CampusController : ApiBaseController
    {
        private readonly ICampusRepository _campusRepository;

        public CampusController(ICampusRepository campusRepository)
        {
            _campusRepository = campusRepository;
        }

        [HttpGet("GetCampus")]
        public async Task<IActionResult> GetCampus()
        {
            var data = await this._campusRepository.GetCampus();

            return Ok(data);
        }
    }
}
