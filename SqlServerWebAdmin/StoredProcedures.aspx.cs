using SqlAdmin;
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
        public StoredProcedures()
        {
            Page.Init += new System.EventHandler(Page_Init);
        }

        protected void Page_Load(object sender, System.EventArgs e)
        {
            FilterSProcsButton_Click(null, null);
        }

        protected void Page_Init(object sender, EventArgs e)
        {
            if (Page.User.Identity.IsAuthenticated)
            {
                Page.ViewStateUserKey = Page.Session.SessionID;
            }
        }

        protected void FilterSProcsButton_Click(object sender, System.EventArgs e)
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

            string databaseName = database.Name;

            SqlObjectType objectTypeFilter;
            switch (SProcTypeDropDownList.SelectedIndex)
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

            // Get stored procedure list
            AddNewSProcHyperLink.NavigateUrl = String.Format("createstoredprocedure.aspx?database={0}", Server.UrlEncode(Request["database"]));

            SqlStoredProcedureCollection sprocs = database.StoredProcedures;

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
                SqlStoredProcedure sproc = sprocs[i];

                // Only add objects that we want (system or user)
                if ((sproc.StoredProcedureType & objectTypeFilter) > 0)
                    ds.Tables[0].Rows.Add(new object[] { Server.HtmlEncode(sproc.Name), Server.UrlEncode(sproc.Name), Server.HtmlEncode(sproc.Owner), Server.HtmlEncode(sproc.StoredProcedureType.ToString()), Server.HtmlEncode(sproc.CreateDate.ToString()) });
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