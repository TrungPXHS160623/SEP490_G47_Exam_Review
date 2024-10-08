using Library.Common;
using Library.Models;
using MudBlazor;
using WebClient.IServices;

namespace WebClient.Services
{
    public class RoleService : IRoleService
    {
        private readonly HttpClient _httpClient;

        private readonly ISnackbar snackbar;

        public RoleService(HttpClient httpClient, ISnackbar SnackBar)
        {
            _httpClient = httpClient;
            snackbar = SnackBar;
        }

        public async Task<ResultResponse<UserRole>> GetRolesForAdmin()
        {
            HttpResponseMessage response = await _httpClient.GetAsync($"api/Role/GetRolesForAdmin");

            var requestResponse = await response.Content.ReadFromJsonAsync<ResultResponse<UserRole>>();

            if (!requestResponse.IsSuccessful)
            {
                snackbar.Add(requestResponse.Message, Severity.Error);
            }

            return requestResponse;
        }

        public async Task<ResultResponse<UserRole>> GetRolesForExaminer()
        {
            HttpResponseMessage response = await _httpClient.GetAsync($"api/Role/GetRolesForExaminer");

            var requestResponse = await response.Content.ReadFromJsonAsync<ResultResponse<UserRole>>();

            if (!requestResponse.IsSuccessful)
            {
                snackbar.Add(requestResponse.Message, Severity.Error);
            }

            return requestResponse;
        }
    }
}
