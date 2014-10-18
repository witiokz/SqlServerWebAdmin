﻿using SqlAdmin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SqlServerWebAdmin
{
    public partial class CreateDatabaseUser : System.Web.UI.Page
    {
        protected void Create_Click(object sender, EventArgs e)
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
            SqlUser user = database.Users.Add(Logins.SelectedValue, Username.Text.Trim());

            server.Disconnect();
            Response.Redirect("EditDatabaseUser.aspx?database=" + Server.UrlEncode(Request.Params["database"]) + "&user=" + Server.UrlEncode(Username.Text.Trim()));
        }

        protected void Page_Load(object sender, System.EventArgs e)
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

                Logins.DataSource = server.Logins;
                Logins.DataBind();


                // Remove existing users from the Logins selection
                foreach (SqlUser user in database.Users)
                {
                    ListItem item = Logins.Items.FindByValue(user.Login);
                    if (item != null)
                        Logins.Items.Remove(item);
                }

                if (Logins.Items.Count == 0)
                {
                    CreateButton.Enabled = false;
                    ErrorMessage.Text = "All Logins are Users of this database.";
                }

                Username.Text = Logins.SelectedValue;

                server.Disconnect();
            }
        }
    }
}