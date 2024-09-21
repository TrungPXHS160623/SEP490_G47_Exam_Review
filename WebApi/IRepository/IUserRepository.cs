using Library.Common;
using Library.Models;

namespace WebApi.IRepository
{
    public interface IUserRepository
    {
        Task<User> CreateAsync(User user);

        Task<ResultResponse<User>> GetAllAsync();

        Task<List<User>> GetAllWithFilterAsync(string? filterOn = null, string? filterQuery = null);

        Task<List<User>> GetAllWithFilterAsync(string? filterOn = null, string? filterQuery = null, string? sortBy = null, bool isAscending = true);

        Task<User?> GetByIdAsync(int id);

        Task<User?> UpdateAsync(int id, User user);

        Task<User?> DeleteAsync(int id);



    }
}
