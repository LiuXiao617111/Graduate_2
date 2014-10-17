using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MvcGraduate.Models
{
    public class StudyClass
    {
        private DataClassesDataContext db = new DataClassesDataContext();
        public IEnumerable<HomeWork> GetHomeWork(int id)
        {
            var q = from c in db.HomeWork
                where c.StudentID == id
                select c;
            if (!q.Any())
                return null;
            return q;
        }
        public IEnumerable<TimeTable> GetTimeTable(int id)
        {
            var grade = from c in db.Students
                where c.ID == id
                select c.GradeID;
            var q = from c in db.TimeTable
                    where c.GradeID == grade.First()
                    select c;
            if (!q.Any())
                return null;
            return q;
        }
        public IEnumerable<Vacation> GetMyVacation(int id)
        {
            var q = from c in db.Vacation
                    where c.PeopleID == id
                    select c;
            if (!q.Any())
                return null;
            return q;
        }
        //未实现
        public IEnumerable<Vacation> GetScore(int id)
        {
            var q = from c in db.Vacation
                    where c.PeopleID == id
                    select c;
            if (!q.Any())
                return null;
            return q;
        }
    }
}