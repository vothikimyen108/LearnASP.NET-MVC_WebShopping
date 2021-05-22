using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Myapp.Areas.Admin.Controllers
{
    public class IndexController : Controller
    {
        // GET: Admin/Home
        [Authorize(Roles = "admin")]
        public ActionResult Index()
        {
            return View();
        }
    }
}