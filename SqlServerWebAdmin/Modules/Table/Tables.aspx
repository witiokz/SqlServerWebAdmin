<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Tables.aspx.cs" Inherits="SqlServerWebAdmin.Tables" MasterPageFile="~/Site.master" %>

<asp:content id="MainContent1" contentplaceholderid="MainContent" runat="server">
    <h3>Tables</h3>

        <div class="row">
            <div class="col-md-10">
                <asp:DropDownList ID="TableTypeDropDownList" runat="server" CssClass="form-control">
                                <asp:ListItem Value="User Tables Only">Show User Tables Only</asp:ListItem>
                                <asp:ListItem Value="User and System Tables">Show User and System Tables</asp:ListItem>
                            </asp:DropDownList>
            </div>
            <div class="col-md-2">
                <asp:Button ID="FilterTablesButton" runat="server" Text="Filter" CssClass="btn btn-default"></asp:Button>
            </div>
       </div>

        <div class="row" style="margin-top: 10px">
            <div class="col-md-12">
                <a class="btn btn-success pull-right" href="/Modules/Table/createtable.aspx?database=<%= Server.UrlEncode(Request["database"]) %>">Create new table</a>
             </div>
         </div>

        <div class="row" style="margin-top: 10px">
            <div class="col-md-12">
        <asp:GridView ID="TablesDataGrid" runat="server" GridLines="None" Border="0" AutoGenerateColumns="False" CssClass="table table-bordered" CellPadding="4" CellSpacing="1">
        <Columns>
            <asp:TemplateField HeaderText="Name">
                <ItemTemplate>
                    <a href='<%# string.Format("~/Modules/Column/columns.aspx?database={0}&table={1}", Server.UrlEncode(Request["database"]), DataBinder.Eval(Container.DataItem, "encodedname")) %>'>
                    <asp:HyperLink ID="Hyperlink1" runat="server" Text='<%# "&amp;nbsp;" + DataBinder.Eval(Container.DataItem, "name") %>'
                        CssClass="databaseListBlack" NavigateUrl='<%# String.Format("~/Modules/Column/columns.aspx?database={0}&table={1}", Server.UrlEncode(Request["database"]), DataBinder.Eval(Container.DataItem, "encodedname")) %>'>
                    </asp:HyperLink>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Owner">
                <HeaderStyle Wrap="False"></HeaderStyle>
                <ItemStyle Wrap="False"></ItemStyle>
                <ItemTemplate>
                    <asp:HyperLink ID="Hyperlink2" runat="server" Text='<%# "&amp;nbsp;" + DataBinder.Eval(Container.DataItem, "Owner") %>'
                        CssClass="databaseListBlack" NavigateUrl='<%# String.Format("EditDatabaseUser.aspx?database={0}&user={1}", Server.UrlEncode(Request["database"]), DataBinder.Eval(Container.DataItem, "owner")) %>'>
                    </asp:HyperLink>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Type">
                <HeaderStyle Wrap="False"></HeaderStyle>
                <ItemStyle Wrap="False"></ItemStyle>
                <ItemTemplate>
                    <%# DataBinder.Eval(Container.DataItem, "type") %>
                    &nbsp;
                </ItemTemplate>
            </asp:TemplateField>
            <asp:BoundField DataField="createdate" HeaderText="Create Date" DataFormatString="{0}">
            </asp:BoundField>
            <asp:BoundField DataField="rows" HeaderText="Rows" DataFormatString="{0}">
            </asp:BoundField>
            <asp:TemplateField HeaderText="" HeaderStyle-Width="10px">
                <ItemTemplate>
                    <asp:HyperLink ID="RenameHyperLink" runat="server" 
                        NavigateUrl='<%# String.Format("renametable.aspx?database={0}&table={1}", Server.UrlEncode(Request["database"]), DataBinder.Eval(Container.DataItem, "encodedname")) %>'>
                         <span class="glyphicon glyphicon-edit"></span>
                    </asp:HyperLink>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="" HeaderStyle-Width="10px">
                <ItemTemplate>
                    <asp:HyperLink ID="DeleteTable" runat="server"
                        NavigateUrl='<%# String.Format("deletetable.aspx?database={0}&table={1}", Server.UrlEncode(Request["database"]), DataBinder.Eval(Container.DataItem, "encodedname")) %>'>
                         <span class="glyphicon glyphicon-remove-sign text-danger"></span>
                    </asp:HyperLink>
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>
    </asp:GridView>
    <asp:Label ID="TableTypeErrorLabel" runat="server" EnableViewState="False">There are no tables to display.</asp:Label>
            </div>
        </div>
    
</asp:content>


