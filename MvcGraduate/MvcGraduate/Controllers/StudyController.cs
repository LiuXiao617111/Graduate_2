﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcGraduate.Models;

namespace MvcGraduate.Controllers
{
    public class StudyController : Controller
    {
        private StudyClass sClass = new StudyClass();
        public ActionResult Index()
        {
            return View();
        }

        #region 详情页面
        public PartialViewResult Details_HomeWork(int id = 1)
        {
            var res = DetailsClass.Details_HomeWork(id);
            return PartialView(res);
        }
        public PartialViewResult Details_Vacation(int id = 1)
        {
            var res = DetailsClass.Details_Vacation(id);
            return PartialView(res);
        }
        #endregion
        #region HttpPost请求
        [HttpPost]
        public PartialViewResult GetHomeWork(int id = 1208203301)
        {
            var res=sClass.GetHomeWork(id);
            return PartialView(res);
        }
        [HttpPost]
        public PartialViewResult GetTimeTable(int id = 1208203301)
        {
            var res = sClass.GetTimeTable(id);
            return PartialView(res);
        }
        [HttpPost]
        public PartialViewResult GetMyVacation(int id = 1208203301)
        {
            var res = sClass.GetMyVacation(id);
            return PartialView(res);
        }
        //未实现
        [HttpPost]
        public PartialViewResult GetScore(int id = 1208203301)
        {
            var res = sClass.GetScore(id);
            return PartialView(res);
        }
        #endregion
    }
}
