using Library.Common;
using Library.Models;
using Library.Response;

namespace WebApi.IRepository
{
    public interface IFacultyRepository
    {
        Task<ResultResponse<Faculty>> GetFaculties();
        Task<RequestResponse> UpdateFaculties(Faculty req);
        Task<ResultResponse<Faculty>> GetFacutiesByUserID(int? userId);
        Task<ResultResponse<FacutyResponse>> GetFacutyByRole(int roleId, int userId, int campusId);
    }
}
