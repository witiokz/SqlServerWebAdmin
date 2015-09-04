<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CreateTable.aspx.cs" Inherits="SqlServerWebAdmin.CreateTable" MasterPageFile="~/Site.master" %>

<asp:content id="MainContent1" contentplaceholderid="MainContent" runat="server">
    <h3>Create table</h3>
     <div class="form-horizontal">
        <div class="form-group">
            <label class="col-md-3 control-label">Table name</label>
            <div class="col-md-9">
                 <asp:TextBox CssClass="form-control" runat="server" ID="TableNameTextBox" Columns="30"></asp:TextBox>
                <asp:RequiredFieldValidator runat="server" ID="TableNameRequiredValidator" ControlToValidate="TableNameTextBox"
                                                Display="Dynamic" ErrorMessage="The table name cannot be empty."></asp:RequiredFieldValidator>                             
            </div>
         </div>
         <div class="form-group">
            <div class="col-md-offset-3 col-md-9">
                <asp:Button runat="server" ID="CreateNewTableButton" CssClass="btn btn-default" Text="Create" OnClick="CreateNewTableButton_Click"></asp:Button>
                 
                <a class="btn btn-default" href="/Modules/Table/tables.aspx?database=<%= Server.UrlEncode(Request["database"]) %>">Cancel</a>
                <asp:Label ID="ErrorCreatingLabel" runat="server" Visible="False" ForeColor="red"></asp:Label>
            </div>
        </div>
    </div>
</asp:content>

