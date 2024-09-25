using Library.Common;
using Library.Response;

namespace WebApi.IRepository
{
	public interface IExamAssignRepository
	{
		Task<ResultResponse<ExamAssignResponse>> GetExamAssign(int examID);
		Task<ResultResponse<ExamAssignResponse>> GetAndEditExamAssign(int examID, string newStatus);  
	}

}
