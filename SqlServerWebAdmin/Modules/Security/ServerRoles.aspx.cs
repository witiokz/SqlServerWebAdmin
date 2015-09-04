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
    public partial class ServerRoles : System.Web.UI.Page
    {
        protected System.Web.UI.WebControls.DataGrid LoginDataGrid;

        protected void Page_Load(object sender, System.EventArgs e)
        {
             Microsoft.SqlServer.Management.Smo.Server server = DbExtensions.CurrentServer;
            ServerRole serverRole;
            try
            {
                server.Connect();
            }
            catch (System.Exception ex)
            {
                //Response.Redirect("Error.aspx?errorPassCode=" + 2002);
                Response.Redirect(String.Format("error.aspx?errormsg={0}&stacktrace={1}", Server.UrlEncode(ex.Message), Server.UrlEncode(ex.StackTrace)));
            }
            ServerRoleCollection serverRoles = server.Roles;
            server.Disconnect();

            // Create DataSet from list of databases
            DataSet ds = new DataSet();
            ds.Tables.Add();
            ds.Tables[0].Columns.Add("FullName");
            ds.Tables[0].Columns.Add("Name");
            ds.Tables[0].Columns.Add("Description");

            for (int i = 0; i < serverRoles.Count; i++)
            {

                serverRole = serverRoles[i];

                ds.Tables[0].Rows.Add(
                    new object[] {
                        //Server.HtmlEncode(serverRole.FullName), 
                        Server.HtmlEncode(serverRole.Name),
                        //Server.HtmlEncode(serverRole.Description),\
                    
                    
                    }
                );
            }

            RoleDataGrid.DataSource = ds;
            RoleDataGrid.DataBind();
        }
    }
}