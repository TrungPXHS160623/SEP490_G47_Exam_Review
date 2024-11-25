using Library.Common;
using Library.Models;
using Library.Request;

namespace WebClient.IServices
{
    public interface ISemesterService
    {
        Task<ResultResponse<Semester>> GetSemester();

        Task<RequestResponse> AddSemester(SemesterRequest req);

        Task<RequestResponse> UpdateSemester(SemesterRequest req);

        Task<RequestResponse> DeleteSemester(int campusId);

        Task<ResultResponse<SemesterRequest>> GetSemesterId(int campusId);

    }
}
