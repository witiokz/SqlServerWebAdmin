<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Databases.aspx.cs" Inherits="SqlServerWebAdmin.Databases" MasterPageFile="~/Site.master" %>

<asp:Content ID="MainContent1" ContentPlaceHolderID="MainContent" runat="server">
    <h3>DATABASES </h3>

    <asp:HyperLink runat="server" CssClass="createLink btn btn-success" ID="AddNewDatabaseHyperLink" NavigateUrl="CreateDatabase.aspx">
        Create new database
    </asp:HyperLink>

    <asp:DataGrid ID="DatabasesDataGrid" runat="server" GridLines="None" Border="0" AutoGenerateColumns="False" Width="100%">
        <Columns>
            <asp:TemplateColumn HeaderText="Name">
                <HeaderStyle Wrap="False"></HeaderStyle>
                <ItemStyle Wrap="False"></ItemStyle>
                <ItemTemplate>
                    <a href='<%# String.Format("tables.aspx?database={0}", DataBinder.Eval(Container.DataItem, "encodedname")) %>'>
                        <span class="glyphicon glyphicon-oil"></span>
                    </a>
                    &nbsp;
                    <asp:HyperLink ID="Hyperlink1" runat="server" Text='<%# "&amp;nbsp;" + DataBinder.Eval(Container.DataItem, "name") %>' NavigateUrl='<%# String.Format("tables.aspx?database={0}", DataBinder.Eval(Container.DataItem, "encodedname")) %>' />
                </ItemTemplate>
            </asp:TemplateColumn>
            <asp:BoundColumn DataField="size" HeaderText="Size" DataFormatString="{0}">
                <HeaderStyle Wrap="False" HorizontalAlign="Right"></HeaderStyle>
                <ItemStyle Wrap="False" HorizontalAlign="Right"></ItemStyle>
            </asp:BoundColumn>
            <asp:TemplateColumn HeaderText="Edit">
                <HeaderStyle Wrap="False" HorizontalAlign="Right"></HeaderStyle>
                <ItemStyle Wrap="False" HorizontalAlign="Right"></ItemStyle>
                <ItemTemplate>
                    <asp:HyperLink ID="EditDatabase" runat="server" Text="edit" NavigateUrl='<%# String.Format("tables.aspx?database={0}", DataBinder.Eval(Container.DataItem, "encodedname")) %>'>
                    </asp:HyperLink>
                </ItemTemplate>
            </asp:TemplateColumn>
            <asp:TemplateColumn HeaderText="Query">
                <HeaderStyle Wrap="False" HorizontalAlign="Right"></HeaderStyle>
                <ItemStyle Wrap="False" HorizontalAlign="Right"></ItemStyle>
                <ItemTemplate>
                    <asp:HyperLink ID="QueryDatabase" runat="server" Text="query" NavigateUrl='<%# String.Format("querydatabase.aspx?database={0}", DataBinder.Eval(Container.DataItem, "encodedname")) %>'>
                    </asp:HyperLink>
                </ItemTemplate>
            </asp:TemplateColumn>
            <asp:TemplateColumn HeaderText="Delete">
                <HeaderStyle Wrap="False" HorizontalAlign="Right"></HeaderStyle>
                <ItemStyle Wrap="False" HorizontalAlign="Right"></ItemStyle>
                <ItemTemplate>
                    <asp:HyperLink ID="DeleteDatabase" runat="server" Text="delete" NavigateUrl='<%# String.Format("deletedatabase.aspx?database={0}", DataBinder.Eval(Container.DataItem, "encodedname")) %>'>
                    </asp:HyperLink>
                </ItemTemplate>
            </asp:TemplateColumn>
        </Columns>
    </asp:DataGrid>
</asp:Content>

