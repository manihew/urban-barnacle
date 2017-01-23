using Company1.Department1.Project1.Services.Authentication.Models;
using System;
using System.Threading.Tasks;

namespace Company1.Department1.Project1.Services.Authentication.Interfaces
{
    public interface IAuthenticationService
    {
        Task<AuthenticationResult> AuthenticateAsync(String appId, String appKey, String url, String username, String password, String auth);

        Task<TokenResult> GetTokenInfoAsync(String appId, String appKey, String url);

        Task UpdateExpiredTokenAsync(String appId, String appKey, String url);
    }
}
