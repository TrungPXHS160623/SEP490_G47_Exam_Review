using Library.Common;
using Library.Models.Dtos;
using Library.Response;

namespace WebApi.IRepository
{
	public interface IExamRepository
	{
		Task<IEnumerable<ExamInfoDto>> GetExamInfoAsync();

		Task<ResultResponse<TestDepartmentExamResponse>> GetExamList();

		Task<ResultResponse<TestDepartmentExamResponse>> GetExamById(int examId);
	}
}
