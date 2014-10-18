using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SqlServerWebAdmin.Toolbars
{
    public partial class servertoolbar : System.Web.UI.UserControl
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

        public servertoolbar()
        {
            this.Init += new System.EventHandler(Page_Init);
        }

        protected void Page_Load(object sender, System.EventArgs e)
        {
            // Initialize links
            DatabasesHyperLink.NavigateUrl = "../databases.aspx";
            ImportHyperLink.NavigateUrl = "../import.aspx";
            ExportHyperLink.NavigateUrl = "../export.aspx";
            SecurityHyperLink.NavigateUrl = "../security.aspx";

            switch (selected)
            {
                case "databases":
                    DatabasesTd.Attributes["class"] = "selectedLink";
                    DatabasesHyperLink.Attributes.Remove("onMouseOver");
                    break;
                case "import":
                    ImportTd.Attributes["class"] = "selectedLink";
                    ImportHyperLink.Attributes.Remove("onMouseOver");
                    break;
                case "export":
                    ExportTd.Attributes["class"] = "selectedLink";
                    ExportHyperLink.Attributes.Remove("onMouseOver");
                    break;
                case "security":
                    SecurityTd.Attributes["class"] = "selectedLink";
                    SecurityHyperLink.Attributes.Remove("onMouseOver");
                    break;
            }

            Page.RegisterClientScriptBlock("Global_Script", "<script language=javascript src=Global.js></script>");
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