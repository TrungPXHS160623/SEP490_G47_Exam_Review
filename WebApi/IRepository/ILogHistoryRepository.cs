using Library.Common;
using Library.Request;
using Library.Response;

namespace WebApi.IRepository
{
    public interface ILogHistoryRepository
    {
        public Task LogAsync(string message, string token);

        public Task<ResultResponse<LogResponse>> GetLog(LogRequest req);
    }
}
