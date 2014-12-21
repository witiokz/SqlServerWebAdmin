//=====================================================================
//
// THIS CODE AND INFORMATION IS PROVIDED TO YOU FOR YOUR REFERENTIAL
// PURPOSES ONLY, AND IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND,
// EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE IMPLIED
// WARRANTIES OF MERCHANTABILITY AND/OR FITNESS FOR A PARTICULAR PURPOSE,
// AND MAY NOT BE REDISTRIBUTED IN ANY MANNER.
//
// Copyright (C) 2003  Microsoft Corporation.  All rights reserved.
//
//=====================================================================
using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using Microsoft.SqlServer.Management.Smo;
using SqlServerWebAdmin.Models;
using Microsoft.Win32;
using System.Collections.Generic;

using System.Linq;
using System.ServiceProcess;
using System.Diagnostics;

namespace SqlWebAdmin
{
    /// <summary>
    /// Summary description for Login.
    /// </summary>
    public partial class Login : Page
    {
        public Login()
        {
            Page.Init += new System.EventHandler(Page_Init);
        }

        protected void Page_Load(object sender, System.EventArgs e)
        {

            //if (this.ServerTextBox.Text != null && this.ServerTextBox.Text.Length == 0)
            //{
            //    ServerTextBox.Text = "(local)"; // "localhost";
            //}

            this.ErrorLabel.Visible = false;
            this.LogoutInfoLabel.Visible = false;
            this.LoginInfoLabel.Visible = false;

            if (Request["error"] != null)
            {
                switch (Request["error"])
                {
                    case "sessionexpired":
                        ErrorLabel.Text = "Your session has expired or you have already logged out. Please enter your login info again to reconnect.";
                        break;
                    case "userinfo":
                        ErrorLabel.Text = "Invalid username and/or password, or server does not exist.";
                        break;
                    default:
                        ErrorLabel.Text = "An unknown error occured.";
                        break;
                }
                ErrorLabel.Visible = true;
            }
            else if (Request["action"] == "logout")
            {
                this.LogoutInfoLabel.Visible = true;
            }
            else
            {
                LoginInfoLabel.Visible = true;
            }

            if (!IsPostBack)
            {
                SqlServerDLL.DataSource = GetSqlServers();
                SqlServerDLL.DataValueField = "Key";
                SqlServerDLL.DataTextField = "Value";
                SqlServerDLL.DataBind();

                    this.UsernameTextBox.Text = GetCurrentUser();
                    UsernameTextBox.Enabled = false;
                    PasswordTextBox.Enabled = false;
            }
        }

        protected void Page_Init(object sender, EventArgs e)
        {
            if (Page.User.Identity.IsAuthenticated)
            {
                Page.ViewStateUserKey = Page.Session.SessionID;
            }
            
        }

        protected void LoginButton_Click(object sender, System.EventArgs e)
        {
            if (!IsValid)
                return;

            var serverName = SqlServerDLL.SelectedItem.Text;//ServerTextBox.Text

            bool useIntegrated;
            Server server = new Server(serverName);

            var connected = false;

            if (AuthRadioButtonList.SelectedItem.Value == "windows")
            {
                if (GetCurrentUser() != this.UsernameTextBox.Text)
                {
                    ErrorLabel.Visible = true;
                    ErrorLabel.Text = "IIS verion of SQL Web Admin doesn't support windows logins other than your own.<br>";
                }

                try
                {
                   // server = new SqlServer(ServerTextBox.Text, this.UsernameTextBox.Text, this.PasswordTextBox.Text, true);

                    //Using windows authentication
                    server.ConnectionContext.LoginSecure = true;
                    server.ConnectionContext.Connect();
                    connected = true;


                    useIntegrated = true;
                }
                catch (System.ComponentModel.Win32Exception w32Ex)
                {
                    ErrorLabel.Visible = true;
                    ErrorLabel.Text = "Invalid username and/or password, or server does not exist.";
                    return;
                }
                catch (Exception ex)
                {
                    ErrorLabel.Visible = true;
                    ErrorLabel.Text = ex.Message;
                    return;
                }
            }
            else
            {
                //Using SQL Server authentication
                server.ConnectionContext.LoginSecure = false;
                server.ConnectionContext.Login = UsernameTextBox.Text;
                server.ConnectionContext.Password = PasswordTextBox.Text;
                server.ConnectionContext.Connect();
                useIntegrated = false;
            }

            if (connected)
            {
                if (useIntegrated)
                {
                    AdminUser.CurrentUser = new AdminUser(UsernameTextBox.Text, this.PasswordTextBox.Text, serverName, true);
                    DbUtlity.WriteCookieForFormsAuthentication(UsernameTextBox.Text, PasswordTextBox.Text, false, SqlLoginType.NTUser);
                }
                else
                {
                    AdminUser.CurrentUser = new AdminUser(this.UsernameTextBox.Text, this.PasswordTextBox.Text, serverName, false);
                    DbUtlity.WriteCookieForFormsAuthentication(
                        UsernameTextBox.Text,
                        PasswordTextBox.Text,
                        false,
                        SqlLoginType.Standard);
                }
                Response.Redirect("databases.aspx");
            }
            else
            {
                ErrorLabel.Visible = true;
                ErrorLabel.Text = "Invalid username and/or password, you are using a windows login that is not your own, or server does not exist.<br>";
            }
        }

        protected void AuthRadioButtonList_SelectedIndexChanged(object sender, System.EventArgs e)
        {
            string authMethod = AuthRadioButtonList.SelectedItem.Value;

            switch (authMethod)
            {
                case "sql":
                    this.UsernameTextBox.Text = "";
                    this.UsernameTextBox.Enabled = true;
                    this.PasswordTextBox.Enabled = true;
                    break;
                case "windows":
                        this.UsernameTextBox.Text = GetCurrentUser();
                        this.UsernameTextBox.Enabled = false;
                        this.PasswordTextBox.Enabled = false;
                    
                    break;
                default:
                    break;
            }
        }
        private Dictionary<string, string> GetSqlServers()
        {
            DataTable dt = SmoApplication.EnumAvailableSqlServers(true);
            List<string> servers = new List<string>();
            var sqlServers = new Dictionary<string, string>();

            if (dt.Rows.Count > 0)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    if (servers.Contains("\\") || servers.Contains("/"))
                    {
                        sqlServers.Add(sqlServers.Count.ToString(), dr["Name"].ToString());
                    }
                    else
                    {
                        servers.Add(dr["Name"].ToString());
                    }
                }
            }

            if (servers.Count > 0)
            {
                List<string> processes = new List<string>();
                RegistryKey rk = Registry.LocalMachine.OpenSubKey(@"SOFTWARE\Microsoft\Microsoft SQL Server");
                String[] instances = (String[])rk.GetValue("InstalledInstances");
                foreach (var instance in instances)
                {
                    processes.Add(instance);
                }

                foreach (var item in rk.GetSubKeyNames())
                {
                    if(!processes.Any(i => i.Contains(item)))
                    {
                        processes.Add(item);
                    }
                }

                var services = ServiceController.GetServices();

                foreach (var item in processes.Where(i => i.ToLower().Contains("sql")))
                {
                    foreach (var server in servers)
                    {
                        var sc = services.FirstOrDefault(i => i.ServiceName.Contains(item));

		                if(sc != null && sc.Status == ServiceControllerStatus.Running)
                        {
                            sqlServers.Add(sqlServers.Count.ToString(), server + "\\" + item);
                        }
	                    
                    }

                }
            }

            return sqlServers;
        }

        private string GetCurrentUser()
        {
            return System.Security.Principal.WindowsIdentity.GetCurrent().Name;
        }
    }
}