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
                db.Article.DeleteOnSubmit(db.Article.Single(n => n.ID == Convert.ToInt32(strs[i])));
            }
            db.SubmitChanges();
        }
        public void DelQuestion(int id)
        {
            db.Questions.DeleteOnSubmit(db.Questions.Single(n => n.ID == id));
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
    }
}