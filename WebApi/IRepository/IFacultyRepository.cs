using Library.Common;
using Library.Models;
using Library.Request;
using Library.Response;

namespace WebApi.IRepository
{
    public interface IFacultyRepository
    {
        Task<ResultResponse<Faculty>> GetFaculties();
        Task<RequestResponse> DeleteFaculties(int facultyId);
        Task<ResultResponse<Faculty>> GetHeadFaculties(int userId);
        Task<ResultResponse<FacutyRequest>> GetFacutiesByUserID(int? userId);
        Task<ResultResponse<FacutyResponse>> GetFacutyByRole(int roleId, int userId, int campusId);
        Task<ResultResponse<FacutyRequest>> GetFacutyByIdAsync(int FacutyID);
        Task<RequestResponse> CreateFacutyAsync(FacutyRequest request);
        Task<RequestResponse> UpdateFacutyAsync(FacutyRequest request);
    }
}
