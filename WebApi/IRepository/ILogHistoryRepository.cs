using Library.Common;
using Library.Request;
using Library.Response;

namespace WebApi.IRepository
{
    public interface ILogHistoryRepository
    {
        public Task LogAsync(string message,int userId);

        public Task<ResultResponse<LogResponse>> GetLog(LogRequest req);
    }
}
