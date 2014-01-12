using StackUnderflow.Common.BL;
using StackUnderflow.Models;
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
            var user = _userService.GetUser(User.Identity.Name);
            if (user != null)
            {
                ViewBag.User = new UserViewModel(user);
            }
            var questions = _userService.GetAllQuestions(0);
            return View(GetQuestionsViewModelCollection(questions));
        }

        [OutputCache(Duration = 240, VaryByParam = "id")]
        public ActionResult Details(int id)
        {
            var answers = _userService.GetAllAnswers(id, 0);
            return View(GetAnswersViewModelCollection(answers));
        }

        [HttpGet]
        public ViewResult Search(string query)
        {
            var questions = _userService.SearchForQuestions(query, 0);
            return View("Index", GetQuestionsViewModelCollection(questions));
        }

        [Authorize]
        public ViewResult SearchUser(string username)
        {
            var users = _userService.SearchForUsers(username, 0);
            return View("Index");
        }

        [Authorize]
        public ViewResult EditUser(int id)
        {
            var user = new UserViewModel(_userService.GetUser(id));
            if (user.isAdmin)
            {
                return View(user);
            }
            else
            {
                return View("Index");
            }
            
        }

        [Authorize]
        public ActionResult Ask()
        {
            return View();
        }

        [Authorize]
        [HttpPost]
        public ActionResult Ask(string title, string content)
        {
            if (_userService.CreateQuestion(User.Identity.Name, title, content))
            {
                var questions = _userService.GetAllQuestions(0);
                return View("Index", GetQuestionsViewModelCollection(questions));
            }
            else
            {
                return View();
            }
        }

        [Authorize]
        [HttpPost]
        public ActionResult Answer(int question_id, string content)
        {
            if (_userService.CreateAnswer(User.Identity.Name, question_id, content))
            {
                var answers = _userService.GetAllAnswers(question_id, 0);
                return View("Details", GetAnswersViewModelCollection(answers));
            }
            else
            {
                return View("Details");
            }
        }

        [Authorize]
        [HttpGet]
        public ActionResult Rate(int answer_id, Boolean positive)
        {
            if (positive)
            {
                _userService.RateUpAnswer(answer_id);
            }
            else
            {
                _userService.RateDownAnswer(answer_id);
            }
            var questions = _userService.GetAllQuestions(0);
            return View("Details", GetQuestionsViewModelCollection(questions));
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your app description page.";

            return View();
        }

        private static List<QuestionViewModel> GetQuestionsViewModelCollection(IQueryable<Common.DAL.Question> questions)
        {
            var viewmodel_questions = new List<QuestionViewModel>();
            foreach (var question in questions)
            {
                viewmodel_questions.Add(new QuestionViewModel(question));
            }
            return viewmodel_questions;
        }

        private static List<AnswerViewModel> GetAnswersViewModelCollection(IQueryable<Common.DAL.Answer> answers)
        {
            var viewmodel_answers = new List<AnswerViewModel>();
            foreach (var answer in answers)
            {
                viewmodel_answers.Add(new AnswerViewModel(answer));
            }
            return viewmodel_answers;
        }
    }
}
