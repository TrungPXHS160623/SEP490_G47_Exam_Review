using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApi.IRepository;
using Library.Request;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Google;
using Microsoft.AspNetCore.Authentication.Cookies;
using System.Security.Claims;
using Newtonsoft.Json;
using Library.Response;

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
            var tokenResponse = await GetGoogleTokenAsync(code);

            if (!string.IsNullOrEmpty(tokenResponse.AccessToken))
            {
                var userInfo = await GetGoogleUserInfoAsync(tokenResponse.AccessToken);

                if (userInfo != null)
                {
                    var response = await _accountRepository.GoogleLoginCallback(userInfo.Email);
                    return Ok(response);
                }
            }

            return BadRequest("Login with Google failed.");
        }

        private async Task<TokenResponse> GetGoogleTokenAsync(string code)
        {
            using var client = new HttpClient();
            var request = new HttpRequestMessage(HttpMethod.Post, "https://accounts.google.com/o/oauth2/token");
            var content = new FormUrlEncodedContent(new Dictionary<string, string>
            {
                { "code", code },
                { "client_id", _configuration["GoogleKeys:ClientId"] },
                { "client_secret", _configuration["GoogleKeys:ClientSecret"] },
                { "redirect_uri", "https://localhost:7255/api/account/googlelogincallback" },
                { "grant_type", "authorization_code" }
            });

            request.Content = content;
            var response = await client.SendAsync(request);
            var responseContent = await response.Content.ReadAsStringAsync();

            return JsonConvert.DeserializeObject<TokenResponse>(responseContent);
        }

        private async Task<GoogleUserInfo> GetGoogleUserInfoAsync(string accessToken)
        {
            using var client = new HttpClient();
            var response = await client.GetAsync($"https://www.googleapis.com/oauth2/v2/userinfo?access_token={accessToken}");
            var json = await response.Content.ReadAsStringAsync();

            return JsonConvert.DeserializeObject<GoogleUserInfo>(json);
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
    }
}
