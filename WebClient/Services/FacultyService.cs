using Library.Common;
using Library.Models;
using MudBlazor;
using WebClient.IServices;

namespace WebClient.Services
{
    public class FacultyService : IFacultyService
    {
        private readonly HttpClient _httpClient;

        private readonly ISnackbar snackbar;

        public FacultyService(HttpClient httpClient, ISnackbar SnackBar)
        {
            _httpClient = httpClient;
            snackbar = SnackBar;
        }

        public async Task<ResultResponse<Faculty>> GetFaculties()
        {
            HttpResponseMessage response = await _httpClient.GetAsync($"api/Faculty/GetFaculties");

            var requestResponse = await response.Content.ReadFromJsonAsync<ResultResponse<Faculty>>();

            if (!requestResponse.IsSuccessful)
            {
                snackbar.Add(requestResponse.Message, Severity.Error);
            }

            return requestResponse;
        }

        public async Task<ResultResponse<Faculty>> GetFacutiesByUserID(int? userId)
        {
            try
            {
                HttpResponseMessage response = await _httpClient.GetAsync($"api/Facuti/GetFacutiByUserId/{userId}");

                var requestResponse = await response.Content.ReadFromJsonAsync<ResultResponse<Faculty>>();

                if (!requestResponse.IsSuccessful)
                {
                    snackbar.Add(requestResponse.Message, Severity.Error);
                }

                return requestResponse;
            }
            catch (Exception ex)
            {
                snackbar.Add(ex.Message, Severity.Error);
                return new ResultResponse<Faculty>
                {
                    IsSuccessful = false,
                };
            }
        }

    }
}
