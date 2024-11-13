using Library.Common;
using Library.Models;
using Library.Request;
using Library.Response;

namespace WebApi.IRepository
{
    public interface IFacultyRepository
    {
        Task<ResultResponse<Faculty>> GetFaculties();

        Task<ResultResponse<Faculty>> GetHeadFaculties(int userId);
        Task<RequestResponse> UpdateFaculties(Faculty req);
        Task<ResultResponse<Faculty>> GetFacutiesByUserID(int? userId);
        Task<ResultResponse<FacutyResponse>> GetFacutyByRole(int roleId, int userId, int campusId);
        Task<ResultResponse<FacutyResponse>> GetFacutyByIdAsync(int FacutyID);
        Task<RequestResponse> CreateFacutyAsync(FacutyRequest request);
        Task<RequestResponse> UpdateFacutyAsync(int facutyID, FacutyRequest request);
    }
}
