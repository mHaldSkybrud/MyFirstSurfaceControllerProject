using MyFirstSurfaceControllerProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Umbraco.Core;
using Umbraco.Core.Services;
using Umbraco.Web.Mvc;

namespace MyFirstSurfaceControllerProject.Controllers
{
    public class CommentFormSurfaceController : SurfaceController
    {
        // GET: CommentFormSurface
        public ActionResult Index()
        {
            return PartialView("CommentForm", new CommentFormViewModel());
        }

        [HttpPost]
        public ActionResult HandleFormSubmit(CommentFormViewModel model)
        {
            if (!ModelState.IsValid)
                return CurrentUmbracoPage();

            //post comment
            Udi udi = Udi.Parse(@"umb://document/"+CurrentPage.Key.ToString());//Parse Udi for CreateContent().
            var newComment = Services.ContentService.CreateContent(model.Title, udi, "commentPost");
            newComment.SetValue("title", model.Title);
            newComment.SetValue("author", model.Author);
            newComment.SetValue("email", model.Email);
            //newComment.SetValue("comment", model.Comment);
            newComment.SetValue("comment", model.Comment);
            Services.ContentService.SaveAndPublish(newComment);

            TempData["success"] = true;
            return RedirectToCurrentUmbracoPage();
        }
    }
}