using Library.Common;
using Library.Models;

namespace WebApi.IRepository
{
    public interface ISubjectRepository
    {
        Task<ResultResponse<Subject>> GetSubjects();
    }
}
