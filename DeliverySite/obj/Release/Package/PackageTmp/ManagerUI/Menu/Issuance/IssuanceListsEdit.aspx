<%@ Page Title="" Language="C#" MasterPageFile="~/ManagerMasterPage.master" AutoEventWireup="true" CodeBehind="IssuanceListsEdit.aspx.cs" Inherits="Delivery.ManagerUI.Menu.Issuance.IssuanceListsEdit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
	<script type="text/javascript">
		$(function () {
			$("#<%= tbIssuanceDate.ClientID %>")
				.datepicker({ nextText: "", prevText: "", changeMonth: true, changeYear: true })
				.mask("99-99-9999");

			if ($('#<%= lblError.ClientID%>').html() == "") {
				$('#errorDiv').hide();
			} else {
				$('#errorDiv').show();
			}
		});
	</script>
	<div class="logon" style="width: 400px; margin-left: auto; margin-right: auto; overflow: hidden; padding-right: 40px;">
		<h3 class="blueTitle"><asp:Label runat="server" ID="lblIssuanceList" Text="Создание расчетного листа"></asp:Label></h3>
		<div class="loginError" style="width: 100%" id="errorDiv">
			<asp:Label ID="lblError" runat="server" />
		</div>
		
		
		<div class="form-group">
			<label for="<%= tbComment.ClientID %>">Описание расчетного листа</label>
			<asp:TextBox ID="tbComment" CssClass="multi-control" runat="server" TextMode="MultiLine" placeholder="Тут пишем описание листа"/>
		</div>

		<div class="form-group" style="width: 280px; display: inline-block">
			<label for="<%= tbUID.ClientID %>">UID клиента</label><br/>
			<asp:TextBox ID="tbUID" CssClass="form-control" runat="server" placeholder="UID" style="width: 75px;"/>
		</div>
		
		<div class="form-group"  style="width: 110px; display: inline-block">
			<label for="<%= tbIssuanceDate.ClientID %>">Дата расчета</label><br/>
			<asp:TextBox ID="tbIssuanceDate" CssClass="form-control" runat="server" style="width: 75px;"/>
		</div>
		

		<div style="margin-top: 20px;">
			<asp:Button ID="bSubmit" Text="Сохранить" runat="server" CssClass="btn btn-default btn-right" ValidationGroup="LoginGroup"/>
		</div>
		
	</div>
	<asp:CustomValidator ID="CustomValidator1" ControlToValidate="tbUID" ClientValidationFunction="validateIfEmpty" EnableClientScript="True" ValidationGroup="LoginGroup" ValidateEmptyText="True" runat="server"  Display="None" ErrorMessage="Вы не ввели UID клиента" ></asp:CustomValidator>
	<asp:CustomValidator ID="CustomValidator18" ControlToValidate="tbIssuanceDate" ClientValidationFunction="validateIfEmpty" EnableClientScript="True" ValidationGroup="LoginGroup" ValidateEmptyText="True" runat="server"  Display="None" ErrorMessage="Вы не ввели дату расчета" ></asp:CustomValidator>
	<asp:ValidationSummary ID="ValidationSummary1" runat="server" ValidationGroup="LoginGroup" ShowMessageBox="true" ShowSummary="false"/>
</asp:Content>
