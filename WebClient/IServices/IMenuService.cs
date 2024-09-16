using PRN231_Library.Common;
using PRN231_Library.Models;

namespace WebClient.IServices
{
    public interface IMenuService
    {
        Task<ResultResponse<Menu>> GetMenuByRole(int roleId);
    }
}
