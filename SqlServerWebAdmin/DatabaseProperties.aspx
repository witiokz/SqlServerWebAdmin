<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="DatabaseProperties.aspx.cs" Inherits="SqlServerWebAdmin.DatabaseProperties" %>

<%@ Register TagPrefix="Toolbar" TagName="HelpLogout" Src="Toolbars/HelpLogoutToolbar.ascx" %>
<%@ Register TagPrefix="Location" TagName="Database" Src="Toolbars/DatabaseLocation.ascx" %>
<%@ Register TagPrefix="Location" TagName="Server" Src="Toolbars/ServerLocation.ascx" %>
<%@ Register TagPrefix="Toolbar" TagName="Server" Src="Toolbars/ServerToolbar.ascx" %>
<%@ Register TagPrefix="Toolbar" TagName="Database" Src="Toolbars/DatabaseToolbar.ascx" %>
<%@ Register TagPrefix="FileProperties" TagName="FileProperties" Src="FileProperties.ascx" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<html>
<head>
    <title>Web Data Administrator - Database Properties</title>
    <link rel="shortcut icon" href="favicon.ico">
    <link rel="stylesheet" type="text/css" href="admin.css">
</head>
<body bottommargin="0" leftmargin="0" topmargin="0" rightmargin="0">
    <form method="post" runat="server">
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
            <tr>
                <!-- START NAVIGATION SECTION -->
                <td bgcolor="#6699ff" valign="top" align="middle" width="172" height="100%">
                    <Toolbar:Server runat="server" ID="ServerToolbar"></Toolbar:Server>
                    <Toolbar:Database Selected="Properties" runat="server" ID="DatabaseToolbar"></Toolbar:Database>
                </td>
                <!-- END NAVIGATION SECTION -->
                <!-- START CONTENT SECTION -->
                <td valign="top" align="left">
                    <table cellspacing="0" cellpadding="0" border="0" width="100%">
                        <tbody>
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
                                                DATABASE PROPERTIES
                                            </td>
                                        </tr>
                                        <!-- SECTION HEADER: END -->
                                        <!-- SECTION: START -->
                                        <tr>
                                            <!--BEGIN ONE LINE-->
                                            <td height="3" valign="center" background="images/blue_dotted_line.gif">
                                                <img src="images/blue_dotted_line.gif" width="150" height="3" alt="" border="0"></td>
                                            <!--END ONE LINE-->
                                        </tr>
                                        <tr>
                                            <!--BEGIN ONE LINE-->
                                            <td height="4" valign="center">
                                                <img src="images/spacer.gif" width="1" height="4" alt="" border="0"></td>
                                            <!--END ONE LINE-->
                                        </tr>
                                        <tr>
                                            <td bgcolor="white" class="databaseListItem">
                                                <asp:Label ID="ErrorLabel" runat="server" Visible="False" ForeColor="red"></asp:Label>
                                                <table cellspacing="1" cellpadding="4" width="100%" border="0">
                                                    <tr class="tableHeader">
                                                        <td width="100">
                                                            Property</td>
                                                        <td>
                                                            Value</td>
                                                    </tr>
                                                    <tr class="tableItems">
                                                        <td>
                                                            Name</td>
                                                        <td>
                                                            <asp:Label ID="NamePropertyLabel" runat="server"></asp:Label></td>
                                                    </tr>
                                                    <tr class="tableItems">
                                                        <td>
                                                            Status</td>
                                                        <td>
                                                            <asp:Label ID="StatusPropertyLabel" runat="server"></asp:Label></td>
                                                    </tr>
                                                    <tr class="tableItems">
                                                        <td>
                                                            Owner</td>
                                                        <td>
                                                            <asp:Label ID="OwnerPropertyLabel" runat="server"></asp:Label></td>
                                                    </tr>
                                                    <tr class="tableItems">
                                                        <td>
                                                            Date created</td>
                                                        <td>
                                                            <asp:Label ID="DateCreatedPropertyLabel" runat="server"></asp:Label></td>
                                                    </tr>
                                                    <tr class="tableItems">
                                                        <td>
                                                            Size</td>
                                                        <td>
                                                            <asp:Label ID="SizePropertyLabel" runat="server"></asp:Label>MB</td>
                                                    </tr>
                                                    <tr class="tableItems">
                                                        <td>
                                                            Space available</td>
                                                        <td>
                                                            <asp:Label ID="SpaceAvailablePropertyLabel" runat="server"></asp:Label>MB</td>
                                                    </tr>
                                                    <tr class="tableItems">
                                                        <td>
                                                            Number of users</td>
                                                        <td>
                                                            <asp:Label ID="NumberOfUsersPropertyLabel" runat="server"></asp:Label></td>
                                                    </tr>
                                                </table>
                                            </td>
                                        </tr>
                                        <!-- Section END -->
                                        <!-- Section Footer START -->
                                    </table>
                                    <br>
                                    <!-- Section Footer END -->
                                    <!-- Section Header START -->
                                    <table cellspacing="0" cellpadding="0" width="100%" border="0">
                                        <!-- Section Header END -->
                                        <!-- Section START -->
                                        <tr>
                                            <td height="2">
                                                <b>Data Files</b><br>
                                                <hr>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="databaseListItem" bgcolor="white">
                                                <!-- Data File Properties -->
                                                <FileProperties:FileProperties ID="DataFileProperties" runat="server"></FileProperties:FileProperties>
                                            </td>
                                        </tr>
                                        <!-- Section END -->
                                        <!-- Section Footer START -->
                                    </table>
                                    <br>
                                    <!-- Section Footer END -->
                                    <!-- Section Header START -->
                                    <table cellspacing="0" cellpadding="0" width="100%" border="0">
                                        <!-- Section Header END -->
                                        <!-- Section START -->
                                        <tr>
                                            <td height="2">
                                                <b>Transaction Log</b><br>
                                                <hr>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="databaseListItem" bgcolor="white">
                                                <!-- Log File Properties -->
                                                <FileProperties:FileProperties ID="LogFileProperties" runat="server"></FileProperties:FileProperties>
                                            </td>
                                        </tr>
                                        <!-- Section END -->
                                        <!-- Section Footer START -->
                                    </table>
                                    <br>
                                    <hr>
                                    <asp:Button ID="ApplyButton" CssClass="button" onMouseOver="this.style.color='#808080';"
                                        onMouseOut="this.style.color='#000000';" runat="server" Text="Apply" OnClick="ApplyButton_Click">
                                    </asp:Button>&nbsp;
                                    <asp:Button ID="CancelButton" CssClass="button" onMouseOver="this.style.color='#808080';"
                                        onMouseOut="this.style.color='#000000';" runat="server" Text="Cancel" OnClick="CancelButton_Click">
                                    </asp:Button>&nbsp;
                                </td>
                </td>
            </tr>
            <!-- Section END -->
            <!-- Section Footer START -->
        </table>
        <br>
        <!-- Section Footer END -->
        <!-- Page content END -->
        </TD></TR></TBODY></TABLE></TD></TR>
        <!-- THIRD ROW: BOTTOM SECTION -->
        </TABLE>
    </form>
</body>
</html>

