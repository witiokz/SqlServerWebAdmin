using Microsoft.SqlServer.Management.Smo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;

namespace SqlServerWebAdmin.Models
{
    public class Models
    {
    }

    public enum SqlLoginType
    {
        /// <summary>
        /// The login is an NT User
        /// </summary>
        NTUser = 0,

        /// <summary>
        /// The login is an NT Groups
        /// </summary>
        NTGroup = 1,

        /// <summary>
        /// The login is a SQL Server login
        /// </summary>
        Standard = 2
    }

    public enum SqlObjectType
    {
        /// <summary>
        /// Indicates that the value is not set.
        /// </summary>
        NotSet = 0,

        /// <summary>
        /// Indicates that the object was created by a user.
        /// </summary>
        User = 1,

        /// <summary>
        /// Indicates that the object is a system object.
        /// </summary>
        System = 2,

        /// <summary>
        /// Indicates that the object is a database.
        /// </summary>
        Database = 135168,

        /// <summary>
        /// Indicates that the object is a stored procedure
        /// </summary>
        StoredProcedure = 16,

        /// <summary>
        /// Indicates that the object is a System object. In this case a System Table
        /// </summary>
        SystemTable = System,

        /// <summary>
        /// Indicates that the object is a user table.
        /// </summary>
        UserTable = 8,

        /// <summary>
        /// Indicates that the object is a view.
        /// </summary>
        View = 4,
    }


    public class AdminUser
    {

        public AdminUser(string username, string password, string server, bool useIntegratedSecurity)
        {
            this.username = username;
            this.password = password;
            this.server = server;
            this.useIntegratedSecurity = useIntegratedSecurity;
        }

        private string username;
        /// <summary>
        /// The Sql Username used for this user on the selected server.
        /// </summary>
        public string Username
        {
            get { return username; }
        }

        private string password = String.Empty;
        /// <summary>
        /// The Sql Password used for this user on the selected server.
        /// </summary>
        /// <remarks>
        /// This property is not used for Integrated Security.
        /// </remarks>
        public string Password
        {
            get { return password; }
        }

        private string server;
        /// <summary>
        /// The Sql Server this user is administering.
        /// </summary>
        public string Server
        {
            get { return server; }
        }

        private bool useIntegratedSecurity;
        /// <summary>
        /// Determines whether or not to use Integrated Security for the selected servers Authentication.
        /// </summary>
        public bool UseIntegratedSecurity
        {
            get { return useIntegratedSecurity; }
        }


        public static AdminUser CurrentUser
        {
            get { return HttpContext.Current.Session["CurrentUser"] as AdminUser; }
            set { HttpContext.Current.Session["CurrentUser"] = value; }
        }
    }

    public class DbUtlity
    {
        public static void WriteCookieForFormsAuthentication(string username, string password, bool persist, SqlLoginType loginType)
        {
            //Create the ticket, and add the groups.
            bool isCookiePersistent = persist;
            string userData = null;
            userData = password + "," + loginType.ToString();
            FormsAuthenticationTicket authTicket = new FormsAuthenticationTicket(1, username, DateTime.Now, DateTime.Now.AddMinutes(60), isCookiePersistent, userData);

            //Encrypt the ticket.
            String encryptedTicket = FormsAuthentication.Encrypt(authTicket);

            //Create a cookie, and then add the encrypted ticket to the cookie as data.
            HttpCookie authCookie = new HttpCookie(FormsAuthentication.FormsCookieName, encryptedTicket);

            if (true == isCookiePersistent)
                authCookie.Expires = authTicket.Expiration;

            //Add the cookie to the outgoing cookies collection.
           HttpContext.Current.Response.Cookies.Add(authCookie);

        }

        public static Server Connect()
        {
                if (AdminUser.CurrentUser == null)
                {
                    HttpContext.Current.Response.Redirect("~/Default.aspx?error=sessionexpired");
                }

                if (AdminUser.CurrentUser.UseIntegratedSecurity)
                {
                    Server server = new Server(AdminUser.CurrentUser.Server);
                    server.ConnectionContext.LoginSecure = true;
                    return server;
                }
                else
                {
                    Server server = new Server(AdminUser.CurrentUser.Server);
                    server.ConnectionContext.LoginSecure = false;
                    server.ConnectionContext.Login = AdminUser.CurrentUser.Username;
                    server.ConnectionContext.Password = AdminUser.CurrentUser.Password;
                    return server;
                }
            
        }


        public static void Disconnect(Server server)
        {
            if (server.ConnectionContext.IsOpen)
            {
                server.ConnectionContext.Disconnect();
            }

        }
    }

    public static class DbExtensions
    {
        public static void Disconnect(this Server server)
        {
            if (server.ConnectionContext.IsOpen)
            {
                server.ConnectionContext.Disconnect();
            }
        }

        public static void Connect(this Server server)
        {
            server.ConnectionContext.Connect();
        }

        public static Server CurrentServer
        {
            get
            {
                if (AdminUser.CurrentUser == null)
                {
                    HttpContext.Current.Response.Redirect("~/Default.aspx?error=sessionexpired");
                }

                if (AdminUser.CurrentUser.UseIntegratedSecurity)
                {
                    var server = new Server(AdminUser.CurrentUser.Server);
                    server.ConnectionContext.LoginSecure = true;
                    return server;
                }
                else
                {
                    var server = new Server(AdminUser.CurrentUser.Server);
                    server.ConnectionContext.LoginSecure = false;
                    server.ConnectionContext.Login = AdminUser.CurrentUser.Username;
                    server.ConnectionContext.Password = AdminUser.CurrentUser.Password;
                    return server;
                }
            }

        }


    }



}