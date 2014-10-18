using SqlAdmin;
using SqlAdmin.Controls;
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