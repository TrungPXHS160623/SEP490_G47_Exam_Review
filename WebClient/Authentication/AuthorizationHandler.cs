using Library.Common;
using System.Net.Http.Headers;
using WebClient.Services;

namespace WebClient.Authentication
{
    public class AuthorizationHandler : DelegatingHandler
    {
        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            var token = Constants.JWTToken; 

            if (!string.IsNullOrEmpty(token))
            {
                request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);
            }

            await Task.Delay(50);
            return await base.SendAsync(request, cancellationToken);
        }
    }
}
