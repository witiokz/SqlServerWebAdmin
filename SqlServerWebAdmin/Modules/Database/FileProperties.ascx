<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="FileProperties.ascx.cs" Inherits="SqlServerWebAdmin.FileProperties" %>

<asp:CheckBox Runat="server" Text="Automatically grow file" ID="AutomaticallyGrowFileCheckBox"></asp:CheckBox>
<br>
<br>
File growth
<br>

<asp:DropDownList Runat="server" ID="GrowthTypeDropDownList">
    <asp:ListItem Text="In megabytes"></asp:ListItem>
    <asp:ListItem Text="By percent"></asp:ListItem>
</asp:DropDownList>
<asp:TextBox Runat="server" Columns="4" ID="GrowthTextBox"></asp:TextBox>
<br>
<br>
 Maximum file size<br>
<asp:RadioButton Runat="server" GroupName="MaximumFileSizeType" Text="Unrestricted file growth" ID="UnrestrictedGrowthRadioButton"></asp:RadioButton><br>
<asp:RadioButton Runat="server" GroupName="MaximumFileSizeType" Text="Restrict file growth" ID="RestrictGrowthRadioButton"></asp:RadioButton>
<asp:TextBox Runat="server" Columns="4" ID="MaximumFileSizeTextBox"></asp:TextBox>
MB
<br>
