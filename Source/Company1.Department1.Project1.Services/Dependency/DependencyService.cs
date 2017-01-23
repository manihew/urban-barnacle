using Company1.Department1.Project1.Services.Authentication;
using Company1.Department1.Project1.Services.Authentication.Interfaces;
using Company1.Department1.Project1.Services.Helper;
using Company1.Department1.Project1.Services.Helper.Interfaces;
using Company1.Department1.Project1.Services.Helper.Models;
using Microsoft.Practices.Unity;
using System.Configuration;

namespace Company1.Department1.Project1.Services.Dependency
{
    /// <summary>
    /// Dependency Service
    /// </summary>
    public static class DependencyService
    {
        public static IUnityContainer DependencyContainer { get; private set; }

        static DependencyService() { DependencyContainer = new UnityContainer(); Register(); }
        /// <summary>
        /// Register
        /// </summary>
        static void Register()
        {
            DependencyContainer.RegisterType<IAuthenticationService, AuthenticationService>();
            DependencyContainer.RegisterType<IHelperService, HelperService>();

            var config = (AuthenticationConfig)ConfigurationManager.GetSection("authenticationConfig");
            if (config != null)
            {
                DependencyContainer.RegisterInstance<IAuthenticationConfig>(config);
            }
        }
    }
}
