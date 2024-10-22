using Library.Common;
using Library.Models;
using Library.Request;
using Library.Response;
using MudBlazor;
using WebClient.IServices;

namespace WebClient.Services
{
    public class UserHistoryService : IUserHistoryService
    {
        private readonly HttpClient _httpClient;

        private readonly ISnackbar snackbar;

        public UserHistoryService(HttpClient httpClient, ISnackbar SnackBar)
        {
            _httpClient = httpClient;
            snackbar = SnackBar;
        }

        public async Task<ResultResponse<LogResponse>> GetLog(LogRequest request)
        {
            try
            {
                HttpResponseMessage response = await _httpClient.PostAsJsonAsync($"api/UserHistory/GetLog",request);

                var requestResponse = await response.Content.ReadFromJsonAsync<ResultResponse<LogResponse>>();

                if (!requestResponse.IsSuccessful)
                {
                    snackbar.Add(requestResponse.Message, Severity.Error);
                }

                return requestResponse;
            }
            catch (Exception ex)
            {
                snackbar.Add(ex.Message, Severity.Error);
                return new ResultResponse<LogResponse>
                {
                    IsSuccessful = false,
                };
            }
        }
    }
}
