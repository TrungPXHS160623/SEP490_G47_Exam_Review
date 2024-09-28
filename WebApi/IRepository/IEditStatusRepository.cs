using Library.Common;
using Library.Request;
using Library.Response;

namespace WebApi.IRepository
{
	public interface IEditStatusRepository
	{
		Task<ResultResponse<StatusRequest>> EditStatus(int examID, string newStatus);
	}
}
