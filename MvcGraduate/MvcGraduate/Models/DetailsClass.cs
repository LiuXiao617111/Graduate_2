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
        public HomeWork HomeWork_Details(int id)
        {
            //var tt=from c in db.Vacation
            //       select c.State
            var res=db.HomeWork.SingleOrDefault(n => n.ID == id);
            return res;
        }
        public Vacation Vacation_Details(int id)
        {
            var res = db.Vacation.SingleOrDefault(n => n.ID == id);
            return res;
        }
    }
}