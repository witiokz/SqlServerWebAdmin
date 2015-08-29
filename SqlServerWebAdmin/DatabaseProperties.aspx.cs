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
    public partial class DatabaseProperties : System.Web.UI.Page
    {
        public DatabaseProperties()
        {
            Page.Init += new System.EventHandler(Page_Init);
        }

        protected void Page_Load(object sender, System.EventArgs e)
        {
            throw new Exception("not implemented");
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

                NamePropertyLabel.Text = Server.HtmlEncode(database.Name);
                StatusPropertyLabel.Text = Server.HtmlEncode(database.Status.ToString());
                OwnerPropertyLabel.Text = Server.HtmlEncode(database.Owner);
                DateCreatedPropertyLabel.Text = Server.HtmlEncode(Convert.ToString(database.CreateDate));
                SizePropertyLabel.Text = database.Size.ToString("f2");
                SpaceAvailablePropertyLabel.Text = database.SpaceAvailable.ToString("f2");
                NumberOfUsersPropertyLabel.Text = Convert.ToString(database.Users.Count);
            }
            catch (System.Runtime.InteropServices.COMException ex)
            // Thrown if GetDatabaseProperties fails due to lack of permissions
            {
                //Response.Redirect("Error.aspx?errorPassCode=" + 2001);
                Response.Redirect(String.Format("error.aspx?errormsg={0}&stacktrace={1}", Server.UrlEncode(ex.Message), Server.UrlEncode(ex.StackTrace)));
            }
            catch (System.Exception ex)             // Catch any unknown errors
            {
                //Response.Redirect("Error.aspx");    // Display user-friendly error page
                Response.Redirect(String.Format("error.aspx?errormsg={0}&stacktrace={1}", Server.UrlEncode(ex.Message), Server.UrlEncode(ex.StackTrace)));
            }
            finally
            {
                server.Disconnect();
            }

            // On first load of the page, force data gathering...
            if (!IsPostBack)
                CancelButton_Click(null, null);
        }

        protected void Page_Init(object sender, EventArgs e)
        {
            if (Page.User.Identity.IsAuthenticated)
            {
                Page.ViewStateUserKey = Page.Session.SessionID;
            }
        }

        protected void CancelButton_Click(object sender, System.EventArgs e)
        {
            ErrorLabel.Visible = false;

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

                //DataFileProperties.Properties = props.DataFile;
                //LogFileProperties.Properties = props.LogFile;
            }
            catch (System.Runtime.InteropServices.COMException ex)
            // Thrown if GetDatabaseProperties fails due to lack of permissions
            {
                //Response.Redirect("Error.aspx?errorPassCode=" + 2001);
                Response.Redirect(String.Format("error.aspx?errormsg={0}&stacktrace={1}", Server.UrlEncode(ex.Message), Server.UrlEncode(ex.StackTrace)));
            }
            catch (System.Exception ex)             // Catch any unknown errors
            {
                //Response.Redirect("Error.aspx");    // Display user-friendly error page
                Response.Redirect(String.Format("error.aspx?errormsg={0}&stacktrace={1}", Server.UrlEncode(ex.Message), Server.UrlEncode(ex.StackTrace)));
            }
            finally
            {
                server.Disconnect();
            }
        }

        protected void ApplyButton_Click(object sender, System.EventArgs e)
        {
            ErrorLabel.Visible = false;

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

            // Grab data from the form
            DatabaseProperties props = null;

            FileProperties dataFileProperties = null;
            FileProperties logFileProperties = null;

            try
            {
                dataFileProperties = DataFileProperties;
            }
            catch (Exception ex)
            {
                ErrorLabel.Visible = true;
                ErrorLabel.Text = "Error reading data file properties: " + Server.HtmlEncode(ex.Message).Replace("\n", "<br>") + "<br><br>";
                return;
            }

            try
            {
                logFileProperties = LogFileProperties;
            }
            catch (Exception ex)
            {
                ErrorLabel.Visible = true;
                ErrorLabel.Text = "Error reading log file properties: " + Server.HtmlEncode(ex.Message).Replace("\n", "<br>") + "<br><br>";
                return;
            }

            //props = new DatabaseProperties(dataFileProperties, logFileProperties);
            props = new DatabaseProperties();
            //props = dataFileProperties;
            //props.LogFileProperties = logFileProperties;
            //DatabaseProperties origProps = database.Properties;

            // First validate input ourselves
            ArrayList errorList = new ArrayList();

            /*if (props.DataFileProperties.FileGrowth < 0)
                errorList.Add("Data file growth must be positive");

            if (props.DataFile.MaximumSize < -1)
                errorList.Add("Data file maximum size must be positive");

            if (props.LogFile.FileGrowth < 0)
                errorList.Add("Log file growth must be positive");

            if (props.LogFile.MaximumSize < -1)
                errorList.Add("Log file maximum size must be positive");

            if (props.DataFile.MaximumSize != -1 && origProps.Size > props.DataFile.MaximumSize)
                errorList.Add("Maximum file growth must be greater than or equal to the current database size");

            if (errorList.Count > 0)
            {
                ErrorLabel.Visible = true;

                ErrorLabel.Text = "The following error(s) occured:<br><ul>";
                for (int i = 0; i < errorList.Count; i++)
                    ErrorLabel.Text += String.Format("<li>{0}</li>", (string)errorList[i]);
                ErrorLabel.Text += "</ul>";

                return;
            }*/

            // Try to set properties
            try
            {
                //database = props;
            }
            catch (Exception ex)
            {
                // Show error message and quit
                server.Disconnect();

                ErrorLabel.Text = "The following error occured:<br>" + Server.HtmlEncode(ex.Message).Replace("\n", "<br>") + "<br><br>";
                return;
            }

            // Only reload data if there were no errors
            // Get database properties and fill in their info
            //props = database.GetDatabaseProperties();

            //DataFileProperties.Properties = props.DataFile;
            //LogFileProperties.Properties = props.LogFile;

            server.Disconnect();
        }
    }
}