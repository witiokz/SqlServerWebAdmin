<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="DatabaseProperties.aspx.cs" Inherits="SqlServerWebAdmin.DatabaseProperties" MasterPageFile="~/Site.master" %>

<%@ Register TagPrefix="FileProperties" TagName="FileProperties" Src="~/Modules/Database/FileProperties.ascx" %>

<asp:Content ID="MainContent1" ContentPlaceHolderID="MainContent" runat="server">
    <h3>Database Properties</h3>

    <asp:Label ID="ErrorLabel" runat="server" Visible="False" ForeColor="red"></asp:Label>
    Property 
                                       Value
                                       Name
                                      <asp:Label ID="NamePropertyLabel" runat="server"></asp:Label>
    Status
                                      <asp:Label ID="StatusPropertyLabel" runat="server"></asp:Label>
    Owner
                                      <asp:Label ID="OwnerPropertyLabel" runat="server"></asp:Label>
    Date created
                                      <asp:Label ID="DateCreatedPropertyLabel" runat="server"></asp:Label>
    Size
                                      <asp:Label ID="SizePropertyLabel" runat="server"></asp:Label>MB
                                      Space available
                                      <asp:Label ID="SpaceAvailablePropertyLabel" runat="server"></asp:Label>MB
                                      Number of users
                                      <asp:Label ID="NumberOfUsersPropertyLabel" runat="server"></asp:Label>
    <b>Data Files</b>
    <fileproperties:fileproperties id="DataFileProperties" runat="server"></fileproperties:fileproperties>
    <b>Transaction Log</b>
    <fileproperties:fileproperties id="LogFileProperties" runat="server"></fileproperties:fileproperties>
    <asp:Button ID="ApplyButton" CssClass="button" runat="server" Text="Apply" OnClick="ApplyButton_Click"></asp:Button>
    <asp:Button ID="CancelButton" CssClass="button" runat="server" Text="Cancel" OnClick="CancelButton_Click"></asp:Button>


</asp:Content>


