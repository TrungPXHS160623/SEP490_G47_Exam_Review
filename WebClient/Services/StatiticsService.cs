using Library.Common;
using Library.Request;
using Library.Response;
using MudBlazor;
using WebClient.IServices;

namespace WebClient.Services
{
    public class StatiticsService : IStatiticsService
    {
        private readonly HttpClient _httpClient;

        private readonly ISnackbar snackbar;

        public StatiticsService(HttpClient httpClient, ISnackbar SnackBar)
        {
            _httpClient = httpClient;
            snackbar = SnackBar;
        }
        public async Task<ResultResponse<CampusReportResponse>> GetCampusReport()
        {
            HttpResponseMessage response = await _httpClient.GetAsync($"api/Exam/GetCampusReport");

            var requestResponse = await response.Content.ReadFromJsonAsync<ResultResponse<CampusReportResponse>>();

            if (!requestResponse.IsSuccessful)
            {
                snackbar.Add(requestResponse.Message, Severity.Error);
            }

            return requestResponse;
        }

        public async Task<ResultResponse<DepartmentReportResponse>> GetDepartmentReport(UserRequest req)
        {
            HttpResponseMessage response = await _httpClient.PostAsJsonAsync($"api/Exam/GetDepartmentReport", req);

            var requestResponse = await response.Content.ReadFromJsonAsync<ResultResponse<DepartmentReportResponse>>();

            if (!requestResponse.IsSuccessful)
            {
                snackbar.Add(requestResponse.Message, Severity.Error);
            }

            return requestResponse;
        }

        public async Task<ResultResponse<CampusSubjectExamResponse>> GetExamByCampusAndSubject(UserRequest req)
        {
            HttpResponseMessage response = await _httpClient.PostAsJsonAsync($"api/Exam/GetExamByCampusAndSubject", req);

            var requestResponse = await response.Content.ReadFromJsonAsync<ResultResponse<CampusSubjectExamResponse>>();

            if (!requestResponse.IsSuccessful)
            {
                snackbar.Add(requestResponse.Message, Severity.Error);
            }

            return requestResponse;
        }
    }
}
