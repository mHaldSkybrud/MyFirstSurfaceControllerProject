using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MyFirstSurfaceControllerProject.Models
{
    public class ImageGalleryFormViewModel
    {
        [Required]
        public int GalleryId { get; set; }
        [Required]
        public IEnumerable<HttpPostedFileBase> Files { get; set; }
    }
}