using Company1.Department1.Project1.AuthenticationService.Models;
using System;
using System.Web.Mvc;

namespace Company1.Department1.Project1.AuthenticationService.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            ViewBag.Title = "Home Page";

            return View();
        }
    }
}
