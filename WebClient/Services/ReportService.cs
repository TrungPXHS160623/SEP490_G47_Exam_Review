using Library.Common;
using Library.Response;
using Microsoft.AspNetCore.Components.Forms;
using MudBlazor;
using WebClient.IServices;

namespace WebClient.Services
{
    public class ReportService : IReportService
    {
        private readonly HttpClient _httpClient;

        private readonly ISnackbar snackbar;

        public ReportService(HttpClient httpClient, ISnackbar SnackBar)
        {
            _httpClient = httpClient;
            snackbar = SnackBar;
        }

        public async Task<RequestResponse> AddEditReport(LectureExamResponse reportRequest, bool isSubmit)
        {
            try
            {
                HttpResponseMessage response = await _httpClient.PostAsJsonAsync($"api/Report/SaveReport/{isSubmit}", reportRequest);

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
        public async Task<RequestResponse> ImportFile(IBrowserFile file, bool isSubmit)
        {
            if (string.IsNullOrWhiteSpace(Constants.JWTToken))
            {
                snackbar.Add("Authorization token is missing.", Severity.Error);
                return new RequestResponse { IsSuccessful = false, Message = "Missing Authorization Token" };
            }

            // Set authorization header
            _httpClient.DefaultRequestHeaders.Authorization =
                new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", Constants.JWTToken);

            try
            {
                // Create multipart form data content
                using var content = new MultipartFormDataContent();

                // Add the file stream to the form data
                var fileStream = file.OpenReadStream(maxAllowedSize: 10 * 1024 * 1024); // Set max size as needed
                var streamContent = new StreamContent(fileStream);
                content.Add(streamContent, "file", file.Name); // Ensure "file" matches the API's expected parameter name

                // Send the POST request
                HttpResponseMessage response = await _httpClient.PostAsync($"api/Report/UploadReportWithFiles/{isSubmit}", content);
                response.EnsureSuccessStatusCode();  // Throws if not successful

                // Parse the response
                var requestResponse = await response.Content.ReadFromJsonAsync<RequestResponse>();

                if (requestResponse == null)
                {
                    snackbar.Add("Failed to process server response.", Severity.Error);
                    return new RequestResponse { IsSuccessful = false, Message = "Null response from server" };
                }

                // Handle success or error response
                if (requestResponse.IsSuccessful)
                {
                    snackbar.Add("Exams imported successfully!", Severity.Success);
                }
                else
                {
                    snackbar.Add(requestResponse.Message, Severity.Error);
                }

                return requestResponse;
            }
            catch (Exception ex)
            {
                snackbar.Add($"Error during file upload: {ex.Message}", Severity.Error);
                return new RequestResponse { IsSuccessful = false, Message = ex.Message };
            }
        }

    }
}
