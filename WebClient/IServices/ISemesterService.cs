using Library.Common;
using Library.Models;

namespace WebClient.IServices
{
    public interface ISemesterService
    {
        Task<ResultResponse<Semester>> GetSemester();

        Task<RequestResponse> AddSemester(Semester req);

        Task<RequestResponse> UpdateSemester(Semester req);

        Task<RequestResponse> DeleteSemester(int campusId);

        Task<ResultResponse<Semester>> GetSemesterId(int campusId);
    }
}
