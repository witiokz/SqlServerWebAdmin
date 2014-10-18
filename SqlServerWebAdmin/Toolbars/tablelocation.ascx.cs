using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SqlServerWebAdmin.Toolbars
{
    public partial class tablelocation : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, System.EventArgs e)
        {
            string databaseName = Request["database"];
            string tableName = Request["table"];

            TableNameHyperLink.NavigateUrl = String.Format("../Columns.aspx?database={0}&table={1}", Server.UrlEncode(databaseName), Server.UrlEncode(tableName));
            TableNameHyperLink.Text = Server.HtmlEncode(tableName);
        }
    }
}