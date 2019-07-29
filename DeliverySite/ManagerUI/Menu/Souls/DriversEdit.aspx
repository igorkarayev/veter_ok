<%@ Page Title="" Language="C#" MasterPageFile="~/ManagerMasterPage.master" AutoEventWireup="true" CodeBehind="DriversEdit.aspx.cs" Inherits="Delivery.ManagerUI.Menu.Souls.DriversEdit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
	<script type="text/javascript">
		$(function () {
			if ($('#<%= lblError.ClientID%>').html() == "") {
				$('#errorDiv').hide();
			} else {
				$('#errorDiv').show();
			}

			$("#<%= tbPhoneOne.ClientID %>").mask("+375 (99) 999-99-99");
			$("#<%= tbPhoneTwo.ClientID %>").mask("+375 (99) 999-99-99");
			$("#<%= tbPhoneHome.ClientID %>").mask("+375 (99) 999-99-99");
			$("#<%= tbContactPersonPhone.ClientID %>").mask("+375 (99) 999-99-99");

			$.datepicker.setDefaults($.datepicker.regional['ru']);
			$("#<%= tbValidity.ClientID %>").datepicker({ nextText: "", prevText: "", changeMonth: true, changeYear: true }).mask("99-99-9999");
			$("#<%= tbBirthDay.ClientID %>").datepicker({ nextText: "", prevText: "", changeMonth: true, changeYear: true }).mask("99-99-9999");
			$("#<%= tbDateOfIssue.ClientID %>").datepicker({ nextText: "", prevText: "", changeMonth: true, changeYear: true }).mask("99-99-9999");

			$("#<%= tbDriverPassportDateOfIssue.ClientID %>").datepicker({ nextText: "", prevText: "", changeMonth: true, changeYear: true }).mask("99-99-9999");
			$("#<%= tbDriverPassportValidity.ClientID %>").datepicker({ nextText: "", prevText: "", changeMonth: true, changeYear: true }).mask("99-99-9999");
			$("#<%= tbMedPolisDateOfIssue.ClientID %>").datepicker({ nextText: "", prevText: "", changeMonth: true, changeYear: true }).mask("99-99-9999");
			$("#<%= tbMedPolisValidity.ClientID %>").datepicker({ nextText: "", prevText: "", changeMonth: true, changeYear: true }).mask("99-99-9999");
		});
	</script>
	<h3 class="h3custom" style="margin-top: 0;"><%# ActionText %> водителя</h3>
	
	<div class="loginError" style="width: 90%; margin-left: auto; margin-right: auto; margin-top: 0px;" id="errorDiv">
		<asp:Label ID="lblError" runat="server" />
	</div>

	<div class="single-input-fild" style="width: 400px">
		<div class="form-group" id="div9" runat="server">
			<label>Машина</label>
			<asp:DropDownList runat="server" CssClass="multi-control" ID="ddlCar"/>
		</div>

		<div class="form-group" id="divStatus" runat="server">
			<label>Статус</label>
			<asp:DropDownList runat="server" CssClass="multi-control" ID="ddlStatus"/>
		</div>
		
		<hr class="styleHR2"/>

		<div class="form-group" id="div5" runat="server">
			<label>Фамилия:</label>
			<asp:TextBox ID="tbFirstName" CssClass="form-control" runat="server" width="95%"/>
		</div>
		
		<div class="form-group" id="div6" runat="server">
			<label>Имя:</label>
			<asp:TextBox ID="tbLastName" CssClass="form-control" runat="server" width="95%"/>
		</div>
		
		<div class="form-group" id="div7" runat="server">
			<label>Отчество:</label>
			<asp:TextBox ID="tbThirdName" CssClass="form-control" runat="server" width="95%"/>
		</div>
		
		<div class="form-group" id="div1" runat="server">
			<label>Телефон #1:</label>
			<asp:TextBox ID="tbPhoneOne" CssClass="form-control" runat="server" style="width: 135px;"/>
		</div>
		
		<div class="form-group" id="div8" runat="server">
			<label>Телефон #2:</label>
			<asp:TextBox ID="tbPhoneTwo" CssClass="form-control" runat="server" style="width: 135px;"/>
		</div>

		<div class="form-group">
			<label>Домашний телефон:</label>
			<asp:TextBox ID="tbPhoneHome" CssClass="form-control"  runat="server" style="width: 135px;"/>
		</div>
		
		<div class="form-group">
			<label>Домашний адрес</label><br/>
			<asp:TextBox ID="tbHomeAddress" CssClass="form-control"  runat="server" TextMode="MultiLine" style="height: 100px;"/>
		</div>
		
		<div class="form-group">
			<label>Дата рождения:</label>
			<asp:TextBox ID="tbBirthDay" CssClass="form-control"  runat="server" style="width: 75px;"/>
		</div>
		
		<div class="form-group" id="div2" runat="server">
			<label>ФИО родственника, через которого можно найти водителя:</label>
			<asp:TextBox ID="tbContactPersonFIO" CssClass="form-control" runat="server" width="95%"/>
		</div>

		<div class="form-group">
			<label>Телефон родственника, через которого можно найти водителя:</label>
			<asp:TextBox ID="tbContactPersonPhone" CssClass="form-control"  runat="server" style="width: 135px;"/>
		</div>

		<hr class="styleHR2"/>
		
		<div class="form-group">
			<label>Серия паспорта:</label>
			<asp:TextBox ID="tbPassportSeria" CssClass="form-control"  runat="server" style="width: 17px;"/>
		</div>

		<div class="form-group">
			<label>Номер паспорта:</label>
			<asp:TextBox ID="tbPassportNumber" CssClass="form-control"  runat="server" style="width: 60px;"/>
		</div>

		<div class="form-group">
			<label>Личный код</label><br/>
			<asp:TextBox ID="tbPersonalNumber" CssClass="form-control"  runat="server" />
		</div>

		<div class="form-group">
			<label>Орган, выдавший паспорт</label><br/>
			<asp:TextBox ID="tbROVD" CssClass="form-control"  runat="server" TextMode="MultiLine" style="height: 100px;"/>
		</div>
					
		<div class="form-group">
			<label>Дата выдачи:</label>
			<asp:TextBox ID="tbDateOfIssue" CssClass="form-control"  runat="server" style="width: 75px;"/>
		</div>

		<div class="form-group">
			<label>Действителен до:</label>
			<asp:TextBox ID="tbValidity" CssClass="form-control"  runat="server" style="width: 75px;"/>
		</div>

		<div class="form-group">
			<label>Адрес прописки</label><br/>
			<asp:TextBox ID="tbRegistrationAddress" CssClass="form-control"  runat="server" TextMode="MultiLine" style="height: 100px;"/>
		</div>

		<hr class="styleHR2"/>
	  
		<div class="form-group" id="div4" runat="server">
			<label>Номер ВУ:</label>
			<asp:TextBox ID="tbDriverPassport" CssClass="form-control" runat="server" width="95%"/>
			<asp:HiddenField runat="server" ID="hfDriverPassport" />
		</div>
		
		<div class="form-group">
			<label>Дата выдачи ВУ:</label>
			<asp:TextBox ID="tbDriverPassportDateOfIssue" CssClass="form-control"  runat="server" style="width: 75px;"/>
		</div>
		
		<div class="form-group">
			<label>ВУ действителено до:</label>
			<asp:TextBox ID="tbDriverPassportValidity" CssClass="form-control"  runat="server" style="width: 75px;"/>
		</div>
		
		<div class="form-group">
			<label>Дата выдачи мед. справки:</label>
			<asp:TextBox ID="tbMedPolisDateOfIssue" CssClass="form-control"  runat="server" style="width: 75px;"/>
		</div>
		
		<div class="form-group">
			<label>Мед. справка действительна до:</label>
			<asp:TextBox ID="tbMedPolisValidity" CssClass="form-control"  runat="server" style="width: 75px;"/>
		</div>
			
		<div class="form-group" style="margin-top: 10px;">
			<asp:Button ID="btnCreate" class="btn btn-default btn-right" runat="server" Text='<%# ButtonText %>' ValidationGroup="LoginGroup" style="margin-left: 30px;"/>
			<asp:Button ID="btnBack" OnClientClick="window.history.go(-1);return false;" class="btn btn-default btn-right" runat="server" Text='Назад'/>
		</div>
	</div>
	<asp:CustomValidator ID="CustomValidator1" ControlToValidate="tbFirstName" ClientValidationFunction="validateIfEmpty" EnableClientScript="True" ValidationGroup="LoginGroup" ValidateEmptyText="True" runat="server"  Display="None" ErrorMessage="Вы не ввели фамилию" ></asp:CustomValidator>
	<asp:CustomValidator ID="CustomValidator2" ControlToValidate="tbLastName" ClientValidationFunction="validateIfEmpty" EnableClientScript="True" ValidationGroup="LoginGroup" ValidateEmptyText="True" runat="server"  Display="None" ErrorMessage="Вы не ввели имя" ></asp:CustomValidator>
	<asp:CustomValidator ID="CustomValidator3" ControlToValidate="tbThirdName" ClientValidationFunction="validateIfEmpty" EnableClientScript="True" ValidationGroup="LoginGroup" ValidateEmptyText="True" runat="server"  Display="None" ErrorMessage="Вы не ввели отчество" ></asp:CustomValidator>
	<asp:CustomValidator ID="CustomValidator4" ControlToValidate="tbPhoneOne" ClientValidationFunction="validateIfEmpty" EnableClientScript="True" ValidationGroup="LoginGroup" ValidateEmptyText="True" runat="server"  Display="None" ErrorMessage="Вы не ввели телефон водителя" ></asp:CustomValidator>
	<asp:CustomValidator ID="CustomValidator5" ControlToValidate="tbDriverPassport" ClientValidationFunction="validateIfEmpty" EnableClientScript="True" ValidationGroup="LoginGroup" ValidateEmptyText="True" runat="server"  Display="None" ErrorMessage="Вы не ввели ВУ водителя" ></asp:CustomValidator>

	<asp:ValidationSummary ID="ValidationSummary2" runat="server" ValidationGroup="LoginGroup" ShowMessageBox="true" ShowSummary="false"/>
</asp:Content>
