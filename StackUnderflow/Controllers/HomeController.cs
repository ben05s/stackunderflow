using StackUnderflow.Common.BL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;


namespace StackUnderflow.Controllers
{
    public class HomeController : Controller
    {
        private IUserService _userService;

        public HomeController(IUserService UserService)
        {
            _userService = UserService;
        }

        public ActionResult Index()
        {
            var a = _userService.GetAllUsers(0).Count();
            ViewBag.Message = "Modify this template to jump-start your ASP.NET MVC application." + a;

            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your app description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}
