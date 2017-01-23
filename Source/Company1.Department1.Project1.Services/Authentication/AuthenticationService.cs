using Company1.Department1.Project1.Services.Authentication.Interfaces;
using Company1.Department1.Project1.Services.Authentication.Models;
using Company1.Department1.Project1.Services.HandShake;
using NLog;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace Company1.Department1.Project1.Services.Authentication
{
    /// <summary>
    /// Authentication Service
    /// </summary>
    public class AuthenticationService : IAuthenticationService
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();

        /// <summary>
        /// Authenticate
        /// </summary>
        /// <param name="appId"></param>
        /// <param name="appKey"></param>
        /// <param name="url"></param>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <param name="auth"></param>
        /// <returns></returns>
        public async Task<AuthenticationResult> AuthenticateAsync(String appId, String appKey, String url, String username, String password, String auth)
        {
            var client = HttpClientFactory.Create(new HandShakeService(appId, appKey));

            var response = await client.PostAsJsonAsync(url, new { UserName = username, Password = password, Token = auth });

            if (!response.IsSuccessStatusCode) { logger.Trace("Error"); }

            return await response.Content.ReadAsAsync<AuthenticationResult>();
        }

        /// <summary>
        /// Get Token
        /// </summary>
        /// <param name="appId"></param>
        /// <param name="appKey"></param>
        /// <param name="url"></param>
        /// <returns></returns>
        public async Task<TokenResult> GetTokenInfoAsync(String appId, String appKey, String url)
        {
            var client = HttpClientFactory.Create(new HandShakeService(appId, appKey));

            var response = await client.GetAsync(url);

            if (!response.IsSuccessStatusCode) { logger.Trace("Error"); }

            return await response.Content.ReadAsAsync<TokenResult>();
        }

        /// <summary>
        /// Delete Token
        /// </summary>
        /// <param name="appId"></param>
        /// <param name="appKey"></param>
        /// <param name="url"></param>
        /// <returns></returns>
        public async Task UpdateExpiredTokenAsync(String appId, String appKey, String url)
        {
            var client = HttpClientFactory.Create(new HandShakeService(appId, appKey));

            var response = await client.DeleteAsync(url);

            if (!response.IsSuccessStatusCode) { logger.Trace("Error"); }
        }
    }
}
