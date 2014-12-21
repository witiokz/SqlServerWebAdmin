using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using Microsoft.SqlServer.Management.Smo;
using SqlServerWebAdmin.Models;

namespace SqlServerWebAdmin
{
    public partial class Tables : System.Web.UI.Page
    {
        public Tables()
        {
            Page.Init += new System.EventHandler(Page_Init);
        }

        protected void Page_Load(object sender, System.EventArgs e)
        {
            FilterTablesButton_Click(null, null);
        }

        protected void Page_Init(object sender, EventArgs e)
        {
            if (Page.User.Identity.IsAuthenticated)
            {
                Page.ViewStateUserKey = Page.Session.SessionID;
            }

        }

        private void FilterTablesButton_Click(object sender, System.EventArgs e)
        {
            Microsoft.SqlServer.Management.Smo.Server server = DbExtensions.CurrentServer;
            try
            {
                server.Connect();
            }
            catch (System.Exception ex)
            {
                Response.Redirect(String.Format("error.aspx?errormsg={0}&stacktrace={1}", Page.Server.UrlEncode(ex.Message), Page.Server.UrlEncode(ex.StackTrace)));
            }

            Database database = server.Databases[HttpContext.Current.Server.HtmlDecode(HttpContext.Current.Request["database"])];

            SqlObjectType objectTypeFilter;
            switch (TableTypeDropDownList.SelectedIndex)
            {
                case 0:
                    objectTypeFilter = SqlObjectType.User;
                    break;
                case 1:
                    objectTypeFilter = SqlObjectType.User | SqlObjectType.System;
                    break;
                default:
                    throw new Exception("Invalid TableType selected");
            }

            // Get table list
            AddNewTableHyperLink.NavigateUrl = String.Format("createtable.aspx?database={0}", Server.UrlEncode(Request["database"]));

            TableCollection tables = database.Tables;

            // Create DataSet from result
            DataSet ds = new DataSet();
            ds.Tables.Add();
            ds.Tables[0].Columns.Add("name");
            ds.Tables[0].Columns.Add("encodedname");
            ds.Tables[0].Columns.Add("owner");
            ds.Tables[0].Columns.Add("type");
            ds.Tables[0].Columns.Add("createdate");
            ds.Tables[0].Columns.Add("rows");
            for (int i = 0; i < tables.Count; i++)
            {
                Microsoft.SqlServer.Management.Smo.Table table = tables[i];

                // Only add objects that we want (system or user)
                /*if ((table. && objectTypeFilter) > 0)
                    ds.Tables[0].Rows.Add(new object[] { Server.HtmlEncode(table.Name), 
                        Server.UrlEncode(table.Name), Server.HtmlEncode(table.Owner), 
                        Server.HtmlEncode(table.TableType.ToString()), Server.HtmlEncode(table.CreateDate.ToString()), table.Rows });*/
            }

            // Show message if there are no tables, otherwise show datagrid
            if (ds.Tables[0].Rows.Count == 0)
            {
                TablesDataGrid.Visible = false;
                TableTypeErrorLabel.Visible = true;
            }
            else
            {
                TableTypeErrorLabel.Visible = false;
                TablesDataGrid.Visible = true;

                TablesDataGrid.DataSource = ds;
                TablesDataGrid.DataBind();
            }

            server.Disconnect();
        }
    }
}