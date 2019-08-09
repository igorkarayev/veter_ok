﻿<%@ Page Title="" Language="C#" MasterPageFile="~/ManagerMasterPage.master" AutoEventWireup="true" CodeBehind="UserTicketNotProcessedView.aspx.cs" Inherits="Delivery.ManagerUI.Menu.Tickets.UserTicketNotProcessedView" %>
<%@ Import Namespace="Delivery.AppServices" %>
<%@ Import Namespace="Delivery.BLL" %> <%@ Import Namespace="Delivery.BLL.StaticMethods" %>
<%@ Import Namespace="Delivery.BLL.Helpers" %>
<%@ Import Namespace="Delivery.Resources" %>
<%@ Register Tagprefix ="grb" Tagname="GoodsList" src="~/ManagerUI/Controls/GoodsList.ascx" %> 
<%@ Register Tagprefix ="grb" Tagname="PrintNaklInMap" src="~/ManagerUI/Controls/PrintNaklInMap.ascx" %> 
<%@ Register Tagprefix ="grb" Tagname="PrintNakl" src="~/ManagerUI/Controls/PrintNakl.ascx" %>
<%@ Register TagPrefix="grb" TagName="GruzobozCost" Src="~/ManagerUI/Controls/GruzobozCost.ascx" %> 
<%@ Register TagPrefix ="grb" Tagname="Weight" src="~/ManagerUI/Controls/Weight.ascx" %> 
<%@ Register TagPrefix="grb" TagName="MoneyForMoneyView" Src="~/ManagerUI/Controls/MoneyForMoneyView.ascx" %>
<%@ Register TagPrefix="grb" TagName="SenderAddress" Src="~/ManagerUI/Controls/SenderAddress.ascx" %>
<%@ Register TagPrefix="grb" TagName="AdditionalOptions" Src="~/ManagerUI/Controls/AdditionalOptions.ascx" %>
<%@ Register TagPrefix="grb" TagName="KK" Src="~/ManagerUI/Controls/KK.ascx" %>
<%@ Register TagPrefix="grb" TagName="ChangeTitle" Src="~/ManagerUI/Controls/ChangeTitle.ascx" %>
<%@ Register TagPrefix="grb" TagName="ChangeAddress" Src="~/ManagerUI/Controls/ChangeAddress.ascx" %>
<%@Register Tagprefix ="grb" Tagname="Comment" src="~/ManagerUI/Controls/Comment.ascx" %> 
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <script type="text/javascript">
        $(function () {
            if ($('.floatMenu .ddlAction option').size() <= 0) {
                $(".floatMenu").css("display", "none");
            }
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
            
            $.datepicker.setDefaults($.datepicker.regional['ru']);
            $("#<%= stbRecipientPhone.ClientID %>").mask("+375 (99) 999-99-99");
            $("#<%= stbDeliveryDate1.ClientID %>")
                .datepicker({ nextText: "", prevText: "", changeMonth: true, changeYear: true })
                .mask("99-99-9999");
            $("#<%= stbDeliveryDate2.ClientID %>")
                .datepicker({ nextText: "", prevText: "", changeMonth: true, changeYear: true })
                .mask("99-99-9999");
            $("#<%= tbDeliveryDate.ClientID %>")
                .datepicker({ nextText: "", prevText: "", changeMonth: true, changeYear: true })
                .mask("99-99-9999");
            
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
                setTimeout(function () { $('.lblStatus').hide(); }, 15000);
            }
        });

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
    <h3 class="h3custom" style="margin-top: 0;">Необработанные заявки</h3>
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
                                    <asp:TextBox runat="server" ID="stbID" CssClass="searchField form-control" style="width: 45px;" AccessKey="u"></asp:TextBox>
                                    UID:
                                    <asp:TextBox runat="server" ID="stbUID" CssClass="searchField form-control" style="width: 35px;" AccessKey="i"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    ТП:
                                </td>
                                <td>
                                    <asp:TextBox runat="server" ID="stbRecipientPhone" CssClass="searchField form-control" style="width: 140px;" AccessKey="p"></asp:TextBox>
                                </td>
                            </tr>
                        </table>
                    </td>
                    <td>
                        Направления:<br/>
                        <asp:ListBox runat="server" ID="sddlTracks" CssClass="searchField multi-control" SelectionMode="Multiple" Height="65px" Width="170px" AccessKey="t"></asp:ListBox>
                    </td>
                    <td>
                        &nbsp;<br/>
                        <table>
                            <tr>
                                <td>
                                    Отправка с:&nbsp;
                                    <asp:TextBox runat="server" ID="stbDeliveryDate1" CssClass="searchField form-control" Width="80px"></asp:TextBox>
                                    &nbsp;по:&nbsp;<asp:TextBox runat="server" ID="stbDeliveryDate2" CssClass="searchField form-control" Width="80px"></asp:TextBox>включ.
                                </td>
                            </tr>
                            <tr>
                                <td style="text-align: right">
                                    <asp:Button runat="server" Text="Сброс" CssClass="btn btn-default" ID="btnReload"/>
                                    <asp:Button runat="server" Text="Искать" CssClass="btn btn-default" ID="btnSearch"/> 
                                    &nbsp;&nbsp;&nbsp;<i class="small-informer">Найдено:<b> <asp:Label runat="server" ID="lblAllResult"></asp:Label></b></i>    
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td colspan="3">
                        Нас. пункты:
                        <asp:TextBox runat="server" ID="stbCity" CssClass="searchField form-control" style="width: 83%" AccessKey="c"></asp:TextBox>
                        <asp:HiddenField runat="server" ID="shfCityID"/>
                    </td>
                </tr>
                <tr>
                    <td colspan="4">
                        <div style="width:100%; font-size: 12px;">                           
                            <div style="width:10px; display:inline-block">&nbsp;</div>
                            не проверено: <asp:CheckBox runat="server" ID="cbNotCheckedOut"/>
                            <div style="width:10px; display:inline-block">&nbsp;</div>
                            не дозвонились: <asp:CheckBox runat="server" ID="cbNotPhoned"/>
                            <div style="width:10px; display:inline-block">&nbsp;</div>
                            счет не выставлен: <asp:CheckBox runat="server" ID="cbNotBilled"/>
                            <div style="width:10px; display:inline-block">&nbsp;</div>
                            не с гл. ск.: <asp:CheckBox runat="server" ID="cbNotMinsk"/>
                        </div>
                    </td>
                </tr>
            </table>
        </asp:Panel><br/>
        <div style="text-align: center; color: red; font-weight: bold; margin-bottom: 5px;">
            1) Сначала печатайте наклейки, затем чеки; <br/>
            2) При печати чеков заявка автоматически переходит в статус "<%=TicketStatusesResources.InStock %>" либо "<%=TicketStatusesResources.Transfer_InStock %>"!
        </div>
        <div class="loginError" id="errorDiv" style="width: 90%">
            <asp:Label runat="server" ID="lblNotif" ForeColor="White"></asp:Label>
        </div>

        <asp:HiddenField runat="server" ID="hfSortDirection"/>
        <asp:HiddenField runat="server" ID="hfSortExpression"/>

        <asp:ListView runat="server" ID="lvAllTickets"  DataKeyNames="ID" ClientIDMode="Predictable" 
        ClientIDRowSuffix="ID">
            <LayoutTemplate>
                <table runat="server" id="tableResult" class="tableViewClass tableClass colorTable">
                    <tr class="table-header" runat="server" id="tr1">
                        
                        <th>
                            <asp:CheckBox ID="chkboxSelectAll" runat="server"/>
                        </th>

                        <th>
                            ID
                        </th>

                        <th id="Th1" runat="server">
                             <asp:LinkButton runat="server" Text="UID" ID="lbSUID" CommandArgument="UserID"  OnClick="lvAllTickets_OnSortingButtonClick"/>
                        </th>
                        
                        <th>
                           HН
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

                        <th class='<%# UserRoles.TableNotes == 1 ? "displayColumn" : "notDisplayColumn" %>'>
                            Примечания
                        </th>

                        <th class='<%# UserRoles.TableNotes == 1 ? "displayColumn" : "notDisplayColumn" %>'>
                            Комментарии
                        </th>

                        <th>
                            <asp:LinkButton runat="server" Text="Дата отпр." ID="LinkButton4" CommandArgument="DeliveryDate" OnClick="lvAllTickets_OnSortingButtonClick"/>   
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

                        <th>
                            Дополнит. опции
                        </th>

                    </tr>
                    <tr runat="server" id="itemPlaceholder"></tr>
                </table>
            </LayoutTemplate>
            <ItemTemplate>
                <asp:TableRow id="Tr2" runat="server" CssClass='<%# String.Format("{0} {1} {2}",OtherMethods.SpecialClientTrClass(Convert.ToInt32(Eval("UserID"))), OtherMethods.TicketColoredStatusRows("1"), TicketsHelper.BackgroundCheckedOutPhoned(Eval("Phoned").ToString(), Eval("CheckedOut").ToString())) %>'>
                    
                    <asp:TableCell id="Td7" runat="server">
                        <asp:CheckBox ID="cbSelect" AutoPostBack="false" runat="server" CssClass="selectItem" ViewStateMode = "Enabled"/>
                    </asp:TableCell>

                    <asp:TableCell id="Td4" runat="server" EnableViewState="False">
                        <asp:HyperLink ID="HyperLink1" runat="server" NavigateUrl='<%#"~/ManagerUI/Menu/Tickets/UserTicketEdit.aspx?id=" + OtherMethods.SecureIDOrFullSecureID(Eval("ID").ToString()) + "&page=userticketsnotprocessed&" + OtherMethods.LinkBuilder(stbID.Text, 
                        stbUID.Text, stbRecipientPhone.Text, shfCityID.Value, String.Empty, String.Empty, stbDeliveryDate1.Text, stbDeliveryDate2.Text, 
                        sddlTracks.SelectedValue)%>'>
                            <asp:Label ID="lblSecureID" runat="server" Text='<%#Eval("SecureID") %>' />
                        </asp:HyperLink>
                        <asp:HiddenField runat="server" ID="HiddenField1" Value='<%# Eval("ID") %>'/>
                        <asp:HiddenField runat="server" ID="hfDriverID" Value='<%# Eval("DriverID") %>'/>
                        <asp:HiddenField runat="server" ID="hfStatusID" Value='<%# Eval("StatusID") %>'/>
                        <asp:HiddenField runat="server" ID="hfStatusDescription" Value='<%# Eval("StatusDescription") %>'/>
                        <asp:HiddenField runat="server" ID="hfAdmissionDate" Value='<%# Eval("AdmissionDate") %>'/>
                    </asp:TableCell>

                    <asp:TableCell id="Td3" runat="server" EnableViewState="False">
                        <asp:HyperLink ID="HyperLink2" runat="server" NavigateUrl='<%#"~/ManagerUI/Menu/Souls/ClientEdit.aspx?id="+Eval("UserID") %>'>
                            <asp:Label ID="Label5" runat="server" Text='<%#Eval("UserID") %>' />
                        </asp:HyperLink><br/>
                        <asp:Label ID="Label1" runat="server" style="text-transform: lowercase; font-weight: normal !important" Text='<%#UsersProfilesHelper.UserTypeToStr(UsersProfilesHelper.UserProfileIdToType(Convert.ToInt32(Eval("UserProfileID"))).ToString()) %>' />
                    </asp:TableCell>
                    
                    <asp:TableCell id="TableCell6" runat="server" EnableViewState="False">
                        <grb:PrintNaklInMap 
                            ID="PrintNaklInMap" 
                            runat="server" 
                            TicketID='<%# Eval("ID").ToString() %>' 
                            PrintNaklInMapValue='<%# Eval("PrintNaklInMap") %>' 
                            AvailableOtherDocuments='<%# Eval("AvailableOtherDocuments") %>' 
                            ListViewControlFullID='#ctl00_MainContent_lvAllTickets'
                            PageName = "UserTicketNotProcessedView"/>
                    </asp:TableCell>
                    
                    <asp:TableCell id="Td10" runat="server" EnableViewState="False">
                        <grb:GoodsList 
                            ID="GoodsList" 
                            runat="server" 
                            TicketID='<%# Eval("ID").ToString() %>'
                            ListViewControlFullID='#ctl00_MainContent_lvAllTickets'
                            PageName = "UserTicketNotProcessedView"/>
                    </asp:TableCell>

                    <asp:TableCell id="Td12" runat="server" EnableViewState="False">
                        <grb:KK
                            ID="KK" 
                            runat="server" 
                            TicketID='<%# Eval("ID").ToString() %>'
                            KKValue='<%# Eval("BoxesNumber").ToString() %>'
                            ListViewControlFullID='#ctl00_MainContent_lvAllTickets'
                            PageName = "UserTicketNotProcessedView"/>
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
                        <asp:Label ID="Label2" runat="server" Text='<%# CityHelper.CityIDToCityName(Eval("CityID").ToString()) %>' /><br/>
                        <span style="font-style: italic; font-size: 11px;"><asp:Label runat="server" ID="Label22" Text='<%# CityHelper.CityIDToFullDistrictName(Eval("CityID").ToString()) %>'></asp:Label></span>
                    </asp:TableCell>
                    
                    <asp:TableCell id="Td2" runat="server" EnableViewState="False">
                        <asp:Label ID="Label21" runat="server" Text='<%# Eval("RecipientStreetPrefix") %>' />&nbsp;
                        <asp:Label ID="Label12" runat="server" Text='<%# Eval("RecipientStreet") %>' />&nbsp;
                        <asp:Label ID="Label13" runat="server" Text='<%# OtherMethods.GetRecipientAddressWithoutStreet(Eval("ID").ToString()) %>' />
                        <grb:ChangeAddress 
                            ID="ChangeAddress" 
                            runat="server" 
                            TicketID='<%# Eval("ID").ToString() %>'
                            ListViewControlFullID='#ctl00_MainContent_lvAllTickets'
                            PageName = "UserTicketView"
                            />
                    </asp:TableCell>

                    <asp:TableCell id="Td9" runat="server" EnableViewState="False">
                         <asp:Label ID="Label10" runat="server" Text='<%# (Eval("RecipientFirstName").ToString()) %>' /> &nbsp;
                        <asp:Label ID="Label17" runat="server" Text='<%# (Eval("RecipientLastName").ToString()) %>' /> &nbsp;
                        <asp:Label ID="Label18" runat="server" Text='<%# (Eval("RecipientThirdName").ToString()) %>' /><br/>
                        <i style="font-size: 11px"><asp:Label ID="Label4" runat="server" Text='<%# (Eval("RecipientPhone").ToString().Replace("+375 ", " ")) %>' /></i><br/>
                        <i style="font-size: 11px"><asp:Label ID="Label19" runat="server" Text='<%# (Eval("RecipientPhoneTwo").ToString().Replace("+375 ", " ")) %>' /></i>
                    </asp:TableCell>

                    <asp:TableCell CssClass='<%# UserRoles.TableNotes == 1 ? "displayColumn" : "notDisplayColumn" %>' id="TableCell2" runat="server" EnableViewState="False">
                        <div class="noteDiv">
                            <grb:ChangeTitle 
                            ID="ChangeTitle" 
                            runat="server" 
                            TicketID='<%# Eval("ID").ToString() %>'
                            ListViewControlFullID='#ctl00_MainContent_lvAllTickets'
                            PageName = "UserTicketView"
                            TextTitle = '<%# Eval("Note").ToString() %>'/>
                        </div>
                    </asp:TableCell>
                    
                    <asp:TableCell CssClass='<%# UserRoles.TableComments == 1 ? "displayColumn" : "notDisplayColumn" %>' id="TableCell3" runat="server" EnableViewState="False">
                       <grb:Comment 
                            ID="Comment" 
                            runat="server" 
                            TicketID='<%# Eval("ID").ToString() %>'
                            CommentValue='<%# Eval("Comment").ToString() %>'
                            ListViewControlFullID='#ctl00_MainContent_lvAllTickets'
                            PageName = "UserTicketView"/>
                    </asp:TableCell>

                    <asp:TableCell id="Td0" runat="server" EnableViewState="False">
                        <asp:Label ID="lblDeliveryDate" runat="server" Text='<%# OtherMethods.DateConvert(Eval("DeliveryDate").ToString()) %>' />
                        <asp:HiddenField runat="server" ID="hfID" Value='<%# Eval("ID") %>'/>
                    </asp:TableCell>
                    
                    <asp:TableCell id="TableCell7" runat="server" EnableViewState="False" CssClass='<%# MoneyMethods.AgreedAccessedCostOver100BazVelich(MoneyMethods.AgreedAssessedCosts(Eval("ID").ToString())) %>'>
                        <grb:MoneyForMoneyView 
                            ID="MoneyForMoneyView" 
                            runat="server" 
                            TicketID='<%# Eval("ID").ToString() %>'
                            ListViewControlFullID='#ctl00_MainContent_lvAllTickets'
                            PageName = "UserTicketNotProcessedView"/>
                    </asp:TableCell>

                    <asp:TableCell id="Td16" runat="server" EnableViewState="False">
                        <grb:GruzobozCost 
                            ID="GruzobozCost" 
                            runat="server" 
                            TicketID='<%# Eval("ID").ToString() %>'
                            UserID='<%# Eval("UserID").ToString() %>'
                            GruzobozCostValue='<%# Eval("GruzobozCost").ToString() %>'
                            ListViewControlFullID='#ctl00_MainContent_lvAllTickets'
                            PageName = "UserTicketNotProcessedView"/>
                    </asp:TableCell>

                    <asp:TableCell id="TableCell4" runat="server" EnableViewState="False">
                        <grb:PrintNakl 
                            ID="PrintNakl" 
                            runat="server" 
                            TicketID='<%# Eval("ID").ToString() %>' 
                            PrintNaklValue='<%# Eval("PrintNakl") %>' 
                            ListViewControlFullID='#ctl00_MainContent_lvAllTickets'
                            PageName = "UserTicketNotProcessedView"/>
                    </asp:TableCell>

                    <asp:TableCell id="TableCell5" runat="server" EnableViewState="False">
                        <grb:Weight 
                            ID="Weight" 
                            runat="server" 
                            TicketID='<%# Eval("ID").ToString() %>'
                            WeightValue='<%# Eval("Weight").ToString() %>'
                            ListViewControlFullID='#ctl00_MainContent_lvAllTickets'
                            PageName = "UserTicketNotProcessedView"/>
                    </asp:TableCell>

                    <asp:TableCell id="TableCell9" runat="server" EnableViewState="False">
                       <grb:AdditionalOptions 
                            ID="AdditionalOptions" 
                            runat="server" 
                            TicketID='<%# Eval("ID").ToString() %>'
                            ListViewControlFullID='#ctl00_MainContent_lvAllTickets'
                            PageName = "UserTicketNotProcessedView"/>
                    </asp:TableCell>

                </asp:TableRow>
            </ItemTemplate>
            <EmptyDataTemplate>
                <asp:ListView runat="server" ID="lvAllTickets">  
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
                <asp:DataPager ID="lvDataPager" runat="server" PagedControlID="lvAllTickets" PageSize="100">
                    <Fields>
                        <asp:NumericPagerField ButtonType="Link" />
                    </Fields>
                </asp:DataPager>  
            </div>
        </div>
    </div>
    <div class="floatMenu">
        <asp:Label runat="server" ID="lblStstusDescription" CssClass="notVisible"><span style="color: #000; font-size: 12px; font-weight: bold;">Пояснение:</span> </asp:Label>
        <asp:TextBox ID="tbStatusDescription" runat="server" Width="125px" TextMode="MultiLine" CssClass="notVisible form-control"/>
        <asp:Label runat="server" ID="lblDeliveryDate" CssClass="notVisible"><span style="color: #000; font-size: 12px; font-weight: bold;">Дата отправки:</span> </asp:Label>
        <asp:TextBox ID="tbDeliveryDate" runat="server" Width="70px" CssClass="notVisible form-control"/>
        <asp:DropDownList runat="server" ID="ddlStatus"  Width="200px" CssClass="notVisible ddl-control"/>
        <asp:DropDownList runat="server" ID="ddlAction" CssClass="ddl-control ddlAction"/>
        <asp:Button runat="server" ID="btnAction" Text="Выполнить" CssClass="btn btn-default" OnClientClick="return ifEnyChecked();"/>  
    </div>

    <div class="floatMenuLeft lblStatus" style="margin-left: 0">
        <asp:Label runat="server" ID="lblStatus"></asp:Label>
    </div>

    <script>
        $(function () {
        });
    </script>
</asp:Content>
