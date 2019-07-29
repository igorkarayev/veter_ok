<%@ Page Title="" Language="C#" MasterPageFile="~/ManagerMasterPage.master" AutoEventWireup="true" CodeBehind="WarehouseEdit.aspx.cs" Inherits="Delivery.ManagerUI.Menu.Souls.WarehouseEdit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
	<script type="text/javascript">
		$(function () {
			if ($('#<%= lblError.ClientID%>').html() == "") {
				$('#errorDiv').hide();
			} else {
				$('#errorDiv').show();
			}

			/** Autocomplete для городов СТАРТ **/
			$('#<%= tbCity.ClientID%>').autocomplete({
				minChars: '2',
				params: {
					appkey: '<%= AppKey%>'
				},
			    serviceUrl: '../../../AppServices/AutocompliteAPI.asmx/GetCityForAutocompliteJSON',
				onSelect: function (suggestion) {
					$('#<%= hfCityID.ClientID%>').val(suggestion.data);
				}
			});
			/** Autocomplete для городов КОНЕЦ **/
		});
	</script>

	<h3 class="h3custom" style="margin-top: 0;"><%# ActionText %> склада</h3>
	
	<div class="loginError" style="width: 90%; margin-left: auto; margin-right: auto; margin-top: 0px;" id="errorDiv">
		<asp:Label ID="lblError" runat="server" />
	</div>

	<div class="single-input-fild" style="width: 400px">

		<div class="form-group" id="div5" runat="server">
			<label>Название склада</label>
			<asp:TextBox ID="tbName" CssClass="form-control" runat="server" width="95%"/>
		</div>
		
		<div class="form-group" id="div1" runat="server">
			<label>Населенный пункт</label>
			<asp:TextBox runat="server" ID="tbCity" CssClass="form-control" width="95%"/>
			<asp:HiddenField runat="server" ID="hfCityID"/>
		</div>

		<div class="form-group" id="div6" runat="server">
			<asp:DropDownList ID="ddlStreetPrefix" Width="50px" runat="server" CssClass="ddl-control" style="padding: 0; height: 28px;">
				<asp:ListItem text="ул." value="ул."/>
				<asp:ListItem text="аллея" value="аллея"/>
				<asp:ListItem text="бул." value="бул."/>
				<asp:ListItem text="дор." value="дор."/>
				<asp:ListItem text="линия" value="линия"/>
				<asp:ListItem text="маг." value="маг."/>
				<asp:ListItem text="мик-н" value="мик-н"/>
				<asp:ListItem text="наб." value="наб."/>
				<asp:ListItem text="пер." value="пер."/>
				<asp:ListItem text="пл." value="пл."/>
				<asp:ListItem text="пр." value="пр."/>
				<asp:ListItem text="пр-кт" value="пр-кт"/>
				<asp:ListItem text="ряд" value="ряд"/>
				<asp:ListItem text="тракт" value="тракт"/>
				<asp:ListItem text="туп." value="туп."/>
				<asp:ListItem text="ш." value="ш."/>
			</asp:DropDownList>
			<asp:TextBox ID="tbStreetName" CssClass="form-control" runat="server" width="55%"/>
			<asp:TextBox ID="tbStreetNumber" CssClass="form-control" runat="server" width="10%"/>
		</div>
		
		<div class="form-group" id="div2" runat="server">
			<label>корп.:</label>
			<asp:TextBox ID="tbHousing" CssClass="form-control" runat="server" width="10%"/>
			
			<label>кв.:</label>
			<asp:TextBox ID="tbApartmentNumber" CssClass="form-control" runat="server" width="10%"/>
		</div>
		
		<div class="form-group" id="div3" runat="server">
		</div>
		
		 <div class="form-group" style="margin-top: 10px;">
			<asp:Button ID="btnCreate" class="btn btn-default btn-right" runat="server" Text='<%# ButtonText %>' ValidationGroup="LoginGroup" style="margin-left: 30px;"/>
			<asp:Button ID="btnBack" OnClientClick="window.history.go(-1);return false;" class="btn btn-default btn-right" runat="server" Text='Назад'/>
		</div>
	</div>
	<asp:CustomValidator ID="CustomValidator1" ControlToValidate="tbName" ClientValidationFunction="validateIfEmpty" EnableClientScript="True" ValidationGroup="LoginGroup" ValidateEmptyText="True" runat="server"  Display="None" ErrorMessage="Вы не ввели название склада" ></asp:CustomValidator>
	<asp:CustomValidator ID="CustomValidator2" ControlToValidate="tbStreetName" ClientValidationFunction="validateIfEmpty" EnableClientScript="True" ValidationGroup="LoginGroup" ValidateEmptyText="True" runat="server"  Display="None" ErrorMessage="Вы не ввели название улицы" ></asp:CustomValidator>
	<asp:CustomValidator ID="CustomValidator3" ControlToValidate="tbStreetNumber" ClientValidationFunction="validateIfEmpty" EnableClientScript="True" ValidationGroup="LoginGroup" ValidateEmptyText="True" runat="server"  Display="None" ErrorMessage="Вы не ввели номер улицы" ></asp:CustomValidator>
	<asp:CustomValidator ID="CustomValidator4" ControlToValidate="tbCity" ClientValidationFunction="validateIfEmpty" EnableClientScript="True" ValidationGroup="LoginGroup" ValidateEmptyText="True" runat="server"  Display="None" ErrorMessage="Вы не ввели населенный пункт" ></asp:CustomValidator>
	<asp:ValidationSummary ID="ValidationSummary2" runat="server" ValidationGroup="LoginGroup" ShowMessageBox="true" ShowSummary="false"/>
</asp:Content>
