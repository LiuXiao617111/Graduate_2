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
        TeacherClass tClass=new TeacherClass();
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
        [HttpPost]
        public PartialViewResult GetHomeWork()
        {
            var tt = Session["Teacher"];
            int teacherID = ((Teachers)Session["Teacher"]).ID;
            var res= tClass.GetHomeWork(teacherID);
            return PartialView(res);
        }
        [HttpPost]
        public PartialViewResult GetValidate()
        {
            var tt = Session["Teacher"];
            int teacherID = ((Teachers)Session["Teacher"]).ID;
            var res = tClass.GetVacation(teacherID);
            return PartialView(res);
        }
        [HttpPost]
        public PartialViewResult GetQuestion()
        {
            var tt = Session["Teacher"];
            int teacherID = ((Teachers)Session["Teacher"]).ID;
            var res = tClass.GetQuestion(teacherID);
            return PartialView(res);
        }
    }
}
