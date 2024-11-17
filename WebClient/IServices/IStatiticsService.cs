using Library.Common;
using Library.Response;

namespace WebClient.IServices
{
    public interface IStatiticsService
    {
        Task<ResultResponse<CampusSubjectExamResponse>> GetExamByCampusAndSubject(int campusId);
        Task<ResultResponse<CampusReportResponse>> GetCampusReport();
        Task<ResultResponse<DepartmentReportResponse>> GetDepartmentReport(int campusId);
    }
}
