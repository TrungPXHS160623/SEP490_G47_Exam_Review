using Library.Common;
using Library.Response;

namespace WebApi.IRepository
{
    public interface IInstructorAssignmentRepository
    {
        Task<RequestResponse> AssignExamToLecture(LeaderExamResponse req);
        Task<RequestResponse> SetAssignDate(LectureExamResponse req);
    }
}
