using Library.Common;
using Library.Request;
using Library.Response;

namespace WebApi.IRepository
{
	public interface IEstimatedTimeRepository
	{
		Task<EstimatedTimeResponse> AddEstimatedTimeTest(int instructorAssignmentId, EstimatedTimeRequest request);
		Task<EstimatedTimeResponse> UpdateEstimatedTimeTest(int instructorAssignmentId, EstimatedTimeRequest request);
		Task<EstimatedTimeResponse> GetEstimatedTimeTest(int instructorAssignmentId);
	}
}
