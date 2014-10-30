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
            var res = db.Faculty.Select(n => n.Name);
            return res.ToArray();
        }
    }
}