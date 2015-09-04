<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="QueryDatabase.aspx.cs" Inherits="SqlServerWebAdmin.QueryDatabase" MasterPageFile="~/Site.master" %>

<asp:content id="MainContent1" contentplaceholderid="MainContent" runat="server">
    <h3>Query database</h3>

    <asp:TextBox runat="server" TextMode="MultiLine" Columns="60" Rows="15" ID="QueryTextbox" CssClass="form-control"></asp:TextBox>
    <asp:CheckBox runat="server" ID="WrapCheckBox" Checked="True" Text="Wrap cell contents in results"></asp:CheckBox>

    <div class="row">
        <div class="col-md-6">
            <asp:Button runat="server" Text="Execute" CssClass="btn btn-default" ID="ExecuteButton" OnClick="ExecuteButton_Click"></asp:Button>
            <asp:Button runat="server" Text="Save" CssClass="btn btn-default" ID="SaveButton" OnClick="SaveButton_Click"></asp:Button>
        </div>
        <div class="col-md-6">
            <input id="FileUploadInput" type="file" runat="server" style="display: inline-block" />
            <asp:Button runat="server" Text="Load query..." CssClass="btn btn-default" ID="LoadButton" OnClick="LoadButton_Click"></asp:Button>
        </div>
        <div class="col-md-12">
            <asp:Panel runat="server" ID="ResultsPanel"></asp:Panel>
            <asp:Label ID="ErrorLabel" runat="server" Visible="False" ForeColor="red"></asp:Label>
        </div>
    </div>
</asp:content>


