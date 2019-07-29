<%@ Page Language="C#" Inherits="Delivery.Default" MasterPageFile="~/EmptyMasterPage.master" Title="Вход в кабинет"%>
<%@ Import Namespace="Delivery.BLL.Helpers" %>
<asp:Content ContentPlaceHolderID="MainContent" ID="MainContentContent" runat="server">
	<script type="text/javascript">
		$(function () {
			if ($('#<%= Msg.ClientID%>').html() == "") {
				$('#errorDiv').hide();
			} else {
				$('#errorDiv').show();
			}
		});
	</script>
	<div class="login-title">Вход в кабинет</div>
	<hr style="margin-top: 0;">
	<div class="logon">
		<div class="loginError" style="width: 100%" id="errorDiv">
			<asp:Label ID="Msg" runat="server" />
		</div>
		
		<div class="form-group">
		    <asp:Label runat="server" CssClass="form-label" Text="Логин"></asp:Label>
			<asp:TextBox ID="tbUserLogin" CssClass="form-control" runat="server" placeholder="Логин"/>
		</div>

		<div class="form-group">
		    <asp:Label runat="server" CssClass="form-label" Text="Пароль"></asp:Label>
			<asp:TextBox ID="tbUserPass" CssClass="form-control" TextMode="Password" runat="server" placeholder="Пароль"/>
		</div>

		<div class="form-group">
			<asp:Button ID="bSubmit" Text="Войти" runat="server"  CssClass="btn btn-default" ValidationGroup="LoginGroup"/>
		</div>

	</div>
	<div class="form-group" style="font-size: 14px; overflow: hidden">
	    <span style="float: left; color: #666;">
            <asp:CheckBox runat="server" ID="cbRememberMe" Checked="false"/>  Запомнить 
        </span>
		<a href="forgotpassword" class="forgot-password" style="float: right">Забыли?</a>
	</div>
	<asp:CustomValidator ID="CustomValidator1" ControlToValidate="tbUserLogin" ClientValidationFunction="validateIfEmpty" EnableClientScript="True" ValidationGroup="LoginGroup" ValidateEmptyText="True" runat="server"  Display="None" ErrorMessage="Вы не ввели логи (почтовый адрес)" ></asp:CustomValidator>
	<asp:CustomValidator ID="CustomValidator18" ControlToValidate="tbUserPass" ClientValidationFunction="validateIfEmpty" EnableClientScript="True" ValidationGroup="LoginGroup" ValidateEmptyText="True" runat="server"  Display="None" ErrorMessage="Вы не ввели пароль" ></asp:CustomValidator>
	<asp:ValidationSummary ID="ValidationSummary1" runat="server" ValidationGroup="LoginGroup" ShowMessageBox="true" ShowSummary="false"/>
</asp:Content>


