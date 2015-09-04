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
    public partial class RenameTable : System.Web.UI.Page
    {

        protected void Page_Load(object sender, System.EventArgs e)
        {
            ErrorCreatingLabel.Visible = false;

            if (!IsPostBack)
                TableNameTextBox.Text = Request["table"];
        }

        protected void RenameButton_Click(object sender, System.EventArgs e)
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

            try
            {
                server.Connect();
            }
            catch (System.Exception ex)
            {
                //Response.Redirect("Error.aspx?errorPassCode=" + 2002);
                Response.Redirect(String.Format("error.aspx?errormsg={0}&stacktrace={1}", Server.UrlEncode(ex.Message), Server.UrlEncode(ex.StackTrace)));
            }

            Microsoft.SqlServer.Management.Smo.Table table = database.Tables[Request["table"]];
            if (table == null)
            {
                server.Disconnect();

                // Table doesn't exist - break out and go to error page
                Response.Redirect(String.Format("error.aspx?error={0}", 1002));
                return;
            }

            // Rename the table
            try
            {
                table.Rename(TableNameTextBox.Text);

                // If successful, disconnect
                server.Disconnect();

                // Redirect to info page
                Response.Redirect(String.Format("~/Modules/Table/tables.aspx?database={0}", Server.UrlEncode(Request["database"])));
            }
            catch (Exception ex)
            {
                ErrorCreatingLabel.Visible = true;
                ErrorCreatingLabel.Text = "There was an error renaming the table:<br>" + Server.HtmlEncode(ex.Message).Replace("\n", "<br>");

                server.Disconnect();
            }
        }

    }
}