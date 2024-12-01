using Library.Common;
using Library.Models;
using Library.Request;
using Library.Response;

namespace WebClient.IServices
{
    public interface IFacultyService
    {
        Task<ResultResponse<Faculty>> GetFaculties();
        Task<ResultResponse<Faculty>> GetHeadFaculties(int userId);
        Task<ResultResponse<FacutyRequest>> GetFacutiesByUserID(int? userId);
        Task<ResultResponse<FacutyResponse>> GetFacutyByRole(int roleId, int userId, int campusId);
        Task<ResultResponse<FacutyRequest>> GetFacutyByID(int campusId);
        Task<RequestResponse> AddFacuty(FacutyRequest req);
        Task<RequestResponse> UpdateFacuty(FacutyRequest req);
    }
}
