using Library.Common;
using Library.Request;
using Library.Response;
using Microsoft.AspNetCore.Components.Forms;
using MudBlazor;
using WebClient.IServices;

namespace WebClient.Services
{
    public class UserService : IUserService
    {
        private readonly HttpClient _httpClient;

        private readonly ISnackbar snackbar;

        public UserService(HttpClient httpClient, ISnackbar SnackBar)
        {
            _httpClient = httpClient;
            snackbar = SnackBar;
        }

        public async Task<RequestResponse> ClearJWT()
        {
            HttpResponseMessage response = await _httpClient.GetAsync($"api/User/ClearJWT");

            var requestResponse = await response.Content.ReadFromJsonAsync<RequestResponse>();

            if (!requestResponse.IsSuccessful)
            {
                snackbar.Add(requestResponse.Message, Severity.Error);
            }

            return requestResponse;
        }

        public async Task<RequestResponse> CreateAsync(UserRequest user)
        {
            try
            {
                HttpResponseMessage response = await _httpClient.PostAsJsonAsync($"api/User/create", user);

                // Reading the response content
                var requestResponse = await response.Content.ReadFromJsonAsync<RequestResponse>();

                if (requestResponse.IsSuccessful)
                {
                    snackbar.Add(requestResponse.Message, Severity.Success);
                }
                else
                {
                    snackbar.Add(requestResponse.Message, Severity.Error);
                }

                return requestResponse;
            }
            catch (Exception ex)
            {
                snackbar.Add(ex.Message, Severity.Error);
                return new RequestResponse { IsSuccessful = false, Message = "An error occurred." };
            }
        }

        public async Task<RequestResponse> DeleteAsync(int id)
        {
            try
            {
                HttpResponseMessage response = await _httpClient.DeleteAsync($"api/User/Delete/{id}");

                // Reading the response content
                var requestResponse = await response.Content.ReadFromJsonAsync<RequestResponse>();

                if (requestResponse.IsSuccessful)
                {
                    snackbar.Add(requestResponse.Message, Severity.Success);
                }
                else
                {
                    snackbar.Add(requestResponse.Message, Severity.Error);
                }

                return requestResponse;
            }
            catch (Exception ex)
            {
                snackbar.Add(ex.Message, Severity.Error);
                return new RequestResponse { IsSuccessful = false, Message = "An error occurred." };
            }
        }

        public async Task<ResultResponse<UserResponse>> GetLectureListBySubject(int subjectId, int campusId)
        {
            try
            {
                HttpResponseMessage response = await _httpClient.GetAsync($"api/User/GetLectureBySubject/{subjectId}/{campusId}");

                var requestResponse = await response.Content.ReadFromJsonAsync<ResultResponse<UserResponse>>();

                if (!requestResponse.IsSuccessful)
                {
                    snackbar.Add(requestResponse.Message, Severity.Error);
                }

                return requestResponse;
            }
            catch (Exception ex)
            {
                snackbar.Add(ex.Message, Severity.Error);
                return new ResultResponse<UserResponse>
                {
                    IsSuccessful = false,
                };
            }
        }


        public async Task<ResultResponse<UserResponse>> GetUserForAdmin(string filterQuery)
        {
            try
            {
                HttpResponseMessage response = await _httpClient.GetAsync($"api/User/GetUserForAdmin/{filterQuery}");

                var requestResponse = await response.Content.ReadFromJsonAsync<ResultResponse<UserResponse>>();

                if (!requestResponse.IsSuccessful)
                {
                    snackbar.Add(requestResponse.Message, Severity.Error);
                }

                return requestResponse;
            }
            catch (Exception ex)
            {
                snackbar.Add(ex.Message, Severity.Error);
                return new ResultResponse<UserResponse>
                {
                    IsSuccessful = false,
                };
            }
        }

        public async Task<ResultResponse<UserResponse>> GetUserForExaminer(int userId, string filterQuery)
        {
            try
            {
                HttpResponseMessage response = await _httpClient.GetAsync($"api/User/GetUserForExaminer/{userId}/{filterQuery}");

                var requestResponse = await response.Content.ReadFromJsonAsync<ResultResponse<UserResponse>>();

                if (!requestResponse.IsSuccessful)
                {
                    snackbar.Add(requestResponse.Message, Severity.Error);
                }

                return requestResponse;
            }
            catch (Exception ex)
            {
                snackbar.Add(ex.Message, Severity.Error);
                return new ResultResponse<UserResponse>
                {
                    IsSuccessful = false,
                };
            }
        }

        public async Task<ResultResponse<UserRequest>> GetByIdAsync(int id)
        {
            try
            {
                HttpResponseMessage response = await _httpClient.GetAsync($"api/User/get-by-id/{id}");

                var requestResponse = await response.Content.ReadFromJsonAsync<ResultResponse<UserRequest>>();

                if (!requestResponse.IsSuccessful)
                {
                    snackbar.Add(requestResponse.Message, Severity.Error);
                }

                return requestResponse;
            }
            catch (Exception ex)
            {
                snackbar.Add(ex.Message, Severity.Error);
                return new ResultResponse<UserRequest>
                {
                    IsSuccessful = false,
                };
            }
        }

        public async Task<ResultResponse<UserSubjectRequest>> GetUserSubjectByIdAsync(int id)
        {
            try
            {
                HttpResponseMessage response = await _httpClient.GetAsync($"api/User/GetUserSubjectById/{id}");

                var requestResponse = await response.Content.ReadFromJsonAsync<ResultResponse<UserSubjectRequest>>();

                if (!requestResponse.IsSuccessful)
                {
                    snackbar.Add(requestResponse.Message, Severity.Error);
                }

                return requestResponse;
            }
            catch (Exception ex)
            {
                snackbar.Add(ex.Message, Severity.Error);
                return new ResultResponse<UserSubjectRequest>
                {
                    IsSuccessful = false,
                };
            }
        }

        public async Task<ResultResponse<UserResponse>> GetHeadOfDepartment(int subjectId, int campusId)
        {
            try
            {
                HttpResponseMessage response = await _httpClient.GetAsync($"api/User/GetHead/{subjectId}/{campusId}");

                var requestResponse = await response.Content.ReadFromJsonAsync<ResultResponse<UserResponse>>();

                if (!requestResponse.IsSuccessful)
                {
                    snackbar.Add(requestResponse.Message, Severity.Error);
                }

                return requestResponse;
            }
            catch (Exception ex)
            {
                snackbar.Add(ex.Message, Severity.Error);
                return new ResultResponse<UserResponse>
                {
                    IsSuccessful = false,
                };
            }
        }

        public async Task<AuthenticationResponse> GetJWT()
        {
            HttpResponseMessage response = await _httpClient.GetAsync($"api/User/GetJWT");

            var requestResponse = await response.Content.ReadFromJsonAsync<AuthenticationResponse>();

            if (!requestResponse.IsSuccessful)
            {
                snackbar.Add(requestResponse.Message, Severity.Error);
            }

            return requestResponse;
        }

        public async Task<AuthenticationResponse> LoginUserAsync(UserRequest request)
        {

            try
            {
                HttpResponseMessage response = await _httpClient.PostAsJsonAsync("api/User/Login", request);

                var requestResponse = await response.Content.ReadFromJsonAsync<AuthenticationResponse>();

                if (!requestResponse.IsSuccessful)
                {
                    snackbar.Add(requestResponse.Message, Severity.Error);
                }

                return requestResponse;
            }
            catch (Exception ex)
            {
                snackbar.Add(ex.Message, Severity.Error);
                return new AuthenticationResponse { IsSuccessful = false };
            }
        }

        public async Task<RequestResponse> RegisterUserAsync(UserRegisterRequest request)
        {
            try
            {
                HttpResponseMessage response = await _httpClient.PostAsJsonAsync("api/User/Register", request);

                var requestResponse = await response.Content.ReadFromJsonAsync<RequestResponse>();

                if (requestResponse.IsSuccessful)
                {
                    snackbar.Add(requestResponse.Message, Severity.Success);
                }
                else
                {
                    snackbar.Add(requestResponse.Message, Severity.Error);
                }

                return requestResponse;
            }
            catch (Exception ex)
            {
                snackbar.Add(ex.Message, Severity.Error);
                return new RequestResponse { IsSuccessful = false };
            }
        }

        public async Task<RequestResponse> UpdateAsync(UserRequest user)
        {
            try
            {
                HttpResponseMessage response = await _httpClient.PutAsJsonAsync($"api/User/update", user);

                // Reading the response content
                var requestResponse = await response.Content.ReadFromJsonAsync<RequestResponse>();

                if (requestResponse.IsSuccessful)
                {
                    snackbar.Add(requestResponse.Message, Severity.Success);
                }
                else
                {
                    snackbar.Add(requestResponse.Message, Severity.Error);
                }

                return requestResponse;
            }
            catch (Exception ex)
            {
                snackbar.Add(ex.Message, Severity.Error);
                return new RequestResponse { IsSuccessful = false, Message = "An error occurred." };
            }
        }

        public async Task<RequestResponse> ExaminerUpdateUserAsync(UserSubjectRequest user)
        {
            try
            {
                HttpResponseMessage response = await _httpClient.PutAsJsonAsync($"api/User/ExaminerUpdateUser", user);

                // Reading the response content
                var requestResponse = await response.Content.ReadFromJsonAsync<RequestResponse>();

                if (requestResponse.IsSuccessful)
                {
                    snackbar.Add(requestResponse.Message, Severity.Success);
                }
                else
                {
                    snackbar.Add(requestResponse.Message, Severity.Error);
                }

                return requestResponse;
            }
            catch (Exception ex)
            {
                snackbar.Add(ex.Message, Severity.Error);
                return new RequestResponse { IsSuccessful = false, Message = "An error occurred." };
            }
        }

        public async Task<RequestResponse> ImportUserFromExcel(IBrowserFile files)
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
                HttpResponseMessage response = await _httpClient.PostAsync("api/User/ImportUsersFromExcel", content);

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

        public async Task<ResultResponse<UserResponse>> GetAssignedUserByExam(int examId)
        {
            try
            {
                HttpResponseMessage response = await _httpClient.GetAsync($"api/User/GetAssignedUser/{examId}");

                var requestResponse = await response.Content.ReadFromJsonAsync<ResultResponse<UserResponse>>();

                if (!requestResponse.IsSuccessful)
                {
                    snackbar.Add(requestResponse.Message, Severity.Error);
                }

                return requestResponse;
            }
            catch (Exception ex)
            {
                snackbar.Add(ex.Message, Severity.Error);
                return new ResultResponse<UserResponse>
                {
                    IsSuccessful = false,
                };
            }
        }

        public async Task<ResultResponse<UserResponse>> GetLectureListByHead(int userId)
        {
            try
            {
                HttpResponseMessage response = await _httpClient.GetAsync($"api/User/GetLectureListByHead/{userId}");

                var requestResponse = await response.Content.ReadFromJsonAsync<ResultResponse<UserResponse>>();

                if (!requestResponse.IsSuccessful)
                {
                    snackbar.Add(requestResponse.Message, Severity.Error);
                }

                return requestResponse;
            }
            catch (Exception ex)
            {
                snackbar.Add(ex.Message, Severity.Error);
                return new ResultResponse<UserResponse>
                {
                    IsSuccessful = false,
                };
            }
        }

        public async Task<ResultResponse<UserSubjectRequest>> GetUserFacutyByIdAsync(int id)
        {
            try
            {
                HttpResponseMessage response = await _httpClient.GetAsync($"api/User/GetUserFacutyById/{id}");

                var requestResponse = await response.Content.ReadFromJsonAsync<ResultResponse<UserSubjectRequest>>();

                if (!requestResponse.IsSuccessful)
                {
                    snackbar.Add(requestResponse.Message, Severity.Error);
                }

                return requestResponse;
            }
            catch (Exception ex)
            {
                snackbar.Add(ex.Message, Severity.Error);
                return new ResultResponse<UserSubjectRequest>
                {
                    IsSuccessful = false,
                };
            }
        }
    }
}
