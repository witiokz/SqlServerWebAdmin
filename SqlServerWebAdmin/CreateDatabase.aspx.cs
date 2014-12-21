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
        public CreateDatabase()
        {
            Page.Init += new System.EventHandler(Page_Init);
        }

        protected void Page_Load(object sender, System.EventArgs e)
        {
            // Put user code to initialize the page here
        }

        protected void CreateNewDatabaseButton_Click(object sender, System.EventArgs e)
        {
            // If database name is empty or invalid, quit immediately
            if (!IsValid)
                return;



            // Create the database

            this.ErrorCreatingLabel.Visible = false;

            bool success = true;

            Server server = null;
            try
            {
                server = DbUtlity.Connect();
                server.ConnectionContext.Connect();
            }
            catch (System.Exception ex)
            {
                //Response.Redirect("Error.aspx?errorPassCode=" + 2002);
                Response.Redirect(String.Format("error.aspx?errormsg={0}&stacktrace={1}", Server.UrlEncode(ex.Message), Server.UrlEncode(ex.StackTrace)));
            }

            // Check that database doesn't exist
            if (server.Databases[DatabaseNameTextBox.Text] != null)
            {
                this.ErrorCreatingLabel.Visible = true;
                this.ErrorCreatingLabel.Text = "A database with this name already exists.";
                DbUtlity.Disconnect(server);
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
                Response.Redirect("Tables.aspx?database=" + Server.UrlEncode(DatabaseNameTextBox.Text));
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