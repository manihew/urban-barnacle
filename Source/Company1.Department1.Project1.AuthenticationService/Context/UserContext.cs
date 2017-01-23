using Company1.Department1.Project1.AuthenticationService.Models;
using System.Data.Entity;

namespace Company1.Department1.Project1.AuthenticationService.Context
{
    /// <summary>
    /// User Context
    /// </summary>
    public class UserContext : DbContext
    {
        /// <summary>
        /// Constructor
        /// </summary>
        public UserContext()
            : base("DefaultConnection")
        {
        }

        public DbSet<UserLog> UserLogs { get; set; }

        public DbSet<Application> Applications { get; set; }

        public DbSet<Token> Tokens { get; set; }

    }
}