using Microsoft.SqlServer.Management.Smo;
using SqlServerWebAdmin.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SqlServerWebAdmin
{
    public partial class ServerLogins : System.Web.UI.Page
    {
        protected void Page_Load(object sender, System.EventArgs e)
        {
            Microsoft.SqlServer.Management.Smo.Server server = DbExtensions.CurrentServer;
            Microsoft.SqlServer.Management.Smo.Login login;
            try
            {
                server.Connect();
            }
            catch (System.Exception ex)
            {
                //Response.Redirect("Error.aspx?errorPassCode=" + 2002);
                Response.Redirect(String.Format("error.aspx?errormsg={0}&stacktrace={1}", Server.UrlEncode(ex.Message), Server.UrlEncode(ex.StackTrace)));
            }
            LoginCollection logins = server.Logins;
            server.Disconnect();

            // Create DataSet from list of databases
            DataSet ds = new DataSet();
            ds.Tables.Add();
            ds.Tables[0].Columns.Add("Name");
            ds.Tables[0].Columns.Add("LoginType");
            ds.Tables[0].Columns.Add("NTLoginAccessType");
            ds.Tables[0].Columns.Add("Database");
            ds.Tables[0].Columns.Add("LanguageAlias");

            for (int i = 0; i < logins.Count; i++)
            {
                login = logins[i];

                ds.Tables[0].Rows.Add(
                    new object[] {
                        Server.HtmlEncode(login.Name), 
                        Server.HtmlEncode(login.LoginType.ToString()),
                        Server.HtmlEncode(login.WindowsLoginAccessType.ToString()),
                        Server.HtmlEncode(login.DefaultDatabase.ToString()),
                        Server.HtmlEncode(login.LanguageAlias.ToString()),}
                );
            }

            LoginDataGrid.DataSource = ds;
            LoginDataGrid.DataBind();
        }
    }
}