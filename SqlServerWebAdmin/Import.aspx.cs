using SqlServerWebAdmin.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SqlServerWebAdmin
{
    public partial class Import : System.Web.UI.Page
    {
        protected void Page_Load(object sender, System.EventArgs e)
        {
            ImportErrorLabel.Visible = false;
            ImportSuccessLabel.Visible = false;
        }

        protected void ImportButton_Click(object sender, System.EventArgs e)
        {
            Microsoft.SqlServer.Management.Smo.Server server = DbExtensions.CurrentServer;

            // Grab file from post data
            HttpPostedFile file = FileUploadInput.PostedFile;

            int length = file.ContentLength;

            byte[] buff = new byte[length];
            file.InputStream.Read(buff, 0, length);

            // Convert from byte array to string
            StringBuilder qsb = new StringBuilder();
            for (int i = 0; i < length; i++)
                qsb.Append(Convert.ToChar(buff[i]));

            string q = qsb.ToString();

            if (q.Trim().Length == 0)
            {
                ImportErrorLabel.Visible = true;
                ImportErrorLabel.Text = "Imported file contains no data.";
                return;
            }

            try
            {
                // No need for connect/disconnect since Query() uses ADO.NET, not DMO
                //server.query(q);
                ImportSuccessLabel.Visible = true;
            }
            catch (SqlException ex)
            {
                ImportErrorLabel.Visible = true;
                ImportErrorLabel.Text = "There was an error importing the database. The status of the import is unknown.<br><br>" +
                    Server.HtmlEncode(ex.Message).Replace("\n", "<br>");
            }
        }
    }
}