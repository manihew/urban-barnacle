using Company1.Department1.Project1.AuthenticationService.Filters;
using Company1.Department1.Project1.AuthenticationService.Interfaces;
using Company1.Department1.Project1.AuthenticationService.Models;
using Company1.Department1.Project1.AuthenticationService.Services;
using Microsoft.Practices.Unity;
using NLog;
using System;
using System.Threading.Tasks;
using System.Web.Http;

namespace Company1.Department1.Project1.AuthenticationService.Controllers
{
    /// <summary>
    ///  Token Controller
    /// </summary>
    public class TokenController : ApiController
    {
        private ITokenRepository _TokenRepository;
        private Logger _Logger;
        /// <summary>
        /// Constructor
        /// </summary>
        public TokenController()
        {
            _TokenRepository = DependencyService.DependencyContainer.Resolve<ITokenRepository>();
            _Logger = LogManager.GetCurrentClassLogger();
        }

        /// <summary>
        /// Get Request
        /// </summary>
        /// <param name="id">token id</param>
        /// <returns></returns>
        [HandShake]
        // GET api/Token/{id}
        public async Task<TokenResult> Get(String id)
        {
            _Logger.Debug("Token:  {0} get request", id);
            var token = await _TokenRepository.GetTokenAsync(id);
            _Logger.Debug<TokenResult>("Token Result : {1}", (TokenResult)token);
            return (TokenResult)token;
        }

        /// <summary>
        /// Delete request
        /// </summary>
        /// <param name="id">token id</param>
        /// <returns></returns>
        [HandShake]
        // DELETE api/Token/{id}
        public async Task Delete(String id)
        {
            _Logger.Debug("Token:  {0} delete request", id);
            await _TokenRepository.DeleteTokenAsync(id);
        }
    }
}
