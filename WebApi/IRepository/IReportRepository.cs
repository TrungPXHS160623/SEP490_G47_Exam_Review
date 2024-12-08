using Library.Common;
using Library.Response;

namespace WebApi.IRepository
{
    public interface IReportRepository
    {
        Task<ResultResponse<ReportResponse>> GetReportsByLecturerId(int lecturerId);

        Task<RequestResponse> AddEditReport(LectureExamResponse reportRequest, bool isSubmit);

        Task<RequestResponse> UploadReportWithFiles(LectureExamResponseFinal reportRequest, bool isSubmit);

        Task<RequestResponse> UploadFiles(int reportId, IList<IFormFile> files);

    }
}
