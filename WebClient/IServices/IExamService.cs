using Library.Common;
using Library.Request;
using Library.Response;
using Microsoft.AspNetCore.Components.Forms;

namespace WebClient.IServices
{
    public interface IExamService
    {
        Task<ResultResponse<TestDepartmentExamResponse>> GetExamList(ExamSearchRequest req);
        Task<ResultResponse<LeaderExamResponse>> GetLeaderExamList(ExamSearchRequest req);
        Task<ResultResponse<LectureExamResponse>> GetLectureExamList(ExamSearchRequest req);

        Task<ResultResponse<TestDepartmentExamResponse>> GetExamById(int examId);

        Task<ResultResponse<LeaderExamResponse>> GetLeaderExamById(int examId);

        Task<ResultResponse<LectureExamResponse>> GetLectureExamById(int examId);

        Task<RequestResponse> UpdateExam(TestDepartmentExamResponse exam);

        Task<RequestResponse> ChangeStatusExam(List<TestDepartmentExamResponse> exam);

        Task<RequestResponse> ChangeStatusExamById(int examId, int statusId);

        Task<RequestResponse> CreateExam(ExamCreateRequest exam);
        Task<RequestResponse> ImportExamsFromExcel(List<IBrowserFile> files);
        Task<ResultResponse<byte[]>> ExportAllExams();


    }
}
