using Library.Common;
using Library.Response;
using Microsoft.AspNetCore.Components.Forms;

namespace WebClient.IServices
{
    public interface IReportService
    {
        Task<RequestResponse> AddEditReport(LectureExamResponse reportRequest, bool isSubmit);
        Task<RequestResponse> ImportFile(IBrowserFile files, bool isSubmit);
    }
}
