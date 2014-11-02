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
        //验证回执
        public PartialViewResult ResetPwd(string id, string parms)
        {
            if (oClass.AddReaptCode(id, parms))
            {
                ViewBag.AccountID = id;
                return PartialView("ResetPwd");
            }
            return PartialView("Error", "Shared");
        }
        public PartialViewResult Register()
        {
            return PartialView();
        }
        public PartialViewResult Details_Register()
        {
            //年纪
            ViewBag.FacultyList = oClass.GetFacultySelectList();
            //班级
            ViewBag.GradeList = oClass.GetGradeSelectList();
            return PartialView();
        }
        public PartialViewResult ForgetAccount()
        {
            return PartialView();
        }
        public void ValidateImage()
        {
            ValidateImage image = new ValidateImage();
            string randCode = image.RndNum();
            Session["RandCode"] = randCode;

            var ms = image.CreateCheckCodeImage(randCode);
            Response.ClearContent();
            Response.ContentType = "image/Gif";
            Response.BinaryWrite(ms.ToArray());
        }

        #region HttpPost

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
        [HttpPost]
        public RedirectToRouteResult Register(FormCollection form)
        {
            if (oClass.IsExistStudent(form["name"]))
            {
                return RedirectToAction("Details_Register", "Home");
            }
            else
            {
                if (oClass.AddAccount(form))
                {
                    return RedirectToAction("Index", "Home");
                }
                return RedirectToAction("Error", "Shared");
            }
        }
        [HttpPost]
        public bool AddStudents(Students stu)
        {
            return oClass.AddStudent(stu);
        }
        [HttpPost]
        public int IsHaveCount(string loginID, string validateCode)
        {
            var code = Session["RandCode"].ToString().ToUpper();
            if (validateCode.ToUpper() != code)
                return 1;//验证码错误
            if (!oClass.ISHaveCount(loginID))
                return 2;//账户或邮箱不存在
            return 0;
        }
        [HttpPost]
        public bool ValidateCode(string validateCode)
        {
            var session = Session["RandCode"].ToString().ToUpper();
            if (validateCode == null)
                return false;
            return session == validateCode.ToUpper();
        }
        [HttpPost]
        public string SendEmailToGueat(string loginID,string email)
        {
            string host=Request.Url.Host;
            int port = Request.Url.Port;
            string uri = host + ":" + port  + Url.Action("ResetPwd", "Account");
            if (SendEamil.CreateCodeMessage(loginID, uri, email))
            { 
                int start=email.IndexOf('@');
                int dian=email.LastIndexOf('.');
                string str = email.Substring(start, dian-start);
                var res=str.ToLower();
                if (res == "@qq")
                    return @"https://mail.qq.com/cgi-bin/loginpage";
                if (res == "@163")
                    return @"http://mail.163.com/";
                if (res == "@gmail")
                    return @"https://mail.google.com/";
            }
            return "false";
        }
        [HttpPost]
        public bool ChangePwd(string loginID,string pwd)
        {
            return oClass.ChangePwd(loginID, pwd);
        }
        #endregion
    }
}
