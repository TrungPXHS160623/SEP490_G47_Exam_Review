using Library.Common;
using Library.Response;

namespace WebClient.IServices
{
    public interface IExamService
    {
        Task<ResultResponse<TestDepartmentExamResponse>> GetExamList();

        Task<ResultResponse<TestDepartmentExamResponse>> GetExamById(int examId);
    }
}
