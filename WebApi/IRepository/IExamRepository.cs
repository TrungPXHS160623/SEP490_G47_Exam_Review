using Library.Models.Dtos;

namespace WebApi.IRepository
{
	public interface IExamRepository
	{
		Task<IEnumerable<ExamInfoDto>> GetExamInfoAsync();
	}
}
