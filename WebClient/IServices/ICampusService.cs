using Library.Common;
using Library.Models;

namespace WebClient.IServices
{
    public interface ICampusService
    {
        Task<ResultResponse<Campus>> GetCampus();

        Task<RequestResponse> AddCampus(Campus req);

        Task<RequestResponse> UpdateCampus(Campus req);

        Task<RequestResponse> DeleteCampus(int campusId);

        Task<ResultResponse<Campus>> GetCampusById(int campusId);
    }
}
