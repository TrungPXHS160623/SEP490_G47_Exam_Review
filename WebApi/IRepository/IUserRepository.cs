using Library.Common;
using Library.Models;
using Library.Request;
using Library.Response;

namespace WebApi.IRepository
{
    public interface IUserRepository
    {
        Task<RequestResponse> CreateAsync(UserRequest user);

        Task<ResultResponse<UserResponse>> GetAllAsync();

        Task<ResultResponse<UserResponse>> GetAllWithFilterAsync(string filterQuery);

        Task<List<User>> GetAllWithFilterAsync(string? filterOn = null, string? filterQuery = null, string? sortBy = null, bool isAscending = true);

        Task<ResultResponse<UserRequest>> GetByIdAsync(int id);

        Task<RequestResponse> UpdateAsync(UserRequest user);

        Task<RequestResponse> DeleteAsync(int id);

        Task<ResultResponse<UserResponse>> GetHeadOfDepartment(int subjectId, int campusId);

        Task<ResultResponse<UserResponse>> GetLecture();

    }
}
