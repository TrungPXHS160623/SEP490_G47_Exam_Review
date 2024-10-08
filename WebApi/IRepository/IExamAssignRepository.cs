using Library.Common;
using Library.Response;

namespace WebApi.IRepository
{
    public interface IExamAssignRepository
    {
        Task<ResultResponse<ExamAssignResponse>> GetExamAssignByHeadId(int userId);
    }
}
