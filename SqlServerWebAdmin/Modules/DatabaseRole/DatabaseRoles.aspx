<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="DatabaseRoles.aspx.cs" Inherits="SqlServerWebAdmin.DatabaseRoles" MasterPageFile="~/Site.master" %>

<asp:Content ID="MainContent1" ContentPlaceHolderID="MainContent" runat="server">
    <h3>Database roles</h3>

    <div class="row" style="margin-top: 10px">
        <div class="col-md-12">
            <asp:GridView ID="RolesGrid" runat="server" AutoGenerateColumns="False">
                <Columns>
                    <asp:TemplateField HeaderText="Role">
                        <ItemTemplate>
                            <a href='~/Modules/DatabaseRole/EditDatabaseRole.aspx?database=<%=Request["database"]%>&role=<%#(Container.DataItem)%>'><%# Container.DataItem %></a>
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
            </asp:GridView>
        </div>
    </div>
</asp:Content>

