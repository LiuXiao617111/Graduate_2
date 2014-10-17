using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MvcGraduate.Models
{
    public class NotifyClass
    {
        DataClassesDataContext db = new DataClassesDataContext();
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
        private int GetGradeID(int id)
        {
            return db.Students.Single(n => n.ID == id).GradeID;
        }
    }
}