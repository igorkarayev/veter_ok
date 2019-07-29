<%@ Page Title="" Language="C#" MasterPageFile="~/UserMasterPage.master" AutoEventWireup="true" CodeBehind="TicketEdit.aspx.cs" Inherits="Delivery.UserUI.TicketEdit" Async="true"%>
<%@ Import Namespace="Delivery.BLL.Helpers" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <style>
        /* #234 */
         .autocomplete-suggestions {
             width: 700px !important;
         }

         .noTtn {

         }

         .onlyYur {

         }
    </style>
    <h3 class="h3custom" style="margin-top: 0;"><%= ActionText %> заявки <%= TicketSecureID %></h3>
    <asp:HiddenField runat="server" ID="hfTicketSecureID"/>
    <div id="info"></div>
    <div class="loginError" id="errorDiv" style="width: 90%; display: none;">
        <asp:Label runat="server" ID="lblError" ForeColor="White"></asp:Label>
    </div>

    <div class="block1" style="border-bottom: 3px solid #EBEEF2; padding-bottom: 10px;">
        <table class="tableClass"  style="width: 100%;">
            <tr>
                <td class="valignTable" style="width: 140px;">
                    <span>Выберите профиль:</span>
                </td>
                <td style="width: 30px;">&nbsp;</td>
                <td class="valignTable">
                    <div class="custom-combobox">
                        <asp:TextBox runat="server" ID="tbUserProfile" onfocus="profileOnClick();" CssClass="custom-combobox-input"/>
                        <a runat="server" class="custom-combobox-button" onclick="profileAutocomplite();" ID="btnUserProfileChoise">▼</a>
                    </div>
                    <asp:HiddenField runat="server" ID="hfUserProfileID"/>
                    <asp:HiddenField runat="server" ID="hfUserID"/>
                    <asp:HiddenField runat="server" ID="hfUserDiscount"/>
                </td>
                <td style="text-align: right; font-weight: bold; font-size: 14px;">
                    <asp:HyperLink runat="server" ID="hlFeedbackNewCategory" NavigateUrl="~/UserUI/FeedbackCreate.aspx?type=new_category">заявка на добавление нового наименования</asp:HyperLink>
                </td>
            </tr>
        </table>
    </div> 
    
    
    

    <div class="block2" style="width: 48%; min-height: 350px; display: inline-block; vertical-align: top; padding-top: 10px;">
        <table class="tableClass" style="padding-bottom: 10px;">
            <tr>
                <td class="valignTable">
                    <span>Грузы</span>
                </td>
            </tr> 
        </table>  
         
        <div class="goods" style="padding-left: 30px; font-size: 12px;">
            <asp:Panel runat="server" ID="pnlGoods"></asp:Panel>
            <div style="text-align: right; padding-right: 30px;">
                <asp:LinkButton runat="server" Text="убрать" ForeColor="Red" OnClick="btnDeleteLast_CLick" ID="btnDeleteLast" Visible="False"></asp:LinkButton>&nbsp;&nbsp;
                <asp:LinkButton runat="server" Text="добавить" ForeColor="Green" OnClick="btnMore_CLick" ID="btnMore"></asp:LinkButton>
            </div>
            <asp:HiddenField runat="server" ID="hfHowManyControls"/>
            <asp:Label runat="server" ID="lblOldGoods" Visible="False"></asp:Label>
        </div>    
        

        <table class="tableClass">            
            <tr>
                <td class="valignTable paddingTable" style="width: 150px;">
                    <span>Оценочная стоимость:</span>
                </td>
                <td class="valignTable paddingTable" style="padding-left: 7px; font-weight: bold">
                    <asp:Label ID="lblAssessedCost" runat="server"/> <i>руб.</i>
                    <asp:Label ID="lblAssessedCostCoints" runat="server"/> <i>коп.</i>
                    <asp:HiddenField runat="server" ID="hfAssessedCost"/>
                </td>
            </tr>
            
            <!--tr class="onlyYur">
				<td class="valignTable" style="height: 45px; padding-top: 3px;">
					<span>Доставка для получателя платная?</span>
				</td>
				<td style="vertical-align: top;">
					<table>
						<tr>
							<td style="vertical-align: top; height: 15px">
								<asp:CheckBox ID="cbIsDeliveryCost" runat="server" Enabled="True"/>&nbsp;&nbsp;
							</td>
							<td>
								<div class="deliveryCost goodsTable" style="display: inline-block; width: 240px">
									<span>за доставку:</span> &nbsp;&nbsp; 
									<asp:TextBox ID="tbDeliveryCost" runat="server" Width="25%" Enabled="True" Text="0,00" CssClass="moneyMask form-control"/> руб.
								</div>
							</td>
						</tr>
					</table>               
				</td>
			</tr--> 
            <tr class="noTtn" style="display: none;">
                <td>
                    <span>Кредитные документы </span>
                </td>
                <td style="padding-left: 4px;">
                    <asp:CheckBox ID="cbIsCreditDocuments" runat="server" Enabled="True"/>
                </td>
            </tr>
            <tr>
                <td class="valignTable">
                    <span>Количество коробок:</span>
                </td>
                <td class="paddingTable">
                    <asp:TextBox ID="tbBoxesNumber" runat="server" Width="25px" CssClass="form-control"/> шт.
                </td>
            </tr>
            
            <tr>
                <td class="valignTable">
                    <span class="ttn">ТТН данные:</span>
                </td>
                <td class="paddingTable">
                    <span class="ttn">ТТН серия</span> <asp:TextBox ID="tbTtnSeria" runat="server" width="15px" CssClass="form-control"/> 
                    <span class="ttn">ТТН номер</span> <asp:TextBox ID="tbTtnNumber" runat="server" width="50px" CssClass="form-control"/> 
                </td>
            </tr>

            <tr>
                <td class="valignTable">
                    <span class="ttn">Другие документы:</span>
                </td>
                <td class="paddingTable">
                    <asp:TextBox CssClass="ttn multi-control" ID="tbOtherDocuments" runat="server" width="95%" TextMode="MultiLine" Rows="3" Columns="40"/>
                </td>
            </tr>
        </table>
    </div>

    <div class="block3" style="width: 48%; display: inline-block; vertical-align: top; padding-left: 20px; border-left: 3px solid #EBEEF2; padding-top: 10px;">
        <table class="tableClass">
            <tr>
                <td class="paddingTable valignTable citybox" style="width: 100%;" colspan="2">
                    <div style="margin-bottom: 10px;">
                        <span>Отправляем из (адрес пункта погрузки):</span> <a href="#" onclick="OpenWarehouses('from'); return false;" style="float: right">выбрать склад <%= BackendHelper.TagToValue("official_name") %></a>
                    </div>
                    <asp:HiddenField runat="server" ID="hfWharehouse" Value=""/>
                    <asp:TextBox runat="server" ID="tbSenderCity" Width="97%" CssClass="form-control"/>
                    <asp:HiddenField runat="server" ID="hfSenderCityID"/>
                </td>
            </tr>
            <tr>
                <td class="valignTable" style="padding-top: 7px;">
                    <span>Адрес отправки:</span>
                </td>
                <td class="paddingTable">
                    <table>
                        <tr>
                            <td>
                                <asp:DropDownList ID="ddlSenderStreetPrefix" Width="50px" runat="server" CssClass="ddl-control" style="padding: 0; height: 28px;">
                                    <asp:listitem text="ул." value="ул."/>
                                    <asp:listitem text="аллея" value="аллея"/>
                                    <asp:listitem text="бул." value="бул."/>
                                    <asp:listitem text="дор." value="дор."/>
                                    <asp:listitem text="линия" value="линия"/>
                                    <asp:listitem text="маг." value="маг."/>
                                    <asp:listitem text="мик-н" value="мик-н"/>
                                    <asp:listitem text="наб." value="наб."/>
                                    <asp:listitem text="пер." value="пер."/>
                                    <asp:listitem text="пл." value="пл."/>
                                    <asp:listitem text="пр." value="пр."/>
                                    <asp:listitem text="пр-кт" value="пр-кт"/>
                                    <asp:listitem text="ряд" value="ряд"/>
                                    <asp:listitem text="тракт" value="тракт"/>
                                    <asp:listitem text="туп." value="туп."/>
                                    <asp:listitem text="ш." value="ш."/>
                                </asp:DropDownList>
                            </td>
                            <td>
                                <asp:TextBox ID="tbSenderStreetName" runat="server" width="130px" CssClass="form-control"/>
                            </td>
                        </tr>
                        <tr>
                            <td>дом: </td>
                            <td><asp:TextBox ID="tbSenderStreetNumber" runat="server" width="25px" CssClass="form-control"/></td>
                        </tr>
                        <tr>
                            <td style="width: 50px;">
                                <span>корпус:</span>
                            </td>
                            <td>
                                <asp:TextBox ID="tbSenderHousing" runat="server" width="15px" CssClass="form-control"/>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                квартира:
                            </td>
                            <td><asp:TextBox ID="tbSenderApartmentNumber" runat="server" width="25px" CssClass="form-control"/></td>
                        </tr>
                    </table>
                </td>
            </tr>
            
            <tr>
                <td class="valignTable">
                    <span>Паспортные данные:</span>
                </td>
                <td class="paddingTable">
                    серия <asp:TextBox ID="tbPassportSeria" runat="server" width="17px" CssClass="form-control"/> 
                    номер <asp:TextBox ID="tbPassportNumber" runat="server" width="50px" CssClass="form-control"/> 
                </td>
            </tr>

            <tr class="ttn">
                <td>
                    <div style="margin-bottom: 10px;">
                    Новый профиль
                    </div>
                </td>                
            </tr>

            <tr class="ttn">
                <td>
                    <asp:TextBox runat="server" ID="tbNewProfile" Width="80%" CssClass="form-control"/>
                </td>
                <td style="padding-left: 10px; padding-top: 8px;">
                    <asp:Button CssClass="buttonAdd" ID="btNewProfile" runat="server"/>                    
                </td>
            </tr>

            <tr class="ttn">
                <td>
                    <asp:Label BackColor="Red" Visible="false" ID="errAddProfile" runat="server">Введите название</asp:Label>
                </td>                
            </tr>

            <tr class="ttn">
                <td>
                    <div style="margin-bottom: 10px;">
                        Выбор профиля
                    </div>
                    <asp:HiddenField runat="server" ID="hfSelectProfile"/>
                    <asp:HiddenField runat="server" ID="hfSelectProfileID"/>
                </td>                 
            </tr>

            <tr class="ttn">                
                <td>                    
                    <div class="custom-combobox">
                        <asp:TextBox runat="server" ID="tbSelectProfile" onfocus="profileOnClick();" CssClass="custom-combobox-input"/>
                        <a runat="server" class="custom-combobox-button" onclick="selectProfileAutocomplite();" ID="SelectProfilesList">▼</a>
                    </div>
                </td>
                <td style="padding-left: 10px; padding-top: 8px;">
                    <asp:Button CssClass="buttonUpdate" ID="btUpdateProfile" runat="server"/>
                    <asp:Button CssClass="buttonDelete" ID="btDeleteProfile" runat="server"/>                    
                </td>                
            </tr>

            <tr class="ttn">
                <td>
                    <asp:Label BackColor="Red" Visible="false" ID="errUpdateProfile" runat="server">Выберите профиль</asp:Label>
                </td>                
            </tr>

            <tr>
                <td colspan="2" style="text-align: right">
                    <a href="#" onclick="SaveDefaultAddress(); return false;">сделать адресом "по умолчанию"</a> | 
                    <a href="#" onclick="ClearDefaultSenderAddress(); return false;" style="color: red;">очистить адрес "по умолчанию"</a>
                </td>
            </tr>

            <tr>
                <td class="paddingTable valignTable citybox" style="width: 100%; border-top: 3px solid #ebeef2; padding-top: 10px;" colspan="2">
                    <div style="margin-bottom: 10px;">
                        Отправляем в (адрес пункта разгрузки):   <a href="#" onclick="OpenWarehouses('to'); return false;" style="float: right">выбрать склад <%= BackendHelper.TagToValue("official_name") %></a>
                    </div>
                    <asp:TextBox runat="server" ID="tbCity" Width="97%" CssClass="form-control"/>
                    <div class="notVisible" id="surcharge">
                        доплата за отклонение:
                        <asp:Label runat="server" style="font-weight: bold; margin-right: 20px;" ID="lblCityCost"/>
                    </div>
                    <div class="notVisible" id="delivery-days">
                        дни доставки (с нашего склада): <asp:Label runat="server" style="font-weight: bold;" ID="lblCityDeliveryDate"/>
                    </div>
                    <div class="notVisible" id="delivery-terms">
                        срок доставки (с нашего склада):
                        <asp:Label runat="server" style="font-weight: bold; margin-right: 20px;" ID="lblCityDeliveryTerms"/>
                    </div>
                    <asp:HiddenField runat="server" ID="hfCityID"/>
                </td>
            </tr>
            
            <tr>
                <td class="valignTable" style="padding-top: 7px;">
                    <span>Адрес доставки:</span>
                </td>
                <td class="paddingTable">
                    <table>
                        <tr>
                            <td>
                                <asp:DropDownList ID="ddlRecipientStreetPrefix" Width="50px" runat="server" CssClass="ddl-control" style="padding: 0; height: 28px;">
                                    <asp:listitem text="ул." value="ул."/>
                                    <asp:listitem text="аллея" value="аллея"/>
                                    <asp:listitem text="бул." value="бул."/>
                                    <asp:listitem text="дор." value="дор."/>
                                    <asp:listitem text="линия" value="линия"/>
                                    <asp:listitem text="маг." value="маг."/>
                                    <asp:listitem text="мик-н" value="мик-н"/>
                                    <asp:listitem text="наб." value="наб."/>
                                    <asp:listitem text="пер." value="пер."/>
                                    <asp:listitem text="пл." value="пл."/>
                                    <asp:listitem text="пр." value="пр."/>
                                    <asp:listitem text="пр-кт" value="пр-кт"/>
                                    <asp:listitem text="ряд" value="ряд"/>
                                    <asp:listitem text="тракт" value="тракт"/>
                                    <asp:listitem text="туп." value="туп."/>
                                    <asp:listitem text="ш." value="ш."/>
                                </asp:DropDownList>
                            </td>
                            <td>
                                <asp:TextBox ID="tbRecipientStreet" runat="server" width="130px" CssClass="form-control"/>
                            </td>
                        </tr>
                        <tr>
                            <td>дом: </td>
                            <td><asp:TextBox ID="tbRecipientStreetNumber" runat="server" width="25px" CssClass="form-control"/></td>
                        </tr>
                        <tr>
                            <td style="width: 50px;">
                                <span>корпус:</span>
                            </td>
                            <td>
                                <asp:TextBox ID="tbRecipientKorpus" runat="server" width="15px" CssClass="form-control"/>
                            </td>
                        </tr>
                        <tr>
                            <td>квартира: </td>
                            <td><asp:TextBox ID="tbRecipientKvartira" runat="server" width="25px" CssClass="form-control"/></td>
                        </tr>
                    </table>
                </td>
            </tr>

            <tr>
                <td colspan="2" style="width: 100%; border-top: 3px solid #ebeef2; padding-top: 10px;">
                    
                </td>
            </tr>
            <tr>
                <td class="valignTable" >
                    <span>Дата отправки:</span>
                </td>
                <td class="paddingTable">
                    <asp:TextBox ID="tbDeliveryDate" runat="server" style="width: 75px" CssClass="form-control"/>
                </td>
            </tr>  
            <tr>
                <td class="valignTable">
                    <span>Фамилия получателя:</span>
                </td>
                <td class="paddingTable">
                    <asp:TextBox ID="tbRecipientFirstName" runat="server" width="95%" CssClass="form-control"/>
                </td>
            </tr>
            <tr>
                <td class="valignTable">
                    <span>Имя получателя:</span>
                </td>
                <td class="paddingTable">
                    <asp:TextBox ID="tbRecipientLastName" runat="server" width="95%" CssClass="form-control"/>
                </td>
            </tr>
            <tr>
                <td class="valignTable">
                    <span>Отчество получателя:</span>
                </td>
                <td class="paddingTable">
                    <asp:TextBox ID="tbRecipientThirdName" runat="server" width="95%" CssClass="form-control"/>
                </td>
            </tr>
            <tr>
                <td class="valignTable">
                    <span>Телефоны получателя:</span>
                </td>
                <td class="paddingTable" style="padding-left: 10px;">
                    <div style="padding-bottom: 5px;">
                        #1: <asp:TextBox ID="tbRecipientPhone" runat="server" Width="120px" CssClass="form-control"/>
                    </div>
                    <div>
                       #2: <asp:TextBox ID="tbRecipientPhone2" runat="server" Width="120px" CssClass="form-control"/> 
                    </div>
                </td>
            </tr>
            
            <tr>
                <td class="valignTable paddingTable" style="width: 150px;">
                    <asp:HyperLink runat="server" ID="hlHowMatch" NavigateUrl="#calculate" onclick="calculate();" >Сколько за услугу?</asp:HyperLink>
                    <asp:Label runat="server" ID ="lblHowMatch" Visible="False" Text="За услугу"></asp:Label> 
                </td>
                <td class="valignTable paddingTable" style="padding-left: 7px; font-weight: bold">
                    <span id="calculate"></span>
                    <b><asp:Label runat="server" ID ="lblHowMatchValue" Visible="False"></asp:Label>
                    </b>
                </td>
            </tr>
        </table>
    </div>
    
    
    

    <div class="block4" style="border-top: 3px solid #EBEEF2; padding-top: 10px;">
       <table class="tableClass" style="width:100%"> 
            <tr>
                <td class="valignTable" style="width: 100px">
                    <span>Примечания:</span>
                </td>
                <td >
                    <asp:TextBox ID="tbNote" runat="server" width="450px" TextMode="MultiLine" Rows="4" Columns="40" CssClass="multi-control"/>
                </td>
                <td style="text-align: right; vertical-align: bottom">
                    <asp:Button ID="btnBack" OnClientClick="window.history.go(-1);return false;" runat="server" CssClass="btn btn-default" Text='Назад'/>
                    <asp:Button ID="btnCreate" runat="server" Text='<%# ButtonText %>' CssClass="btn btn-default" ValidationGroup="LoginGroup"/>
                    <asp:TextBox ID="tbID" runat="server" width="100%" Visible="False"/>
                </td>
            </tr>        
        </table>
        <div class="tableClass">
            <asp:Label runat="server" ID="lblStatusLabel" Visible="False">Статус: </asp:Label><b><asp:Label runat="server" ID="lblStatus" Visible="False"></asp:Label></b> <asp:HiddenField runat="server" ID="hfStatus"></asp:HiddenField>&nbsp;&nbsp;&nbsp;
            <asp:Label runat="server" ID="lblStatusDescriptionLabel" Visible="False">Расшифровка: </asp:Label><i><asp:Label runat="server" ID="lblStatusDescription" Visible="False"></asp:Label></i>
        </div>
    </div>
    
    <div id="warehouses" style="display: none;">
        <div id="warehouses-close" onclick="HideWarehouses();">х</div>
        <div id="warehouses-content">
            <div style="color: white; text-align: center;">Склады <%= BackendHelper.TagToValue("official_name") %></div>
            <asp:ListView ID="lvAllWarehouses" runat="server" DataKeyNames="ID" ClientIDMode="Predictable" ClientIDRowSuffix="ID">
                <ItemTemplate>
                    <div class="popupNotReadNews" style="width: 100%">
                        <a href="#" onclick="<%# String.Format(
                        "SetWarehouseAddress(" +
                        "$('#ctl00_MainContent_lvAllWarehouses_hfStreetName_{0}').val()," +
                        "$('#ctl00_MainContent_lvAllWarehouses_hfStreetNumber_{0}').val()," +
                        "$('#ctl00_MainContent_lvAllWarehouses_hfHousing_{0}').val()," +
                        "$('#ctl00_MainContent_lvAllWarehouses_hfApartmentNumber_{0}').val()," +
                        "$('#ctl00_MainContent_lvAllWarehouses_hfStreetPrefix_{0}').val()," +
                        "$('#ctl00_MainContent_lvAllWarehouses_hfCityID_{0}').val()," +
                        "$('#ctl00_MainContent_lvAllWarehouses_hfCity_{0}').val(), '" +
                        Eval("ID").ToString() + "'); return false;"
                        ,Eval("ID"))%>">
                            <div class="popupNotReadNews-date" style="display: block">
                                <span>
                                    <%# Eval("Name")%> | <%# WarehousesHelper.WarehouseAddress(Eval("ID").ToString()) %>
                                </span>
                            </div>
                        </a>
                    </div>
                    <asp:HiddenField runat="server" ID="hfStreetName" Value='<%# Eval("StreetName")%>'/>
                    <asp:HiddenField runat="server" ID="hfStreetNumber" Value='<%# Eval("StreetNumber")%>'/>
                    <asp:HiddenField runat="server" ID="hfHousing" Value='<%# Eval("Housing")%>'/>
                    <asp:HiddenField runat="server" ID="hfApartmentNumber" Value='<%# Eval("ApartmentNumber")%>'/>
                    <asp:HiddenField runat="server" ID="hfStreetPrefix" Value='<%# Eval("StreetPrefix")%>'/>
                    <asp:HiddenField runat="server" ID="hfCityID" Value='<%# Eval("CityID")%>'/>
                    <asp:HiddenField runat="server" ID="hfCity" Value='<%# CityHelper.CityIDToCityName(Eval("CityID").ToString())%>'/>
                </ItemTemplate>
                <LayoutTemplate>
                    <div id="ItemPlaceholder" runat="server"></div>
                </LayoutTemplate>
            </asp:ListView>
        </div>
    </div>
    
    <asp:CustomValidator ID="CustomValidator11" ControlToValidate="tbUserProfile" ClientValidationFunction="validateUserProfile" EnableClientScript="True" ValidationGroup="LoginGroup" ValidateEmptyText="True" runat="server"  Display="None" ErrorMessage="Вы не выбрали профиль" ></asp:CustomValidator>
    <asp:CustomValidator ID="CustomValidator16" ControlToValidate="tbBoxesNumber" ClientValidationFunction="validateIfEmpty" EnableClientScript="True" ValidationGroup="LoginGroup" ValidateEmptyText="True" runat="server"  Display="None" ErrorMessage="Введите количество коробок" ></asp:CustomValidator>
    <asp:CustomValidator ID="CustomValidator14" ControlToValidate="tbTtnSeria" ClientValidationFunction="validateTTNSeria" EnableClientScript="True" ValidationGroup="LoginGroup" ValidateEmptyText="True" runat="server"  Display="None" ErrorMessage="ТТН серия должна содержать 2 буквы" ></asp:CustomValidator>
    <asp:CustomValidator ID="CustomValidator15" ControlToValidate="tbTtnNumber" ClientValidationFunction="validateTTNNumber" EnableClientScript="True" ValidationGroup="LoginGroup" ValidateEmptyText="True" runat="server"  Display="None" ErrorMessage="ТТН номер должн содержать 7 цифр" ></asp:CustomValidator>
    <asp:CustomValidator ID="CustomValidator12" ControlToValidate="tbCity" ClientValidationFunction="validateCityTb" EnableClientScript="True" ValidationGroup="LoginGroup" ValidateEmptyText="True" runat="server"  Display="None" ErrorMessage="Вы не выбрали нас. пункт доставки" ></asp:CustomValidator>
    <asp:CustomValidator ID="CustomValidator7" ControlToValidate="tbDeliveryDate" ClientValidationFunction="validateIfEmpty" EnableClientScript="True" ValidationGroup="LoginGroup" ValidateEmptyText="True" runat="server"  Display="None" ErrorMessage="Вы не ввели дату отправки (она должна быть на день больше даты создания!)" ></asp:CustomValidator>
    <asp:CustomValidator ID="CustomValidator2" ControlToValidate="tbRecipientFirstName" ClientValidationFunction="validateFamily" EnableClientScript="True" ValidationGroup="LoginGroup" ValidateEmptyText="True" runat="server"  Display="None" ErrorMessage="Фамилия должна содержать минимум 2 буквы и не содержать цифры и пробелы" ></asp:CustomValidator>
    <asp:CustomValidator ID="CustomValidator1" ControlToValidate="tbRecipientLastName" ClientValidationFunction="validateNameAndOtchestvo" EnableClientScript="True" ValidationGroup="LoginGroup" ValidateEmptyText="True" runat="server"  Display="None" ErrorMessage="Имя должно содержать миниму 2 буквы и не содержать цифры и пробелы" ></asp:CustomValidator>
    <asp:CustomValidator ID="CustomValidator3" ControlToValidate="tbRecipientThirdName" ClientValidationFunction="validateNameAndOtchestvo" EnableClientScript="True" ValidationGroup="LoginGroup" ValidateEmptyText="True" runat="server"  Display="None" ErrorMessage="Отчество должно содержать миниму 2 буквы и не содержать цифры и пробелы" ></asp:CustomValidator>
    <asp:CustomValidator ID="CustomValidator8" ControlToValidate="tbPassportSeria" ClientValidationFunction="validatePasportSeriaEmpty" EnableClientScript="True" ValidationGroup="LoginGroup" ValidateEmptyText="True" runat="server"  Display="None" ErrorMessage="Серия паспорта должна содержать 2 буквы" ></asp:CustomValidator>
    <asp:CustomValidator ID="CustomValidator9" ControlToValidate="tbPassportNumber" ClientValidationFunction="validatePasportNumberEmpty" EnableClientScript="True" ValidationGroup="LoginGroup" ValidateEmptyText="True" runat="server"  Display="None" ErrorMessage="Номер паспорта должн содержать 7 цифр" ></asp:CustomValidator>
    <asp:CustomValidator ID="CustomValidator4" ControlToValidate="tbRecipientStreet" ClientValidationFunction="validateIfEmpty" EnableClientScript="True" ValidationGroup="LoginGroup" ValidateEmptyText="True" runat="server"  Display="None" ErrorMessage="Вы не ввели улицу получателя" ></asp:CustomValidator>
    <asp:CustomValidator ID="CustomValidator5" ControlToValidate="tbRecipientStreetNumber" ClientValidationFunction="validateNotEmptyNumber" EnableClientScript="True" ValidationGroup="LoginGroup" ValidateEmptyText="True" runat="server"  Display="None" ErrorMessage="Номер дома получателя введен неверно" ></asp:CustomValidator>
    <asp:CustomValidator ID="CustomValidator6" ControlToValidate="tbRecipientPhone" ClientValidationFunction="validateIfEmpty" EnableClientScript="True" ValidationGroup="LoginGroup" ValidateEmptyText="True" runat="server"  Display="None" ErrorMessage="Вы не ввели основной телефон получателя" ></asp:CustomValidator>
    <asp:CustomValidator ID="CustomValidator13" ControlToValidate="tbSenderStreetName" ClientValidationFunction="validateIfEmpty" EnableClientScript="True" ValidationGroup="LoginGroup" ValidateEmptyText="True" runat="server"  Display="None" ErrorMessage="Вы не ввели улицу точки отправки" ></asp:CustomValidator>
    <asp:CustomValidator ID="CustomValidator17" ControlToValidate="tbSenderStreetNumber" ClientValidationFunction="validateNotEmptyNumber" EnableClientScript="True" ValidationGroup="LoginGroup" ValidateEmptyText="True" runat="server"  Display="None" ErrorMessage="Неверно введен номер дома точки отправки" ></asp:CustomValidator>
    <asp:CustomValidator ID="CustomValidator18" ControlToValidate="tbSenderCity" ClientValidationFunction="validateIfEmptyCity" EnableClientScript="True" ValidationGroup="LoginGroup" ValidateEmptyText="True" runat="server"  Display="None" ErrorMessage="Неверно введен нас. пункт точки отправки" ></asp:CustomValidator>
    
    <asp:ValidationSummary ID="ValidationSummary1" runat="server" ValidationGroup="LoginGroup" ShowMessageBox="true" ShowSummary="false"/>
    
    <script type="text/javascript">
        /** ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~ Объявление переменных ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~ **/

        /** JSON-наименований **/
        var availableTitles = [ <%= AvailableTitles %>];
        
        /** JSON-профилей **/
        var availableUserProfiles = <%= AvailableUserProfiles %>;

        /** JSON-выбранного профиля **/
        var selectProfiles = "";

        /** Тип открытого попапа **/
        var warehouseType = "";

        var warehouseSelected = false;

        /** ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~ Код, выполняемый после загрузки страницы ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~ **/

        $(function () {
            /** Инициализация масок и виджетов **/
            $.datepicker.setDefaults($.datepicker.regional['ru']);
            $("#<%= tbRecipientPhone.ClientID %>").mask("+375 (99) 999-99-99");
            $("#<%= tbRecipientPhone2.ClientID %>").mask("+375 (99) 999-99-99");
            $(".moneyMask").maskMoney();
            $("#<%= tbDeliveryDate.ClientID %>").datepicker({
                nextText: "", 
                prevText: "", 
                changeMonth: true, 
                changeYear: true,
                /*beforeShowDay: function (date) {
                    var dayOfWeek = date.getDay();
                    return $.inArray(dateStr,forbiddeneDates) == -1;
                }*/

            }).mask("99-99-9999");


            /** Autocomplete для городов СТАРТ **/
            $('#<%= tbCity.ClientID%>').autocomplete({
                minChars: '2',
                params: {
                    appkey: '<%= AppKey%>'
                },
                serviceUrl: '../AppServices/AutocompliteAPI.asmx/GetCityForAutocompliteJSON',
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

                    if ($('#<%= lblCityDeliveryDate.ClientID%>').html().length !== 0) {
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
                serviceUrl: '../AppServices/AutocompliteAPI.asmx/GetCityForAutocompliteJSON',
                onSelect: function(suggestion) {
                    $('#<%= hfSenderCityID.ClientID%>').val(suggestion.data);

                    if ($('#<%= lblCityDeliveryDate.ClientID%>').html().length !== 0
                        && $('<%= BackendHelper.TagToValue("loading_point_street") %>').val().indexOf('#<%= tbSenderStreetName.ClientID%>') >= 0) {
                        $('#delivery-days').show();
                        $('#<%= lblCityDeliveryDate.ClientID%>').show();
                    } else {
                        $('#delivery-days').hide();
                        $('#<%= lblCityDeliveryDate.ClientID%>').hide();
                    }
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


            /** Autocomplete для профилей СТАРТ **/
            var ttnSEria = "";
            var ttnNumber = "";
            $('#<%= tbUserProfile.ClientID%>').autocomplete({
                lookup: availableUserProfiles,                
                onSelect: function (suggestion) {
                    $('#<%= hfUserProfileID.ClientID%>').val(suggestion.data);  
              
                    /** Условия для отображения особых полей в зависимости от типа профиля (при смене аккаунта на лету) **/
                    if ($('#<%= hfUserProfileID.ClientID%>').val().substring(0, 1) == "2" || $('#<%= hfUserProfileID.ClientID%>').val().substring(0, 1) == "3") {
                        $("#<%= tbTtnNumber.ClientID %>").show();
                        $("#<%= tbTtnSeria.ClientID %>").show();
                        $("#<%= tbTtnNumber.ClientID %>").val(ttnSEria);
                        $("#<%= tbTtnSeria.ClientID %>").val(ttnNumber);
                        $(".noTtn").hide();
                        $(".ttn").show(); 
                        $(".onlyYur").hide();
                    } else {
                        $("#<%= tbTtnNumber.ClientID %>").hide();
                        $("#<%= tbTtnSeria.ClientID %>").hide();
                        ttnSEria = $("#<%= tbTtnNumber.ClientID %>").val();
                        ttnNumber = $("#<%= tbTtnSeria.ClientID %>").val();
                        $("#<%= tbTtnNumber.ClientID %>").val("");
                        $("#<%= tbTtnSeria.ClientID %>").val("");
                        $(".noTtn").show();
                        $(".ttn").hide();
                    }
                    
                    if ($('#<%= tbUserProfile.ClientID%>').val().trim().length == 0 || $("#<%= hfUserProfileID.ClientID%>").val().length == 0) {
                        $('#<%= tbUserProfile.ClientID%>').css("background-color", "#ffdda8");
                    } else {
                        $('#<%= tbUserProfile.ClientID%>').css("background-color", "white");
                    }
                }
            });

            
            /** Autocomplete для профилей КОНЕЦ **/

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

            /** Условия для показа и сокрытия расшифровки статуса **/
            if ($('#<%= hfStatus.ClientID %>').val() == "4" || $('#<%= hfStatus.ClientID %>').val() == "7" || 
                $('#<%= hfStatus.ClientID %>').val() == "8" || $('#<%= hfStatus.ClientID %>').val() == "9" || 
                $('#<%= hfStatus.ClientID %>').val() == "10" || $('#<%= hfStatus.ClientID %>').val() == "11") {
                $("#<%= lblStatusDescription.ClientID %>").show();
                $("#<%= lblStatusDescriptionLabel.ClientID %>").show();
            } else {
                $("#<%= lblStatusDescription.ClientID %>").hide();
                $("#<%= lblStatusDescriptionLabel.ClientID %>").hide();
            }


            /** Условия для отображения особых полей в зависимости от типа профиля **/
            if ($('#<%= hfUserProfileID.ClientID%>').val().substring(0, 1) === "2" || $('#<%= hfUserProfileID.ClientID%>').val().substring(0, 1) === "3") {
                $("#<%= tbTtnNumber.ClientID %>").show();
                $("#<%= tbTtnSeria.ClientID %>").show();
                $(".ttn").show();
            } else {
                $("#<%= tbTtnNumber.ClientID %>").hide();
                $("#<%= tbTtnSeria.ClientID %>").hide();
                $(".ttn").hide();
            }

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
                        
            $.ajax({
                type: "POST",
                dataType: "json",
                url: "../AppServices/LoadAjaxService.asmx/CreateDataAutocompliteSelectedProfiles",
                data: ({
                    profileID: "<%= UserID%>"
                }),
                success: function (data) {
                    selectProfiles = data;
                    updateProfileAutocomplite();
                },
                error: function (result) {

                }
            }); 
        });



        /** ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~ Функции ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~ **/

        /** Обработка нажатия кнопки "Все профили" СТАРТ **/
        function profileAutocomplite() {
            var c = $(".autocomplete-suggestions");
            if ($(".autocomplete-suggestions").eq(3).is(":visible")) {
                $(".autocomplete-suggestions").eq(3).css("display","none");
                $('#<%= tbUserProfile.ClientID%>').val("").change();
            } else {
                $('#<%= tbUserProfile.ClientID%>').val(" ").change();
                $('#<%= tbUserProfile.ClientID%>').autocomplete();
                $(".autocomplete-suggestions").eq(3).css("display","block");
            }
        }

        /** Вызов автокомплита для выбранного профиля **/
        function selectProfileAutocomplite() {
            var c = $(".autocomplete-suggestions");
            if ($(".autocomplete-suggestions").last().is(":visible")) {
                $(".autocomplete-suggestions").last().css("display", "none");
                $('#<%= tbSelectProfile.ClientID%>').val("").change();
            } else {
                $('#<%= tbSelectProfile.ClientID%>').val("").change();
                $('#<%= tbSelectProfile.ClientID%>').val(" ").change();
                $('#<%= tbSelectProfile.ClientID%>').autocomplete();
                $(".autocomplete-suggestions").last().css("display", "block");
            }
        }

        /** Настройка автокомплита для выбранного профиля **/
        function updateProfileAutocomplite() {
            $('#<%= tbSelectProfile.ClientID%>').autocomplete({
                lookup: selectProfiles,
                minLength: 0,
                onSelect: function (suggestion) {
                    $('#<%= hfCityID.ClientID%>').val(suggestion.CityID);
                    $('#<%= lblCityCost.ClientID%>').html(suggestion.CityCost);
                    $('#<%= lblCityDeliveryDate.ClientID%>').html(suggestion.CityDeliveryDate);
                    $('#<%= lblCityDeliveryTerms.ClientID%>').html(suggestion.CityDeliveryTerms);
                    $('#<%= tbCity.ClientID%>').val(suggestion.CitySelectedString);
                    $('#<%= ddlRecipientStreetPrefix.ClientID%>').val(suggestion.AddressPrefix);
                    $('#<%= tbRecipientStreet.ClientID%>').val(suggestion.AddressStreet);
                    $('#<%= tbRecipientStreetNumber.ClientID%>').val(suggestion.AddressHouseNumber);
                    $('#<%= tbRecipientKorpus.ClientID%>').val(suggestion.AddressKorpus);
                    $('#<%= tbRecipientKvartira.ClientID%>').val(suggestion.AddressKvartira);                    
                    $('#<%= tbDeliveryDate.ClientID%>').val(suggestion.SendDate);
                    $('#<%= tbDeliveryDate.ClientID%>').html(suggestion.SendDate);
                    $('#<%= tbRecipientPhone.ClientID%>').val(suggestion.RecipientPhone1);
                    $('#<%= tbRecipientPhone2.ClientID%>').val(suggestion.RecipientPhone2);
                    $('#<%= hfSelectProfile.ClientID%>').val(suggestion.ProfileName);
                    $('#<%= hfSelectProfileID.ClientID%>').val(suggestion.ID);
                    $('#<%= tbRecipientFirstName.ClientID%>').val(suggestion.FirstName);
                    $('#<%= tbRecipientLastName.ClientID%>').val(suggestion.LastName);
                    $('#<%= tbRecipientThirdName.ClientID%>').val(suggestion.ThirdName);
                }                
            }).focus(function () {
                $(this).autocomplete('search', $(this).val()) //auto trigger the search with whatever's in the box
            });      
        }
        
        function profileOnClick() {
            $('#<%= tbUserProfile.ClientID%>').val( $('#<%= tbUserProfile.ClientID%>').val().trim());
        }
        /** Обработка нажатия кнопки "Все профили" КОНЕЦ **/
        

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

                var goodCost = parseFloat(goodsCostValue.replace(/\s/g, "").replace(',', '.'));
                var goodNumber = parseFloat(goodsNumberValue.replace(/\s/g, "").replace(',', '.'));
                var overGoodCost = goodCost * goodNumber;
                overCost += overGoodCost;
                overCost = Math.round(parseFloat(overCost) * 100) / 100;
            }
            $("#<%=hfAssessedCost.ClientID%>").val(overCost.toString().replace('.', ','));

            $("#<%=lblAssessedCost.ClientID%>").html(Math.floor(overCost));
            $("#<%= lblAssessedCost.ClientID%>").html($("#<%= lblAssessedCost.ClientID%>").html().replace(/(\d)(?=(\d\d\d)+([^\d]|$))/g, '$1 ').replace('.', ','));

            $("#<%=lblAssessedCostCoints.ClientID%>").html(GetCoints(overCost));
            $("#<%= lblAssessedCostCoints.ClientID%>").html($("#<%= lblAssessedCostCoints.ClientID%>").html().replace(/(\d)(?=(\d\d\d)+([^\d]|$))/g, '$1 ').replace('.', ','));
        }
        /** Подсчета оценочной стоимости "на лету" КОНЕЦ **/

        

        function GetCoints(num) {
            var numb = num > 0 ? num - Math.floor(num) : Math.ceil(num) - num;
            return Math.round(parseFloat(numb) * 100);
        }

        /** Cчетчик символов СТАРТ **/
        function counterDescription(i) {
            var idFull = "#ctl00_MainContent_tbGoodsDescription" + i;
            var counterID = "#counter" + i;
            if ($(idFull).val().indexOf("б/у") !== -1 || 
                $(idFull).val().indexOf("б\\у") !== -1 || 
                $(idFull).val().indexOf("б\\y") !== -1 || 
                $(idFull).val().indexOf("б.у.") !== -1 || 
                $(idFull).val().indexOf("б.y.") !== -1 || 
                $(idFull).val().indexOf("б.у") !== -1 || 
                $(idFull).val().indexOf("б.y") !== -1 || 
                $(idFull).val().indexOf("<") !== -1 || 
                $(idFull).val().indexOf(">") !== -1) {
                var newValue = $(idFull).val().replace("б/у", "")
                    .replace("б/y", "")
                    .replace("б\\у", "")
                    .replace("б\\y", "")
                    .replace("б.у.", "")
                    .replace("б.y.", "")
                    .replace("б.у", "")
                    .replace("б.y", "")
                    .replace("<", "\'")
                    .replace(">", "\'");
                $(idFull).val(newValue);
            }
            var number = 100 - $(idFull).val().length;
            $(counterID).html("осталось <b>" + number + "</b> симв. в наименовании");
        }

        function counterModel(i) {
            var idFull = "#ctl00_MainContent_tbGoodsModel" + i;
            var counterID = "#counter" + i;
            if ($(idFull).val().indexOf("б/у") !== -1 || 
                $(idFull).val().indexOf("<") !== -1 || 
                $(idFull).val().indexOf(">") !== -1) {
                var newValue = $(idFull).val().replace("б/у", "")
                .replace("<", "\'")
                .replace(">", "\'");
                $(idFull).val(newValue); 
            }
            var number = 50 - $(idFull).val().length;
            $(counterID).html("осталось <b>" + number + "</b> симв. в марке/моделе");
        }
        /** Cчетчик символов КОНЕЦ **/

        /** Подсчет стоимости "За услугу" СТАРТ **/
        function calculate() {
           /* if ($('<!--%= BackendHelper.TagToValue("loading_point_street") %>').val().indexOf('#<!--%= tbSenderStreetName.ClientID%>') < 0 ) 
            {
                $('#calculate').html("Уточняйте стоимость доставки у наших менеджеров.");
            } 
            else {*/
                console.log("test");
                $.ajax({
                    type: "POST",
                    dataType: "xml",
                    url: "../WebServices/UserAPI/CalculatorAPI.asmx/Calculate",
                    data: ({
                        userid: '<%= UserID%>',
                        apikey: '<%= FirstUserApiKey%>',
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
        //}
        /** Подсчет стоимости "За услугу" КОНЕЦ **/

        /** Формирование JSON для получения стоимости "За услугу" СТАРТ **/
        function GetJSONString() {
            var goodsNumber = $('#<%= hfHowManyControls.ClientID%>').val();
            var calculateObject = {};
            calculateObject.goods = [];
            calculateObject.city_id = $('#<%= hfCityID.ClientID%>').val();
            calculateObject.profile_type = $('#<%= hfUserProfileID.ClientID%>').val().substring(0, 1);
            calculateObject.user_discount = $('#<%= hfUserDiscount.ClientID%>').val();
            calculateObject.user_id = $('#<%= hfUserID.ClientID%>').val();
            calculateObject.user_profile_id = $('#<%= hfUserProfileID.ClientID%>').val().substring(1);
            
            var wh = $('#<%= hfWharehouse.ClientID%>').val();
            calculateObject.iswharehouse = wh && wh != "" && wh !== "0";
            if ($('#<%= hfUserProfileID.ClientID%>').val().substring(0, 1) === "3") {
                calculateObject.assessed_cost = $('#<%= hfAssessedCost.ClientID%>').val();
            }


            for (var i = 1; i <= goodsNumber; i++) {
                var godsForApi = {};
                godsForApi.description = $("#ctl00_MainContent_tbGoodsDescription" + i).val();
                godsForApi.number = $("#ctl00_MainContent_tbGoodsNumber" + i).val();
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
        /** Формирование JSON для получения стоимости "За услугу" КОНЕЦ **/

        /** Валидаторы СТАРТ**/
        function validateFamily(sender, args) {
            var isValid;
            if (args.Value.length < 2 || args.Value.match(/\d+/g) || args.Value.indexOf(' ') >= 0) {
                isValid = false;
            } else {
                isValid = true;
            }
            validateChangeColor(sender, isValid);
            args.IsValid = isValid;
        }

        function validateNameAndOtchestvo(sender, args) {
            var isValid;
            if (args.Value.length < 2 || args.Value.match(/\d+/g) || args.Value.indexOf(' ') >= 0) {
                isValid = false;
            } else {
                isValid = true;
            }
            validateChangeColor(sender, isValid);
            args.IsValid = isValid;
        }

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
        
        function validateUserProfile(sender, args) {
            var isValid;
            if (args.Value.trim().length == 0 || $("#<%= hfUserProfileID.ClientID%>").val().length == 0) {
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

        function validateTTNSeria(sender, args) {
            var isValid;
            var myValue = args.Value;
            var profileType = $('#<%= hfUserProfileID.ClientID %>').val().substring(0, 1);
            if (profileType === "2" || profileType === "3") {
                if (myValue == "" || myValue.length != 2 || myValue.match(/\d+/g)) {
                    isValid = false;
                    validateChangeColor(sender, isValid);
                } else {
                    isValid = true;
                    validateChangeColor(sender, isValid);
                }
            } else {
                isValid = true;
            }

            args.IsValid = isValid;
        }

        function validateTTNNumber(sender, args) {
            var isValid;
            var myValue = args.Value;
            var profileType = $('#<%= hfUserProfileID.ClientID %>').val().substring(0, 1);
            if (profileType === "2" || profileType === "3") {
                if (myValue != "" && myValue.length == 7 && myValue.match(/^\d+$/)) {
                    isValid = true;
                    validateChangeColor(sender, isValid);
                } else {
                    validateChangeColor(sender, isValid);
                    isValid = false;
                }
            } else {
                isValid = true;
            }
            args.IsValid = isValid;
        }

        function validateIfEmptyCity(sender, args) {
            if ($("#<%= hfSenderCityID.ClientID%>").val().length == 0)
            {
                isValid = false;
                validateChangeColor(sender, isValid);
            }
            else {
                isValid = true;
                validateChangeColor(sender, isValid);
            }
            args.IsValid = isValid;
        }
        /** Валидаторы КОНЕЦ**/

        /** **/
        function SaveDefaultAddress() {
            $.ajax({
                type: "POST",
                url: "../AppServices/DefaultSenderAddressService.asmx/SaveDefaultSenderAdress",
                data: ({
                    userid: '<%= UserID%>',
                    cityid: $('#<%= hfSenderCityID.ClientID%>').val(),
                    appkey: '<%= AppKey%>',
                    senderstreetname: $('#<%= tbSenderStreetName.ClientID%>').val(),
                    senderstreetprefix: $('#<%= ddlSenderStreetPrefix.ClientID%>').val(),
                    senderstreetnumber: $('#<%= tbSenderStreetNumber.ClientID%>').val(),
                    senderhousing: $('#<%= tbSenderHousing.ClientID%>').val(),
                    senderapartmentnumber: $('#<%= tbSenderApartmentNumber.ClientID%>').val(),
                    senderawharehouse: $('#<%= hfWharehouse.ClientID%>').val()
                }),
                success: function (response) {
                    jAlert("Адрес успешно сделан адресом 'по умолчанию'. Теперь при создании заявки этот адрес будет автоматически заполнен", "закрыть");
                },
                error: function (result) {
                    jAlert("Ошибка сохранения. Попробуйте обновить страницу и сохранить адрес еще раз. В случае неудачи обратитесь к нашим менеджерам.", "закрыть");
                }
            });
        }

        function ClearDefaultSenderAddress() {
            $.ajax({
                type: "POST",
                url: "../AppServices/DefaultSenderAddressService.asmx/ClearDefaultSenderAddress",
                data: ({
                    userid: '<%= UserID%>',
                    cityid: $('#<%= hfSenderCityID.ClientID%>').val(),
                    appkey: '<%= AppKey%>'
                }),
                success: function (response) {
                    jAlert("Адрес 'по умолчанию' успешно очищен. Теперь при создании заявки адрес отправки будет пуст", "закрыть");
                },
                error: function (result) {
                    jAlert("Ошибка очистки. Попробуйте обновить страницу и очистить адрес еще раз. В случае неудачи обратитесь к нашим менеджерам.", "закрыть");
                }
            });
        }

        function OpenWarehouses(type) {
            $('#warehouses').show();
            warehouseType = type;
        }

        function HideWarehouses() {
            $('#warehouses').hide();
            warehouseType = "";
        }

        function SetWarehouseAddress(streetname, streetnumber, housing, apartmentnumber, streetprefix, cityid, city, wharehouseId) {
            warehouseSelected = true;
            $('#<%= hfWharehouse.ClientID %>').val(wharehouseId);
            if (warehouseType === "from") {
                $('#<%= tbSenderStreetName.ClientID %>').val(streetname);
                $('#<%= tbSenderStreetNumber.ClientID %>').val(streetnumber);
                $('#<%= tbSenderHousing.ClientID %>').val(housing);
                $('#<%= tbSenderApartmentNumber.ClientID %>').val(apartmentnumber);
                $('#<%= hfSenderCityID.ClientID %>').val(cityid);
                $('#<%= tbSenderCity.ClientID %>').val(city);
                $('#<%= ddlSenderStreetPrefix.ClientID %>').val(streetprefix);
            }

            if (warehouseType === "to") {
                $('#<%= tbRecipientStreet.ClientID %>').val(streetname);
                $('#<%= tbRecipientStreetNumber.ClientID %>').val(streetnumber);
                $('#<%= tbRecipientKorpus.ClientID %>').val(housing);
                $('#<%= tbRecipientKvartira.ClientID %>').val(apartmentnumber);
                $('#<%= hfCityID.ClientID %>').val(cityid);
                $('#<%= tbCity.ClientID %>').val(city);
                $('#<%= ddlRecipientStreetPrefix.ClientID %>').val(streetprefix);
            }
            $('#<%= lblCityCost.ClientID%>').html("");
            $('#<%= lblCityDeliveryDate.ClientID%>').html("");
            $('#<%= lblCityDeliveryTerms.ClientID%>').html("");
            HideWarehouses();
        }
        /** **/
    </script>
    
    <script type="text/javascript">
        function postbackReCalculate() {
            var goodsNumber = $("#<%= hfHowManyControls.ClientID%>").val();
            overAssessedCost(goodsNumber);
        };
    </script>

</asp:Content>
