using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Optimization;
using System.Web.Routing;
using System.Web.Security;
using System.Web.SessionState;

using System.ComponentModel;
using System.Security.Principal;

namespace SqlServerWebAdmin
{
    public class Global : HttpApplication
    {
        void Application_Start(object sender, EventArgs e)
        {
            // Code that runs on application startup
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }
        protected void Application_Error(Object sender, EventArgs e)
        {
            Application.Add("Error", Server.GetLastError());
            // If an error occurs anywhere in the application, it will be saved
            // as an application variable, which can be easily displayed
            // by redirecting the user to the Error.aspx page.
        }

        protected void Application_AuthenticateRequest(Object sender, EventArgs e)
        {

            if (Context.User == null)
            {

                String cookieName = FormsAuthentication.FormsCookieName;
                HttpCookie authCookie = Context.Request.Cookies[cookieName];

                if (null == authCookie)
                {
                    //There is no authentication cookie.
                    return;
                }
                FormsAuthenticationTicket authTicket = null;
                try
                {
                    authTicket = FormsAuthentication.Decrypt(authCookie.Value);
                }
                catch (Exception ex)
                {
                    //Write the exception to the Event Log.
                    return;
                }
                if (null == authTicket)
                {
                    //Cookie failed to decrypt.
                    return;
                }

                string[] loginType = authTicket.UserData.Split(new char[] { ',' }); ;
                GenericIdentity id = new GenericIdentity(authTicket.Name, "webAuth");
                //This principal flows throughout the request.
                GenericPrincipal principal = new GenericPrincipal(id, loginType);
                Context.User = principal;


            }
            //Context.User = (System.Security.Principal.IPrincipal)System.Security.Principal.WindowsIdentity.GetCurrent();

        }
    }
}