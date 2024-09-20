using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApi.IRepository;
using Library.Request;
using Library.Common;

namespace WebApi.Controllers
{
    public class AccountController : ApiBaseController
    {
        private readonly IAccountRepository _accountRepository;
        private readonly IConfiguration _configuration;

        public AccountController(IAccountRepository accountRepository, IConfiguration configuration)
        {
            _accountRepository = accountRepository;
            _configuration = configuration;
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

        [HttpGet("GoogleLoginCallback")]
        [AllowAnonymous]
        public async Task<IActionResult> GoogleLoginCallback(string code)
        {
            var response = await _accountRepository.GoogleLoginCallback(code);
            if (response.IsSuccessful)
            {
                return Redirect($"https://localhost:7158/home");
            }

            return Redirect("https://localhost:7158/error");
        }

        [HttpGet("google-keys")]
        [AllowAnonymous]
        public IActionResult GetGoogleKeys()
        {
            var clientId = _configuration["GoogleKeys:ClientId"];

            return Ok(new
            {
                ClientId = clientId,
            });
        }

        [HttpGet("GetJWT")]
        [AllowAnonymous]
        public IActionResult GetJWT()
        {
            return Ok(new AuthenticationResponse
            {
                Token = Constants.JWTToken,
            });
        }
    }
}
