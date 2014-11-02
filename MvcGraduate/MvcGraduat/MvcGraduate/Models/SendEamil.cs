using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;
using System.Xml;

namespace MvcGraduate.Models
{
    public class SendEamil
    {
        //返回是否成功
        public static bool CreateCodeMessage(string loginId,string uri,string ToEmail)
        {
            OperateClass oClass=new OperateClass();
            ReadXml();
            string parms = RndNum();//验证码
            if (oClass.AddVerificationCode(loginId, parms,ToEmail))
            {
                uri = String.Format("{0}?id={1}&parms={2}",uri,loginId,parms);
                string sendInfo = String.Format(body, uri);//重新组织信息
                try
                {
                    SmtpClient client = new SmtpClient();
                    client.Host = "smtp.163.com";
                    MailMessage mm = new MailMessage();
                    client.Port = 25;
                    mm.From = new MailAddress(from);
                    mm.To.Add(new MailAddress(ToEmail));
                    mm.Subject = subject;//标题
                    mm.Body = sendInfo;//信息体
                    mm.IsBodyHtml = false;
                    mm.Priority = MailPriority.High;//优先级

                    client.Credentials = new NetworkCredential(account, pwd);//发件人凭证
                    client.Send(mm);
                    return true;
                }
                catch (Exception ex)
                {
                    string str = ex.ToString();
                }
            }
            return false; 
        }

        private static string RndNum()
        {
            int number;
            char code;
            string checkCode = String.Empty;

            System.Random random = new Random();

            for (int i = 0; i < 8; i++)
            {
                number = random.Next();
                if (number % 2 == 0)
                    code = (char)('0' + (char)(number % 10));
                else
                    code = (char)('A' + (char)(number % 26));
                checkCode += code.ToString();
            }
            //Response.Cookies.Add(new HttpCookie("checkcode", checkCode));
            string res=System.Web.Security.FormsAuthentication.HashPasswordForStoringInConfigFile(checkCode, "MD5");//加密
            return res;
        }
        static string from;
        static string subject;
        static string body;
        static string account;
        static string pwd;
        private static void ReadXml()
        {
            XmlDocument document = new XmlDocument();
            try
            {
                document.Load(System.Web.HttpContext.Current.Server.MapPath("~/App_Data/emailConfig.xml"));

                XmlElement root = document.DocumentElement;
                from = root.SelectSingleNode("From").InnerText;
                subject = root.SelectSingleNode("Subject").InnerText;
                body = root.SelectSingleNode("Body").InnerText;
                account = root.SelectSingleNode("Account").InnerText;
                pwd = root.SelectSingleNode("Pwd").InnerText;
            }
            catch{}
        }

    }
}