<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Views.aspx.cs" Inherits="SqlServerWebAdmin.Views" MasterPageFile="~/Site.master" %>

<asp:Content ID="MainContent1" ContentPlaceHolderID="MainContent" runat="server">
     
    <h3>Views</h3>

    <asp:DropDownList runat="server" ID="SProcTypeDropDownList">
                                                            <asp:ListItem Value="Show User Stored Procedures Only">User Stored Procedures Only</asp:ListItem>
                                                            <asp:ListItem Value="Show User and System Stored Procedures">User and System Stored Procedures</asp:ListItem>
                                                        </asp:DropDownList>
                                                        <asp:Button ID="FilterViewsButton" runat="server" Text="Filter" CssClass="btn btn-default"
                                                            OnClick="FilterViewsButton_Click"></asp:Button>

    <asp:HyperLink runat="server" NavigateUrl="/createview.aspx?database=">Create new view</asp:HyperLink>


                                                <asp:DataGrid ID="SProcsDataGrid" runat="server" GridLines="None" Border="0" AutoGenerateColumns="False"
                                                Width="100%" CellPadding="4" CellSpacing="1">
                                                <HeaderStyle CssClass="tableHeader"></HeaderStyle>
                                                <ItemStyle CssClass="tableItems"></ItemStyle>
                                                <Columns>
                                                    <asp:TemplateColumn HeaderText="Name">
                                                        <HeaderStyle Wrap="False"></HeaderStyle>
                                                        <ItemStyle Wrap="False"></ItemStyle>
                                                        <ItemTemplate>
                                                            <a href='<%# String.Format("editstoredprocedure.aspx?database={0}&sproc={1}", Server.UrlEncode(Request["database"]), DataBinder.Eval(Container.DataItem, "encodedname")) %>'>
                                                                <img src="images/sproc_ico.gif" border="0" align="absmiddle"></a> &nbsp;
                                                            <asp:HyperLink ID="Hyperlink1" runat="server" Text='<%# "&amp;nbsp;" + DataBinder.Eval(Container.DataItem, "name") %>'
                                                                CssClass="databaseListBlack" NavigateUrl='<%# String.Format("editstoredprocedure.aspx?database={0}&sproc={1}", Server.UrlEncode(Request["database"]), DataBinder.Eval(Container.DataItem, "encodedname")) %>'></asp:HyperLink>
                                                        </ItemTemplate>
                                                    </asp:TemplateColumn>
                                                    <asp:BoundColumn DataField="owner" HeaderText="Owner" DataFormatString="{0}">
                                                        <HeaderStyle Wrap="False"></HeaderStyle>
                                                        <ItemStyle Wrap="False"></ItemStyle>
                                                    </asp:BoundColumn>
                                                    <asp:TemplateColumn HeaderText="Type">
                                                        <HeaderStyle Wrap="False"></HeaderStyle>
                                                        <ItemStyle Wrap="False"></ItemStyle>
                                                        <ItemTemplate>
                                                            <%# DataBinder.Eval(Container.DataItem, "type") %>
                                                            &nbsp;
                                                        </ItemTemplate>
                                                    </asp:TemplateColumn>
                                                    <asp:BoundColumn DataField="createdate" HeaderText="Create Date" DataFormatString="{0}">
                                                        <HeaderStyle Wrap="False"></HeaderStyle>
                                                        <ItemStyle Wrap="False"></ItemStyle>
                                                    </asp:BoundColumn>
                                                    <asp:TemplateColumn HeaderText="Edit">
                                                        <HeaderStyle Wrap="False" HorizontalAlign="Right"></HeaderStyle>
                                                        <ItemStyle Wrap="False" HorizontalAlign="Right"></ItemStyle>
                                                        <ItemTemplate>
                                                            <asp:HyperLink ID="EditSProc" runat="server" Text="edit" CssClass="databaseListAction"
                                                                NavigateUrl='<%# String.Format("editstoredprocedure.aspx?database={0}&sproc={1}", Server.UrlEncode(Request["database"]), DataBinder.Eval(Container.DataItem, "encodedname")) %>'></asp:HyperLink>
                                                        </ItemTemplate>
                                                    </asp:TemplateColumn>
                                                    <asp:TemplateColumn HeaderText="Delete">
                                                        <HeaderStyle Wrap="False" HorizontalAlign="Right"></HeaderStyle>
                                                        <ItemStyle Wrap="False" HorizontalAlign="Right"></ItemStyle>
                                                        <ItemTemplate>
                                                            <asp:HyperLink ID="DeleteSProc" runat="server" Text="delete" CssClass="databaseListAction"
                                                                NavigateUrl='<%# String.Format("deletestoredprocedure.aspx?database={0}&sproc={1}", Server.UrlEncode(Request["database"]), DataBinder.Eval(Container.DataItem, "encodedname")) %>'></asp:HyperLink>
                                                        </ItemTemplate>
                                                    </asp:TemplateColumn>
                                                </Columns>
                                            </asp:DataGrid>
                                            <asp:Label ID="SProcTypeErrorLabel" runat="server" EnableViewState="False" Font-Bold="true"
                                                Font-Size="10">There are no stored procedures to display.</asp:Label>

</asp:Content>
