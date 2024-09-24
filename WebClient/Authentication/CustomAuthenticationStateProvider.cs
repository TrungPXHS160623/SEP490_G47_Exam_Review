using Microsoft.AspNetCore.Components.Authorization;
using Library.Common;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Xml.Serialization;
using WebClient.Components.Pages.Lecturer;

namespace WebClient.Authentication
{
    public class CustomAuthenticationStateProvider : AuthenticationStateProvider
    {
        private readonly ClaimsPrincipal anonymous = new ClaimsPrincipal(new ClaimsIdentity());

        public async override Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            try
            {
                if (string.IsNullOrEmpty(Constants.JWTToken))
                {
                    return await Task.FromResult(new AuthenticationState(anonymous));
                }
                
                var getUserClaims = DecryptToken(Constants.JWTToken);
                if (getUserClaims == null)
                {

                    return await Task.FromResult(new AuthenticationState(anonymous));
                }

                var claimsPrincipal = SetClaimsPrincipal(getUserClaims);

                return await Task.FromResult(new AuthenticationState(claimsPrincipal));
            }
            catch
            {
                return await Task.FromResult(new AuthenticationState(anonymous));
            }
        }

        public void NotifyUserAuthentication(ClaimsPrincipal user)
        {
            var authState = Task.FromResult(new AuthenticationState(user));
            NotifyAuthenticationStateChanged(authState);
        }

        public void NotifyUserLogout()
        {
            Constants.JWTToken = string.Empty;
            var authState = Task.FromResult(new AuthenticationState(new ClaimsPrincipal(new ClaimsIdentity())));
            NotifyAuthenticationStateChanged(authState);
        }

        public static ClaimsPrincipal SetClaimsPrincipal(AccountClaims claims)
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
                    new(ClaimTypes.Role, claims.RoleId.ToString()!),
                }, "JwtAuth"));
        }

        private static AccountClaims? DecryptToken(string jwtToken)
        {
            if (string.IsNullOrEmpty(jwtToken))
            {
                return null;
            }

            var handler = new JwtSecurityTokenHandler();
            var token = handler.ReadJwtToken(jwtToken);

            var Email = token.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Email)?.Value ?? string.Empty;
            var Id = int.TryParse(token.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value, out var id1) ? id1 : 0;
            var RoleId = int.TryParse(token.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Role)?.Value, out var role1) ? role1 : 0;

            return new AccountClaims
            {
                Email = token.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Email)?.Value ?? string.Empty,
                Id = int.TryParse(token.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value, out var id) ? id : 0,
                RoleId = int.TryParse(token.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Role)?.Value, out var role) ? role : 0
            };
        }

        public async Task UpdateAuthenticationState(string jwtToken)
        {
            var claimsPrincipal = new ClaimsPrincipal();
            if (!string.IsNullOrEmpty(jwtToken))
            {
                Constants.JWTToken = jwtToken;
                var getUserClaims = DecryptToken(jwtToken);
                claimsPrincipal = SetClaimsPrincipal(getUserClaims);
            }
            else
            {
                Constants.JWTToken = string.Empty;
                claimsPrincipal = anonymous;
            }
            NotifyUserAuthentication(claimsPrincipal);
        }

        public async Task<bool> IsUserAuthenticated()
        {
            var authState = await GetAuthenticationStateAsync();
            return authState.User.Identity != null && authState.User.Identity.IsAuthenticated;
        }
    }
}
