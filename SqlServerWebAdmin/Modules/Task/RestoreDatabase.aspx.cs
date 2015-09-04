using Microsoft.SqlServer.Management.Smo;
using SqlServerWebAdmin.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SqlServerWebAdmin
{
    public partial class RestoreDatabase : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Microsoft.SqlServer.Management.Smo.Server server = DbExtensions.CurrentServer;

            try
            {
                server.Connect();
            }
            catch (System.Exception ex)
            {
                Response.Redirect(String.Format("error.aspx?errormsg={0}&stacktrace={1}", Server.UrlEncode(ex.Message), Server.UrlEncode(ex.StackTrace)));
            }
            DatabaseCollection databases = server.Databases;
            server.Disconnect();

            // Clear out list and populate with database names
            if (!IsPostBack)
            {
                DatabaseList.Items.Clear();

                for (int i = 0; i < databases.Count; i++)
                {
                    DatabaseList.Items.Add(new ListItem(databases[i].Name));
                }
            }

            DatabaseList.Items.FindByValue(Request["database"]).Selected = true;

            SourceList.DataSource = Directory.EnumerateFiles(server.BackupDirectory).Select(i => Path.GetFileName(i));
            SourceList.DataBind();
        }

        protected void RestoreButton_Click(object sender, System.EventArgs e)
        {
            Microsoft.SqlServer.Management.Smo.Server server = DbExtensions.CurrentServer;
            string databaseName = this.DatabaseList.SelectedItem.Text;

            Restore res = new Restore();

            //this.dataGridView1.DataSource = string.Empty;
            try
            {
                var selectedIndex = SourceList.SelectedRow.Cells[1].Text;
                string fileName = Path.Combine(server.BackupDirectory, selectedIndex);

                if (RestoreItem.SelectedItem.Value == "Restore")
                {
                    res.Database = databaseName;
                    res.Action = RestoreActionType.Database;
                }

                res.Devices.AddDevice(fileName, DeviceType.File);

                //this.progressBar1.Value = 0;
                //this.progressBar1.Maximum = 100;
                //this.progressBar1.Value = 10;
                res.PercentCompleteNotification = 10;
                //res.PercentComplete += new PercentCompleteEventHandler(ProgressEventHandler);
                server.KillAllProcesses(databaseName);
                if (RestoreItem.SelectedItem.Value == "Restore")
                {
                    res.ReplaceDatabase = true;
                    res.SqlRestore(server);

                    Status.Text = "Restore of " + databaseName + " Complete!";
                }
                else
                {
                    bool verifySuccessful = res.SqlVerify(server);

                    if (verifySuccessful)
                    {
                        Status.Text = "Backup Verified!";
                        DataTable dt = res.ReadFileList(server);
                        SourceList.DataSource = dt;
                    }
                    else
                    {
                        Status.Text = "Backup NOT Verified!";
                    }
                }
            }
            //  catch (SmoException exSMO)
            catch (Exception ex)
            {
                Response.Redirect(String.Format("error.aspx?errormsg={0}&stacktrace={1}", Server.UrlEncode(ex.Message), Server.UrlEncode(ex.StackTrace)));
            }
            finally
            {
                //this.progressBar1.Value = 0;
            }
        }

        protected void SourceList_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
        {
            GridViewRow row = SourceList.Rows[e.NewSelectedIndex];

            row.Attributes["style"] = "background: grey";
        }


    }
}