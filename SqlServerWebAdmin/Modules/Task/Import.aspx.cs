﻿using SqlServerWebAdmin.Models;
using System;
using System.Collections.Generic;
using System.Data;
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
                ImportLabel.Text = "Imported file contains no data.";
                return;
            }

            try
            {
                // No need fo9r connect/disconnect since Query() uses ADO.NET, not DMO
                //server.(q);


                var strConnString = server.ConnectionContext.SqlConnectionObject.ConnectionString;
                using(SqlConnection con = new SqlConnection(strConnString))
                {
                    con.Open();

                    using (SqlCommand cmd = new SqlCommand(q))
                    {
                        cmd.Connection = con;
                        cmd.CommandType = CommandType.Text;
                        cmd.ExecuteNonQuery();

                    }
                }

                ImportLabel.Text = "The database was successfully imported";
            }
            catch (SqlException ex)
            {
                ImportLabel.Text = "There was an error importing the database. The status of the import is unknown.<br><br>" +
                    Server.HtmlEncode(ex.Message).Replace("\n", "<br>");
            }
        }
    }
}