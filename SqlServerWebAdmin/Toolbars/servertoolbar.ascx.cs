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
        }

        protected string CheckLink(string link)
        {
            if(string.IsNullOrEmpty(selected))
            {
                selected = Request.FilePath.Trim('/').Replace(".aspx", "");
            }

            return (selected == link) ? "selectedLink" : ""; 
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