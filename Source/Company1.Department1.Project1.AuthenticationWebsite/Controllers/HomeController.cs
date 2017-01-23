using Company1.Department1.Project1.AuthenticationWebsite.Models;
using Company1.Department1.Project1.Services.Authentication.Interfaces;
using Company1.Department1.Project1.Services.Dependency;
using Microsoft.Practices.Unity;
using System;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace Company1.Department1.Project1.AuthenticationWebsite.Controllers
{
    public class HomeController : Controller
    {
        private IAuthenticationService _AuthenticationService;

        public HomeController()
        {
            _AuthenticationService = DependencyService.DependencyContainer.Resolve<IAuthenticationService>();
        }

        // GET: /Home/Index
        public ActionResult Index(String returnUrl, String auth)
        {
            ViewBag.ReturnUrl = returnUrl;
            ViewBag.Auth = auth;
            return View();
        }

        // POST: /Home/Index
        [HttpPost]
        public async Task<ActionResult> Index(LogonViewModel model, String returnUrl, String auth)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var result = await _AuthenticationService.AuthenticateAsync(Config.AppId, Config.ApiKey, Config.AuthenticationServiceUrl, model.UserName, model.Password, auth);

            if (result.IsAuthenticated)
            {
                var url = HttpUtility.UrlDecode(returnUrl);

                return Redirect(url);
            }

            return View(model);
        }
    }
}