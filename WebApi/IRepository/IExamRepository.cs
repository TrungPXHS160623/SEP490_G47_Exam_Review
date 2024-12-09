using Library.Common;
using Library.Models.Dtos;
using Library.Request;
using Library.Response;
using System.Security.Claims;

namespace WebApi.IRepository
{
    public interface IExamRepository
    {
        Task<IEnumerable<ExamInfoDto>> GetExamInfoAsync();

        Task<ResultResponse<ExaminerExamResponse>> GetExamList(ExamSearchRequest req);
        Task<ResultResponse<ExaminerExamResponse>> GetExamById(int examId);
        Task<ResultResponse<LeaderExamResponse>> GetAdminExamList(ExamSearchRequest req);
        Task<ResultResponse<LeaderExamResponse>> GetLeaderExamList(ExamSearchRequest req);
        Task<ResultResponse<LeaderExamResponse>> GetRemindExamList();

        Task<ResultResponse<LeaderExamResponse>> GetLeaderExamById(int examId);

        Task<ResultResponse<LectureExamResponse>> GetLectureExamList(ExamSearchRequest req);

        Task<ResultResponse<LectureExamResponse>> GetLectureExamById(int examId);

        Task<RequestResponse> UpdateExam(ExaminerExamResponse exam);

        Task<RequestResponse> ChangeStatusExam(List<ExaminerExamResponse> exam);

        Task<RequestResponse> ChangeStatusExamById(int examId, int statusId);

        Task<RequestResponse> CreateExam(ExamCreateRequest exam);
        Task<RequestResponse> ImportExamsFromExcel(IFormFile file, ClaimsPrincipal currentUser);

        Task<ResultResponse<CampusSubjectExamResponse>> GetExamByCampusAndSubject(UserRequest req);
        Task<ResultResponse<CampusReportResponse>> GetCampusReport();
        Task<ResultResponse<DepartmentReportResponse>> GetDepartmentReport(UserRequest req);

        Task<(IEnumerable<ExamByStatusResponse> Exams, int Count)> GetExamsByStatus(int? statusId = null, int? campusId = null);

        Task<List<ExamBySemesterResponse>> ExamBySemesterNameAndUserId(int semesterId, int userId);

        // Task<List<ExamRemindResponse>> GetRemindExam();
    }
}
