using Library.Common;
using Library.Models;
using MudBlazor;
using WebClient.IServices;

namespace WebClient.Services
{
    public class SemesterService : ISemesterService
    {
        private readonly HttpClient _httpClient;

        private readonly ISnackbar snackbar;

        public SemesterService(HttpClient httpClient, ISnackbar SnackBar)
        {
            _httpClient = httpClient;
            snackbar = SnackBar;
        }


        public async Task<ResultResponse<Semester>> GetSemester()
        {
            HttpResponseMessage response = await _httpClient.GetAsync($"api/Semester/GetAllSemesters");

            var requestResponse = await response.Content.ReadFromJsonAsync<ResultResponse<Semester>>();

            if (!requestResponse.IsSuccessful)
            {
                snackbar.Add(requestResponse.Message, Severity.Error);
            }

            return requestResponse;
        }

        public async Task<RequestResponse> AddSemester(Semester req)
        {
            HttpResponseMessage response = await _httpClient.PostAsJsonAsync($"api/Semester/CreateSemester", req);

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

        public async Task<RequestResponse> UpdateSemester(Semester req)
        {
            HttpResponseMessage response = await _httpClient.PutAsJsonAsync($"api/Semester/UpdateSemesterAsync", req);

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

        public async Task<RequestResponse> DeleteSemester(int semesterId)
        {

            HttpResponseMessage response = await _httpClient.DeleteAsync($"api/Campus/DeleteSemesterAsync/{semesterId}");

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

        public async Task<ResultResponse<Semester>> GetSemesterId(int semesterId)
        {
            HttpResponseMessage response = await _httpClient.GetAsync($"api/Campus/GetSemesterById/{semesterId}");

            var requestResponse = await response.Content.ReadFromJsonAsync<ResultResponse<Semester>>();

            if (!requestResponse.IsSuccessful)
            {
                snackbar.Add(requestResponse.Message, Severity.Error);
            }

            return requestResponse;
        }
    }
}
