using Microsoft.SqlServer.Management.Smo;
using SqlServerWebAdmin.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SqlServerWebAdmin
{
    public partial class BackupDatabase : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Microsoft.SqlServer.Management.Smo.Server server = DbExtensions.CurrentServer;

            var path = server.BackupDirectory;
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
                ExportDatabaseList.Items.Clear();

                for (int i = 0; i < databases.Count; i++)
                {
                    ExportDatabaseList.Items.Add(new ListItem(databases[i].Name));
                }
            }
        }

        protected void BackupButton_Click(object sender, System.EventArgs e)
        {
            Microsoft.SqlServer.Management.Smo.Server server = DbExtensions.CurrentServer;
            string databaseName = this.ExportDatabaseList.SelectedItem.Text;

            Backup bkp = new Backup();

            //this.Cursor = Cursors.WaitCursor;
            //this.dataGridView1.DataSource = string.Empty;

           
            try
            {
                /*Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Backups",*/
                string fileName = string.Format("{0}-{1}.backup", databaseName, DateTime.Now.ToString("yyyyMMdd_HHmmss"));

                bkp.Action = BackupItem.SelectedItem.Value == "Database" ? BackupActionType.Database : BackupActionType.Log;
                bkp.Database = databaseName;
                bkp.Devices.AddDevice(fileName, DeviceType.File);
                //bkp.Incremental = true;
                //this.progressBar1.Value = 0;
                //this.progressBar1.Maximum = 100;
                //this.progressBar1.Value = 10;

                bkp.PercentCompleteNotification = 10;
                //bkp.PercentComplete += new PercentCompleteEventHandler(ProgressEventHandler);

                bkp.SqlBackup(server);
                
                Status.Text = "Database was successfully backed up to: " + fileName;
            }

            catch (Exception ex)
            {
                Response.Redirect(String.Format("error.aspx?errormsg={0}&stacktrace={1}", Server.UrlEncode(ex.Message), Server.UrlEncode(ex.StackTrace)));
            }
            finally
            {
                //this.progressBar1.Value = 0;
            }
        }
    }
}