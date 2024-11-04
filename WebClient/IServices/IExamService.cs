using Library.Common;
using Library.Request;
using Library.Response;
using Microsoft.AspNetCore.Components.Forms;

namespace WebClient.IServices
{
    public interface IExamService
    {
        Task<ResultResponse<ExaminerExamResponse>> GetExamList(ExamSearchRequest req);
        Task<ResultResponse<LeaderExamResponse>> GetLeaderExamList(ExamSearchRequest req);
        Task<ResultResponse<LectureExamResponse>> GetLectureExamList(ExamSearchRequest req);

        Task<ResultResponse<ExaminerExamResponse>> GetExamById(int examId);

        Task<ResultResponse<LeaderExamResponse>> GetLeaderExamById(int examId);

        Task<ResultResponse<LectureExamResponse>> GetLectureExamById(int examId,int userId);

        Task<RequestResponse> UpdateExam(ExaminerExamResponse exam);

        Task<RequestResponse> ChangeStatusExam(List<ExaminerExamResponse> exam);

        Task<RequestResponse> ChangeStatusExamById(int examId, int statusId);

        Task<RequestResponse> CreateExam(ExamCreateRequest exam);
        Task<RequestResponse> ImportExamsFromExcel(IBrowserFile files);
        Task<ResultResponse<byte[]>> ExportAllExams();


    }
}
