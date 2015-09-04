using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using Microsoft.SqlServer.Management.Smo;
using SqlServerWebAdmin.Models;

namespace SqlServerWebAdmin
{
    public partial class CreateDatabase : System.Web.UI.Page
    {
        protected void CreateNewDatabaseButton_Click(object sender, System.EventArgs e)
        {
            // If database name is empty or invalid, quit immediately
            if (!IsValid)
                return;

            this.ErrorCreatingLabel.Visible = false;

            bool success = true;

            Microsoft.SqlServer.Management.Smo.Server server = DbExtensions.CurrentServer;
            try
            {
                server.Connect();
            }
            catch (System.Exception ex)
            {
                Response.Redirect(String.Format("error.aspx?errormsg={0}&stacktrace={1}", Server.UrlEncode(ex.Message), Server.UrlEncode(ex.StackTrace)));
            }

            // Check that database doesn't exist
            if (server.Databases[DatabaseNameTextBox.Text] != null)
            {
                this.ErrorCreatingLabel.Visible = true;
                this.ErrorCreatingLabel.Text = "A database with this name already exists.";
                server.Disconnect();
                return;
            }

            try
            {
                Database newDatabase = new Database(server, DatabaseNameTextBox.Text);
                newDatabase.Create();
            }
            catch (Exception ex)
            {
                this.ErrorCreatingLabel.Visible = true;
                this.ErrorCreatingLabel.Text = "There was an error creating the database.<br>" + Server.HtmlEncode(ex.Message).Replace("\n", "<br>");
                success = false;
            }

            server.Disconnect();

            if (success)
                Response.Redirect("~Modules/Table/Tables.aspx?database=" + Server.UrlEncode(DatabaseNameTextBox.Text));
        }
    }
}