using Microsoft.SqlServer.Management.Smo;
using SqlServerWebAdmin.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SqlServerWebAdmin
{
    public partial class EditServerLogin : System.Web.UI.Page
    {
        Microsoft.SqlServer.Management.Smo.Login sqlLogin = null;
        DatabaseCollection databases = null;
        ServerRoleCollection allRoles = null;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request["Login"] == null)
                Response.Redirect("CreateLogin.aspx");

            LoginLabel.Text = Request["Login"].ToUpper();

            if (!Page.IsPostBack)
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

                sqlLogin = server.Logins[Request["Login"]];

                if (sqlLogin == null)
                    Response.Redirect("CreateLogin.aspx");

                if (sqlLogin.LoginType == LoginType.WindowsUser || sqlLogin.LoginType == LoginType.WindowsGroup)
                {
                    SecurityAccess.Enabled = true;
                    SecurityAccessLabel.Enabled = true;

                    if (sqlLogin.WindowsLoginAccessType == WindowsLoginAccessType.Deny)
                    {
                        SecurityAccess.Items[1].Selected = true;
                    }
                    else
                    {
                        SecurityAccess.Items[0].Selected = true;
                    }
                }

                databases = server.Databases;

                DefaultDatabase.DataSource = databases;
                DefaultDatabase.DataBind();

                DatabaseAccessGrid.DataSource = databases;
                DatabaseAccessGrid.DataBind();

                // Select default database
                ListItem databaseItem = DefaultDatabase.Items.FindByValue(sqlLogin.DefaultDatabase);
                if (databaseItem != null)
                {
                    databaseItem.Selected = true;
                }
                else
                {
                    databaseItem = DefaultDatabase.Items.FindByValue("master");
                    if (databaseItem != null)
                        databaseItem.Selected = true;
                }

                allRoles = server.Roles;

                ServerRoles.DataSource = allRoles;
                ServerRoles.DataBind();

                // Select member roles
                foreach (ListItem item in ServerRoles.Items)
                {
                    if (sqlLogin.IsMember(item.Value))
                    {
                        item.Selected = true;
                    }
                }

                DefaultLanguage.DataSource = server.Languages;
                DefaultLanguage.DataBind();

                // Select default language
                ListItem languageItem = DefaultLanguage.Items.FindByValue(sqlLogin.Language);
                if (languageItem != null)
                {
                    languageItem.Selected = true;
                }
                else
                {
                    languageItem = DefaultLanguage.Items.FindByValue("English");
                    if (languageItem != null)
                        languageItem.Selected = true;
                }

                server.Disconnect();
                focusPanel(GeneralPanel);
            }
        }

        protected void DatabaseAccessGrid_ItemCommand(object sender, GridViewCommandEventArgs e)
        {
            switch (e.CommandName)
            {
                case "EditRoles":
                    if (this.Save())
                    {
                       // Response.Redirect("DatabaseRoles.aspx?database=" + Server.UrlEncode((string)DatabaseAccessGrid.DataKeys[e.CommandArgument]));
                    }
                    break;
            }
        }

        protected void DatabaseAccessGrid_Databound(object sender, GridViewRowEventArgs e)
        {
            //if (e.Row.RowType == DataControlRowType.A ListItemType.AlternatingItem || e.Row.RowType == ListItemType.Item)
            //{
            //    Database database = databases[(string)DatabaseAccessGrid.DataKeys[e.Row.RowIndex]];

            //    if (sqlLogin.GetDatabaseUser(database.Name) != null)
            //    {
            //        CheckBox cb = e.RowType.FindControl("DatabaseAccess") as CheckBox;
            //        if (cb != null)
            //            cb.Checked = true;
            //    }
            //}
        }

        protected void Sections_Changed(object sender, EventArgs e)
        {
            switch (Sections.SelectedValue)
            {
                case "General":
                    focusPanel(GeneralPanel);
                    break;
                case "Roles":
                    focusPanel(RolesPanel);
                    break;
                case "Databases":
                    focusPanel(DatabasesPanel);
                    break;
            }
        }

        protected void Save_Click(object sender, EventArgs e)
        {
            if (this.Save())
            {
                Response.Redirect("ServerLogins.aspx");
            }
        }

        private bool Save()
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

            try
            {
                // Save Login settings
                sqlLogin = server.Logins[Request["Login"]];

                if (SecurityAccess.Enabled)
                {
                    sqlLogin.DenyWindowsLogin = SecurityAccess.SelectedValue == "Deny" ? true : false;
                }

                sqlLogin.DefaultDatabase = DefaultDatabase.SelectedValue;
                sqlLogin.Language = DefaultLanguage.SelectedValue;

                // Save server roles
                foreach (ListItem item in ServerRoles.Items)
                {
                    if (sqlLogin.IsMember(item.Value) && !item.Selected)
                    {
                        server.Roles[item.Value].DropMember(sqlLogin.Name);
                    }
                    else if (!sqlLogin.IsMember(item.Value) && item.Selected)
                    {
                        server.Roles[item.Value].AddMember(sqlLogin.Name);
                    }
                }

                databases = server.Databases;

                // Save database access
                foreach (GridViewRow item in DatabaseAccessGrid.Rows)
                {
                    Database database = null;//databases[(string)DatabaseAccessGrid.DataKeys[item.RowIndex]];
                    CheckBox cb = item.FindControl("DatabaseAccess") as CheckBox;
                    if (database != null && cb != null)
                    {
                        string dbName = sqlLogin.GetDatabaseUser(database.Name);
                        if (dbName != null && !cb.Checked)
                        {
                            database.Users[dbName].Drop();
                        }
                        else if (dbName == null && cb.Checked)
                        {
                            var user = new User(database, sqlLogin.Name);
                            user.Create();
                            //database.Users.Add(sqlLogin.Name, sqlLogin.Name);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                ErrorMessage.Text = ex.Message;
                return false;
            }
            finally
            {
                server.Disconnect();
            }
            return true;
        }

        private void focusPanel(Panel focusPanel)
        {
            Panel[] panels = new Panel[] { GeneralPanel, RolesPanel, DatabasesPanel };
            foreach (Panel panel in panels)
            {
                panel.Visible = false;
            }

            focusPanel.Visible = true;
        }
    }
}