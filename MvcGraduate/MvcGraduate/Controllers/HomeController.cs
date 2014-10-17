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
        private HomeClass homeFun = new HomeClass();
        StudyClass studyFun = new StudyClass();
        //
        // GET: /Home/

        public ActionResult Index(int id=1208203301)
        {
            var res = homeFun.GetStudent(id);
            ViewBag.AricleCount = res.Article.Count;
            ViewBag.ImageCount = res.Images.Count;
            ViewBag.NotifyCount = homeFun.GetNotifyCount(id);
            ViewBag.QuestionCount = res.Questions.Count;
            ViewBag.HomeWorkCount = res.HomeWork.Count;
            ViewBag.MaterialCount = homeFun.GetMaterialCount(id);
            return View(res);
        }
        #region 详情页
        public PartialViewResult Details_NotiyNews(int id = 1)
        {
            var res = homeFun.GetNotify(id);
            ViewBag.NotifyPeoples = homeFun.GetAllNotifyPeople(id);
            return PartialView(res);
        }
        #endregion
        #region HttpPost
        [HttpPost]
        public PartialViewResult GetSchedule(int id = 1208203301)
        {
            var res = studyFun.GetTimeTable(id);
            return PartialView(res);
        }
        [HttpPost]
        public PartialViewResult GetMyArt(int id = 1208203301)
        {
            var res=homeFun.GetArticle(id);
            return PartialView(res);
        }
        [HttpPost]
        public PartialViewResult GetShareArt(int id = 1208203301)
        {
            var res = homeFun.GetShareicle(id);
            return PartialView(res);
        }
        [HttpPost]
        public PartialViewResult GetNotify(int id = 1208203301)
        {
            var res = homeFun.GetNotifyPeople(id);
            return PartialView(res);
        }
        [HttpPost]
        public PartialViewResult GetMyImages(int id=120803301)
        {
            var res = homeFun.GetmMyImages(id);
            return PartialView(res);
        }
        [HttpPost]
        public PartialViewResult GetShareImages(int id = 120803301)
        {
            var res = homeFun.GetShareImages(id);
            return PartialView(res);
        }
        [HttpPost]
        public PartialViewResult GetTeacherImages(int id = 120803301)
        {
            var res = homeFun.GetTeacher(id);
            return PartialView("GetPeople",res.ToList());
        }
        [HttpPost]
        public PartialViewResult GetBanWei(int id = 120803301)
        {
            var res = homeFun.GetBanWei(id);
            return PartialView("GetPeople",res.ToList());
        }
        #endregion
    }
}
