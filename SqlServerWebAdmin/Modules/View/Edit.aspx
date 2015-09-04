<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Edit.aspx.cs" Inherits="SqlServerWebAdmin.Modules.View.Edit"  MasterPageFile="~/Site.master" %>

<asp:content id="MainContent1" contentplaceholderid="MainContent" runat="server">
    <h3>Edit stored procedure</h3>
     <div class="form-horizontal">
        <div class="form-group">
            <label class="col-md-3 control-label">Name</label>
            <div class="col-md-9">
                <asp:Label CssClass="form-control" ID="NameLabel" runat="server"></asp:Label>                            
            </div>
         </div>
         <div class="form-group">
            <label class="col-md-3 control-label">Owner</label>
            <div class="col-md-9">
                <asp:Label CssClass="control-label" ID="OwnerLabel" runat="server"></asp:Label>                             
            </div>
         </div>
         <div class="form-group">
            <label class="col-md-3 control-label">Create date</label>
            <div class="col-md-9">
                <asp:Label CssClass="control-label"  ID="CreateDateLabel" runat="server"></asp:Label>                             
            </div>
         </div>
         <div class="form-group">
            <label class="col-md-3 control-label">Text</label>
            <div class="col-md-9">
               <asp:TextBox  CssClass="form-control" runat="server" TextMode="MultiLine" Columns="60" Rows="15" ID="TextTextbox"></asp:TextBox>                            
            </div>
         </div>
         <div class="form-group">
            <div class="col-md-offset-3 col-md-9">
                <asp:Button runat="server" ID="SaveButton" CssClass="btn btn-default" Text="Save" OnClick="SaveButton_Click"></asp:Button>
                                 
                <a class="btn btn-default" href="<%= ResolveUrl("~/Modules/View/Views.aspx?database=" + Server.UrlEncode(Request["database"])) %>">Cancel</a>
                <asp:RequiredFieldValidator runat="server" ID="NameRequiredValidator" ControlToValidate="TextTextbox"
                                                Display="Dynamic" ErrorMessage="The name cannot be empty."></asp:RequiredFieldValidator>
                 <br />
                 <asp:Label ID="ErrorLabel" runat="server" Visible="False"></asp:Label>
            </div>
        </div>
    </div>
</asp:content>

