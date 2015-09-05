<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="DatabaseUsers.aspx.cs" Inherits="SqlServerWebAdmin.DatabaseUsers" MasterPageFile="~/Site.master" %>

<asp:Content ID="MainContent1" ContentPlaceHolderID="MainContent" runat="server">
    <h3>Database users</h3>

    <div class="row">
        <div class="col-md-12">
            <a class="btn btn-success pull-right" href="<%= ResolveUrl("CreateDatabaseUser.aspx?database=" + Server.UrlEncode(database.Name)%>">Create new user</a>
        </div>
    </div>

    <div class="row" style="margin-top: 10px">
        <div class="col-md-12">
            <asp:GridView ID="UsersGrid" runat="server" AutoGenerateColumns="False">
                <Columns>
                    <asp:TemplateField HeaderText="Name">
                        <ItemTemplate>
                            <a href='EditDatabaseUser.aspx?database=<%=Request["database"]%>&user=<%#(Container.DataItem)%>'><%# (Container.DataItem)%></a>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:BoundField HeaderText="Login Name" DataField="Login" />
                    <asp:BoundField HeaderText="Permit" DataField="HasDbAccess" />
                    <asp:TemplateField>
                        <ItemTemplate>
                            <a href='DeleteDatabaseUser.aspx?database=<%=Request["database"]%>&user=<%#(Container.DataItem)%>'>
                                <span class="glyphicon glyphicon-remove-sign text-danger"></span>
                            </a>
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
            </asp:GridView>
        </div>
    </div>
</asp:Content>


