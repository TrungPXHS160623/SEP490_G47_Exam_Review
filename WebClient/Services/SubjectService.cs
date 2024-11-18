using Library.Common;
using Library.Models;
using Library.Request;
using Library.Response;
using Microsoft.AspNetCore.Components.Forms;
using MudBlazor;
using WebClient.IServices;

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
                HttpResponseMessage response = await _httpClient.PostAsJsonAsync($"api/Subject/AddSubject", req);

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

        public async Task<ResultResponse<SubjectResponse>> GetSubjectByRole(int roleId, int userId, int campusId)
        {
            try
            {
                HttpResponseMessage response = await _httpClient.GetAsync($"api/Subject/GetSubjectByRole/{roleId}/{userId}/{campusId}");

                var requestResponse = await response.Content.ReadFromJsonAsync<ResultResponse<SubjectResponse>>();

                if (!requestResponse.IsSuccessful)
                {
                    snackbar.Add(requestResponse.Message, Severity.Error);
                }

                return requestResponse;
            }
            catch (Exception ex)
            {
                snackbar.Add(ex.Message, Severity.Error);
                return new ResultResponse<SubjectResponse>
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

        public async Task<ResultResponse<SubjectResponse>> GetSubjectsList(SubjectRequest req)
        {
            try
            {
                HttpResponseMessage response = await _httpClient.PostAsJsonAsync($"api/Subject/GetList",req);

                var requestResponse = await response.Content.ReadFromJsonAsync<ResultResponse<SubjectResponse>>();

                if (!requestResponse.IsSuccessful)
                {
                    snackbar.Add(requestResponse.Message, Severity.Error);
                }

                return requestResponse;
            }
            catch (Exception ex)
            {
                snackbar.Add(ex.Message, Severity.Error);
                return new ResultResponse<SubjectResponse>
                {
                    IsSuccessful = false,
                };
            }
        }

        public async Task<ResultResponse<SubjectResponse>> GetLectureSubject(int userId)
        {
            try
            {
                HttpResponseMessage response = await _httpClient.GetAsync($"api/Subject/GetLectureSubject/{userId}");

                var requestResponse = await response.Content.ReadFromJsonAsync<ResultResponse<SubjectResponse>>();

                if (!requestResponse.IsSuccessful)
                {
                    snackbar.Add(requestResponse.Message, Severity.Error);
                }

                return requestResponse;
            }
            catch (Exception ex)
            {
                snackbar.Add(ex.Message, Severity.Error);
                return new ResultResponse<SubjectResponse>
                {
                    IsSuccessful = false,
                };
            }
        }

        public async Task<ResultResponse<HeadSubjectRepsonse>> GetHeadSubject(int userId)
        {
            try
            {
                HttpResponseMessage response = await _httpClient.GetAsync($"api/Subject/GetHeadSubject/{userId}");

                var requestResponse = await response.Content.ReadFromJsonAsync<ResultResponse<HeadSubjectRepsonse>>();

                if (!requestResponse.IsSuccessful)
                {
                    snackbar.Add(requestResponse.Message, Severity.Error);
                }

                return requestResponse;
            }
            catch (Exception ex)
            {
                snackbar.Add(ex.Message, Severity.Error);
                return new ResultResponse<HeadSubjectRepsonse>
                {
                    IsSuccessful = false,
                };
            }
        }

        public async Task<RequestResponse> ImportSubjectFromExcel(IBrowserFile files)
        {
            if (string.IsNullOrWhiteSpace(Constants.JWTToken))
            {
                snackbar.Add("Authorization token is missing.", Severity.Error);
                return new RequestResponse { IsSuccessful = false, Message = "Missing Authorization Token" };
            }

            _httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", Constants.JWTToken);

            try
            {
                // Create multipart form data content
                var content = new MultipartFormDataContent();

                // Add the file to the multipart form data content
                var fileStream = files.OpenReadStream(maxAllowedSize: 10 * 1024 * 1024); // Adjust max size as needed
                var streamContent = new StreamContent(fileStream);
                content.Add(streamContent, "file", files.Name); // Name 'file' matches the parameter in your API

                // Send the POST request
                HttpResponseMessage response = await _httpClient.PostAsync("api/Subject/ImportSubjectsFromExcel", content);

                response.EnsureSuccessStatusCode();  // Throw if the response is not a success

                var requestResponse = await response.Content.ReadFromJsonAsync<RequestResponse>();

                if (!requestResponse.IsSuccessful)
                {
                    snackbar.Add(requestResponse.Message, Severity.Error);
                }
                else
                {
                    snackbar.Add("Exams imported successfully!", Severity.Success);
                }

                return requestResponse;
            }
            catch (Exception ex)
            {
                snackbar.Add($"Error during file upload: {ex.Message}", Severity.Error);
                return new RequestResponse { IsSuccessful = false, Message = ex.Message };
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

        public async Task<RequestResponse> LecturerSubjectModify(int userId, HashSet<SubjectResponse> req)
        {
            try
            {
                HttpResponseMessage response = await _httpClient.PutAsJsonAsync($"api/Subject/LecturerSubjectModify/{userId}", req);

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
