<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="EditServerLogin.aspx.cs" Inherits="SqlServerWebAdmin.EditServerLogin" MasterPageFile="~/Site.master" %>

<%@ Reference Page="~/ServerRoles.aspx" %>
<%@ Reference Page="~/Databases.aspx" %>

<asp:content id="MainContent1" contentplaceholderid="MainContent" runat="server">
    <h3>Edit login <asp:Label ID="LoginLabel" runat="server" /></h3>
    <div class="form-horizontal">
        <div class="form-group">
            <label class="col-md-3 control-label">Sections</label>
            <div class="col-md-9">
                <asp:DropDownList ID="Sections" AutoPostBack="True" OnSelectedIndexChanged="Sections_Changed"
                                                runat="server">
                                                <asp:ListItem Selected="True" Value="General">General</asp:ListItem>
                                                <asp:ListItem Value="Roles">Server Roles</asp:ListItem>
                                                <asp:ListItem Value="Databases">Database Access</asp:ListItem>
                                            </asp:DropDownList>
            </div>
         </div>

        

<asp:Panel ID="GeneralPanel" Visible="False" runat="server">
    General
    Authentication
    <asp:Label ID="SecurityAccessLabel" Enabled="False" runat="server">
        Security Access:
    </asp:Label>
    <asp:RadioButtonList ID="SecurityAccess" Enabled="False" RepeatDirection="Horizontal"
        runat="server">
        <asp:ListItem Value="Grant">Grant access</asp:ListItem>
        <asp:ListItem Value="Deny">Deny access</asp:ListItem>
    </asp:RadioButtonList>

    Defaults
    Database:
    <asp:DropDownList ID="DefaultDatabase" DataValueField="Name" runat="server" />
    Language:
    <asp:DropDownList ID="DefaultLanguage" DataValueField="Name" DataTextField="Alias"
        runat="server" />
 </asp:Panel>

<asp:Panel ID="RolesPanel" colspan="2" Visible="False" runat="server">
                                                            Server Roles
                                                            <asp:CheckBoxList ID="ServerRoles" DataValueField="Name" runat="server" />
                                                </asp:Panel>

<asp:Panel ID="DatabasesPanel" colspan="2" Visible="False" runat="server">
    Database Access
    <asp:GridView ID="DatabaseAccessGrid" DataKeyField="Name" AutoGenerateColumns="False"
        OnItemCommand="DatabaseAccessGrid_ItemCommand" OnItemDataBound="DatabaseAccessGrid_Databound"
        Class="TablesDataGrid" Border="0" CellPadding="4" CellSpacing="1" runat="server">
        <Columns>
            <asp:TemplateField HeaderText="Permit" ItemStyle-HorizontalAlign="Center">
                <ItemTemplate>
                    <asp:CheckBox ID="DatabaseAccess" runat="server" />
                </ItemTemplate>
            </asp:TemplateField>
            <asp:BoundField DataField="Name" HeaderText="Database" />
            <asp:ButtonField ButtonType="LinkButton" Text="Database Roles" CommandName="EditRoles" />
        </Columns>
    </asp:GridView>
</asp:Panel>
         <div class="form-group">
            <div class="col-md-offset-3 col-md-9">
                <asp:Button runat="server" CssClass="btn btn-default" Text="Save Changes" OnClick="AddSave_ClickLogin_Click"></asp:Button>
                 <asp:Label ID="ErrorMessage" ForeColor="Red" runat="server" />
            </div>
        </div>
    </div>
</asp:content>


