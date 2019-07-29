<%@ Page Title="" Language="C#" MasterPageFile="~/ManagerMasterPage.master" AutoEventWireup="true" CodeBehind="MoneyView.aspx.cs" Inherits="Delivery.ManagerUI.Menu.Finance.MoneyView" %>
<%@ Import Namespace="Delivery.AppServices" %>
<%@ Import Namespace="Delivery.BLL.StaticMethods" %>
<%@ Import Namespace="Delivery.BLL.Helpers" %>
<%@ Register Tagprefix ="grb" Tagname="PrintNaklInMap" src="~/ManagerUI/Controls/PrintNaklInMap.ascx" %> 
<%@ Register Tagprefix ="grb" Tagname="Comment" src="~/ManagerUI/Controls/Comment.ascx" %>
<%@ Register Tagprefix ="grb" Tagname="MoneyStatuses" src="~/ManagerUI/Controls/MoneyStatuses.ascx" %>
<%@ Register TagPrefix="grb" TagName="MoneyForMoneyView" Src="~/ManagerUI/Controls/MoneyForMoneyView.ascx" %>
<%@ Register TagPrefix="grb" TagName="DeliveryCostForMoneyView" Src="~/ManagerUI/Controls/DeliveryCostForMoneyView.ascx" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <h3 class="h3custom" style="margin-top: 0;">Финансы</h3>
    <div>
        <asp:Panel runat="server" ID="SearchPanel" CssClass="mainFont"  DefaultButton="btnSearch">
            <table class="table searchPanel" style="margin-left:auto; margin-right: auto; width: 100%">
                <tr>
                    <td>
                        Водители:&nbsp;<asp:DropDownList runat="server" ID="sddlDrivers" CssClass="searchField ddl-control" Width="280px"></asp:DropDownList>
                        Дата отправки с:&nbsp;<asp:TextBox runat="server" ID="stbDeliveryDate1" CssClass="searchField form-control" Width="75px"></asp:TextBox>
                        &nbsp;по:&nbsp;<asp:TextBox runat="server" ID="stbDeliveryDate2" CssClass="searchField form-control" Width="75px"></asp:TextBox>
                    </td>
                    <td style="float: right; padding-top: 6px;"> 
                        &nbsp;&nbsp;&nbsp;<i class="small-informer">Найдено:<b> <asp:Label runat="server" ID="lblAllResult"></asp:Label></b></i>  
                    </td>
                </tr>
                <tr>
                    <td colspan="2" style="text-align: right; width: 100%">
                        <asp:Button runat="server" Text="Сброс" CssClass="btn btn-default" ID="btnReload"/>
                        <asp:Button runat="server" Text="Искать" CssClass="btn btn-default" ID="btnSearch"/>  
                        <asp:Button runat="server" Text="Распределить BLR" CssClass="btn btn-default" ID="btnScatter" OnClientClick="Scatter(); return false;"/>  
                    </td>
                </tr>
            </table>
        </asp:Panel><br/>
        <asp:ListView runat="server" ID="lvAllTickets"  DataKeyNames="ID" ClientIDMode="Predictable" 
        ClientIDRowSuffix="ID">
            <LayoutTemplate>
                <table runat="server" id="Table1" class="table tableBorderRadius tableViewClass tableClass moneyTable">
                    <tr>
                        <th>
                            Инфо
                        </th>
                        <th>
                            НН
                        </th>
                        <th>
                            Оцен/Согл
                        </th>
                        <th>
                            Доставка
                        </th>
                        <th>
                            BLR
                        </th>
                        <th style="display: none;">
                            USD
                        </th>
                        <th style="display: none;">
                            EUR
                        </th>
                        <th style="display: none;">
                            RUR
                        </th>
                        <th style="padding: 0 15px;">
                            Разница
                        </th>
                        <th>
                            Статус
                        </th>
                        <th>
                            Город
                        </th>
                        <th>
                            Адрес
                        </th>
                        <th>
                            Дата отправки
                        </th>
                        <th>
                            Примечание
                        </th>
                        <th>
                            Комментарий
                        </th>
                    </tr>
                    <tr runat="server" id="itemPlaceholder"></tr>
                </table>
            </LayoutTemplate>
            <ItemTemplate>
                <asp:TableRow ID="Tr2" runat="server" CssClass='<%# String.Format("money {0}",OtherMethods.TicketColoredStatusRows(Eval("StatusID").ToString())) %>'>
                    <asp:TableCell id="Td4" runat="server" EnableViewState="False">
                        <div>
                            <asp:HyperLink ID="HyperLink1" runat="server" NavigateUrl='<%#"~/ManagerUI/Menu/Tickets/UserTicketEdit.aspx?id="+Eval("SecureID") + "&page=money&" + OtherMethods.LinkBuilder(String.Empty, 
                            String.Empty, String.Empty, String.Empty, String.Empty,sddlDrivers.SelectedValue,stbDeliveryDate1.Text, stbDeliveryDate2.Text, String.Empty)%>'>
                                <asp:Label ID="lblSecureID" runat="server" Text='<%#Eval("SecureID") %>' />
                            </asp:HyperLink>
                        </div>
                    
                        <div style="font-size: 11px; padding-top: 5px; padding-left: 2px; text-align: left;">
                            <div>
                                <span style="padding-right: 2px;">#П:</span><asp:HyperLink ID="HyperLink2" runat="server" NavigateUrl='<%#"~/ManagerUI/Menu/Souls/ClientEdit.aspx?id="+Eval("UserID") %>'>
                                    <asp:Label ID="Label5" runat="server" Text='<%#Eval("UserID") %>' />
                                </asp:HyperLink>
                            </div>
                    
                            <div>
                                <span style="padding-right: 2px;">#В:</span><asp:HyperLink ID="HyperLink4" runat="server" NavigateUrl='<%#"~/ManagerUI/Menu/Souls/DriverView.aspx?id="+Eval("DriverID") %>'>
                                    <asp:Label ID="Label1" runat="server" Text='<%# DriversHelper.DriverIDConvert(Eval("DriverID").ToString()) %>' />
                                </asp:HyperLink>
                            </div>
                        </div>
                        <asp:HiddenField runat="server" Value='<%#Eval("ID") %>' ID="hfID"/>
                        <input type="hidden" class="data-id" value='<%#Eval("ID") %>'/>
                    </asp:TableCell>
                
                    <asp:TableCell id="TableCell6" runat="server" EnableViewState="False">
                        <grb:PrintNaklInMap 
                                ID="PrintNaklInMap" 
                                runat="server" 
                                TicketID='<%# Eval("ID").ToString() %>' 
                                PrintNaklInMapValue='<%# Eval("PrintNaklInMap") %>' 
                                ListViewControlFullID='#ctl00_MainContent_lvAllTickets'
                                PageName="MoneyView"/>
                    </asp:TableCell>

                    <asp:TableCell id="Td6" runat="server" EnableViewState="False">
                        <grb:MoneyForMoneyView 
                            ID="MoneyForMoneyView" 
                            runat="server" 
                            TicketID='<%# Eval("ID").ToString() %>'
                            ListViewControlFullID='#ctl00_MainContent_lvAllTickets'
                            PageName = "MoneyView"/>
                    </asp:TableCell>
                    
                    <asp:TableCell id="TableCell8" runat="server" EnableViewState="False">
                        <grb:DeliveryCostForMoneyView 
                            ID="DeliveryCostForMoneyView" 
                            runat="server" 
                            TicketID='<%# Eval("ID").ToString() %>'
                            ListViewControlFullID='#ctl00_MainContent_lvAllTickets'
                            PageName = "MoneyView"/>
                    </asp:TableCell>

                    <asp:TableCell id="Td10" runat="server" EnableViewState="False">
                        <asp:Label runat="server" CssClass="courseLabel" Text='&nbsp;' style="padding:2px;"></asp:Label>
                        <asp:TextBox ID="tbReceivedBLR" style="width: 70px;" runat="server" CssClass="moneyMask recivedBLR" Text='<%# MoneyMethods.MoneySeparator(Eval("ReceivedBLR").ToString()) %>' Width="100px" 
                            onblur='<%# AppServicesHelper.SaveBLROnBlur( Eval("ID").ToString())%>' 
                            onfocus='<%# AppServicesHelper.SaveBLROnFocus( Eval("ID").ToString())%>'/><br/>
                        <asp:Label runat="server" ID="lblReceivedBLRStatus"></asp:Label> 
                    </asp:TableCell>
                                
                    <asp:TableCell style="display: none;" id="Td2" CssClass="recivedUSDTd" runat="server" EnableViewState="False">
                        <asp:TextBox ID="tbReceivedUSDCourse" runat="server"  CssClass="moneyMask moneyCourse recivedUSDCource" Width="40px" Text='<%# MoneyMethods.MoneySeparator(Eval("CourseUSD").ToString()) %>'
                            onkeyup='<%# AppServicesHelper.USDOnBlur( Eval("ID").ToString())%>' ></asp:TextBox>
                        <asp:TextBox ID="tbReceivedUSD" style="width: 70px;" runat="server" CssClass="moneyMask recivedUSD" Text='<%# MoneyMethods.MoneySeparator(Eval("ReceivedUSD").ToString()) %>' Width="100px" 
                            onblur='<%# AppServicesHelper.SaveUSDOnBlur( Eval("ID").ToString())%>' 
                            onfocus='<%# AppServicesHelper.SaveUSDOnFocus( Eval("ID").ToString())%>'
                            onkeyup='CalculateRecivedUSD();'/><br/>
                        <asp:Label runat="server" ID="lblReceivedUSDStatus"></asp:Label> 
                    </asp:TableCell>

                    <asp:TableCell style="display: none;" id="Td9" CssClass="recivedEURTd" runat="server" EnableViewState="False">
                        <asp:TextBox ID="tbReceivedEURCourse" runat="server"  CssClass="moneyMask moneyCourse recivedEURCource"  Width="40px" Text='<%#  MoneyMethods.MoneySeparator(Eval("CourseEUR").ToString()) %>'
                            onkeyup='<%# AppServicesHelper.EUROnBlur( Eval("ID").ToString())%>'></asp:TextBox>
                        <asp:TextBox ID="tbReceivedEUR" style="width: 70px;" runat="server" CssClass="moneyMask recivedEUR" Text='<%# MoneyMethods.MoneySeparator(Eval("ReceivedEUR").ToString()) %>' Width="100px" 
                            onblur='<%# AppServicesHelper.SaveEUROnBlur( Eval("ID").ToString())%>' 
                            onfocus='<%# AppServicesHelper.SaveEUROnFocus( Eval("ID").ToString())%>'
                            onkeyup='CalculateRecivedEUR();'/><br/>
                        <asp:Label runat="server" ID="lblReceivedEURStatus"></asp:Label> 
                    </asp:TableCell>
                
                    <asp:TableCell style="display: none;" id="Td0" CssClass="recivedRURTd" runat="server" EnableViewState="False">
                        <asp:TextBox ID="tbReceivedRURCourse" runat="server"  CssClass="moneyMask moneyCourse recivedRURCource"  Width="40px" Text='<%#  MoneyMethods.MoneySeparator(Eval("CourseRUR").ToString()) %>'
                            onkeyup='<%# AppServicesHelper.RUROnBlur( Eval("ID").ToString())%>'></asp:TextBox>
                        <asp:TextBox ID="tbReceivedRUR" style="width: 70px;" runat="server" CssClass="moneyMask recivedRUR" Text='<%# MoneyMethods.MoneySeparator(Eval("ReceivedRUR").ToString()) %>' Width="100px" 
                            onblur='<%# AppServicesHelper.SaveRUROnBlur( Eval("ID").ToString())%>' 
                            onfocus='<%# AppServicesHelper.SaveRUROnFocus( Eval("ID").ToString())%>'
                            onkeyup='CalculateRecivedRUR();'/><br/>
                        <asp:Label runat="server" ID="lblReceivedRURStatus"></asp:Label> 
                    </asp:TableCell>
                
                    <asp:TableCell id="TableCell7" runat="server" EnableViewState="False">
                        <asp:Label runat="server" ID="lblRaznica"></asp:Label>
                    </asp:TableCell>
                
                    <asp:TableCell id="TableCell3" runat="server" EnableViewState="False">
                        <grb:MoneyStatuses 
                                ID="MoneyStatuses" 
                                runat="server" 
                                TicketID='<%# Eval("ID").ToString() %>'
                                StatusID='<%# Eval("StatusID").ToString() %>'
                                ListViewControlFullID='#ctl00_MainContent_lvAllTickets'
                                PageName="MoneyView"/>
                    </asp:TableCell>

                    <asp:TableCell id="Td5" runat="server" EnableViewState="False">
                        <asp:Label ID="Label2" runat="server" Text='<%# CityHelper.CityIDToCityName(Eval("CityID").ToString()) %>' />
                    </asp:TableCell>
                
                    <asp:TableCell id="TableCell2" runat="server" EnableViewState="False">
                        <asp:Label ID="Label12" runat="server" Text='<%# Eval("RecipientStreet") %>' />&nbsp;
                        <asp:Label ID="Label13" runat="server" Text='<%# OtherMethods.GetRecipientAddressWithoutStreet(Eval("ID").ToString()) %>' />
                    </asp:TableCell>
                    
                    <asp:TableCell ID="TableCell1" runat="server">
                        <%# OtherMethods.DateConvert(Eval("DeliveryDate").ToString()) %>
                    </asp:TableCell>

                    <asp:TableCell id="TableCell4" runat="server" EnableViewState="False">
                        <div class="noteDiv">
                            <asp:Label ID="Label14" runat="server" Text='<%# Eval("Note") %>' />
                        </div>
                    </asp:TableCell>
                    
                    <asp:TableCell id="TableCell5" runat="server" EnableViewState="False">
                        <grb:Comment 
                                ID="Comment" 
                                runat="server" 
                                TicketID='<%# Eval("ID").ToString() %>'
                                CommentValue='<%# Eval("Comment").ToString() %>'
                                ListViewControlFullID='#ctl00_MainContent_lvAllTickets'
                                PageName="MoneyView"/>
                    </asp:TableCell>
                </asp:TableRow>
            </ItemTemplate>
        
            <EmptyDataTemplate>
                <asp:ListView runat="server" ID="lvAllTickets">  
                    <LayoutTemplate>
                            <div runat="server" id="itemPlaceholder"></div>
                    </LayoutTemplate>
                </asp:ListView>
                <div style="width: 100%; text-align: center; padding: 15px; color: red">По данным критериям ничего не найдено либо не произведен поиск...</div>
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
    
    <div class="floatMenu" style="text-align: left; padding: 0; font-size: 13px;">
        <i>Сумма стоимостей + доставку:</i> <b><asp:Label runat="server" ID='lblAgreedAssessedWithDeliveryCost'><%= AgreedAssessedWithDeliveryCost %></asp:Label></b> BLR;<br/>
        <div id="resivedUSDOverDiv" style="display: none; margin-top:5px;">
            <i>Привезено:</i> <b id="resivedUSDOver">0</b> USD;
        </div>
        <div id="resivedEUROverDiv" style="display: none;">
            <i>Привезено:</i> <b id="resivedEUROver">0</b> EUR;
        </div>
        <div id="resivedRUROverDiv" style="display: none;">
            <i>Привезено:</i> <b id="resivedRURDOver">0</b> RUR;
        </div>
        <div id="balanceBLRDiv" style="display: none;">
            <i>Остаток:</i> <b id="balanceBLR">0</b> BLR;
        </div>
    </div>

    <div class="floatMenuLeft lblStatus" style="margin-left: 0">
        <asp:Label runat="server" ID="lblStatus"></asp:Label>
    </div>
    
    <script type="text/javascript">
        var oldValBLR;
        var oldValUSD;
        var oldValEUR;
        var oldValRUR;
        var separator = '.';


        $(function () {
            
            $("#<%= stbDeliveryDate1.ClientID %>")
                .datepicker({ nextText: "", prevText: "", changeMonth: true, changeYear: true })
                .mask("99-99-9999");
            $("#<%= stbDeliveryDate2.ClientID %>")
                .datepicker({ nextText: "", prevText: "", changeMonth: true, changeYear: true })
                .mask("99-99-9999");
            $(".moneyMask").maskMoney();

            $(".floatMenu").css({
                top: '0',
                position: 'fixed',
                borderBottom: '#0c6792 2px solid',
                borderLeft: '#0c6792 2px solid',
                backgroundColor: '#0c6792',
                borderRadius: '0 0 0 10px',
                padding: '10px',
                color: 'white',
                right: '0',
                width: 'auto'
            });

            if ($.trim($("#<%= lblStatus.ClientID %>").html()) != "") {
                $('.lblStatus').show();
                setTimeout(function () { $('.lblStatus').hide(); }, 3000);
            }
        });

        function saveBLR(ticketidval) {
            if (oldValBLR != $('#ctl00_MainContent_lvAllTickets_tbReceivedBLR_' + ticketidval).val()) {
                $.ajax({
                    type: "POST",
                    url: "../../../AppServices/SaveAjaxService.asmx/SaveBLR",
                    data: ({
                        money: $('#ctl00_MainContent_lvAllTickets_tbReceivedBLR_' + ticketidval).val().replace(/\s/g, ""),
                        ticketid: ticketidval,
                        appkey: "<%= AppKey %>",
                        userid: "<%= UserID%>",
                        userip: "<%= UserIP%>",
                        pagename: "<%= PageName%>"
                    }),
                    success: function (response) {
                        $('.lblStatus').html("<span style='font-size: 12px; color: green; font-weight: bold'>сохранено</span>");
                        $('.lblStatus').show();
                        setTimeout(function () { $('.lblStatus').hide(); }, 3000);
                    },
                    error: function (result) {
                        $('.lblStatus').html("<span style='font-size: 12px; color: red; font-weight: bold'>ошибка</span>");
                        $('.lblStatus').show();
                    }
                });
            }
            CalculateDifference(ticketidval, true);
        }

        function saveUSD(ticketidval) {
            if (oldValUSD != $('#ctl00_MainContent_lvAllTickets_tbReceivedUSD_' + ticketidval).val() ||
                (oldValUSD == 0 && $('#ctl00_MainContent_lvAllTickets_tbReceivedUSD_' + ticketidval).val() == "0")) {
                $.ajax({
                    type: "POST",
                    url: "../../../AppServices/SaveAjaxService.asmx/SaveUSD",
                    data: ({
                        money: $('#ctl00_MainContent_lvAllTickets_tbReceivedUSD_' + ticketidval).val().replace(/\s/g, ""),
                        course: $('#ctl00_MainContent_lvAllTickets_tbReceivedUSDCourse_' + ticketidval).val().replace(/\s/g, ""),
                        ticketid: ticketidval,
                        appkey: "<%= AppKey %>",
                        userid: "<%= UserID%>",
                        userip: "<%= UserIP%>",
                        pagename: "<%= PageName%>"
                    }),
                    success: function (response) {
                        $('.lblStatus').html("<span style='font-size: 12px; color: green; font-weight: bold'>сохранено</span>");
                        $('.lblStatus').show();
                        setTimeout(function () { $('.lblStatus').hide(); }, 3000);
                    },
                    error: function (result) {
                        $('.lblStatus').html("<span style='font-size: 12px; color: red; font-weight: bold'>ошибка</span>");
                        $('.lblStatus').show();
                    }
                });
            }
            CalculateDifference(ticketidval, true);
        }

        function saveRUR(ticketidval) {
            if (oldValRUR != $('#ctl00_MainContent_lvAllTickets_tbReceivedRUR_' + ticketidval).val() ||
                (oldValRUR == 0 && $('#ctl00_MainContent_lvAllTickets_tbReceivedRUR_' + ticketidval).val() == "0")) {
                $.ajax({
                    type: "POST",
                    url: "../../../AppServices/SaveAjaxService.asmx/SaveRUR",
                    data: ({
                        money: $('#ctl00_MainContent_lvAllTickets_tbReceivedRUR_' + ticketidval).val().replace(/\s/g, ""),
                        course: $('#ctl00_MainContent_lvAllTickets_tbReceivedRURCourse_' + ticketidval).val().replace(/\s/g, ""),
                        ticketid: ticketidval,
                        appkey: "<%= AppKey %>",
                        userid: "<%= UserID%>",
                        userip: "<%= UserIP%>",
                        pagename: "<%= PageName%>"
                    }),
                    success: function (response) {
                        $('.lblStatus').html("<span style='font-size: 12px; color: green; font-weight: bold'>сохранено</span>");
                        $('.lblStatus').show();
                        setTimeout(function () { $('.lblStatus').hide(); }, 3000);
                    },
                    error: function (result) {
                        $('.lblStatus').html("<span style='font-size: 12px; color: red; font-weight: bold'>ошибка</span>");
                        $('.lblStatus').show();
                    }
                });
            }
            CalculateDifference(ticketidval, true);
        }

        function saveEUR(ticketidval) {
            if (oldValEUR != $('#ctl00_MainContent_lvAllTickets_tbReceivedEUR_' + ticketidval).val() ||
                (oldValEUR == 0 && $('#ctl00_MainContent_lvAllTickets_tbReceivedEUR_' + ticketidval).val() == "0")) {
                $.ajax({
                    type: "POST",
                    url: "../../../AppServices/SaveAjaxService.asmx/SaveEUR",
                    data: ({
                        money: $('#ctl00_MainContent_lvAllTickets_tbReceivedEUR_' + ticketidval).val().replace(/\s/g, ""),
                        course: $('#ctl00_MainContent_lvAllTickets_tbReceivedEURCourse_' + ticketidval).val().replace(/\s/g, ""),
                        ticketid: ticketidval,
                        appkey: "<%= AppKey %>",
                        userid: "<%= UserID%>",
                        userip: "<%= UserIP%>",
                        pagename: "<%= PageName%>"
                    }),
                    success: function (response) {
                        $('.lblStatus').html("<span style='font-size: 12px; color: green; font-weight: bold'>сохранено</span>");
                        $('.lblStatus').show();
                        setTimeout(function () { $('.lblStatus').hide(); }, 3000);
                    },
                    error: function (result) {
                        $('.lblStatus').html("<span style='font-size: 12px; color: red; font-weight: bold'>ошибка</span>");
                        $('.lblStatus').show();
                    }
                });
            }
            CalculateDifference(ticketidval, true);
        }

        function CalculateDifference(ticketidval, notFirst) {
            var agreedAssessedCosts = $('#ctl00_MainContent_lvAllTickets_MoneyForMoneyView_' + ticketidval + '_tbAgreedAssessedCosts_' + ticketidval).val().replace(/\s*/g, '').replace(/&nbsp;/g, '');
            var deliveryCosts = $('#ctl00_MainContent_lvAllTickets_DeliveryCostForMoneyView_' + ticketidval + '_lblDeliveryCosts_' + ticketidval).html().replace(/\s*/g, '').replace(/&nbsp;/g, '');
            var overCost = parseFloat(agreedAssessedCosts) + parseFloat(deliveryCosts);
            var blr = $('#ctl00_MainContent_lvAllTickets_tbReceivedBLR_' + ticketidval).val().replace(/\s*/g, '');
            var usdInBlr = $('#ctl00_MainContent_lvAllTickets_tbReceivedUSDCourse_' + ticketidval).val().replace(/\s*/g, '') * $('#ctl00_MainContent_lvAllTickets_tbReceivedUSD_' + ticketidval).val().replace(/\s*/g, '');
            var eurInBlr = $('#ctl00_MainContent_lvAllTickets_tbReceivedEURCourse_' + ticketidval).val().replace(/\s*/g, '') * $('#ctl00_MainContent_lvAllTickets_tbReceivedEUR_' + ticketidval).val().replace(/\s*/g, '');
            var rurInBlr = $('#ctl00_MainContent_lvAllTickets_tbReceivedRURCourse_' + ticketidval).val().replace(/\s*/g, '') * $('#ctl00_MainContent_lvAllTickets_tbReceivedRUR_' + ticketidval).val().replace(/\s*/g, '');
            var difference = (parseFloat(blr) + parseFloat(usdInBlr) + parseFloat(eurInBlr) + parseFloat(rurInBlr)) - parseFloat(overCost);
            if (parseFloat(difference) >= 0) {
                $('#ctl00_MainContent_lvAllTickets_lblRaznica_' + ticketidval).html('<span style="color: green;" class="difference">' + difference.toString().replace(/(\d)(?=(\d\d\d)+([^\d]|$))/g, '$1 ') + '</span>');
            } else {
                $('#ctl00_MainContent_lvAllTickets_lblRaznica_' + ticketidval).html('<span style="color: red;" class="difference">' + difference.toString().replace(/(\d)(?=(\d\d\d)+([^\d]|$))/g, '$1 ') + '</span>');
            }
            //Colored()
            if (notFirst) {
                if ((0 - parseFloat(overCost)) == parseFloat(difference)) {
                    $("#ctl00_MainContent_lvAllTickets_Tr2_" + ticketidval).attr('class', 'money yellowRow');
                    $("#ctl00_MainContent_lvAllTickets_MoneyStatuses_" + ticketidval + "_ddlMoneyStatuses_" + ticketidval).val("3");
                }

                if ("<%= AutoChangeDeliveryStatus%>" == "true" && $("#ctl00_MainContent_lvAllTickets_MoneyStatuses_" + ticketidval + "_ddlMoneyStatuses_" + ticketidval).val() !== "7") {
                    if ((0 - parseFloat(overCost)) != parseFloat(difference)) {
                        $("#ctl00_MainContent_lvAllTickets_Tr2_" + ticketidval).attr('class', 'money greenRow');
                        $("#ctl00_MainContent_lvAllTickets_MoneyStatuses_" + ticketidval + "_ddlMoneyStatuses_" + ticketidval).val("5");
                    }
                }
            }

            CalculateBalanceBLR(); //пересчет нужного бабла
        }

        function CalculateRecivedUSD() {
            var overUsd = 0;
            $(".recivedUSDTd").each(function () {
                var usd = parseFloat($(this).find(".recivedUSD").val().replace(/\s*/g, ''));
                overUsd += usd;
            });
            $("#resivedUSDOver").html(overUsd.toString().replace(/(\d)(?=(\d\d\d)+([^\d]|$))/g, '$1 '));
            if (overUsd != 0) {
                $("#resivedUSDOverDiv").show();
            } else {
                $("#resivedUSDOverDiv").hide();
            }
        }

        function CalculateRecivedEUR() {
            var overEur = 0;
            $(".recivedEURTd").each(function () {
                var eur = parseFloat($(this).find(".recivedEUR").val().replace(/\s*/g, ''));
                overEur += eur;
            });
            $("#resivedEUROver").html(overEur.toString().replace(/(\d)(?=(\d\d\d)+([^\d]|$))/g, '$1 '));
            if (overEur != 0) {
                $("#resivedEUROverDiv").show();
            } else {
                $("#resivedEUROverDiv").hide();
            }
        }

        function CalculateRecivedRUR() {
            var overRur = 0;
            $(".recivedRURTd").each(function () {
                var rur = parseFloat($(this).find(".recivedRUR").val().replace('&nbsp;', '').replace(',', '.'));
                overRur += rur;
            });

            console.log(overRur);

            $("#resivedRUROver").html(overRur.toString().replace(/(\d)(?=(\d\d\d)+([^\d]|$))/g, '$1 '));
            if (overRur != 0) {
                $("#resivedRUROverDiv").show();
            } else {
                $("#resivedRUROverDiv").hide();
            }
        }

        function CalculateBalanceBLR() {
            var overBlr = $("span[id *= 'lblAgreedAssessedWithDeliveryCost']").html().replace('&nbsp;', '').replace(',', separator);
            overBlr = parseFloat(overBlr);

            $(".moneyTable tr.money").each(function () {
                var rur = parseFloat($(this).find(".recivedRUR").val().replace('&nbsp;', '').replace(',', separator));
                var rurCourse = parseFloat($(this).find(".recivedRURCource").val().replace('&nbsp;', '').replace(',', separator));

                var eur = parseFloat($(this).find(".recivedEUR").val().replace('&nbsp;', '').replace(',', separator));
                var eurCourse = parseFloat($(this).find(".recivedEURCource").val().replace('&nbsp;', '').replace(',', separator));

                var usd = parseFloat($(this).find(".recivedUSD").val().replace('&nbsp;', '').replace(',', separator));
                var usdCourse = parseFloat($(this).find(".recivedUSDCource").val().replace('&nbsp;', '').replace(',', separator));

                var blr = $(this).find(".recivedBLR").val().replace('&nbsp;', '').split("&nbsp;").join("").replace(',', separator).replace(/ /g, '');
                var reg = /[0-9.]/gm;
                var cblr = blr.match(reg).join('');

                

                // overBlr = overBlr - rur * rurCourse - eur * eurCourse - usd * usdCourse;
                overBlr = Math.round((overBlr - parseFloat(cblr)) * 100) / 100;
            });

            $("#balanceBLR").html(overBlr).toLocaleString();
            $("#balanceBLRDiv").show();
        }

        function CalculateAgreedAssessedWithDeliveryCost() {
            var overalAgreedAssessedCoast = 0;
            var overalDeliveryCoast = 0;

            $(".data-agreedAssessedCost").each(function () {
                overalAgreedAssessedCoast += parseFloat($(this).html().replace(/\s*/g, '').replace(/&nbsp;/g, ''));
            });

            $(".data-deliveryCost").each(function () {
                overalDeliveryCoast += parseFloat($(this).html().replace(/\s*/g, '').replace(/&nbsp;/g, ''));
            });
            var overalCost = overalAgreedAssessedCoast + overalDeliveryCoast;
            //$("#ctl00_MainContent_lblAgreedAssessedWithDeliveryCost").html(overalCost.toString().replace(/(\d)(?=(\d\d\d)+([^\d]|$))/g, '$1 '));

            CalculateBalanceBLR(); //пересчет нужного бабла
        }

        function Scatter() {
            $(".money").each(function () {
                var ticketStatusId = $(this).find("select option:selected")[1].value;
                if (ticketStatusId != "7") {
                    var textboxBlr = $(this).find(".recivedBLR");
                    var textboxBlrVal = parseFloat(textboxBlr.val().replace(/\s*/g, '').replace(/&nbsp;/g, ''));
                    var residueBlr = parseFloat($(this).find(".difference").html().toString().replace(/\s*/g, '').replace(/&nbsp;/g, ''));
                    var id = $(this).find(".data-id").val();
                    if (residueBlr < 0) {
                        textboxBlr.val((textboxBlrVal + (0 - residueBlr)).toString().replace(/(\d)(?=(\d\d\d)+([^\d]|$))/g, '$1 '));
                        setTimeout(CalculateDifference(id, true), 1000);
                        setTimeout(saveBLR(id), 1000);
                    }
                }
            });

        }
    </script>

    <script type="text/javascript">
        $(function () {
            CalculateRecivedUSD();
            CalculateRecivedEUR();
            CalculateRecivedRUR();
            CalculateAgreedAssessedWithDeliveryCost();
            $(".data-id").each(function () {
                CalculateDifference($(this).val(), false);
            });
        });
    </script>
</asp:Content>
