<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="RestoreDatabase.aspx.cs" Inherits="SqlServerWebAdmin.RestoreDatabase"  MasterPageFile="~/Site.master" %>

<%@ Reference Page="~/Databases.aspx" %>


<asp:Content ID="MainContent1" ContentPlaceHolderID="MainContent" runat="server">
    <h3>Restore Database</h3>
    <h5>This operation can take a few minutes, do not click on the button more than once. </h5>
    <div class="form-horizontal">
            <label>Source to restore</label>
    <asp:GridView ID="SourceList" runat="server" CssClass="table table-bordered" OnSelectedIndexChanging="SourceList_SelectedIndexChanging">
        <Columns>
            <asp:CommandField HeaderText="Select" ShowHeader="True" ShowSelectButton="True" />
        </Columns>
    </asp:GridView>
    <div class="form-group">
        <label class="col-md-3 control-label">Database to restore</label> 
        <div class="col-md-9">
            <asp:DropDownList runat="server" ID="DatabaseList" CssClass="form-control">
            </asp:DropDownList>
        </div>
    </div>

    <div class="form-group">
        <label class="col-md-3 control-label">Restore type</label> 
        <div class="col-md-9">
            <asp:DropDownList runat="server" ID="RestoreItem" CssClass="form-control" >
                <asp:ListItem Text="Restore" Value="Restore" />
                <asp:ListItem Text="Verify Only" Value="Verify" />
            </asp:DropDownList>
        </div>
    </div>

    <div class="form-group">
        <div class="col-md-offset-3 col-md-9">
            <asp:Button runat="server" ID="RestoreButton" CssClass="btn btn-default" Text="Restore" OnClick="RestoreButton_Click"></asp:Button>
        </div>
    </div>

    <asp:Label ID="Status" runat="server"></asp:Label>
    </div>
</asp:Content>
