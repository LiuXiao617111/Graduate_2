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

        #region Details
        public PartialViewResult Details_Images(int id = 1)
        {
            var res = DetailsClass.Details_Images(id);
            //获取分享的名称
            var sharesName = "";
            res.Share_Images.ToList().ForEach(n => sharesName = sharesName+n.Students.Name.Trim()+",");
            if (sharesName != "")
            {
                sharesName = sharesName.Substring(0, sharesName.Length - 1);
            }
            ViewBag.SharesName = sharesName;

            return PartialView(res);
        }
        public PartialViewResult Details_Article(int id = 14)
        {
            var res = DetailsClass.Details_Article(id);
            var sharesName = "";
            foreach (var item in res.ArticleComments)
            {
                sharesName += item.Students.Name.Trim() + ",";
            }
            if (sharesName != "")
            {
                sharesName = sharesName.Substring(0, sharesName.Length - 1);
            }
            ViewBag.SharesName = sharesName;//分享的人
            ViewBag.CommentsPeople = hClass.GetArticleComments(res.ID);//评论
            return PartialView(res);
        }
        public PartialViewResult Details_Question(int id = 5)
        {
            var res = DetailsClass.Details_Question(id);
            return PartialView(res);
        }
        #endregion

        #region Edit
        [ValidateInput(false)]
        public PartialViewResult Edit_Article(int id = 14)
        {
            var res = DetailsClass.Details_Article(id);
            string sharesName = "";
            foreach (var item in res.Share_Article)
            {
                sharesName += item.Students.Name.Trim() + ",";
            }
            sharesName = sharesName.Substring(0, sharesName.Length - 1);
            ViewBag.SharesName = sharesName;
            return PartialView(res);
        }
        public PartialViewResult Edit_Images(int id = 1)
        {
            var res = DetailsClass.Details_Images(id);
            string sharesName = "";
            foreach (var item in res.Share_Images)
            {
                sharesName += item.Students.Name.Trim() + ",";
            }
            sharesName = sharesName.Substring(0, sharesName.Length - 1);
            ViewBag.SharesName = sharesName;
            return PartialView(res);
        }
        #endregion

        #region HttpPost
        [HttpPost]
        public PartialViewResult GetImagesInfo(int id = 1208203301)
        {
            var res = hClass.GetImagesInfo(id);//获取所有的图片
            return PartialView(res);
        }
        [HttpPost]
        public PartialViewResult GetShareImagesInfo(int id = 1208203301)
        {
            var res = hClass.GetShareImagesInfo(id);//获取所有的图片
            return PartialView(res);
        }
        [HttpPost]
        public PartialViewResult GetArticle(int id = 1208203301)
        {
            var res = hClass.GetArticle(id);//获取所有的图片
            return PartialView(res);
        }
        [HttpPost]
        public PartialViewResult GetShareArticle(int id = 1208203301)
        {
            var res = hClass.GetShareArticle(id);//获取分享文章
            return PartialView(res);
        }
        [HttpPost]
        public PartialViewResult GetQuestions(int id = 1208203301)
        {
            var res = hClass.GetQuestions(id);//获取所有的提问
            ViewBag.MyTitle = "我的提问";
            return PartialView(res);
        }
        [HttpPost]
        public PartialViewResult GetImagesComments(int id = 1)
        {
            var res = hClass.GetImagesComments(id);//获取对应照片的评论
            return PartialView(res);
        }
        [HttpPost]
        public PartialViewResult GetShareImagesComments(int id = 1)
        {
            var res = hClass.GetImagesComments(id);//获取对应照片的评论
            return PartialView(res);
        }
        #endregion
    }
}
