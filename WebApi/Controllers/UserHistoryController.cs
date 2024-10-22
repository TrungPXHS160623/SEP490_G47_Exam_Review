using Library.Request;
using Microsoft.AspNetCore.Mvc;
using WebApi.IRepository;

namespace WebApi.Controllers
{
    public class UserHistoryController : ApiBaseController
    {
        private readonly ILogHistoryRepository _logHistoryRepository;

        public UserHistoryController(ILogHistoryRepository logHistoryRepository)
        {
            _logHistoryRepository = logHistoryRepository;
        }

        [HttpPost("GetLog")]
        public async Task<IActionResult> GetLog([FromBody] LogRequest req)
        {
            var data = await this._logHistoryRepository.GetLog(req);

            return Ok(data);
        }
    }
}
