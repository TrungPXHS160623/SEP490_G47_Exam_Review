using Library.Common;
using Library.Models;
using Library.Response;
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
    }
}
