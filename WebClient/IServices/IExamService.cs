using Library.Common;
using Library.Request;
using Library.Response;
using Microsoft.AspNetCore.Components.Forms;

namespace WebClient.IServices
{
    public interface IExamService
    {
        Task<ResultResponse<TestDepartmentExamResponse>> GetExamList(ExamSearchRequest req);

        Task<ResultResponse<TestDepartmentExamResponse>> GetExamById(int examId);

        Task<RequestResponse> UpdateExam(TestDepartmentExamResponse exam);

        Task<RequestResponse> ChangeStatusExam(List<TestDepartmentExamResponse> exam);

        Task<RequestResponse> CreateExam(ExamCreateRequest exam);
        Task<RequestResponse> ImportExamsFromExcel(List<IBrowserFile> files);
        Task<ResultResponse<byte[]>> ExportAllExams();


    }
}
