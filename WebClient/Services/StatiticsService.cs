using Library.Common;
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

        public async Task<ResultResponse<DepartmentReportResponse>> GetDepartmentReport(int campusId)
        {
            HttpResponseMessage response = await _httpClient.GetAsync($"api/Exam/GetDepartmentReport/{campusId}");

            var requestResponse = await response.Content.ReadFromJsonAsync<ResultResponse<DepartmentReportResponse>>();

            if (!requestResponse.IsSuccessful)
            {
                snackbar.Add(requestResponse.Message, Severity.Error);
            }

            return requestResponse;
        }

        public async Task<ResultResponse<CampusSubjectExamResponse>> GetExamByCampusAndSubject(int campusId)
        {
            HttpResponseMessage response = await _httpClient.GetAsync($"api/Exam/GetExamByCampusAndSubject/{campusId}");

            var requestResponse = await response.Content.ReadFromJsonAsync<ResultResponse<CampusSubjectExamResponse>>();

            if (!requestResponse.IsSuccessful)
            {
                snackbar.Add(requestResponse.Message, Severity.Error);
            }

            return requestResponse;
        }
    }
}
