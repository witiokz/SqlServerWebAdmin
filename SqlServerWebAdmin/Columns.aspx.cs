using Microsoft.SqlServer.Management.Smo;
using SqlServerWebAdmin.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SqlServerWebAdmin
{
    public partial class Columns : System.Web.UI.Page
    {
        public Columns()
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

            Microsoft.SqlServer.Management.Smo.Table table = database.Tables[Request["table"]];

            // Set link for add new column
            AddNewColumnHyperLink.NavigateUrl = String.Format("editcolumn.aspx?database={0}&table={1}", Server.UrlEncode(Request["database"]), Server.UrlEncode(Request["table"]));

            if (table != null)
            {
                // The table exists and we do normal column editing

               this.ColumnsDataGrid.Visible = true;
                this.NoColumnsLabel.Visible = false;

                if (!IsPostBack)
                {
                    // Update table properties

                    // Get columns list
                    ColumnCollection columns = table.Columns;

                    DataSet ds = new DataSet();
                    ds.Tables.Add();
                    ds.Tables[0].Columns.Add("key", typeof(bool));
                    ds.Tables[0].Columns.Add("id", typeof(bool));
                    ds.Tables[0].Columns.Add("name", typeof(string));
                    ds.Tables[0].Columns.Add("datatype", typeof(string));
                    ds.Tables[0].Columns.Add("size", typeof(int));
                    ds.Tables[0].Columns.Add("precision", typeof(int));
                    ds.Tables[0].Columns.Add("scale", typeof(int));
                    ds.Tables[0].Columns.Add("nulls", typeof(bool));
                    ds.Tables[0].Columns.Add("default", typeof(string));

                    ds.Tables[0].Columns.Add("encodedname", typeof(string));

                    for (int i = 0; i < columns.Count; i++)
                    {
                        Column columnInfo = columns[i];
                        ds.Tables[0].Rows.Add(new object[] { columnInfo.InPrimaryKey, columnInfo.Identity, Server.HtmlEncode(columnInfo.Name), Server.HtmlEncode(columnInfo.DataType.ToString()), 
                            columnInfo.DataType.MaximumLength, columnInfo.DataType.NumericPrecision, columnInfo.DataType.NumericScale,
                            columnInfo.Nullable, Server.HtmlEncode(columnInfo.Default), Server.UrlEncode(columnInfo.Name) });
                    }
                    this.ColumnsDataGrid.DataSource = ds;
                    this.ColumnsDataGrid.DataBind();
                }

                // If the table has data in it, disable edit column
                if (table.RowCount > 0)
                {
                    this.ColumnsDataGrid.Columns[2].Visible = true;
                    this.ColumnsDataGrid.Columns[3].Visible = false;
                    this.ColumnsDataGrid.Columns[8].Visible = false;
                }
                else
                {
                    //this.ColumnsDataGrid.Columns[2].Visible = false;
                    //this.ColumnsDataGrid.Columns[3].Visible = true;
                    //this.ColumnsDataGrid.Columns[8].Visible = true;
                }

                // If the table has only one column, do not allow delete
                //if (table.Columns.Count == 1)
                //    this.ColumnsDataGrid.Columns[9].Visible = false;
                //else
                //    this.ColumnsDataGrid.Columns[9].Visible = true;
            }
            else
            {
                // The table does not exist, implying that it is new

                this.ColumnsDataGrid.Visible = false;
                NoColumnsLabel.Visible = true;
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
    }
}