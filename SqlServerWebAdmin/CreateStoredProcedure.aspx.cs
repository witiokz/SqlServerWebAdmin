using SqlAdmin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SqlServerWebAdmin
{
    public partial class CreateStoredProcedure : System.Web.UI.Page
    {
        public CreateStoredProcedure()
        {
            Page.Init += new System.EventHandler(Page_Init);
        }

        protected void Page_Load(object sender, System.EventArgs e)
        {
            ErrorCreatingLabel.Visible = false;
        }

        protected void CreateNewSProcButton_Click(object sender, System.EventArgs e)
        {
            if (SProcNameTextBox.Text.Length == 0)
            {
                ErrorCreatingLabel.Visible = true;
                ErrorCreatingLabel.Text = "The new stored procedure name cannot be blank";
                return;
            }

            SqlServer server = SqlServer.CurrentServer;
            try
            {
                server.Connect();
            }
            catch (System.Exception ex)
            {
                //Response.Redirect("Error.aspx?errorPassCode=" + 2002);
                Response.Redirect(String.Format("error.aspx?errormsg={0}&stacktrace={1}", Server.UrlEncode(ex.Message), Server.UrlEncode(ex.StackTrace)));
            }

            SqlDatabase database = SqlDatabase.CurrentDatabase(server);

            ErrorCreatingLabel.Visible = false;

            SqlStoredProcedure sproc = database.StoredProcedures[SProcNameTextBox.Text];

            // Ensure that SProc doesn't exist yet
            if (sproc == null)
            {
                // Now we have to do a quick check and see if it's a valid name for a stored procedure
                // The only reliable way to do this is to try to create the stored procedure and see what happens

                // In order to find out whether the table name is valid, we create a temporary dummy table
                // and see what happens.
                SqlStoredProcedure dummySproc = null;

                try
                {
                    dummySproc = database.StoredProcedures.Add(SProcNameTextBox.Text, "CREATE PROCEDURE [" + SProcNameTextBox.Text + "] AS\r\nGO");
                }
                catch (Exception ex)
                {
                    // Disconnect and show error
                    if (dummySproc != null)
                        dummySproc.Remove();

                    server.Disconnect();
                    ErrorCreatingLabel.Visible = true;
                    ErrorCreatingLabel.Text = "There was an error creating the stored procedure:<br>" + Server.HtmlEncode(ex.Message).Replace("\n", "<br>");
                    return;
                }

                // Delete the dummy stored procedure
                dummySproc.Remove();

                server.Disconnect();

                Response.Redirect(String.Format("EditStoredProcedure.aspx?database={0}&sproc={1}", Server.UrlEncode(database.Name), Server.UrlEncode(SProcNameTextBox.Text)));
            }
            else
            {
                server.Disconnect();
                ErrorCreatingLabel.Visible = true;
                ErrorCreatingLabel.Text = "A stored procedure with this name already exists.";
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