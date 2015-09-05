<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ServerRoles.aspx.cs" Inherits="SqlServerWebAdmin.ServerRoles" MasterPageFile="~/Site.master" %>

<asp:Content ID="MainContent1" ContentPlaceHolderID="MainContent" runat="server">
    <h3>Server roles</h3>

    <div class="row" style="margin-top: 10px">
        <div class="col-md-12">
            <asp:GridView ID="RoleDataGrid" runat="server" CssClass="table table-bordered" AutoGenerateColumns="False">
                <Columns>
                    <asp:TemplateField HeaderText="FullName">
                        <ItemTemplate>
                            <a href='/Modules/Security/EditServerRole.aspx?Role=<%# DataBinder.Eval(Container.DataItem, "FullName") %>'> <%# DataBinder.Eval(Container.DataItem, "FullName") %></a>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:BoundField DataField="Name" HeaderText="Name" DataFormatString="{0}"></asp:BoundField>
                    <asp:BoundField DataField="Description" HeaderText="Description" DataFormatString="{0}"></asp:BoundField>
                </Columns>
            </asp:GridView>
        </div>
    </div>
</asp:Content>










