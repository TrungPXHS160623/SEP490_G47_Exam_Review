using Library.Common;
using Library.Models;

namespace WebClient.IServices
{
    public interface ISubjectService
    {
        Task<ResultResponse<Subject>> GetSubjects();
    }
}
