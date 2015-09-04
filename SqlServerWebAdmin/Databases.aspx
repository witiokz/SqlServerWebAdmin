<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Databases.aspx.cs" Inherits="SqlServerWebAdmin.Databases" MasterPageFile="~/Site.master" %>

<asp:Content ID="MainContent1" ContentPlaceHolderID="MainContent" runat="server">
    <h3>Databases</h3>

    <div class="row">
        <div class="col-md-12">
                <asp:HyperLink runat="server" CssClass="btn btn-success pull-right" ID="AddNewDatabaseHyperLink" NavigateUrl="~/Modules/Database/CreateDatabase.aspx">
                    Create new database
                </asp:HyperLink>
        </div>
    </div>
    <div class="row" style="margin-top: 10px">
        <div class="col-md-12">
        <asp:GridView ID="DatabasesDataGrid" CssClass="table table-bordered" runat="server" GridLines="None" Border="0" AutoGenerateColumns="False">
        <Columns>
            <asp:TemplateField HeaderText="Name">
                <HeaderStyle Wrap="False"></HeaderStyle>
                <ItemStyle Wrap="False"></ItemStyle>
                <ItemTemplate>
                    <a href='<%# string.Format("~/Modules/Table/Tables.aspx?database={0}", DataBinder.Eval(Container.DataItem, "encodedname")) %>'>
                        <span class="glyphicon glyphicon-oil"></span>
                    </a>
                    &nbsp;
                    <asp:HyperLink runat="server" Text='<%# "&amp;nbsp;" + DataBinder.Eval(Container.DataItem, "name") %>' NavigateUrl='<%# String.Format("~/Modules/Table/tables.aspx?database={0}", DataBinder.Eval(Container.DataItem, "encodedname")) %>' />
                </ItemTemplate>
            </asp:TemplateField>
            <asp:BoundField DataField="size" HeaderText="Size" DataFormatString="{0}" HeaderStyle-Width="30px">
            </asp:BoundField>
            <asp:TemplateField HeaderStyle-Width="10px">
                <ItemTemplate>
                    <asp:HyperLink ID="QueryDatabase" runat="server" NavigateUrl='<%# String.Format("~/Modules/Database/querydatabase.aspx?database={0}", DataBinder.Eval(Container.DataItem, "encodedname")) %>'>
                        <span class="glyphicon glyphicon-edit"></span>
                    </asp:HyperLink>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="" HeaderStyle-Width="10px">
                <ItemTemplate>
                    <asp:HyperLink ID="DeleteDatabase" runat="server" NavigateUrl='<%# String.Format("~/Modules/Database/deletedatabase.aspx?database={0}", DataBinder.Eval(Container.DataItem, "encodedname")) %>'>
                         <span class="glyphicon glyphicon-remove-sign text-danger"></span>
                    </asp:HyperLink>
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>
    </asp:GridView>
        </div>
    </div>
   
</asp:Content>

