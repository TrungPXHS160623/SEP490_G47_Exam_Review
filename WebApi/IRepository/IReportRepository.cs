using Library.Common;
using Library.Request;
using Library.Response;

namespace WebApi.IRepository
{
    public interface IReportRepository
    {
        Task<ResultResponse<ReportResponse>> GetAllReport();

        Task<RequestResponse> AddReport(ReportRequest reportRequest);

		Task<RequestResponse> EditReportById(int reportId, ReportRequest reportRequest);

	}
}
