using Apps_.Models;
using Apps_.Models;
using System;
using System.Linq;
using System.Web.Helpers;
using System.Web.Mvc;
using System.Web.Security;

namespace Apps_.Controllers
{
    public class AccountController : Controller
    {
        private ModelContainer db = new ModelContainer();

        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(LoginViewModel model, string returnUrl)
        {
            if (ModelState.IsValid)
            {
                // ============================================================
                // INTERNAL AUTHENTICATION

                // Check & Verify Hash passwords in DB
                string orig_pwd = db.Apps_Users.Where(x => x.username == model.UserName).Select(x => x.password).FirstOrDefault();
                var IsPwdVerified = BCrypt.Net.BCrypt.Verify(model.Password, orig_pwd); 

                // Check Valid user in DB
                bool IsValidUser = db.Apps_Users.Any(u => u.username == model.UserName);

                if (IsValidUser && IsPwdVerified)
                {
                    FormsAuthentication.SetAuthCookie(model.UserName, false);

                    // Redirect to last page logoff
                    if (Url.IsLocalUrl(returnUrl))
                    {
                        ViewBag.ReturnUrl = returnUrl;
                        return Redirect(returnUrl);
                    }

                    // Pass: Return to home
                    return RedirectToAction("Index", "Home");
                }
            }
            ModelState.AddModelError("", "invalid Username or Password");
            return View();
        }

        public ActionResult LogOff()
        {
            FormsAuthentication.SignOut();
            return base.RedirectToAction("Index", "Home");
        }

        [HttpPost]
        public JsonResult SessionTimeout()
        {
            return new JsonResult
            {
                Data = "Beat Generated"
            };
        }
    }
}

