using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MvcGraduate.Controllers
{
    public class TeacherHomeController : Controller
    {
        //
        // GET: /TeacherHome/

        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Manager()
        {
            return PartialView();
        }

    }
}
