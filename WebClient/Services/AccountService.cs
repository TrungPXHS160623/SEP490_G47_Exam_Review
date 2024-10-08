﻿using Library.Common;
using Library.Models;
using Library.Request;
using Library.Response;
using MudBlazor;
using WebClient.IServices;

namespace WebClient.Services
{
    public class AccountService : IAccountService
    {
        private readonly HttpClient _httpClient;

        private readonly ISnackbar snackbar;

        public AccountService(HttpClient httpClient, ISnackbar SnackBar)
        {
            _httpClient = httpClient;
            snackbar = SnackBar;
        }

        public async Task<RequestResponse> ClearJWT()
        {
            HttpResponseMessage response = await _httpClient.GetAsync($"api/Account/ClearJWT");

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

        public async Task<ResultResponse<UserResponse>> GetAllUserList()
        {
            try
            {
                HttpResponseMessage response = await _httpClient.GetAsync($"api/User/get-all");

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

        public async Task<ResultResponse<UserResponse>> GetLectureList()
        {
            try
            {
                HttpResponseMessage response = await _httpClient.GetAsync($"api/User/GetLecture");

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

        public async Task<ResultResponse<UserResponse>> GetUserForExaminer(string filterQuery)
        {
            try
            {
                HttpResponseMessage response = await _httpClient.GetAsync($"api/User/GetUserForExaminer/{filterQuery}");

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
                HttpResponseMessage response = await _httpClient.GetAsync($"api/User/GetUserSubject/{id}");

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
            HttpResponseMessage response = await _httpClient.GetAsync($"api/Account/GetJWT");

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
                HttpResponseMessage response = await _httpClient.PostAsJsonAsync("api/Account/Login", request);

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
                HttpResponseMessage response = await _httpClient.PostAsJsonAsync("api/Account/Register", request);

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

    }
}
