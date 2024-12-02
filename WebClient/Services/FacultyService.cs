using Library.Common;
using Library.Models;
using Library.Request;
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

        public async Task<RequestResponse> AddFacuty(FacutyRequest req)
        {
            HttpResponseMessage response = await _httpClient.PutAsJsonAsync($"api/Faculty/CreateFacuty", req);

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
        public async Task<ResultResponse<FacutyRequest>> GetFacutiesByUserID(int? userId)
        {
            try
            {
                HttpResponseMessage response = await _httpClient.GetAsync($"api/Faculty/GetFacutiByUserId/{userId}");

                var requestResponse = await response.Content.ReadFromJsonAsync<ResultResponse<FacutyRequest>>();

                if (!requestResponse.IsSuccessful)
                {
                    snackbar.Add(requestResponse.Message, Severity.Error);
                }

                return requestResponse;
            }
            catch (Exception ex)
            {
                snackbar.Add(ex.Message, Severity.Error);
                return new ResultResponse<FacutyRequest>
                {
                    IsSuccessful = false,
                };
            }
        }

        public async Task<ResultResponse<FacutyRequest>> GetFacutyByID(int facutyID)
        {
            HttpResponseMessage response = await _httpClient.GetAsync($"api/Faculty/GetFacutyById/{facutyID}");

            var requestResponse = await response.Content.ReadFromJsonAsync<ResultResponse<FacutyRequest>>();

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

        public async Task<RequestResponse> UpdateFacuty(FacutyRequest req)
        {
            HttpResponseMessage response = await _httpClient.PutAsJsonAsync($"api/Faculty/UpdateFacuty", req);

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
