using Microsoft.SqlServer.Management.Smo;
using SqlServerWebAdmin.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SqlServerWebAdmin
{
    public class BasePage : System.Web.UI.Page
    {
        public Microsoft.SqlServer.Management.Smo.Server DbServer
        {
            get
            {
                var currentServer = DbExtensions.CurrentServer;

                return currentServer;
            }
        }

        public Database Database
        {
            get
            {
                if (Request["database"] != null)
                {
                    return DbServer.Databases[HttpContext.Current.Server.HtmlDecode(HttpContext.Current.Request["database"])];
                }

                return null;
            }
        }
    }
}