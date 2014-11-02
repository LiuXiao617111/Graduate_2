using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MvcGraduate.Models
{
    public class UEditorClass
    {
        DataClassesDataContext db = new DataClassesDataContext();
        public Array FacultyInfo()
        {
            var res = db.Faculty.Select(n => n.Name.Trim());
            return res.ToArray();
        }
        public Array FacultyPeopleInfo(string parm="男")
        {
            var faculty = db.Faculty.Select(n => n.Grade);
            var res = new List<int>();
            foreach (var item in faculty)
            {
                int facultyCount = 0;
                foreach (var grade in item)
                {
                    if (parm == "总数")
                    {
                        facultyCount += grade.Students.Count;
                        continue;
                    }
                    foreach (var stu in grade.Students)
                    {
                        if (stu.Sex != null)
                        {
                            if (stu.Sex.Trim() == parm)
                            {
                                facultyCount++;
                            }
                        }
                    }
                }
                res.Add(facultyCount);
            }
            return res.ToArray();
        }


        public Array TeacherDutiesInfo()
        {
            return (from c in db.Teachers
                    select c.Duty.Trim()).Distinct().ToArray();
        }

        public Array TeacherDutiesCount(string sex="男")
        {
            var duties = (from c in db.Teachers
                         select c.Duty.Trim()).Distinct();

            List<int> res = new List<int>();
            foreach (var duty in duties)
            {
                int count = 0;
                foreach (var item in db.Teachers)
                {
                    if (item.Duty.Trim()==duty.Trim() && item.Sex.Trim() == sex)
                        count++;
                }
                res.Add(count);
            }
            return res.ToArray();
        }
    }
}