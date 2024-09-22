using Library.Common;
using Library.Models;
using Library.Request;
using Library.Response;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using WebApi.IRepository;

namespace WebApi.Repository
{
    public class AccountRepository : IAccountRepository
    {
        private readonly QuizManagementContext DBcontext;
        private readonly IConfiguration config;

        public AccountRepository(QuizManagementContext DBcontext, IConfiguration Config)
        {
            this.DBcontext = DBcontext;
            this.config = Config;
        }

        public async Task<AuthenticationResponse> UserLogin(UserRequest request)
        {
            try
            {
                AuthenticationResponse response = new AuthenticationResponse();

                //var data = await this.DBcontext.Accounts.FirstOrDefaultAsync(x => x.Email!.Equals(request.Email) && x.Password!.Equals(request.Password));

                //if (data != null)
                //{
                //    response.IsSuccessful = true;
                //    response.Token = GenerateToken(data);
                //}
                //else
                //{
                //    response.IsSuccessful = false;
                //    response.Message = "Your Email or Password is Incorrect! Please try again.";
                //}

                return response;
            }
            catch (Exception ex)
            {
                return new AuthenticationResponse
                {
                    IsSuccessful = false,
                    Message = ex.Message
                };
            }
        }

        public async Task<RequestResponse> UserRegister(UserRegisterRequest request)
        {
            try
            {
                RequestResponse response = new RequestResponse();

                var data = await this.DBcontext.Accounts.FirstOrDefaultAsync(x => x.Email.Equals(request.Email));

                if (data != null)
                {
                    response.IsSuccessful = false;
                    response.Message = "Email already exist!";
                }
                else
                {
                    var acc = new Account
                    {
                        Email = request.Email,
                        Password = request.Password,
                        RoleId = 2,
                    };

                    await this.DBcontext.Accounts.AddAsync(acc);

                    await this.DBcontext.SaveChangesAsync();

                    response.IsSuccessful = true;

                    response.Message = "Register successfuly";
                }

                return response;
            }
            catch (Exception ex)
            {
                return new RequestResponse
                {
                    IsSuccessful = false,
                    Message = ex.Message
                };
            }
        }

        public string GenerateToken(User acc)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config["Jwt:Key"]!));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            var userClaims = new[]
            {
                new Claim(ClaimTypes.Email, acc.Mail!),
                new Claim(ClaimTypes.Role,acc.RoleId.ToString()!),
            };

            var token = new JwtSecurityToken(
                issuer: config["Jwt:Issuer"],
                audience: config["Jwt:Audience"],
                claims: userClaims,
                expires: DateTime.Now.AddDays(2),
                signingCredentials: credentials
                );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        public async Task<AuthenticationResponse> GoogleLoginCallback(string code)
        {
            try
            {
                AuthenticationResponse response = new AuthenticationResponse();

                var tokenResponse = await GetGoogleTokenAsync(code);

                if (!string.IsNullOrEmpty(tokenResponse.AccessToken))
                {
                    var userInfo = await GetGoogleUserInfoAsync(tokenResponse.AccessToken);

                    if (userInfo != null)
                    {
                        var user = await FindOrCreateUserAsync(userInfo.Email);

                        if (user != null)
                        {
                            var token = GenerateToken(user);
                            Constants.JWTToken = token;
                            return new AuthenticationResponse
                            {
                                IsSuccessful = true,
                                Token = token
                            };
                        }
                    }
                }
                return new AuthenticationResponse
                {
                    IsSuccessful = false,
                    Message = "Login with Google failed."
                };
            }
            catch (Exception ex)
            {
                return new AuthenticationResponse
                {
                    IsSuccessful = false,
                    Message = ex.Message
                };
            }
        }

        private async Task<User?> FindOrCreateUserAsync(string email)
        {
            try
            {
                var user = await DBcontext.Users.FirstOrDefaultAsync(x => x.Mail.Equals(email));
                if (user == null)
                {
                    user = new User
                    {
                        Mail = email,
                    };
                    await DBcontext.Users.AddAsync(user);
                    await DBcontext.SaveChangesAsync();
                }
                return user;
            }
            catch (Exception ex)
            {
                return null;
            }

        }

        public async Task<ResultResponse<Account>> GetUserList()
        {
            try
            {
                var data = await this.DBcontext.Accounts.ToListAsync();

                return new ResultResponse<Account>
                {
                    IsSuccessful = true,
                    Items = data,
                };
            }
            catch (Exception ex)
            {
                return new ResultResponse<Account>
                {
                    IsSuccessful = false,
                    Message = ex.Message
                };
            }
        }

        private async Task<TokenResponse> GetGoogleTokenAsync(string code)
        {
            using var client = new HttpClient();
            var request = new HttpRequestMessage(HttpMethod.Post, "https://accounts.google.com/o/oauth2/token");
            var content = new FormUrlEncodedContent(new Dictionary<string, string>
            {
                { "code", code },
                { "client_id", config["GoogleKeys:ClientId"] },
                { "client_secret", config["GoogleKeys:ClientSecret"] },
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
    }
}
