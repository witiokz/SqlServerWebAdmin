﻿using SqlAdmin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SqlServerWebAdmin
{
    public partial class DatabaseUsers : System.Web.UI.Page
    {
        protected override void OnLoad(EventArgs e)
        {
            if (!Page.IsPostBack)
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

                UsersGrid.DataSource = database.Users;
                UsersGrid.DataBind();

                CreateUserLink.NavigateUrl = "CreateDatabaseUser.aspx?database=" + Server.UrlEncode(database.Name);

                server.Disconnect();
            }
            base.OnLoad(e);
        }
    }
}