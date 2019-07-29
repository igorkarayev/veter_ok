<%@ Page Title="" Language="C#" MasterPageFile="~/ManagerMasterPage.master" AutoEventWireup="true" CodeBehind="ClientCreate.aspx.cs" Inherits="Delivery.ManagerUI.Menu.Souls.ClientCreate" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
	<div class="floatMenuLeft lblStatus" style="margin-left: 0">
	  <asp:Label runat="server" ID="lblStatus"></asp:Label>
	</div>
	
	<h3 class="h3custom" style="margin-top: 0;">Добавление клиента</h3>
	
	<div class="loginError" style="width: 80%; margin: 20px 0 10px 25px;" id="errorDiv">
		<asp:Label ID="lblError" runat="server" />
	</div>

	<span style="font-size: 18px; margin-left: 20px; font-style: italic">Основная информация</span>
	<hr class="styleHR2" style="margin-top: 5px;"/>
	<div class="single-input-fild" style="width: 500px; margin-bottom: 10px;">
		<table class="table">
			<tr>
				<td style="vertical-align: top">
					<div class="form-group">
						<div style="display: inline-block; width: 80px;">
							E-mail: 
						</div>
						<asp:TextBox ID="tbEmail" runat="server" CssClass="form-control" style="width: 185px"/>
					</div>

					<div class="form-group">
						<div style="display: inline-block; width: 80px;">
							Имя: 
						</div>
						<asp:TextBox ID="tbName" runat="server" CssClass="form-control" style="width: 135px"/>
					</div>
	
					<div class="form-group">
						<div style="display: inline-block; width: 80px;">
							Фамилия: 
						</div>
						<asp:TextBox ID="tbFamily" runat="server" CssClass="form-control" style="width: 135px"/>
					</div>
	
					<div class="form-group">
						<div style="display: inline-block; width: 80px;">
							Телефон: 
						</div>
						<asp:TextBox ID="tbPhone" runat="server" CssClass="form-control" style="width: 135px;"/>
					</div>
					
					<div class="form-group">
						<label>Ответственный мене. по прод.:</label>
						<asp:DropDownList ID="ddlSalesManager" runat="server" width="100%" CssClass="searchField ddl-control" style="width: 235px;"/>
					</div>

					<div class="form-group">
						Статус проработки:
						<asp:DropDownList runat="server" CssClass="ddl-control" ID="ddlStatusStady" style="width: 50%"/>
					</div>
					
					<div class="form-group">
						Дата сл. контакта:
						<asp:TextBox runat="server" CssClass="form-control" ID="tbContactDate" style="width: 75px"/>
					</div>
					<div class="form-group">
						Комментарий:<br/>
						<asp:TextBox runat="server" TextMode="MultiLine" CssClass="form-control" ID="tbComment" style="width: 100%; height: 60px;"/>
					</div>
				</td>
			</tr>
		</table>
	</div>
	

	
	<span style="font-size: 18px; margin-left: 20px; font-style: italic">Данные для профиля клиента</span>
	<hr class="styleHR2" style="margin-top: 5px;"/>
	<div class="single-input-fild" style="width: 600px;  margin-bottom: 20px;">
		<div class="form-group">
			<div style="display: inline-block;">
				Тип профиля: 
			</div>
			<asp:DropDownList CssClass="ddl-control" runat="server" ID="ddlProfileType" AutoPostBack="False"  width="200px"/>
		</div>
		
		<div class="form-group">
			<div style="display: inline-block;">
				Компания: 
			</div>
			<asp:DropDownList CssClass="ddl-control" runat="server" ID="ddlCompanyType" AutoPostBack="False"   style="width:80px"/>
			«<asp:TextBox ID="tbCompanyName" runat="server" CssClass="form-control" style="width: 300px;"/>»
		</div>
		
		<div class="form-group">
			<div style="display: inline-block; width: 160px;">
				Фамилия директора: 
			</div>
			<asp:TextBox ID="tbDirectorFamily" runat="server" CssClass="form-control" style="width: 310px;"/>
		</div>
		
		<div class="form-group">
			<div style="display: inline-block; width: 160px;">
				Имя директора: 
			</div>
			<asp:TextBox ID="tbDirectorName" runat="server" CssClass="form-control" style="width: 310px;"/>
		</div>
		
		<div class="form-group">
			<div style="display: inline-block; width: 160px;">
				Отчество директора: 
			</div>
			<asp:TextBox ID="tbDirectorPatronymic" runat="server" CssClass="form-control" style="width: 310px;"/>
		</div>
		
		<div class="form-group">
			<div style="display: inline-block; width: 160px;">
				Телефон директора: 
			</div>
			<asp:TextBox ID="tbDirectorPhone" runat="server" CssClass="form-control" style="width: 135px;"/>
		</div>
		
		<div class="form-group">
			<div style="display: inline-block; width: 160px;">
				ФИО контакт. лица: 
			</div>
			<asp:TextBox CssClass="form-control" ID="tbContactPersonFIO" runat="server" width="91%"/>
		</div>
		
		<div class="form-group">
			<div>
				Контакт. телефоны
			</div>
			#1: <asp:TextBox CssClass="form-control" ID="tbContactPhoneNumbers" runat="server" Width="135px"/>&nbsp;&nbsp;&nbsp;Совпадает с тел. директора <asp:CheckBox ID="cbContactPhoneNumber" runat="server"/>
			<br/><br/>
			#2: <asp:TextBox CssClass="form-control" ID="tbContactPhoneNumbers2" runat="server" Width="135px"/>
		</div>
	</div>
	
	
	
	<span style="font-size: 18px; margin-left: 20px; font-style: italic">Категории, доступные клиенту</span>
	<hr class="styleHR2" style="margin-top: 5px;"/>
	<div style="color: darkred; font-style: italic; font-size: 11px; text-align: left; margin-top: -15px; margin-left: 20px; ">
		Если не выбрана ни одна категория, то клиенту будут доступны все наименования
	</div>
	<div class="single-input-fild" style="width: 90%; margin-bottom: 50px;">
		<asp:ListView runat="server" ID="lvAllCategory">
			<LayoutTemplate>
				<div runat="server" id="itemPlaceholder"></div>
			</LayoutTemplate>
			<ItemTemplate>
				<div id="Tr2" runat="server" style="width: 32%; display: inline-block">
					<asp:CheckBox runat="server" ID="cbCategory"/>
					<asp:HiddenField ID="hfCategoryId" runat="server" Value='<%# Eval("ID") %>' />
					<asp:HyperLink ID="HyperLink2" runat="server" NavigateUrl='<%#"~/ManagerUI/Menu/Souls/UserToCategoryView.aspx?id=" + ClientID %>' CssClass="alternative">
						<asp:Label ID="lblName" runat="server" Text='<%# Eval("Name") %>' />
					</asp:HyperLink>
				</div>
			</ItemTemplate>
		</asp:ListView>
	</div>
	

	
	<div class="single-input-fild" style="width: 500px;">
		<asp:Button ID="btnCreate" runat="server" CssClass="btn btn-default btn-right" Text='Сохранить' ValidationGroup="LoginGroup" style="margin-left: 30px;"/>
		<asp:Button ID="btnBack" OnClientClick="window.history.go(-1);return false;"  CssClass="btn btn-default btn-right"  runat="server" Text='Назад' style="margin-left: 30px;"/> 
	</div>
	
	<script type="text/javascript">
		$(function () {
			$("#<%# tbContactDate.ClientID%>").datepicker({ nextText: "", prevText: "", changeMonth: true, changeYear: true }).mask("99-99-9999");

			$.datepicker.setDefaults($.datepicker.regional['ru']);
			$("#<%= tbPhone.ClientID %>").mask("+375 (99) 999-99-99");
			$("#<%= tbDirectorPhone.ClientID %>").mask("+375 (99) 999-99-99");
			$("#<%= tbContactPhoneNumbers.ClientID %>").mask("+375 (99) 999-99-99");
			$("#<%= tbContactPhoneNumbers2.ClientID %>").mask("+375 (99) 999-99-99");

		});

		$(function () {
			if ($.trim($("#<%= lblStatus.ClientID %>").html()) !== "") {
				$('.lblStatus').show();
				setTimeout(function () { $('.lblStatus').hide(); }, 10000);
			}


			/** Autocomplete для городов СТАРТ **/
			$('#<%= tbCompanyName.ClientID%>').autocomplete({
				minChars: '2',
				params: {
					appkey: '<%= AppKey%>'
				},
				serviceUrl: '../../../AppServices/AutocompliteAPI.asmx/GetAvaliableCompanyJson',
				onSelect: function (suggestion) {
				}
			});
		});

		$("#<%= cbContactPhoneNumber.ClientID %>").click(function () {
			if (this.checked) {
				$("#<%= tbContactPhoneNumbers.ClientID %>").val($("#<%= tbDirectorPhone.ClientID %>").val());
			} else {
				$("#<%= tbContactPhoneNumbers.ClientID %>").val("");
			}
		});

		$(function () {
			 if ($('#<%= lblError.ClientID%>').html() == "") {
				  $('#errorDiv').hide();
			  } else {
				  $('#errorDiv').show();
			  }
		  });
	</script>

	<asp:CustomValidator ID="CustomValidator5" ControlToValidate="tbEmail" runat="server"  ClientValidationFunction="validateIfEmpty" EnableClientScript="True" ValidationGroup="LoginGroup" ValidateEmptyText="True" Display="None" ErrorMessage="Вы не ввели email клиента"></asp:CustomValidator>
	<asp:CustomValidator ID="CustomValidator1" ControlToValidate="tbName" runat="server"  ClientValidationFunction="validateIfEmpty" EnableClientScript="True" ValidationGroup="LoginGroup" ValidateEmptyText="True" Display="None" ErrorMessage="Вы не ввели имя клиента"></asp:CustomValidator>
	<asp:CustomValidator ID="CustomValidator2" ControlToValidate="tbFamily" runat="server"  ClientValidationFunction="validateIfEmpty" EnableClientScript="True" ValidationGroup="LoginGroup" ValidateEmptyText="True" Display="None" ErrorMessage="Вы не ввели фамилию клиента"></asp:CustomValidator>
	<asp:CustomValidator ID="CustomValidator3" ControlToValidate="tbPhone" runat="server"  ClientValidationFunction="validateIfEmpty" EnableClientScript="True" ValidationGroup="LoginGroup" ValidateEmptyText="True" Display="None" ErrorMessage="Вы не ввели телефон клиента"></asp:CustomValidator>
	<asp:CustomValidator ID="CustomValidator4" ControlToValidate="tbCompanyName" runat="server"  ClientValidationFunction="validateIfEmpty" EnableClientScript="True" ValidationGroup="LoginGroup" ValidateEmptyText="True" Display="None" ErrorMessage="Вы не ввели название компании"></asp:CustomValidator>
	<asp:CustomValidator ID="CustomValidator6" ControlToValidate="tbContactPersonFIO" runat="server"  ClientValidationFunction="validateIfEmpty" EnableClientScript="True" ValidationGroup="LoginGroup" ValidateEmptyText="True" Display="None" ErrorMessage="Вы не ввели ФИО контактного лица"></asp:CustomValidator>
	<asp:CustomValidator ID="CustomValidator7" ControlToValidate="tbContactPhoneNumbers" runat="server"  ClientValidationFunction="validateIfEmpty" EnableClientScript="True" ValidationGroup="LoginGroup" ValidateEmptyText="True" Display="None" ErrorMessage="Вы не ввели телефон контактного лица"></asp:CustomValidator>
	<asp:ValidationSummary ID="ValidationSummary1" runat="server" ValidationGroup="LoginGroup" ShowMessageBox="true" ShowSummary="false"/>
</asp:Content>
