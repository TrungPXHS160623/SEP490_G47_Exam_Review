using Library.Common;
using Library.Models;
using Library.Request;

namespace WebClient.IServices
{
    public interface IAccountService
    {
        Task<AuthenticationResponse> LoginUserAsync(UserRequest request);

        Task<RequestResponse> RegisterUserAsync(UserRegisterRequest request);

        Task<ResultResponse<Account>> GetUserList();
        Task<ResultResponse<User>> GetAllUserList();
        Task<ResultResponse<User>> GetAllWithFilterAsync(string? filterOn = null, string? filterQuery = null);
        Task<AuthenticationResponse> GetJWT();
        Task<RequestResponse> ClearJWT();
        Task<ResultResponse<User>> GetByIdAsync(int id);
        Task<RequestResponse> UpdateAsync(User user);
        Task<RequestResponse> DeleteAsync(int id);
        Task<RequestResponse> CreateAsync(User user);
    }
}
