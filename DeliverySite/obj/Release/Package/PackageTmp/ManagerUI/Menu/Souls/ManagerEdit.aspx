<%@ Page Title="" Language="C#" MasterPageFile="~/ManagerMasterPage.master" AutoEventWireup="true" CodeBehind="ManagerEdit.aspx.cs" Inherits="Delivery.ManagerUI.Menu.Souls.ManagerEdit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
	<script type="text/javascript">
		$(function () {
			if ($('#<%= lblError.ClientID%>').html() == "") {
				$('#errorDiv').hide();
			} else {
				$('#errorDiv').show();
			}

			$("#<%= tbPhone.ClientID %>").mask("+375 (99) 999-99-99");
			$("#<%= tbPhoneHome.ClientID %>").mask("+375 (99) 999-99-99");
			$("#<%= tbPhoneWorkOne.ClientID %>").mask("+375 (99) 999-99-99");
			$("#<%= tbPhoneWorkTwo.ClientID %>").mask("+375 (99) 999-99-99");


			$.datepicker.setDefaults($.datepicker.regional['ru']);
			$("#<%= tbValidity.ClientID %>").datepicker({ nextText: "", prevText: "", changeMonth: true, changeYear: true }).mask("99-99-9999");
			$("#<%= tbBirthDay.ClientID %>").datepicker({ nextText: "", prevText: "", changeMonth: true, changeYear: true }).mask("99-99-9999");
			$("#<%= tbDateOfIssue.ClientID %>").datepicker({ nextText: "", prevText: "", changeMonth: true, changeYear: true }).mask("99-99-9999");
		});
	</script>
	<h3  class="h3custom" style="margin-top: 0;"><asp:Label runat="server" ID="lblTitle"></asp:Label> сотрудника</h3>
	<div class="single-input-fild" style="width: 600px">
		<div class="loginError" style="width: 100%; margin-left: auto; margin-right: auto; margin-top: -40px;" id="errorDiv">
			<asp:Label ID="lblError" runat="server" />
		</div>
		<table class="table">
			<tr>
				<td style="width: 180px; padding-right: 50px; vertical-align: top;">
					<div class="form-group" runat="server" ID="divGravatar">
						<div>
							<asp:Image runat="server" ID="imgGravatar"/>
						</div>
					</div>

					<div class="form-group" style="text-align: center" runat="server" ID="divChangePassword">
						<asp:HyperLink runat="server" ID="hlChangePassword">сменить пароль</asp:HyperLink>
					</div>
				</td>
				<td style="vertical-align: top">
					<div class="form-group">
						<label for="<%= tbLogin.ClientID %>">Логин</label>
						<asp:TextBox ID="tbLogin" CssClass="form-control"  runat="server" />
					</div>
		
					<div class="form-group">
						<asp:Label runat="server" ID="lblPass">Пароль</asp:Label>
						<asp:TextBox ID="tbPassword" TextMode="Password" CssClass="form-control"  runat="server" />
					</div>
		
					<div class="form-group">
						<label for="<%= tbEmail.ClientID %>">E-mail</label>
						<asp:TextBox ID="tbEmail" CssClass="form-control"  runat="server" />
					</div>
		
					<div class="form-group" id="divRole" runat="server">
						<label for="<%= ddlRole.ClientID %>">Роль</label>
						<asp:DropDownList runat="server" CssClass="multi-control" ID="ddlRole"/>
					</div>
		
					<div class="form-group" id="divStatus" runat="server">
						<label for="<%= ddlStatus.ClientID %>">Статус</label>
						<asp:DropDownList runat="server" CssClass="multi-control" ID="ddlStatus"/>
					</div>
		
					<div class="form-group" id="divWhiteList" runat="server">
						<label for="<%= ddlStatus.ClientID %>">Доступ по WhiteList</label>
						<asp:CheckBox runat="server" ID="cbAccessOnlyByWhiteList"/>
					</div>
					
					<hr class="styleHR2"/>
					
					<div class="form-group">
						<label for="<%= tbName.ClientID %>">Имя сотрудника</label>
						<asp:TextBox ID="tbName" CssClass="form-control"  runat="server" />
					</div>

					<div class="form-group">
						<label for="<%= tbFamily.ClientID %>">Фамилия сотрудника</label>
						<asp:TextBox ID="tbFamily" CssClass="form-control"  runat="server" />
					</div>
					
					<div class="form-group">
						<label for="<%= tbAddress.ClientID %>">Адрес проживания</label>
						<asp:TextBox ID="tbAddress" CssClass="form-control"  runat="server" TextMode="MultiLine" style="height: 100px;"/>
					</div>

					<div class="form-group">
						<label for="<%= tbPhone.ClientID %>">Личный телефон:</label>
						<asp:TextBox ID="tbPhone" CssClass="form-control"  runat="server" style="width: 135px;"/>
					</div>
					
					<div class="form-group">
						<label for="<%= tbPhoneHome.ClientID %>">Домашний телефон:</label>
						<asp:TextBox ID="tbPhoneHome" CssClass="form-control"  runat="server" style="width: 135px;"/>
					</div>

					<div class="form-group">
						<label for="<%= tbPhoneWorkOne.ClientID %>">Рабочий телефон #1:</label>
						<asp:TextBox ID="tbPhoneWorkOne" CssClass="form-control"  runat="server" style="width: 135px;"/>
					</div>
					
					<div class="form-group">
						<label for="<%= tbPhoneWorkTwo.ClientID %>">Рабочий телефон #2:</label>
						<asp:TextBox ID="tbPhoneWorkTwo" CssClass="form-control"  runat="server" style="width: 135px;"/>
					</div>
					
					<div class="form-group">
						<label for="<%= tbValidity.ClientID %>">Дата рождения:</label>
						<asp:TextBox ID="tbBirthDay" CssClass="form-control"  runat="server" style="width: 75px;"/>
					</div>
					
					<div class="form-group">
						<label for="<%= tbSkype.ClientID %>">Рабочий skype-аккаунт</label>
						<asp:TextBox ID="tbSkype" CssClass="form-control"  runat="server" />
					</div>

					<hr class="styleHR2"/>

					<div class="form-group">
						<label for="<%= tbPassportSeria.ClientID %>">Серия паспорта:</label>
						<asp:TextBox ID="tbPassportSeria" CssClass="form-control"  runat="server" style="width: 17px;"/>
					</div>

					<div class="form-group">
						<label for="<%= tbPassportNumber.ClientID %>">Номер паспорта:</label>
						<asp:TextBox ID="tbPassportNumber" CssClass="form-control"  runat="server" style="width: 60px;"/>
					</div>

					<div class="form-group">
						<label for="<%= tbPersonalNumber.ClientID %>">Личный код</label><br/>
						<asp:TextBox ID="tbPersonalNumber" CssClass="form-control"  runat="server" />
					</div>

					<div class="form-group">
						<label for="<%= tbROVD.ClientID %>">Орган, выдавший паспорт</label><br/>
						<asp:TextBox ID="tbROVD" CssClass="form-control"  runat="server" TextMode="MultiLine" style="height: 100px;"/>
					</div>
					
					<div class="form-group">
						<label for="<%= tbValidity.ClientID %>">Дата выдачи:</label>
						<asp:TextBox ID="tbDateOfIssue" CssClass="form-control"  runat="server" style="width: 75px;"/>
					</div>

					<div class="form-group">
						<label for="<%= tbValidity.ClientID %>">Действителен до:</label>
						<asp:TextBox ID="tbValidity" CssClass="form-control"  runat="server" style="width: 75px;"/>
					</div>

					<div class="form-group">
						<label for="<%= tbRegistrationAddress.ClientID %>">Адрес прописки</label><br/>
						<asp:TextBox ID="tbRegistrationAddress" CssClass="form-control"  runat="server" TextMode="MultiLine" style="height: 100px;"/>
					</div>
				</td>
			</tr>
			<tr>
				<td colspan="2">
					<div class="form-group">
						<asp:Button ID="btnCreate" style="margin-left: 30px" Text='<%# ButtonText %>' runat="server" CssClass="btn btn-default btn-right" ValidationGroup="LoginGroup"/>
						<asp:Button ID="btnBack" OnClientClick="window.history.go(-1);return false;" CssClass="btn btn-default btn-right" runat="server" Text='Назад'/>          
					</div>
				</td>
			</tr>
		</table>
	</div>

	<asp:CustomValidator ID="CustomValidator1" ControlToValidate="tbName" ClientValidationFunction="validateIfEmpty" EnableClientScript="True" ValidationGroup="LoginGroup" ValidateEmptyText="True" runat="server"  Display="None" ErrorMessage="Вы не ввели имя" ></asp:CustomValidator>
	<asp:CustomValidator ID="CustomValidator2" ControlToValidate="tbFamily" ClientValidationFunction="validateIfEmpty" EnableClientScript="True" ValidationGroup="LoginGroup" ValidateEmptyText="True" runat="server"  Display="None" ErrorMessage="Вы не ввели фамилию" ></asp:CustomValidator>
	<asp:CustomValidator ID="CustomValidator3" ControlToValidate="tbLogin" ClientValidationFunction="validateIfEmpty" EnableClientScript="True" ValidationGroup="LoginGroup" ValidateEmptyText="True" runat="server"  Display="None" ErrorMessage="Вы не ввели логин" ></asp:CustomValidator>
	<asp:CustomValidator ID="CustomValidator4" ControlToValidate="tbPassword" ClientValidationFunction="validateIfEmpty" EnableClientScript="True" ValidationGroup="LoginGroup" ValidateEmptyText="True" runat="server"  Display="None" ErrorMessage="Вы не ввели пароль" ></asp:CustomValidator>
	<asp:CustomValidator ID="CustomValidator5" ControlToValidate="tbEmail" ClientValidationFunction="validateIfEmptyAndEmail" EnableClientScript="True" ValidationGroup="LoginGroup" ValidateEmptyText="True" runat="server"  Display="None" ErrorMessage="Вы не ввели e-mail" ></asp:CustomValidator>
	<asp:CustomValidator ID="CustomValidator6" ControlToValidate="tbPhone" ClientValidationFunction="validateIfEmpty" EnableClientScript="True" ValidationGroup="LoginGroup" ValidateEmptyText="True" runat="server"  Display="None" ErrorMessage="Вы не ввели личный телефон" ></asp:CustomValidator>
	<asp:CustomValidator ID="CustomValidator7" ControlToValidate="tbDateOfIssue" ClientValidationFunction="validateIfEmpty" EnableClientScript="True" ValidationGroup="LoginGroup" ValidateEmptyText="True" runat="server"  Display="None" ErrorMessage="Вы не ввели личный телефон" ></asp:CustomValidator>
	<asp:CustomValidator ID="CustomValidator8" ControlToValidate="tbValidity" ClientValidationFunction="validateIfEmpty" EnableClientScript="True" ValidationGroup="LoginGroup" ValidateEmptyText="True" runat="server"  Display="None" ErrorMessage="Вы не ввели личный телефон" ></asp:CustomValidator>
	<asp:CustomValidator ID="CustomValidator9" ControlToValidate="tbBirthDay" ClientValidationFunction="validateIfEmpty" EnableClientScript="True" ValidationGroup="LoginGroup" ValidateEmptyText="True" runat="server"  Display="None" ErrorMessage="Вы не ввели личный телефон" ></asp:CustomValidator>
	
	<asp:ValidationSummary ID="ValidationSummary2" runat="server" ValidationGroup="LoginGroup" ShowMessageBox="true" ShowSummary="false"/>
</asp:Content>
