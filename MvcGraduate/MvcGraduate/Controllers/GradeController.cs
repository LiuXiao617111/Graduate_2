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
        private HomeClass hClass = new HomeClass();
        public ActionResult Index()
        {
            return View();
        }

        #region 详情页
        public PartialViewResult Details_SubjectMaterial(int id=4)
        {
            var res = DetailsClass.Details_SubjectMaterial(id);
            return PartialView(res);
        }
        #endregion
        #region ajax请求
        [HttpPost]
        public PartialViewResult GetBanWei(int id)
        {
            var res=hClass.GetBanWei(id);
            ViewBag.MyTitle = "班委成员";
            return PartialView("GetPeopleInfo",res);
        }
        [HttpPost]
        public PartialViewResult GetTeacher(int id)
        {
            var res = hClass.GetTeacher(id);
            ViewBag.MyTitle = "任课老师";
            return PartialView("GetPeopleInfo",res);
        }
        [HttpPost]
        public PartialViewResult GetClassmate(int id)
        {
            var res = hClass.GetClassmate(id);
            ViewBag.MyTitle = "我的同学";
            return PartialView("GetPeopleInfo",res);
        }
        [HttpPost]
        public PartialViewResult GetSubjectMaterial(int id)
        {
            var res = hClass.GetSubjectMaterial(id);
            var ttt = res.ToList();
            ViewBag.MyTitle = "课程资料";
            return PartialView(res);
        }
        #endregion
    }
}
