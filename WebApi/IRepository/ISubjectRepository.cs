using Library.Common;
using Library.Models;

namespace WebApi.IRepository
{
    public interface ISubjectRepository
    {
        Task<ResultResponse<Subject>> GetSubjects();
        Task<ResultResponse<Subject>> GetSubjectById(int subjectId);
        Task<RequestResponse> AddSubject(Subject req);
        Task<RequestResponse> UpdateSubject(Subject req);
        Task<RequestResponse> DeleteSubject(int subjectId);
    }
}
