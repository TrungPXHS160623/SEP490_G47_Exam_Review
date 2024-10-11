using Library.Common;
using Library.Models;
using Library.Response;

namespace WebApi.IRepository
{
    public interface ISubjectRepository
    {
        Task<ResultResponse<Subject>> GetSubjects();
        Task<ResultResponse<Subject>> GetSubjectById(int subjectId);
        Task<RequestResponse> AddSubject(Subject req);
        Task<RequestResponse> UpdateSubject(Subject req);
        Task<RequestResponse> DeleteSubject(int subjectId);

        Task<ResultResponse<SubjectResponse>> GetSubjectByRole(int roleId, int userId,int campusId);
    }
}
