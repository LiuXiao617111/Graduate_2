using MvcGraduate.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MvcGraduate.Controllers
{
    public class InteractionController : Controller
    {
        ListClass hClass = new ListClass();
        public ActionResult Index()
        {
            return View();
        }
        #region
        public PartialViewResult Details_Images(int id = 1)
        {
            var res = DetailsClass.Details_Images(id);
            //获取分享的名称
            var sharesName = "";
            res.Share_Images.ToList().ForEach(n => sharesName += n.Students.Name);
            ViewBag.SharesName = sharesName;

            return PartialView(res);
        }
        #endregion
        [HttpPost]
        public PartialViewResult GetShareImagesPeopleName(int id = 1208203301)
        {
            var res = hClass.GetImagesInfo(id);//获取所有的图片
            return PartialView(res);
        }
        [HttpPost]
        public PartialViewResult GetArticle(int id = 1208203301)
        {
            var res = hClass.GetArticle(id);//获取所有的图片
            return PartialView(res);
        }
    }
}
