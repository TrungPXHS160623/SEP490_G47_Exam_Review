using Library.Common;
using Library.Request;
using Library.Response;

namespace WebClient.IServices
{
    public interface IExamService
    {
        Task<ResultResponse<TestDepartmentExamResponse>> GetExamList(ExamSearchRequest req);

        Task<ResultResponse<TestDepartmentExamResponse>> GetExamById(int examId);

        Task<RequestResponse> UpdateExam(TestDepartmentExamResponse exam);

        Task<RequestResponse> ChangeStatusExam(List<TestDepartmentExamResponse> exam);

    }
}
