using BetsKiller.BL.Account;
using BetsKiller.BL.UserManagement;
using BetsKiller.Helper.Constants;
using BetsKiller.ViewModel.Account;
using BotDetect.Web;
using BotDetect.Web.Mvc;
using System;
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
                if (!WebSecurity.UserExists(login.Email))
                {
                    ModelState.AddModelError(string.Empty, "User does not exist!");
                }
                else if (!WebSecurity.IsConfirmed(login.Email))
                {
                    ModelState.AddModelError(string.Empty, "User is not verified by email verification link! Take a look in your Spam/Trash/Junk folder just in case the confirmation email got delivered there instead of your inbox. If you continue to have a problem, please contact us on info@betskillers.com!");
                }
                else if (WebSecurity.Login(login.Email, login.Password, login.RememberMe))
                {
                    // Save login history
                    AddUserActionHistory addUserActionHistory = new AddUserActionHistory(WebSecurity.GetUserId(login.Email), AddUserActionHistory.ActionType.LOGIN);
                    addUserActionHistory.Start();

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
                string confirmationToken = WebSecurity.CreateUserAndAccount(register.Email, register.Password, new { FullName = register.FullName, RoleActiveFrom = DateTime.Now.Date }, true);
                
                Roles.AddUserToRole(register.Email, RolesConst.Free);
                MvcCaptcha.ResetCaptcha("RegistrationCaptcha");

                SendMailConfirmation sendMail = new SendMailConfirmation(confirmationToken, register.Email);
                sendMail.Start();

                AddUserActionHistory addUserActionHistory = new AddUserActionHistory(WebSecurity.GetUserId(register.Email), AddUserActionHistory.ActionType.REGISTER);
                addUserActionHistory.Start();

                return RedirectToAction("ConfirmationSent");
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

            AddUserActionHistory addUserActionHistory = new AddUserActionHistory(WebSecurity.CurrentUserId, AddUserActionHistory.ActionType.LOGOUT);
            addUserActionHistory.Start();

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
                    AddUserActionHistory addUserActionHistory = new AddUserActionHistory(WebSecurity.CurrentUserId, AddUserActionHistory.ActionType.CHANGED_PASSWORD);
                    addUserActionHistory.Start();

                    WebSecurity.Login(currentUserName, manageViewModel.NewPassword, false);

                    addUserActionHistory = new AddUserActionHistory(WebSecurity.CurrentUserId, AddUserActionHistory.ActionType.LOGIN);
                    addUserActionHistory.Start();

                    return Json(new { success = true, message = "Password changed successfully!" });
                }
                else
                {
                    return Json(new { success = false, message = "Change password failed!" });
                }
            }
            else
            {
                return Json(new { success = false, message = "Invalid data." });
            }
        }

        #endregion

        #region UserProfilePreview

        [Authorize]
        [HttpGet]
        public ActionResult UserProfilePreview()
        {
            GetUserProfilePreview getUserProfilePreview = new GetUserProfilePreview(User.Identity.Name);
            getUserProfilePreview.Start();

            return PartialView("_UserProfilePreviewPartial", getUserProfilePreview.UserProfilePreviewViewModel);
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

        #region Email confirmation

        [HttpGet]
        public ActionResult ConfirmationSent()
        {
            return View();
        }

        [HttpGet]
        public ActionResult Confirmed(string mail, string token)
        {
            if (WebSecurity.ConfirmAccount(mail, token))
            {
                AddUserActionHistory addUserActionHistory = new AddUserActionHistory(WebSecurity.GetUserId(mail), AddUserActionHistory.ActionType.MAIL_CONFIRMED);
                addUserActionHistory.Start();

                return View();
            }
            else
            {
                return View("Index", "Error");
            }
        }

        #endregion

        #region Password forgot

        [HttpGet]
        public ActionResult PasswordForgot()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult PasswordForgot(PasswordForgotViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                string forgotToken = WebSecurity.GeneratePasswordResetToken(viewModel.Email);

                SendPasswordForgot sendPasswordForgot = new SendPasswordForgot(forgotToken, viewModel.Email);
                sendPasswordForgot.Start();

                if (sendPasswordForgot.Response.Success)
                {
                    AddUserActionHistory addUserActionHistory = new AddUserActionHistory(WebSecurity.GetUserId(viewModel.Email), AddUserActionHistory.ActionType.MAIL_PASSWORD_FORGOT_SENT);
                    addUserActionHistory.Start();

                    return View("PasswordForgotSent");
                }
                else
                {
                    return View("Index", "Error");
                }
            }
            else
            {
                ModelState.AddModelError(string.Empty, "Invalid data.");
            }

            return View();
        }

        [HttpGet]
        public ActionResult PasswordForgotSent()
        {
            return View();
        }

        #endregion

        #region Password reset

        [HttpGet]
        public ActionResult PasswordReset(string mail, string token)
        {
            PasswordResentViewModel viewModel = new PasswordResentViewModel()
            {
                Email = mail,
                ResetToken = token
            };

            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult PasswordReset(PasswordResentViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                if (WebSecurity.ResetPassword(viewModel.ResetToken, viewModel.NewPassword))
                {
                    AddUserActionHistory addUserActionHistory = new AddUserActionHistory(WebSecurity.GetUserId(viewModel.Email), AddUserActionHistory.ActionType.PASSWORD_RESET);
                    addUserActionHistory.Start();

                    return View("PasswordResetConfirmed");
                }
                else
                {
                    return View("Index", "Error");
                }
            }
            else
            {
                ModelState.AddModelError(string.Empty, "Invalid data.");
            }

            return View();
        }

        public ActionResult PasswordResetConfirmed()
        {
            return View();
        }

        #endregion

        #region Confirmation resent

        [HttpGet]
        public ActionResult ConfirmationResent()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ConfirmationResent(ConfirmationResentViewModel confirmationResent)
        {
            if (ModelState.IsValid)
            {
                if (!WebSecurity.UserExists(confirmationResent.Email))
                {
                    ModelState.AddModelError(string.Empty, "User does not exist!");
                }
                else if (WebSecurity.IsConfirmed(confirmationResent.Email))
                {
                    ModelState.AddModelError(string.Empty, "User is already confirmed!");
                }
                else
                {
                    GetUserConfirmationToken userMembership = new GetUserConfirmationToken(WebSecurity.GetUserId(confirmationResent.Email));
                    userMembership.Start();

                    if (userMembership.Response.Success)
                    {
                        SendMailConfirmation sendMail = new SendMailConfirmation(userMembership.UserConfirmationToken, confirmationResent.Email);
                        sendMail.Start();

                        if (!sendMail.Response.Success)
                        {
                            ModelState.AddModelError(string.Empty, "Failed to sent verification mail!");
                        }
                        else
                        {
                            AddUserActionHistory addUserActionHistory = new AddUserActionHistory(WebSecurity.GetUserId(confirmationResent.Email), AddUserActionHistory.ActionType.MAIL_CONFIRMATION_RESENT);
                            addUserActionHistory.Start();

                            return RedirectToAction("ConfirmationSent");
                        }
                    }
                    else
                    {
                        ModelState.AddModelError(string.Empty, "Get user data failed!");
                    }
                }
            }
            else
            {
                ModelState.AddModelError(string.Empty, "Invalid data!");
            }

            return View();
        }

        #endregion
    }
}