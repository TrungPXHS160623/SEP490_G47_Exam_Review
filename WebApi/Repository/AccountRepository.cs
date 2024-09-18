using Azure.Core;
using Library.Response;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using WebApi.IRepository;
using Library.Common;
using Library.Models;
using Library.Request;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

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

                var data = await this.DBcontext.Accounts.FirstOrDefaultAsync(x => x.Email!.Equals(request.Email) && x.Password!.Equals(request.Password));

                if (data != null)
                {
                    response.IsSuccessful = true;
                    response.Token = GenerateToken(data);
                }
                else
                {
                    response.IsSuccessful = false;
                    response.Message = "Your Email or Password is Incorrect! Please try again.";
                }

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

        public string GenerateToken(Account acc)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config["Jwt:Key"]!));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            var userClaims = new[]
            {
                new Claim(ClaimTypes.Email, acc.Email!),
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

        public async Task<AuthenticationResponse> GoogleLoginCallback(string email)
        {
            try
            {
                AuthenticationResponse response = new AuthenticationResponse();
                var user = await FindOrCreateUserAsync(email);

                if (user != null)
                {
                    response.IsSuccessful = true;
                    response.Token = GenerateToken(user); 
                }

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

        private async Task<Account?> FindOrCreateUserAsync(string email)
        {
            var user = await DBcontext.Accounts.FirstOrDefaultAsync(x => x.Email.Equals(email));
            if (user == null)
            {
                user = new Account
                {
                    Email = email,
                    RoleId = 1  
                };
                await DBcontext.Accounts.AddAsync(user);
                await DBcontext.SaveChangesAsync();
            }
            return user;
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
    }
}
