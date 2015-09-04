<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CreateLogin.aspx.cs" Inherits="SqlServerWebAdmin.CreateLogin" MasterPageFile="~/Site.master" %>

<asp:content id="MainContent1" contentplaceholderid="MainContent" runat="server">
    <h3>Create login</h3>
    <div class="form-horizontal">
        <div class="form-group">
            <label class="col-md-3 control-label">Authentication Method</label>
            <div class="col-md-9">
                <asp:DropDownList ID="AuthType" AutoPostBack="True" OnSelectedIndexChanged="AuthType_Changed"
                                                            runat="server">
                                                            <asp:ListItem Value="NTUser">Windows Integrated</asp:ListItem>
                                                            <asp:ListItem Value="Standard">Sql Login</asp:ListItem>
                                                        </asp:DropDownList>
            </div>
         </div>
         <div class="form-group">
            <label class="col-md-3 control-label">Login Name</label>
            <div class="col-md-9">
                <asp:TextBox ID="LoginName" Columns="35" MaxLength="128" runat="server" />
                <asp:RequiredFieldValidator ControlToValidate="LoginName" ErrorMessage="* Required"
                                                            Display="Dynamic" runat="server" ID="RequiredFieldValidator1" />
            </div>
         </div>
         <div class="form-group">
            <label class="col-md-3 control-label">Password</label>
            <div class="col-md-9">
                <asp:TextBox ID="Password" Columns="35" Enabled="False" MaxLength="128" TextMode="Password" runat="server" />
            </div>
         </div>
         <div class="form-group">
            <div class="col-md-offset-3 col-md-9">
                <asp:Button runat="server" CssClass="btn btn-default" Text="Create Login" OnClick="AddLogin_Click"></asp:Button>
                 <asp:Label ID="ErrorMessage" ForeColor="Red" runat="server" />
            </div>
        </div>
    </div>
</asp:content>


