<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="EditColumn.aspx.cs" Inherits="SqlServerWebAdmin.EditColumn" MasterPageFile="~/Site.master" %>

<asp:content id="MainContent1" contentplaceholderid="MainContent" runat="server">
    <h3>Edit column</h3>
    <asp:Label ID="DataLossWarningLabel" runat="server" Visible="False" ForeColor="red">
        Warning:
        There is a potential for column data loss when updating an existing column that has been created or modified outside of the Web Data Adminstrator tool.
        Properties such as foreign keys and indexes are not preserved when editing an existing column.
        <br>
        <br>
    </asp:Label>

    <div class="form-horizontal">
        <div class="form-group">
            <label class="col-md-3 control-label">Primary Key</label>
            <div class="col-md-9">
                 <asp:CheckBox ID="PrimaryKeyCheckbox" runat="server" CssClass="form-control"></asp:CheckBox>                            
            </div>
         </div>
         <div class="form-group">
            <label class="col-md-3 control-label"> Column Name</label>
            <div class="col-md-9">
                 <asp:HiddenField ID="OriginalName" runat="server"></asp:HiddenField>
                 <asp:TextBox ID="ColumnNameTextbox" runat="server" Columns="15" CssClass="form-control"></asp:TextBox>  
                 <asp:RequiredFieldValidator ID="ColumnNameRequiredFieldValidator" runat="server"
                                                            ErrorMessage=" A column name must be specified." ControlToValidate="ColumnNameTextBox"
                                                            Display="Dynamic"></asp:RequiredFieldValidator>                         
            </div>
         </div>
         <div class="form-group">
            <label class="col-md-3 control-label">Data Type</label>
            <div class="col-md-9">
                 <asp:DropDownList ID="DataTypeDropdownlist" runat="server" CssClass="form-control"></asp:DropDownList>                           
            </div>
         </div>
         <div class="form-group">
            <label class="col-md-3 control-label">Length</label>
            <div class="col-md-9">
                 <asp:TextBox ID="LengthTextbox" runat="server" Text="10" Columns="15" CssClass="form-control">10</asp:TextBox>
                 <asp:RequiredFieldValidator ID="LengthRequiredFieldValidator" runat="server" ErrorMessage=" Must specify a length (or specify 0 for non-length datatypes)."
                                                            ControlToValidate="LengthTextBox" Display="Dynamic"></asp:RequiredFieldValidator>
                                                        <asp:RangeValidator ID="LengthRangeValidator" runat="server" ErrorMessage=" Length must be between 0 and 8000"
                                                            ControlToValidate="LengthTextBox" Display="Dynamic" MaximumValue="8000" MinimumValue="0"
                                                            Type="Integer"></asp:RangeValidator>                          
            </div>
         </div>
        <div class="form-group">
            <label class="col-md-3 control-label">Allow Null</label>
            <div class="col-md-9">
                 <asp:CheckBox ID="AllowNullCheckbox" runat="server" CssClass="form-control"></asp:CheckBox>                         
            </div>
         </div>
         <hr />
         <div class="form-group">
            <label class="col-md-3 control-label">Default Value</label>
            <div class="col-md-9">
                 <asp:TextBox ID="DefaultValueTextbox" runat="server" Columns="15" CssClass="form-control"></asp:TextBox>                          
            </div>
         </div>
         <div class="form-group">
            <label class="col-md-3 control-label">Precision</label>
            <div class="col-md-9">
                 <asp:TextBox ID="PrecisionTextbox" runat="server" Columns="15" CssClass="form-control"></asp:TextBox> 
                 <asp:RangeValidator ID="PrecisionRangeValidator" runat="server" ErrorMessage=" Precision must be an integer"
                                                            ControlToValidate="PrecisionTextBox" Display="Dynamic" MaximumValue="32000" MinimumValue="0"
                                                            Type="Integer"></asp:RangeValidator>                         
            </div>
         </div>
         <div class="form-group">
            <label class="col-md-3 control-label">Scale</label>
            <div class="col-md-9">
                <asp:TextBox ID="ScaleTextbox" runat="server" Columns="15" CssClass="form-control"></asp:TextBox>                        
            </div>
         </div>
         <div class="form-group">
            <label class="col-md-3 control-label">Identity</label>
            <div class="col-md-9">
                <asp:CheckBox ID="IdentityCheckBox" runat="server" CssClass="form-control"></asp:CheckBox>                        
            </div>
         </div>
         <div class="form-group">
            <label class="col-md-3 control-label">Identity Seed</label>
            <div class="col-md-9">
                <asp:TextBox ID="IdentitySeedTextbox" runat="server" Columns="15" CssClass="form-control"></asp:TextBox>                       
            </div>
         </div>
         <div class="form-group">
            <label class="col-md-3 control-label">Identity Increment</label>
            <div class="col-md-9">
                 <asp:TextBox ID="IdentityIncrementTextbox" runat="server" Columns="15" CssClass="form-control"></asp:TextBox>                      
            </div>
         </div>
         <div class="form-group">
            <label class="col-md-3 control-label">Is RowGuid</label>
            <div class="col-md-9">
                 <asp:CheckBox ID="IsRowGuidCheckBox" runat="server" CssClass="form-control"></asp:CheckBox>                      
            </div>
         </div>
         <div class="form-group">
            <div class="col-md-offset-3 col-md-9">
                <asp:Button ID="UpdateButton" runat="server" CssClass="btn btn-default" Text="Update" OnClick="UpdateButton_Click"></asp:Button>
                <a class="btn btn-default" href="/Modules/Column/columns.aspx?database=<%= Server.UrlEncode(Request["database"]) %>&table=<%= Server.UrlEncode(Request["table"]) %>">Cancel</a>
                 <p><asp:Label ID="ErrorUpdatingColumnLabel" runat="server" Visible="False"></asp:Label></p>
            </div>
        </div>
    </div>
</asp:content>






