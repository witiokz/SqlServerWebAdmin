<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="servertoolbar.ascx.cs" Inherits="SqlServerWebAdmin.Toolbars.servertoolbar" %>

<%@ Register TagPrefix="Toolbar" TagName="Database" Src="DatabaseToolbar.ascx" %>

<ul class="nav nav-pills nav-stacked">
    <li class="nav-header">Server Tools</li>
    <li> <asp:HyperLink CssClass="leftMenu" NavigateUrl="../databases.aspx" runat="server">
               <span class="glyphicon glyphicon-list-alt"></span> Databases
		 </asp:HyperLink>
    </li>
    <li> <asp:HyperLink id="SecurityHyperLink" CssClass="leftMenu" NavigateUrl = "~/Modules/Security/security.aspx" runat="server">
        <span class="glyphicon glyphicon-flash"></span> Security
         </asp:HyperLink>
    </li>
</ul>

<% if(Request["database"] != null) { %>
    <Toolbar:Database Selected="" runat="server" ID="DatabaseToolbar"></Toolbar:Database>
<% } %>