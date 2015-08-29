using Microsoft.SqlServer.Management.Smo;
using SqlServerWebAdmin.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SqlServerWebAdmin
{
    public partial class DeleteStoredProcedure : System.Web.UI.Page
    {
        public DeleteStoredProcedure()
        {
            Page.Init += new System.EventHandler(Page_Init);
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            Microsoft.SqlServer.Management.Smo.Server server = DbExtensions.CurrentServer;
            try
            {
                server.Connect();
            }
            catch (System.Exception ex)
            {
                //Response.Redirect("Error.aspx?errorPassCode=" + 2002);
                Response.Redirect(String.Format("error.aspx?errormsg={0}&stacktrace={1}", Server.UrlEncode(ex.Message), Server.UrlEncode(ex.StackTrace)));
            }

            Database database = server.Databases[HttpContext.Current.Server.HtmlDecode(HttpContext.Current.Request["database"])];

            DatabaseNameLabel.Text = database.Name;
            SProcNameLabel.Text = Server.HtmlEncode(Request["sproc"]);
            server.Disconnect();
        }

        protected void Page_Init(object sender, EventArgs e)
        {
            if (Page.User.Identity.IsAuthenticated)
            {
                Page.ViewStateUserKey = Page.Session.SessionID;
            }
        }

        protected void YesButton_Click(object sender, System.EventArgs e)
        {
            Microsoft.SqlServer.Management.Smo.Server server = DbExtensions.CurrentServer;
            try
            {
                server.Connect();
            }
            catch (System.Exception ex)
            {
                //Response.Redirect("Error.aspx?errorPassCode=" + 2002);
                Response.Redirect(String.Format("error.aspx?errormsg={0}&stacktrace={1}", Server.UrlEncode(ex.Message), Server.UrlEncode(ex.StackTrace)));
            }

            Database database = server.Databases[HttpContext.Current.Server.HtmlDecode(HttpContext.Current.Request["database"])];

            StoredProcedure sproc = database.StoredProcedures[Request["sproc"]];
            if (sproc == null)
            {
                server.Disconnect();

                // Stored procedure doesn't exist - break out and go to error page
                Response.Redirect(String.Format("error.aspx?error={0}", 1001));
                return;
            }

            // Delete the sproc
            sproc.Drop();

            server.Disconnect();

            // Redirect to info page
            Response.Redirect("storedprocedures.aspx?database=" + Server.UrlEncode(Request["database"]));
        }

        protected void NoButton_Click(object sender, System.EventArgs e)
        {
            // Redirect to info page
            Response.Redirect("storedprocedures.aspx?database=" + Server.UrlEncode(Request["database"]));
        }
    }
}