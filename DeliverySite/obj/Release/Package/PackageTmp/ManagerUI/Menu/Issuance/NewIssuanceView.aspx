<%@ Page Title="" Language="C#" MasterPageFile="~/ManagerMasterPage.master" AutoEventWireup="true" CodeBehind="NewIssuanceView.aspx.cs" Inherits="Delivery.ManagerUI.Menu.Issuance.NewIssuanceView" %>
<%@ Register TagPrefix="grb" TagName="Comment" Src="~/ManagerUI/Controls/Comment.ascx" %>
<%@ Import Namespace="System.Net" %>
<%@ Import Namespace="Delivery.BLL.StaticMethods" %>
<%@ Import Namespace="Delivery.BLL.Helpers" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <script type="text/javascript">
        $(function () {
            $.datepicker.setDefaults($.datepicker.regional['ru']);
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
            var checkboxes = $("table[id*='tableResult'] input[type*='checkbox']:checked");//$('#ctl00_MainContent_lvAllTickets_chkboxSelectAll').closest('table').find(':checkbox');
            console.log(checkboxes);
            if (checkboxes.length == 0) {
                alert('Выберите хотябы одну заявку!');
                return false;
            } else {
                return confirm('Вы уверены?');
            }
        }

        /*function ifEnyChecked() {
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
        }*/
        
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
    <h3 class="h3custom" style="margin-top: 0; min-height: 20px;">
        <div style="float:left;">Расчётный лист</div>
        <asp:Panel ID="pnlSearschResult" runat="server" style="width:100%;">
            <i class="small-informer" >( Заявок в рассчётном листе:<b> <asp:Label runat="server" ID="lblAllResult"></asp:Label></b> )</i>  
        </asp:Panel>
    </h3>
    
    <asp:Panel runat="server" ID="SearchPanel" CssClass="mainFont"  DefaultButton="btnSearch">
        <table class="table searchPanel" style="margin-left:auto; margin-right: auto;">
            <tr>
                <td style="text-align: left; vertical-align: top" Width="200px">
                    UID:<br/>
                    <asp:DropDownList runat="server" ID="sddlUID" CssClass="searchField ddl-control" Width="180px"></asp:DropDownList>
                </td>
                <td style="text-align: left; vertical-align: top" Width="200px">
                    Тип:<br/>
                    <asp:DropDownList runat="server" ID="sddlProfileType" CssClass="searchField ddl-control" Width="180px"></asp:DropDownList>
                </td>
                <td style="text-align: right; vertical-align: bottom" Width="200px">
                    <asp:Button runat="server" Text="Сброс" CssClass="btn btn-default" ID="btnReload"/>
                    <asp:Button runat="server" Text="Искать" CssClass="btn btn-default" ID="btnSearch"/>
                </td>
            </tr>
        </table>
    </asp:Panel><br/>
        
    <div class="loginError" id="errorDiv" style="width: 90%">
        <asp:Label runat="server" ID="lblNotif" ForeColor="White"></asp:Label>
    </div>
    <asp:Panel class="infoBlock2" runat="server" ID="pnlResultPanel">
        <i>Всего за услугу:</i> <b style="color: green"><asp:Label runat="server" ID="lblOverGruzobozCost" Text="0"></asp:Label></b> BLR<br/>
        <i>Итого к выдаче по расчетному листу:</i>  <asp:Label runat="server" ID="lblReceivedBLRUser" Text="0"></asp:Label>
    </asp:Panel>
    <asp:ListView runat="server" ID="lvAllTickets"  DataKeyNames="ID" ClientIDMode="Predictable" 
    ClientIDRowSuffix="ID">
        <LayoutTemplate>
            <table runat="server" id="tableResult" class="table tableViewClass tableClass colorTable">
                <tr style="background-color: #EECFBA;">
                    <th>
                        <asp:CheckBox ID="chkboxSelectAll" runat="server"/>
                    </th>
                    <th>
                        Дата отпр.
                    </th>
                    <th>
                        Профиль
                    </th>
                    <th>
                        ID/стат.
                    </th>
                    <th>
                        Оцен/Согл + за дост.
                    </th>
                    <th>
                        За услугу
                    </th>
                    <th>
                        Оцен/Согл + за дост. - за усл.
                    </th>
                    <th style="width: 150px;">
                        Примечание
                    </th>
                    <th style="width: 150px;">
                        Ком. менеджера
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
                    <asp:Label ID="Label10" runat="server" Text='<%# OtherMethods.DateConvert(Eval("DeliveryDate").ToString()) %>' />
                </asp:TableCell>

                <asp:TableCell id="Td3" runat="server">
                    <asp:HyperLink ID="HyperLink2" runat="server" NavigateUrl='<%#"~/ManagerUI/Menu/Souls/ProfileEdit.aspx?id="+Eval("UserProfileID") %>'>
                        <asp:Label ID="Label5" runat="server" Text='<%# UsersProfilesHelper.UserProfileIDToFullFamilyOrCompanyname(Eval("UserProfileID").ToString()) %>' />
                    </asp:HyperLink>
                </asp:TableCell>
                
                <asp:TableCell id="Td13" runat="server">
                        <asp:HyperLink ID="HyperLink1" runat="server" NavigateUrl='<%#"~/ManagerUI/Menu/Tickets/UserTicketEdit.aspx?id="+Eval("SecureID") + "&page=newissuanceview&" + OtherMethods.LinkBuilder(String.Empty, 
                    String.Empty, String.Empty, String.Empty, String.Empty, String.Empty , String.Empty, String.Empty, String.Empty)%>'>
                        <asp:Label ID="lblSecureID" runat="server" Text='<%#Eval("SecureID") %>' />
                        <asp:HiddenField runat="server" ID="hfID" Value='<%# Eval("ID") %>'/>
                    </asp:HyperLink><br/>
                    <asp:Label ID="Label3" runat="server" Text='<%# OtherMethods.TicketStatusToText(Eval("StatusID").ToString()) %>' />

                    <asp:HiddenField runat="server" ID="HiddenField1" Value='<%# Eval("ID") %>'/>
                    <asp:HiddenField runat="server" ID="hfDriverID" Value='<%# Eval("DriverID") %>'/>
                    <asp:HiddenField runat="server" ID="hfStatusID" Value='<%# Eval("StatusID") %>'/>
                    <asp:HiddenField runat="server" ID="hfStatusIDOld" Value='<%# Eval("StatusIDOld") %>'/>
                    <asp:HiddenField runat="server" ID="hfStatusDescription" Value='<%# Eval("StatusDescription") %>'/>
                    <asp:HiddenField runat="server" ID="hfAdmissionDate" Value='<%# Eval("AdmissionDate") %>'/>
                </asp:TableCell>
                
                <asp:TableCell id="TableCell1" runat="server">
                    <asp:Label ID="lblAgreedAssessedDeliveryCosts" runat="server" 
                        CssClass='<%# OtherMethods.AgreedAssessedDeliveryCostsColor(Eval("ID").ToString()) %>'
                        Text='<%# MoneyMethods.MoneySeparator(MoneyMethods.AgreedAssessedDeliveryCosts(Eval("ID").ToString())) %>'>
                    </asp:Label>
                </asp:TableCell>

                <asp:TableCell id="Td5" runat="server">
                    <asp:Label ID="Label1" runat="server"  
                        Text='<%# MoneyMethods.MoneySeparator(Convert.ToDecimal(Eval("GruzobozCost"))) %>'></asp:Label>
                </asp:TableCell>

                <asp:TableCell id="Td8" runat="server">
                    <asp:Label ID="Label2" runat="server"  
                        Text='<%# MoneyMethods.MoneySeparator(Convert.ToDecimal(MoneyMethods.AgreedAssessedDeliveryCosts(Eval("ID").ToString())) - Convert.ToDecimal(Eval("GruzobozCost"))) %>'></asp:Label>
                </asp:TableCell>
                
                <asp:TableCell id="Td1" runat="server">
                    <div class="noteDiv" style="width: 100%; margin-bottom: 10px; font-weight: normal; font-style: normal; text-align: left;">
                        <asp:Label ID="lblStatusID" runat="server" Text='<%# Eval("Note").ToString() %>' />
                    </div>
                </asp:TableCell>
                    
                <asp:TableCell id="TableCell4" runat="server">
                    <grb:Comment 
                        ID="Comment" 
                        runat="server" 
                        TicketID='<%# Eval("ID").ToString() %>'
                        CommentValue='<%# Eval("Comment").ToString() %>'
                        ListViewControlFullID='#ctl00_MainContent_lvAllTickets_tableResult'
                        PageName = "NewIssuanceView"/>
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

    <div class="controlsBlock">
        <asp:Button runat="server" CssClass="btn btn-default" OnClientClick="return ifEnyChecked();" ID="bCloseList" Text="Закрыть заявки"/>
        <asp:Button runat="server" CssClass="btn btn-default" OnClientClick="return ifEnyChecked();" ID="bPrintList" Text="Печать заявок"/>
    </div>
            
    <div class="floatMenuLeft lblStatus" style="margin-left: 0">
        <asp:Label runat="server" ID="lblStatus"></asp:Label>
    </div>

</asp:Content>
