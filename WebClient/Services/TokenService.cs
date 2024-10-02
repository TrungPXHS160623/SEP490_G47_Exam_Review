using Library.Common;
using System.Net.Http.Headers;

namespace WebClient.Services
{
    public class TokenService
    {
        private readonly HttpClient _httpClient;

        public TokenService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public string Token { get; private set; }

        public void SetToken()
        {
            Token = Constants.JWTToken;

            // Update HttpClient with the new token
            if (!string.IsNullOrEmpty(Token))
            {
                _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Token);
            }
            else
            {
                _httpClient.DefaultRequestHeaders.Authorization = null;
            }
        }
    }
}
