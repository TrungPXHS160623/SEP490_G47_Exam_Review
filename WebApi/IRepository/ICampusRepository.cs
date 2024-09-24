using Library.Common;
using Library.Models;

namespace WebApi.IRepository
{
    public interface ICampusRepository
    {
        Task<ResultResponse<Campus>> GetCampus();
    }
}
