using Library.Common;
using Library.Request;
using Library.Response;

namespace WebClient.IServices
{
    public interface IUserHistoryService
    {
        Task<ResultResponse<LogResponse>> GetLog(LogRequest request);
    }
}
