using MudBlazor;
using WebClient.IServices;
using PRN231_Library.Common;
using PRN231_Library.Models;
using System;

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
        public async Task<ResultResponse<Menu>> GetMenuByRole(int roleId)
        {
            try
            {
                if (Constants.JWTToken == "")
                {
                    return null;
                }
                _httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", Constants.JWTToken);

                HttpResponseMessage response = await _httpClient.GetAsync($"api/Menu/GetMenuByRole/{roleId}");

                var requestResponse = await response.Content.ReadFromJsonAsync<ResultResponse<Menu>>();

                if (!requestResponse.IsSuccessful)
                {
                    snackbar.Add(requestResponse.Message, Severity.Error);
                }

                return requestResponse;
            }
            catch (Exception ex)
            {
                snackbar.Add(ex.Message, Severity.Error);
                return new ResultResponse<Menu>
                {
                    IsSuccessful = false,
                };
            }
        }
    }
}
