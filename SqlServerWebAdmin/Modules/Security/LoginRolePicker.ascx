<%@ Control Language="c#" AutoEventWireup="false" Inherits="SqlServerWebAdmin.ItemPicker" TargetSchema="http://schemas.microsoft.com/intellisense/ie5"%>
<table class="table table-bordered">
	<tr>
		<tr>
			<td>
				Available Logins:
			</td>
			<td>
				&nbsp;
			</td>
			<td>
				Assigned Logins:
			</td>
		</tr>
		<tr>	
			<td>
				<asp:ListBox id="ItemsBox" CssClass="form-control" Runat="server"/>
			</td>
			<td>
				<asp:Button CssClass="btn btn-default" Text="<<<" OnClick="RemoveItem_Click" Runat="server" />
				<br/>
				<asp:Button Text=">>>" CssClass="btn btn-default" OnClick="AddItem_Click" Runat="server" />
			</td>
			<td class="databaseListItem">
				<asp:ListBox id="SelectedItemsBox" CssClass="form-control" Runat="server" />
			</td>
		</tr>
	</table>