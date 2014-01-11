using System;
using System.Collections.Generic;
using System.Linq;
using System.Transactions;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using WebMatrix.WebData;
using StackUnderflow.Filters;
using StackUnderflow.Models;
using StackUnderflow.Common.BL;

namespace StackUnderflow.Controllers
{
    public class AccountController : Controller
    {
        private IUserService _userService;
        public AccountController(IUserService UserService)
        {
            _userService = UserService;
        }

        // GET: /Account/Register

        public ActionResult Register()
        {
            return View();
        }

        //
        // POST: /Account/Register

        [HttpPost]
        public ActionResult Register(RegisterModel model)
        {
            if (ModelState.IsValid)
            {
                // Attempt to register the user
               if(_userService.CreateUser(model.Username, model.Password, model.Email))
               {
                    FormsAuthentication.SetAuthCookie(model.Username, false);
                    return RedirectToAction("Index", "Home");
               } else {
                    ModelState.AddModelError("", "Unable to create user");
               }
            }

            // If we got this far, something failed, redisplay form
            return View(model);
        }


    }
}
