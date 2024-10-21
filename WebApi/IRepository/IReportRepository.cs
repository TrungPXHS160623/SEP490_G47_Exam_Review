using Library.Common;
using Library.Request;
using Library.Response;

namespace WebApi.IRepository
{
    public interface IReportRepository
    {
        Task<ResultResponse<ReportResponse>> GetReportsByLecturerId(int lecturerId);

        Task<RequestResponse> AddEditReport(LectureExamResponse reportRequest, bool isSubmit);

        Task<RequestResponse> EditReportById(int reportId, ReportRequest reportRequest);

        Task<ResultResponse<ReportDurationResponse>> GetReportDuration(int assignmentId);

    }
}
