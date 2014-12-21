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
    public partial class Databases : System.Web.UI.Page
    {
        public Databases()
        {
            Page.Init += new System.EventHandler(Page_Init);
        }

        protected void Page_Load(object sender, System.EventArgs e)
        {
            Server server = null;
            try
            {
                server = DbUtlity.Connect();
                server.ConnectionContext.Connect();
            }
            catch (System.Exception ex)
            {
                Response.Redirect(String.Format("error.aspx?errormsg={0}&stacktrace={1}", Server.UrlEncode(ex.Message), Server.UrlEncode(ex.StackTrace)));
            }

            var databases = server.Databases;
            DbUtlity.Disconnect(server);

            // Create DataSet from list of databases
            DataSet ds = new DataSet();
            ds.Tables.Add();
            ds.Tables[0].Columns.Add("name");
            ds.Tables[0].Columns.Add("encodedname");
            ds.Tables[0].Columns.Add("size");
            for (int i = 0; i < databases.Count; i++)
            {
                Database database = databases[i];
                ds.Tables[0].Rows.Add(new object[] { Server.HtmlEncode(database.Name), Server.UrlEncode(database.Name), database.Size == -1 ? "Unknown" : String.Format("{0}MB", database.Size) });
            }
            DatabasesDataGrid.DataSource = ds;
            DatabasesDataGrid.DataBind();
        }

        protected void Page_Init(object sender, EventArgs e)
        {
            if (Page.User.Identity.IsAuthenticated)
            {
                Page.ViewStateUserKey = Page.Session.SessionID;
            }
        }
    }
}