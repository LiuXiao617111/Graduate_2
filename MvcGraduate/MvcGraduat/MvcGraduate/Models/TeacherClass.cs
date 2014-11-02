using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MvcGraduate.Models
{
    public class TeacherClass
    {
        DataClassesDataContext db = new DataClassesDataContext();

        public List<HomeWork> GetHomeWork(int teacherID)
        {
            var grades = db.Grades_Teachers.Where(n => n.TeacherID == teacherID).Select(n=>n.Grade);
            List<HomeWork> res = new List<HomeWork>();
            foreach (var grade in grades)
            {
                List<HomeWork> temp = db.Students.Where(n => n.Grade.ID == grade.ID).SelectMany(n => n.HomeWork).ToList() ;
                res.AddRange(temp);
            }
            return res;
        }

        public List<Vacation> GetVacation(int teacherID)
        {
            var grades = db.Grades_Teachers.Where(n => n.TeacherID == teacherID).Select(n => n.Grade);
            List<Vacation> res = new List<Vacation>();
            foreach (var grade in grades)
            {
                List<Vacation> temp = db.Students.Where(n => n.Grade.ID == grade.ID).SelectMany(n => n.Vacation).ToList();
                res.AddRange(temp);
            }
            return res;
        }
        public List<Questions> GetQuestion(int teacherID)
        {
            var grades = db.Grades_Teachers.Where(n => n.TeacherID == teacherID).Select(n => n.Grade);
            List<Questions> res = new List<Questions>();
            foreach (var grade in grades)
            {
                List<Questions> temp = db.Students.Where(n => n.Grade.ID == grade.ID).SelectMany(n => n.Questions).ToList();
                res.AddRange(temp);
            }
            return res;
        }
    }
}