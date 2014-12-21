<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="EditColumn.aspx.cs" Inherits="SqlServerWebAdmin.EditColumn" %>

<%@ Register TagPrefix="Toolbar" TagName="HelpLogout" Src="Toolbars/HelpLogoutToolbar.ascx" %>
<%@ Register TagPrefix="Toolbar" TagName="Database" Src="Toolbars/DatabaseToolbar.ascx" %>
<%@ Register TagPrefix="Toolbar" TagName="Server" Src="Toolbars/ServerToolbar.ascx" %>
<%@ Register TagPrefix="Location" TagName="Server" Src="Toolbars/ServerLocation.ascx" %>
<%@ Register TagPrefix="Location" TagName="Database" Src="Toolbars/DatabaseLocation.ascx" %>
<%@ Register TagPrefix="Location" TagName="Table" Src="Toolbars/TableLocation.ascx" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<html>
<head>
    <title>Web Data Administrator - Edit Column</title>
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
            <tr>
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
                                            EDIT COLUMN
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
                                            <!-- Column Editor START -->
                                            <asp:Label ID="DataLossWarningLabel" runat="server" Visible="False" ForeColor="red">
                                                Warning:
                                                There is a potential for column data loss when updating an existing column that has been created or modified outside of the Web Data Adminstrator tool.
                                                Properties such as foreign keys and indexes are not preserved when editing an existing column.
                                                <br>
                                                <br>
                                            </asp:Label>
                                            <table cellspacing="2" cellpadding="0" border="0">
                                                <tr>
                                                    <td class="databaseListItem">
                                                        Primary Key</td>
                                                    <td class="databaseListItem" width="25">
                                                        &nbsp;</td>
                                                    <td class="databaseListItem">
                                                        <asp:CheckBox ID="PrimaryKeyCheckbox" runat="server"></asp:CheckBox></td>
                                                    <td class="databaseListItem">
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td class="databaseListItem">
                                                        Column Name</td>
                                                    <td class="databaseListItem" width="25">
                                                        &nbsp;</td>
                                                    <td class="databaseListItem">
                                                        <asp:TextBox ID="ColumnNameTextbox" runat="server" Columns="15"></asp:TextBox>
                                                    </td>
                                                    <td class="databaseListItem">
                                                        <asp:RequiredFieldValidator ID="ColumnNameRequiredFieldValidator" runat="server"
                                                            ErrorMessage=" A column name must be specified." ControlToValidate="ColumnNameTextBox"
                                                            Display="Dynamic"></asp:RequiredFieldValidator>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td class="databaseListItem">
                                                        Data Type</td>
                                                    <td class="databaseListItem" width="25">
                                                        &nbsp;</td>
                                                    <td class="databaseListItem">
                                                        <asp:DropDownList ID="DataTypeDropdownlist" runat="server">
                                                            <asp:ListItem Value="bigint">bigint</asp:ListItem>
                                                            <asp:ListItem Value="binary">binary</asp:ListItem>
                                                            <asp:ListItem Value="bit">bit</asp:ListItem>
                                                            <asp:ListItem Value="char" Selected="True">char</asp:ListItem>
                                                            <asp:ListItem Value="datetime">datetime</asp:ListItem>
                                                            <asp:ListItem Value="decimal">decimal</asp:ListItem>
                                                            <asp:ListItem Value="float">float</asp:ListItem>
                                                            <asp:ListItem Value="image">image</asp:ListItem>
                                                            <asp:ListItem Value="int">int</asp:ListItem>
                                                            <asp:ListItem Value="money">money</asp:ListItem>
                                                            <asp:ListItem Value="nchar">nchar</asp:ListItem>
                                                            <asp:ListItem Value="ntext">ntext</asp:ListItem>
                                                            <asp:ListItem Value="numeric">numeric</asp:ListItem>
                                                            <asp:ListItem Value="nvarchar">nvarchar</asp:ListItem>
                                                            <asp:ListItem Value="real">real</asp:ListItem>
                                                            <asp:ListItem Value="smalldatetime">smalldatetime</asp:ListItem>
                                                            <asp:ListItem Value="smallint">smallint</asp:ListItem>
                                                            <asp:ListItem Value="smallmoney">smallmoney</asp:ListItem>
                                                            <asp:ListItem Value="sql_varient">sql_varient</asp:ListItem>
                                                            <asp:ListItem Value="text">text</asp:ListItem>
                                                            <asp:ListItem Value="timestamp">timestamp</asp:ListItem>
                                                            <asp:ListItem Value="tinyint">tinyint</asp:ListItem>
                                                            <asp:ListItem Value="uniqueidentifier">uniqueidentifier</asp:ListItem>
                                                            <asp:ListItem Value="varbinary">varbinary</asp:ListItem>
                                                            <asp:ListItem Value="varchar">varchar</asp:ListItem>
                                                        </asp:DropDownList>
                                                    </td>
                                                    <td class="databaseListItem">
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td class="databaseListItem">
                                                        Length</td>
                                                    <td class="databaseListItem" width="25">
                                                        &nbsp;</td>
                                                    <td class="databaseListItem">
                                                        <asp:TextBox ID="LengthTextbox" runat="server" Text="10" Columns="15">10</asp:TextBox>
                                                    </td>
                                                    <td class="databaseListItem">
                                                        <asp:RequiredFieldValidator ID="LengthRequiredFieldValidator" runat="server" ErrorMessage=" Must specify a length (or specify 0 for non-length datatypes)."
                                                            ControlToValidate="LengthTextBox" Display="Dynamic"></asp:RequiredFieldValidator>
                                                        <asp:RangeValidator ID="LengthRangeValidator" runat="server" ErrorMessage=" Length must be between 0 and 8000"
                                                            ControlToValidate="LengthTextBox" Display="Dynamic" MaximumValue="8000" MinimumValue="0"
                                                            Type="Integer"></asp:RangeValidator>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td class="databaseListItem">
                                                        Allow Null</td>
                                                    <td class="databaseListItem" width="25">
                                                        &nbsp;</td>
                                                    <td class="databaseListItem">
                                                        <asp:CheckBox ID="AllowNullCheckbox" runat="server"></asp:CheckBox>
                                                    </td>
                                                    <td class="databaseListItem">
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td colspan="3" height="2">
                                                        <hr>
                                                    </td>
                                                    <td class="databaseListItem">
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td class="databaseListItem">
                                                        Default Value</td>
                                                    <td class="databaseListItem" width="25">
                                                        &nbsp;</td>
                                                    <td class="databaseListItem">
                                                        <asp:TextBox ID="DefaultValueTextbox" runat="server" Columns="15"></asp:TextBox>
                                                    </td>
                                                    <td class="databaseListItem">
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td class="databaseListItem">
                                                        Precision</td>
                                                    <td class="databaseListItem" width="25">
                                                        &nbsp;</td>
                                                    <td class="databaseListItem">
                                                        <asp:TextBox ID="PrecisionTextbox" runat="server" Columns="15"></asp:TextBox>
                                                    </td>
                                                    <td class="databaseListItem">
                                                        <asp:RangeValidator ID="PrecisionRangeValidator" runat="server" ErrorMessage=" Precision must be an integer"
                                                            ControlToValidate="PrecisionTextBox" Display="Dynamic" MaximumValue="32000" MinimumValue="0"
                                                            Type="Integer"></asp:RangeValidator>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td class="databaseListItem">
                                                        Scale</td>
                                                    <td class="databaseListItem" width="25">
                                                        &nbsp;</td>
                                                    <td class="databaseListItem">
                                                        <asp:TextBox ID="ScaleTextbox" runat="server" Columns="15"></asp:TextBox>
                                                    </td>
                                                    <td class="databaseListItem">
                                                        <asp:RangeValidator ID="ScaleRangeValidator" runat="server" ErrorMessage=" Scale must be an integer"
                                                            ControlToValidate="ScaleTextBox" Display="Dynamic" MaximumValue="32000" MinimumValue="0"
                                                            Type="Integer"></asp:RangeValidator>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td class="databaseListItem">
                                                        Identity</td>
                                                    <td class="databaseListItem" width="25">
                                                        &nbsp;</td>
                                                    <td class="databaseListItem">
                                                        <asp:CheckBox ID="IdentityCheckBox" runat="server"></asp:CheckBox>
                                                    </td>
                                                    <td class="databaseListItem">
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td class="databaseListItem">
                                                        Identity Seed</td>
                                                    <td class="databaseListItem" width="25">
                                                        &nbsp;</td>
                                                    <td class="databaseListItem">
                                                        <asp:TextBox ID="IdentitySeedTextbox" runat="server" Columns="15"></asp:TextBox>
                                                    </td>
                                                    <td class="databaseListItem">
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td class="databaseListItem">
                                                        Identity Increment</td>
                                                    <td class="databaseListItem" width="25">
                                                        &nbsp;</td>
                                                    <td class="databaseListItem">
                                                        <asp:TextBox ID="IdentityIncrementTextbox" runat="server" Columns="15"></asp:TextBox>
                                                    </td>
                                                    <td class="databaseListItem">
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td class="databaseListItem">
                                                        Is RowGuid</td>
                                                    <td class="databaseListItem" width="25">
                                                        &nbsp;</td>
                                                    <td class="databaseListItem">
                                                        <asp:CheckBox ID="IsRowGuidCheckBox" runat="server"></asp:CheckBox>
                                                    </td>
                                                    <td class="databaseListItem">
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td colspan="3" height="2">
                                                    </td>
                                                    <td class="databaseListItem">
                                                    </td>
                                                </tr>
                                            </table>
                                            <br>
                                            <asp:Button ID="UpdateButton" runat="server" CssClass="button" onMouseOver="this.style.color='#808080';"
                                                onMouseOut="this.style.color='#000000';" Text="Update" OnClick="UpdateButton_Click">
                                            </asp:Button>
                                            &nbsp;
                                            <asp:Button ID="CancelButton" runat="server" CssClass="button" onMouseOver="this.style.color='#808080';"
                                                onMouseOut="this.style.color='#000000';" Text="Cancel" CausesValidation="false"
                                                OnClick="CancelButton_Click"></asp:Button>
                                            <br>
                                            <br>
                                            <asp:Label ID="ErrorUpdatingColumnLabel" runat="server" Visible="False" ForeColor="red"></asp:Label>
                                            <!-- Column Editor END -->
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

