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
using System.Security.Cryptography;
using System.Text;

namespace StackUnderflow.Controllers
{
    public class AccountController : Controller
    {
        private IUserService _userService;
        public AccountController(IUserService UserService)
        {
            _userService = UserService;
        }

        //
        // GET: /Account/Login

        public ActionResult Login()
        {
            return View();
        }

        //
        // POST: /Account/Login

        [HttpPost]
        public ActionResult Login(LoginModel model, string returnUrl)
        {
            if (ModelState.IsValid)
            {
                if (_userService.LoginUser(model.Username, model.Password))
                {
                    FormsAuthentication.SetAuthCookie(model.Username, model.RememberMe);
                    
                    if (Url.IsLocalUrl(returnUrl) && returnUrl.Length > 1 && returnUrl.StartsWith("/")
                        && !returnUrl.StartsWith("//") && !returnUrl.StartsWith("/\\"))
                    {
                        return Redirect(returnUrl);
                    }
                    else
                    {
                        TempData["Info"] = "You have been succesfully logged in";
                        return RedirectToAction("Index", "Home");
                    }
                }
                else
                {
                    ModelState.AddModelError("", "The user name or password provided is incorrect.");
                }
            }

            // If we got this far, something failed, redisplay form
            return View(model);
        }

        //
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
                MD5 md5Hasher = MD5.Create();
                string md5 = BitConverter.ToString(md5Hasher.ComputeHash(Encoding.Default.GetBytes(model.Username+"1234"))).Replace("-", "");

                // Attempt to register the user
               if(_userService.CreateUser(model.Username, model.Password, model.Email))
               {
                    return RedirectToAction("Confirm", "Account", new { username = model.Username, m = md5 });
               } else {
                    ModelState.AddModelError("", "Unable to create user");
               }
            }

            // If we got this far, something failed, redisplay form
            return View(model);
        }

        //
        // GET: /Account/Logout

        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            TempData["Info"] = "You have been succesfully logged off";
            return RedirectToAction("Index", "Home");
        }

        // GET: /Account/Confirm
        public ActionResult Confirm(String username, String m) 
        {
            ViewBag.Username = username;
            ViewBag.M = m;
            return View();
        }

        // GET: /Account/ConfirmLink
        public ActionResult ConfirmLink(String username, String m)
        {
            MD5 md5Hasher = MD5.Create();
            string md5 = BitConverter.ToString(md5Hasher.ComputeHash(Encoding.Default.GetBytes(username + "1234"))).Replace("-", "");

            if (m.Equals(md5) && _userService.RegisterUser(username))
            {
                FormsAuthentication.SetAuthCookie(username, false);
                TempData["Info"] = "Registration completed";
                return RedirectToAction("Index", "Home");
            }
            else
            {
                TempData["Error"] = "Unable to confirm user. Contact administration";
                return RedirectToAction("Index", "Home");
            }

        }

        //
        // GET: /Account/ForgotPassword

        public ActionResult ForgotPassword()
        {
            return View();
        }

        //
        // POST: /Account/ForgotPassword

        [HttpPost]
        public ActionResult ForgotPassword(ForgotPasswordModel model)
        {
            if (ModelState.IsValid)
            {

                // ChangePassword will throw an exception rather
                // than return false in certain failure scenarios.
                
                if (_userService.ForgotPassword(model.Username, model.Email, model.NewPassword))
                {
                    // Passwort wurde geändert
                    TempData["Info"] = "Password has been changed";
                    return RedirectToAction("Login", "Account");
                }
                else
                {
                    ModelState.AddModelError("", "No user with this data has been found!");
                }
            }

            // If we got this far, something failed, redisplay form
            return View(model);
        }
    }
}
