using SqlAdmin;
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

            try
            {
                SqlDatabase database = SqlDatabase.CurrentDatabase(server);

                SqlDatabaseProperties props = database.GetDatabaseProperties();

                NamePropertyLabel.Text = Server.HtmlEncode(props.Name);
                StatusPropertyLabel.Text = Server.HtmlEncode(props.Status);
                OwnerPropertyLabel.Text = Server.HtmlEncode(props.Owner);
                DateCreatedPropertyLabel.Text = Server.HtmlEncode(Convert.ToString(props.DateCreated));
                SizePropertyLabel.Text = props.Size.ToString("f2");
                SpaceAvailablePropertyLabel.Text = props.SpaceAvailable.ToString("f2");
                NumberOfUsersPropertyLabel.Text = Convert.ToString(props.NumberOfUsers);
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

            try
            {
                SqlDatabase database = SqlDatabase.CurrentDatabase(server);

                SqlDatabaseProperties props = database.GetDatabaseProperties();

                DataFileProperties.Properties = props.DataFile;
                LogFileProperties.Properties = props.LogFile;
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

            // Grab data from the form
            SqlDatabaseProperties props = null;

            SqlFileProperties dataFileProperties = null;
            SqlFileProperties logFileProperties = null;

            try
            {
                dataFileProperties = DataFileProperties.Properties;
            }
            catch (Exception ex)
            {
                ErrorLabel.Visible = true;
                ErrorLabel.Text = "Error reading data file properties: " + Server.HtmlEncode(ex.Message).Replace("\n", "<br>") + "<br><br>";
                return;
            }

            try
            {
                logFileProperties = LogFileProperties.Properties;
            }
            catch (Exception ex)
            {
                ErrorLabel.Visible = true;
                ErrorLabel.Text = "Error reading log file properties: " + Server.HtmlEncode(ex.Message).Replace("\n", "<br>") + "<br><br>";
                return;
            }

            props = new SqlDatabaseProperties(dataFileProperties, logFileProperties);
            SqlDatabaseProperties origProps = database.GetDatabaseProperties();

            // First validate input ourselves
            ArrayList errorList = new ArrayList();

            if (props.DataFile.FileGrowth < 0)
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
            }

            // Try to set properties
            try
            {
                database.SetDatabaseProperties(props);
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
            props = database.GetDatabaseProperties();

            DataFileProperties.Properties = props.DataFile;
            LogFileProperties.Properties = props.LogFile;

            server.Disconnect();
        }
    }
}