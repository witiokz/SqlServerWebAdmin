<%@ Page Language="c#" Inherits="SqlWebAdmin.Login" CodeBehind="Default.aspx.cs" MasterPageFile="~/Site.master" %>

<asp:Content ID="MainContent1" ContentPlaceHolderID="MainContent" runat="server">
    <h3>Login</h3>

    <h5>
        <asp:Label ID="LogoutInfoLabel" runat="server" Visible="False">You are now logged out.</asp:Label>
        <asp:Label ID="LoginInfoLabel" runat="server" Visible="False" Font-Size="10 pt" Font-Bold="true">Welcome to the Web Data Administrator.</asp:Label>
    </h5>

    <div class="form-horizontal">
        <div class="form-group">
            <label class="col-md-3 control-label">Server name</label>
            <div class="col-md-9">
                <asp:DropDownList CssClass="form-control" runat="server" ID="SqlServerDLL"></asp:DropDownList>
            </div>
        </div>
        <div class="form-group">
            <label class="col-md-3 control-label">Authentication</label>
            <div class="col-md-9">
                <asp:DropDownList ID="AuthRadioButtonList" CssClass="form-control" runat="server" AutoPostBack="True"
                    OnSelectedIndexChanged="AuthRadioButtonList_SelectedIndexChanged">
                    <asp:ListItem Value="windows" Selected="True">Windows Authentication</asp:ListItem>
                    <asp:ListItem Value="sql">SQL Server Authentication</asp:ListItem>
                </asp:DropDownList>
            </div>
        </div>
        <div class="form-group">
            <label class="col-md-3 control-label">User name</label>
            <div class="col-md-9">
                <asp:TextBox ID="UsernameTextBox" runat="server" Columns="35" CssClass="form-control"></asp:TextBox>
                <asp:RequiredFieldValidator ID="UsernameRequiredFieldValidator" runat="server" ErrorMessage=" Must specify a user name"
                    ControlToValidate="UsernameTextBox" Display="Dynamic"></asp:RequiredFieldValidator>
            </div>
        </div>
        <div class="form-group">
            <label class="col-md-3 control-label">Password</label>
            <div class="col-md-9">
                <asp:TextBox ID="PasswordTextBox" runat="server" Columns="35" TextMode="Password" CssClass="form-control"></asp:TextBox>
            </div>
        </div>
        <div class="form-group">
            <div class="col-md-offset-3 col-md-9">
                <asp:Button ID="LoginButton" runat="server" CssClass="btn btn-default" Text="Connect" OnClick="LoginButton_Click"></asp:Button>
                <br />
                <asp:Label ID="ErrorLabel" runat="server" Visible="False"></asp:Label>
            </div>
        </div>
    </div>

</asp:Content>


