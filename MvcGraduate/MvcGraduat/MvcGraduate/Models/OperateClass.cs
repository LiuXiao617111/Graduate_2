using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
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

        public byte[] GetAppendixByte(string filePath)
        {
            byte[] buffer = null;
            try
            {
                FileStream fs = new FileStream(filePath, FileMode.Open);
                buffer = new byte[fs.Length];
                fs.Read(buffer, 0, (int)fs.Length);
            }
            catch { return null; }
            return buffer;

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
        public bool SaveArticle(HttpServerUtilityBase Server,HttpRequestBase Request,FormCollection form,int id)
        {
            Article newArticle = new Article();
            newArticle.Title = form["Title"];
            newArticle.WritingID = id;
            newArticle.Time = DateTime.Now;
            newArticle.ClickRate = 0;
            newArticle.Contents = form["editorValue"];

            var file = Request.Files["fileUpload"];

            if (file != null)
            {
                var fileName = file.FileName;
                string path = Server.MapPath("~/") + "DownLoad\\" + fileName;
                try
                {
                    file.SaveAs(path);
                    newArticle.Appendix = path;
                }
                catch
                {
                    return false;
                }
            }
            db.Article.InsertOnSubmit(newArticle);
            try
            {
                db.SubmitChanges();
            }
            catch { return false; }
            return true;
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
        public bool AddStudent(Students stu)
        {
            db.Students.InsertOnSubmit(stu);
            try
            {
                db.SubmitChanges();
                return true;
            }
            catch{ return false; }
        }
        public bool IsExistStudent(string id)
        {
            int i = 0;
            try
            {
                i = Convert.ToInt32(id);
            }
            catch { }
            return db.Students.Any(n => n.ID == i);
        }
        public bool SaveArticleComment(int stuID, string articleId, string contents)
        {
            ArticleComments item = new ArticleComments();
            item.Contents = contents;
            item.PeopleID = stuID;
            item.Time = DateTime.Now;
            try
            {
                item.ArticleID = Convert.ToInt32(articleId);
                db.ArticleComments.InsertOnSubmit(item);
                db.SubmitChanges();
                return true;
            }
            catch { }
            return false;
        }

        public List<SelectListItem> GetGradeSelectList()
        {
            List<SelectListItem> items = new List<SelectListItem>();
            var allGrade = db.Grade.Select(n=>n);
            foreach (var item in allGrade)
            {
                items.Add(new SelectListItem { Text = item.Name,Value=item.ID.ToString() });
            }
            return items;
        }
        public List<SelectListItem> GetFacultySelectList()
        {
            List<SelectListItem> items = new List<SelectListItem>();
            var allFaculty = db.Faculty.Select(n => n);
            foreach (var item in allFaculty)
            {
                items.Add(new SelectListItem { Text = item.Name, Value = item.ID.ToString() });
            }
            return items;
        }

        #region Account
        public bool AddAccount(FormCollection form)
        {
            Account account = new Account();
            account.LoginID = form["name"];
            account.Email = form["email"];
            account.Pwd = System.Web.Security.FormsAuthentication.HashPasswordForStoringInConfigFile(form["pwd"], "MD5");
            db.Account.InsertOnSubmit(account);
            try
            {
                db.SubmitChanges();
            }
            catch (SqlException e)
            {
                return false;
            }
            return true;
        }
        public bool ISHaveCount(string loginID)
        {
            return db.Account.Any(n => n.LoginID == loginID);//验证账户
        }
        public bool AddReaptCode(string id, string parms)
        {
            var account = db.Account.Single(n => n.LoginID == id);
            if (account != null)
            {
                if (account.verificationCode != parms)//校验码和请求码不一样
                {
                    return false;
                }
                try
                {
                    account.repeatCode = parms;
                    db.SubmitChanges();
                    return true;
                }
                catch { }
            }
            return false;
        }
        public bool AddVerificationCode(string id, string parms,string toEmail)
        {
            var account = db.Account.Single(n => n.LoginID == id);
            if (account != null)
            {
                if (toEmail != account.Email)
                    return false;
                try
                {
                    account.verificationCode = parms;
                    db.SubmitChanges();
                    return true;
                }
                catch { }
            }
            return false;
        }
        public bool ChangePwd(string id, string pwd)
        {
            var account = db.Account.Single(n => n.LoginID == id);
            account.Pwd = System.Web.Security.FormsAuthentication.HashPasswordForStoringInConfigFile(pwd, "MD5");
            account.verificationCode = "";//重置验证码，防止重复提交
            try
            {
                db.SubmitChanges();
                return true;
            }
            catch { }
            return false;
        }
        public Students ValidateCount(FormCollection form)
        {
            string count = form["LoginID"];
            string pwd = System.Web.Security.FormsAuthentication.HashPasswordForStoringInConfigFile(form["Pwd"], "MD5");
            if (db.Account.Any(n => n.LoginID == count && n.Pwd == pwd))
            {
                DetailsClass dClass = new DetailsClass();
                try
                {
                    return dClass.Details_Student(Convert.ToInt32(count));
                }
                catch (Exception e)
                {
                }
            }
            return null;
        }
        public Teachers ValidateTeacher(FormCollection form)
        {
            string count = form["LoginID"];
            string pwd = System.Web.Security.FormsAuthentication.HashPasswordForStoringInConfigFile(form["Pwd"], "MD5");
            if (db.Account.Any(n => n.LoginID == count && n.Pwd == pwd))
            {
                DetailsClass dClass = new DetailsClass();
                try
                {
                    return db.Teachers.Single(n => n.ID == Convert.ToInt32(count));
                }
                catch (Exception e)
                {
                }
            }
            return null;
        }
        #endregion
    }
}