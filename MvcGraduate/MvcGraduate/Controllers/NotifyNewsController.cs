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
        ListClass hClass = new ListClass();
        public ActionResult Index()
        {
            return View();
        }

        #region Details
        public PartialViewResult Detail_Notify(int id = 4)
        {
            var res = DetailsClass.Detail_Notify(id);
            return PartialView(res);
        }
        public PartialViewResult Details_Notify_School(int id = 3)
        {
            var res = DetailsClass.Details_Notify_School(id);
            return PartialView(res);
        }
        #endregion

        #region HttpPost
        [HttpPost]
        public PartialViewResult GetAllNotify(int id=1208203301)
        {
            var res = hClass.GetAllNotify(id);
            ViewBag.MyTitle = "活动通知";
            return PartialView(res);
        }
        [HttpPost]
        public PartialViewResult GetSchoolNotify()
        {
            var res = DetailsClass.GetNotify_School();
            ViewBag.MyTitle = "学校新闻";
            return PartialView(res);
        }
        #endregion
    }
}
