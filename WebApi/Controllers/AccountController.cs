using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApi.IRepository;
using Library.Request;

namespace WebApi.Controllers
{
    public class AccountController : ApiBaseController
    {
        private readonly IAccountRepository _accountRepository;

        public AccountController(IAccountRepository accountRepository)
        {
            _accountRepository = accountRepository;
        }

        [HttpPost("Login")]
        [AllowAnonymous]
        public async Task<IActionResult> UserLogin([FromBody] UserRequest req)
        {
            var data = await this._accountRepository.UserLogin(req);

            return Ok(data);
        }

        [HttpPost("Register")]
        [AllowAnonymous]
        public async Task<IActionResult> UserRegister([FromBody] UserRegisterRequest req)
        {
            var data = await this._accountRepository.UserRegister(req);

            return Ok(data);
        }

        [HttpGet("GetUser")]
        [AllowAnonymous]
        public async Task<IActionResult> GetUserList()
        {
            var data = await this._accountRepository.GetUserList();

            return Ok(data);
        }
    }
}
