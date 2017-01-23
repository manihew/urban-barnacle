using Company1.Department1.Project1.AuthenticationService.Context;
using Company1.Department1.Project1.AuthenticationService.Interfaces;
using Company1.Department1.Project1.AuthenticationService.Models;
using System;
using System.Threading.Tasks;

namespace Company1.Department1.Project1.AuthenticationService.Repositories
{
    /// <summary>
    /// Token Repository
    /// </summary>
    public class TokenRepository : ITokenRepository
    {
        /// <summary>
        /// Save Token
        /// </summary>
        /// <param name="user">user credentials</param>
        /// <returns></returns>
        public async Task SaveTokenAsync(User user)
        {
            using (var db = new UserContext())
            {
                var token = new Token() { AuthToken = user.Token, CreatedOn = DateTime.UtcNow, UserName = user.UserName };
                db.Tokens.Add(token);
                await db.SaveChangesAsync();
            }
        }

        /// <summary>
        /// Get Token
        /// </summary>
        /// <param name="id">token id</param>
        /// <returns>valid token</returns>
        public async Task<Token> GetTokenAsync(String id)
        {
            using (var db = new UserContext())
            {
                var token = await db.Tokens.FindAsync(id);

                if (token != null)
                {
                    if (await db.UserLogs.FindAsync(token.UserName) == null)
                    {
                        token = null;
                    }
                }

                return token;
            }
        }

        public async Task DeleteTokenAsync(String id)
        {
            using (var db = new UserContext())
            {
                var token = await db.Tokens.FindAsync(id);

                if (token != null)
                {
                    var log = await db.UserLogs.FindAsync(token.UserName);

                    if (log != null)
                    {
                        db.UserLogs.Remove(log);
                    }

                    db.Tokens.Remove(token);

                    await db.SaveChangesAsync();
                }
            }
        }
    }
}