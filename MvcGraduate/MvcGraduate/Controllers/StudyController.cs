using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcGraduate.Models;

namespace MvcGraduate.Controllers
{
    public class StudyController : Controller
    {
        private DetailsClass dClass = new DetailsClass();
        private ListClass hClass = new ListClass();
        public ActionResult Index()
        {
            return View();
        }

        #region Details
        public PartialViewResult Details_HomeWork(int id)
        {
            var res = dClass.Details_HomeWork(id);
            return PartialView(res);
        }
        public PartialViewResult Details_Vacation(int id)
        {
            var res = dClass.Details_Vacation(id);
            return PartialView(res);
        }
        #endregion

        #region HttpPost
        [HttpPost]
        public PartialViewResult GetHomeWork()
        {
            var res=hClass.GetHomeWork(((Students)Session["User"]).ID);
            return PartialView(res);
        }
        [HttpPost]
        public PartialViewResult GetTimeTable()
        {
            var res = hClass.GetTimeTable(((Students)Session["User"]).ID);
            return PartialView(res);
        }
        [HttpPost]
        public PartialViewResult GetMyVacation()
        {
            var res = hClass.GetMyVacation(((Students)Session["User"]).ID);
            return PartialView(res);
        }
        //未实现
        [HttpPost]
        public PartialViewResult GetScore()
        {
            var res = hClass.GetScore(((Students)Session["User"]).ID);
            return PartialView(res);
        }
        #endregion
    }
}
