<%@ Page Title="" Language="C#" MasterPageFile="~/EmptyMasterPage.master" AutoEventWireup="true" CodeBehind="ForgotPassword.aspx.cs" Inherits="Delivery.ForgotPassword" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
	<script type="text/javascript">
		$(function () {
			if ($('#<%= lblError.ClientID%>').html() == "") {
				$('#errorDiv').hide();
			} else {
				$('#errorDiv').show();
			}
		});
	</script>
	<div class="loginError" id="errorDiv">
		<asp:Label runat="server" ID="lblError"></asp:Label>
	</div>
	<div class="login-title">Восстановление логина и пароля. Шаг 1</div>
	<hr style="margin-top: 0;">
	<div class="logon">
		<label for="<%= tbEmail.ClientID %>">Введите e-mail, указанный при регистрации</label>
		<asp:TextBox CssClass="form-control" runat="server" ID="tbEmail" placeholder="example@site.com"></asp:TextBox><br/><br/>
		<asp:Button runat="server" ID="btnRemember" Text="Восстановить" OnClick="btnRemember_OnClick" ValidationGroup="LoginGroup" CssClass="btn btn-default btn-right"/><br/><br/>
	</div>
	
	<hr style="margin-top: 0;">
	<asp:CustomValidator ID="CustomValidator18" ControlToValidate="tbEmail" ClientValidationFunction="validateIfEmpty" EnableClientScript="True" ValidationGroup="LoginGroup" ValidateEmptyText="True" runat="server"  Display="None" ErrorMessage="Введите емейл" ></asp:CustomValidator>
	<asp:ValidationSummary ID="ValidationSummary1" runat="server" ValidationGroup="LoginGroup" ShowMessageBox="true" ShowSummary="false"/>
</asp:Content>
