using Library.Common;
using Library.Request;
using Library.Response;
using System.Threading.Tasks;

namespace WebApi.IRepository
{
	public interface IExamAssignRepository
	{
		Task<ResultResponse<ExamAssignResponse>> GetExamAssignByHeadId(int userId);
	}
}
