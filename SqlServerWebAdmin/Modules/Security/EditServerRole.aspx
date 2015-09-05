<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="EditServerRole.aspx.cs" Inherits="SqlServerWebAdmin.EditServerRole" MasterPageFile="~/Site.master" %>

<%@ Register TagPrefix="SqlAdmin" TagName="LoginRolePicker" Src="LoginRolePicker.ascx" %>

<asp:content id="MainContent1" contentplaceholderid="MainContent" runat="server">
    <h3>Edit role <%= Request["Role"] %></h3>
     <div class="form-horizontal">
        <div class="form-group">
            <label class="col-md-3 control-label">Select which logins are assigned to this role</label>
            <div class="col-md-9">
                 <SqlAdmin:LoginRolePicker ID="RoleLogins" OnItemChanged="RoleLogins_Changed" runat="server" />                          
            </div>
         </div>
         <div class="form-group">
            <div class="col-md-offset-3 col-md-9">                
                <a class="btn btn-default" href="<%= ResolveUrl("~/Modules/Security/ServerRoles.aspx") %>">Done</a>
                <br />
                <asp:Label ID="ErrorMessage" ForeColor="Red" runat="server" />
            </div>
        </div>
    </div>
</asp:content>

