using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;

namespace uFramework.Security.Attributes
{
    public class AuthorizeAttribute :
            System.Web.Mvc.AuthorizeAttribute
    {
        public string LoginUrl { get; set; }

        public override void OnAuthorization(AuthorizationContext filterContext)
        {
            //Send the return url to the login page
            LoginUrl += "?returnUrl=" + filterContext.HttpContext.Request.RawUrl;

            if (!filterContext.HttpContext.User.Identity.IsAuthenticated)
            {
                filterContext.HttpContext.Response.Redirect(LoginUrl);
            }
            else
            {
                if (Roles != null)
                {
                    if (!filterContext.HttpContext.User.IsInRole(Roles))
                        filterContext.HttpContext.Response.Redirect(LoginUrl);
                }
            }

            base.OnAuthorization(filterContext);
        }
    }
}
