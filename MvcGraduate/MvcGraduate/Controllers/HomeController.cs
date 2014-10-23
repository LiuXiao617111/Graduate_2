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
        private ListClass hClass = new ListClass();

        public ActionResult Index(int id=1208203301)
        {
            var res = DetailsClass.Details_Student(id);
            ViewBag.AricleCount = res.Article.Count;
            ViewBag.ImageCount = res.Images.Count;
            ViewBag.NotifyCount = hClass.GetNotifyCount(id);
            ViewBag.QuestionCount = res.Questions.Count;
            ViewBag.HomeWorkCount = res.HomeWork.Count;
            ViewBag.MaterialCount = hClass.GetMaterialCount(id);
            return View(res);
        }

        #region HttpPost
        [HttpPost]
        public PartialViewResult GetSchedule(int id = 1208203301)
        {
            var res = hClass.GetTimeTable(id);
            return PartialView(res);
        }
        [HttpPost]
        public PartialViewResult GetMyArt(int id = 1208203301)
        {
            var res=hClass.GetArticle(id);
            return PartialView(res);
        }
        [HttpPost]
        public PartialViewResult GetShareArt(int id = 1208203301)
        {
            var res = hClass.GetShareicle(id);
            return PartialView(res);
        }
        [HttpPost]
        public PartialViewResult GetNotify(int id = 1208203301)
        {
            var res = hClass.GetNotifyPeople(id);
            return PartialView(res);
        }
        [HttpPost]
        public PartialViewResult GetMyImages(int id=120803301)
        {
            var res = hClass.GetmMyImagesPath(id);
            return PartialView(res);
        }
        [HttpPost]
        public PartialViewResult GetShareImages(int id = 120803301)
        {
            var res = hClass.GetShareImagesPath(id);
            return PartialView(res);
        }
        [HttpPost]
        public PartialViewResult GetTeacherImages(int id = 120803301)
        {
            var res = hClass.GetTeacher(id);
            return PartialView("GetPeople",res.ToList());
        }
        [HttpPost]
        public PartialViewResult GetBanWei(int id = 120803301)
        {
            var res = hClass.GetBanWei(id);
            return PartialView("GetPeople",res.ToList());
        }
        #endregion
    }
}
