using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MvcGraduate.Models
{
    public class OperateClass
    {
        DataClassesDataContext db = new DataClassesDataContext();
        public void DelImage(string id)
        {
            string[] strs = new string[]{};
            if (id != "")
            {
                strs=id.Split(',');
            }
            int last = 0;
            if (strs.Length != 1)
                last = 1;
            for(int i=0;i<strs.Length-last;i++)
            {
                if (strs[i] == "on")
                    continue;
                db.Images.DeleteOnSubmit(db.Images.Single(n => n.ID == Convert.ToInt32(strs[i])));
            }
            db.SubmitChanges();
        }
        public void DelArticle(string id)
        {
            string[] strs = new string[]{};
            if (id != "")
            {
                strs=id.Split(',');
            }
            int last = 0;
            if (strs.Length != 1)
                last = 1;
            for(int i=0;i<strs.Length-last;i++)
            {
                if (strs[i] == "on" || strs[i]=="")
                    continue;
                db.Article.DeleteOnSubmit(db.Article.Single(n => n.ID == Convert.ToInt32(strs[i])));
            }
            db.SubmitChanges();
        }
        public void DelQuestion(int id)
        {
            db.Questions.DeleteOnSubmit(db.Questions.Single(n => n.ID == id));
            db.SubmitChanges();
        }
        public void DelImageComments(int id)
        {
            db.ImageComments.DeleteOnSubmit(db.ImageComments.Single(n => n.ID == id));
            db.SubmitChanges();
        }
        public void DelArticleComments(int id)
        {
            db.ArticleComments.DeleteOnSubmit(db.ArticleComments.Single(n => n.ID == id));
            db.SubmitChanges();
        }
        public bool SaveImageChange(FormCollection form)
        {
            var image = db.Images.Single(n => n.ID == Convert.ToInt32(form["ID"]));
            image.Description = form["editorValue"];
            image.Appendix = form["Appendix"];
            try
            {
                db.SubmitChanges();
                return true;
            }
            catch {
                return false;
            }
        }
        public bool SaveArticleChange(FormCollection form)
        {
            var article = db.Article.Single(n => n.ID == Convert.ToInt32(form["ID"]));
            article.Contents = form["editorValue"];
            article.Appendix = form["Appendix"];
            try
            {
                db.SubmitChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }
        public bool AddQuestion(string info, int id)
        { 
            Questions item=new Questions();
            item.Title=info;
            item.Contents=info;
            item.Time=DateTime.Now;
            item.StudentID=id;
            try
            {
                db.Questions.InsertOnSubmit(item);
                db.SubmitChanges();
                return true;
            }
            catch { return false; }
        }
        public Students ValidateCount(FormCollection form)
        {
            string count = form["LoginID"];
            string pwd = form["Pwd"];
            if (db.Account.Any(n => n.LoginID == count && n.Pwd == pwd))
            {
                DetailsClass dClass = new DetailsClass();
                try
                {
                    return dClass.Details_Student(Convert.ToInt32(count));
                }
                catch { }
            }
            return null;
        }
    }
}