using Library.Common;
using Library.Models;
using MudBlazor;
using WebClient.IServices;

namespace WebClient.Services
{
    public class MenuService : IMenuService
    {
        private readonly HttpClient _httpClient;

        private readonly ISnackbar snackbar;

        public MenuService(HttpClient httpClient, ISnackbar SnackBar)
        {
            _httpClient = httpClient;
            snackbar = SnackBar;
        }

        public async Task<ResultResponse<Menu>> GetMenuByUser(int role)
        {
            //Check JWT key
            if (Constants.JWTToken == "")
            {
                return null;
            }
            _httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", Constants.JWTToken);

            HttpResponseMessage response = await _httpClient.GetAsync($"api/Menu/GetMenu/{role}");

            var requestResponse = await response.Content.ReadFromJsonAsync<ResultResponse<Menu>>();

            if (!requestResponse.IsSuccessful)
            {
                snackbar.Add(requestResponse.Message, Severity.Error);
            }

            return requestResponse;
        }

    }
}
