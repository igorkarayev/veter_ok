<%@ Page Title="" Language="C#" MasterPageFile="~/ManagerMasterPage.master" AutoEventWireup="true" CodeBehind="~/ManagerUI/Menu/Tickets/UserTicketEdit.aspx.cs" Inherits="Delivery.ManagerUI.Menu.Tickets.UserTicketEdit" %>
<%@ Import Namespace="Delivery.BLL" %> <%@ Import Namespace="Delivery.BLL.StaticMethods" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
	<style>
		/* #234 */
		 .autocomplete-suggestions {
			 width: 700px !important;
		 }

         .pointer {
             cursor: pointer;
             border-bottom: 1px dotted;
         }
	</style>
    <script>
        showEdit = function () {
            if ($('#changeDate span').css('display') == 'none')
                $('#changeDate span, #changeDate input').show();
            else
                $('#changeDate span, #changeDate input').hide();
        }
    </script>
	<div id="info"></div>
	<div class="loginError" id="errorDiv" style="width: 90%; display: none;">
		<asp:Label runat="server" ID="lblError" ForeColor="White"></asp:Label>
	</div>

	<div style="display: inline-block; width: 48%; float: left; margin-right: 2%">
		<h3 class="h3custom">Общая информация:</h3>
		<table class='<%= string.Format("tableClass {0}",OtherMethods.SpecialClientTrClass(SpecialClient.ToString()))%>' style="margin-left: 30px; margin-bottom: 30px">
			<tr>
				<td class="valignTable" style="width: 150px;">
					<span>ID:</span>
					<asp:HiddenField ID="hfID" runat="server"/>
				</td>
				<td class="paddingTable">
					<asp:Label runat="server" ID="lblID" Width="70%"/>
					<asp:HiddenField runat="server" ID="hfUserID"/>
					<asp:HiddenField runat="server" ID="hfUserApiKey"/>
				</td>
			</tr>
			<tr>
				<td class="valignTable">
					<span>Дата создания:</span>
				</td>
				<td class="paddingTable">
					<asp:Label ID="lblCreateDate" runat="server" Width="70px"/>
				</td>
			</tr>
			<tr>
				<td class="valignTable">
					<span>Дата приема:</span>
				</td>
				<td class="paddingTable">
					<asp:Label ID="lblAdmissionDate" runat="server"/>
					<asp:HiddenField runat="server" ID="hfAdmissionDate"/>
				</td>
			</tr>
			<tr>
				<td class="valignTable">
					<span>Дата отправки:</span>
				</td>
				<td class="paddingTable">
					<asp:Label ID="lblDeliveryDateStatic" runat="server" CssClass="pointer" onclick="showEdit()" title="Изменить дату отправки"/>
				</td>
			</tr>
			<tr runat="server" ID="trUserAccountData">
				<td class="valignTable">
					<span>Пользователь:</span>
				</td>
				<td class="paddingTable">
				   <asp:HyperLink runat="server" ID="hlUser" Text="Пользователь" NavigateUrl="~/UserUI/Menu/Souls/ProfileView.aspx"></asp:HyperLink>
				</td>
			</tr>
			<tr runat="server" ID="trUserProfileData">
				<td class="valignTable">
					<span>Профиль:</span>
				</td>
				<td class="paddingTable">
					<asp:HyperLink runat="server" ID="hlProfile" /> <i style="font-size: 10px;">(<asp:Label runat="server" ID="lblProfileType"></asp:Label>)</i>
					<asp:HiddenField runat="server" ID="hfUserProfileType"/>
					<asp:HiddenField runat="server" ID="hfUserDiscount"/>
				</td>
			</tr>
			<tr>
				<td class="valignTable">
					<span>Направление:</span>
				</td>
				<td class="paddingTable">
					<asp:Label runat="server" ID="lblTrack" /> <asp:DropDownList runat="server" ID="ddlUserTrack" Visible="False"/>
				</td>
			</tr>
			
			<tr>
				<td class="valignTable">
					<span>Обратка:</span><br/>
					<span>Без оплаты:</span>
				</td>
				<td class="paddingTable">
					<asp:Label ID="lblIsExchange" runat="server"/><br/>
                    <asp:CheckBox runat="server" ID="cbWithoutMoney" />
				</td>
			</tr>
			
			<tr>
				<td class="valignTable">
					<span>Наличие накл.:</span><br/>
					<span>Печать в накл.:</span>
				</td>
				<td class="paddingTable">
					<asp:Label ID="lblNN" runat="server"/><br/>
					<asp:Label ID="lblPN" runat="server"/>
				</td>
			</tr>
			
			<tr runat="server" id="tdComment">
				<td class="valignTable">
					<span>Комментарии:</span>
				</td>
				<td class="paddingTable" style="font-style: italic">
					<asp:Label ID="lblComment" runat="server"/>
				</td>
			</tr>
		</table>
	</div>

	<div style="display: inline-block; width: 49%; float: left">
		<h3 class="h3custom">Информация, заполняемая менеджером:</h3>
		 <table class="tableClass" style="margin-left: 30px;">
			<tr>
				<td class="valignTable">
					<span>Согласованная стоимость:</span>
				</td>
				<td class="paddingTable">
					<asp:TextBox ID="tbAgreedCost" runat="server" Width="70px" CssClass="moneyMask form-control"/> руб.
				</td>
			</tr>
			<tr>
				<td class="valignTable">
					<span>Стоимость за услугу:</span>
				</td>
				<td class="paddingTable">
					<asp:TextBox ID="tbGruzobozCost" runat="server" Width="70px" CssClass="moneyMask form-control"/> руб.
				</td>
			</tr>
			 <tr>
				<td class="valignTable">
					<span>Обнулить согласованную:</span>
				</td>
				<td class="paddingTable">
					<asp:CheckBox ID="cbAgreedCostIsNull" runat="server" />
				</td>
			</tr>
			 <tr>
				<td class="valignTable">
					<span>Обнулить за доставку:</span>
				</td>
				<td class="paddingTable">
					<asp:CheckBox ID="cbDeliveryCostIsNull" runat="server"/>
				</td>
			</tr>
			<tr>
				<td class="valignTable">
					<span>Водитель:</span>
				</td>
				<td class="paddingTable">
					<asp:DropDownList runat="server" ID="ddlDrivers" CssClass="multi-control"/>
					<asp:HiddenField ID="hfDriverID" runat="server"/>
				</td>
			</tr>
			<tr>
				<td class="valignTable">
					<span>Статус:</span>
				</td>
				<td class="paddingTable">
					<asp:DropDownList ID="ddlStatus" runat="server" Width="150px" CssClass="multi-control"/>
					<asp:HiddenField ID="hfStatusID" runat="server"/>
					<asp:HiddenField ID="hfStatusIDOld" runat="server"/>
					<asp:HiddenField ID="hfStatusDescription" runat="server"/>
				</td>
			</tr>
			<tr>
				<td class="valignTable">
					<asp:Label ID="lblStatusDescription" runat="server" CssClass="notVisible">Расшифровка статуса:</asp:Label>
				</td>
				<td class="paddingTable">
					<asp:TextBox ID="tbStatusDescription" runat="server" CssClass="notVisible multi-control" width="100%" TextMode="MultiLine" Rows="4" Columns="40"/>
				</td>
			</tr>
			<tr id="changeDate">
				<td class="valignTable">
					<asp:Label runat="server" ID="lblDeliveryDate" CssClass="notVisible">Введите новую дату отправки:</asp:Label>
				</td>
				<td class="paddingTable">
					<asp:TextBox ID="tbDeliveryDate" runat="server" Width="70px" CssClass="form-control notVisible"/>
				</td>
			</tr>
		</table>    
	</div>
	
	
	<div style="clear:both">
		<h3 class="h3custom" id="calculatehref">Информация, заполняемая пользователем:</h3>
	</div>
	<div class="block2" style="width: 48%; min-height: 350px; display: inline-block; vertical-align: top; border-right: 3px solid #EBEEF2; padding-top: 10px;">
		<table class="tableClass" style="padding-bottom: 10px;">
			<tr>
				<td class="valignTable">
					<span>Грузы (что перевозим)</span>
				</td>
			</tr> 
		</table>  

		<div class="goods" style="padding-left: 30px; font-size: 12px;">
			<asp:Panel runat="server" ID="pnlBooks"></asp:Panel>
			<asp:HiddenField runat="server" ID="hfHowManyControls"/>
			<asp:HiddenField runat="server" ID="hfFullSecureID"/>
			<asp:Label runat="server" ID="lblOldGoods" Visible="False"></asp:Label>
		</div>   

		<table class="tableClass">            
			<tr>
				<td class="valignTable paddingTable" style="width: 150px;">
					<span>Оценочная стоимость:</span>
				</td>
				<td class="valignTable paddingTable" style="padding-left: 7px; font-weight: bold">
					<asp:Label ID="lblAssessedCost" runat="server"/> <i>руб.</i>
					<asp:HiddenField runat="server" ID="hfAssessedCost"/>
				</td>
			</tr>

			<!--tr>
				<td class="valignTable" style="height: 45px; padding-top: 3px;">
					<span>Доставка для получателя платная?</span>
				</td>
				<td style="vertical-align: top;">
					<table>
						<tr>
							<td style="vertical-align: top; height: 15px">
								<asp:CheckBox ID="cbIsDeliveryCost" runat="server" Enabled="False"/>&nbsp;&nbsp;
							</td>
							<td>
								<div class="deliveryCost goodsTable" style="display: inline-block; width: 240px">
									<span>за доставку:</span> &nbsp;&nbsp; 
									<asp:TextBox ID="tbDeliveryCost" runat="server" Width="25%" Enabled="False" CssClass="moneyMask form-control"/> руб.
								</div>
							</td>
						</tr>
					</table>               
				</td>
			</tr--> 

			<tr>
				<td class="valignTable">
					<span>Количество коробок:</span>
				</td>
				<td class="paddingTable">
					<asp:TextBox ID="tbBoxesNumber" runat="server" CssClass="form-control" Width="25px" Enabled="False"/> шт.
				</td>
			</tr>
			
			<tr>
				<td class="valignTable">
					<span class="ttn">ТТН данные:</span>
				</td>
				<td class="paddingTable">
					<span class="ttn">ТТН серия</span> <asp:TextBox ID="tbTtnSeria" runat="server" width="15px" CssClass="form-control" Enabled="False"/>
					<span class="ttn">ТТН номер</span> <asp:TextBox ID="tbTtnNumber" runat="server" width="50px" CssClass="form-control" Enabled="False"/>
				</td>
			</tr>

			<tr>
				<td class="valignTable">
					<span class="ttn">Другие документы:</span>
				</td>
				<td class="paddingTable">
					<asp:TextBox CssClass="ttn form-control" ID="tbOtherDocuments" runat="server" width="95%" TextMode="MultiLine" Rows="3" Columns="40" Enabled="False"/>
				</td>
			</tr>
		</table>

	</div>
	
	
	

	<div class="block3" style="width: 48%; display: inline-block; vertical-align: top; padding-left: 20px;  padding-top: 10px;">
		<table class="tableClass">
			
			<tr>
				<td class="paddingTable valignTable citybox" style="width: 100%;" colspan="2">
					<div style="margin-bottom: 10px;">
						<span>Отправляем из (нас. пункт):</span>
					</div>
					<asp:TextBox runat="server" ID="tbSenderCity" Width="97%" CssClass="form-control"/>
					<asp:HiddenField runat="server" ID="hfSenderCityID"/>
				</td>
			</tr>
			<tr>
				<td class="valignTable" style="padding-top: 7px;">
					<span>Адрес отправки:</span>
				    <asp:HiddenField runat="server" ID="hfWharehouse" Value=""/>
				</td>
				<td class="paddingTable">
					<table>
						<tr>
							<td>
								<asp:DropDownList ID="ddlSenderStreetPrefix" Width="50px" runat="server" CssClass="ddl-control" style="padding: 0; height: 28px;">
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
							</td>
							<td>
								<asp:TextBox ID="tbSenderStreetName" runat="server" width="130px" CssClass="form-control"/>
								<span>дом: </span><asp:TextBox ID="tbSenderStreetNumber" runat="server" width="25px" CssClass="form-control"/>
							</td>
						</tr>
						<tr>
							<td style="width: 50px;">
								<span>корпус:</span>
							</td>
							<td>
								<asp:TextBox ID="tbSenderHousing" runat="server" width="15px" CssClass="form-control"/>
								   <span>квартира: </span><asp:TextBox ID="tbSenderApartmentNumber" runat="server" width="25px" CssClass="form-control"/>
							</td>
						</tr>
					</table>
				</td>
			</tr>

			

			<tr>
				<td class="paddingTable valignTable citybox" style="width: 100%; border-top: 3px solid #ebeef2; padding-top: 10px;" colspan="2">
					<div style="margin-bottom: 10px;">Отправляем в (нас. пункт):</div>
					<asp:TextBox runat="server" ID="tbCity" Width="97%" CssClass="form-control"/>
					<div class="notVisible" id="surcharge">
						доплата за отклонение:
						<asp:Label runat="server" style="font-weight: bold; margin-right: 20px;" ID="lblCityCost"/>
					</div>
					<div class="notVisible" id="delivery-days">
						дни доставки: <asp:Label runat="server" style="font-weight: bold;" ID="lblCityDeliveryDate"/>
					</div>
					<div class="notVisible" id="delivery-terms">
						срок доставки:
						<asp:Label runat="server" style="font-weight: bold; margin-right: 20px;" ID="lblCityDeliveryTerms"/>
					</div>
					<asp:HiddenField runat="server" ID="hfCityID"/>
				</td>
			</tr> 
			<tr>
				<td class="valignTable" style="padding-top: 7px;">
					<span>Адрес получателя:</span>
				</td>
				<td class="paddingTable">
					<table>
						<tr>
							<td>
								<asp:DropDownList ID="ddlRecipientStreetPrefix" Width="50px" runat="server" CssClass="ddl-control" style="padding: 0; height: 28px;">
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
							</td>
							<td>
								<asp:TextBox ID="tbRecipientStreet" runat="server" width="150px" CssClass="form-control" Enabled="False"/>
								<span>дом: </span><asp:TextBox ID="tbRecipientStreetNumber" runat="server" width="25px" Enabled="False"/>
							</td>
						</tr>
						<tr>
							<td style="width: 50px;">
								<span>корпус:</span>
							</td>
							<td>
								<asp:TextBox ID="tbRecipientKorpus" runat="server" width="25px" CssClass="form-control" Enabled="False"/>
								   <span>квартира: </span><asp:TextBox ID="tbRecipientKvartira" runat="server" width="25px" Enabled="False"/>
							</td>
						</tr>
					</table>
				</td>
			</tr>
			
			
			

			<tr>
				<td class="valignTable">
					<span>Фамилия получателя:</span>
				</td>
				<td class="paddingTable">
					<asp:TextBox ID="tbRecipientFirstName" CssClass="form-control" runat="server" width="95%"/>
				</td>
			</tr>
			<tr>
				<td class="valignTable">
					<span>Имя получателя:</span>
				</td>
				<td class="paddingTable">
					<asp:TextBox ID="tbRecipientLastName" CssClass="form-control" runat="server" width="95%"/>
				</td>
			</tr>
			<tr>
				<td class="valignTable">
					<span>Отчество получателя:</span>
				</td>
				<td class="paddingTable">
					<asp:TextBox ID="tbRecipientThirdName" CssClass="form-control" runat="server" width="95%"/>
				</td>
			</tr>
			<tr>
				<td class="valignTable">
					<span>Паспортные данные:</span>
				</td>
				<td class="paddingTable">
					серия <asp:TextBox ID="tbPassportSeria" CssClass="form-control" runat="server" width="17px" Enabled="False"/>
					номер <asp:TextBox ID="tbPassportNumber" CssClass="form-control" runat="server" width="50px" Enabled="False"/>
				</td>
			</tr>
			
			<tr>
				<td class="valignTable">
					<span>Телефоны получателя:</span>
				</td>
				<td class="paddingTable" style="padding-left: 10px;">
					<div style="padding-bottom: 5px;">
						#1: <asp:TextBox ID="tbRecipientPhone" runat="server" CssClass="form-control" Width="120px" Enabled="False"/>
					</div>
					<div>
					   #2: <asp:TextBox ID="tbRecipientPhone2" runat="server" CssClass="form-control" Width="120px" Enabled="False"/> 
					</div>
				</td>
			</tr>
			<tr>
				<td class="valignTable paddingTable" style="width: 150px;">
					<a href="#calculatehref" onclick="calculate();" >Сколько за услугу?</a><br/>
					<span style="font-size: 10px; color: olive">скидка: <%= MoneyMethods.UserDiscount(UserID) %>%</span>
				</td>
				<td class="valignTable paddingTable" style="padding-left: 7px; font-weight: bold; vertical-align: top">
					<span id="calculate"></span>
				</td>
			</tr>
		</table>
	</div>
	
	
	

	<div class="block4" style="border-top: 3px solid #EBEEF2; padding-top: 10px; margin-bottom: 10px;">
	   <table class="tableClass" style="width:100%"> 
			<tr>
				<td class="valignTable" style="width: 100px">
					<span>Примечания:</span>
				</td>
				<td colspan="2">
					<asp:TextBox ID="tbNote" runat="server" CssClass="multi-control" width="450px" TextMode="MultiLine" Rows="4" Columns="40" Enabled="False"/>
				</td>
			</tr>        
		</table>
	</div>
	
	<div style="text-align: right;">
		<asp:Button ID="btnBack" OnClientClick="window.history.go(-1);return false;" CssClass="btn btn-default" runat="server" Text='Назад'/>
		<asp:Button ID="btnCreate" runat="server" CssClass="btn btn-default" Text="Сохранить" ValidationGroup="LoginGroup"/>
		<asp:Button ID="btnPrint" runat="server" CssClass="btn btn-default" Text='Печать'/>
		<asp:Button ID="btnPrintZP" runat="server" CssClass="btn btn-default" Text='Печать заказ-поручений'/>
		<asp:Button ID="btnDelete" runat="server" OnClientClick="return confirm('Вы уверены?');" CssClass="btn btn-default" Text='Удалить'/>
	</div>
	
	<asp:CustomValidator ID="CustomValidator12" ControlToValidate="tbCity" ClientValidationFunction="validateCityTb" EnableClientScript="True" ValidationGroup="LoginGroup" ValidateEmptyText="True" runat="server"  Display="None" ErrorMessage="Вы не выбрали город" ></asp:CustomValidator>
	
	<asp:ValidationSummary ID="ValidationSummary1" runat="server" ValidationGroup="LoginGroup" ShowMessageBox="true" ShowSummary="false"/>
	
	<script type="text/javascript">
		/** ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~ Объявление переменных ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~ **/

		/** JSON-наименований **/
		var availableTitles = [
			<%= AvailableTitles %>
		];


		/** ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~ Код, выполняемый после загрузки страницы ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~ **/

        $(function () {
            $.datepicker.setDefaults($.datepicker.regional['ru']);
            $("#<%= tbRecipientPhone.ClientID %>").mask("+375 (99) 999-99-99");
            $("#<%= tbRecipientPhone2.ClientID %>").mask("+375 (99) 999-99-99");
            $(".moneyMask").maskMoney();
            $("#<%= tbDeliveryDate.ClientID %>").datepicker({ nextText: "", prevText: "", changeMonth: true, changeYear: true }).mask("99-99-9999");


            /** Autocomplete для городов СТАРТ **/
            $('#<%= tbCity.ClientID%>').autocomplete({
                minChars: '2',
                params: {
                    appkey: '<%= AppKey%>'
                },
                serviceUrl: '../../../AppServices/AutocompliteAPI.asmx/GetCityForAutocompliteJSON',
                onSelect: function (suggestion) {
                    $('#<%= hfCityID.ClientID%>').val(suggestion.data);
                    $('#<%= lblCityCost.ClientID%>').html(suggestion.cost);
                    $('#<%= lblCityDeliveryDate.ClientID%>').html(suggestion.deliverydate);
                    $('#<%= lblCityDeliveryTerms.ClientID%>').html(suggestion.deliveryterms);

                    if ($('#<%= lblCityCost.ClientID%>').html().length != 0) {
                        $('#surcharge').show();
                        $('#<%= lblCityCost.ClientID%>').show();
                    } else {
                        $('#surcharge').hide();
                        $('#<%= lblCityCost.ClientID%>').hide();
                    }

                    if ($('#<%= lblCityDeliveryDate.ClientID%>').html().length != 0) {
                        $('#delivery-days').show();
                        $('#<%= lblCityDeliveryDate.ClientID%>').show();
                    } else {
                        $('#delivery-days').hide();
                        $('#<%= lblCityDeliveryDate.ClientID%>').hide();
                    }

                    if ($('#<%= lblCityDeliveryTerms.ClientID%>').html().length != 0) {
                        $('#delivery-terms').show();
                        $('#<%= lblCityDeliveryTerms.ClientID%>').show();
                    } else {
                        $('#delivery-terms').hide();
                        $('#<%= lblCityDeliveryTerms.ClientID%>').hide();
                    }

                    if ($('#<%= tbCity.ClientID%>').val().trim().length == 0 || $("#<%= hfCityID.ClientID%>").val().length == 0) {
                        $('#<%= tbCity.ClientID%>').css("background-color", "#ffdda8");
                    } else {
                        $('#<%= tbCity.ClientID%>').css("background-color", "white");
                    }
                }
            });

            $('#<%= tbSenderCity.ClientID%>').autocomplete({
                minChars: '2',
                params: {
                    appkey: '<%= AppKey%>'
                },
                serviceUrl: '../../../AppServices/AutocompliteAPI.asmx/GetCityForAutocompliteJSON',
                onSelect: function (suggestion) {
                    $('#<%= hfSenderCityID.ClientID%>').val(suggestion.data);
                }
            });

            if ($('#<%= lblCityCost.ClientID%>').html().length != 0) {
                $('#surcharge').show();
                $('#<%= lblCityCost.ClientID%>').show();
            } else {
                $('#surcharge').hide();
                $('#<%= lblCityCost.ClientID%>').hide();
            }

            if ($('#<%= lblCityDeliveryDate.ClientID%>').html().length != 0) {
                $('#delivery-days').show();
                $('#<%= lblCityDeliveryDate.ClientID%>').show();
            } else {
                $('#delivery-days').hide();
                $('#<%= lblCityDeliveryDate.ClientID%>').hide();
            }

            if ($('#<%= lblCityDeliveryTerms.ClientID%>').html().length != 0) {
                $('#delivery-terms').show();
                $('#<%= lblCityDeliveryTerms.ClientID%>').show();
            } else {
                $('#delivery-terms').hide();
                $('#<%= lblCityDeliveryTerms.ClientID%>').hide();
            }
            /** Autocomplete для городов КОНЕЦ **/


            /** Условия для показа и сокрытия расшифровки статуса **/
            if ($('#<%= ddlStatus.ClientID %>').val() == "4" || $('#<%= ddlStatus.ClientID %>').val() == "7" ||
                $('#<%= ddlStatus.ClientID %>').val() == "8" || $('#<%= ddlStatus.ClientID %>').val() == "9" ||
                $('#<%= ddlStatus.ClientID %>').val() == "10" || $('#<%= ddlStatus.ClientID %>').val() == "11") {
                $("#<%= tbStatusDescription.ClientID %>").show();
                $("#<%= lblStatusDescription.ClientID %>").show();
            } else {
                $("#<%= tbStatusDescription.ClientID %>").hide();
                $("#<%= lblStatusDescription.ClientID %>").hide();
            }

            $('#<%= ddlStatus.ClientID %>').change(function () {
                if ($(this).val() == "4" || $(this).val() == "7" ||
                    $(this).val() == "8" || $(this).val() == "9" ||
                    $(this).val() == "10" || $(this).val() == "11") {
                    $("#<%= tbStatusDescription.ClientID %>").show();
                    $("#<%= lblStatusDescription.ClientID %>").show();
                } else {
                    $("#<%= tbStatusDescription.ClientID %>").hide();
                    $("#<%= lblStatusDescription.ClientID %>").hide();
                }
            });


            /** Условия для показа и сокрытия текстбокса "Дата переноса" **/
            if ($('#<%= ddlStatus.ClientID %>').val() == "4" || $('#<%= ddlStatus.ClientID %>').val() == "11") {
                $("#<%= tbDeliveryDate.ClientID %>").show();
                $("#<%= lblDeliveryDate.ClientID %>").show();
            } else {
                $("#<%= tbDeliveryDate.ClientID %>").hide();
                $("#<%= lblDeliveryDate.ClientID %>").hide();
            }

            $('#<%= ddlStatus.ClientID %>').change(function () {
                if ($(this).val() == "4" || $(this).val() == "11") {
                    $("#<%= tbDeliveryDate.ClientID %>").show();
                    $("#<%= lblDeliveryDate.ClientID %>").show();
                } else {
                    $("#<%= tbDeliveryDate.ClientID %>").hide();
                    $("#<%= lblDeliveryDate.ClientID %>").hide();
                }
            });


            /** Условия для отображения особых полей в зависимости от типа профиля **/
            if ($('#<%= hfUserProfileType.ClientID %>').val() == "2" || $('#<%= hfUserProfileType.ClientID %>').val() == "3") {
				$("#<%= tbTtnNumber.ClientID %>").show();
				$("#<%= tbTtnSeria.ClientID %>").show();
				$(".ttn").show();
			} else {
				$("#<%= tbTtnNumber.ClientID %>").hide();
				$("#<%= tbTtnSeria.ClientID %>").hide();
				$(".ttn").hide();
			}


			/** Условия для отображение блока "За доставку" **/
			if ($("#<%= cbIsDeliveryCost.ClientID %>").attr('checked') == "checked") {
				$(".deliveryCost").show();
			} else {
				$(".deliveryCost").hide();
			}

			$("#<%= cbIsDeliveryCost.ClientID %>").click(function () {
				if (this.checked) {
					$(".deliveryCost").show();
				} else {
					$(".deliveryCost").hide();
				}
			});

			/** Условия для отображение блока ошибок **/
			if ($('#<%= lblError.ClientID%>').html() == "") {
				$('#errorDiv').hide();
			} else {
				$('#errorDiv').show();
			}

			/** Запуск расчета оценочной стоимости, если она равна нулю СТАРТ **/
			var goodsNumber = $("#<%= hfHowManyControls.ClientID%>").val();
			var lblAssessedCost = $("#<%= lblAssessedCost.ClientID%>").html();
			if (lblAssessedCost.trim() == "" || lblAssessedCost.trim() == "0") {
				overAssessedCost(goodsNumber);
			}
			/** Запуск расчета оценочной стоимости, если она равна нулю КОНЕЦ **/
		});



		/** ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~ Функции ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~ **/

		/** Подсчета оценочной стоимости "на лету" СТАРТ **/
		function overAssessedCost(numberOfGoods) {
			var overCost = parseFloat("0");
			for (var i = 1; i <= numberOfGoods; i++) {
				var goodsCostValue = $("#ctl00_MainContent_tbGoodsCost" + i).val();
				if (goodsCostValue.trim() == "") {
					goodsCostValue = "0";
				}

				var goodsNumberValue = $("#ctl00_MainContent_tbGoodsNumber" + i).val();
				if (goodsNumberValue.trim() == "") {
					goodsNumberValue = "0";
				}

				var goodCost = parseFloat(goodsCostValue.replace(/\s/g, ""));
				var goodNumber = parseFloat(goodsNumberValue.replace(/\s/g, ""));
				var overGoodCost = goodCost * goodNumber;
				overCost += overGoodCost;
			}
			$("#<%=lblAssessedCost.ClientID%>").html(overCost);
			$("#<%=hfAssessedCost.ClientID%>").val(overCost);
			$("#<%= lblAssessedCost.ClientID%>").html($("#<%= lblAssessedCost.ClientID%>").html().replace(/(\d)(?=(\d\d\d)+([^\d]|$))/g, '$1 '));
		}
		/** Подсчета оценочной стоимости "на лету" КОНЕЦ **/


		/** Cчетчик символов СТАРТ **/
		function counterDescription(i) {
			var idFull = "#ctl00_MainContent_tbGoodsDescription" + i;
			var counterID = "#counter" + i;
			var number = 100 - $(idFull).val().length;
			$(counterID).html(number);
		}

		function counterModel(i) {
			var idFull = "#ctl00_MainContent_tbGoodsModel" + i;
			var counterID = "#counter" + i;
			var number = 50 - $(idFull).val().length;
			$(counterID).html(number);
		}
		/** Cчетчик символов КОНЕЦ **/
		

		/** Подсчет стоимости "За услугу" СТАРТ **/
		function calculate() {
			$.ajax({
				type: "POST",
				dataType: "xml",
				url: "../../../WebServices/UserAPI/CalculatorAPI.asmx/Calculate",
				data: ({
					userid: '1',
					apikey: '<%= FirstUserApiKey %>',
					jsonString: GetJSONString()
				}),
				success: function (xml) {
					$(xml).find('string').each(function () {
						var jsonReturnedData = $(this).text().replace(/(\d)(?=(\d\d\d)+([^\d]|$))/g, '$1 ');
						$('#calculate').html(CalculatorApiReturnValueToLocalString(jsonReturnedData));
					});

				},
				error: function (result) {
					$('#calculate').html("Ошибка сети. Попробуйте повторить действие через несколько минут.");
				}
			});
		}
		/** Подсчет стоимости "За услугу" КОНЕЦ **/


		/** Формирование JSON для получения стоимости "За услугу" СТАРТ **/
		function GetJSONString() {
			var goodsNumber = $("#<%= hfHowManyControls.ClientID%>").val();

			var calculateObject = {};
			calculateObject.goods = [];
			calculateObject.city_id = $('#<%= hfCityID.ClientID%>').val();
			calculateObject.profile_type = $('#<%= hfUserProfileType.ClientID%>').val();
		    calculateObject.user_discount = $('#<%= hfUserDiscount.ClientID%>').val();

		    var wh = $('#<%= hfWharehouse.ClientID%>').val();
		    calculateObject.iswharehouse = wh && wh != "" && wh !== "0";
			if ($('#<%= hfUserProfileType.ClientID%>').val().substring(0, 1) === "3") {
				calculateObject.assessed_cost = $('#<%= hfAssessedCost.ClientID%>').val();
			}
			calculateObject.user_id = $('#<%= hfUserID.ClientID%>').val();

			for (var i = 1; i <= goodsNumber; i++) {
				var godsForApi = {};
				godsForApi.description = $("#ctl00_MainContent_tbGoodsDescription" + i).val();
				godsForApi.number = $("#ctl00_MainContent_tbGoodsNumber" + i).val();
				godsForApi.without_akciza = $("#ctl00_MainContent_hfWithoutAkciza" + i).val();
				godsForApi.description = godsForApi.description.replace("б/у", "")
					.replace("б/y", "")
					.replace("б\\у", "")
					.replace("б\\y", "")
					.replace("б.у.", "")
					.replace("б.y.", "")
					.replace("б.у", "")
					.replace("б.y", "");
				calculateObject.goods.push(godsForApi);
			}
			return JSON.stringify(calculateObject);
		}
		/** Формирование JSON для получения стоимости "За услугу" СТАРТ **/


		/** Валидаторы СТАРТtbDeliveryDate**/
		function validateCityTb(sender, args) {
			var isValid;
			if (args.Value.trim().length == 0 || $("#<%= hfCityID.ClientID%>").val().length == 0) {
				isValid = false;
			} else {
				isValid = true;
			}

			var object = sender.controltovalidate;
			if (!isValid) {
				$("#" + object).css("background-color", "#ffdda8");
			} else {
				$("#" + object).css("background-color", "white");
			}

			args.IsValid = isValid;
		}
		/** Валидаторы КОНЕЦ**/
	</script>
</asp:Content>
