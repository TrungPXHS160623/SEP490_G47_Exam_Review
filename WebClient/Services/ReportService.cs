using Library.Common;
using Library.Request;
using Library.Response;
using MudBlazor;
using WebClient.IServices;

namespace WebClient.Services
{
    public class ReportService : IReportService
    {
        private readonly HttpClient _httpClient;

        private readonly ISnackbar snackbar;

        public ReportService(HttpClient httpClient, ISnackbar SnackBar)
        {
            _httpClient = httpClient;
            snackbar = SnackBar;
        }

        public async Task<RequestResponse> AddEditReport(LectureExamResponse reportRequest)
        {
            try
            {
                HttpResponseMessage response = await _httpClient.PostAsJsonAsync($"api/Report/SaveReport", reportRequest);

                var requestResponse = await response.Content.ReadFromJsonAsync<RequestResponse>();

                if (!requestResponse.IsSuccessful)
                {
                    snackbar.Add(requestResponse.Message, Severity.Error);
                }
                else
                {
                    snackbar.Add(requestResponse.Message, Severity.Success);
                }

                return requestResponse;
            }
            catch (Exception ex)
            {
                snackbar.Add(ex.Message, Severity.Error);

                return new RequestResponse
                {
                    IsSuccessful = false,
                };
            }
        }
    }
}
