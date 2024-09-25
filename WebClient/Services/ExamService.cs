using Library.Common;
using Library.Response;
using MudBlazor;
using WebClient.IServices;

namespace WebClient.Services
{
    public class ExamService : IExamService
    {
        private readonly HttpClient _httpClient;

        private readonly ISnackbar snackbar;

        public ExamService(HttpClient httpClient, ISnackbar SnackBar)
        {
            _httpClient = httpClient;
            snackbar = SnackBar;
        }

        public async Task<ResultResponse<TestDepartmentExamResponse>> GetExamList()
        {
            //Check JWT key
            if (Constants.JWTToken == "")
            {
                return null;
            }
            _httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", Constants.JWTToken);

            HttpResponseMessage response = await _httpClient.GetAsync($"api/Exam/GetExamList");

            var requestResponse = await response.Content.ReadFromJsonAsync<ResultResponse<TestDepartmentExamResponse>>();

            if (!requestResponse.IsSuccessful)
            {
                snackbar.Add(requestResponse.Message, Severity.Error);
            }

            return requestResponse;
        }

        public async Task<ResultResponse<TestDepartmentExamResponse>> GetExamById(int examId)
        {
            //Check JWT key
            if (Constants.JWTToken == "")
            {
                return null;
            }
            _httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", Constants.JWTToken);

            HttpResponseMessage response = await _httpClient.GetAsync($"api/Exam/GetExamById/{examId}");

            var requestResponse = await response.Content.ReadFromJsonAsync<ResultResponse<TestDepartmentExamResponse>>();

            if (!requestResponse.IsSuccessful)
            {
                snackbar.Add(requestResponse.Message, Severity.Error);
            }

            return requestResponse;
        }

    }
}
