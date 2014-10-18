using SqlAdmin;
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
        public RenameTable()
        {
            Page.Init += new System.EventHandler(Page_Init);
        }

        protected void Page_Load(object sender, System.EventArgs e)
        {
            ErrorCreatingLabel.Visible = false;

            if (!IsPostBack)
                TableNameTextBox.Text = Request["table"];
        }

        protected void Page_Init(object sender, EventArgs e)
        {
            if (Page.User.Identity.IsAuthenticated)
            {
                Page.ViewStateUserKey = Page.Session.SessionID;
            }

        }

        protected void RenameButton_Click(object sender, System.EventArgs e)
        {
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

            try
            {
                server.Connect();
            }
            catch (System.Exception ex)
            {
                //Response.Redirect("Error.aspx?errorPassCode=" + 2002);
                Response.Redirect(String.Format("error.aspx?errormsg={0}&stacktrace={1}", Server.UrlEncode(ex.Message), Server.UrlEncode(ex.StackTrace)));
            }

            SqlTable table = database.Tables[Request["table"]];
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
                table.Name = TableNameTextBox.Text;

                // If successful, disconnect
                server.Disconnect();

                // Redirect to info page
                Response.Redirect(String.Format("tables.aspx?database={0}", Server.UrlEncode(Request["database"])));
            }
            catch (Exception ex)
            {
                ErrorCreatingLabel.Visible = true;
                ErrorCreatingLabel.Text = "There was an error renaming the table:<br>" + Server.HtmlEncode(ex.Message).Replace("\n", "<br>");

                server.Disconnect();
            }
        }

        protected void CancelButton_Click(object sender, System.EventArgs e)
        {
            // Redirect to info page
            Response.Redirect(String.Format("tables.aspx?database={0}", Server.UrlEncode(Request["database"])));
        }
    }
}