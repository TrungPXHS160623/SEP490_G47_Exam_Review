using PRN231_Library.Common;
using PRN231_Library.Models;

namespace PRN231_API.IRepository
{
    public interface IMenuRepository 
    {
        Task<ResultResponse<Menu>> GetMenuByRole(int roleId);
    }
}
