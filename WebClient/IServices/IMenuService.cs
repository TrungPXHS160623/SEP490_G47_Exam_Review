using Library.Common;
using Library.Models;

namespace WebClient.IServices
{
    public interface IMenuService
    {
        Task<ResultResponse<Menu>> GetMenuByUser(int userId);
    }
}
