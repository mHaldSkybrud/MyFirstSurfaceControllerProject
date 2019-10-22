//using System;
//using System.Web.Mvc;
//using Umbraco.Web.Mvc;
//using MyFirstSurfaceControllerProject.Models;
//using System.Web;
//using Umbraco.Core;
//using Umbraco.Core.Models;

//namespace MyFirstSurfaceControllerProject.Controllers
//{
//    public class ImageGalleryController : SurfaceController
//    {
//        // GET: ImageGallery
//        public ActionResult Index()
//        {
//            throw new Exception(ModelState.IsValid+"");
//            //var m = new ImageGalleryFormViewModel { GalleryId = galleryId };
//            return PartialView("ImageGalleryForm", new ImageGalleryFormViewModel());

//        }

//        [HttpPost]
//        public ActionResult HandleFormSubmit(ImageGalleryFormViewModel model)
//        {
//            throw new Exception(ModelState.IsValid+"");
//            if (!ModelState.IsValid)
//                return CurrentUmbracoPage();
            
            
//            HttpPostedFileBase file = Request.Files["file"];    
//            //create new images here
//            foreach (var image in model.Files)
//            {
//                //Initialize new image at given location
//                IMedia media = Services.MediaService.CreateMedia(image.FileName, 1088, Constants.Conventions.MediaTypes.Image);

//                //Set property value - most of the work is done by Umbraco
//                media.SetValue(Services.ContentTypeBaseServices, Constants.Conventions.Media.File, image.FileName, image);
//                Services.MediaService.Save(media);
//            }

            
//            TempData["success"] = true;
//            return RedirectToCurrentUmbracoPage();
//        }
//    }
//}


////-----------------------------------Solution from video-----------------------------
////------------------Results in same "path must start..." error-----------------------

////var media = Services.MediaService.CreateMedia(file.FileName, 1088, "Image");
////media.SetValue("umbracoFile", file);

////Services.MediaService.Save(media);

