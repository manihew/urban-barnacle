using Company1.Department1.Project1.AuthenticationService.Interfaces;
using NLog;
using System;
using System.Web.Security;

namespace Company1.Department1.Project1.AuthenticationService.Repositories
{
    /// <summary>
    /// LDAP repository
    /// </summary>
    public class LdapRepository : ILdapRepository
    {
        private static Logger _Logger = LogManager.GetCurrentClassLogger();
        /// <summary>
        /// Validate User
        /// </summary>
        /// <param name="username">username</param>
        /// <param name="password">password</param>
        /// <returns></returns>
        public bool ValidateUser(string username, string password)
        {
            try
            {
                return Membership.ValidateUser(username, password);
            }
            catch (Exception ex)
            {
                _Logger.Error(ex, "LDAP Error");
                return false;
            }
        }
    }
}