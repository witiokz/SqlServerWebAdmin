<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="RestoreDatabase.aspx.cs" Inherits="SqlServerWebAdmin.RestoreDatabase"  MasterPageFile="~/Site.master" %>

<%@ Reference Page="~/Databases.aspx" %>


<asp:Content ID="MainContent1" ContentPlaceHolderID="MainContent" runat="server">
    <h3>Restore Database</h3>
        <label>Ssource to restore</label>
        <asp:GridView ID="SourceList" runat="server" OnSelectedIndexChanging="SourceList_SelectedIndexChanging">
            <Columns>
                <asp:CommandField HeaderText="Select" ShowHeader="True" ShowSelectButton="True" />
            </Columns>
        </asp:GridView>
       <label>Database to restore: </label>
        <asp:DropDownList runat="server" ID="DatabaseList">
        </asp:DropDownList>

        <div class="form-group">
            <label class="col-md-3 control-label">Restore type</label> 
            <div class="col-md-9">
                <asp:DropDownList runat="server" ID="RestoreItem" CssClass="form-control" >
                    <asp:ListItem Text="Restore" Value="Restore" />
                    <asp:ListItem Text="Verify Only" Value="Verify" />
                </asp:DropDownList>
            </div>
        </div>
        <asp:Button runat="server" ID="RestoreButton" CssClass="button" onMouseOver="this.style.color='#808080';"
            onMouseOut="this.style.color='#000000';" Text="Restore" OnClick="RestoreButton_Click">
        </asp:Button>

        This operation can take a few minutes, do not click on the button more than once.

        <asp:Label ID="Status" runat="server"></asp:Label>

</asp:Content>
