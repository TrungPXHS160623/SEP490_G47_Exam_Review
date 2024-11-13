using Library.Common;
using Library.Models;

namespace WebApi.IRepository
{
    public interface IFacultyRepository
    {
        Task<ResultResponse<Faculty>> GetFaculties();

        Task<ResultResponse<Faculty>> GetHeadFaculties(int userId);
    }
}
