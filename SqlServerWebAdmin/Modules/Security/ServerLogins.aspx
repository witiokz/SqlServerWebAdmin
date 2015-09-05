<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ServerLogins.aspx.cs" Inherits="SqlServerWebAdmin.ServerLogins" MasterPageFile="~/Site.master" %>

<asp:Content ID="MainContent1" ContentPlaceHolderID="MainContent" runat="server">
    <h3>Server logins</h3>

    <div class="row">
        <div class="col-md-12">
            <asp:HyperLink runat="server" CssClass="btn btn-success pull-right" ID="AddNewDatabaseHyperLink" NavigateUrl="~/Modules/Security/CreateLogin.aspx">
                   Create new Login
            </asp:HyperLink>
        </div>
    </div>

    <div class="row" style="margin-top: 10px">
        <div class="col-md-12">
            <asp:GridView ID="LoginDataGrid" runat="server" CssClass="table table-bordered" AutoGenerateColumns="False">
                <Columns>
                    <asp:TemplateField HeaderText="Name">
                        <ItemTemplate>
                             <a href="<%= ResolveUrl("/Modules/Security/EditServerLogin.aspx?Login=")%><%# DataBinder.Eval(Container.DataItem, "name") %>"> 
                                  <%# DataBinder.Eval(Container.DataItem, "name") %>
                             </a>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:BoundField DataField="LoginType" HeaderText="Type" DataFormatString="{0}">
                        <HeaderStyle Wrap="False" HorizontalAlign="Right"></HeaderStyle>
                        <ItemStyle Wrap="False" HorizontalAlign="Right"></ItemStyle>
                    </asp:BoundField>
                    <asp:BoundField DataField="NTLoginAccessType" HeaderText="Server Access" DataFormatString="{0}">
                        <HeaderStyle Wrap="False" HorizontalAlign="Right"></HeaderStyle>
                        <ItemStyle Wrap="False" HorizontalAlign="Right"></ItemStyle>
                    </asp:BoundField>
                    <asp:BoundField DataField="Database" HeaderText="Default Database" DataFormatString="{0}">
                        <HeaderStyle Wrap="False" HorizontalAlign="Right"></HeaderStyle>
                        <ItemStyle Wrap="False" HorizontalAlign="Right"></ItemStyle>
                    </asp:BoundField>
                    <asp:BoundField DataField="LanguageAlias" HeaderText="Language" DataFormatString="{0}">
                        <HeaderStyle Wrap="False" HorizontalAlign="Right"></HeaderStyle>
                        <ItemStyle Wrap="False" HorizontalAlign="Right"></ItemStyle>
                    </asp:BoundField>
                </Columns>
            </asp:GridView>
        </div>
</asp:Content>

