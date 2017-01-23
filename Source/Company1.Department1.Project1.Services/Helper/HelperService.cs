using Company1.Department1.Project1.Services.Authentication.Models;
using Company1.Department1.Project1.Services.HandShake.Models;
using Company1.Department1.Project1.Services.Helper.DataAccess;
using Company1.Department1.Project1.Services.Helper.Interfaces;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Security.Claims;
using System.Web;

namespace Company1.Department1.Project1.Services.Helper
{
    /// <summary>
    /// Helper Service
    /// </summary>
    public class HelperService : IHelperService
    {
        /// <summary>
        ///  Get current token
        /// </summary>
        /// <param name="ctx"></param>
        /// <param name="config"></param>
        /// <returns></returns>
        public String GetCurrentToken(HttpContext ctx, IAuthenticationConfig config)
        {
            String token;
            SingletonData.Instance.TokenCollection.TryGetValue(GetKey(ctx.Request.ServerVariables, config), out token);
            return token;
        }

        /// <summary>
        /// Is Logout Request
        /// </summary>
        /// <param name="ctx"></param>
        /// <param name="config"></param>
        /// <returns></returns>
        public Boolean IsLogoutRequest(HttpContext ctx, IAuthenticationConfig config)
        {
            return ctx.Request.Url.Segments.Any(x => String.Compare(config.LogoutAction, x, StringComparison.OrdinalIgnoreCase) == 0);
        }

        /// <summary>
        /// Process Token
        /// </summary>
        /// <param name="ctx"></param>
        /// <param name="config"></param>
        /// <param name="authToken"></param>
        public void ProcessToken(HttpContext ctx, IAuthenticationConfig config, TokenResult authToken)
        {
            if (authToken == null || authToken.UserName == null)
            {
                Redirect(ctx, config, config.HomePageUrl);
            }
            else
            {
                ctx.User = (ClaimsPrincipal)authToken;
            }
        }

        /// <summary>
        /// Process Logout request
        /// </summary>
        /// <param name="ctx"></param>
        /// <param name="config"></param>
        public void ProcessLogoutRequest(HttpContext ctx, IAuthenticationConfig config)
        {
            SingletonData.Instance.TokenCollection.Remove(GetKey(ctx.Request.ServerVariables, config));

            ctx.Response.Redirect(config.HomePageUrl);
        }

        /// <summary>
        /// redirect for authentication
        /// </summary>
        /// <param name="ctx"></param>
        /// <param name="config"></param>
        public void RedirectForAuthentication(HttpContext ctx, IAuthenticationConfig config)
        {
            Redirect(ctx, config, config.LoginUrl);
        }

        /// <summary>
        /// Get Key
        /// </summary>
        /// <param name="variables"></param>
        /// <param name="config"></param>
        /// <returns></returns>
        Tuple<String, String, String> GetKey(NameValueCollection variables, IAuthenticationConfig config)
        {
            return Tuple.Create(config.AppId, variables["REMOTE_ADDR"], variables["HTTP_USER_AGENT"]);
        }

        /// <summary>
        /// Redirect
        /// </summary>
        /// <param name="ctx"></param>
        /// <param name="config"></param>
        /// <param name="url"></param>
        void Redirect(HttpContext ctx, IAuthenticationConfig config, String url)
        {
            var token = Util.NewAppId;

            var qryString = new Dictionary<String, String>() { { "ReturnUrl", ctx.Request.Url.ToString() }, { "AUTH", token } };

            var redirectUrl = Util.AppendQuery(url, qryString);

            SingletonData.Instance.TokenCollection[GetKey(ctx.Request.ServerVariables, config)] = token;

            ctx.Response.Redirect(redirectUrl, true);
        }
    }
}
