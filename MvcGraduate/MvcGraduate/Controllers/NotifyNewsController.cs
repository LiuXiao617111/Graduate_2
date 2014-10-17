using MvcGraduate.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MvcGraduate.Controllers
{
    public class NotifyNewsController : Controller
    {
        NotifyClass nClass = new NotifyClass();
        //
        // GET: /NotifyNews/

        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public PartialViewResult GetAllNotify(int id)
        {
            var res = nClass.GetAllNotify(id);
            ViewBag.MyTitle = "活动通知";
            return PartialView(res);
        }
    }
}
