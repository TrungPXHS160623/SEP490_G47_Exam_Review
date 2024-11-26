using Microsoft.AspNetCore.Components.Authorization;
using Library.Common;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using WebClient.Common;

namespace WebClient.Authentication
{
    public class CustomAuthenticationStateProvider : AuthenticationStateProvider
    {
        private readonly LocalStorageService _localStorageService;
        private readonly ClaimsPrincipal anonymous = new ClaimsPrincipal(new ClaimsIdentity());
        private const string JwtTokenKey = "authToken";

        public CustomAuthenticationStateProvider(LocalStorageService localStorageService)
        {
            _localStorageService = localStorageService;
        }

        public async override Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            var token = await _localStorageService.GetItemAsync(JwtTokenKey);
            if (string.IsNullOrEmpty(token))
            {
                return new AuthenticationState(anonymous);
            }

            var getUserClaims = DecryptToken(token);
            if (getUserClaims == null)
            {
                return new AuthenticationState(anonymous);
            }

            var isValid = await IsUserAuthenticated();
            if (!isValid)
            {
                return new AuthenticationState(anonymous);
            }

            var claimsPrincipal = SetClaimsPrincipal(getUserClaims);
            return new AuthenticationState(claimsPrincipal);
        }

        public async Task UpdateAuthenticationState(string jwtToken)
        {
            if (!string.IsNullOrEmpty(jwtToken))
            {
                await _localStorageService.SetItemAsync(JwtTokenKey, jwtToken);

                var getUserClaims = DecryptToken(jwtToken);
                var claimsPrincipal = SetClaimsPrincipal(getUserClaims);
                NotifyUserAuthentication(claimsPrincipal);
            }
            else
            {
                await _localStorageService.RemoveItemAsync(JwtTokenKey);
                NotifyUserAuthentication(anonymous);
            }
        }

        public async Task Logout()
        {
            await _localStorageService.RemoveItemAsync(JwtTokenKey);
            NotifyUserAuthentication(anonymous);
        }

        private void NotifyUserAuthentication(ClaimsPrincipal user)
        {
            var authState = Task.FromResult(new AuthenticationState(user));
            NotifyAuthenticationStateChanged(authState);
        }

        private static ClaimsPrincipal SetClaimsPrincipal(AccountClaims claims)
        {
            if (string.IsNullOrEmpty(claims.Email))
            {
                return new ClaimsPrincipal();
            }

            return new ClaimsPrincipal(new ClaimsIdentity(
                new List<Claim>
                {
                    new(ClaimTypes.Email, claims.Email),
                    new(ClaimTypes.NameIdentifier, claims.Id.ToString()),
                    new(ClaimTypes.Role, claims.RoleId.ToString()),
                    new("CampusId", claims.CampusId.ToString()),
                }, "JwtAuth"));
        }

        private static AccountClaims? DecryptToken(string jwtToken)
        {
            var handler = new JwtSecurityTokenHandler();
            var token = handler.ReadJwtToken(jwtToken);

            return new AccountClaims
            {
                Email = token.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Email)?.Value ?? string.Empty,
                Id = int.TryParse(token.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value, out var id) ? id : 0,
                RoleId = int.TryParse(token.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Role)?.Value, out var role) ? role : 0,
                CampusId = int.TryParse(token.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Role)?.Value, out var campus) ? role : 0,
            };
        }

        public async Task<bool> IsUserAuthenticated()
        {
            var token = await _localStorageService.GetItemAsync(JwtTokenKey);

            if (string.IsNullOrEmpty(token))
            {
                return false;
            }

            var handler = new JwtSecurityTokenHandler();
            try
            {
                var jwtToken = handler.ReadJwtToken(token);

                var expiry = jwtToken.ValidTo;
                if (expiry < DateTime.UtcNow)
                {
                    await _localStorageService.RemoveItemAsync(JwtTokenKey);
                    return false;
                }

                return true;
            }
            catch
            {
                await _localStorageService.RemoveItemAsync(JwtTokenKey);
                return false;
            }
        }
    }
}
