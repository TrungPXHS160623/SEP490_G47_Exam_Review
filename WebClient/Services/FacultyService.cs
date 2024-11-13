using Library.Common;
using Library.Models;
using Library.Response;
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

        public async Task<RequestResponse> AddFacuty(Faculty req)
        {
            HttpResponseMessage response = await _httpClient.PostAsJsonAsync($"api/Facuty/CreateFacuty", req);

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

        public async Task<ResultResponse<Faculty>> GetHeadFaculties(int userId)
        {
            try
            {
                HttpResponseMessage response = await _httpClient.GetAsync($"api/Faculty/GetHeadFaculties/{userId}");
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

        public async Task<ResultResponse<Faculty>> GetFacutyByID(int facutyID)
        {
            HttpResponseMessage response = await _httpClient.GetAsync($"api/Facuty/GetFacutyByID/{facutyID}");

            var requestResponse = await response.Content.ReadFromJsonAsync<ResultResponse<Faculty>>();

            if (!requestResponse.IsSuccessful)
            {
                snackbar.Add(requestResponse.Message, Severity.Error);
            }

            return requestResponse;
        }

        public async Task<ResultResponse<FacutyResponse>> GetFacutyByRole(int roleId, int userId, int campusId)
        {
            try
            {
                HttpResponseMessage response = await _httpClient.GetAsync($"api/Faculty/GetFacutyByRole/{roleId}/{userId}/{campusId}");

                var requestResponse = await response.Content.ReadFromJsonAsync<ResultResponse<FacutyResponse>>();

                if (!requestResponse.IsSuccessful)
                {
                    snackbar.Add(requestResponse.Message, Severity.Error);
                }

                return requestResponse;
            }
            catch (Exception ex)
            {
                snackbar.Add(ex.Message, Severity.Error);
                return new ResultResponse<FacutyResponse>
                {
                    IsSuccessful = false,
                };
            }
        }

        public async Task<RequestResponse> UpdateFacuty(Faculty req)
        {
            HttpResponseMessage response = await _httpClient.PutAsJsonAsync($"api/Facuty/UpdateFacuty", req);

            var requestResponse = await response.Content.ReadFromJsonAsync<RequestResponse>();

            if (!requestResponse.IsSuccessful)
            {
                snackbar.Add(requestResponse.Message, Severity.Error);
            }
            else
            {
                snackbar.Add(requestResponse.Message, Severity.Success);
            }

            return requestResponse; throw new NotImplementedException();
        }
    }
}
