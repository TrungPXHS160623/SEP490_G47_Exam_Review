using Microsoft.AspNetCore.Mvc;
using WebApi.IRepository;

namespace WebApi.Controllers
{
    public class StatusController : ApiBaseController
    {
        private readonly IStatusRepository _statusRepository;

        public StatusController(IStatusRepository statusRepository)
        {
            _statusRepository = statusRepository;
        }

        [HttpGet("GetAll")]
        public async Task<IActionResult> GetStatus()
        {
            var data = await this._statusRepository.GetStatus();

            return Ok(data);
        }
    }
}
