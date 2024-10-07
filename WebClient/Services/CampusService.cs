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

        public async Task<RequestResponse> AddCampus(Campus req)
        {
            HttpResponseMessage response = await _httpClient.PostAsJsonAsync($"api/Campus/AddCampus",req);

            var requestResponse = await response.Content.ReadFromJsonAsync<RequestResponse>();

            if (!requestResponse.IsSuccessful)
            {
                snackbar.Add(requestResponse.Message, Severity.Error);
            } else
            {
                snackbar.Add(requestResponse.Message, Severity.Success);
            }

            return requestResponse;
        }

        public async Task<RequestResponse> UpdateCampus(Campus req)
        {
            HttpResponseMessage response = await _httpClient.PutAsJsonAsync($"api/Campus/UpdateCampus",req);

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
        public async Task<RequestResponse> DeleteCampus(int campusId)
        {
            HttpResponseMessage response = await _httpClient.DeleteAsync($"api/Campus/DeleteCampus/{campusId}");

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

        public async Task<ResultResponse<Campus>> GetCampusById(int campusId)
        {
            HttpResponseMessage response = await _httpClient.GetAsync($"api/Campus/GetCampusById/{campusId}");

            var requestResponse = await response.Content.ReadFromJsonAsync<ResultResponse<Campus>>();

            if (!requestResponse.IsSuccessful)
            {
                snackbar.Add(requestResponse.Message, Severity.Error);
            }

            return requestResponse;
        }
    }
}
