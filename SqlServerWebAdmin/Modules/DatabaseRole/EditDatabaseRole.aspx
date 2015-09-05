<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="EditDatabaseRole.aspx.cs" Inherits="SqlServerWebAdmin.EditDatabaseRole" MasterPageFile="~/Site.master"%>

<asp:content id="MainContent1" contentplaceholderid="MainContent" runat="server">
    <h3>Edit role</h3>
    <div class="form-horizontal">
        <div class="form-group">
            <label class="col-md-3 control-label">Role Name</label>
            <div class="col-md-9">
                <asp:Label CssClass="control-label" ID="RoleNameLabel" runat="server" />
            </div>
        </div>
        <div class="form-group">
            <label class="col-md-3 control-label">Role Type</label>
            <div class="col-md-9">
                <asp:Label ID="RoleTypeLabel" CssClass="control-label" runat="server" />
            </div>
        </div>
        <asp:Panel ID="StandardRolePanel" Visible="False" runat="server">
            Role Users:
            <asp:CheckBoxList ID="RoleUsers" runat="server" />
        </asp:Panel>
        <asp:Panel ID="ApplicationRolePanel" Visible="False" runat="server">
            Role Password:
            <asp:TextBox ID="RolePassword" MaxLength="32" TextMode="Password" runat="server" />
        </asp:Panel>
        <div class="form-group">
            <div class="col-md-offset-3 col-md-9">
                 <asp:Button Text="Save Role" CssClass="btn btn-default"  OnClick="Save_Click" runat="server" />
                <br />
                <asp:Label ID="ErrorMessage" runat="server" />
            </div>
        </div>
    </div>
</asp:content>



