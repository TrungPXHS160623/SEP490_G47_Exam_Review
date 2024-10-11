using Library.Common;
using Library.Models.Dtos;
using Library.Request;
using Library.Response;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.IRepository
{
	public interface IExamRepository
	{
		Task<IEnumerable<ExamInfoDto>> GetExamInfoAsync();

		Task<ResultResponse<TestDepartmentExamResponse>> GetExamList(ExamSearchRequest req);
        Task<ResultResponse<TestDepartmentExamResponse>> GetExamById(int examId);

        Task<ResultResponse<LeaderExamResponse>> GetLeaderExamList(ExamSearchRequest req);

        Task<ResultResponse<LeaderExamResponse>> GetLeaderExamById(int examId);

        Task<ResultResponse<LectureExamResponse>> GetLectureExamList(ExamSearchRequest req);

        Task<ResultResponse<LectureExamResponse>> GetLectureExamById(int examId);

        Task<RequestResponse> UpdateExam(TestDepartmentExamResponse exam);

		Task<RequestResponse> ChangeStatusExam(List<TestDepartmentExamResponse> exam);

        Task<RequestResponse> ChangeStatusExamById(int examId, int statusId);

        Task<RequestResponse> CreateExam(ExamCreateRequest exam);

		//phần tui làm
		Task<ResultResponse<ExamExportResponse>> ExportExamsToCsv();

        Task<ResultResponse<ExamExportResponse>> ExportExamsToExcel();

        Task<RequestResponse> ImportExamsFromCsv(List<ExamImportRequest> examImportDtos);

        Task<RequestResponse> ImportExamsFromExcel(IFormFile file);

		//get exam by status
		Task<(IEnumerable<ExamByStatusResponse> Exams, int Count)> GetExamsByStatus(int? statusId = null, int? campusId = null);
	}
}
