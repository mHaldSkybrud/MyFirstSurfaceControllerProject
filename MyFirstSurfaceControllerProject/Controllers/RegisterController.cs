using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Umbraco.Web.Mvc;
using Umbraco.Web.Models;
using Umbraco.Core.Services;
using Umbraco.Core;

namespace MyFirstSurfaceControllerProject.Controllers
{
    public class RegisterController : SurfaceController
    {
        // GET: Register

        public ActionResult Index(Models.RegisterModel model)
        //custom properties in the model requires you to specify that it's the custom RegisterModel, and not the
        //default one in Umbraco.Web.Models when passing your model as parameter in the ActionResult method.
        {
            if (!ModelState.IsValid)
            return CurrentUmbracoPage();

            //Member creation code here:
            var memberService = Services.MemberService;

            if(memberService.GetByEmail(model.Email) != null)
            {
                ModelState.AddModelError("", "Member with this email already exists.");
                return CurrentUmbracoPage();
            }


            var member = memberService.CreateMemberWithIdentity(model.Email, model.Email, model.Name, "newsMember");

            member.SetValue("bio", model.Biography);

            //avatar is slightly more complicated, and the below is not shown in Umbraco.tv's tutorial. Was found here:
            //our.umbraco.com/forum/extending-umbraco-and-using-the-api/96345-avatar-image-upload-failed
            member.SetValue(Services.ContentTypeBaseServices, "avatar", model.Avatar.FileName, model.Avatar);

            //In the Umbraco.TV video, Andrew had the following two methods swapped around,
            //so the password was saved before the member. This didn't save the password
            //however, but googling the issue led me to switching it and now this works. 
            memberService.Save(member);
            memberService.SavePassword(member, model.Password);

            //Assigns member to "Premium" group upon registration, thus giving access to the subpage.
            memberService.AssignRole(member.Username, "Premium");


            Members.Login(model.Email, model.Password);
            

            
            return RedirectToCurrentUmbracoPage();

        }
    }
}