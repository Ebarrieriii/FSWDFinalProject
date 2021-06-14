using FSWDFinalProject.UI.MVC.Models;
using System;
using System.Net;
using System.Net.Mail;
using System.Web.Mvc;

namespace FSWDFinalProject.UI.MVC.Controllers
{
    public class HomeController : Controller
    {
        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult Contact()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Contact(ContactViewModel cvm)
        {
            if (!ModelState.IsValid)
            {
                return View(cvm);
            }

            string message = $"You have recieved an email from {cvm.Name} with a subject of + " +
                $"{cvm.Subject}. Respond to {cvm.Email}. Message:<br />{cvm.Message}";

            MailMessage mm = new MailMessage("admin@edwardbarrier.com", "edwardbarrieriii@outlook.com",
               cvm.Subject, message);

            mm.IsBodyHtml = true;

            mm.ReplyToList.Add(cvm.Email);

            SmtpClient client = new SmtpClient("mail.edwardbarrier.com");

            client.Credentials = new NetworkCredential("admin@edwardbarrier.com", "Cam#Jay#71#");

            try
            {
                client.Send(mm);
            }
            catch (Exception ex)
            {

                ViewBag.ErrorMessage = $"Your message could not be sent at this time. Please" +
                    $"try again later. <br />Error message:<br />{ex.Message}.";

                return View(cvm);
            }


            return View("EmailConfirmation", cvm);
        }
    }
}
