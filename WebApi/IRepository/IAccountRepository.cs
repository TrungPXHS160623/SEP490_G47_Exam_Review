using Library.Response;
using Library.Common;
using Library.Models;
using Library.Request;

namespace WebApi.IRepository
{
    public interface IAccountRepository
    {
        Task<AuthenticationResponse> UserLogin(UserRequest request);

        Task<RequestResponse> UserRegister(UserRegisterRequest request);

        Task<ResultResponse<Account>> GetUserList();

        Task<AuthenticationResponse> GoogleLoginCallback(string email);
    }
}
