<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="HelpHeader.ascx.cs" Inherits="SqlServerWebAdmin.Help.HelpHeader" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<html>
<head>
    <title>Web Data Administrator -
        <asp:Label ID="TitleLabel" runat="server"></asp:Label></title>
    <link rel="shortcut icon" href="../favicon.ico">
    <link rel="stylesheet" type="text/css" href="admin.css">
</head>
<body bottommargin="0" leftmargin="0" topmargin="0" rightmargin="0">
    <table style="width: 100%; height: 62" cellspacing="0" cellpadding="0" border="0">
        <!-- FIRST ROW: HEADER -->
        <tr>
            <td colspan="3" valign="bottom" align="left" width="100%" height="36" background="../images/bg_horizontal_top_right.gif"
                background-repeat="repeat-x" bgcolor="#c0c0c0">
                <table cellspacing="0" cellpadding="0" width="100%" border="0">
                    <tr>
                        <!--BEGIN ONE LINE-->
                        <td valign="bottom" width="308">
                            <img src="../images/logo_top.gif" width="308" height="36" alt="" border="0"></td>
                        <!--END ONE LINE-->
                        <td valign="bottom" align="right" width="100%">
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
                <img src="../images/logo_bottom.gif" width="238" height="26" alt="" border="0"></td>
            <!--END ONE LINE-->
            <td align="left" bgcolor="#b4c6f3" background="../images/blue_back.gif" width="100%"
                height="26">
                <table width="100%" height="26" cellspacing="0" cellpadding="0" border="0" style="table-layout: fixed">
                    <tr>
                        <td width="12">
                            &nbsp;
                        </td>
                        <td valign="center" align="left" width="100%" height="26">
                        </td>
                    </tr>
                </table>
            </td>
            <!--BEGIN ONE LINE-->
            <td align="left" bgcolor="#b4c6f3" width="12" height="26">
                <img src="../images/blue_back_right.gif" width="12" height="26" alt="" border="0"></td>
            <!--END ONE LINE-->
        </tr>
    </table>
    <!-- SECOND ROW: CRUMBS -->
    <!-- THIRD ROW: BOTTOM SECTION -->
    <table style="width: 100%; height: 100%; padding: 0px; border: 0px" cellspacing="0">
        <tr>
            <!-- START NAVIGATION SECTION -->
            <td bgcolor="#6699ff" valign="top" align="middle" width="172" height="100%">
                <!--#include file="TOC.inc" -->
            </td>
            <!-- END NAVIGATION SECTION -->
            <!-- START CONTENT SECTION -->
            <td valign="top" align="left">
                <table cellspacing="0" cellpadding="0" border="0" width="100%">
                    <tr>
                        <!--BEGIN ONE LINE-->
                        <td valign="bottom" colspan="2" height="8" width="100%">
                            <img src="../images/spacer.gif" width="1" height="8" alt="" border="0"></td>
                        <!--END ONE LINE-->
                    </tr>
                    <tr>
                        <!--BEGIN ONE LINE-->
                        <td align="left" width="12">
                            <img src="../images/spacer.gif" width="12" height="1" alt="" border="0"></td>
                        <!--END ONE LINE-->
                        <td align="left" class="databaseListItem" width="100%">
                            <!-- PAGE CONTENT: START -->
                            <!-- SECTION HEADER: START -->
                            <table cellspacing="0" cellpadding="0" width="100%" border="0">
                                <tr>
                                    <td class="databaseListHeader">
                                        <asp:Label ID="TopicLabel" runat="server"></asp:Label>
                                    </td>
                                </tr>
                                <!-- SECTION HEADER: END -->
                                <!-- SECTION: START -->
                                <tr>
                                    <!--BEGIN ONE LINE-->
                                    <td height="3" valign="center" background="../images/blue_dotted_line.gif">
                                        <img src="../images/blue_dotted_line.gif" width="150" height="3" alt="" border="0"></td>
                                    <!--END ONE LINE-->
                                </tr>
                                <tr>
                                    <!--BEGIN ONE LINE-->
                                    <td height="4" valign="center">
                                        <img src="../images/spacer.gif" width="1" height="4" alt="" border="0"></td>
                                    <!--END ONE LINE-->
                                </tr>
                                <tr>
                                    <td bgcolor="white" class="databaseListItem">
