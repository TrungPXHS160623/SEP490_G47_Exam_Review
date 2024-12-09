using Library.Common;
using Library.Models;
using Library.Request;
using Library.Response;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using OfficeOpenXml.FormulaParsing.LexicalAnalysis;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using WebApi.IRepository;

namespace WebApi.Repository
{
    public class LogHistoryRepository : ILogHistoryRepository
    {
        private readonly QuizManagementContext DBcontext;
        private readonly IConfiguration Config;

        public LogHistoryRepository(QuizManagementContext DBcontext, IConfiguration config)
        {
            this.DBcontext = DBcontext;
            Config = config;
        }

        public async Task<ResultResponse<LogResponse>> GetLog(LogRequest req)
        {
            try
            {
                var data = await (from uh in DBcontext.UserHistories
                           join u in DBcontext.Users on uh.UserId equals u.UserId
                           where (u.Mail.ToLower().Contains(req.Mail) || string.IsNullOrEmpty(req.Mail))
                           && (uh.LogDt >= req.StartDate || req.StartDate == null)
                           && (uh.LogDt <= req.EndDate || req.EndDate == null)
                           select new LogResponse
                           {
                               Mail = u.Mail,
                               LogDt = uh.LogDt,
                               LogContent = uh.LogContent,
                           }).OrderByDescending(x => x.LogDt).ToListAsync();

                return new ResultResponse<LogResponse>
                {
                    IsSuccessful = true,
                    Items = data
                };
            }
            catch (Exception ex)
            {
                return new ResultResponse<LogResponse>
                {
                    IsSuccessful = false,
                    Message = ex.Message
                };
            }

        }

        public async Task LogAsync(string message,string token)
        {
            try
            {
                var claimsPrincipal = GetClaimsFromToken(token);

                if (claimsPrincipal == null)
                {
                    return;
                }

                var userId = int.Parse(claimsPrincipal.FindFirst(ClaimTypes.NameIdentifier)?.Value);

                var log = new UserHistory
                {
                    LogContent = message,
                    LogDt = DateTime.Now,
                    UserId = userId
                };

                await DBcontext.UserHistories.AddAsync(log);

                await DBcontext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                return;
            }
        }

        public ClaimsPrincipal? GetClaimsFromToken(string token)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.UTF8.GetBytes(Config["Jwt:Key"]!);

            try
            {
                var claimsPrincipal = tokenHandler.ValidateToken(token, new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = true,
                    ValidIssuer = Config["Jwt:Issuer"],
                    ValidateAudience = true,
                    ValidAudience = Config["Jwt:Audience"],
                    ValidateLifetime = true, 
                    ClockSkew = TimeSpan.Zero 
                }, out SecurityToken validatedToken);

                return claimsPrincipal; 
            }
            catch
            {
                return null;
            }
        }
    }
}
