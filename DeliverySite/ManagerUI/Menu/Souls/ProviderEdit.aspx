<%@ Page Title="" Language="C#" MasterPageFile="~/ManagerMasterPage.master" AutoEventWireup="true" CodeBehind="ProviderEdit.aspx.cs" Inherits="Delivery.ManagerUI.Menu.Souls.ProviderEdit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <script type="text/javascript">
		$(function () {
			$("#<%= tbContactPhone.ClientID %>").mask("+375 (99) 999-99-99");
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
    <h3 class="h3custom" style="margin-top: 0;"><%= ActionText %> поставщика</h3>
    <div class="single-input-fild" style="width: 400px">
		<div class="form-group" id="div9" runat="server">
			<label>Форма собственности:</label>
			<asp:DropDownList ID="ddlNamePrefix" CssClass="ddl-control" runat="server" style="width: 100px;"/>
		</div>
        
        <div class="form-group" id="div1" runat="server">
			<label>Название</label>
			<asp:TextBox ID="tbName" runat="server" CssClass="form-control" width="100%"/>
		</div>
        
        <div class="form-group" id="div3" runat="server">
			<label>ФИО контактного лица</label>
			<asp:TextBox ID="tbContactFIO" runat="server" CssClass="form-control" width="100%"/>
		</div>
        
        <div class="form-group" id="div4" runat="server">
			<label>Контактный телефон:</label>
			<asp:TextBox ID="tbContactPhone" runat="server" CssClass="form-control" style="width: 135px;"/>
		</div>
        
        <div class="form-group" id="div5" runat="server">
			<label>Список продукции</label>
			<asp:TextBox ID="tbTypesOfProducts" runat="server" TextMode="MultiLine" CssClass="form-control" style="width: 100%; height: 60px;"/>
		</div>
        
        <div class="form-group" id="div6" runat="server">
			<label>Город</label>
			<asp:TextBox ID="tbCity" runat="server" CssClass="form-control" width="100%"/>
            <asp:HiddenField runat="server" ID="hfCityID"/>
		</div>
        
        <div class="form-group" id="div7" runat="server">
			<label>Адрес</label>
			<asp:TextBox ID="tbAddress" runat="server" CssClass="form-control" width="100%"/>
		</div>
        
        <div class="form-group" id="div8" runat="server">
			<label>Заметка</label>
			<asp:TextBox ID="tbNote" runat="server" TextMode="MultiLine" CssClass="form-control" style="width: 100%; height: 60px;"/>
		</div>
        
        <div class="form-group" id="div2" runat="server">
			<asp:Button ID="btnCreate" class="btn btn-default btn-right" runat="server" Text='Сохранить' ValidationGroup="LoginGroup" style="margin-left: 30px;"/>
            <asp:Button ID="btnBack" OnClientClick="window.history.go(-1);return false;" class="btn btn-default btn-right" runat="server" Text='Назад'/>
		</div>
    </div>

    <asp:CustomValidator ID="CustomValidator1" ControlToValidate="tbName" runat="server"  ClientValidationFunction="validateIfEmpty" EnableClientScript="True" ValidationGroup="LoginGroup" ValidateEmptyText="True" Display="None" ErrorMessage="Вы не ввели название поставщика"></asp:CustomValidator>
    <asp:CustomValidator ID="CustomValidator2" ControlToValidate="tbContactFIO" runat="server"  ClientValidationFunction="validateIfEmpty" EnableClientScript="True" ValidationGroup="LoginGroup" ValidateEmptyText="True" Display="None" ErrorMessage="Вы не ввели контактное лицо поставщика"></asp:CustomValidator>
    <asp:CustomValidator ID="CustomValidator3" ControlToValidate="tbTypesOfProducts" runat="server"  ClientValidationFunction="validateIfEmpty" EnableClientScript="True" ValidationGroup="LoginGroup" ValidateEmptyText="True" Display="None" ErrorMessage="Вы не ввели продукцию поставщика"></asp:CustomValidator>
    <asp:CustomValidator ID="CustomValidator4" ControlToValidate="tbContactPhone" runat="server"  ClientValidationFunction="validateIfEmpty" EnableClientScript="True" ValidationGroup="LoginGroup" ValidateEmptyText="True" Display="None" ErrorMessage="Вы не ввели контактный телефон поставщика"></asp:CustomValidator>
    <asp:CustomValidator ID="CustomValidator5" ControlToValidate="tbAddress" runat="server"  ClientValidationFunction="validateIfEmpty" EnableClientScript="True" ValidationGroup="LoginGroup" ValidateEmptyText="True" Display="None" ErrorMessage="Вы не ввели адрес поставщика"></asp:CustomValidator>
    <asp:ValidationSummary ID="ValidationSummary1" runat="server" ValidationGroup="LoginGroup" ShowMessageBox="true" ShowSummary="false"/>
</asp:Content>
