using Library.Common;
using Library.Models;

namespace WebApi.IRepository
{
    public interface IStatusRepository
    {
        Task<ResultResponse<ExamStatus>> GetStatus();
    }
}
