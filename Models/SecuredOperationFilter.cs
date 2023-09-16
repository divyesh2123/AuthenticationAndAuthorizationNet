using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AuthenticationAndAuthorization.Models
{
    public class SecuredOperationFilter : ActionFilterAttribute
    {
        public string Roles;
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            string[] roles = Roles.Split(',');
            bool isAuthorizated = false;
            for (int i = 0; i < roles.Length; i++)
            {
               
                if (System.Threading.Thread.CurrentPrincipal.IsInRole(roles[i]))
                {
                    isAuthorizated = true;
                }
            }
            if (!isAuthorizated)
            {
                filterContext.Result = new RedirectResult("/Login/Login");
            }
        }
    }
}