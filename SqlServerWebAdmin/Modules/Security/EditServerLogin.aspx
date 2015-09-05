<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="EditServerLogin.aspx.cs" Inherits="SqlServerWebAdmin.EditServerLogin" MasterPageFile="~/Site.master" %>

<%@ Reference Page="~/Modules/Security/ServerRoles.aspx" %>
<%@ Reference Page="~/Databases.aspx" %>


<asp:Content ID="MainContent1" ContentPlaceHolderID="MainContent" runat="server">
    <h3>Edit login
        <asp:Label ID="LoginLabel" runat="server" /></h3>
    <div class="form-horizontal">
        <div class="form-group">
            <label class="col-md-3 control-label">Sections</label>
            <div class="col-md-9">
                <asp:DropDownList ID="Sections" CssClass="form-control" AutoPostBack="True" OnSelectedIndexChanged="Sections_Changed"
                    runat="server">
                    <asp:ListItem Selected="True" Value="General">General</asp:ListItem>
                    <asp:ListItem Value="Roles">Server Roles</asp:ListItem>
                    <asp:ListItem Value="Databases">Database Access</asp:ListItem>
                </asp:DropDownList>
            </div>
        </div>
        <asp:Panel ID="GeneralPanel" Visible="False" runat="server">
            <div class="form-group">
                <label class="col-md-3 control-label">General Authentication. Security Access</label>
                <div class="col-md-9">
                    <asp:RadioButtonList ID="SecurityAccess" CssClass="form-control" Enabled="False" RepeatDirection="Horizontal"
                        runat="server">
                        <asp:ListItem Value="Grant">Grant access</asp:ListItem>
                        <asp:ListItem Value="Deny">Deny access</asp:ListItem>
                    </asp:RadioButtonList>
                </div>
            </div>
            <div class="form-group">
                <label class="col-md-3 control-label">Default Database</label>
                <div class="col-md-9">
                    <asp:DropDownList CssClass="form-control" ID="DefaultDatabase" DataValueField="Name" runat="server" />
                </div>
            </div>
            <div class="form-group">
                <label class="col-md-3 control-label">Language</label>
                <div class="col-md-9">
                    <asp:DropDownList ID="DefaultLanguage" CssClass="form-control" DataValueField="Name" DataTextField="Alias"
                        runat="server" />
                </div>
            </div>
        </asp:Panel>
        <asp:Panel ID="RolesPanel" Visible="False" runat="server">
            <div class="form-group">
                <label class="col-md-3 control-label">Server Roles</label>
                <div class="col-md-9">
                    <asp:CheckBoxList ID="ServerRoles" CssClass="row col-md-10" DataValueField="Name" runat="server" />
                </div>
            </div>
        </asp:Panel>
        <asp:Panel ID="DatabasesPanel" colspan="2" Visible="False" runat="server">
            <div class="form-group">
                <label class="col-md-3 control-label">Database Access</label>
                <div class="col-md-9">
                    <asp:GridView ID="DatabaseAccessGrid" CssClass="table table-bordered" DataKeyField="Name" AutoGenerateColumns="False"
                        OnItemCommand="DatabaseAccessGrid_ItemCommand" OnItemDataBound="DatabaseAccessGrid_Databound"
                        runat="server">
                        <Columns>
                            <asp:TemplateField HeaderText="Permit" ItemStyle-HorizontalAlign="Center">
                                <ItemTemplate>
                                    <asp:CheckBox ID="DatabaseAccess" runat="server" />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:BoundField DataField="Name" HeaderText="Database" />
                            <asp:ButtonField ButtonType="Link" Text="Database Roles" CommandName="EditRoles" />
                        </Columns>
                    </asp:GridView>
                </div>
            </div>
        </asp:Panel>
        <div class="form-group">
            <div class="col-md-offset-3 col-md-9">
                <asp:Button runat="server" CssClass="btn btn-default" Text="Save Changes" OnClick="Save_Click"></asp:Button>
                <asp:Label ID="ErrorMessage" ForeColor="Red" runat="server" />
            </div>
        </div>
    </div>
</asp:Content>


