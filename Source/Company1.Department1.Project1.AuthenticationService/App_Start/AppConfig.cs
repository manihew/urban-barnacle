using Company1.Department1.Project1.AuthenticationService.Context;
using System.Linq;

namespace Company1.Department1.Project1.AuthenticationService.App_Start
{
    /// <summary>
    /// App Config
    /// </summary>
    public class AppConfig
    {
        /// <summary>
        /// Register all app id's and api key's
        /// </summary>
        public static void RegisterApplications()
        {
            using (var db = new UserContext())
            {
                SingletonContext.Instance.Applications = db.Applications.ToList().ToDictionary(x => x.AppId, x => x.AppKey);
            }
        }
    }
}