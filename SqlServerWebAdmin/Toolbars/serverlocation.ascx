<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="serverlocation.ascx.cs" Inherits="SqlServerWebAdmin.Toolbars.serverlocation" %>

<%@ Register TagPrefix="Location" TagName="Database" Src="DatabaseLocation.ascx" %>

<span class="glyphicon glyphicon-hdd" aria-hidden="true"></span> Server:
<asp:HyperLink id="ServerNameHyperLink" runat="server" CssClass="currentItem" />
&nbsp;&nbsp; 

<% if (Request["database"] != null) { %>
     <Location:Database runat="Server" ID="DatabaseLocation"></Location:Database>
<% } %>