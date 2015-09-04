using Microsoft.SqlServer.Management.Smo;
using SqlServerWebAdmin.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SqlServerWebAdmin
{
    public partial class EditDatabaseRole : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                if (Request["role"] == null)
                    Response.Redirect("DatabaseRoles.aspx?database=" + Server.UrlEncode(Request["database"]));

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

                DatabaseRole role = database.Roles[Request["role"]];
                if (role == null)
                    Response.Redirect("DatabaseRoles.aspx?database=" + Server.UrlEncode(Request["database"]));

                RoleNameLabel.Text = role.Name;
                RoleTypeLabel.Text = "some";//role.State ? "Application" : "Standard";

                if (/*role.AppRole*/false)
                {
                    ApplicationRolePanel.Visible = true;
                }
                else
                {
                    StandardRolePanel.Visible = true;
                    // Parse out names from Users
                    ArrayList userNames = new ArrayList();
                    foreach (User user in database.Users)
                    {
                        userNames.Add(user.Name);
                    }
                    RoleUsers.DataSource = userNames;
                    RoleUsers.DataBind();

                    foreach (ListItem item in RoleUsers.Items)
                    {
                        foreach (string name in role.EnumMembers())
                        {
                            if (item.Value == name)
                            {
                                item.Selected = true;
                                break;
                            }
                        }
                    }
                }

                server.Disconnect();
            }
        }

        protected void Save_Click(object sender, EventArgs e)
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
                Database database = server.Databases[HttpContext.Current.Server.HtmlDecode(HttpContext.Current.Request["database"])];
                DatabaseRole role = database.Roles[Request["Role"]];

                foreach (ListItem item in RoleUsers.Items)
                {
                    User user = database.Users[item.Value];
                    if (user.IsMember(role.Name) && !item.Selected)
                    {
                        role.DropMember(user.Name);
                    }
                    else if (!user.IsMember(role.Name) && item.Selected)
                    {
                        role.AddMember(role.Name);
                    }
                }
            }
            catch (Exception ex)
            {
                ErrorMessage.Text = ex.Message;
                return;
            }
            finally
            {
                server.Disconnect();
            }

            Response.Redirect("DatabaseRoles.aspx?database=" + Server.UrlEncode(Request["database"]));
        }
    }
}