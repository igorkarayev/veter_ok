<%@ Page Title="" Language="C#" MasterPageFile="~/ManagerMasterPage.master" AutoEventWireup="true" CodeBehind="IssuanceView.aspx.cs" Inherits="Delivery.ManagerUI.Menu.Issuance.IssuanceView" %>
<%@ Import Namespace="System.Net" %>
<%@ Import Namespace="Delivery.BLL.StaticMethods" %>
<%@ Import Namespace="Delivery.BLL.Helpers" %>
<%@ Register TagPrefix="grb" TagName="GruzobozCost" Src="~/ManagerUI/Controls/GruzobozCost.ascx" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <script type="text/javascript">
        $(function () {
            $.datepicker.setDefaults($.datepicker.regional['ru']);
            $("#<%= stbDeliveryDate1.ClientID %>")
                .datepicker({ nextText: "", prevText: "", changeMonth: true, changeYear: true })
                .mask("99-99-9999");
            $("#<%= stbDeliveryDate2.ClientID %>")
                .datepicker({ nextText: "", prevText: "", changeMonth: true, changeYear: true })
                .mask("99-99-9999");
            

            //чекаем все чекбоксы
            $('#ctl00_MainContent_lvAllTickets_chkboxSelectAll').change(function () {
                var checkboxes = $(this).closest('table').find(':checkbox');
                if ($(this).is(':checked')) {
                    checkboxes.attr('checked', 'checked');
                } else {
                    checkboxes.removeAttr('checked');
                }
            });
            
            //отображение сообщения с ошибкой
            if ($('#<%= lblNotif.ClientID%>').html() == "") {
                $('#errorDiv').hide();
            } else {
                $('#errorDiv').show();
            }
            
            if ($.trim($("#<%= lblStatus.ClientID %>").html()) != "") {
                $('.lblStatus').show();
                setTimeout(function () { $('.lblStatus').hide(); }, 3000);
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
        
        $(function () {
            $(window).scroll(function () {
                var top = $(document).scrollTop();
                if (top > 300 && $(".tableViewClass").height() >= $(window).height())
                    $(".floatMenu").css({ top: '0', position: 'fixed', borderBottom: '#025070 2px solid', borderRight: '#025070 2px solid', borderLeft: '#025070 2px solid', backgroundColor: '#0C72AF', borderRadius: '0 0 10px 10px', padding: '10px' });
                else
                    $(".floatMenu").css({ position: 'relative', border: 'none', padding: '0px', borderRadius: '0', backgroundColor: 'transparent' });
            });
        });
    </script>
    <h3 class="h3custom" style="margin-top: 0;">Заявки к выдаче</h3>
    <div>
        <asp:Panel runat="server" ID="SearchPanel" CssClass="mainFont"  DefaultButton="btnSearch">
            <table class="table searchPanel" style="margin-left:auto; margin-right: auto;">
                <tr>

                    <td style="vertical-align: top; padding-top: 8px;">
                        UID:
                    </td>
                    <td style="vertical-align: top">
                        <asp:DropDownList runat="server" ID="sddlUID" CssClass="searchField ddl-control" Width="180px"></asp:DropDownList>
                    </td>

                    <td style="text-align: right; vertical-align: top">
                        Дата отправки с:&nbsp;<asp:TextBox runat="server" ID="stbDeliveryDate1" CssClass="searchField form-control" Width="70px"></asp:TextBox>
                        &nbsp;по:&nbsp;<asp:TextBox runat="server" ID="stbDeliveryDate2" CssClass="searchField form-control" Width="70px"></asp:TextBox>
                        <asp:Button runat="server" Text="Сброс" CssClass="btn btn-default" ID="btnReload"/>
                        <asp:Button runat="server" Text="Искать" CssClass="btn btn-default" ID="btnSearch"/>  <br/>
                        <asp:Panel ID="pnlSearschResult" runat="server" style="margin-top:10px;">
                            <i class="small-informer" >Найдено:<b> <asp:Label runat="server" ID="lblAllResult"></asp:Label></b></i>  
                        </asp:Panel>
                    </td>
                </tr>
            </table>
        </asp:Panel><br/>
        
        <div class="loginError" id="errorDiv" style="width: 90%">
            <asp:Label runat="server" ID="lblNotif" ForeColor="White"></asp:Label>
        </div>

        <asp:ListView runat="server" ID="lvAllTickets"  DataKeyNames="ID" ClientIDMode="Predictable" 
        ClientIDRowSuffix="ID">
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
                            ILID
                        </th>
                        <th>
                            Куда
                        </th>
                        <th>
                            Нпр.
                        </th>
                        <th>
                            Дата приемки
                        </th>
                        <th>
                            Дата отправки
                        </th>
                        <th>
                            Статаус
                        </th>
                        <th>
                            Общая стоимость (с/о+дост.)
                        </th>
                        <th>
                            За услугу
                        </th>
                        <th>
                            Приехало
                        </th>
                        <th>
                            Комм. менеджера
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
                        <asp:HyperLink ID="HyperLink1" runat="server" NavigateUrl='<%#"~/ManagerUI/Menu/Tickets/UserTicketEdit.aspx?id="+Eval("SecureID") + "&page=issuance&" + OtherMethods.LinkBuilder(String.Empty, 
                        String.Empty, String.Empty, String.Empty, String.Empty, String.Empty ,stbDeliveryDate1.Text, stbDeliveryDate2.Text, String.Empty)%>'>
                            <asp:Label ID="lblSecureID" runat="server" Text='<%#Eval("SecureID") %>' />
                            <asp:HiddenField runat="server" ID="hfID" Value='<%# Eval("ID") %>'/>
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
                
                    <asp:TableCell id="TableCell1" runat="server">
                        <asp:HyperLink ID="HyperLink4" runat="server" NavigateUrl='<%#"~/ManagerUI/Menu/Issuance/IssuanceListView.aspx?id="+Eval("IssuanceListID") %>'>
                            <asp:Label ID="Label1" runat="server" Text='<%# OtherMethods.IssuanceListIdToString(Eval("IssuanceListID").ToString()) %>' />
                        </asp:HyperLink>
                    </asp:TableCell>

                    <asp:TableCell id="Td5" runat="server">
                        <asp:Label ID="Label2" runat="server" Text='<%# CityHelper.CityIDToCityName(Eval("CityID").ToString()) %>' />
                    </asp:TableCell>
                
                    <asp:TableCell id="Td8" runat="server">
                        <asp:Label ID="Label3" runat="server" Text='<%# CityHelper.CityToTrack(Convert.ToInt32(Eval("CityID")), Eval("ID").ToString()) %>' />
                    </asp:TableCell>
                
                    <asp:TableCell id="Td6" runat="server">
                        <asp:Label ID="Label4" runat="server" Text='<%# OtherMethods.DateConvert(Eval("AdmissionDate").ToString()) %>' />
                    </asp:TableCell>

                    <asp:TableCell id="Td12" runat="server">
                        <asp:Label ID="Label8" runat="server" Text='<%# OtherMethods.DateConvert(Eval("DeliveryDate").ToString()) %>' />
                    </asp:TableCell>

                    <asp:TableCell id="Td1" runat="server">
                        <asp:Label ID="lblStatusID" runat="server" Text='<%# OtherMethods.TicketStatusToText(Eval("StatusID").ToString()) %>' />
                    </asp:TableCell>
                    
                    <asp:TableCell id="TableCell2" runat="server">
                        <asp:Label ID="lblAgreedAssessedDeliveryCosts" runat="server"  Text='<%# MoneyMethods.MoneySeparator(MoneyMethods.AgreedAssessedDeliveryCosts(Eval("ID").ToString())) %>'></asp:Label>
                    </asp:TableCell>
                    
                    <asp:TableCell id="Td16" runat="server">
                        <grb:GruzobozCost 
                            ID="GruzobozCost" 
                            runat="server" 
                            TicketID='<%# Eval("ID").ToString() %>'
                            UserID='<%# Eval("UserID").ToString() %>'
                            GruzobozCostValue='<%# Eval("GruzobozCost").ToString() %>'
                            ListViewControlFullID='#ctl00_MainContent_lvAllTickets'/>
                    </asp:TableCell>
                    
                    <asp:TableCell id="TableCell3" runat="server" style="text-align: left;">
                        <asp:Label ID="Label6" runat="server" Text='<%# MoneyMethods.ReceivedMoneyToIssuance(Eval("ID").ToString()) %>'></asp:Label>
                    </asp:TableCell>
                    
                    <asp:TableCell id="TableCell4" runat="server">
                        <div class="noteDiv" style="width: 100%; margin-bottom: 10px; font-weight: normal; font-style: normal; text-align: left;">
                            <asp:Label ID="Label7" runat="server" Text='<%# WebUtility.HtmlDecode(Eval("Comment").ToString()) %>'></asp:Label>
                        </div>
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
    <asp:Panel CssClass="floatMenu" runat="server" ID="pnlActions">
        <asp:Label runat="server" ID="lblStstusDescription"><span style="color: #000; font-size: 12px; font-weight: bold;">Добавить заявки к расчетному листу </span> </asp:Label>
        <asp:DropDownList runat="server" ID="ddlIssuanceLists" CssClass="ddl-control"/>
        <asp:Button runat="server" ID="btnAction" Text="Выполнить" CssClass="btn btn-default" OnClientClick="return ifEnyChecked();"/>  
    </asp:Panel>
    
    <div class="floatMenuLeft lblStatus" style="margin-left: 0">
        <asp:Label runat="server" ID="lblStatus"></asp:Label>
    </div>

</asp:Content>
