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
        public ActionResult Index(string RedirectStr = "RedirectToImage")
        {
            ViewBag.Redirect = RedirectStr;
            return View();
        }

        #region Details
        public PartialViewResult Details_Images(int id)
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
        public PartialViewResult Details_Article(int id)
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
            if (res.Appendix != "" && res.Appendix != null)
            {
                string[] strs=res.Appendix.Split('\\');
                ViewBag.Appendix = strs.Last();
            }
            return PartialView(res);
        }
        public PartialViewResult Details_Question(int id)
        {
            var res = dClass.Details_Question(id);
            return PartialView(res);
        }
        #endregion

        #region Edit
        public PartialViewResult Edit_Article(int id)
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
        public PartialViewResult Edit_Images(int id)
        {
            //原来是用的static来调用方法的，但是一直获取的是原始的。。用非静态的就有用了
            var res = dClass.Details_Images(id);
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

            #region Article
            [HttpPost]
            public PartialViewResult GetArticle()
            {
                var res = hClass.GetArticle(((Students)Session["User"]).ID);//获取所有的图片
                return PartialView(res);
            }
            [HttpPost]
            public PartialViewResult GetShareArticle()
            {
                var res = hClass.GetShareArticle(((Students)Session["User"]).ID);//获取分享文章
                return PartialView(res);
            }
            [HttpPost]
            public PartialViewResult GetArticleComments(int id)
            {
                var res = hClass.GetArticleComments(id);//获取对应文章的评论
                return PartialView(res);
            }
            [HttpPost]
            public PartialViewResult GetShareArticleComments(int id)
            {
                var res = hClass.GetArticleComments(id);//获取对应文章的评论
                ViewBag.Title = "文章";
                return PartialView("ShareComment", res);
            }
            #endregion

            #region Image
            [HttpPost]
            public PartialViewResult GetImagesInfo()
            {
                var res = hClass.GetImagesInfo(((Students)Session["User"]).ID);//获取所有的图片
                return PartialView(res);
            }
            [HttpPost]
            public PartialViewResult GetShareImagesInfo()
            {
                var res = hClass.GetShareImagesInfo(((Students)Session["User"]).ID);//获取所有的图片
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
                ViewBag.Title = "照片";
                return PartialView("ShareComment",res);
            }
            #endregion

            [HttpPost]
            public PartialViewResult GetQuestions(int index=0)
            {
                var res = hClass.GetQuestions(((Students)Session["User"]).ID);//获取所有的提问
                ViewBag.MyTitle = "我的提问";
                ViewBag.AllCount = res.ToList().Count;
                ViewBag.MaxPage = res.ToList().Count / 10;
                ViewBag.Index = index;

                res = res.Skip(index * 10).Take(10);
                var ttt = res.ToList();
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
        [HttpPost]
        public void DelImageComment(int id)
        {
            oClass.DelImageComments(id);
        }
        [HttpPost]
        public void DelArticleComment(int id)
        {
            oClass.DelArticleComments(id);
        }
        #endregion

        #region HttpPost SaveChange
        [ValidateInput(false)]
        [HttpPost]
        public PartialViewResult SaveChange(FormCollection form,string name)
        {
            if (form["Sign"] == "Picture")
            {
                ViewBag.Title = "图片";
                oClass.SaveImageChange(form);
            }
            else if (form["Sign"] == "Article")
            {
                ViewBag.Title = "文章";
                oClass.SaveArticleChange(form);
            }
            return PartialView();
        }
        [HttpPost]
        public bool SaveArticleComments(string articleId,string contents)
        {
            var id = ((Students)Session["User"]).ID;
            return oClass.SaveArticleComment(id, articleId, contents);
        }
        #endregion

        #region Http ResposeFile
        public void ResposeFile(string filePath)
        {
            byte[] buffer= oClass.GetAppendixByte(filePath);
            string[] strs = filePath.Split('\\');
            
            Response.ContentType = "application/octet-stream";
            //通知浏览器下载文件而不是打开
            Response.AddHeader("Content-Disposition", "attachment;  filename=" + HttpUtility.UrlEncode(strs.Last(), System.Text.Encoding.UTF8));
            Response.BinaryWrite(buffer);
            Response.Flush();
            Response.End();
        }
        #endregion
    }
}
