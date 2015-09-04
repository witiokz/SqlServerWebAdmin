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
    public partial class CreateDatabaseRole : System.Web.UI.Page
    {
        protected ItemPicker RoleUsers;
        protected TextBox RolePassword;

        protected void CreateRole_Click(object sender, EventArgs e)
        {
            if (Page.IsValid)
            {
                try
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

                    // TODO: Finish

                    server.Disconnect();
                }
                catch (Exception ex)
                {
                    this.ErrorMessage.Text = ex.Message;
                }
            }
        }
    }
}