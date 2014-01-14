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
            UserViewModel userViewModel = null;
            if(user != null)
            {
                userViewModel = new UserViewModel(user);
            }
            var allUsers = _userService.GetAllUsers(0);
            var questions = _userService.GetAllQuestions(0);
            return View(new HomeViewModel(userViewModel, GetUsersViewModelCollection(allUsers), GetQuestionsViewModelCollection(questions)));
        }

        [OutputCache(Duration = 1, VaryByParam = "id")]
        public ActionResult Details(int id)
        {
            var user = _userService.GetUser(User.Identity.Name);
            UserViewModel userViewModel = null;
            if (user != null)
            {
                userViewModel = new UserViewModel(user);
            }
            var question = new QuestionViewModel(_userService.GetQuestion(id));
            var answers = _userService.GetAllAnswers(id, 0);
            return View(new DetailsViewModel(userViewModel, question, GetAnswersViewModelCollection(answers)));
        }

        public JsonResult QuestionAutocomplete(string query)
        {
            var result = _userService.SearchForQuestions(query, 0);
            var list = new List<string>();
            foreach(var item in result)
            {
                list.Add(item.title);
            }
            return Json(list, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ViewResult Search(string query)
        {
            var user = _userService.GetUser(User.Identity.Name);
            UserViewModel userViewModel = null;
            if (user != null)
            {
                userViewModel = new UserViewModel(user);
            }
            var allUsers = _userService.GetAllUsers(0);
            var questions = _userService.SearchForQuestions(query, 0);
            if (System.Linq.Enumerable.Count(questions) == 0)
            {
                questions = _userService.GetAllQuestions(0);
                TempData["Error"] = "No questions have been found";
            }
            return View("Index", new HomeViewModel(userViewModel, GetUsersViewModelCollection(allUsers), GetQuestionsViewModelCollection(questions)));
        }

        [Authorize]
        public ViewResult SearchUser(string username)
        {
            var user = _userService.GetUser(User.Identity.Name);
            UserViewModel userViewModel = null;
            if (user != null)
            {
                userViewModel = new UserViewModel(user);
            }
            var users = _userService.SearchForUsers(username, 0);
            var questions = _userService.GetAllQuestions(0);
            return View("Index", new HomeViewModel(userViewModel, GetUsersViewModelCollection(users), GetQuestionsViewModelCollection(questions)));
        }

        [Authorize]
        public ViewResult EditUser(int id)
        {
            var editUser = new UserViewModel(_userService.GetUser(id));
            if (editUser.isAdmin)
            {
                return View(editUser);
            }
            else
            {
                var user = _userService.GetUser(id);
                UserViewModel userViewModel = null;
                if (user != null)
                {
                    userViewModel = new UserViewModel(user);
                }
                return View(userViewModel);
            }
            
        }

        [Authorize]
        [HttpPost]
        public ViewResult EditUser(UserViewModel editUser)
        {
            Boolean registered = false;
            registered = editUser.registered;
            if (_userService.SaveUser(editUser.id, editUser.username, editUser.email, registered, editUser.isAdmin))
            {
                if (editUser.newpassword != null)
                {
                    if (!_userService.UpdatePassword(editUser.id, editUser.newpassword))
                    {
                        ModelState.AddModelError("", "Error when saving the password");
                        return View();
                    }
                }
                TempData["Info"] = "User has been saved";
            }
            else
            {
                ModelState.AddModelError("", "Error updating the user information");
                return View();
            }
            

            var user = _userService.GetUser(User.Identity.Name);
            UserViewModel userViewModel = null;
            if (user != null)
            {
                userViewModel = new UserViewModel(user);
            }
            var allUsers = _userService.GetAllUsers(0);
            var questions = _userService.GetAllQuestions(0);
            return View("Index", new HomeViewModel(userViewModel, GetUsersViewModelCollection(allUsers), GetQuestionsViewModelCollection(questions)));
       
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
            if (title.Length > 0 && _userService.CreateQuestion(User.Identity.Name, title, content))
            {
                var user = _userService.GetUser(User.Identity.Name);
                UserViewModel userViewModel = null;
                if (user != null)
                {
                    userViewModel = new UserViewModel(user);
                }
                var allUsers = _userService.GetAllUsers(0);
                var questions = _userService.GetAllQuestions(0);
                TempData["Info"] = "Question has been added";
                return View("Index", new HomeViewModel(userViewModel, GetUsersViewModelCollection(allUsers), GetQuestionsViewModelCollection(questions)));
            }
            else
            {
                TempData["Error"] = "Error when saving the question";
                return View();
            }
        }

        [Authorize]
        [HttpPost]
        public ActionResult Answer(int question_id, string content)
        {
            var user = _userService.GetUser(User.Identity.Name);
            UserViewModel userViewModel = null;
            if (user != null)
            {
                userViewModel = new UserViewModel(user);
            }
            var question = new QuestionViewModel(_userService.GetQuestion(question_id));
            var answers = _userService.GetAllAnswers(question_id, 0);

            if (content.Length > 0 && _userService.CreateAnswer(User.Identity.Name, question_id, content))
            {
                answers = _userService.GetAllAnswers(question_id, 0);
                TempData["Info"] = "Answer has been added";
                return View("Details", new DetailsViewModel(userViewModel, question, GetAnswersViewModelCollection(answers)));
            }
            else
            {
                TempData["Error"] = "Error when saving the answer";
                return View("Details", new DetailsViewModel(userViewModel, question, GetAnswersViewModelCollection(answers)));
            }
        }

        [Authorize]
        [HttpGet]
        public ActionResult Rate(int answer_id, int question_id, Boolean positive)
        {
            if (positive)
            {
                _userService.RateUpAnswer(answer_id);
            }
            else
            {
                _userService.RateDownAnswer(answer_id);
            }
            var user = _userService.GetUser(User.Identity.Name);
            UserViewModel userViewModel = null;
            if (user != null)
            {
                userViewModel = new UserViewModel(user);
            }
            var question = new QuestionViewModel(_userService.GetQuestion(question_id));
            var answers = _userService.GetAllAnswers(question_id, 0);
            return View("Details", new DetailsViewModel(userViewModel, question, GetAnswersViewModelCollection(answers)));
        }

        public ActionResult About()
        {
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

        private static List<UserViewModel> GetUsersViewModelCollection(IQueryable<Common.DAL.User> users)
        {
            var viewmodel_users = new List<UserViewModel>();
            foreach (var user in users)
            {
                viewmodel_users.Add(new UserViewModel(user));
            }
            return viewmodel_users;
        }
    }
}
