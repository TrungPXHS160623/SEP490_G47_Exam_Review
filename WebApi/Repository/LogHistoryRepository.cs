using Library.Common;
using Library.Models;
using Library.Request;
using Library.Response;
using Microsoft.EntityFrameworkCore;
using WebApi.IRepository;

namespace WebApi.Repository
{
    public class LogHistoryRepository : ILogHistoryRepository
    {
        private readonly QuizManagementContext DBcontext;

        public LogHistoryRepository(QuizManagementContext DBcontext)
        {
            this.DBcontext = DBcontext;
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
                           }).ToListAsync();

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

        public async Task LogAsync(string message,int userId)
        {
            try
            {
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

            }
        }
    }
}
