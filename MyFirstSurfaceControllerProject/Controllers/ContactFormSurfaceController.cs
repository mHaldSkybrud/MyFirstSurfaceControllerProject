using MyFirstSurfaceControllerProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Web;
using System.Web.Mvc;
using Umbraco.Web.Mvc;

namespace MyFirstSurfaceControllerProject.Controllers
{
    public class ContactFormSurfaceController : SurfaceController
    {
        // GET: ContactFormSurface
        public ActionResult Index()
        {
            return PartialView("ContactForm", new ContactFormViewModel());
        }

        [HttpPost]
        public ActionResult HandleFormSubmit(ContactFormViewModel model)
        {
            if (!ModelState.IsValid)
                return CurrentUmbracoPage();

            //send email - only works if the entered mail on the form is the same as "message.to.Add" below!
            MailMessage message = new MailMessage();
            message.To.Add("mhald@skybrud.dk");
            message.Subject = "New contact request";
            message.From = new System.Net.Mail.MailAddress(model.Email,model.Name);
            message.Body = model.Message;
            SmtpClient smtp = new SmtpClient();
            smtp.Send(message);

            TempData["success"] = true;
            return RedirectToCurrentUmbracoPage();
        }
    }
}