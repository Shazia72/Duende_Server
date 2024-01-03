using Microsoft.AspNetCore.Mvc.Filters;
using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace CMSApplication
{
    public class AuthorizationFilter : Attribute, IAuthorizationFilter
    {
        public void OnAuthorization(AuthorizationFilterContext context)
        {

            var result = false;
            if (context.HttpContext.Session.GetString("Token") != null) result = true;
            if (!result)
            {
                //context.ModelState.AddModelError("Unauthorize", "You are not authorize to see this result please login");
                //context.Result = new UnauthorizedObjectResult(context.ModelState);
                context.Result = new ViewResult
                {
                    ViewName = "~/Views/Shared/Error.cshtml"
                };
            }
        }
    }
}
