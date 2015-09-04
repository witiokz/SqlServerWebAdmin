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
    public partial class EditStoredProcedure : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
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

            Database database = server.Databases[HttpContext.Current.Server.HtmlDecode(HttpContext.Current.Request["database"])];

            string sprocName = Request["sproc"];

            if (!IsPostBack)
            {
                try
                {
                    server.Connect();
                }
                catch (System.Exception ex)
                {
                    Response.Redirect(String.Format("error.aspx?errormsg={0}&stacktrace={1}", Server.UrlEncode(ex.Message), Server.UrlEncode(ex.StackTrace)));
                }

                // Check to see if SProc is new or it already exists
                StoredProcedure sproc = database.StoredProcedures[sprocName];
                if (sproc == null)
                {
                    // SProc is new, create template
                    NameLabel.Text = Server.HtmlEncode(sprocName);
                    OwnerLabel.Text = "";
                    CreateDateLabel.Text = "";
                    TextTextbox.Text = "";
                }
                else
                {
                    // SProc already exists, load it from database
                    NameLabel.Text = Server.HtmlEncode(sproc.Name);
                    OwnerLabel.Text = Server.HtmlEncode(sproc.Owner);
                    CreateDateLabel.Text = Server.HtmlEncode(Convert.ToString(sproc.CreateDate));

                    TextTextbox.Text = sproc.TextBody;
                }

                server.Disconnect();
            }
        }

        protected void SaveButton_Click(object sender, System.EventArgs e)
        {
            Microsoft.SqlServer.Management.Smo.Server server = DbExtensions.CurrentServer;
            try
            {
                server.Connect();
            }
            catch (System.Exception exConnect)
            {
                //Response.Redirect("Error.aspx?errorPassCode=" + 2002);
                Response.Redirect(String.Format("error.aspx?errormsg={0}&stacktrace={1}", Server.UrlEncode(exConnect.Message), Server.UrlEncode(exConnect.StackTrace)));
            }

            Database database = server.Databases[HttpContext.Current.Server.HtmlDecode(HttpContext.Current.Request["database"])];

            string sprocName = Request["sproc"];

            bool success = true;

            Exception ex = null;

            try
            {
                // Check to see if SProc is new or it already exists
                StoredProcedureCollection sprocs = database.StoredProcedures;
                StoredProcedure sproc = sprocs[sprocName];
                
                if (sproc == null)
                {
                    // SProc is new, so create entirely new SProc (don't care about return value)
                    sproc = new StoredProcedure(database, sprocName);
                    sproc.TextMode = false;
                    sproc.TextBody = TextTextbox.Text;
                    sproc.Create();
                }
                else
                {
                    // SProc already exists, just update its text
                    sproc.TextBody = TextTextbox.Text;
                    sproc.Alter();
                }
            }
            catch (Exception ex2)
            {
                ex = ex2;
                success = false;
            }

            server.Disconnect();

            if (success)
            {
                // Redirect back to SProc page
                Response.Redirect("~/Modules/StoredProcedure/StoredProcedures.aspx?database=" + Server.UrlEncode(Request["database"]));
            }
            else
            {
                // Show error
                ErrorLabel.Visible = true;
                ErrorLabel.Text = "There was an error saving the stored procedure.<br>" + Server.HtmlEncode(Utility.MessageFormat(ex)).Replace("\n", "<br>");
            }
        }
    }
}