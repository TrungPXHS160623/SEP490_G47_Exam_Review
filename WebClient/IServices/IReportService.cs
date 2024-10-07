using Library.Common;
using Library.Response;

namespace WebClient.IServices
{
    public interface IReportService
    {
        Task<RequestResponse> AddEditReport(LectureExamResponse reportRequest);
    }
}
