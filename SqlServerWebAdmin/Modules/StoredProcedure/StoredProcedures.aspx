<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="StoredProcedures.aspx.cs" Inherits="SqlServerWebAdmin.StoredProcedures" MasterPageFile="~/Site.master" %>

<asp:Content ID="MainContent1" ContentPlaceHolderID="MainContent" runat="server">
    <h3>Stored procedures</h3>

    <div class="row">
        <div class="col-md-10">
            <asp:DropDownList runat="server" ID="TypeDropDownList" CssClass="form-control">
                <asp:ListItem Value="Show User Stored Procedures Only">User Items Only</asp:ListItem>
                <asp:ListItem Value="Show User and System Stored Procedures">User and System Items</asp:ListItem>
            </asp:DropDownList>
        </div>
        <div class="col-md-2">
            <asp:Button ID="FilterButton" runat="server" Text="Filter" CssClass="btn btn-default" OnClick="FilterButton_Click"></asp:Button>
        </div>
    </div>

    <div class="row" style="margin-top: 10px">
        <div class="col-md-12">
            <a class="btn btn-success pull-right" href="<%= ResolveUrl("~/Modules/StoredProcedure/createstoredprocedure.aspx?database=" + Server.UrlEncode(Request["database"])) %>">Create new stored procedure</a>
        </div>
    </div>

    <div class="row" style="margin-top: 10px">
        <div class="col-md-12">
            <asp:GridView ID="SProcsDataGrid" runat="server" GridLines="None" Border="0" AutoGenerateColumns="False" CssClass="table table-bordered">
                <Columns>
                    <asp:TemplateField HeaderText="Name">
                        <ItemTemplate>
                            <asp:HyperLink ID="Hyperlink1" runat="server" Text='<%# "&amp;nbsp;" + DataBinder.Eval(Container.DataItem, "name") %>'
                                NavigateUrl='<%# string.Format("~/Modules/StoredProcedure/editstoredprocedure.aspx?database={0}&sproc={1}", Server.UrlEncode(Request["database"]), DataBinder.Eval(Container.DataItem, "encodedname")) %>'></asp:HyperLink>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:BoundField DataField="owner" HeaderText="Owner" DataFormatString="{0}"></asp:BoundField>
                    <asp:TemplateField HeaderText="Type">
                        <ItemTemplate>
                            <%# DataBinder.Eval(Container.DataItem, "type") %>
                        &nbsp;
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:BoundField DataField="createdate" HeaderText="Create Date" DataFormatString="{0}"></asp:BoundField>
                    <asp:TemplateField HeaderText="" HeaderStyle-Width="10px">
                        <ItemTemplate>
                            <asp:HyperLink ID="DeleteSProc" runat="server" Text="delete" CssClass="databaseListAction"
                                NavigateUrl='<%# String.Format("deletestoredprocedure.aspx?database={0}&sproc={1}", Server.UrlEncode(Request["database"]), DataBinder.Eval(Container.DataItem, "encodedname")) %>'>
                             <span class="glyphicon glyphicon-remove-sign text-danger"></span>
                            </asp:HyperLink>
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
            </asp:GridView>
            <asp:Label ID="SProcTypeErrorLabel" runat="server" EnableViewState="False" Font-Bold="true"
                Font-Size="10">There are no stored procedures to display.</asp:Label>
        </div>
    </div>
</asp:Content>



