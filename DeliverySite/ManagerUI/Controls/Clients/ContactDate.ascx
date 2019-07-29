<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ContactDate.ascx.cs" Inherits="Delivery.ManagerUI.Controls.Clients.ContactDate" %>
<script type="text/javascript">
	$(function () {
		$(".client-contact-date").datepicker({ nextText: "", prevText: "", changeMonth: true, changeYear: true }).mask("99-99-9999");
	});
</script>
<asp:TextBox ID="tbContactDate" Width="65px" runat="server"  CssClass="client-contact-date form-control"
	onchange='<%# string.Format("SaveContactDate({0},\"{1}\",\"{2}\",\"{3}\",\"{4}\",\"{5}\")", ClientID, AppKey, ListViewControlFullID, UserID, UserIP, PageName)%>'
	onfocus='<%# string.Format("ClearLabelStatusContactDate({0},\"{1}\")", ClientID, ListViewControlFullID)%>' /><br/>
<asp:Label runat="server" ID="lblSaveContactDateStatus"></asp:Label> 
