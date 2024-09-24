using Library.Common;
using Library.Models;

namespace WebApi.IRepository
{
    public interface IMenuRepository
    {
        Task<ResultResponse<Menu>> GetMenu(int userId);
    }
}
