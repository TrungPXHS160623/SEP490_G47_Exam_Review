using Library.Common;
using Library.Request;
using Library.Response;

namespace WebClient.IServices
{
    public interface IStatiticsService
    {
        Task<ResultResponse<CampusSubjectExamResponse>> GetExamByCampusAndSubject(UserRequest req);
        Task<ResultResponse<CampusReportResponse>> GetCampusReport();
        Task<ResultResponse<DepartmentReportResponse>> GetDepartmentReport(UserRequest req);
    }
}
