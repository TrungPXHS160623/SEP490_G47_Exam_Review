using Library.Common;
using Library.Request;
using Library.Response;
using Microsoft.AspNetCore.Components.Forms;

namespace WebClient.IServices
{
    public interface ISubjectService
    {
        Task<ResultResponse<SubjectResponse>> GetSubjects();
        Task<ResultResponse<SubjectResponse>> GetSubjectsList(SubjectRequest req);
        Task<ResultResponse<SubjectResponse>> GetLectureSubject(int userId);
        Task<ResultResponse<HeadSubjectRepsonse>> GetHeadSubject(int userId);
        Task<ResultResponse<SubjectRequest>> GetSubjectById(int subjectId);
        Task<RequestResponse> AddSubject(SubjectRequest req);
        Task<RequestResponse> UpdateSubject(SubjectRequest req);
        Task<RequestResponse> DeleteSubject(int subjectId);
        Task<ResultResponse<SubjectResponse>> GetSubjectByRole(int roleId, int userId, int campusId);
        Task<RequestResponse> ImportSubjectFromExcel(IBrowserFile files);

        Task<RequestResponse> LecturerSubjectModify(int userId, HashSet<SubjectResponse> req);

        Task<RequestResponse> AddSubjectToDepartment(SubjectDepartmentRequest req);
    }
}
