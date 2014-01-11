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
            var questions = _userService.GetAllQuestions(0);

            return View(questions);
        }

        [HttpGet]
        public ActionResult Search(string query)
        {
            var questions = _userService.SearchForQuestions(query, 0);

            return View(questions);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your app description page.";

            return View();
        }
    }
}
