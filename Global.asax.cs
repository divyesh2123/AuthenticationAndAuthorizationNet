using AuthenticationAndAuthorization.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Threading;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using System.Web.Security;

namespace AuthenticationAndAuthorization
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }

        public override void Init()
        {
            PostAuthenticateRequest += MvcApplication_PostAuthenticateRequest;
            base.Init();
        }

        private void MvcApplication_PostAuthenticateRequest(object sender, EventArgs e)
        {
            try
            {
                var authCookie = HttpContext.Current.Request.Cookies[FormsAuthentication.FormsCookieName];
                if (authCookie == null)
                {
                    return;
                }

                var encTicket = authCookie.Value;
                if (String.IsNullOrEmpty(encTicket))
                {
                    return;
                }

                var ticket = FormsAuthentication.Decrypt(encTicket); // bileti çöz.
                var securityUtilities = new SecurityUtilities();
                var identity = securityUtilities.FormsAuthTicketToIdentity(ticket); // Çözülmüþ ticket'dan identity oluþturuyoruz.
                var principal = new GenericPrincipal(identity, identity.Roles); // principal oluþturuyoruz.

                HttpContext.Current.User = principal; // Web'de kullaným için.
                Thread.CurrentPrincipal = principal; // Back-end' de kullaným için.
            }
            catch (Exception)
            {
            }
        }
    }
}
