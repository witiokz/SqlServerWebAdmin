using Microsoft.SqlServer.Management.Smo;
using SqlServerWebAdmin.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SqlServerWebAdmin.Toolbars
{
    public partial class databasetoolbar : System.Web.UI.UserControl
    {
        private string selected = "";

        public string Selected
        {
            get
            {
                return selected;
            }
            set
            {
                selected = value.ToLower();
            }
        }

        /// <summary>
        public databasetoolbar()
        {
            this.Init += new System.EventHandler(Page_Init);
        }

        protected void Page_Load(object sender, System.EventArgs e)
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

            server.Disconnect();

            // Initialize links
            QueryHyperLink.NavigateUrl = "../QueryDatabase.aspx?database=" + Server.UrlEncode(databaseName);
            TablesHyperLink.NavigateUrl = "../Tables.aspx?database=" + Server.UrlEncode(databaseName);
            PropertiesHyperLink.NavigateUrl = "../DatabaseProperties.aspx?database=" + Server.UrlEncode(databaseName);
            StoredProceduresHyperLink.NavigateUrl = "../StoredProcedures.aspx?database=" + Server.UrlEncode(databaseName);
            UsersHyperLink.NavigateUrl = "../DatabaseUsers.aspx?database=" + Server.UrlEncode(databaseName);
            RolesHyperLink.NavigateUrl = "../DatabaseRoles.aspx?database=" + Server.UrlEncode(databaseName);

            switch (selected)
            {
                case "tables":
                    TablesTd.Attributes["class"] = "selectedLink";
                    TablesHyperLink.Attributes.Remove("onMouseOver");

                    break;
                case "query":
                    QueryTd.Attributes["class"] = "selectedLink";
                    QueryHyperLink.Attributes.Remove("onMouseOver");
                    break;
                case "properties":
                    PropertiesTd.Attributes["class"] = "selectedLink";
                    PropertiesHyperLink.Attributes.Remove("onMouseOver");
                    break;
                case "storedprocedures":
                    StoredProceduresTd.Attributes["class"] = "selectedLink";
                    StoredProceduresHyperLink.Attributes.Remove("onMouseOver");
                    break;
                case "users":
                    UsersTd.Attributes["class"] = "selectedLink";
                    UsersHyperLink.Attributes.Remove("onMouseOver");
                    break;
                case "roles":
                    RolesTd.Attributes["class"] = "selectedLink";
                    RolesHyperLink.Attributes.Remove("onMouseOver");
                    break;


            }
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