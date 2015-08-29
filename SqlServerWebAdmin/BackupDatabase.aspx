<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="BackupDatabase.aspx.cs" Inherits="SqlServerWebAdmin.BackupDatabase" MasterPageFile="~/Site.master" %>

<%@ Reference Page="~/Databases.aspx" %>

<asp:Content ID="MainContent1" ContentPlaceHolderID="MainContent" runat="server">
     
    <h3>Backup Database</h3>
    <h5>This operation can take a few minutes, do not click on the button more than once. </h5>
    <asp:Label ID="Status" runat="server" CssClass="label label-success"></asp:Label>

    <div class="form-horizontal">
         <div class="form-group">
            <label class="col-md-3 control-label"> Database to backup</label>
            <div class="col-md-9">
                <asp:DropDownList runat="server" ID="ExportDatabaseList" CssClass="form-control" ></asp:DropDownList>
            </div>
         </div>
        <div class="form-group">
            <label class="col-md-3 control-label">Backup type</label> 
            <div class="col-md-9">
                <asp:DropDownList runat="server" ID="BackupItem" CssClass="form-control" >
                    <asp:ListItem Text="Database" Value="Database" />
                    <asp:ListItem Text="Log" Value="Log" />
                </asp:DropDownList>
            </div>
        </div>
        <div class="form-group">
            <div class="col-md-offset-3 col-md-9">
                <asp:Button runat="server" ID="BackupButton" CssClass="btn btn-default" Text="Backup" OnClick="BackupButton_Click"></asp:Button>
            </div>
        </div>
     </div>
</asp:Content>



