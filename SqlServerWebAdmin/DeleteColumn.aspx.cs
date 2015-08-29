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
    public partial class DeleteColumn : System.Web.UI.Page
    {
        public DeleteColumn()
        {
            Page.Init += new System.EventHandler(Page_Init);
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            // Note: While no security is checked at this point, it is impossible
            // for the delete operation to execute without authentication.
            TableNameLabel.Text = Server.HtmlEncode(Request["table"]);
            ColumnNameLabel.Text = Server.HtmlEncode(Request["column"]);
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

            Microsoft.SqlServer.Management.Smo.Table table = database.Tables[Request["table"]];
            if (table == null)
            {
                server.Disconnect();

                // Table doesn't exist - break out and go to error page
                Response.Redirect(String.Format("error.aspx?error={0}", 1002));
                return;
            }

            if (table.Columns.Count == 1)
            {
                server.Disconnect();
                throw new Exception("Cannot delete last column from table. A table must contain at least one column.");
            }

            // Select column from table
            Column column = table.Columns[Request["column"]];
            if (column == null)
            {
                server.Disconnect();

                // Column doesn't exist - break out and go to error page
                Response.Redirect(String.Format("error.aspx?error={0}", 1003));
                return;
            }

            // Delete the sproc
            column.Drop();

            server.Disconnect();

            // Redirect to info page
            Response.Redirect(String.Format("columns.aspx?database={0}&table={1}", Server.UrlEncode(Request["database"]), Server.UrlEncode(Request["table"])));
        }

        protected void NoButton_Click(object sender, System.EventArgs e)
        {
            // Redirect to info page
            Response.Redirect(String.Format("columns.aspx?database={0}&table={1}", Server.UrlEncode(Request["database"]), Server.UrlEncode(Request["table"])));
        }

        protected void Page_Init(object sender, EventArgs e)
        {
            if (Page.User.Identity.IsAuthenticated)
            {
                Page.ViewStateUserKey = Page.Session.SessionID;
            }
        }
    }
}