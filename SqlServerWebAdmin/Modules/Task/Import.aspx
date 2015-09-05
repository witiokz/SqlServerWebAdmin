<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Import.aspx.cs" Inherits="SqlServerWebAdmin.Import"  MasterPageFile="~/Site.master" %>

<asp:Content ID="MainContent1" ContentPlaceHolderID="MainContent" runat="server">
     
    <h3>Import database</h3>
    <h5>This operation can take a few minutes, do not click on the button more than once. </h5>
    <h5>Note: If you are importing a database created or modified outside of the Web Data
                                            Administrator, it may not correctly preserve all database objects (triggers, views,
                                            and relationships for example).</h5>
    <asp:Label ID="Status" runat="server" CssClass="label label-success"></asp:Label>

    <div class="form-horizontal">
         <div class="form-group">
            <label class="col-md-3 control-label">Enter the path to a .sql file containing a previously exported database</label>
            <div class="col-md-9">
                 <input id="FileUploadInput" type="file" runat="server" />
            </div>
         </div>
        <div class="form-group">
            <div class="col-md-offset-3 col-md-9">
                <asp:Button runat="server" ID="ImportButton" CssClass="btn btn-default" Text="Import" OnClick="ImportButton_Click"></asp:Button>
                <br />
                <asp:Label ID="ImportLabel" runat="server"></asp:Label>
            </div>
        </div>
     </div>
</asp:Content>
