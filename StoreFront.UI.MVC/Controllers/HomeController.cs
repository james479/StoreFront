using System.Web.Mvc;
using StoreFront.DATA.EF;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Collections.Generic;
using StoreFront.UI.MVC.Models;
using System.Net.Mail;
using System;

namespace StoreFront.UI.MVC.Controllers
{
    public class HomeController : Controller
    {
        private StoreFrontEntities1 db = new StoreFrontEntities1();

        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        [Authorize]
        public ActionResult About()
        {
            ViewBag.Message = "Your app description page.";

            return View();
        }

        [HttpGet]
        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        [HttpPost]
        public JsonResult ContactAjax(ContactViewModel contact)
        {
            string body = $"{contact.Name} has sent you the following message: <br/>{contact.Message} <strong> from the email address:</strong>{contact.Email}";

            MailMessage m = new MailMessage("Admin@codingbyjames.com", "james.e.glover57@gmail.com", contact.Subject, body);

            m.IsBodyHtml = true;

            m.Priority = MailPriority.High;

            m.ReplyToList.Add(contact.Email);

            SmtpClient client = new SmtpClient("mail.codingbyjames.com");

            client.Credentials = new NetworkCredential("Admin@codingbyjames.com", "*James*1979");

            client.Port = 8889;

            try
            {
                client.Send(m);
            }
            catch (Exception ex)
            {
                ViewBag.message = $"We're sorry your request cannot be sent at this time. Please try again later. <br/>Error Message: <br/>{ex.StackTrace}";
            }

            return Json(contact);
        }
    }
}
