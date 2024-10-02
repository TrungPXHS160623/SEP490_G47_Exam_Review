using Library.Common;
using Library.Models;
using MudBlazor;
using WebClient.IServices;

namespace WebClient.Services
{
    public class CampusService : ICampusService
    {
        private readonly HttpClient _httpClient;

        private readonly ISnackbar snackbar;

        public CampusService(HttpClient httpClient, ISnackbar SnackBar)
        {
            _httpClient = httpClient;
            snackbar = SnackBar;
        }

        public async Task<ResultResponse<Campus>> GetCampus()
        {
            HttpResponseMessage response = await _httpClient.GetAsync($"api/Campus/GetCampus");

            var requestResponse = await response.Content.ReadFromJsonAsync<ResultResponse<Campus>>();

            if (!requestResponse.IsSuccessful)
            {
                snackbar.Add(requestResponse.Message, Severity.Error);
            }

            return requestResponse;
        }

    }
}
