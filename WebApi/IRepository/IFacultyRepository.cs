using Library.Common;
using Library.Models;

namespace WebApi.IRepository
{
    public interface IFacultyRepository
    {
        Task<ResultResponse<Faculty>> GetFaculties();
        Task<RequestResponse> UpdateFaculties(Faculty req);
        Task<ResultResponse<Faculty>> GetFacutiesByUserID(int? userId);
    }
}
