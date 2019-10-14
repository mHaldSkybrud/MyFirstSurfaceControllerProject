using System;
using System.Web.Mvc;
using Umbraco.Web.Mvc;
using MyFirstSurfaceControllerProject.Models;
using System.IO;
using System.Web;
using Umbraco.Core;
using System.Net.Http;
using System.Collections.Generic;
using Umbraco.Core.Services;
using Umbraco.Core.Services.Implement;
using Umbraco.Web.WebApi;
using System.Net;
using Umbraco.Core.Models;

namespace MyFirstSurfaceControllerProject.Controllers
{
    public class ImageGalleryController : SurfaceController
    {
        // GET: ImageGallery
        public ActionResult Index()
        {
            //var m = new ImageGalleryFormViewModel { GalleryId = galleryId };
            return PartialView("ImageGalleryForm", new ImageGalleryFormViewModel());

        }

        [HttpPost]
        public ActionResult HandleFormSubmit(ImageGalleryFormViewModel model)
        {
            if (!ModelState.IsValid)
                return CurrentUmbracoPage();

            //create new images here
            foreach (var file in model.Files)
            {
                var media = Services.MediaService.CreateMedia(file.FileName, 1088, "Image");
                media.SetValue("umbracoFile", file);

                Services.MediaService.Save(media);
            }

            TempData["success"] = true;
            return RedirectToCurrentUmbracoPage();
        }
    }
}



//-----------------------------------Alternatives - none have worked so far---------------------------------------------------------------
//-------------------------Solution for testing - Ends up with the same "Path must start.." error---------------

//using (var fileStream = System.IO.File.Create(HttpContext.Current.Server.MapPath("~/App_Data/AcadreToUmbraco7/" + id + "." + part)))
//{
//    HttpContext.Current.Request.InputStream.CopyTo(fileStream);
//}

//var media = Services.MediaService.CreateMedia(agendaItemEnclosureFileName, int.Parse(mediaFolderId), ((extension == ".jpg" || extension == ".jpeg" || extension == ".gif" || extension == ".bmp" || extension == ".png" || extension == ".tif" || extension == ".tiff") ? "Image" : "File"));
//using (var fs = System.IO.File.OpenRead(HttpContext.Current.Server.MapPath("/App_Data/AcadreToUmbraco7/" + agendaItemEnclosureId)))
//{
//    media.SetValue("umbracoFile", agendaItemEnclosureFileName, fs);
//}
//Services.MediaService.Save(media, AcadreConfig.UserId);


//--------------The above after adjusting, same "path must start..." error"---------------------------

//Saves a file with the right name and file extension in the specified folder. 
//It is however *not* possible to open it as an image.
//using (var fileStream = System.IO.File.Create(HttpContext.Server.MapPath("~/Media/TestFolder/"+file.FileName)))
//{
//    HttpContext.Request.InputStream.CopyTo(fileStream);
//}


//var media = Services.MediaService.CreateMedia(file.FileName, 1088, "Image");
//using (var fs = System.IO.File.OpenRead(HttpContext.Server.MapPath("/Media/TestFolder/"+file.FileName)))
//{
//    media.SetValue("umbracoFile", fs);
//}
//Services.MediaService.Save(media);




//-----------------------------------Solution from video-----------------------------
//------------------Results in same "path must start..." error-----------------------

//var media = Services.MediaService.CreateMedia(file.FileName, 1088, "Image");
//media.SetValue("umbracoFile", file);

//Services.MediaService.Save(media);




//-----------------------------------Solution from google----------------------------
//----------------Results in IIS error, HttpPostedFileWrapper------------------------

//var media = Services.MediaService.CreateMedia(file.FileName, Guid.Parse("5fbf4a9f-813e-43e5-9a56-5101b1f2ce3b"), "File");

//FileStream s = new FileStream(file.ToString(), FileMode.Open);
//media.SetValue("umbracoFile", Path.GetFileName(file.ToString()), s.ToString()); //automatically populates all the media properties.
//s.Close();

//Services.MediaService.Save(media);//persists the save




//----------------------------- Claimed solution in umbraco 8 with API Controller--------------------------------
//private readonly IContentTypeBaseServiceProvider _contentTypeBaseServiceProvider;
//private readonly IMediaService _mediaService;
//public ImageGalleryController(IContentTypeBaseServiceProvider contentTypeBaseServiceProvider, IMediaService mediaService)
//{
//    _contentTypeBaseServiceProvider = contentTypeBaseServiceProvider;
//    _mediaService = mediaService;
//}

//[HttpPost]
//public HttpResponseMessage HandleFormSubmit(string name)
//{
//    HttpResponseMessage result;
//    var httpRequest = HttpContext.Request;

//    if (httpRequest.Files.Count > 0)
//    {
//        var fileLocations = new List<string>();
//        foreach (string file in httpRequest.Files)
//        {
//            var fileIn = httpRequest.Files[file];

//            if (fileIn != null)
//            {
//                var ms = new MediaService();
//                var ctbsp = new ContentTypeBaseServiceProvider();
//                var newFile = Services.MediaService.CreateMedia(name, -1, "File");
//                newFile.SetValue(ctbsp, "umbracoFile", fileIn.FileName, fileIn.InputStream);
//                ms.Save(newFile);
//                fileLocations.Add(newFile.GetUrl("umbracoFile", Logger));
//            }
//        }
//        result = Request.CreateResponse(HttpStatusCode.Created, fileLocations);
//    }
//    else
//    {
//        result = Request.CreateResponse(HttpStatusCode.BadRequest);
//    }
//    return result;
//}