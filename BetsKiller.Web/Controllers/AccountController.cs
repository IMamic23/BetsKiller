using BetsKiller.Helper.Constants;
using BetsKiller.ViewModel.Account;
using BotDetect.Web;
using BotDetect.Web.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using System.Web.UI;
using WebMatrix.WebData;

namespace BetsKiller.Web.Controllers
{
    public class AccountController : Controller
    {
        #region Login

        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(LoginViewModel login)
        {
            if (ModelState.IsValid)
            {
                if (WebSecurity.Login(login.Email, login.Password, login.RememberMe))
                {
                    Request.Cookies[0].Expires = DateTime.Now.AddDays(30);
                    string returnUrl = Request.QueryString["ReturnUrl"];
                    if (returnUrl == null)
                    {
                        return RedirectToAction("Index", "Dashboard");
                    }
                    else
                    {
                        return RedirectToAction(returnUrl);
                    }
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Login failed!");
                }
            }
            else
            {
                ModelState.AddModelError(string.Empty, "Invalid data.");
            }

            return View();
        }

        #endregion

        #region Register

        [HttpGet]
        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [CaptchaValidation("CaptchaCode", "RegistrationCaptcha", "Incorrect CAPTCHA Code!")]
        public ActionResult Register(RegisterViewModel register)
        {
            if (ModelState.IsValid)
            {
                WebSecurity.CreateUserAndAccount(register.Email, register.Password, new { FullName = register.FullName, RoleActiveFrom = DateTime.Now.Date });
                Roles.AddUserToRole(register.Email, RolesConst.Free);

                if (WebSecurity.Login(register.Email, register.Password, false))
                {
                    MvcCaptcha.ResetCaptcha("RegistrationCaptcha");
                    return RedirectToAction("Index", "Dashboard");
                }
                else
                {
                    return RedirectToAction("Login", "Account");
                }
            }

            return View();
        }

        [OutputCache(Location = OutputCacheLocation.None, NoStore = true)]
        public JsonResult CheckCaptcha(string captchaId, string instanceId, string userInput)
        {
            bool ajaxValidationResult = Captcha.AjaxValidate(captchaId, userInput, instanceId);
            return Json(ajaxValidationResult, JsonRequestBehavior.AllowGet);
        }

        #endregion

        #region Logout

        [Authorize]
        public ActionResult Logout()
        {
            WebSecurity.Logout();
            return RedirectToAction("Index", "Home");
        }

        #endregion

        #region Manage

        [Authorize]
        [HttpPost]
        public ActionResult Manage(ManageViewModel manageViewModel)
        {
            if (ModelState.IsValid)
            {
                string currentUserName = WebSecurity.CurrentUserName;
                if (WebSecurity.ChangePassword(currentUserName, manageViewModel.OldPassword, manageViewModel.NewPassword))
                {
                    WebSecurity.Login(currentUserName, manageViewModel.NewPassword, false);

                    return RedirectToAction("Index", "Dashboard");
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Change password failed!");
                }
            }
            else
            {
                ModelState.AddModelError(string.Empty, "Invalid data.");
            }

            return View(manageViewModel);
        }

        #endregion

        #region Helper

        public static MvcCaptcha GetRegistrationCaptcha()
        {
            // create the control instance
            MvcCaptcha registrationCaptcha = new MvcCaptcha("RegistrationCaptcha");

            // set up client-side processing of the Captcha code input textbox
            registrationCaptcha.UserInputID = "CaptchaCode";

            // Captcha settings
            registrationCaptcha.ImageSize = new System.Drawing.Size(255, 50);

            return registrationCaptcha;
        }

        #endregion
    }
}