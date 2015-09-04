<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="tablelocation.ascx.cs" Inherits="SqlServerWebAdmin.Toolbars.tablelocation" %>

Table:
<a href="/Modules/Column/Columns.aspx?database=<%=Server.UrlEncode(Request["database"]) %>&table=<%=Server.UrlEncode(Request["table"]) %>"><%= Server.UrlEncode(Request["table"]) %></a>
