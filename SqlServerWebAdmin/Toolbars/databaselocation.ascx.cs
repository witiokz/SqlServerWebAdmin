using SqlAdmin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SqlServerWebAdmin.Toolbars
{
    public partial class databaselocation : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
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

            string databaseName = SqlDatabase.CurrentDatabase(server).Name;

            server.Disconnect();

            DatabaseNameHyperLink.NavigateUrl = "../Tables.aspx?database=" + Server.UrlEncode(databaseName);
            DatabaseNameHyperLink.Text = Server.HtmlEncode(databaseName);

            
        }
    }
}