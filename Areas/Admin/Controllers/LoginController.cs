using Myapp.Areas.Admin.Models;
using sql.farmwork;
using System;
using System.Linq;
using System.Web.Mvc;
using System.Web.Security;
using System.Web;

namespace Myapp.Areas.Admin.Controllers
{
    public class LoginController : Controller
    {
       
       [HttpGet]
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult Index(LoginModel model, string returnUrl)
        {
            if (ModelState.IsValid)
            {
                using (Myappcontext objContext = new Myappcontext())
                {
                    var objUser = objContext.Users.FirstOrDefault(x => x.UserName == model.UserName && x.Password == model.Password);
                    if (objUser == null)
                    {
                        ModelState.AddModelError("LogOnError", "The user name or password provided is incorrect.");
                    }
                    else
                    {
                        FormsAuthentication.SetAuthCookie(model.UserName, model.RememberMe);
                   
                        if (Url.IsLocalUrl(returnUrl) && returnUrl.Length > 1 && returnUrl.StartsWith("/")
                           && !returnUrl.StartsWith("//") && !returnUrl.StartsWith("/\\"))
                        {
                            return RedirectToAction("Index", "Index");
                        }
                        else
                        {
                            //Redirect to default page
                            return RedirectToAction("RedirectToDefault");
                        }
                    }
                }
            }
            // If we got this far, something failed, redisplay form
            return View(model);
        }

        public ActionResult RedirectToDefault()
        {

            String[] roles = Roles.GetRolesForUser(this.CurrentUserName);
            Console.Write(roles);
            if (roles.Contains("admin"))
            {
                Console.Write("1");
                return RedirectToAction("Index", "Index");
            }
            else if (roles.Contains("nv"))
            {
                return RedirectToAction("Index", "c");
            }
            else if (roles.Contains("admin"))
            {
                return RedirectToAction("Index", "b");
            }
            else
            {
                Console.Write(roles);
                return RedirectToAction("Index", this.CurrentUserName);
            }
        }

        public string CurrentUserName
        {
            get
            {
                string userName = string.Empty;

                if (System.Web.HttpContext.Current.Request.IsAuthenticated)
                {
                    userName = System.Web.HttpContext.Current.User.Identity.Name.Split('|')[0];
                }

                return userName;
            }
        }
    }
}