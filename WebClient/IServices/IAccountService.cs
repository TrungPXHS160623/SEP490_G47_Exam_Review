using Library.Response;
using PRN231_Library.Common;
using PRN231_Library.Models;
using PRN231_Library.Request;

namespace WebClient.IServices
{
    public interface IAccountService
    {
        Task<AuthenticationResponse> LoginUserAsync(UserRequest request);

        Task<RequestResponse> RegisterUserAsync(UserRegisterRequest request);

        Task<ResultResponse<Account>> GetUserList();
    }
}
