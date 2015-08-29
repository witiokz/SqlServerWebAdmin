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
    public partial class databaselocation : System.Web.UI.UserControl
    {
        protected string GetDbName()
        {
            return Server.UrlEncode(Request["database"]);
        }
    }
}