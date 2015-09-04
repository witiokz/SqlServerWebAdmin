<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="helplogouttoolbar.ascx.cs" Inherits="SqlServerWebAdmin.Toolbars.helplogouttoolbar" %>

<li>
<asp:HyperLink id="HelpImageHyperLink" runat="server" Target="_blank">
    Documentation
</asp:HyperLink>
</li>
<li>
    <asp:HyperLink id="LogoutImageHyperLink" NavigateUrl="~/Modules/Account/Logout.aspx" runat="server" Text="Logout" />
</li>


