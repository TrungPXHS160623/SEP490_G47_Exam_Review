using Library.Common;
using Library.Models;

namespace WebApi.IRepository
{
    public interface IRoleRepository
    {
        Task<ResultResponse<UserRole>> GetRolesForAdmin();

        Task<ResultResponse<UserRole>> GetRolesForExaminer();
    }
}
