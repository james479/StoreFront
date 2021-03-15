using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;
using System.Web.Mvc;
using StoreFront.UI.MVC.Models;

namespace StoreFront.UI.MVC.Controllers
{
    public class ContactController : Controller
    {
        [HttpPost]
        public JsonResult ContactAjax(ContactViewModel contact)
        {
            string body = $"{contact.Name} has sent you the following message: <br/>{contact.Message} <strong> from the email address:</strong>{contact.Email}";

            MailMessage message = new MailMessage("Admin@codingbyjames.com", "james.e.glover57@gmail.com", "New Message from " + contact.Name, body);

            message.IsBodyHtml = true;

            message.Priority = MailPriority.High;

            message.ReplyToList.Add(contact.Email);

            SmtpClient client = new SmtpClient("mail.codingbyjames.com");

            client.Credentials = new NetworkCredential("Admin@codingbyjames.com", "*James*1979");

            client.Port = 8899;

            try
            {
                client.Send(message);
            }
            catch (Exception ex)
            {
                ViewBag.message = $"We're sorry your request cannot be sent at this time. Please try again later. <br/>Error Message: <br/>{ex.StackTrace}";
            }

            return Json(contact);
        }
    }
}