using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;//For "required" and "EmailAddress" annotations.
using System.Linq;
using System.Web;

namespace MyFirstSurfaceControllerProject.Models
{
    public class ContactFormViewModel
    {
        [Required] //Serverside validation
        public string Name { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        public string Message { get; set; }

    }
}