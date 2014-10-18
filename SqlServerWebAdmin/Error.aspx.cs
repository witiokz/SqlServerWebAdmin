using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SqlServerWebAdmin
{
    public partial class Error : System.Web.UI.Page
    {
        private string ErrorLookup(int id)
        {
            /* To handle more errors:
             * When catching the error, redirect to "Error.aspx?errorPassCode="
             * followed by the number of your error code, which can be any number
             * that is not used already in the switch statement below.
             * Finally, just add the case for that number in the switch statement
             * below and write a user-friendly error message.
             */
            switch (id)
            {
                case 1000:
                    return "Database does not exist";
                case 1001:
                    return "Stored procedure does not exist";
                case 1002:
                    return "Table does not exist";
                case 1003:
                    return "Column does not exist";
                case 2001:  // DatabaseProperties.aspx (user might not have permission to view properties)
                    return "There was a problem reading the properties of the database. " +
                        "It is possible you are not authorized to view or change the properties of this database.";
                case 2002:  // Used whenever the application tries to connect to a database.
                    return "There was a problem connecting to the database. Please check that " +
                        "the database is available, your login information is correct, and you have permission to access the database.";
                default:
                    return "An unknown error has occurred. If it continues, please try logging out and logging back in.";
            }
        }

        protected void Page_Load(object sender, System.EventArgs e)
        {
            // There are two kinds of errors - custom errors with numbers, and uncaught exceptions
            if (Request["error"] != null)
            {
                ErrorLabel.Text = String.Format("Error {0}: {1}", Server.HtmlEncode(Request["error"]), ErrorLookup(Convert.ToInt32(Request["error"])));
            }
            else if (Request["errormsg"] != null || Request["stacktrace"] != null)
            {
                ErrorLabel.Text = "Error Message: <br>" + Request["errormsg"].Replace("\n", "<br>") + "<br><br>" +
                                  "Stack Trace: <br>" + Request["stacktrace"].Replace("\n", "<br>");
            }
            //else if (HttpContext.Current.Request.QueryString["errorPassCode"] != null)
            //// Check to see if there is an error code in the query string of the redirect url
            //{
            //    ErrorLabel.Text = ErrorLookup(Int32.Parse(HttpContext.Current.Request.QueryString["errorPassCode"]));

            //    Exception x = (Exception)Application["Error"];

            //    while (x != null)
            //    {
            //        ErrorLabel.Text += x.Message.Replace("\n", "<br>") + "<br><br>" + "<br><hr><br>";
            //        x = x.InnerException;
            //    }

            //    Application.Remove("Error");
            //}
            else
            {
                ErrorLabel.Text = "An unknown error has occured. Please try again.";

                Exception x = (Exception)Application["Error"];

                while (x != null)
                {
                    ErrorLabel.Text += x.Message.Replace("\n", "<br>") + "<br><br>" + x.StackTrace.Replace("\n", "<br>") + "<br><hr><br>";
                    x = x.InnerException;
                }

                Application.Remove("Error");
            }
        }
    }
}