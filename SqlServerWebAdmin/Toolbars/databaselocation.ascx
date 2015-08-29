<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="databaselocation.ascx.cs" Inherits="SqlServerWebAdmin.Toolbars.databaselocation" %>

Database:
<a href="Tables.aspx?database=<%=Server.UrlEncode(Request["database"]) %>"><%= Server.UrlEncode(Request["database"]) %></a>