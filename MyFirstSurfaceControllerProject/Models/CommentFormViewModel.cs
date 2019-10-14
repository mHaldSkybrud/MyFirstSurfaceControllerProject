using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MyFirstSurfaceControllerProject.Models
{
    public class CommentFormViewModel
    {

        [Required]
        public string Title { get; set; }
        [Required]
        public string Author { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        public string Comment { get; set; }

    }
}