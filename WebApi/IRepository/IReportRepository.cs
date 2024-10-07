using Library.Common;
using Library.Request;
using Library.Response;

namespace WebApi.IRepository
{
    public interface IReportRepository
    {
        Task<ResultResponse<ReportResponse>> GetReportsByLecturerId(int lecturerId);

        Task<RequestResponse> AddEditReport(LectureExamResponse reportRequest);

        Task<RequestResponse> EditReportById(int reportId, ReportRequest reportRequest);

	}
}
