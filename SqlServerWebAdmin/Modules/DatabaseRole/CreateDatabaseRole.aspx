<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CreateDatabaseRole.aspx.cs" Inherits="SqlServerWebAdmin.CreateDatabaseRole" MasterPageFile="~/Site.master" %>

<asp:Content ID="MainContent1" ContentPlaceHolderID="MainContent" runat="server">
    <h3>Edit role</h3>
    <div class="form-horizontal">
        <div class="form-group">
            <label class="col-md-3 control-label">Role Name</label>
            <div class="col-md-9">
                <asp:TextBox ID="RoleName" MaxLength="32" runat="server" />
                <asp:RequiredFieldValidator ControlToValidate="RoleName" ErrorMessage="* Requied"
                Display="Dynamic" runat="server" />
            </div>
        </div>
        <div class="form-group">
            <label class="col-md-3 control-label">Role Type (SQL Server supports two types of database roles: standard roles, which contain members,
                                                    and database roles, which require a password)</label>
            <div class="col-md-9">
            <asp:RadioButtonList ID="RoleType" runat="server">
                <asp:ListItem Value="Standard" />
                <asp:ListItem Value="Application" />
            </asp:RadioButtonList>
            <asp:RequiredFieldValidator ControlToValidate="RoleType" ErrorMessage="* Required"
                Display="Dynamic" runat="server" />
            </div>
        </div>
        <div class="form-group">
            <div class="col-md-offset-3 col-md-9">
                <asp:Button Text="Create Role" CssClass="btn btn-default" runat="server" />
                <br />
                <asp:Label ID="ErrorMessage" runat="server" />
            </div>
        </div>
    </div>
</asp:Content>
