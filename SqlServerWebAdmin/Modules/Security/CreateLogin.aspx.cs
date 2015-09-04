using Microsoft.SqlServer.Management.Smo;
using SqlServerWebAdmin.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
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
            }
            else
            {
                Password.Enabled = false;
            }
        }

        public bool IsUserValid()
        {

            //todo:
            bool success = true;
            SqlConnection myConnection = new SqlConnection("");

            try
            {
                myConnection.Open();
            }
            catch (SqlException ex)
            {
                string message = ex.Message;
                success = false;
            }
            finally
            {
                myConnection.Close();
            }

            return success;

        }

        protected void AddLogin_Click(object sender, EventArgs e)
        {
            if (Page.IsValid)
            {
                LoginCollection logins;
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

                if (IsUserValid())
                {
                    logins = server.Logins;
                    try
                    {
                        var login = new Microsoft.SqlServer.Management.Smo.Login(server, LoginName.Text.Trim());
                        login.LoginType = (LoginType)Enum.Parse(typeof(LoginType), AuthType.SelectedValue);
                        login.Create();
                        logins.Add(login);
                        login.ChangePassword(Password.Text.Trim());
                        
                        // Redirect user to the edit screen so they can edit more properties
                        Response.Redirect("~/Modules/Security/EditServerLogin.aspx?Login=" + Server.UrlEncode(login.Name));
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