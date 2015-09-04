<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Columns.aspx.cs" Inherits="SqlServerWebAdmin.Columns" MasterPageFile="~/Site.master" %>

<asp:Content ID="MainContent1" ContentPlaceHolderID="MainContent" runat="server">
    <h3>Columns</h3>

    <div class="row" style="margin-top: 10px">
        <div class="col-md-12">
            <a class="btn btn-success pull-right" href="/Modules/Column/editcolumn.aspx?database=<%= Server.UrlEncode(Request["database"]) %>&table=<%= Server.UrlEncode(Request["table"]) %>">Create new column</a>
        </div>
    </div>

    <div class="row" style="margin-top: 10px">
        <div class="col-md-12">
            <asp:GridView ID="ColumnsDataGrid" runat="server" Border="0" AutoGenerateColumns="False" CssClass="table table-bordered"
                GridLines="None" CellPadding="4" CellSpacing="1">
                <Columns>
                    <asp:TemplateField HeaderText="Key">
                        <HeaderStyle Wrap="False"></HeaderStyle>
                        <ItemStyle Wrap="False" HorizontalAlign="Center"></ItemStyle>
                        <ItemTemplate>
                            <asp:Label Visible='<%# ((bool)DataBinder.Eval(Container.DataItem, "key")) %>' runat="server"
                                 ID="Image1"></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="ID">
                        <ItemTemplate>
                            <asp:Label Visible='<%# ((bool)DataBinder.Eval(Container.DataItem, "id")) %>' runat="server"
                                 ID="Image2"></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Name">
                        <ItemTemplate>
                            <asp:HyperLink ID="Hyperlink1" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "name") %>'
                                CssClass="databaseListBlack" NavigateUrl='<%# String.Format("editcolumn.aspx?database={0}&table={1}&column={2}", Server.UrlEncode(Request["database"]), Server.UrlEncode(Request["table"]), DataBinder.Eval(Container.DataItem, "encodedname")) %>'></asp:HyperLink>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:BoundField DataField="datatype" HeaderText="Data Type" DataFormatString="{0}">
                    </asp:BoundField>
                    <asp:TemplateField HeaderText="Size">
                        <ItemTemplate>
                            <%# DataBinder.Eval(Container.DataItem, "size") %>
                            <%# ((((int)DataBinder.Eval(Container.DataItem, "precision")) != 0) &&
                                    (((int)DataBinder.Eval(Container.DataItem, "scale")) != 0)) ?
                                    String.Format("({0}, {1})", (int)DataBinder.Eval(Container.DataItem, "precision"), (int)DataBinder.Eval(Container.DataItem, "scale")) : "" %>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Nulls">
                        <HeaderStyle Wrap="False"></HeaderStyle>
                        <ItemStyle Wrap="False" HorizontalAlign="Center"></ItemStyle>
                        <ItemTemplate>
                            <span class="glyphicon <%# ((bool)DataBinder.Eval(Container.DataItem, "nulls")) ? "glyphicon-collapse-down" : "glyphicon-unchecked" %>"></span>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:BoundField DataField="default" HeaderText="Default" DataFormatString="{0}">
                    </asp:BoundField>
                    <asp:TemplateField HeaderText="" HeaderStyle-Width="10px">
                        <ItemTemplate>
                            <asp:HyperLink ID="DeleteColumn" runat="server" CssClass="databaseListAction"
                                NavigateUrl='<%# String.Format("deletecolumn.aspx?database={0}&table={1}&column={2}", Server.UrlEncode(Request["database"]), Server.UrlEncode(Request["table"]), DataBinder.Eval(Container.DataItem, "encodedname")) %>'>
                                 <span class="glyphicon glyphicon-remove-sign text-danger"></span>
                            </asp:HyperLink>
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
            </asp:GridView>
            <asp:Label ID="NoColumnsLabel" runat="server" EnableViewState="False" Font-Bold="true"
                Font-Size="10">There are no columns to display.</asp:Label>
        </div>
    </div>
</asp:Content>
