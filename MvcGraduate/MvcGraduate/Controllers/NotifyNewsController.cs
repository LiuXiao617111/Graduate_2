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
        private DetailsClass dClass = new DetailsClass();
        ListClass hClass = new ListClass();
        public ActionResult Index()
        {
            return View();
        }

        #region Details
        public PartialViewResult Detail_Notify(int id )
        {
            var res = dClass.Detail_Notify(id);
            return PartialView(res);
        }
        public PartialViewResult Details_Notify_School(int id )
        {
            var res = dClass.Details_Notify_School(id);
            return PartialView(res);
        }
        #endregion

        #region HttpPost
        [HttpPost]
        public PartialViewResult GetAllNotify()
        {
            var res = hClass.GetAllNotify(((Students)Session["User"]).ID);
            ViewBag.MyTitle = "活动通知";
            return PartialView(res);
        }
        [HttpPost]
        public PartialViewResult GetSchoolNotify()
        {
            var res = dClass.GetNotify_School();
            ViewBag.MyTitle = "学校新闻";
            return PartialView(res);
        }
        #endregion
    }
}
