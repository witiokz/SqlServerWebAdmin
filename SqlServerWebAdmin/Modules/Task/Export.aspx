<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Export.aspx.cs" Inherits="SqlServerWebAdmin.Export" MasterPageFile="~/Site.master" %>

<%@ Reference Page="~/Databases.aspx" %>

<asp:Content ID="MainContent1" ContentPlaceHolderID="MainContent" runat="server">
     
    <h3>Export database</h3>
    <h5>This operation can take a few minutes, do not click on the button more than once. </h5>
    <h5>Note: If you are exporting a database created or modified outside of the Web Data
                                            Administrator, it may not correctly preserve all database objects (triggers, views,
                                            and relationships for example).</h5>
    <asp:Label ID="Status" runat="server" CssClass="label label-success"></asp:Label>

    <div class="form-horizontal">
         <div class="form-group">
            <label class="col-md-3 control-label">Select the database to export</label>
            <div class="col-md-9">
                <asp:DropDownList runat="server" ID="ExportDatabaseList" CssClass="form-control"></asp:DropDownList>
            </div>
         </div>
        <div class="form-group">
            <label class="col-md-3 control-label">Objects to export</label> 
            <div class="col-md-9">
                <asp:CheckBox runat="server" ID="ScriptDatabaseCheckBox" Text="Database" Checked="True"></asp:CheckBox> <br />
                <asp:CheckBox runat="server" ID="ScriptTableSchemeCheckBox" Text="Table schemas" Checked="True"></asp:CheckBox><br />
                <asp:CheckBox runat="server" ID="ScriptTableDataCheckBox" Text="Table data" Checked="True"></asp:CheckBox><br />
                <asp:CheckBox runat="server" ID="ScriptStoredProceduresCheckBox" Text="Stored procedures" Checked="True"></asp:CheckBox>
            </div>
        </div>
        <div class="form-group">
            <label class="col-md-3 control-label">Options</label>
            <div class="col-md-9">
                <asp:CheckBox runat="server" ID="ScriptDropCheckBox" Text="DROP commands" Checked="True"></asp:CheckBox><br />
                <asp:CheckBox runat="server" ID="ScriptCommentsCheckBox" Text="Include descriptive comments" Checked="True"></asp:CheckBox><br>
            </div>
         </div>
        <div class="form-group">
            <div class="col-md-offset-3 col-md-9">
                <asp:Button runat="server" ID="ExportButton" CssClass="btn btn-default" Text="Export" OnClick="ExportButton_Click"></asp:Button>
            </div>
        </div>
     </div>
</asp:Content>



