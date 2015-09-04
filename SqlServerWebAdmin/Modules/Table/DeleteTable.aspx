<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="DeleteTable.aspx.cs" Inherits="SqlServerWebAdmin.DeleteTable"  MasterPageFile="~/Site.master" %>

<asp:content id="MainContent1" contentplaceholderid="MainContent" runat="server">
    <h3>Confirm delete</h3>
    <h4>Are you sure you want to delete stored procedure <strong><%= Request["table"] %></strong> from database <strong><%= Request.QueryString["database"] %></strong>? </h4>
    <p>
        <asp:Button ID="Button1" runat="server" CssClass="btn btn-default" Text="Yes" OnClick="YesButton_Click"></asp:Button>
        <a href="/Modules/Table/tables.aspx?database=<%= Server.UrlEncode(Request["database"]) %>" class="btn btn-default">No</a>
    </p>
</asp:content>