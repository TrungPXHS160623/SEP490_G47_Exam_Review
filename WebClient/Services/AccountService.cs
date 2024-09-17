using WebClient.Authentication;
using Library.Common;
using Library.Request;
using System.Text;
using Newtonsoft.Json;
using WebClient.IServices;
using MudBlazor;
using Library.Response;
using Library.Models;

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

        public async Task<ResultResponse<Account>> GetUserList()
        {
            try
            {
                //Check JWT key
                if (Constants.JWTToken == "")
                {
                    return null;
                }
                _httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", Constants.JWTToken);

                HttpResponseMessage response = await _httpClient.GetAsync($"api/Account/GetUser");

                var requestResponse = await response.Content.ReadFromJsonAsync<ResultResponse<Account>>();

                if (!requestResponse.IsSuccessful)
                {
                    snackbar.Add(requestResponse.Message, Severity.Error);
                }

                return requestResponse;
            }
            catch (Exception ex)
            {
                snackbar.Add(ex.Message, Severity.Error);
                return new ResultResponse<Account>
                {
                    IsSuccessful = false,
                };
            }
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
            } catch (Exception ex)
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
                } else
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
    }
}
