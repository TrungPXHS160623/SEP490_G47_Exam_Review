using Library.Common;
using Library.Models;

namespace WebClient.IServices
{
    public interface IStatusService
    {
        Task<ResultResponse<ExamStatus>> GetStatus();
    }
}
