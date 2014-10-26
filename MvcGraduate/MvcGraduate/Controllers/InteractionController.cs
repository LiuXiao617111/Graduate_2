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
        OperateClass oClass = new OperateClass();
        DetailsClass dClass = new DetailsClass();
        public ActionResult Index()
        {
            return View();
        }

        #region Details
        public PartialViewResult Details_Images(int id = 1)
        {
            var res = dClass.Details_Images(id);
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
            var res = dClass.Details_Article(id);
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
            var res = dClass.Details_Question(id);
            return PartialView(res);
        }
        #endregion

        #region Edit
        public PartialViewResult Edit_Article(int id = 14)
        {
            var res = dClass.Details_Article(id);
            string sharesName = "";
            foreach (var item in res.Share_Article)
            {
                sharesName += item.Students.Name.Trim() + ",";
            }
            if (sharesName != "")
            {
                sharesName = sharesName.Substring(0, sharesName.Length - 1);
            }
            ViewBag.SharesName = sharesName;
            return PartialView(res);
        }
        public PartialViewResult Edit_Images(int id = 1)
        {
            //原来是用的static来调用方法的，但是一直获取的是原始的。。用非静态的就有用了
            var res = dClass.Details_Images(id);
            var tt = res.Description;
            string sharesName = "";
            foreach (var item in res.Share_Images)
            {
                sharesName += item.Students.Name.Trim() + ",";
            }
            if (sharesName != "")
            {
                sharesName = sharesName.Substring(0, sharesName.Length - 1);
            }
            ViewBag.SharesName = sharesName;
            //ASP.NET MVC3中POST之后，页面内容无法更改。
            //MVC3中，使用ModelState的对象数据，在Cotroller中，我们一旦初始化了ModelState之后，
            //无论我们对对象怎么操作，在放入视图都不会有任何效果。
            //视图默认使用的是ModelState的初始化数据，如果要改变种情况，就要让ModelState感觉到我们修改了。
            //1. 直接清除ModelState；2.修改ModelState中数据。
            //1： ModelState.Clear();
            //2：ModelState["PropertyName"] =ModelState["PropertyName2"];
            //这边不是这个问题。
            //这边原来是用Static方法获取详情的，一直获取的是老数据
            //用非static后就是新数据
            return PartialView(res);
        }
        #endregion

        #region HttpPost Get
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

        #region HttpPost Del
        [HttpPost]
        public void DelImage(string ids)
        {
            oClass.DelImage(ids);
        }
        [HttpPost]
        public void DelArticle(string ids)
        {
            oClass.DelArticle(ids);
        }
        [HttpPost]
        public void DelQuestion(int id)
        {
            oClass.DelQuestion(id);
        }
        #endregion

        #region HttpPost SaveChange
        [ValidateInput(false)]
        [HttpPost]
        public PartialViewResult SaveImageChange(FormCollection form)
        {
            oClass.SaveImageChange(form);
            return PartialView();
        }
        #endregion
    }
}
