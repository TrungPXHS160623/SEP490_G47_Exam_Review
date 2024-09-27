using Library.Common;
using Library.Models;
using MudBlazor;
using WebClient.IServices;

namespace WebClient.Services
{
    public class StatusService : IStatusService
    {
        private readonly HttpClient _httpClient;

        private readonly ISnackbar snackbar;

        public StatusService(HttpClient httpClient, ISnackbar SnackBar)
        {
            _httpClient = httpClient;
            snackbar = SnackBar;
        }

        public async Task<ResultResponse<ExamStatus>> GetStatus()
        {
            //Check JWT key
            if (Constants.JWTToken == "")
            {
                return null;
            }
            _httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", Constants.JWTToken);

            HttpResponseMessage response = await _httpClient.GetAsync($"api/Status/GetAll");

            var requestResponse = await response.Content.ReadFromJsonAsync<ResultResponse<ExamStatus>>();

            if (!requestResponse.IsSuccessful)
            {
                snackbar.Add(requestResponse.Message, Severity.Error);
            }

            return requestResponse;
        }

    }
}
