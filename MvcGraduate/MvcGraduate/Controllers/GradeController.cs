using MvcGraduate.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MvcGraduate.Controllers
{
    public class GradeController : Controller
    {
        private DetailsClass dClass = new DetailsClass();
        private ListClass hClass = new ListClass();
        public ActionResult Index(string RedirectStr="Index")
        {
            ViewBag.Redirect = RedirectStr;
            return View();
        }

        #region Details
        public PartialViewResult Details_SubjectMaterial(int id)
        {
            var res = dClass.Details_SubjectMaterial(id);
            return PartialView(res);
        }
        #endregion

        #region HttpPost
        [HttpPost]
        public PartialViewResult GetBanWei()
        {
            var res = hClass.GetBanWei(((Students)Session["User"]).ID);
            ViewBag.MyTitle = "班委成员";
            return PartialView("GetPeopleInfo",res);
        }
        [HttpPost]
        public PartialViewResult GetTeacher()
        {
            var res = hClass.GetTeacher(((Students)Session["User"]).ID);
            ViewBag.MyTitle = "任课老师";
            return PartialView("GetPeopleInfo",res);
        }
        [HttpPost]
        public PartialViewResult GetClassmate()
        {
            var res = hClass.GetClassmate(((Students)Session["User"]).ID);
            ViewBag.MyTitle = "我的同学";
            return PartialView("GetPeopleInfo",res);
        }
        [HttpPost]
        public PartialViewResult GetSubjectMaterial()
        {
            var res = hClass.GetSubjectMaterial(((Students)Session["User"]).ID);
            var ttt = res.ToList();
            ViewBag.MyTitle = "课程资料";
            return PartialView(res);
        }
        #endregion
    }
}
