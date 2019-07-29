﻿<%@ Page Title="" Language="C#" MasterPageFile="~/ManagerMasterPage.master" AutoEventWireup="true" CodeBehind="UserTicketByDeliveryOnBelarus.aspx.cs" Inherits="Delivery.ManagerUI.Menu.Tickets.UserTicketByDeliveryOnBelarus" %>
<%@ Import Namespace="Delivery.AppServices" %>
<%@ Import Namespace="Delivery.BLL" %> <%@ Import Namespace="Delivery.BLL.StaticMethods" %>
<%@ Import Namespace="Delivery.BLL.Helpers" %>
<%@Register Tagprefix ="grb" Tagname="GoodsList" src="~/ManagerUI/Controls/GoodsList.ascx" %> 
<%@Register Tagprefix ="grb" Tagname="PrintNaklInMap" src="~/ManagerUI/Controls/PrintNaklInMap.ascx" %> 
<%@Register Tagprefix ="grb" Tagname="PrintNakl" src="~/ManagerUI/Controls/PrintNakl.ascx" %> 
<%@Register Tagprefix ="grb" Tagname="GruzobozCost" src="~/ManagerUI/Controls/GruzobozCost.ascx" %> 
<%@Register Tagprefix ="grb" Tagname="Comment" src="~/ManagerUI/Controls/Comment.ascx" %> 
<%@Register Tagprefix ="grb" Tagname="Weight" src="~/ManagerUI/Controls/Weight.ascx" %> 
<%@ Register TagPrefix="grb" TagName="MoneyForMoneyView" Src="~/ManagerUI/Controls/MoneyForMoneyView.ascx" %>
<%@ Register TagPrefix="grb" TagName="AdditionalOptions" Src="~/ManagerUI/Controls/AdditionalOptions.ascx" %>
<%@ Register TagPrefix="grb" TagName="DeliveryFromToTime" Src="~/ManagerUI/Controls/DeliveryFromToTime.ascx" %>
<%@ Register TagPrefix="grb" TagName="DeliveredForCity" Src="~/ManagerUI/Controls/DeliveredForCity.ascx" %>
<%@ Register TagPrefix="grb" TagName="CityOrder" Src="~/ManagerUI/Controls/CityOrder.ascx" %>
<%@ Register TagPrefix="grb" TagName="SenderAddress" Src="~/ManagerUI/Controls/SenderAddress.ascx" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <script type="text/javascript">
        $(function () {
            if ($('.floatMenu .ddlAction option').size() <= 0) {
                $(".floatMenu").css("display", "none");
            }

            $(".city-informer-close").click(function () {
                $(".floatCity").hide(500);
            });
        });
        
        $(function () {
            $(window).scroll(function () {
                var top = $(document).scrollTop();
                if (top > 300 && $(".tableViewClass").height() >= $(window).height())
                    $(".floatMenu").css({ top: '0', position: 'fixed', borderBottom: '#025070 2px solid', borderRight: '#025070 2px solid', borderLeft: '#025070 2px solid', backgroundColor: '#0C72AF', borderRadius: '0 0 10px 10px', padding: '10px' });
                else
                    $(".floatMenu").css({ position: 'relative', border: 'none', padding: '0px', borderRadius: '0', backgroundColor: 'transparent' });
            });
        });

        $(function () {

            $(".moneyMask").maskMoney();
            $.datepicker.setDefaults($.datepicker.regional['ru']);
            $("#<%= stbRecipientPhone.ClientID %>").mask("+375 (99) 999-99-99");
            $("#<%= stbDeliveryDate1.ClientID %>")
                .datepicker({ nextText: "", prevText: "", changeMonth: true, changeYear: true })
                .mask("99-99-9999");
            $("#<%= stbDeliveryDate2.ClientID %>")
                .datepicker({ nextText: "", prevText: "", changeMonth: true, changeYear: true })
                .mask("99-99-9999");
            $("#<%= stbAdmissionDate1.ClientID %>")
                .datepicker({ nextText: "", prevText: "", changeMonth: true, changeYear: true })
                .mask("99-99-9999");
            $("#<%= stbAdmissionDate2.ClientID %>")
                .datepicker({ nextText: "", prevText: "", changeMonth: true, changeYear: true })
                .mask("99-99-9999");
            $("#<%= tbDeliveryDate.ClientID %>")
                .datepicker({ nextText: "", prevText: "", changeMonth: true, changeYear: true })
                .mask("99-99-9999");
            $('#<%= tbDelayTime.ClientID %>').timepicker({ 'timeFormat': 'H:i' });
            
            /** Autocomplete для городов СТАРТ **/
            $('#<%= stbCity.ClientID%>').autocomplete({
                minChars: '2',
                params: {
                    appkey: '<%= AppKey%>'
                },
                serviceUrl: '../../../AppServices/AutocompliteAPI.asmx/GetCityForAutocompliteJSON',
                onSelect: function (suggestion) {
                    $('#<%= shfCityID.ClientID%>').val($('#<%= shfCityID.ClientID%>').val() + ";" + suggestion.data);

                },
                delimiter: ";"
            });

            $('#<%= stbCity.ClientID%>').click(function() {
                if ($(this).val().trim() == "") {
                    $('#<%= shfCityID.ClientID%>').val("");
                }
            });
            
            $('#<%= stbCity.ClientID%>').keyup(function () {
                if ($(this).val().trim() == "") {
                    $('#<%= shfCityID.ClientID%>').val("");
                }
            });
            /** Autocomplete для городов КОНЕЦ **/


            if ($('#<%= ddlAction.ClientID %>').val() == "Изменить статус") {
                $("#<%= ddlStatus.ClientID %>").show();
            }

            //показываем текстбокс для даты переноса если статус "На складе (перенесен) или Перенос (у курьера)"
            if ($('#<%= ddlStatus.ClientID %>').val() == "4" || $('#<%= ddlStatus.ClientID %>').val() == "11") {
                $("#<%= tbDeliveryDate.ClientID %>").show();
                $("#<%= lblDeliveryDate.ClientID %>").show();
            }
            
            //показываем текстбокс для причины статуса в статусах 4,7,8,9,10
            if ($('#<%= ddlStatus.ClientID %>').val() == "4" || $('#<%= ddlStatus.ClientID %>').val() == "7" || $('#<%= ddlStatus.ClientID %>').val() == "8" || $('#<%= ddlStatus.ClientID %>').val() == "9" || $('#<%= ddlStatus.ClientID %>').val() == "10" || $('#<%= ddlStatus.ClientID %>').val() == "11") {
                $("#<%= tbStatusDescription.ClientID %>").show();
                $("#<%= lblStstusDescription.ClientID %>").show();
            }

            //баг 188
            if ($('#<%= ddlAction.ClientID %>').val() == "Добавить водителя") {
                $("#<%= ddlDrivers.ClientID %>").show();
                $("#<%= tbStatusDescription.ClientID %>").hide();
                $("#<%= lblStstusDescription.ClientID %>").hide();
                $("#<%= tbDeliveryDate.ClientID %>").hide();
                $("#<%= lblDeliveryDate.ClientID %>").hide();
            }
            
            $('#<%= ddlAction.ClientID %>').change(function () {
                $("#<%= tbDeliveryDate.ClientID %>").hide(); //скрываем дату
                $("#<%= tbStatusDescription.ClientID %>").hide(); //скрывает описание дескрипшн
                $("#<%= lblDeliveryDate.ClientID %>").hide(); //скрываем дату
                $("#<%= lblStstusDescription.ClientID %>").hide(); //скрывает описание дескрипшн
                

                if ($(this).val() == "Изменить статус") {
                    $("#<%= ddlStatus.ClientID %>").show();
                } else {
                    $("#<%= ddlStatus.ClientID %>").hide();
                }

                if ($(this).val() == "Добавить водителя") {
                    $("#<%= ddlDrivers.ClientID %>").show();
                } else {
                    $("#<%= ddlDrivers.ClientID %>").hide();
                }
            });
            
            $('#<%= ddlStatus.ClientID %>').change(function () {
                //показываем текстбокс для даты переноса если статус "На складе (перенесен) или Перенос (у курьера)"
                if ($(this).val() == "4" || $(this).val() == "11") {
                    $("#<%= tbDeliveryDate.ClientID %>").show();
                    $("#<%= lblDeliveryDate.ClientID %>").show();
                } else {
                    $("#<%= tbDeliveryDate.ClientID %>").hide();
                    $("#<%= lblDeliveryDate.ClientID %>").hide();
                }
                
                //показываем текстбокс для причины статуса в статусах 4,7,8,9,10,11
                if ($(this).val() == "4" || $(this).val() == "7" || $(this).val() == "8" || $(this).val() == "9" || $(this).val() == "10" || $(this).val() == "11") {
                    $("#<%= tbStatusDescription.ClientID %>").show();
                    $("#<%= lblStstusDescription.ClientID %>").show();
                } else {
                    $("#<%= tbStatusDescription.ClientID %>").hide();
                    $("#<%= lblStstusDescription.ClientID %>").hide();
                }
            });
            
            $('#ctl00_MainContent_lvAllTickets_chkboxSelectAll').change(function () {
                var checkboxes = $(this).closest('table').find('.selectItem input:checkbox');
                if ($(this).is(':checked')) {
                    checkboxes.attr('checked', 'checked');
                } else {
                    checkboxes.removeAttr('checked');
                }
            });
            if ($('#<%= lblNotif.ClientID%>').html() == "") {
                $('#errorDiv').hide();
            } else {
                $('#errorDiv').show();
            }

            if ($.trim($("#<%= lblStatus.ClientID %>").html()) != "") {
                $('.lblStatus').show();
                setTimeout(function () { $('.lblStatus').hide(); }, 10000);
            }

            $(".city-in-floatcity").click(function() {
                $("#<%=shfCityID.ClientID%>").val($(this).find("input[type=hidden]").val());
                $("#<%=stbCity.ClientID%>").val($(this).find(".data-city-name").text());
                $("#<%=btnSearch.ClientID%>").click();
            });

            $("#<%=lblDelayAdd.ClientID%>").click(function () {
                $(".floatCity table input.time").each(function () {
                    var delay = $("#ctl00_MainContent_tbDelayTime");
                    if ($(this).val() != "" && delay.val() != "") {
                        var curentTime = $(this);
                        var newTime = new Date("2000-01-01T" + curentTime.val() + ":00");
                        newTime.setHours(newTime.getHours() + new Date("2000-01-01T" + delay.val() + ":00").getHours());
                        newTime.setMinutes(newTime.getMinutes() + new Date("2000-01-01T" + delay.val() + ":00").getMinutes());

                        var hour = newTime.getHours();
                        var hourStr = hour.toString();
                        if (hour.toString().length == 1) { hourStr = "0" + hour.toString(); }

                        var minutes = newTime.getMinutes();
                        var minutesStr = minutes.toString();
                        if (minutes.toString().length == 1) { minutesStr = "0" + minutes.toString(); }
                        curentTime.val(hourStr + ":" + minutesStr);
                        curentTime.change();
                    }
                });
            });

            $("#<%=lblDelayDelete.ClientID%>").click(function () {
                $(".floatCity table input.time").each(function () {
                    var delay = $("#ctl00_MainContent_tbDelayTime");
                    if ($(this).val() != "" && delay.val() != "") {
                        var curentTime = $(this);
                        var newTime = new Date("2000-01-01T" + curentTime.val() + ":00");
                        newTime.setHours(newTime.getHours() - new Date("2000-01-01T" + delay.val() + ":00").getHours());
                        newTime.setMinutes(newTime.getMinutes() - new Date("2000-01-01T" + delay.val() + ":00").getMinutes());

                        var hour = newTime.getHours();
                        var hourStr = hour.toString();
                        if (hour.toString().length == 1) { hourStr = "0" + hour.toString(); }

                        var minutes = newTime.getMinutes();
                        var minutesStr = minutes.toString();
                        if (minutes.toString().length == 1) { minutesStr = "0" + minutes.toString(); }
                        curentTime.val(hourStr + ":" + minutesStr);
                        curentTime.change();
                    }
                });
            });
        });

        function SendSmsAjaxRequest() {
            $.ajax({
                type: "POST",
                url: "../../../AppServices/SmsService.asmx/SendSmsBulk",
                data: ({
                    ticketidlist: "<%= TickedIdListString %>",
                    appkey: "<%= AppKey %>"
                }),
                success: function (response) {
                    $('.lblStatus').html("<span style='font-size: 12px; color: green; font-weight: bold'>рассылка поставлена в очередь. <br/>статус отправки сообщений можно просмотреть в личном кабинете <a href=\"http://cp.uniqtele.com\">http://cp.uniqtele.com</a></span>");
                    $('.lblStatus').show();
                    setTimeout(function () { ClearLabelStatus(); }, 10000);
                },
                error: function (result) {
                    $('.lblStatus').html("<span style='font-size: 12px; color: red; font-weight: bold'>ошибка при рассылке</span>");
                    $('.lblStatus').show();
                }
            });
        }

        function ClearLabelStatus() {
            $('.lblStatus').hide();
        }

        function ifEnyChecked() {
            var enumer = 0;
            var checkboxes = $('#ctl00_MainContent_lvAllTickets_chkboxSelectAll').closest('table').find(':checkbox');
            if (checkboxes.is(':checked')) {
                enumer++;
            }
            if (enumer == 0) {
                alert('Выберите хотябы одну заявку!');
                return false;
            } else {
                return confirm('Вы уверены?');
            }
        }

        
    </script>
    <h3 class="h3custom" style="margin-top: 0;">Заявки РБ</h3>
    <div>
        <asp:Panel runat="server" ID="SearchPanel" CssClass="mainFont" DefaultButton="btnSearch">
            <table class="searchPanel" style="margin-left:auto; margin-right: auto;">
                <tr>
                    <td>
                        &nbsp;<br/>
                        <table>
                            <tr>
                                <td>
                                    ID:
                                </td>
                                <td>
                                    <asp:TextBox runat="server" ID="stbID" CssClass="searchField form-control" AccessKey="i" EnableViewState="False"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    UID:
                                </td>
                                <td>
                                    <asp:TextBox runat="server" ID="stbUID" CssClass="searchField form-control" AccessKey="u" EnableViewState="False"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    ТП:
                                </td>
                                <td>
                                    <asp:TextBox runat="server" ID="stbRecipientPhone" CssClass="searchField form-control" AccessKey="p" EnableViewState="False"></asp:TextBox>
                                </td>
                            </tr>
                        </table>
                    </td>
                    <td>
                        Направления:<br/>
                        <asp:ListBox runat="server" ID="sddlTracks" CssClass="searchField multi-control" SelectionMode="Multiple" Height="95px" Width="170px" AccessKey="t"></asp:ListBox>
                    </td>
                    <td>
                        Статусы:<br/>
                        <asp:ListBox runat="server" ID="sddlStatus" CssClass="searchField multi-control" SelectionMode="Multiple" Height="95px" Width="170px" AccessKey="s"></asp:ListBox>
                    </td>
                    <td>
                        Водители:<br/>
                        <asp:ListBox runat="server" ID="sddlDrivers" CssClass="searchField multi-control" SelectionMode="Multiple" Height="95px" Width="170px" AccessKey="d"></asp:ListBox>
                        <asp:HiddenField runat="server" ID="hfsddlDriverID"/>
                    </td>
                </tr>
                <tr>
                    <td colspan="4">
                        Нас. пункты:
                        <asp:TextBox runat="server" ID="stbCity" CssClass="searchField form-control" style="width: 83%" AccessKey="c"></asp:TextBox>
                        <asp:HiddenField runat="server" ID="shfCityID"/>
                    </td>
                </tr>
                <tr>
                    <td colspan="3" style="text-align: left;">
                        Отправка &nbsp;с:&nbsp;<asp:TextBox runat="server" ID="stbDeliveryDate1" CssClass="searchField form-control" Width="80px" EnableViewState="False"></asp:TextBox>
                        &nbsp;по:&nbsp;<asp:TextBox runat="server" ID="stbDeliveryDate2" CssClass="searchField form-control" Width="80px" EnableViewState="False"></asp:TextBox>включ.
                    </td>
                    <td style="text-align: right;">
                        <a href="#" id="openCity" style="font-weight: bold;">[города]</a>
                    </td>
                </tr>
                <tr>
                    <td colspan="4" style="text-align: left;">
                        Приемка&nbsp;&nbsp; с:&nbsp;<asp:TextBox runat="server" ID="stbAdmissionDate1" CssClass="searchField form-control" Width="80px" EnableViewState="False"></asp:TextBox>
                        &nbsp;по:&nbsp;<asp:TextBox runat="server" ID="stbAdmissionDate2" CssClass="searchField form-control" Width="80px" EnableViewState="False"></asp:TextBox>включ.
                        <div style="width:120px; display:inline-block">&nbsp;</div>
                        <asp:Button runat="server" Text="Сброс" CssClass="btn btn-default" ID="btnReload"/>
                        <asp:Button runat="server" Text="Искать" CssClass="btn btn-default" ID="btnSearch"/> 
                        &nbsp;&nbsp;&nbsp;<i class="small-informer">Найдено:<b> <asp:Label runat="server" ID="lblAllResult"></asp:Label></b></i>  
                    </td>
                </tr>
                <tr>
                    <td colspan="4">
                        <div style="width:100%; font-size: 12px;">
                            за услугу "0": <asp:CheckBox runat="server" ID="cbGruzobozCostIsNull"/>
                            <div style="width:10px; display:inline-block">&nbsp;</div>
                            не проверено: <asp:CheckBox runat="server" ID="cbNotCheckedOut"/>
                            <div style="width:10px; display:inline-block">&nbsp;</div>
                            не дозвонились: <asp:CheckBox runat="server" ID="cbNotPhoned"/>
                            <div style="width:10px; display:inline-block">&nbsp;</div>
                            счет не выставлен: <asp:CheckBox runat="server" ID="cbNotBilled"/>
                        </div>
                    </td>
                </tr>
                <tr>
                    <td colspan="4">
                        <div style="width:100%; font-size: 12px; text-align: center">
                            <asp:Button runat="server" CssClass="btn btn-default" ID="btnDownloadXmlCityForRouteRu" OnClick="btnDownloadXmlCityForRouteRu_Click" Text="Скачать XML-города для route.ru"/>
                            <asp:Button runat="server" CssClass="btn btn-default" ID="btnDownloadXmlForRouteRu" OnClick="btnDownloadXmlForRouteRu_Click" Text="Скачать XML для route.ru"/>
                            <asp:Button runat="server" CssClass="btn btn-default" ID="btnSendToRouteRu" OnClick="btnSendToRouteRu_Click" Text="Отправить в route.ru"/>
                            <asp:Button runat="server" CssClass="btn btn-default" ID="btnGetFromRouteRu" OnClick="btnGetFromRouteRu_Click" Text="Получить из route.ru"/>
                            <asp:HiddenField runat="server" ID="hfIdList"/>
                        </div>
                    </td>
                </tr>
            </table>
        </asp:Panel><br/>
        <div class="loginError" id="errorDiv" style="width: 90%;">
            <asp:Label runat="server" ID="lblNotif" ForeColor="White"></asp:Label>
        </div>

        <asp:HiddenField runat="server" ID="hfSortDirection" EnableViewState="False"/>
        <asp:HiddenField runat="server" ID="hfSortExpression" EnableViewState="False"/>
        <asp:HiddenField runat="server" ID="hfIsVisibleUserProfile" EnableViewState="False"/>

        <asp:ListView runat="server" ID="lvAllTickets"  DataKeyNames="ID" ClientIDMode="Predictable" 
        ClientIDRowSuffix="ID" >
            <LayoutTemplate>
                <table runat="server" id="tableResult" class="tableBorderRadius tableViewClass tableClass colorTable allTickets">
                    <tr>
                        
                        <th>
                            <asp:CheckBox ID="chkboxSelectAll" runat="server"/>
                        </th>

                        <th>
                            ID
                        </th>

                        <th runat="server">
                             <asp:LinkButton runat="server" Text="UID" ID="lbSUID" CommandArgument="UserID"  OnClick="lvAllTickets_OnSortingButtonClick"/>
                        </th>
                        
                        <th>
                           HН
                        </th>

                        <th runat="server"  class="thIsVisibleUserProfile">
                            Профиль
                        </th>

                        <th style="width: 140px;">
                            Грузы
                        </th>

                        <th>
                            КК
                        </th>
                        
                        <th>
                            Отпр. из
                        </th>

                        <th>
                            Нпр.
                        </th>

                        <th>
                            Город получ.
                        </th>

                        <th style="width: 120px;">
                            Адр. получателя
                        </th>

                        <th style="width: 110px;">
                            Получатель
                        </th>

                        <th>
                            Примечания
                        </th>

                        <th>
                            Комментарии
                        </th>
                        
                        <th>
                            Дополнит. опции
                        </th>

                        <th>
                            <asp:LinkButton runat="server" Text="Дата прие." ID="LinkButton3" CommandArgument="AdmissionDate" OnClick="lvAllTickets_OnSortingButtonClick"/>        
                        </th>

                        <th>
                            <asp:LinkButton runat="server" Text="Дата отпр." ID="LinkButton4" CommandArgument="DeliveryDate" OnClick="lvAllTickets_OnSortingButtonClick"/>   
                        </th>

                        <th>
                            <asp:LinkButton runat="server" Text="Статус" ID="LinkButton5" CommandArgument="StatusID" OnClick="lvAllTickets_OnSortingButtonClick"/>  
                        </th>

                        <th>
                            <asp:LinkButton runat="server" Text="DID" ID="LinkButton1" CommandArgument="DriverID" OnClick="lvAllTickets_OnSortingButtonClick"/>
                        </th>
                        
                        <th style="width: 70px">
                            Оцен/Согл
                        </th>

                        <th style="width: 70px">
                            За услугу
                        </th>

                        <th>
                            ПН
                        </th>

                        <th style="width: 40px">
                            Вес
                        </th>

                    </tr>
                    <tr runat="server" id="itemPlaceholder"></tr>
                </table>
            </LayoutTemplate>
            <ItemTemplate>
                <asp:TableRow 
                    id="Tr2" 
                    runat="server" 
                    CssClass='<%# String.Format("{0} {1} {2}", 
                                    OtherMethods.SpecialClientTrClass(Convert.ToInt32(Eval("UserID"))), 
                                    OtherMethods.TicketColoredStatusRows(Eval("StatusID").ToString()),
                                    TicketsHelper.BackgroundCheckedOutPhoned(Eval("Phoned").ToString(), Eval("CheckedOut").ToString())) %>'>
                    
                    <asp:TableCell id="Td7" runat="server">
                        <asp:CheckBox ID="cbSelect" AutoPostBack="false" runat="server" CssClass="selectItem" ViewStateMode="Enabled"/>
                    </asp:TableCell>

                    <asp:TableCell id="Td4" runat="server" EnableViewState="False">
                        <asp:HyperLink ID="HyperLink1" runat="server" NavigateUrl='<%#"~/ManagerUI/Menu/Tickets/UserTicketEdit.aspx?id=" + OtherMethods.SecureIDOrFullSecureID(Eval("ID").ToString()) + "&page=userticketbydeliveryonbelarus&" + OtherMethods.LinkBuilder(stbID.Text, 
                        stbUID.Text, stbRecipientPhone.Text, shfCityID.Value,sddlStatus.SelectedValue,sddlDrivers.SelectedValue,stbDeliveryDate1.Text, stbDeliveryDate2.Text, 
                        sddlTracks.SelectedValue)%>'>
                            <asp:Label ID="lblSecureID" runat="server" Text='<%#Eval("SecureID") %>' />
                        </asp:HyperLink>
                    </asp:TableCell>

                    <asp:TableCell id="Td3" runat="server"  EnableViewState="False">
                        <asp:HyperLink ID="HyperLink2" runat="server" NavigateUrl='<%#"~/ManagerUI/Menu/Souls/ClientEdit.aspx?id="+Eval("UserID") %>'>
                            <asp:Label ID="Label5" runat="server" Text='<%#Eval("UserID") %>' />
                        </asp:HyperLink>
                    </asp:TableCell>

                    <asp:TableCell id="TableCell6" runat="server" EnableViewState="False">
                        <grb:PrintNaklInMap 
                            ID="PrintNaklInMap" 
                            runat="server" 
                            TicketID='<%# Eval("ID").ToString() %>' 
                            PrintNaklInMapValue='<%# Eval("PrintNaklInMap") %>' 
                            AvailableOtherDocuments='<%# Eval("AvailableOtherDocuments") %>' 
                            ListViewControlFullID='#ctl00_MainContent_lvAllTickets'
                            PageName = "UserTicketView"/>
                    </asp:TableCell>

                    <asp:TableCell id="TableCell1" runat="server" EnableViewState="False" Visible="<%# IsVisibleUserProfile %>">
                        <asp:HyperLink ID="HyperLink4" runat="server" NavigateUrl='<%#"~/ManagerUI/Menu/Souls/ProfileEdit.aspx?id="+Eval("UserProfileID") %>'>
                            <asp:Label ID="Label1" runat="server" Text='<%# OtherMethods.GetProfileData(Eval("UserProfileID").ToString()) %>' />
                        </asp:HyperLink><br/>
                        <i style="font-size: 11px"><asp:Label ID="Label7" runat="server" Text='<%# OtherMethods.GetProfileContactPhone(Eval("UserProfileID").ToString()) %>' /></i>
                    </asp:TableCell>
                    
                    <asp:TableCell id="Td10" runat="server" EnableViewState="False">
                        <grb:GoodsList 
                            ID="GoodsList" 
                            runat="server" 
                            TicketID='<%# Eval("ID").ToString() %>'
                            ListViewControlFullID='#ctl00_MainContent_lvAllTickets'
                            PageName = "UserTicketView"/>
                    </asp:TableCell>

                    <asp:TableCell id="Td12" runat="server" EnableViewState="False">
                        <asp:Label ID="Label8" runat="server" Text='<%#Eval("BoxesNumber") %>' />
                    </asp:TableCell>
                    
                    <asp:TableCell id="TableCell10" runat="server" EnableViewState="False">
                        <grb:SenderAddress 
                            ID="SenderAddress" 
                            runat="server" 
                            TicketID='<%# Eval("ID").ToString() %>'
                            ListViewControlFullID='#ctl00_MainContent_lvAllTickets'
                            PageName = "UserTicketView"/>
                    </asp:TableCell>
                    
                    <asp:TableCell id="Td8" runat="server" EnableViewState="False">
                        <asp:Label ID="Label3" runat="server" Text='<%# CityHelper.CityToTrack(Convert.ToInt32(Eval("CityID")), Eval("ID").ToString()) %>' />
                    </asp:TableCell>
                    
                    <asp:TableCell id="Td5" runat="server" EnableViewState="False">
                        <asp:Label ID="lblCity" runat="server" Text='<%# CityHelper.CityIDToCityName(Eval("CityID").ToString()) %>' /><br/>
                        <span style="font-style: italic; font-size: 11px;">
                            <asp:Label runat="server" ID="lblDistrict" Text='<%# CityHelper.CityIDToFullDistrictName(Eval("CityID").ToString()) %>'></asp:Label>
                        </span>
                    </asp:TableCell>
                    
                    <asp:TableCell id="Td2" runat="server" EnableViewState="False">
                        <asp:Label ID="Label21" runat="server" Text='<%# Eval("RecipientStreetPrefix") %>' />&nbsp;
                        <asp:Label ID="Label12" runat="server" Text='<%# Eval("RecipientStreet") %>' />&nbsp;
                        <asp:Label ID="Label13" runat="server" Text='<%# OtherMethods.GetRecipientAddressWithoutStreet(Eval("ID").ToString()) %>' />
                    </asp:TableCell>

                    <asp:TableCell id="Td9" runat="server" EnableViewState="False">
                        <asp:Label ID="Label10" runat="server" Text='<%# (Eval("RecipientFirstName").ToString()) %>' /> &nbsp;
                        <asp:Label ID="Label17" runat="server" Text='<%# (Eval("RecipientLastName").ToString()) %>' /> &nbsp;
                        <asp:Label ID="Label18" runat="server" Text='<%# (Eval("RecipientThirdName").ToString()) %>' /><br/>
                        <i style="font-size: 11px"><asp:Label ID="Label4" runat="server" Text='<%# (Eval("RecipientPhone").ToString().Replace("+375 ", " ")) %>' /></i><br/>
                        <i style="font-size: 11px"><asp:Label ID="Label19" runat="server" Text='<%# (Eval("RecipientPhoneTwo").ToString().Replace("+375 ", " ")) %>' /></i>
                    </asp:TableCell>
                    
                    <asp:TableCell id="TableCell2" runat="server" EnableViewState="False">
                        <div class="noteDiv">
                            <asp:Label ID="Label14" runat="server" Text='<%# Eval("Note") %>' />
                        </div>
                    </asp:TableCell>
                    
                    <asp:TableCell id="TableCell3" runat="server" EnableViewState="False">
                       <grb:Comment 
                            ID="Comment" 
                            runat="server" 
                            TicketID='<%# Eval("ID").ToString() %>'
                            CommentValue='<%# Eval("Comment").ToString() %>'
                            ListViewControlFullID='#ctl00_MainContent_lvAllTickets'
                            PageName = "UserTicketView"/>
                    </asp:TableCell>
                    
                    <asp:TableCell id="TableCell9" runat="server" EnableViewState="False">
                       <grb:AdditionalOptions 
                            ID="AdditionalOptions" 
                            runat="server" 
                            TicketID='<%# Eval("ID").ToString() %>'
                            ListViewControlFullID='#ctl00_MainContent_lvAllTickets'
                            PageName = "UserTicketView"/>
                    </asp:TableCell>

                    <asp:TableCell id="Td14" runat="server" EnableViewState="False">
                        <asp:Label ID="Label11" runat="server" Text='<%# OtherMethods.DateConvert(Eval("AdmissionDate").ToString()) %>' />
                    </asp:TableCell>

                    <asp:TableCell id="Td0" runat="server" EnableViewState="False">
                        <asp:Label ID="lblDeliveryDate" runat="server" Text='<%# OtherMethods.DateConvert(Eval("DeliveryDate").ToString()) %>' />
                    </asp:TableCell>
                    
                    <asp:TableCell id="Td1" runat="server" EnableViewState="False">
                        <asp:Label ID="lblStatusID" runat="server" Text='<%# OtherMethods.TicketStatusToText(Eval("StatusID").ToString()) %>' />
                        <asp:HiddenField runat="server" ID="hfID" Value='<%# Eval("ID") %>'/>
                        <asp:HiddenField runat="server" ID="hfDriverID" Value='<%# Eval("DriverID") %>'/>
                        <asp:HiddenField runat="server" ID="hfStatusID" Value='<%# Eval("StatusID") %>'/>
                        <asp:HiddenField runat="server" ID="hfStatusIDOld" Value='<%# Eval("StatusIDOld") %>'/>
                        <asp:HiddenField runat="server" ID="hfStatusDescription" Value='<%# Eval("StatusDescription") %>'/>
                        <asp:HiddenField runat="server" ID="hfAdmissionDate" Value='<%# Eval("AdmissionDate") %>'/>
                    </asp:TableCell>
                    
                    <asp:TableCell id="Td13" runat="server" EnableViewState="False">
                        <asp:HyperLink ID="HyperLink3" runat="server" NavigateUrl='<%#"~/ManagerUI/Menu/Souls/DriverView.aspx?id="+Eval("DriverID") %>'>
                            <asp:Label ID="Label9" runat="server" Text='<%# DriversHelper.DriverIDConvert(Eval("DriverID").ToString()) %>' />
                        </asp:HyperLink>
                    </asp:TableCell>
                    
                    <asp:TableCell id="TableCell7" runat="server" EnableViewState="False" CssClass='<%# MoneyMethods.AgreedAccessedCostOver100BazVelich(MoneyMethods.AgreedAssessedCosts(Eval("ID").ToString())) %>'>
                        <grb:MoneyForMoneyView 
                            ID="MoneyForMoneyView" 
                            runat="server" 
                            TicketID='<%# Eval("ID").ToString() %>'
                            ListViewControlFullID='#ctl00_MainContent_lvAllTickets'
                            PageName = "UserTicketView"/>
                    </asp:TableCell>

                    <asp:TableCell id="Td16" runat="server" EnableViewState="False">
                        <grb:GruzobozCost 
                            ID="GruzobozCost" 
                            runat="server" 
                            TicketID='<%# Eval("ID").ToString() %>'
                            UserID='<%# Eval("UserID").ToString() %>'
                            GruzobozCostValue='<%# Eval("GruzobozCost").ToString() %>'
                            ListViewControlFullID='#ctl00_MainContent_lvAllTickets'
                            PageName = "UserTicketView"/>
                    </asp:TableCell>

                    <asp:TableCell id="TableCell4" runat="server" EnableViewState="False">
                        <grb:PrintNakl 
                            ID="PrintNakl" 
                            runat="server" 
                            TicketID='<%# Eval("ID").ToString() %>' 
                            PrintNaklValue='<%# Eval("PrintNakl") %>' 
                            ListViewControlFullID='#ctl00_MainContent_lvAllTickets'
                            PageName = "UserTicketView"/>
                    </asp:TableCell>

                    <asp:TableCell id="TableCell5" runat="server" EnableViewState="False">
                        <grb:Weight 
                            ID="Weight" 
                            runat="server" 
                            TicketID='<%# Eval("ID").ToString() %>'
                            WeightValue='<%# Eval("Weight").ToString() %>'
                            ListViewControlFullID='#ctl00_MainContent_lvAllTickets'
                            PageName = "UserTicketView"/>
                    </asp:TableCell>

                </asp:TableRow>
            </ItemTemplate>
            <EmptyDataTemplate>
                <asp:ListView runat="server" ID="lvAllTickets"  EnableViewState="False">  
                    <LayoutTemplate>
                            <asp:CheckBox ID="chkboxSelectAll" runat="server"/>
                            <div runat="server" id="itemPlaceholder"></div>
                    </LayoutTemplate>
                </asp:ListView>
                <div style="width: 100%; text-align: center; padding: 15px; color: red">По данным критериям ничего не найдено...</div>
            </EmptyDataTemplate>
        </asp:ListView>
        <div class="infoBlock" style="margin-bottom: 0;">
            <div class="pager">
                <asp:Label ID="lblPage" runat="server">Страница:</asp:Label>
                <asp:DataPager ID="lvDataPager" runat="server" PagedControlID="lvAllTickets" PageSize="100" EnableViewState="false">
                    <Fields>
                        <asp:NumericPagerField ButtonType="Link" />
                    </Fields>
                </asp:DataPager>  
            </div>
        </div>
    </div>
    <div class="floatMenu" style="padding: 0">
        <asp:Label runat="server" ID="lblStstusDescription" CssClass="notVisible" EnableViewState="false"><span style="color: #000; font-size: 12px; font-weight: bold;">Пояснение:</span> </asp:Label>
        <asp:TextBox ID="tbStatusDescription" runat="server" Width="125px" TextMode="MultiLine" CssClass="notVisible form-control" EnableViewState="false"/>
        <asp:Label runat="server" ID="lblDeliveryDate" CssClass="notVisible" EnableViewState="false"><span style="color: #000; font-size: 12px; font-weight: bold;">Дата отправки:</span> </asp:Label>
        <asp:TextBox ID="tbDeliveryDate" runat="server" Width="70px" CssClass="notVisible form-control"/>
        <asp:DropDownList runat="server" ID="ddlDrivers" Width="250px" CssClass="notVisible ddl-control"/>
        <asp:DropDownList runat="server" ID="ddlStatus"  Width="200px" CssClass="notVisible ddl-control"/>
        <asp:DropDownList runat="server" ID="ddlAction" CssClass="ddl-control ddlAction"/>
        <asp:Button runat="server" ID="btnAction" Text="Выполнить" CssClass="btn btn-default" OnClientClick="return ifEnyChecked();"/>  
    </div>
    
    <div class="floatMenuRight floatCity" style="width: 700px; max-height: 500px; overflow: auto;">
        <div class="city-informer">
            <asp:Panel runat="server" ID="pnlDelayTime" style="display: inline-block; float: left; padding-left: 5px;" EnableViewState="False">
                <span style="font-size: 12px;">изменить ОВД на: </span><asp:TextBox EnableViewState="False" runat="server" ID="tbDelayTime"  style="width: 35px;"></asp:TextBox>
                <asp:Label runat="server" CssClass="spanPlus" ID="lblDelayAdd" title="добавить время" Text="+"/>
                <asp:Label runat="server" CssClass="spanMinus" ID="lblDelayDelete" title="убрать время" Text="-"/>
            </asp:Panel>
            
            <asp:Label runat="server" EnableViewState="False" ID="lblCityCount"></asp:Label>
            <div class="city-informer-close" title="закрыть">х</div>
        </div>
        <asp:ListView runat="server" ID="lvAllCity" DataKeyNames="ID" ClientIDMode="Predictable" 
        ClientIDRowSuffix="ID" EnableViewState="False">
            <LayoutTemplate>
                <table>
                    <tr>
                        <th style="display: none;" id="thCityOrder">
                            ПН
                        </th>
                        <th>
                            К1
                        </th>
                        <th>
                            Нас. пункт
                        </th>
                        <th>
                            Район
                        </th>
                        <th>
                            Напр.
                        </th>
                        <th>
                            К2
                        </th>
                        <th style="width: 110px;">
                            ОВД
                        </th>
                        <th style="width: 50px; display: none;" id="thDeliveredForCity">
                            
                        </th>
                    </tr>
                    <tr runat="server" id="itemPlaceholder"></tr>
                </table>
            </LayoutTemplate>
            <ItemTemplate>
                <tr id="Tr2" runat="server" EnableViewState="False">
                    <td style="text-align: center;" runat="server" ID="tdCityOrder" Visible="<%# IsVisibleCityOrder %>">
                        <grb:CityOrder 
                            ID="CityOrder" 
                            runat="server" 
                            CityIdValue='<%# Eval("CityID").ToString() %>'
                            DriverIdValue='<%# Eval("DriverID").ToString() %>'
                            CityRowId ='<%# Eval("ID").ToString() %>'
                            ListViewControlFullID='#ctl00_MainContent_lvAllCity'
                            PageName = "UserTicketView"/>
                    </td>
                    <td style="text-align: center;">
                        <asp:Label runat="server" EnableViewState="False" ID="Label20" style="font-weight: normal;" Text='<%# Eval("TicketsNumberWithoutPhoned") %>'></asp:Label>
                    </td>
                    <td>
                        <a href="#" class="city-in-floatcity">
                            <asp:Label runat="server" EnableViewState="False" ID="lblLololo" CssClass="data-city-name" style="font-weight: normal; color: black" Text='<%# Eval("CityName") %>'></asp:Label>
                            <asp:HiddenField runat="server" Value='<%# Eval("CityID") %>'/>
                        </a>
                    </td>
                    <td>
                        <asp:Label runat="server" EnableViewState="False" ID="Label2" style="font-weight: normal;" Text='<%# Eval("DistrictName") %>'></asp:Label>
                    </td>
                    <td>
                        <asp:Label runat="server" EnableViewState="False" ID="Label6" style="font-weight: normal;" Text='<%# Eval("TrackName") %>'></asp:Label>
                    </td>
                    <td style="text-align: center;">
                        <asp:Label runat="server" EnableViewState="False" ID="Label15" style="font-weight: normal;" Text='<%# Eval("TicketsNumber") %>'></asp:Label>
                    </td>
                    <td style="text-align: center; padding-left: 0px; padding-right: 0px; font-weight: normal">
                        <grb:DeliveryFromToTime 
                            ID="DeliveryFromToTime" 
                            runat="server" 
                            OvdFrom='<%# Eval("OvdFrom").ToString() %>'
                            OvdTo='<%# Eval("OvdTo").ToString() %>'
                            ListViewControlFullID='#ctl00_MainContent_lvAllCity'
                            CityRowId ='<%# Eval("ID").ToString() %>'
                            PageName = "UserTicketView"/>
                        <asp:HiddenField runat="server" ID="hfTicketIdList" Value='<%# Eval("TicketIdList").ToString() %>'/>
                    </td>
                    <td runat="server" ID="tdDeliveredForCity" Visible="<%# IsVisibleDeliveredForCity %>">
                        <grb:DeliveredForCity 
                            ID="DeliveredForCity" 
                            runat="server" 
                            OvdFrom='<%# Eval("OvdFrom").ToString() %>'
                            OvdTo='<%# Eval("OvdTo").ToString() %>'
                            ListViewControlFullID='#ctl00_MainContent_lvAllCity'
                            CityRowId ='<%# Eval("ID").ToString() %>'
                            PageName = "UserTicketView"/>
                    </td>
                </tr>
            </ItemTemplate>
        </asp:ListView>
        <asp:Panel runat="server" ID="pnlLegend" style="font-weight: normal; font-size: 10px; padding: 10px; display: inline-block; width: 68%; float: left">
            <asp:Panel runat="server" ID="pnlDeliveredForCity" CssClass="pnlDeliveredForCity">
                Ссылка <b>"доставлено"</b> переводит все заявки города в статус "доставлено";<br/>    
            </asp:Panel>
            <asp:Panel runat="server" ID="pnlCityOrder" CssClass="pnlCityOrder">
                <b>ПН</b> - порядковый номер города при сортировке;<br/>    
            </asp:Panel>
            <b>К1</b> - количество заявок в нас. пункт за исключением заявок помеченых как "дозвонились";<br/>
            <b>К2</b> - количество всех заявок в нас. пункт;<br/>
            <b>Напр.</b> - направление;<br/>
            <asp:HiddenField runat="server" ID="hfCityIdList"/>
        </asp:Panel>
        <asp:Panel runat="server" ID="pnlSendSms" style="font-weight: normal; font-size: 10px; padding: 10px; display: inline-block; width: 23%; float: left" Visible="False">
            <div onclick="return confirm('Вы уверены?'); SendSmsAjaxRequest(); return false;" class="btn btn-default" style="width: 100%;  font-size: 12px; white-space:normal; text-align: center; border: 1px solid #ccc">Отправить СМС-рассылку получателям</div>
            <asp:HiddenField runat="server" ID="hfTicketID" Value="<%#TickedIdListString %>"/>
        </asp:Panel>
    </div>
    
    <div class="floatMenuLeft lblStatus" style="margin-left: 0">
        <asp:Label runat="server" ID="lblStatus"></asp:Label>
    </div>
    
    
    <script>
        $(function() {
            $("#openCity").click(function() {
                if ($(".floatCity").is(":visible")) {
                    $(".floatCity").hide(500);
                } else {
                    $(".floatCity").show(500);
                }
            });

            if ($('.pnlCityOrder').length != 0) {
                $('#thCityOrder').show();
            } else {
                $('#thCityOrder').hide();
            }

            if ($('.pnlDeliveredForCity').length != 0) {
                $('#thDeliveredForCity').show();
            } else {
                $('#thDeliveredForCity').hide();
            }

            if ($("#<%= hfIsVisibleUserProfile.ClientID%>").val() == "True") {
                $('.thIsVisibleUserProfile').show();
            } else {
                $('.thIsVisibleUserProfile').hide();
            }
        })
    </script>
</asp:Content>
