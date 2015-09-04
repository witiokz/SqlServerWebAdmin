<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CreateDatabase.aspx.cs" Inherits="SqlServerWebAdmin.CreateDatabase" MasterPageFile="~/Site.master" %>

<asp:content id="MainContent1" contentplaceholderid="MainContent" runat="server">
    <h3>Create database</h3>
    <div class="form-horizontal">
        <div class="form-group">
            <label class="col-md-3 control-label">Database name</label>
            <div class="col-md-9">
                <asp:TextBox ID="DatabaseNameTextBox" runat="server" Columns="30" CssClass="form-control"></asp:TextBox>
                <asp:RequiredFieldValidator runat="server" ID="DatabaseNameRequiredValidator" ControlToValidate="DatabaseNameTextBox"
                                Display="Dynamic" ErrorMessage="The database name cannot be empty."></asp:RequiredFieldValidator>
                            <asp:Label ID="ErrorCreatingLabel" runat="server" Visible="False" ForeColor="red"></asp:Label>
            </div>
         </div>
         <div class="form-group">
            <div class="col-md-offset-3 col-md-9">
                <asp:Button ID="CreateNewDatabaseButton" runat="server" CssClass="btn btn-default" Text="Create" OnClick="CreateNewDatabaseButton_Click"></asp:Button>
                <asp:HyperLink NavigateUrl="~/Databases.aspx" Text="Cancel" CssClass="btn btn-default" runat="server" />
            </div>
        </div>
    </div>
</asp:content>
