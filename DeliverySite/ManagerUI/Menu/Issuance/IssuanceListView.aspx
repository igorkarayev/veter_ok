<%@ Page Title="" Language="C#" MasterPageFile="~/ManagerMasterPage.master" AutoEventWireup="true" CodeBehind="IssuanceListView.aspx.cs" Inherits="Delivery.ManagerUI.Menu.Issuance.IssuanceListView" %>
<%@ Import Namespace="Delivery.BLL.StaticMethods" %> 
<%@ Import Namespace="Delivery.BLL.Helpers" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <script type="text/javascript">
        $(function () {
           //чекаем все чекбоксы
           $('#ctl00_MainContent_lvAllTickets_chkboxSelectAll').change(function () {
               var checkboxes = $(this).closest('table').find(':checkbox');
               if ($(this).is(':checked')) {
                   checkboxes.attr('checked', 'checked');
               } else {
                   checkboxes.removeAttr('checked');
               }
           });
            
           $(".moneyMask").maskMoney();
        });
        
        $(function () {
            $(window).scroll(function () {
                var top = $(document).scrollTop();
                if (top < 300) $(".floatMenu").css({ position: 'relative', border: 'none', backgroundColor: '#ffffff' });
                else $(".floatMenu").css({ top: '0', position: 'fixed', borderBottom: '#025070 2px solid', borderRight: '#025070 2px solid', borderLeft: '#025070 2px solid', backgroundColor: '#0C72AF', borderRadius: '0 0 10px 10px' });
            });
        });
   </script>
    <h3 class="h3custom" style="margin-top: 0;">Расчетный лист <asp:Label runat="server" ID="lblListInfo"></asp:Label></h3>
    <div>
        <asp:Panel ID="pnlSearschResult" runat="server" style="margin-top: -52px; float: right; margin-right: 5px;">
            <i class="small-informer" >Заявок в расчетном листе:<b> <asp:Label runat="server" ID="lblAllResult"></asp:Label></b></i>  
        </asp:Panel>
        <asp:Panel class="infoBlock2" runat="server" ID="pnlResultPanel">
            <i>В расчетном листе инвалюты приехало:</i>
                <b><asp:Label runat="server" ID="lblReceivedUSDOver" Text="0"></asp:Label></b> USD;
                <b><asp:Label runat="server" ID="lblReceivedEUROver" Text="0"></asp:Label></b> EUR;
                <b><asp:Label runat="server" ID="lblReceivedRUROver" Text="0"></asp:Label></b> RUR;<br/>

            <i>В пересчете на руб.по курсам клиента это:</i> <b><asp:Label runat="server" ID="lblReceivedBLROverWithCourse" Text="0"></asp:Label></b> BLR;<br/>

            <i>Всего за услугу:</i> <b style="color: green"><asp:Label runat="server" ID="lblOverGruzobozCost" Text="0"></asp:Label></b> BLR;<br/><br/> 
            

            <i>Итого к выдаче по расчетному листу:</i>  <asp:Label runat="server" ID="lblReceivedBLRUser" Text="0"></asp:Label>
                <b><asp:Label runat="server" ID="lblReceivedUSDUser" Text="0"></asp:Label></b> USD;
                <b><asp:Label runat="server" ID="lblReceivedEURUser" Text="0"></asp:Label></b> EUR;
                <b><asp:Label runat="server" ID="lblReceivedRURUser" Text="0"></asp:Label></b> RUR;
            
                                 
        </asp:Panel>
        <br/>

        <asp:ListView runat="server" ID="lvAllTickets">
        <LayoutTemplate>
            <table runat="server" id="Table1" class="table tableViewClass tableClass colorTable">
                <tr style="background-color: #EECFBA;">
                    <th>
                        <asp:CheckBox ID="chkboxSelectAll" runat="server"/>
                    </th>
                    <th>
                        ID
                    </th>
                    <th>
                        UID
                    </th>
                    <th>
                        DID
                    </th>
                    <th>
                        Оцн/Согл + дост.
                    </th>
                    <th>
                        BLR
                    </th>
                    <th>
                        USD
                    </th>
                    <th>
                        EUR
                    </th>
                    <th>
                        RUR
                    </th>
                    <th>
                        За услугу
                    </th>
                    <th style="width: 250px;">
                        К выдаче
                    </th>
                    <th>
                        Статаус
                    </th>
                </tr>
                <tr runat="server" id="itemPlaceholder"></tr>
            </table>
        </LayoutTemplate>
        <ItemTemplate>
            <asp:TableRow ID="Tr2" runat="server" CssClass='<%# OtherMethods.TicketColoredStatusRows(Eval("StatusID").ToString()) %>'>
                <asp:TableCell id="Td7" runat="server">
                    <asp:CheckBox ID="cbSelect" AutoPostBack="false" runat="server" ViewStateMode = "Enabled"/>
                </asp:TableCell>

                <asp:TableCell id="Td4" runat="server">
                    <asp:HyperLink ID="HyperLink1" runat="server" NavigateUrl='<%#"~/ManagerUI/Menu/Tickets/UserTicketEdit.aspx?id="+Eval("SecureID") + "&page=issuance"%>'>
                        <asp:Label ID="lblSecureID" runat="server" Text='<%#Eval("SecureID") %>' />
                    </asp:HyperLink>
                </asp:TableCell>

                <asp:TableCell id="Td3" runat="server">
                    <asp:HyperLink ID="HyperLink2" runat="server" NavigateUrl='<%#"~/ManagerUI/Menu/Souls/ClientEdit.aspx?id="+Eval("UserID") %>'>
                        <asp:Label ID="Label5" runat="server" Text='<%#Eval("UserID") %>' />
                    </asp:HyperLink>
                </asp:TableCell>
                
                <asp:TableCell id="Td13" runat="server">
                    <asp:HyperLink ID="HyperLink3" runat="server" NavigateUrl='<%#"~/ManagerUI/Menu/Souls/DriverView.aspx?id="+Eval("DriverID") %>'>
                        <asp:Label ID="Label9" runat="server" Text='<%# DriversHelper.DriverIDConvert(Eval("DriverID").ToString()) %>' />
                    </asp:HyperLink>
                </asp:TableCell>

                <asp:TableCell id="Td6" runat="server">
                    <span style='color: <%# OtherMethods.AgreedAssessedDeliveryCostsColor(Eval("ID").ToString()) %>'>
                        <asp:Label ID="lblAgreedAssessedDeliveryCosts" runat="server" CssClass="courseLabelForIssuance" Text='<%# MoneyMethods.MoneySeparator(MoneyMethods.AgreedAssessedDeliveryCosts(Eval("ID").ToString())) %>'></asp:Label>
                    </span>
                </asp:TableCell>
                
                <asp:TableCell id="Td10" runat="server">
                    <asp:Label ID="tbReceivedBLR" runat="server" CssClass="moneyMask" Text='<%# MoneyMethods.MoneySeparator(MoneyMethods.IfMoneyNull(Eval("ID").ToString(), "BLR")) %>' Width="100px" />
                </asp:TableCell>
                                
                <asp:TableCell id="Td2" runat="server">
                    <asp:Label ID="tbReceivedUSDCourse" runat="server"  CssClass="moneyMask moneyCourseForIssuance" Width="40px" Text='<%# MoneyMethods.MoneySeparator(MoneyMethods.IfMoneyNull(Eval("ID").ToString(), "USD", true)) %>' /><br/>
                    <asp:Label ID="tbReceivedUSD" runat="server" CssClass="moneyMask" Text='<%# MoneyMethods.MoneySeparator(MoneyMethods.IfMoneyNull(Eval("ID").ToString(), "USD")) %>' Width="100px" />
                </asp:TableCell>

                <asp:TableCell id="Td9" runat="server">
                    <asp:Label ID="tbReceivedEURCourse" runat="server"  CssClass="moneyMask moneyCourseForIssuance"  Width="40px" Text='<%#  MoneyMethods.MoneySeparator(MoneyMethods.IfMoneyNull(Eval("ID").ToString(), "EUR", true)) %>'/><br/>
                    <asp:Label ID="tbReceivedEUR" runat="server" CssClass="moneyMask" Text='<%# MoneyMethods.MoneySeparator(MoneyMethods.IfMoneyNull(Eval("ID").ToString(), "EUR")) %>' Width="100px" />
                </asp:TableCell>
                
                <asp:TableCell id="Td0" runat="server">
                    <asp:Label ID="tbReceivedRURCourse" runat="server"  CssClass="moneyMask moneyCourseForIssuance"  Width="40px" Text='<%#  MoneyMethods.MoneySeparator(MoneyMethods.IfMoneyNull(Eval("ID").ToString(), "RUR", true)) %>'/><br/>
                    <asp:Label ID="tbReceivedRUR" runat="server" CssClass="moneyMask" Text='<%# MoneyMethods.MoneySeparator(MoneyMethods.IfMoneyNull(Eval("ID").ToString(), "RUR")) %>' Width="100px"/>
                </asp:TableCell>
                
                <asp:TableCell id="TableCell1" runat="server">
                    <asp:Label ID="Label1" runat="server" CssClass="courseLabelForIssuance" Text='<%# MoneyMethods.MoneySeparator(Eval("GruzobozCost").ToString()) %>'></asp:Label>
                </asp:TableCell>
                
                <asp:TableCell id="TableCell2" runat="server">
                    <asp:Label ID="Label2" runat="server" CssClass="courseLabelForIssuance" Text='<%# MoneyMethods.MoneyToIssuance(Eval("ID").ToString()) %>'></asp:Label>
                </asp:TableCell>

                <asp:TableCell id="Td1" runat="server">
                    <asp:Label ID="lblStatusID" runat="server" Text='<%# OtherMethods.TicketStatusToText(Eval("StatusID").ToString()) %>' />
                    <asp:HiddenField runat="server" ID="hfID" Value='<%# Eval("ID") %>'/>
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
            <div style="width: 100%; text-align: center; padding: 15px; color: red">Расчетный лист пуст...</div>
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
    <asp:Panel CssClass="floatMenu" runat="server" ID="pnlActions">
        <asp:Button ID="btnBack" OnClientClick="window.history.go(-1);return false;" runat="server" CssClass="btn btn-default" Text='Назад'/>  
        <asp:Button runat="server" ID="btnDelete" Text="Удалить лист" class="btn btn-default" OnClientClick="return ifEnyChecked();"/>
        <asp:Button runat="server" ID="btnClose" Text="Закрыть лист" class="btn btn-default" OnClientClick="return ifEnyChecked();"/>
        <asp:Button runat="server" ID="btnReopen" Text="Переоткрыть лист" CssClass="btn btn-default" OnClientClick="return ifEnyChecked();"/>
        <asp:Button runat="server" ID="btnAction" Text="Удалить нечекнутые заявки" CssClass="btn btn-default" OnClientClick="return ifEnyChecked();"/>
    </asp:Panel>
</asp:Content>
