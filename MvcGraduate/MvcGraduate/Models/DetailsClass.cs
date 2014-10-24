using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MvcGraduate.Models
{
    public static class DetailsClass
    {
        static DataClassesDataContext db = new DataClassesDataContext();
        static public HomeWork Details_HomeWork(int id)
        {
            return db.HomeWork.SingleOrDefault(n => n.ID == id);
        }
        static public Vacation Details_Vacation(int id)
        {
            return db.Vacation.SingleOrDefault(n => n.ID == id);
        }
        static public Article Details_Article(int id)
        {
            return db.Article.SingleOrDefault(n => n.ID == id);
        }
        static public Notify Detail_Notify(int id)
        {
            return db.Notify.Single(n => n.ID == id);
        }
        static public Images Details_Images(int id)
        {
            return db.Images.SingleOrDefault(n => n.ID == id);
        }
        static public SubjectMaterial Details_SubjectMaterial(int id)
        {
            return db.SubjectMaterial.Single(n => n.ID == id);
        }
        static public Students Details_Student(int id)
        {
            return db.Students.Single(n => n.ID == id);
        }
        //返回通知所通知的对象 string
        static public String Details_AllNotifyPeople(int id)
        {
            var q = from c in db.Notify_People
                    where c.NotifyID == id
                    select c.Students.Name;
            if (!q.Any())
                return null;
            string res = string.Join(",", q.ToList());
            return res;
        }
        static public Notify_School Details_Notify_School(int id)
        {
            return db.Notify_School.Single(n => n.ID == id);
        }
        static public IEnumerable<Notify_School> GetNotify_School()
        {
            return db.Notify_School;
        }
    }
}