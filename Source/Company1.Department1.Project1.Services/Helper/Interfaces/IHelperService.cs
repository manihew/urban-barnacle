using Company1.Department1.Project1.Services.Authentication.Models;
using System;
using System.Web;

namespace Company1.Department1.Project1.Services.Helper.Interfaces
{
    public interface IHelperService
    {
        String GetCurrentToken(HttpContext ctx, IAuthenticationConfig config);
        Boolean IsLogoutRequest(HttpContext ctx, IAuthenticationConfig config);
        void ProcessToken(HttpContext ctx, IAuthenticationConfig config, TokenResult authToken);
        void ProcessLogoutRequest(HttpContext ctx, IAuthenticationConfig config);
        void RedirectForAuthentication(HttpContext ctx, IAuthenticationConfig config);
    }
}
