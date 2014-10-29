using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcGraduate.Models;

namespace MvcGraduate.Controllers
{
    public class HomeController : Controller
    {
        private DetailsClass dClass = new DetailsClass();
        private ListClass hClass = new ListClass();
        private OperateClass oClass = new OperateClass();

        public ActionResult Index()
        {
            if (Session["User"] == null)
                return View(Url.Action("Index", "Account"));
            var res = (Students)Session["User"];
            ViewBag.AricleCount = res.Article.Count;
            ViewBag.ImageCount = res.Images.Count;
            ViewBag.NotifyCount = hClass.GetNotifyCount(res.ID);
            ViewBag.QuestionCount = res.Questions.Count;
            ViewBag.HomeWorkCount = res.HomeWork.Count;
            ViewBag.MaterialCount = hClass.GetMaterialCount(res.ID);
            return View(res);
        }

        public PartialViewResult GetAddArticle()
        {
            return PartialView();
        }

        #region HttpPost
        [HttpPost]
        public PartialViewResult GetSchedule()
        {
            var res = hClass.GetTimeTable(((Students)Session["User"]).ID);
            return PartialView(res);
        }
        [HttpPost]
        public PartialViewResult GetMyArt()
        {
            var res = hClass.GetArticle(((Students)Session["User"]).ID);
            return PartialView(res);
        }
        [HttpPost]
        public PartialViewResult GetShareArt()
        {
            var res = hClass.GetShareArticle(((Students)Session["User"]).ID);
            return PartialView(res);
        }
        [HttpPost]
        public PartialViewResult GetNotify()
        {
            var res = hClass.GetNotifyPeople(((Students)Session["User"]).ID);
            return PartialView(res);
        }
        [HttpPost]
        public PartialViewResult GetQuestion()
        {
            var res = hClass.GetQuestions(((Students)Session["User"]).ID);
            var tt = res.ToList();
            return PartialView(res);
        }
        [HttpPost]
        public PartialViewResult GetMyImages()
        {
            var res = hClass.GetmMyImagesPath(((Students)Session["User"]).ID);
            return PartialView(res);
        }
        [HttpPost]
        public PartialViewResult GetShareImages()
        {
            var res = hClass.GetShareImagesPath(((Students)Session["User"]).ID);
            return PartialView(res);
        }
        [HttpPost]
        public PartialViewResult GetTeacherImages()
        {
            var res = hClass.GetTeacher(((Students)Session["User"]).ID);
            return PartialView("GetPeople",res.ToList());
        }
        [HttpPost]
        public PartialViewResult GetBanWei()
        {
            var res = hClass.GetBanWei(((Students)Session["User"]).ID);
            return PartialView("GetPeople",res.ToList());
        }
        [HttpPost]
        public PartialViewResult GetHonour()
        {
            var res = hClass.GetHonour(((Students)Session["User"]).ID);
            return PartialView(res);
        }
        [HttpPost]
        public PartialViewResult GetAccountInfo()
        {
            var stu = dClass.Details_Student(((Students)Session["User"]).ID);
            return PartialView(stu);
        }
        #endregion

        #region Save
        [HttpPost]
        public bool SubmitQuestion(string info)
        {
            return oClass.AddQuestion(info, ((Students)Session["User"]).ID);
        }
        [HttpPost]
        [ValidateInput(false)]
        public bool SaveArticle(FormCollection form)
        {
            return oClass.SaveArticle(Server,Request,form,((Students)Session["User"]).ID);
        }
        #endregion
    }
}
