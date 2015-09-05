<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="DeleteDatabaseUser.aspx.cs" Inherits="SqlServerWebAdmin.DeleteDatabaseUser" MasterPageFile="~/Site.master" %>

<asp:content id="MainContent1" contentplaceholderid="MainContent" runat="server">
    <h3>Delete database user</h3>
    <h4>Are you sure you want to delete the database role<strong><%= Request["user"] %></strong>? </h4>
    <p>
        <asp:Button runat="server" CssClass="btn btn-default" Text="Yes" OnClick="Yes_Click"></asp:Button>
        <a href="<%= ResolveUrl("~/Modules/DatabaseUser/DatabaseUsers.aspx?database=" + Server.UrlEncode(Request["database"])) %>" class="btn btn-default">No</a>
    </p>
</asp:content>