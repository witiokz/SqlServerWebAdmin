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
    public partial class CreateTable : System.Web.UI.Page
    {
        public CreateTable()
        {
            Page.Init += new System.EventHandler(Page_Init);
        }

        protected void Page_Load(object sender, System.EventArgs e)
        {
            ErrorCreatingLabel.Visible = false;
        }

        protected void CreateNewTableButton_Click(object sender, System.EventArgs e)
        {
            if (TableNameTextBox.Text.Length == 0)
            {
                ErrorCreatingLabel.Visible = true;
                ErrorCreatingLabel.Text = "The new table name cannot be blank";
                return;
            }

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

            ErrorCreatingLabel.Visible = false;

            Microsoft.SqlServer.Management.Smo.Table table = server.Databases[database.Name].Tables[TableNameTextBox.Text];

            // Ensure that the table doesn't exist yet
            if (table == null)
            {
                // Now we have to do a quick check and see if it's a valid name for a table
                // The only reliable way to do this is to try to create the table and see what happens

                // In order to find out whether the table name is valid, we create a temporary dummy table
                // and see what happens.
                Microsoft.SqlServer.Management.Smo.Table dummyTable = null;

                try
                {
                    dummyTable = new Microsoft.SqlServer.Management.Smo.Table(database, TableNameTextBox.Text);
                    dummyTable.Create();
                }
                catch (Exception ex)
                {
                    if(dummyTable != null)
                        dummyTable.Drop();

                    server.Disconnect();
                    ErrorCreatingLabel.Visible = true;
                    ErrorCreatingLabel.Text = "There was an error creating the table:<br>" + Server.HtmlEncode(ex.Message).Replace("\n", "<br>");
                    return;
                }

                // Delete the dummy table
                dummyTable.Drop();

                server.Disconnect();
                Response.Redirect(String.Format("editcolumn.aspx?database={0}&table={1}", Server.UrlEncode(database.Name), Server.UrlEncode(TableNameTextBox.Text)));
            }
            else
            {
                server.Disconnect();
                ErrorCreatingLabel.Visible = true;
                ErrorCreatingLabel.Text = "A table with this name already exists.";
            }
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