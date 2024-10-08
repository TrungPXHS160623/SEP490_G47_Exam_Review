using Library.Common;
using Library.Models;
using Library.Response;
using MudBlazor;
using WebClient.IServices;
using static MudBlazor.Colors;

namespace WebClient.Services
{
    public class SubjectService : ISubjectService
    {
        private readonly HttpClient _httpClient;

        private readonly ISnackbar snackbar;

        public SubjectService(HttpClient httpClient, ISnackbar SnackBar)
        {
            _httpClient = httpClient;
            snackbar = SnackBar;
        }

        public async Task<RequestResponse> AddSubject(Subject req)
        {
            try
            {
                HttpResponseMessage response = await _httpClient.PostAsJsonAsync($"api/Subject/AddSubject",req);

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
            catch (Exception ex)
            {
                snackbar.Add(ex.Message, Severity.Error);
                return new RequestResponse
                {
                    IsSuccessful = false,
                };
            }
        }

        public async Task<RequestResponse> DeleteSubject(int subjectId)
        {
            try
            {
                HttpResponseMessage response = await _httpClient.DeleteAsync($"api/Subject/DeleteSubject/{subjectId}");

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

        public async Task<ResultResponse<Subject>> GetSubjectById(int subjectId)
        {
            try
            {
                HttpResponseMessage response = await _httpClient.GetAsync($"api/Subject/GetSubjectById/{subjectId}");

                var requestResponse = await response.Content.ReadFromJsonAsync<ResultResponse<Subject>>();

                if (!requestResponse.IsSuccessful)
                {
                    snackbar.Add(requestResponse.Message, Severity.Error);
                }

                return requestResponse;
            }
            catch (Exception ex)
            {
                snackbar.Add(ex.Message, Severity.Error);
                return new ResultResponse<Subject>
                {
                    IsSuccessful = false,
                };
            }
        }

        public async Task<ResultResponse<Subject>> GetSubjects()
        {
            try
            {
                HttpResponseMessage response = await _httpClient.GetAsync($"api/Subject/GetAll");

                var requestResponse = await response.Content.ReadFromJsonAsync<ResultResponse<Subject>>();

                if (!requestResponse.IsSuccessful)
                {
                    snackbar.Add(requestResponse.Message, Severity.Error);
                }

                return requestResponse;
            }
            catch (Exception ex)
            {
                snackbar.Add(ex.Message, Severity.Error);
                return new ResultResponse<Subject>
                {
                    IsSuccessful = false,
                };
            }
        }

        public async Task<RequestResponse> UpdateSubject(Subject req)
        {
            try
            {
                HttpResponseMessage response = await _httpClient.PutAsJsonAsync($"api/Subject/UpdateSubject", req);

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
