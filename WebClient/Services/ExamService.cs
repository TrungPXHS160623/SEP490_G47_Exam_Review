using Library.Common;
using Library.Request;
using Library.Response;
using Microsoft.AspNetCore.Components.Forms;
using MudBlazor;
using System.Net.Http.Json;
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

        public async Task<ResultResponse<TestDepartmentExamResponse>> GetExamList(ExamSearchRequest req)
        {
            try
            {
                HttpResponseMessage response = await _httpClient.PostAsJsonAsync($"api/Exam/GetExamList", req);

                var requestResponse = await response.Content.ReadFromJsonAsync<ResultResponse<TestDepartmentExamResponse>>();

                if (!requestResponse.IsSuccessful)
                {
                    snackbar.Add(requestResponse.Message, Severity.Error);
                }

                return requestResponse;
            }
            catch (Exception ex)
            {
                snackbar.Add(ex.Message, Severity.Error);

                return new ResultResponse<TestDepartmentExamResponse>
                {
                    IsSuccessful = false,
                };
            }
        }

        public async Task<ResultResponse<LeaderExamResponse>> GetLeaderExamList(ExamSearchRequest req)
        {
            try
            {
                HttpResponseMessage response = await _httpClient.PostAsJsonAsync($"api/Exam/GetLeaderExamList", req);

                var requestResponse = await response.Content.ReadFromJsonAsync<ResultResponse<LeaderExamResponse>>();

                if (!requestResponse.IsSuccessful)
                {
                    snackbar.Add(requestResponse.Message, Severity.Error);
                }

                return requestResponse;
            }
            catch (Exception ex)
            {
                snackbar.Add(ex.Message, Severity.Error);

                return new ResultResponse<LeaderExamResponse>
                {
                    IsSuccessful = false,
                };
            }
        }

        public async Task<ResultResponse<LectureExamResponse>> GetLectureExamList(ExamSearchRequest req)
        {
            try
            {
                HttpResponseMessage response = await _httpClient.PostAsJsonAsync($"api/Exam/GetLectureExamList", req);

                var requestResponse = await response.Content.ReadFromJsonAsync<ResultResponse<LectureExamResponse>>();

                if (!requestResponse.IsSuccessful)
                {
                    snackbar.Add(requestResponse.Message, Severity.Error);
                }

                return requestResponse;
            }
            catch (Exception ex)
            {
                snackbar.Add(ex.Message, Severity.Error);

                return new ResultResponse<LectureExamResponse>
                {
                    IsSuccessful = false,
                };
            }
        }

        public async Task<ResultResponse<TestDepartmentExamResponse>> GetExamById(int examId)
        {
            try
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
            catch (Exception ex)
            {
                snackbar.Add(ex.Message, Severity.Error);

                return new ResultResponse<TestDepartmentExamResponse>
                {
                    IsSuccessful = false,
                };
            }
        }

        public async Task<ResultResponse<LeaderExamResponse>> GetLeaderExamById(int examId)
        {
            try
            {
                //Check JWT key
                if (Constants.JWTToken == "")
                {
                    return null;
                }
                _httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", Constants.JWTToken);

                HttpResponseMessage response = await _httpClient.GetAsync($"api/Exam/GetLeaderExamById/{examId}");

                var requestResponse = await response.Content.ReadFromJsonAsync<ResultResponse<LeaderExamResponse>>();

                if (!requestResponse.IsSuccessful)
                {
                    snackbar.Add(requestResponse.Message, Severity.Error);
                }

                return requestResponse;
            }
            catch (Exception ex)
            {
                snackbar.Add(ex.Message, Severity.Error);

                return new ResultResponse<LeaderExamResponse>
                {
                    IsSuccessful = false,
                };
            }
        }

        public async Task<ResultResponse<LectureExamResponse>> GetLectureExamById(int examId)
        {
            try
            {
                //Check JWT key
                if (Constants.JWTToken == "")
                {
                    return null;
                }
                _httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", Constants.JWTToken);

                HttpResponseMessage response = await _httpClient.GetAsync($"api/Exam/GetLectureExamById/{examId}");

                var requestResponse = await response.Content.ReadFromJsonAsync<ResultResponse<LectureExamResponse>>();

                if (!requestResponse.IsSuccessful)
                {
                    snackbar.Add(requestResponse.Message, Severity.Error);
                }

                return requestResponse;
            }
            catch (Exception ex)
            {
                snackbar.Add(ex.Message, Severity.Error);

                return new ResultResponse<LectureExamResponse>
                {
                    IsSuccessful = false,
                };
            }
        }

        public async Task<RequestResponse> UpdateExam(TestDepartmentExamResponse exam)
        {
            //Check JWT key
            if (Constants.JWTToken == "")
            {
                return null;
            }
            _httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", Constants.JWTToken);

            HttpResponseMessage response = await _httpClient.PutAsJsonAsync($"api/Exam/UpdateExam", exam);

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

        public async Task<RequestResponse> ChangeStatusExam(List<TestDepartmentExamResponse> exam)
        {
            //Check JWT key
            if (Constants.JWTToken == "")
            {
                return null;
            }
            _httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", Constants.JWTToken);

            HttpResponseMessage response = await _httpClient.PutAsJsonAsync($"api/Exam/ChangeStatus", exam);

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

        public async Task<RequestResponse> ChangeStatusExamById(int examId, int statusId)
        {
            //Check JWT key
            if (Constants.JWTToken == "")
            {
                return null;
            }
            _httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", Constants.JWTToken);

            HttpResponseMessage response = await _httpClient.PutAsJsonAsync($"api/Exam/ChangeStatus/{examId}", statusId);

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

        public async Task<RequestResponse> CreateExam(ExamCreateRequest exam)
        {
            //Check JWT key
            if (Constants.JWTToken == "")
            {
                return null;
            }
            _httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", Constants.JWTToken);

            HttpResponseMessage response = await _httpClient.PostAsJsonAsync($"api/Exam/CreateExam", exam);

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
        public async Task<RequestResponse> ImportExamsFromExcel(List<IBrowserFile> files)
        {
            if (string.IsNullOrWhiteSpace(Constants.JWTToken))
            {
                snackbar.Add("Authorization token is missing.", Severity.Error);
                return new RequestResponse { IsSuccessful = false, Message = "Missing Authorization Token" };
            }

            _httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", Constants.JWTToken);

            using (var content = new MultipartFormDataContent())
            {
                if (files == null || files.Count == 0)
                {
                    snackbar.Add("No files selected for upload.", Severity.Warning);
                    return new RequestResponse { IsSuccessful = false, Message = "No files selected." };
                }

                foreach (var file in files)
                {
                    if (file != null && file.Size > 0)
                    {
                        if (!file.ContentType.Equals("application/vnd.openxmlformats-officedocument.spreadsheetml.sheet"))
                        {
                            snackbar.Add($"File '{file.Name}' is not a valid Excel file.", Severity.Error);
                            return new RequestResponse { IsSuccessful = false, Message = $"Invalid file format for file '{file.Name}'." };
                        }

                        using (var fileStreamContent = new StreamContent(file.OpenReadStream()))
                        {
                            fileStreamContent.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue(file.ContentType);
                            content.Add(fileStreamContent, "files", file.Name);
                        }
                    }
                    else
                    {
                        snackbar.Add($"File '{file?.Name}' is empty or invalid.", Severity.Warning);
                        return new RequestResponse { IsSuccessful = false, Message = $"File '{file?.Name}' is empty or invalid." };
                    }
                }

                try
                {
                    HttpResponseMessage response = await _httpClient.PostAsync("api/Exam/ImportExamsFromExcel", content);

                    response.EnsureSuccessStatusCode();

                    var requestResponse = await response.Content.ReadFromJsonAsync<RequestResponse>();

                    if (!requestResponse.IsSuccessful)
                    {
                        snackbar.Add(requestResponse.Message, Severity.Error);
                    }
                    else
                    {
                        snackbar.Add("Exams imported successfully!", Severity.Success);
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

        public async Task<ResultResponse<byte[]>> ExportAllExams()
        {
            //Check JWT key

            try
            {
                if (string.IsNullOrEmpty(Constants.JWTToken))
                {
                    return null;
                }

                _httpClient.DefaultRequestHeaders.Authorization =
                    new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", Constants.JWTToken);

                HttpResponseMessage response = await _httpClient.GetAsync("api/GenerateExcel/export");
                if (response.IsSuccessStatusCode)
                {
                    var fileBytes = await response.Content.ReadAsByteArrayAsync();

                    return new ResultResponse<byte[]>
                    {
                        IsSuccessful = true,
                        Item = fileBytes,
                        Message = "Export Suscess"
                    };
                }
                else
                {
                    var errorMessage = await response.Content.ReadAsStringAsync();
                    return new ResultResponse<byte[]>
                    {
                        IsSuccessful = false,
                        Message = errorMessage
                    };
                }
            }
            catch (Exception ex)
            {
                return (new ResultResponse<byte[]>
                {
                    IsSuccessful = false,
                    Message = "An error occurred while exporting: " + ex.Message
                });
            }
        }

    }
}
