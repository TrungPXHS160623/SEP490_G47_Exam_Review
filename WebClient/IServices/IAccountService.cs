using Library.Response;
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
    }
}
