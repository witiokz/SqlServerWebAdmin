<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="serverlocation.ascx.cs" Inherits="SqlServerWebAdmin.Toolbars.serverlocation" %>


<span class="glyphicon glyphicon-hdd" aria-hidden="true"></span>Server:
<asp:HyperLink ID="ServerNameHyperLink" runat="server" CssClass="currentItem" />
&nbsp;&nbsp; 

<% if (Request["database"] != null)
   { %>
    Database:
    <a href="Tables.aspx?database=<%=Server.UrlEncode(Request["database"]) %>"><%= Server.UrlEncode(Request["database"]) %></a>
<% } %>

<% if (Request["table"] != null)
   { %>
      Table:
<a href="/Modules/Column/Columns.aspx?database=<%=Server.UrlEncode(Request["database"]) %>&table=<%=Server.UrlEncode(Request["table"]) %>"><%= Server.UrlEncode(Request["table"]) %></a>

<% } %>
