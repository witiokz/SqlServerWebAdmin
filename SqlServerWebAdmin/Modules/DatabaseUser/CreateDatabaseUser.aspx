<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CreateDatabaseUser.aspx.cs" Inherits="SqlServerWebAdmin.CreateDatabaseUser" MasterPageFile="~/Site.master" %>


<asp:Content ID="MainContent1" ContentPlaceHolderID="MainContent" runat="server">
    <h3>Create user</h3>
    <div class="form-horizontal">
        <div class="form-group">
            <label class="col-md-3 control-label">Login name</label>
            <div class="col-md-9">
                <asp:DropDownList ID="Logins" CssClass="form-control" DataValueField="Name" OnChange="ChangeUser(this)" runat="server" />
            </div>
        </div>
        <div class="form-group">
            <label class="col-md-3 control-label">User name</label>
            <div class="col-md-9">
                <asp:TextBox CssClass="form-control" ID="Username" MaxLength="128" runat="server" />
                <asp:RequiredFieldValidator ControlToValidate="Username" ErrorMessage="* Required"
                    Display="Dynamic" runat="server" />
            </div>
        </div>
         <div class="form-group">
             <div class="col-md-offset-3 col-md-9">
                 <asp:Button runat="server" ID="CreateButton" CssClass="btn btn-default" Text="Create" OnClick="Create_Click"></asp:Button>
                 <a class="btn btn-default" href="/Modules/DatabaseUser/DatabaseUsers.aspx?database=<%= Server.UrlEncode(Request["database"]) %>">Cancel</a>
                 <asp:Label ID="ErrorMessage" ForeColor="red" runat="server" />
             </div>
         </div>
    </div>

    <script>
        function ChangeUser(obj) {
            document.all['Username'].value = obj.options[obj.selectedIndex].value;
            document.all['Username'].select();
            document.all['Username'].focus();
        }
    </script>
</asp:Content>


