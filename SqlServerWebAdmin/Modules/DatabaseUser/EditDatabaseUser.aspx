<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="EditDatabaseUser.aspx.cs" Inherits="SqlServerWebAdmin.EditDatabaseUser" MasterPageFile="~/Site.master" %>

<asp:Content ID="MainContent1" ContentPlaceHolderID="MainContent" runat="server">
    <h3>Edit user <%= Request["User"] %></h3>
    <div class="form-horizontal">
        <div class="form-group">
            <label class="col-md-3 control-label">Login name</label>
            <div class="col-md-9">
                <asp:Label CssClass="control-label" ID="LoginLabel" runat="server" />
            </div>
        </div>
        <div class="form-group">
            <label class="col-md-3 control-label">Roles</label>
            <div class="col-md-9">
<asp:CheckBoxList ID="Roles" CssClass="form-control" DataValueField="Name" runat="server" />
            </div>
        </div>
         <div class="form-group">
             <div class="col-md-offset-3 col-md-9">
                 <asp:Button runat="server" CssClass="btn btn-default" Text="Save" OnClick="Save_Click"></asp:Button>
                 <a class="btn btn-default" href="<%= ResolveUrl("~/Modules/DatabaseUser/DatabaseUsers.aspx?database=" + Server.UrlEncode(Request["Database"])) %>">Cancel</a>
                 <br />
                  <asp:Label ID="ErrorMessage" runat="server" />
             </div>
         </div>
    </div>
</asp:Content>



