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

        Task<AuthenticationResponse> GetJWT();
        Task<RequestResponse> ClearJWT();

    }
}
