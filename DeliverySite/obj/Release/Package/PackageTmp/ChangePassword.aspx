<%@ Page Title="" Language="C#" MasterPageFile="~/EmptyMasterPage.master" AutoEventWireup="true" CodeBehind="ChangePassword.aspx.cs" Inherits="Delivery.ChangePassword" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
	<div class="login-title">Восстановление логина и пароля. Шаг 2</div>
	<hr style="margin-top: 0;">
	<div class="logon">
		<asp:Label runat="server" ID="lblName"></asp:Label>, введите новый пароль
		<asp:TextBox CssClass="form-control" runat="server" ID="tbNewPassword" placeholder="новый пароль"></asp:TextBox><br/><br/>
		<asp:Button runat="server" ID="btnSave" Text="Сохранить" OnClick="btnSave_OnClick" ValidationGroup="LoginGroup" CssClass="btn btn-default btn-right"/><br/><br/>
	</div>
	
	<hr style="margin-top: 0;">
	<asp:CustomValidator ID="CustomValidator18" ControlToValidate="tbNewPassword" ClientValidationFunction="validateIfEmpty" EnableClientScript="True" ValidationGroup="LoginGroup" ValidateEmptyText="True" runat="server"  Display="None" ErrorMessage="Введите новый пароль" ></asp:CustomValidator>
	<asp:ValidationSummary ID="ValidationSummary1" runat="server" ValidationGroup="LoginGroup" ShowMessageBox="true" ShowSummary="false"/>
</asp:Content>
