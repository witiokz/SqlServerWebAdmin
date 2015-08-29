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
    public partial class EditDatabaseUser : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
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

                Database database = server.Databases[HttpContext.Current.Server.HtmlDecode(HttpContext.Current.Request["database"])];

                User user = null;
                if (Request["User"] != null)
                {
                    user = database.Users.Cast<User>().FirstOrDefault(i => i.Name == Request["User"].Replace("[", "").Replace("]", ""));
                }
                if (user == null)
                    Response.Redirect("DatabaseUsers.aspx");

                UsernameLabel.Text = user.Name.ToUpper();
                LoginLabel.Text = user.Login;

                DatabaseRoleCollection dbRoles = database.Roles;

                Roles.DataSource = dbRoles;
                Roles.DataBind();

                foreach (string roleName in user.EnumRoles())
                {
                    ListItem roleItem = Roles.Items.FindByValue(roleName);
                    if (roleItem != null)
                    {
                        roleItem.Selected = true;
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

                DatabaseRoleCollection dbRoles = database.Roles;
                User user = database.Users[Request["user"].Replace("[", "").Replace("]", "")];

                foreach (ListItem item in Roles.Items)
                {
                    if (!user.IsMember(item.Value) && item.Selected)
                    {
                        dbRoles[item.Value].AddMember(Request["user"]);
                    }
                    else if (user.IsMember(item.Value) && !item.Selected)
                    {
                        dbRoles[item.Value].DropMember(Request["user"]);
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

            Response.Redirect("DatabaseUsers.aspx?database=" + Server.UrlEncode(Request["database"]));
        }

        protected void Cancel_Click(object sender, EventArgs e)
        {
            Response.Redirect("DatabaseUsers.aspx?database=" + Server.UrlEncode(Request["Database"]));
        }
    }
}