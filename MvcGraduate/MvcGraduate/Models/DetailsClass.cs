using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MvcGraduate.Models
{
    public class DetailsClass
    {
        DataClassesDataContext db = new DataClassesDataContext();
        public HomeWork Details_HomeWork(int id)
        {
            return db.HomeWork.SingleOrDefault(n => n.ID == id);
        }
        public Vacation Details_Vacation(int id)
        {
            return db.Vacation.SingleOrDefault(n => n.ID == id);
        }
        public Article Details_Article(int id)
        {
            return db.Article.SingleOrDefault(n => n.ID == id);
        }
        public Questions Details_Question(int id)
        {
            return db.Questions.SingleOrDefault(n => n.ID == id);
        }
        
        public Notify Detail_Notify(int id)
        {
            return db.Notify.Single(n => n.ID == id);
        }
        public Images Details_Images(int id)
        {
            return db.Images.Single(n => n.ID == id);
        }
        public SubjectMaterial Details_SubjectMaterial(int id)
        {
            return db.SubjectMaterial.Single(n => n.ID == id);
        }
        public Students Details_Student(int id)
        {
            return db.Students.Single(n => n.ID == id);
        }
        //返回通知所通知的对象 string
        public String Details_AllNotifyPeople(int id)
        {
            var q = from c in db.Notify_People
                    where c.NotifyID == id
                    select c.Students.Name;
            if (!q.Any())
                return null;
            string res = string.Join(",", q.ToList());
            return res;
        }
        public Notify_School Details_Notify_School(int id)
        {
            return db.Notify_School.Single(n => n.ID == id);
        }
        public IEnumerable<Notify_School> GetNotify_School()
        {
            return db.Notify_School;
        }
    }
}