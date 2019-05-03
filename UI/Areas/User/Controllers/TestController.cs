using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ZigmaWeb.UI.Areas.User.Controllers
{
    [AllowAnonymous]
    public class TestController : Controller
    {
        // GET: User/Test
        public ActionResult Index()
        {
            return View();
        }
    }
}