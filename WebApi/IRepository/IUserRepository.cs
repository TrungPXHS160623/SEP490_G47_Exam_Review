using Library.Common;
using Library.Models;
using Library.Request;
using Library.Response;
using System.Security.Claims;

namespace WebApi.IRepository
{
    public interface IUserRepository
    {
        Task<RequestResponse> CreateAsync(UserRequest user);

        Task<ResultResponse<UserResponse>> GetAllAsync();

        Task<ResultResponse<UserResponse>> GetUserForAdmin(string filterQuery);

        Task<ResultResponse<UserResponse>> GetUserForExaminer(int userId, string filterQuery);

        Task<List<User>> GetAllWithFilterAsync(string? filterOn = null, string? filterQuery = null, string? sortBy = null, bool isAscending = true);

        Task<ResultResponse<UserRequest>> GetByIdAsync(int id);

        Task<ResultResponse<UserSubjectRequest>> GetUserSubjectByIdAsync(int id);

        Task<RequestResponse> UpdateAsync(UserRequest user);

        Task<RequestResponse> ExaminerUpdateUserAsync(UserSubjectRequest user);

        Task<RequestResponse> DeleteAsync(int id);

        Task<ResultResponse<UserResponse>> GetHeadOfDepartment(int subjectId, int campusId);

        Task<ResultResponse<UserResponse>> GetLecture();

        Task<RequestResponse> ImportUsersFromExcel(IFormFile file, ClaimsPrincipal currentUser);

    }
}
