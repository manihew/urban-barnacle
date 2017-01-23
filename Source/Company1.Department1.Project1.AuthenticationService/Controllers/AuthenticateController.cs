using Company1.Department1.Project1.AuthenticationService.Filters;
using Company1.Department1.Project1.AuthenticationService.Interfaces;
using Company1.Department1.Project1.AuthenticationService.Models;
using Company1.Department1.Project1.AuthenticationService.Services;
using Microsoft.Practices.Unity;
using NLog;
using System.Threading.Tasks;
using System.Web.Http;


namespace Company1.Department1.Project1.AuthenticationService.Controllers
{
    /// <summary>
    /// Authenticate Controller
    /// </summary>
    public class AuthenticateController : ApiController
    {
        private IUserRepository _UserRepository;
        private ILdapRepository _LdapRepository;
        private ITokenRepository _TokenRepository;
        private Logger _Logger;
        /// <summary>
        /// Constructor
        /// </summary>
        public AuthenticateController()
        {
            _UserRepository = DependencyService.DependencyContainer.Resolve<IUserRepository>();
            _LdapRepository = DependencyService.DependencyContainer.Resolve<ILdapRepository>();
            _TokenRepository = DependencyService.DependencyContainer.Resolve<ITokenRepository>();
            _Logger = LogManager.GetCurrentClassLogger();
        }

        /// <summary>
        /// POST request for authentication
        /// </summary>
        /// <param name="user">user credentials</param>
        /// <returns>authentication response</returns>
        [HandShake]
        // POST api/Authenticate
        public async Task<AuthenticationResult> Post([FromBody]User user)
        {
            _Logger.Debug("User:  {0} [token : {1}] tring to authenticate", user.UserName, user.Token);

            var isAuthenticated = _LdapRepository.ValidateUser(user.UserName, user.Password);

            if (isAuthenticated)
            {
                await _TokenRepository.SaveTokenAsync(user);

                await _UserRepository.SaveAuthenticationAsync(user);
            }

            return new AuthenticationResult() { IsAuthenticated = isAuthenticated, Token = user.Token };
        }
    }
}
