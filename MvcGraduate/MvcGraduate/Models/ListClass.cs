using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MvcGraduate.Models
{
    public class ListClass
    {
        private DataClassesDataContext db = new DataClassesDataContext();
        //获取个数
        public int GetNotifyCount(int id)
        {
            return db.Notify_People.Count(n => n.NotifyPeopleID == id);
        }
        public int GetMaterialCount(int id)
        {
            var facultyId = db.Students.Single(n => n.ID == id).Grade.FacultyID;
            return db.SubjectMaterial.Count(n => n.FacultyID == facultyId);
        }

        #region 局部视图。。获取集合
        //获取文章
        public  IEnumerable<Article> GetArticle(int id)
        {
            return db.Article.Where(n => n.WritingID == id);
        }
        public IEnumerable<Article> GetShareArticle(int id)
        {
            return db.Share_Article.Where(n => n.SharedID == id).Select((n)=>n.Article );
        }
        public IEnumerable<Notify> GetNotifyPeople(int id)
        {
            return db.Notify_People.Where(n => n.NotifyPeopleID == id).Select((n) => n.Notify);
        }
        public IEnumerable<string> GetmMyImagesPath(int id)
        {
            return db.Images.Where(n => n.OwnerID == id).Select((n) => n.ImagePath);
        }
        public IEnumerable<string> GetShareImagesPath(int id)
        {
            return db.Share_Images.Where(n => n.StudentID == id).Select(n => n.Images.ImagePath);
        }
        public IEnumerable<Teachers> GetTeacher(int id)
        {
            //找到班级
            var grade = GetGradeID(id);
            //找到老师
            return db.Grades_Teachers.Where(n => n.GradeID == grade).Select((n) => n.Teachers);
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
            return db.Students.Where(n => n.GradeID == grade);
        }
        public IEnumerable<SubjectMaterial> GetSubjectMaterial(int id)
        {
            //找到年级
            var faculty = from c in db.Students
                          where c.ID == id
                          select c.Grade.FacultyID;
            //找到同学
            return db.SubjectMaterial.Where(n => n.FacultyID == faculty.First());
        }
        public IEnumerable<HomeWork> GetHomeWork(int id)
        {
            return db.HomeWork.Where(n => n.StudentID == id);
        }
        public IEnumerable<TimeTable> GetTimeTable(int id)
        {
            var grade = GetGradeID(id);
            return db.TimeTable.Where(n => n.GradeID == grade);
        }
        public IEnumerable<Vacation> GetMyVacation(int id)
        {
            return db.Vacation.Where(n => n.PeopleID == id);
        }
        public IEnumerable<Questions> GetQuestions(int id)
        {
            return db.Questions.Where(n => n.StudentID == id);
        }
        //未实现
        public IEnumerable<Vacation> GetScore(int id)
        {
            return db.Vacation.Where(n => n.PeopleID == id);
        }
        public IEnumerable<Notify> GetAllNotify(int id)
        {
            //找到班级
            var grade = GetGradeID(id);
            //找到班级通知
            var q = from c in db.Notify_Grade
                    where c.Grade.ID == grade
                    select c.Notify;
            //找到属于个人的通知
            var person = from c in db.Notify_People
                         where c.Students.ID == id
                         select c.Notify;
            var res = q.ToList();
            res.AddRange(person.ToList());//2个集合合并
            return res;
        }
        public IEnumerable<Images> GetImagesInfo(int id)
        {
            return db.Images.Where(n => n.OwnerID == id);
        }
        public IEnumerable<Images> GetShareImagesInfo(int id)
        {
            return db.Share_Images.Where(n => n.StudentID == id).Select(n => n.Images);
        }
        public IEnumerable<ArticleComments> GetArticleComments(int id)
        {
            return db.ArticleComments.Where(n => n.AriticleID == id);
        }
        public IEnumerable<ImageComments> GetImagesComments(int id)
        {
            return db.ImageComments.Where(n => n.ImageID == id);
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