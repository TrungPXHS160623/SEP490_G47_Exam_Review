using Library.Common;
using Library.Models;
using Library.Request;
using Library.Response;

namespace WebClient.IServices
{
    public interface IAccountService
    {
        Task<AuthenticationResponse> LoginUserAsync(UserRequest request);

        Task<RequestResponse> RegisterUserAsync(UserRegisterRequest request);

        Task<ResultResponse<UserResponse>> GetAllUserList();
        Task<ResultResponse<UserResponse>> GetAllWithFilterAsync(string filterQuery);
        Task<AuthenticationResponse> GetJWT();
        Task<RequestResponse> ClearJWT();
        Task<ResultResponse<UserRequest>> GetByIdAsync(int id);
        Task<RequestResponse> UpdateAsync(UserRequest user);
        Task<RequestResponse> DeleteAsync(int id);
        Task<RequestResponse> CreateAsync(UserRequest user);
    }
}
