using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MvcGraduate.Models
{
    public class HomeClass
    {
        private DataClassesDataContext db = new DataClassesDataContext();
        public Students GetStudent(int id)
        {
            var q = from c in db.Students
                    where c.ID == id
                    select c;
            if (!q.Any())
                return null;
            return q.First();
        }
        public Notify GetNotify(int id)
        {
            var res = db.Notify.Single(n => n.ID == id);
            return res;
        }
        //返回通知所通知的对象 string
        public String GetAllNotifyPeople(int id)
        {
            var q = from c in db.Notify_People
                    where c.NotifyID == id
                    select c.Students.Name;
            if (!q.Any())
                return null;
            string res = string.Join(",", q.ToList());
            return res;
        }
        public int GetNotifyCount(int id)
        {
            var q = from c in db.Notify_People
                where c.NotifyPeopleID == id
                select c;
            return q.ToList().Count;
        }
        public int GetMaterialCount(int id)
        {
            var facultyId = from c in db.Students
                where c.ID == id
                select c.Grade.FacultyID;
            var q = from c in db.SubjectMaterial
                where c.FacultyID == facultyId.First()
                select c;
            return q.ToList().Count;
        }

        #region 局部视图。。获取集合
        //获取文章
        public  IEnumerable<Article> GetArticle(int id)
        {
            var q = from c in db.Article
                    where c.WritingID == id
                    select c;
            if (!q.Any())
                return null;
            return q;
        }
        public IEnumerable<Article> GetShareicle(int id)
        {
            var q = from c in db.Share_Article
                    where c.SharedID == id
                    select c.Article;
            if (!q.Any())
                return null;
            return q;
        }
        public IEnumerable<Notify> GetNotifyPeople(int id)
        {
            var q = from c in db.Notify_People
                    where c.NotifyPeopleID == id
                    select c.Notify;
            if (!q.Any())
                return null;
            return q;
        }
        public IEnumerable<string> GetmMyImages(int id)
        {
            var q = from c in db.Images
                where c.OwnerID == id
                select c.ImagePath;
            if (!q.Any())
                return null;
            return q;
        }
        public IEnumerable<string> GetShareImages(int id)
        {
            var q = from c in db.Share_Images
                where c.StudentID == id
                select c.Images.ImagePath;
            if (!q.Any())
                return null;
            return q;
        }
        public IEnumerable<Teachers> GetTeacher(int id)
        {
            //找到班级
            var grade = GetGradeID(id);
            //找到老师
            var q = from c in db.Grades_Teachers
                    where c.GradeID == grade
                    select c.Teachers;
            if (!q.Any())
                return null;
            return q;
        }
        public IEnumerable<Students> GetBanWei(int id)
        {
            //找到班级
            var grade = GetGradeID(id);
            //找到班委
            var q = from c in db.Students
                    where c.GradeID == grade && c.Duty != null && c.Duty != ""
                    select c;
            if (!q.Any())
                return null;
            return q;
        }  
        public IEnumerable<Students> GetClassmate(int id)
        {
            //找到班级
            var grade = GetGradeID(id);
            //找到同学
            var q = from c in db.Students
                    where c.GradeID == grade
                    select c;
            if (!q.Any())
                return null;
            return q;
        }
        public IEnumerable<SubjectMaterial> GetSubjectMaterial(int id)
        {
            //找到年级
            var faculty = from c in db.Students
                        where c.ID == id
                        select c.Grade.FacultyID;
            //找到同学
            var q = from c in db.SubjectMaterial
                    where c.FacultyID == faculty.First()
                    select c;
            if (!q.Any())
                return null;
            return q;
        }
        #endregion
        #region 私有函数
        public int GetGradeID(int id)
        {
            return db.Students.Single(n => n.ID == id).GradeID;
        }
        #endregion
    }
}