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

        protected string FormatUrl(string url)
        {
            return url + "?database=" + Server.UrlEncode(Request["database"]);
        }

    }
}