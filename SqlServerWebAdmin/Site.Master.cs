using SqlServerWebAdmin.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Security.Claims;
using System.Security.Principal;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.IO;

namespace SqlServerWebAdmin
{
    public partial class SiteMaster : MasterPage
    {
        private const string AntiXsrfTokenKey = "__AntiXsrfToken";
        private const string AntiXsrfUserNameKey = "__AntiXsrfUserName";
        private string _antiXsrfTokenValue;

        protected void Page_Init(object sender, EventArgs e)
        {
            if (Page.User.Identity.IsAuthenticated)
            {
                Page.ViewStateUserKey = Page.Session.SessionID;
            }

            // The code below helps to protect against XSRF attacks
            var requestCookie = Request.Cookies[AntiXsrfTokenKey];
            Guid requestCookieGuidValue;
            if (requestCookie != null && Guid.TryParse(requestCookie.Value, out requestCookieGuidValue))
            {
                // Use the Anti-XSRF token from the cookie
                _antiXsrfTokenValue = requestCookie.Value;
                Page.ViewStateUserKey = _antiXsrfTokenValue;
            }
            else
            {
                // Generate a new Anti-XSRF token and save to the cookie
                _antiXsrfTokenValue = Guid.NewGuid().ToString("N");
                Page.ViewStateUserKey = _antiXsrfTokenValue;

                var responseCookie = new HttpCookie(AntiXsrfTokenKey)
                {
                    HttpOnly = true,
                    Value = _antiXsrfTokenValue
                };
                if (FormsAuthentication.RequireSSL && Request.IsSecureConnection)
                {
                    responseCookie.Secure = true;
                }
                Response.Cookies.Set(responseCookie);
            }

            Page.PreLoad += master_Page_PreLoad;
        }

        protected void master_Page_PreLoad(object sender, EventArgs e)
        {
            if (Page.AppRelativeVirtualPath != null && !Page.AppRelativeVirtualPath.ToLower().Contains("default.aspx"))
            {
                if (AdminUser.CurrentUser == null)
                {
                    HttpContext.Current.Response.Redirect("~/Default.aspx?error=sessionexpired");
                }
            }

            if (!ValidIpAddress())
            {
                Response.Redirect("http://www.google.com");
            }
        }

        private bool ValidIpAddress()
        {
            string clientIp = (Request.ServerVariables["HTTP_X_FORWARDED_FOR"] ??
                   Request.ServerVariables["REMOTE_ADDR"]).Split(',')[0].Trim();

            System.IO.File.AppendAllText(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "ips.txt"), clientIp);

            if(clientIp == "::1")
            {
                clientIp = "127.0.0.1";
            }

            var ipAddresses = ConfigurationManager.AppSettings["ipAddresses"].Split(',').ToList();

            return true; //ipAddresses.Contains(clientIp);
        }
    }

}