<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="DeleteColumn.aspx.cs" Inherits="SqlServerWebAdmin.DeleteColumn" MasterPageFile="~/Site.master" %>

<asp:content id="MainContent1" contentplaceholderid="MainContent" runat="server">
    <h3>Confirm delete</h3>
    <h4>Are you sure you want to delete column <strong><%= Request.QueryString["column"] %></strong> from table <strong><%= Request.QueryString["table"] %></strong> ? </h4>
    <p>
        <asp:Button ID="YesButton" runat="server" CssClass="btn btn-default" Text="Yes" OnClick="YesButton_Click"></asp:Button>
        <a href="/Modules/Column/columns.aspx?database=<%= Server.UrlEncode(Request["database"]) %>&table=<%=Server.UrlEncode(Request["table"]) %>" class="btn btn-default">No</a>
    </p>
</asp:content>
