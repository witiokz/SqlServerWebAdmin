<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="DeleteDatabaseRole.aspx.cs" Inherits="SqlServerWebAdmin.DeleteDatabaseRole" MasterPageFile="~/Site.master" %>


<asp:content id="MainContent1" contentplaceholderid="MainContent" runat="server">
    <h3>Delete database role</h3>
    <h4>Are you sure you want to delete the database role<strong><%= Request["role"] %></strong>? </h4>
    <p>
        <asp:Button runat="server" CssClass="btn btn-default" Text="Yes" OnClick="Yes_Click"></asp:Button>
        <a href="<%= ResolveUrl("DatabaseRoles.aspx?database=" + Server.UrlEncode(Request["database"])) %>" class="btn btn-default">No</a>
    </p>
</asp:content>


