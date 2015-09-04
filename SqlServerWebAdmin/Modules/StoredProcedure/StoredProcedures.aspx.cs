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
    public partial class StoredProcedures : System.Web.UI.Page
    {

        protected void Page_Load(object sender, System.EventArgs e)
        {
            FilterButton_Click(null, null);
        }

        protected void FilterButton_Click(object sender, System.EventArgs e)
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

            string databaseName = database.Name;

            SqlObjectType objectTypeFilter;
            switch (TypeDropDownList.SelectedIndex)
            {
                case 0:
                    objectTypeFilter = SqlObjectType.User;
                    break;
                case 1:
                    objectTypeFilter = SqlObjectType.User | SqlObjectType.System;
                    break;
                default:
                    throw new Exception("Invalid SProcType selected");
            }

            server.GetDefaultInitFields(typeof(Microsoft.SqlServer.Management.Smo.StoredProcedure));
            server.SetDefaultInitFields(typeof(Microsoft.SqlServer.Management.Smo.StoredProcedure), "IsSystemObject");
            StoredProcedureCollection sprocs = database.StoredProcedures;

            server.Disconnect();

            // Create DataSet from result
            DataSet ds = new DataSet();
            ds.Tables.Add();
            ds.Tables[0].Columns.Add("name");
            ds.Tables[0].Columns.Add("encodedname");
            ds.Tables[0].Columns.Add("owner");
            ds.Tables[0].Columns.Add("type");
            ds.Tables[0].Columns.Add("createdate");
            for (int i = 0; i < sprocs.Count; i++)
            {
                StoredProcedure sproc = sprocs[i];

                // Only add objects that we want (system or user)
                 if (!sproc.IsSystemObject || (sproc.IsSystemObject  && objectTypeFilter != SqlObjectType.User))
                    ds.Tables[0].Rows.Add(new object[] 
                    { 
                        Server.HtmlEncode(sproc.Name), 
                        Server.UrlEncode(sproc.Name), 
                        Server.HtmlEncode(sproc.Owner), 
                        Server.HtmlEncode(sproc.IsSystemObject ? "system" : ""),
                        Server.HtmlEncode(sproc.CreateDate.ToString()) 
                    });
            }

            // Show message if there are no tables, otherwise show datagrid
            if (ds.Tables[0].Rows.Count == 0)
            {
                SProcsDataGrid.Visible = false;
                SProcTypeErrorLabel.Visible = true;
            }
            else
            {
                SProcTypeErrorLabel.Visible = false;
                SProcsDataGrid.Visible = true;

                SProcsDataGrid.DataSource = ds;
                SProcsDataGrid.DataBind();
            }
        }
    }
}