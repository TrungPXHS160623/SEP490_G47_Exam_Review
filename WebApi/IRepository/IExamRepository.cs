using Library.Common;
using Library.Models.Dtos;
using Library.Request;
using Library.Response;

namespace WebApi.IRepository
{
	public interface IExamRepository
	{
		Task<IEnumerable<ExamInfoDto>> GetExamInfoAsync();

		//Task<ResultResponse<TestDepartmentExamResponse>> GetExamList(ExamSearchRequest req);

		//Task<ResultResponse<TestDepartmentExamResponse>> GetExamById(int examId);

		Task<RequestResponse> UpdateExam(TestDepartmentExamResponse exam);

		Task<RequestResponse> ChangeStatusExam(List<TestDepartmentExamResponse> exam);

	}
}
