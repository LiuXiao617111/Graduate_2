using MvcGraduate.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MvcGraduate.Controllers
{
    public class AccountController : Controller
    {
        OperateClass oClass = new OperateClass();
        public PartialViewResult Index()
        {
            return PartialView();
        }
        public PartialViewResult Register()
        {
            return PartialView();
        }
        [HttpPost]
        public RedirectToRouteResult ValidateCount(FormCollection form)
        {
            var res=oClass.ValidateCount(form);
            if (res!=null)
            {
                Session["User"] = res;
                return RedirectToAction("Index", "Home");
            }
            else
            {
                return RedirectToAction("Index");
            }
        }
    }
}
