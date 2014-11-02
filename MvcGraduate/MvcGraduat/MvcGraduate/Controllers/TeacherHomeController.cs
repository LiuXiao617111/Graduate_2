using MvcGraduate.Models;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Collections;

namespace MvcGraduate.Controllers
{
    public class TeacherHomeController : Controller
    {
        UEditorClass uEditor = new UEditorClass();
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Manager()
        {
            return PartialView();
        }
        [HttpPost]
        public JsonResult DataOfECharts() 
        {
            var res = new Hashtable();
            res.Add("data", uEditor.FacultyInfo());
            res.Add("Boys",uEditor.FacultyPeopleInfo());
            res.Add("Girls", uEditor.FacultyPeopleInfo("女"));
            res.Add("FacultyStudtens", uEditor.FacultyPeopleInfo("总数"));

            res.Add("TeacherDuties", uEditor.TeacherDutiesInfo());
            res.Add("TeacherMan", uEditor.TeacherDutiesCount());
            res.Add("TeacherWoman", uEditor.TeacherDutiesCount("女"));

            return Json(res);
        }
    }
}
