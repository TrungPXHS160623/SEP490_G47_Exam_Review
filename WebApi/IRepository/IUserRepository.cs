using Library.Common;
using Library.Models;

namespace WebApi.IRepository
{
    public interface IUserRepository
    {
        Task<RequestResponse> CreateAsync(User user);

        Task<ResultResponse<User>> GetAllAsync();

        Task<ResultResponse<User>> GetAllWithFilterAsync(string? filterOn = null, string? filterQuery = null);

        Task<List<User>> GetAllWithFilterAsync(string? filterOn = null, string? filterQuery = null, string? sortBy = null, bool isAscending = true);

        Task<ResultResponse<User>> GetByIdAsync(int id);

        Task<RequestResponse> UpdateAsync(User user);

        Task<RequestResponse> DeleteAsync(int id);



    }
}
