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
    public partial class DeleteDatabaseUser : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            UserLabel.Text = Request["user"];
        }

        protected void Yes_Click(object sender, EventArgs e)
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
            database.Users[Request["user"]].Drop();

            server.Disconnect();
            Response.Redirect("DatabaseUsers.aspx?database=" + Server.UrlEncode(Request["database"]));
        }

        protected void No_Click(object sender, EventArgs e)
        {
            Response.Redirect("DatabaseUsers.aspx?database=" + Server.UrlEncode(Request["database"]));
        }
    }
}