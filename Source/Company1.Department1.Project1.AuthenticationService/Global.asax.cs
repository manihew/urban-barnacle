using Company1.Department1.Project1.AuthenticationService.App_Start;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Routing;

namespace Company1.Department1.Project1.AuthenticationService
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            //AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            AppConfig.RegisterApplications();
            //BundleConfig.RegisterBundles(BundleTable.Bundles);
        }
    }
}
