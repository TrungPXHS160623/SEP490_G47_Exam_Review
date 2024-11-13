using Library.Common;
using Library.Models;
using Library.Response;

namespace WebClient.IServices
{
    public interface IFacultyService
    {
        Task<ResultResponse<Faculty>> GetFaculties();
        Task<ResultResponse<Faculty>> GetFacutiesByUserID(int? userId);
        Task<ResultResponse<FacutyResponse>> GetFacutyByRole(int roleId, int userId, int campusId);
        Task<ResultResponse<Faculty>> GetFacutyByID(int campusId);
        Task<RequestResponse> AddFacuty(Faculty req);
        Task<RequestResponse> UpdateFacuty(Faculty req);

    }
}
