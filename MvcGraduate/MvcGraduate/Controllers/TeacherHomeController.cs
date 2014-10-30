using MvcGraduate.Models;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MvcGraduate.Controllers
{
    public class TeacherHomeController : Controller
    {
        UEditorClass uEditor = new UEditorClass();
        public ActionResult Index()
        {
            ViewBag.Temp = uEditor.FacultyInfo();
            ViewBag.Length = uEditor.FacultyInfo().Length;
            return View();
        }
        public ActionResult Manager()
        {
            return PartialView();
        }
    }
}
