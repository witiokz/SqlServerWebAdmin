﻿using Microsoft.SqlServer.Management.Smo;
using SqlServerWebAdmin.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SqlServerWebAdmin.Modules.View
{
    public partial class Create : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void CreateNewButton_Click(object sender, System.EventArgs e)
        {
            if (NameTextBox.Text.Length == 0)
            {
                ErrorCreatingLabel.Visible = true;
                ErrorCreatingLabel.Text = "The name cannot be blank";
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

            var sproc = database.Views[NameTextBox.Text];

            // Ensure that SProc doesn't exist yet
            if (sproc == null)
            {
                // Now we have to do a quick check and see if it's a valid name for a stored procedure
                // The only reliable way to do this is to try to create the stored procedure and see what happens

                // In order to find out whether the table name is valid, we create a temporary dummy table
                // and see what happens.
                Microsoft.SqlServer.Management.Smo.View dummySproc = null;

                try
                {
                    dummySproc = new Microsoft.SqlServer.Management.Smo.View(database, NameTextBox.Text); //database.StoredProcedures.Add(SProcNameTextBox.Text, "CREATE PROCEDURE [" + SProcNameTextBox.Text + "] AS\r\nGO");
                    dummySproc.TextMode = false;
                    //dummySproc.TextHeader = "";
                    dummySproc.TextBody = "SELECT TOP (1) GETDATE() AS Expr1";
                    dummySproc.Create();
                }
                catch (Exception ex)
                {
                    // Disconnect and show error
                    if (dummySproc != null)
                        dummySproc.Drop();

                    server.Disconnect();
                    ErrorCreatingLabel.Visible = true;
                    ErrorCreatingLabel.Text = "There was an error creating the stored procedure:<br>" + Server.HtmlEncode(ex.Message).Replace("\n", "<br>");
                    return;
                }

                // Delete the dummy stored procedure
                dummySproc.Drop();

                server.Disconnect();

                Response.Redirect(String.Format("~/Modules/View/Edit.aspx?database={0}&sproc={1}", Server.UrlEncode(database.Name), Server.UrlEncode(NameTextBox.Text)));
            }
            else
            {
                server.Disconnect();
                ErrorCreatingLabel.Visible = true;
                ErrorCreatingLabel.Text = "An item with this name already exists.";
            }
        }
    }
}