using Library.Common;
using Library.Models;

namespace WebClient.IServices
{
    public interface IFacultyService
    {
        Task<ResultResponse<Faculty>> GetFaculties();
        Task<ResultResponse<Faculty>> GetFacutiesByUserID(int? userId);
    }
}
