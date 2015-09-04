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
    public partial class DeleteDatabase : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
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
                Response.Redirect(String.Format("error.aspx?errormsg={0}&stacktrace={1}", Server.UrlEncode(ex.Message), Server.UrlEncode(ex.StackTrace)));
            }

            Database database = server.Databases.Cast<Database>().FirstOrDefault(i => i.Name.ToLower().Contains(Request.QueryString["database"]));

            // Check that database actually exists
            if (database != null)
            {
                try
                {
                    server.KillAllProcesses(database.Name);
                    server.KillDatabase(database.Name);
                    //database.Drop();
                }
                catch (Exception ex)
                {
                    Response.Redirect(String.Format("error.aspx?error={1}", ex.Message));
                }
            }
            else
            {
                server.Disconnect();

                // Database doesn't exist - break out and go to error page
                Response.Redirect(String.Format("error.aspx?error={1}", 1000));
                return;
            }

            server.Disconnect();

            // Redirect to database list page
            Response.Redirect("~/databases.aspx");
        }
    }
}