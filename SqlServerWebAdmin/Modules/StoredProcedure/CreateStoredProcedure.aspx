<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CreateStoredProcedure.aspx.cs" Inherits="SqlServerWebAdmin.CreateStoredProcedure" MasterPageFile="~/Site.master" %>

<asp:content id="MainContent1" contentplaceholderid="MainContent" runat="server">
    <h3>Create stored procedure</h3>
     <div class="form-horizontal">
        <div class="form-group">
            <label class="col-md-3 control-label">Name</label>
            <div class="col-md-9">
                <asp:TextBox CssClass="form-control" ID="NameTextBox" runat="server" Columns="30"></asp:TextBox>&nbsp;
                <asp:RequiredFieldValidator runat="server" ID="TableNameRequiredValidator" ControlToValidate="NameTextBox"
                                                Display="Dynamic" ErrorMessage="The table name cannot be empty."></asp:RequiredFieldValidator>                             
            </div>
         </div>
         <div class="form-group">
            <div class="col-md-offset-3 col-md-9">
                <asp:Button runat="server" ID="CreateNewButton" CssClass="btn btn-default" Text="Create" OnClick="CreateNewButton_Click"></asp:Button>                 
                <a class="btn btn-default" href="/Modules/StoredProcedure/StoredProcedures.aspx?database=<%= Server.UrlEncode(Request["database"]) %>">Cancel</a>
                 <asp:Label ID="ErrorCreatingLabel" runat="server" Visible="False" ForeColor="red"></asp:Label>
            </div>
        </div>
    </div>
</asp:content>



