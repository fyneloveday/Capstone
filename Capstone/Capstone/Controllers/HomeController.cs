using Capstone.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;

namespace Capstone.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult SendEmail()
        {
            return View();
        }

        [HttpPost]
        public ActionResult SendEmail(SendEmailModel obj)
        {
            try
            {
                WebMail.SmtpServer = "smtp.gmail.com";
                WebMail.SmtpPort = 587;
                WebMail.SmtpUseDefaultCredentials = true;
                WebMail.EnableSsl = true;
                WebMail.UserName = "fynecode@gmail.com";
                WebMail.Password = "codemaster";

                WebMail.From = "fynecode@gmail.com";

                WebMail.Send(to: obj.ToEmail, subject: obj.EmailSubject, body: obj.EmailBody, cc: obj.EmailCC, bcc: obj.EmailBCC, isBodyHtml: true);
                ViewBag.status = "Email Sent Successfully.";
        }
            catch (Exception)
            {
                ViewBag.Status = "Problem while sending email, Please check details.";
            }
            return View();
            }
        

    }
}