using Library.Common;
using Library.Models.Dtos;
using Library.Request;
using Library.Response;

namespace WebApi.IRepository
{
    public interface IExamRepository
    {
        Task<IEnumerable<ExamInfoDto>> GetExamInfoAsync();

        Task<ResultResponse<ExaminerExamResponse>> GetExamList(ExamSearchRequest req);
        Task<ResultResponse<ExaminerExamResponse>> GetExamById(int examId);

        Task<ResultResponse<LeaderExamResponse>> GetLeaderExamList(ExamSearchRequest req);
        Task<ResultResponse<LeaderExamResponse>> GetRemindExamList();

        Task<ResultResponse<LeaderExamResponse>> GetLeaderExamById(int examId);

        Task<ResultResponse<LectureExamResponse>> GetLectureExamList(ExamSearchRequest req);

        Task<ResultResponse<LectureExamResponse>> GetLectureExamById(int examId);

        Task<RequestResponse> UpdateExam(ExaminerExamResponse exam);

        Task<RequestResponse> ChangeStatusExam(List<ExaminerExamResponse> exam);

        Task<RequestResponse> ChangeStatusExamById(int examId, int statusId);

        Task<RequestResponse> CreateExam(ExamCreateRequest exam);
        Task<RequestResponse> ImportExamsFromExcel(IFormFile file);

        Task<ResultResponse<CampusSubjectExamResponse>> GetExamByCampusAndSubject(int userID);
        Task<ResultResponse<CampusReportResponse>> GetCampusReport();
        Task<ResultResponse<DepartmentReportResponse>> GetDepartmentReport(int userID);

        Task<(IEnumerable<ExamByStatusResponse> Exams, int Count)> GetExamsByStatus(int? statusId = null, int? campusId = null);

        Task<List<ExamBySemesterResponse>> ExamBySemesterNameAndUserId(int semesterId, int userId);

        Task<List<ExamRemindResponse>> GetRemindExam();
    }
}
