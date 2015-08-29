using Microsoft.SqlServer.Management.Smo;
using SqlServerWebAdmin.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SqlServerWebAdmin
{
    public partial class EditColumn : System.Web.UI.Page
    {
        public EditColumn()
        {
            Page.Init += new System.EventHandler(Page_Init);
        }

        protected void Page_Load(object sender, System.EventArgs e)
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

            if (!IsPostBack)
            {
                DataLossWarningLabel.Visible = false;

                DataTypeDropdownlist.DataSource = Enum.GetValues(typeof(SqlDataType));
                DataTypeDropdownlist.DataBind();

                // If column isn't specified in request, that means we're adding a new column, not editing an existing one
                if (Request["column"] == null || Request["column"].Length == 0)
                {
                    // Set update button text to "Add" instead of "Update"
                    UpdateButton.Text = "Add";

                    // Create new unique column name
                    string columnName = "";

                    Microsoft.SqlServer.Management.Smo.Table table = database.Tables[Request["table"]];
                    if (table == null)
                    {
                        // If table doesn't exist (e.g. new table), set default column name
                        columnName = "Column1";
                    }
                    else
                    {
                        // Come up with non-existent name ColumnXX
                        int i = 1;
                        do
                        {
                            columnName = "Column" + i;
                            i++;
                        } while (table.Columns[columnName] != null);
                    }

                    // Initialize column editor with default values
                    PrimaryKeyCheckbox.Checked = false;
                    ColumnNameTextbox.Text = columnName;
                    DataTypeDropdownlist.SelectedIndex = DataTypeDropdownlist.Items.IndexOf(new ListItem("char"));
                    LengthTextbox.Text = "10";
                    AllowNullCheckbox.Checked = true;
                    DefaultValueTextbox.Text = "";
                    PrecisionTextbox.Text = "0";
                    ScaleTextbox.Text = "0";
                    IdentityCheckBox.Checked = false;
                    IdentitySeedTextbox.Text = "1";
                    IdentityIncrementTextbox.Text = "1";
                    IsRowGuidCheckBox.Checked = false;
                }
                else
                {
                    // Set update button text to "Update" instead of "Add"
                    UpdateButton.Text = "Update";

                    // Load column from table
                    Microsoft.SqlServer.Management.Smo.Table table = database.Tables[Request["table"]];
                    if (table == null)
                    {
                        server.Disconnect();

                        // Table doesn't exist - break out and go to error page
                        Response.Redirect(String.Format("error.aspx?error={0}", 1002));
                        return;
                    }

                    // Select column from table
                    Column column = table.Columns[Request["column"]];
                    if (column == null)
                    {
                        server.Disconnect();

                        // Column doesn't exist - break out and go to error page
                        Response.Redirect(String.Format("error.aspx?error={0}", 1003));
                        return;
                    }

                    var columnInfo = column;

                    // Initialize column editor
                    PrimaryKeyCheckbox.Checked = column.InPrimaryKey;
                    ColumnNameTextbox.Text = column.Name;
                    OriginalName.Value = column.Name;
                    DataTypeDropdownlist.SelectedIndex = DataTypeDropdownlist.Items.IndexOf(new ListItem(columnInfo.DataType.SqlDataType.ToString()));
                    LengthTextbox.Text = Convert.ToString(columnInfo.DataType.MaximumLength);
                    AllowNullCheckbox.Checked = columnInfo.Nullable;
                    DefaultValueTextbox.Text = columnInfo.Default;
                    PrecisionTextbox.Text = Convert.ToString(columnInfo.DataType.NumericPrecision);
                    ScaleTextbox.Text = Convert.ToString(columnInfo.DataType.NumericScale);
                    IdentityCheckBox.Checked = columnInfo.Identity;
                    IdentitySeedTextbox.Text = Convert.ToString(columnInfo.IdentitySeed);
                    IdentityIncrementTextbox.Text = Convert.ToString(columnInfo.IdentityIncrement);
                    //IsRowGuidCheckBox.Checked = columnInfo.i;

                    // Since we are editing an existing column, the table will be recreated,
                    // so we must warn about data loss
                    DataLossWarningLabel.Visible = true;
                }
            }

            server.Disconnect();
        }

        protected void Page_Init(object sender, EventArgs e)
        {
            if (Page.User.Identity.IsAuthenticated)
            {
                Page.ViewStateUserKey = Page.Session.SessionID;
            }
        }

        protected void UpdateButton_Click(object sender, System.EventArgs e)
        {
            if (!IsValid)
                return;

            Microsoft.SqlServer.Management.Smo.Server server = DbExtensions.CurrentServer;
            try
            {
                server.Connect();
            }
            catch (System.Exception ex)
            {
                Response.Redirect(String.Format("error.aspx?errormsg={0}&stacktrace={1}", Server.UrlEncode(ex.Message), Server.UrlEncode(ex.StackTrace)));
            }

            Database database = server.Databases[HttpContext.Current.Server.HtmlDecode(HttpContext.Current.Request["database"])];



            Microsoft.SqlServer.Management.Smo.Table table = null;
            if (!database.Tables.Contains(Request["table"]))
            {
                table = new Microsoft.SqlServer.Management.Smo.Table(database, Request["table"]);
            }
            else
            {
                table = database.Tables.Cast<Microsoft.SqlServer.Management.Smo.Table>().FirstOrDefault( i => i.Name == Request["table"]);
            }
            

            
            // Parse user input and stick it into ColumnInfo
            var type = DataTypeDropdownlist.SelectedItem.Value;
            SqlDataType sqlDataType = (SqlDataType)Enum.Parse(typeof(SqlDataType), type);
            Column columnInfo = table.Columns.Cast<Column>().FirstOrDefault(i => i.Name == OriginalName.Value);

            if (columnInfo == null)
            {
                columnInfo = new Column(table, ColumnNameTextbox.Text, new DataType(sqlDataType));
                columnInfo.Name = ColumnNameTextbox.Text;
                columnInfo.Default = DefaultValueTextbox.Text;
            }
            else
            {
                columnInfo.Rename(ColumnNameTextbox.Text);
                columnInfo.DataType = new DataType(sqlDataType);
            }

                              
            columnInfo.Identity = PrimaryKeyCheckbox.Checked;
           
            try
            {
                columnInfo.DataType.MaximumLength = Convert.ToInt32(LengthTextbox.Text);
               
            }
            catch
            {
                // Show error and quit
                ErrorUpdatingColumnLabel.Visible = true;
                ErrorUpdatingColumnLabel.Text = "Invalid input: Size must be an integer";
                return;
            }

            columnInfo.Nullable = AllowNullCheckbox.Checked;
            

            try
            {
                columnInfo.DataType.NumericPrecision = Convert.ToInt32(PrecisionTextbox.Text);
            }
            catch
            {
                // Show error and quit
                ErrorUpdatingColumnLabel.Visible = true;
                ErrorUpdatingColumnLabel.Text = "Invalid input: Precision must be an integer";
                return;
            }

            try
            {
                columnInfo.DataType.NumericScale =  Convert.ToInt32(ScaleTextbox.Text);
            }
            catch
            {
                // Show error and quit
                ErrorUpdatingColumnLabel.Visible = true;
                ErrorUpdatingColumnLabel.Text = "Invalid input: Scale must be an integer";
                return;
            }

            columnInfo.Identity = IdentityCheckBox.Checked;

            try
            {
                columnInfo.IdentitySeed = Convert.ToInt32(IdentitySeedTextbox.Text);
            }
            catch
            {
                // Show error and quit
                ErrorUpdatingColumnLabel.Visible = true;
                ErrorUpdatingColumnLabel.Text = "Invalid input: Identity seed must be an integer";
                return;
            }

            try
            {
                columnInfo.IdentityIncrement = Convert.ToInt32(IdentityIncrementTextbox.Text);
            }
            catch
            {
                // Show error and quit
                ErrorUpdatingColumnLabel.Visible = true;
                ErrorUpdatingColumnLabel.Text = "Invalid input: Identity increment must be an integer";
                return;
            }

            //columnInfo.IsRowGuid = IsRowGuidCheckBox.Checked;

            

            // First check if the table exists or not
            // If it doesn't exist, that means we are adding the first column of a new table
            // If it does exist, then either we are adding a new column to an existing table
            //   or we are editing an existing column in an existing table

            if (!database.Tables.Contains(Request["table"]))
            {
                // Table does not exist - create a new table and add the new column
                try
                {
                    table.Columns.Add(columnInfo);
                    table.Create();
                }
                catch (Exception ex)
                {
                    // If the table was somehow created, get rid of it
                    table = database.Tables[Request["table"]];
                    if (table != null)
                        table.Drop();

                    // Show error and quit
                    ErrorUpdatingColumnLabel.Visible = true;
                    ErrorUpdatingColumnLabel.Text = "The following error occured while trying to apply the changes.<br>" + Server.HtmlEncode(ex.Message).Replace("\n", "<br>");

                    server.Disconnect();
                    return;
                }
            }
            else
            {
                // Table does exist, do further check

                // If original name is blank that means it is a new column
                string originalColumnName = Request["column"];

                if (originalColumnName == null || originalColumnName.Length == 0)
                {
                    try
                    {
                        table.Columns.Add(columnInfo);
                        table.Alter();
                    }
                    catch (Exception ex)
                    {
                        // Show error and quit
                        ErrorUpdatingColumnLabel.Visible = true;
                        ErrorUpdatingColumnLabel.Text = "The following error occured while trying to apply the changes:<br>" + Server.HtmlEncode(ex.Message).Replace("\n", "<br>");

                        server.Disconnect();
                        return;
                    }
                }
                else
                {
                    // If we get here that means we are editing an existing column

                    // Simply set the column info - internally the table gets recreated
                    try
                    {
                        columnInfo.Drop();
                        table.Columns.Add(columnInfo);
                        table.Alter();
                        //columnInfo.Alter();   
                    }
                    catch (Exception ex)
                    {
                        // Show error and quit
                        ErrorUpdatingColumnLabel.Visible = true;
                        ErrorUpdatingColumnLabel.Text = "The following error occured while trying to apply the changes.<br>" + Server.HtmlEncode(ex.Message).Replace("\n", "<br>");

                        server.Disconnect();
                        return;
                    }
                }
            }

            server.Disconnect();

            // If we get here then that means a column was successfully added/edited
            Response.Redirect(String.Format("columns.aspx?database={0}&table={1}", Server.UrlEncode(Request["database"]), Server.UrlEncode(Request["table"])));
        }

        protected void CancelButton_Click(object sender, System.EventArgs e)
        {
            // Just redirect back to columns list
            Response.Redirect(String.Format("columns.aspx?database={0}&table={1}", Server.UrlEncode(Request["database"]), Server.UrlEncode(Request["table"])));
        }
    }
}