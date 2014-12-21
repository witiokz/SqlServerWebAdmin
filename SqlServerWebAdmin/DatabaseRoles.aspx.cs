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
    public partial class DatabaseRoles : System.Web.UI.Page
    {
        protected override void OnLoad(EventArgs e)
        {
            if (!Page.IsPostBack)
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

                RolesGrid.DataSource = database.Roles;
                RolesGrid.DataBind();

                //CreateRoleLink.NavigateUrl = "CreateDatabaseRole.aspx?database=" + Server.UrlEncode(Request["database"]);

                server.Disconnect();
            }
            base.OnLoad(e);
        }
    }
}