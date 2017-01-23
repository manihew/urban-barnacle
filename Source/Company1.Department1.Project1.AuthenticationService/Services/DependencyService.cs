using Company1.Department1.Project1.AuthenticationService.Interfaces;
using Company1.Department1.Project1.AuthenticationService.Repositories;
using Microsoft.Practices.Unity;

namespace Company1.Department1.Project1.AuthenticationService.Services
{
    /// <summary>
    /// Dependency Injection Service using Unity
    /// </summary>
    public class DependencyService
    {
        public static IUnityContainer DependencyContainer { get; private set; }

        /// <summary>
        /// Static constructor to load dependencies
        /// </summary>
        static DependencyService()
        {
            DependencyContainer = new UnityContainer();
            Register();
        }
        
        /// <summary>
        /// Register all dependencies for the Authenication Service.
        /// </summary>
        static void Register()
        {
            DependencyContainer.RegisterType<IUserRepository, UserRepository>();
            DependencyContainer.RegisterType<ILdapRepository, LdapRepository>();
            DependencyContainer.RegisterType<ITokenRepository, TokenRepository>();
        }
    }
}