using SqlAdmin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SqlServerWebAdmin
{
    public partial class CreateLogin : System.Web.UI.Page
    {
        protected void AuthType_Changed(object sender, EventArgs e)
        {
            if (AuthType.SelectedValue == "Standard")
            {
                Password.Enabled = true;
                PasswordLabel.Enabled = true;
            }
            else
            {
                Password.Enabled = false;
                PasswordLabel.Enabled = false;
            }
        }

        protected void AddLogin_Click(object sender, EventArgs e)
        {
            if (Page.IsValid)
            {
                SqlLoginCollection logins;
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

                if (server.IsUserValid())
                {
                    logins = server.Logins;
                    try
                    {
                        SqlLogin newLogin = logins.Add(
                            LoginName.Text.Trim(),
                            (SqlLoginType)Enum.Parse(typeof(SqlLoginType), AuthType.SelectedValue),
                            Password.Text.Trim()
                            );

                        // Redirect user to the edit screen so they can edit more properties
                        Response.Redirect("EditServerLogin.aspx?Login=" + Server.UrlEncode(newLogin.Name));
                    }
                    catch (Exception ex)
                    {
                        ErrorMessage.Text = ex.Message;
                    }
                }

                server.Disconnect();
            }
        }
    }
}