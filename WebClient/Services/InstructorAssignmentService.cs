using Library.Common;
using Library.Request;
using Library.Response;
using MudBlazor;
using WebClient.IServices;

namespace WebClient.Services
{
    public class InstructorAssignmentService : IInstructorAssignmentService
    {
        private readonly HttpClient _httpClient;

        private readonly ISnackbar snackbar;

        public InstructorAssignmentService(HttpClient httpClient, ISnackbar SnackBar)
        {
            _httpClient = httpClient;
            snackbar = SnackBar;
        }

        public async Task<RequestResponse> AssignExamToLecture(LeaderExamResponse req)
        {
            try
            {
                HttpResponseMessage response = await _httpClient.PostAsJsonAsync($"api/InstructorAssignment/AssignExamToLecture", req);

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

        public async Task<RequestResponse> AssignSubjectToLecture(AddLecturerSubjectRequest req)
        {
            try
            {
                HttpResponseMessage response = await _httpClient.PostAsJsonAsync($"api/InstructorAssignment/AssignSubjectToLecture", req);

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

        public async Task<RequestResponse> SetAssignDate(LectureExamResponse req)
        {
            try
            {
                HttpResponseMessage response = await _httpClient.PostAsJsonAsync($"api/InstructorAssignment/SetAssignDate", req);

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
    }
}
