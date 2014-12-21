using SqlServerWebAdmin.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SqlServerWebAdmin.Toolbars
{
    public partial class serverlocation : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, System.EventArgs e)
        {

            string serverName = AdminUser.CurrentUser.Server;

            ServerNameHyperLink.NavigateUrl = "../databases.aspx";
            ServerNameHyperLink.Text = Server.HtmlEncode(serverName);
        }
    }
}