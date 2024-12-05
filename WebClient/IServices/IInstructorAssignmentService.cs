using Library.Common;
using Library.Request;
using Library.Response;

namespace WebClient.IServices
{
    public interface IInstructorAssignmentService
    {
        Task<RequestResponse> AssignExamToLecture(LeaderExamResponse req);
        Task<RequestResponse> AssignSubjectToLecture(AddLecturerSubjectRequest req);

        Task<RequestResponse> SetAssignDate(LectureExamResponse req);
    }
}
