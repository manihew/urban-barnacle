using Company1.Department1.Project1.Services.Authentication.Interfaces;
using Company1.Department1.Project1.Services.Dependency;
using Company1.Department1.Project1.Services.Helper.Interfaces;
using Microsoft.Practices.Unity;
using System;
using System.Threading.Tasks;
using System.Web;


namespace Company1.Department1.Project1.AuthenticationModule.Modules
{
    /// <summary>
    /// Auth Module
    /// </summary>
    public class AuthModule : IHttpModule
    {
        private IAuthenticationService _AuthenticationService;
        private IHelperService _HelperService;
        private IAuthenticationConfig _AuthenticationConfig;
        /// <summary>
        /// Constructor
        /// </summary>
        public AuthModule()
        {
            _AuthenticationService = DependencyService.DependencyContainer.Resolve<IAuthenticationService>();
            _HelperService = DependencyService.DependencyContainer.Resolve<IHelperService>();
            _AuthenticationConfig = DependencyService.DependencyContainer.Resolve<IAuthenticationConfig>();
        }
        /// <summary>
        /// IHttpModule.Init
        /// </summary>
        /// <param name="context"></param>
        public void Init(HttpApplication context)
        {
            var authTaskHandler = new EventHandlerTaskAsyncHelper(AuthenticateAsync);
            context.AddOnAuthenticateRequestAsync(authTaskHandler.BeginEventHandler, authTaskHandler.EndEventHandler);
        }
        /// <summary>
        /// IHttpModule.Dispose
        /// </summary>
        public void Dispose() { }
        /// <summary>
        /// Authenicate
        /// </summary>
        /// <param name="sender">HttpApplication</param>
        /// <param name="e"></param>
        /// <returns></returns>
        public async Task AuthenticateAsync(object sender, EventArgs e)
        {
            var app = (HttpApplication)sender;
            var ctx = app.Context;

            String token = _HelperService.GetCurrentToken(ctx, _AuthenticationConfig); ;
            if (token != null) // if token is available
            {
                if (_HelperService.IsLogoutRequest(ctx, _AuthenticationConfig)) // if user clicked the logout link
                {
                    // remove the token from the data store
                    await _AuthenticationService.UpdateExpiredTokenAsync(_AuthenticationConfig.AppId, _AuthenticationConfig.ApiKey, _AuthenticationConfig.GetTokenUrl(token));
                    // remove the token from token collection and redirect to homepage [which inturn redirect to Authentication website !]
                    _HelperService.ProcessLogoutRequest(ctx, _AuthenticationConfig);
                }
                else
                {
                    // check for token expiry.
                    var authToken = await _AuthenticationService.GetTokenInfoAsync(_AuthenticationConfig.AppId, _AuthenticationConfig.ApiKey, _AuthenticationConfig.GetTokenUrl(token));
                    // if the token expires [user logout from other website], we need to redirect for authentication.
                    _HelperService.ProcessToken(ctx, _AuthenticationConfig, authToken);
                }
            }
            else
            {
                // this is the first time login for this user.
                _HelperService.RedirectForAuthentication(ctx, _AuthenticationConfig);
            }
        }
    }
}
