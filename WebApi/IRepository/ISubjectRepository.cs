using Library.Common;
using Library.Models;
using Library.Request;
using Library.Response;
using System.Security.Claims;

namespace WebApi.IRepository
{
    public interface ISubjectRepository
    {
        Task<ResultResponse<Subject>> GetSubjects();
        Task<ResultResponse<SubjectResponse>> GetSubjectList(SubjectRequest req);

        Task<ResultResponse<SubjectResponse>> GetLectureSubjectList(int userId);

        Task<ResultResponse<Subject>> GetSubjectById(int subjectId);
        Task<RequestResponse> AddSubject(Subject req);

        Task<RequestResponse> UpdateSubject(Subject req);
        Task<RequestResponse> DeleteSubject(int subjectId);

        Task<RequestResponse> LecturerSubjectModify(int userId,HashSet<SubjectResponse> req);

        Task<ResultResponse<SubjectResponse>> GetSubjectByRole(int roleId, int userId,int campusId);

        Task<RequestResponse> ImportSubjectsFromExcel(IFormFile file, ClaimsPrincipal currentUser);
    }
}
