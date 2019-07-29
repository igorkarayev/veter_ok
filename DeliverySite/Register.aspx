<%@ Page Language="C#" Inherits="Delivery.Register" MasterPageFile="~/EmptyMasterPage.master" Title="Регистрация"%>
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
	<div class="login-title">Регистрация</div>
	<hr style="margin-top: 0;">
	<div class="logon">
		<div class="loginError" style="width: 100%" id="errorDiv">
			<asp:Label ID="Msg" runat="server" />
		</div>
		
		<div class="form-group">
	        <asp:TextBox ID="tbUserEmail" CssClass="form-control" type="email" runat="server" placeholder="Email"/>
	    </div>
        
	    <div class="form-group">
	        <asp:TextBox ID="tbUserLastName" CssClass="form-control" runat="server" placeholder="Фамилия"/>
	    </div>
        
        <div class="form-group">
	        <asp:TextBox ID="tbUserFirstName" CssClass="form-control" runat="server" placeholder="Имя"/>
	    </div>
        
	    <div class="form-group">
	        <asp:TextBox ID="tbUserPhone" CssClass="form-control" runat="server" placeholder="Телефон"/>
	    </div>
        
	    <div class="form-group">
	        <asp:TextBox ID="tbUserLogin" CssClass="form-control" runat="server" placeholder="Логин"/>
	    </div>

		<div class="form-group">
			<asp:TextBox ID="tbUserPass" CssClass="form-control" TextMode="Password" runat="server" placeholder="Пароль"/>
		</div>

		<div class="form-group">
			<asp:Button ID="bSubmit" Text="Зарегистрироваться" runat="server"  CssClass="btn btn-default" ValidationGroup="LoginGroup"/>
		</div>

	</div>
	<hr style="margin-top: 0;">
	<div class="form-group" style="font-size: 14px; font-style: italic; text-align: center;">
			По вопросам регистрации обращайтесь к нашим менеджерам по телефонам <%=BackendHelper.TagToValue("main_phones") %>.
	</div>
	<hr style="margin-top: 0;">

	<asp:CustomValidator ID="CustomValidator1" ControlToValidate="tbUserLogin" ClientValidationFunction="validateIfEmpty" EnableClientScript="True" ValidationGroup="LoginGroup" ValidateEmptyText="True" runat="server"  Display="None" ErrorMessage="Вы не ввели логин" ></asp:CustomValidator>
	<asp:CustomValidator ID="CustomValidator18" ControlToValidate="tbUserPass" ClientValidationFunction="validateIfEmpty" EnableClientScript="True" ValidationGroup="LoginGroup" ValidateEmptyText="True" runat="server"  Display="None" ErrorMessage="Вы не ввели пароль" ></asp:CustomValidator>
    <asp:CustomValidator ID="CustomValidator2" ControlToValidate="tbUserEmail" ClientValidationFunction="validateIfEmpty" EnableClientScript="True" ValidationGroup="LoginGroup" ValidateEmptyText="True" runat="server"  Display="None" ErrorMessage="Вы не ввели почтовый адрес" ></asp:CustomValidator>
    <asp:CustomValidator ID="CustomValidator3" ControlToValidate="tbUserFirstName" ClientValidationFunction="validateIfEmpty" EnableClientScript="True" ValidationGroup="LoginGroup" ValidateEmptyText="True" runat="server"  Display="None" ErrorMessage="Вы не ввели имя" ></asp:CustomValidator>
    <asp:CustomValidator ID="CustomValidator4" ControlToValidate="tbUserLastName" ClientValidationFunction="validateIfEmpty" EnableClientScript="True" ValidationGroup="LoginGroup" ValidateEmptyText="True" runat="server"  Display="None" ErrorMessage="Вы не ввели фамилию" ></asp:CustomValidator>
    <asp:CustomValidator ID="CustomValidator5" ControlToValidate="tbUserPhone" ClientValidationFunction="validateIfEmpty" EnableClientScript="True" ValidationGroup="LoginGroup" ValidateEmptyText="True" runat="server"  Display="None" ErrorMessage="Вы не ввели телефон" ></asp:CustomValidator>
	
    <asp:ValidationSummary ID="ValidationSummary1" runat="server" ValidationGroup="LoginGroup" ShowMessageBox="true" ShowSummary="false"/>
</asp:Content>


