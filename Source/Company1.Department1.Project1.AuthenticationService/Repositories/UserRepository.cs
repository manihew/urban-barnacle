using Company1.Department1.Project1.AuthenticationService.Context;
using Company1.Department1.Project1.AuthenticationService.Interfaces;
using Company1.Department1.Project1.AuthenticationService.Models;
using System.Threading.Tasks;

namespace Company1.Department1.Project1.AuthenticationService.Repositories
{
    /// <summary>
    /// User Repository
    /// </summary>
    public class UserRepository : IUserRepository
    {
        /// <summary>
        /// Save Authentication
        /// </summary>
        /// <param name="user">user credentials</param>
        /// <returns></returns>
        public async Task SaveAuthenticationAsync(User user)
        {
            using (var db = new UserContext())
            {
                var log = await db.UserLogs.FindAsync(user.UserName);

                if (log == null)
                {
                    db.UserLogs.Add(new UserLog() { UserName = user.UserName, LatestAppId = user.Token });
                }
                else
                {
                    log.LatestAppId = user.Token;
                }

                await db.SaveChangesAsync();
            }
        }
    }
}