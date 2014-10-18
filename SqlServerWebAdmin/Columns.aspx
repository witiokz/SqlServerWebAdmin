﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Columns.aspx.cs" Inherits="SqlServerWebAdmin.Columns" %>

<%@ Register TagPrefix="Toolbar" TagName="HelpLogout" Src="Toolbars/HelpLogoutToolbar.ascx" %>
<%@ Register TagPrefix="Toolbar" TagName="Database" Src="Toolbars/DatabaseToolbar.ascx" %>
<%@ Register TagPrefix="Toolbar" TagName="Server" Src="Toolbars/ServerToolbar.ascx" %>
<%@ Register TagPrefix="Location" TagName="Server" Src="Toolbars/ServerLocation.ascx" %>
<%@ Register TagPrefix="Location" TagName="Database" Src="Toolbars/DatabaseLocation.ascx" %>
<%@ Register TagPrefix="Location" TagName="Table" Src="Toolbars/TableLocation.ascx" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<html>
<head>
    <title>Web Data Administrator - Table Columns</title>
    <link rel="shortcut icon" href="favicon.ico">
    <link rel="stylesheet" type="text/css" href="admin.css">
</head>
<body bottommargin="0" leftmargin="0" topmargin="0" rightmargin="0">
    <form id="Tables" method="post" runat="server">
        <table style="width: 100%; height: 62" cellspacing="0" cellpadding="0" border="0">
            <!-- FIRST ROW: HEADER -->
            <tr>
                <td colspan="3" valign="bottom" align="left" width="100%" height="36" background="images/bg_horizontal_top_right.gif"
                    background-repeat="repeat-x" bgcolor="#c0c0c0">
                    <table cellspacing="0" cellpadding="0" width="100%" border="0">
                        <tr>
                            <!--BEGIN ONE LINE-->
                            <td valign="bottom" width="308">
                                <img src="images/logo_top.gif" width="308" height="36" alt="" border="0"></td>
                            <!--END ONE LINE-->
                            <td valign="bottom" align="right" width="100%">
                                <Toolbar:HelpLogout runat="server" ID="HelpLogout" HelpTopic="login"></Toolbar:HelpLogout>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <!-- FIRST ROW: HEADER -->
            <!-- SECOND ROW: CRUMBS -->
            <tr>
                <!--BEGIN ONE LINE-->
                <td align="left" bgcolor="#b4c6f3" background="images/blue_back.gif" width="172"
                    height="26">
                    <img src="images/logo_bottom.gif" width="238" height="26" alt="" border="0"></td>
                <!--END ONE LINE-->
                <td align="left" bgcolor="#b4c6f3" background="images/blue_back.gif" width="100%"
                    height="26">
                    <table width="100%" height="26" cellspacing="0" cellpadding="0" border="0" style="table-layout: fixed">
                        <tr>
                            <td width="12">
                                &nbsp;
                            </td>
                            <td valign="center" align="left" width="100%" height="26">
                                <Location:Server runat="Server" ID="ServerLocation"></Location:Server>
                                <Location:Database runat="Server" ID="DatabaseLocation"></Location:Database>
                                <Location:Table runat="Server" ID="TableLocation"></Location:Table>
                            </td>
                        </tr>
                    </table>
                </td>
                <!--BEGIN ONE LINE-->
                <td align="left" bgcolor="#b4c6f3" width="12" height="26">
                    <img src="images/blue_back_right.gif" width="12" height="26" alt="" border="0"></td>
                <!--END ONE LINE-->
            </tr>
        </table>
        <!-- SECOND ROW: CRUMBS -->
        <!-- THIRD ROW: BOTTOM SECTION -->
        <table style="width: 100%; height: 100%; padding: 0px; border: 0px" cellspacing="0">
            <!-- START NAVIGATION SECTION -->
            <td bgcolor="#6699ff" valign="top" align="middle" width="172" height="100%">
                <Toolbar:Server runat="server" ID="ServerToolbar"></Toolbar:Server>
                <Toolbar:Database runat="server" ID="DatabaseToolbar"></Toolbar:Database>
            </td>
            <!-- END NAVIGATION SECTION -->
            <!-- START CONTENT SECTION -->
            <td valign="top" align="left">
                <table cellspacing="0" cellpadding="0" border="0" width="100%">
                    <tr>
                        <!--BEGIN ONE LINE-->
                        <td valign="bottom" colspan="2" height="8" width="100%">
                            <img src="images/spacer.gif" width="1" height="8" alt="" border="0"></td>
                        <!--END ONE LINE-->
                    </tr>
                    <tr>
                        <!--BEGIN ONE LINE-->
                        <td align="left" width="12">
                            <img src="images/spacer.gif" width="12" height="1" alt="" border="0"></td>
                        <!--END ONE LINE-->
                        <td align="left" class="databaseListItem" width="100%">
                            <!-- PAGE CONTENT: START -->
                            <!-- SECTION HEADER: START -->
                            <table cellspacing="0" cellpadding="0" width="100%" border="0">
                                <tr>
                                    <td class="databaseListHeader">
                                        TABLE COLUMNS
                                    </td>
                                </tr>
                                <!-- SECTION HEADER: END -->
                                <!-- SECTION: START -->
                                <tr>
                                    <!--BEGIN ONE LINE-->
                                    <td height="3" valign="middle" background="images/blue_dotted_line.gif">
                                        <img src="images/blue_dotted_line.gif" width="150" height="3" alt="" border="0"></td>
                                    <!--END ONE LINE-->
                                </tr>
                                <tr>
                                    <!--BEGIN ONE LINE-->
                                    <td height="4" valign="middle">
                                        <img src="images/spacer.gif" width="1" height="4" alt="" border="0"></td>
                                    <!--END ONE LINE-->
                                </tr>
                                <tr>
                                    <td bgcolor="white" class="databaseListItem">
                                        <table width="100%" cellspacing="0" cellpadding="0" border="0">
                                            <tr>
                                                <td align="right">
                                                    <asp:HyperLink runat="server" CssClass="createLink" ID="AddNewColumnHyperLink"><img src="images/new.gif" width="16" height="16" alt="" border="0">
                                                            <span style="position:relative; top: -3px;">Create new column</span></asp:HyperLink>
                                                </td>
                                            </tr>
                                        </table>
                                        <br>
                                        <asp:GridView ID="ColumnsDataGrid" runat="server" Border="0" AutoGenerateColumns="False"
                                            GridLines="None" Width="100%" CellPadding="4" CellSpacing="1">
                                           <%-- <HeaderStyle CssClass="tableHeader"></HeaderStyle>
                                            <ItemStyle CssClass="tableItems"></ItemStyle>
                                            <Columns>
                                                <asp:TemplateColumn HeaderText="Key">
                                                    <HeaderStyle Wrap="False"></HeaderStyle>
                                                    <ItemStyle Wrap="False" HorizontalAlign="Center"></ItemStyle>
                                                    <ItemTemplate>
                                                        <asp:Image Visible='<%# ((bool)DataBinder.Eval(Container.DataItem, "key")) %>' runat="server"
                                                            ImageUrl="images/key.gif" ID="Image1"></asp:Image>
                                                    </ItemTemplate>
                                                </asp:TemplateColumn>
                                                <asp:TemplateColumn HeaderText="ID">
                                                    <HeaderStyle Wrap="False"></HeaderStyle>
                                                    <ItemStyle Wrap="False" HorizontalAlign="Center"></ItemStyle>
                                                    <ItemTemplate>
                                                        <asp:Image Visible='<%# ((bool)DataBinder.Eval(Container.DataItem, "id")) %>' runat="server"
                                                            ImageUrl="images/checkmark.gif" ID="Image2"></asp:Image>
                                                    </ItemTemplate>
                                                </asp:TemplateColumn>
                                                <asp:BoundColumn DataField="name" HeaderText="Name" DataFormatString="{0}">
                                                    <HeaderStyle Wrap="False"></HeaderStyle>
                                                    <ItemStyle Wrap="False"></ItemStyle>
                                                </asp:BoundColumn>
                                                <asp:TemplateColumn HeaderText="Name">
                                                    <HeaderStyle Wrap="False"></HeaderStyle>
                                                    <ItemStyle Wrap="False"></ItemStyle>
                                                    <ItemTemplate>
                                                        <asp:HyperLink ID="Hyperlink1" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "name") %>'
                                                            CssClass="databaseListBlack" NavigateUrl='<%# String.Format("editcolumn.aspx?database={0}&table={1}&column={2}", Server.UrlEncode(Request["database"]), Server.UrlEncode(Request["table"]), DataBinder.Eval(Container.DataItem, "encodedname")) %>'></asp:HyperLink>
                                                    </ItemTemplate>
                                                </asp:TemplateColumn>
                                                <asp:BoundColumn DataField="datatype" HeaderText="Data Type" DataFormatString="{0}">
                                                    <HeaderStyle Wrap="False"></HeaderStyle>
                                                    <ItemStyle Wrap="False"></ItemStyle>
                                                </asp:BoundColumn>
                                                <asp:TemplateColumn HeaderText="Size">
                                                    <HeaderStyle Wrap="False"></HeaderStyle>
                                                    <ItemStyle Wrap="False"></ItemStyle>
                                                    <ItemTemplate>
                                                        <%# DataBinder.Eval(Container.DataItem, "size") %>
                                                        <%# ((((int)DataBinder.Eval(Container.DataItem, "precision")) != 0) &&
                                                                    (((int)DataBinder.Eval(Container.DataItem, "scale")) != 0)) ?
                                                                    String.Format("({0}, {1})", (int)DataBinder.Eval(Container.DataItem, "precision"), (int)DataBinder.Eval(Container.DataItem, "scale")) : "" %>
                                                    </ItemTemplate>
                                                </asp:TemplateColumn>
                                                <asp:TemplateColumn HeaderText="Nulls">
                                                    <HeaderStyle Wrap="False"></HeaderStyle>
                                                    <ItemStyle Wrap="False" HorizontalAlign="Center"></ItemStyle>
                                                    <ItemTemplate>
                                                        <asp:Image runat="server" ImageUrl='<%# String.Format("images/{0}.gif", ((bool)DataBinder.Eval(Container.DataItem, "nulls")) ? "checkmark" : "checknomark") %>'
                                                            ID="Image3"></asp:Image>
                                                    </ItemTemplate>
                                                </asp:TemplateColumn>
                                                <asp:BoundColumn DataField="default" HeaderText="Default" DataFormatString="{0}">
                                                    <HeaderStyle Wrap="False"></HeaderStyle>
                                                    <ItemStyle Wrap="False"></ItemStyle>
                                                </asp:BoundColumn>
                                                <asp:TemplateColumn HeaderText="Edit">
                                                    <HeaderStyle Wrap="False" HorizontalAlign="Right"></HeaderStyle>
                                                    <ItemStyle Wrap="False" HorizontalAlign="Right"></ItemStyle>
                                                    <ItemTemplate>
                                                        <asp:HyperLink ID="EditColumn" runat="server" Text="edit" CssClass="databaseListAction"
                                                            NavigateUrl='<%# String.Format("editcolumn.aspx?database={0}&table={1}&column={2}", Server.UrlEncode(Request["database"]), Server.UrlEncode(Request["table"]), DataBinder.Eval(Container.DataItem, "encodedname")) %>'></asp:HyperLink>
                                                    </ItemTemplate>
                                                </asp:TemplateColumn>
                                                <asp:TemplateColumn HeaderText="Delete">
                                                    <HeaderStyle Wrap="False" HorizontalAlign="Right"></HeaderStyle>
                                                    <ItemStyle Wrap="False" HorizontalAlign="Right"></ItemStyle>
                                                    <ItemTemplate>
                                                        <asp:HyperLink ID="DeleteColumn" runat="server" Text="delete" CssClass="databaseListAction"
                                                            NavigateUrl='<%# String.Format("deletecolumn.aspx?database={0}&table={1}&column={2}", Server.UrlEncode(Request["database"]), Server.UrlEncode(Request["table"]), DataBinder.Eval(Container.DataItem, "encodedname")) %>'></asp:HyperLink>
                                                    </ItemTemplate>
                                                </asp:TemplateColumn>
                                            </Columns>--%>
                                        </asp:GridView>
                                        <asp:Label ID="NoColumnsLabel" runat="server" EnableViewState="False" Font-Bold="true"
                                            Font-Size="10">There are no columns to display.</asp:Label>
                                    </td>
                                </tr>
                                <!-- Section END -->
                                <!-- Section Footer START -->
                            </table>
                            <br>
                            <!-- Section Footer END -->
                            <!-- Page content END -->
                        </td>
                    </tr>
                </table>
            </td>
            </tr>
            <!-- THIRD ROW: BOTTOM SECTION -->
        </table>
    </form>
</body>
</html>

