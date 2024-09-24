using Library.Common;
using Library.Models;

namespace WebClient.IServices
{
    public interface IRoleService
    {
        Task<ResultResponse<UserRole>> GetRoles();
    }
}
