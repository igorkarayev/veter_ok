<%@ Page Language="C#" MasterPageFile="~/ManagerMasterPage.master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="Delivery.ManagerUI.Default" %>
<%@ Import Namespace="System.Security.Policy" %>
<%@ Import Namespace="Delivery.Resources" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <script>
        $(function() {
            if ($("#panelContainer").find(".infoPanel").length <= 0) {
                $(".infoPanelTitle").css("display", "none");
            }

            if ($.trim($("#<%= lblStatus.ClientID %>").html()) != "") {
                $('.lblStatus').show();
                setTimeout(function () { $('.lblStatus').hide(); }, 5000);
            }
        });
    </script>

    <div class="floatMenuLeft lblStatus" style="margin-left: 0; display: none; padding: 10px;">
        <asp:Label runat="server" ID="lblStatus"></asp:Label>
    </div>

    <h3 class="h3custom" style="margin-top: 0;">Кабинет сотрудника</h3>
    <div style="text-align: center;" id="panelContainer">
        <table class="table" style="width: 100%">
            <tr>
                <td style="padding: 12px 15px 5px 15px; vertical-align: top; width: 20%;">
                    <asp:LinkButton OnClick="lbRestartSubMemcache_Click" runat="server" ID="lbRestartSubMemcache" Visible="False" ForeColor="Red">Перезагрузить</asp:LinkButton>
                </td>
                <td style="padding: 5px; text-align: left; vertical-align: top">
                    <table class="table">
                        <tr>
                            <td colspan="2" style="padding-bottom: 20px; font-size: 16px">
                                Добро пожаловать, <i style="font-weight: bold"><asp:Label runat="server" ID="lblUserName"></asp:Label>!</i>
                            </td>
                        </tr>
                        <tr>
                            <td style="padding-right: 10px;">
                                Номер вашего аккаунта:
                            </td>
                            <td style="font-weight: bold; font-style: italic; color: red">
                                <asp:Label runat="server" ID="lblUID"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td style="padding-right: 10px;">
                                Ваш логин:
                            </td>
                            <td style="font-weight: bold; font-style: italic;">
                                <asp:Label runat="server" ID="lblLogin"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td style="padding-right: 10px;">
                                Ваш e-mail:
                            </td>
                            <td style="font-weight: bold; font-style: italic;">
                                <asp:Label runat="server" ID="lblEmail"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td style="padding-right: 10px;">
                                Ваша роль:
                            </td>
                            <td style="font-weight: bold; font-style: italic;">
                                <asp:Label runat="server" ID="lblRole"></asp:Label>
                            </td>
                        </tr>
                    </table>
                </td>
                <td style="padding: 10px; border-left: 2px solid #0c6792; vertical-align: top; text-align: left; width: 350px; font-size: 12px">
                    <div style="" class="lblNewsBlockTitle">последняя новость</div>
                    <div style="padding-left: 15px; padding-bottom: 10px;">
                        <asp:Label runat="server" ID="lblNewsDate" CssClass="lblNewsDateOnManagerDefault"></asp:Label>
                        <asp:HyperLink ID="hlNewsTitle" runat="server" CssClass="lblNewsTitleOnManagerDefault">
                            <asp:Label runat="server" ID="lblNewsTitle"></asp:Label>
                        </asp:HyperLink><br/>
                        <asp:Label runat="server" ID="lblNewsText" CssClass="lblNewsTextOnManagerDefault"></asp:Label>
                    </div>
                </td>
            </tr>
        </table>
        
        <div id="staticInfo" runat="server">
            <div style="width: 95%; text-align: center; margin-bottom: 10px; margin-top: 20px; margin-left: auto; margin-right: auto;"  class="infoBlock3 infoPanelTitle">
                 Ниже приведена краткая статистика по сайту:
            </div>

            <div style="display: inline-block; margin-top: 10px;" class="infoBlock2-grey infoPanel" runat="server" id="pnlUsers" Visible="false">
                <b style="font-size: 14px;">Клиенты</b> <span style="font-size: 10px">(всего <asp:Label runat="server" ID="lblAllUsers"></asp:Label>)</span>
                <hr class="styleHR"/>
                <table class="table" style="text-align: left; margin-left: 15px;">
                    <tr>
                        <td>
                            Активированных:
                        </td>
                        <td>&nbsp;</td>
                        <td>
                            <b style='color: green'><asp:Label runat="server" ID="lblActiveUsers"></asp:Label></b>
                        </td>
                    </tr>
                    
                    <tr>
                        <td>
                            Не активированных:
                        </td>
                        <td>&nbsp;</td>
                        <td>
                            <b style='color: #C6AD07'><asp:Label runat="server" ID="lblNewUsers"></asp:Label></b>
                        </td>
                    </tr>

                    <tr>
                        <td>
                            Заблокированных:
                        </td>
                        <td>&nbsp;</td>
                        <td>
                            <b style='color: red'><asp:Label runat="server" ID="lblBlockedUsers"></asp:Label></b>
                        </td>
                    </tr>
                    
                </table>          
            </div>

            <div style="display: inline-block; margin-left: 20px; margin-top: 10px;" class="infoBlock2-grey infoPanel" runat="server" id="pnlTickets" Visible="false">
                <b style="font-size: 14px;">Заявки</b> <span style="font-size: 10px">(всего <asp:Label runat="server" ID="lblTicketsAll"></asp:Label>)</span>
                <hr class="styleHR"/>
                <table class="table" style="margin-top: -5px;">
                    <tr>
                        <td>
                            <table class="table" style="text-align: left; margin-left: 15px;">
                                <tr>
                                    <td>
                                        Создано за сегодня:
                                    </td>
                                    <td>&nbsp;</td>
                                    <td>
                                        <b style='color: gray'><asp:Label runat="server" ID="lblCreateToday"></asp:Label></b>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        Из них "<%= TicketStatusesResources.NotProcessed %>":
                                    </td>
                                    <td>&nbsp;</td>
                                    <td>
                                        <b style='color: red'><asp:Label runat="server" ID="lblNewToday"></asp:Label></b>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        Всего "<%= TicketStatusesResources.NotProcessed %>":
                                    </td>
                                    <td>&nbsp;</td>
                                    <td>
                                        <b style='color: red'><asp:Label runat="server" ID="lblNewAll"></asp:Label></b>
                                    </td>
                                </tr>
                           
                            </table>
                        </td>
                    
                        <td>
                            <table class="table" style="text-align: left; margin-left: 15px;">
                                 <tr>
                                    <td>
                                        Отправляются сегодня:
                                    </td>
                                    <td>&nbsp;</td>
                                    <td>
                                        <b style='color: #C6AD07'><asp:Label runat="server" ID="lblDeliveryToday"></asp:Label></b>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        Отправляются завтра:
                                    </td>
                                    <td>&nbsp;</td>
                                    <td>
                                        <b style='color: #C6AD07'><asp:Label runat="server" ID="lblDeliveryTomorow"></asp:Label></b>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        Всего "<%= TicketStatusesResources.OnTheWay %>":
                                    </td>
                                    <td>&nbsp;</td>
                                    <td>
                                        <b style='color: #C6AD07'><asp:Label runat="server" ID="lblInProgress"></asp:Label></b>
                                    </td>
                                </tr>
                            </table>
                        </td>

                        <td style="vertical-align: top">
                            <table class="table" style="text-align: left; margin-left: 15px;">
                                <tr>
                                    <td>
                                        Всего "<%= TicketStatusesResources.Delivered %>":
                                    </td>
                                    <td>&nbsp;</td>
                                    <td>
                                        <b style='color: green'><asp:Label runat="server" ID="lblDelivered"></asp:Label></b>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        Всего "<%= TicketStatusesResources.Processed %>":
                                    </td>
                                    <td>&nbsp;</td>
                                    <td>
                                        <b style='color: green'><asp:Label runat="server" ID="lblProcessed"></asp:Label></b>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        Всего "<%= TicketStatusesResources.Completed %>":
                                    </td>
                                    <td>&nbsp;</td>
                                    <td>
                                        <b style='color: blue'><asp:Label runat="server" ID="lblCompleted"></asp:Label></b>
                                    </td>
                                </tr>
                            </table> 
                        </td>
                    </tr>
                </table>       
            </div>
        
            <div style="margin-left: auto; margin-right: auto; margin-top: -5px;">
                <a href='<%= Page.ResolveUrl("~/ManagerUI/Menu/Souls/TitlesView.aspx")%>' class="not-underline">
                    <div style="display: inline-block;" class="infoBlock3-grey" runat="server" id="pnlCategory" Visible="false">
                        Наименований: <b style='color: green'><asp:Label runat="server" ID="lblTitles"></asp:Label></b>
                    </div>
                </a>
                
       
                <a href='<%= Page.ResolveUrl("~/ManagerUI/Menu/Souls/CategoryView.aspx")%>' class="not-underline">
                    <div style="display: inline-block;" class="infoBlock3-grey infoPanel" runat="server" id="pnlClients" Visible="false">
                        Категорий: <b style='color: green'><asp:Label runat="server" ID="lblCategory"></asp:Label></b>
                    </div>
                </a>

                <a href='<%= Page.ResolveUrl("~/ManagerUI/Menu/Souls/CityView.aspx")%>' class="not-underline">
                    <div style="display: inline-block;" class="infoBlock3-grey infoPanel" runat="server" id="pnlCity" Visible="false">
                        Нас. пунктов: <b style='color: green'><asp:Label runat="server" ID="lblCity"></asp:Label></b>
                    </div>
                </a>
        
                <a href='<%= Page.ResolveUrl("~/ManagerUI/Menu/Souls/TracksView.aspx")%>' class="not-underline">
                    <div style="display: inline-block;" class="infoBlock3-grey infoPanel" runat="server" id="pnlTracks" Visible="false">
                        Направлений: <b style='color: green'><asp:Label runat="server" ID="lblTracks"></asp:Label></b>
                    </div>
                </a>
                
                <a href='<%= Page.ResolveUrl("~/ManagerUI/Menu/Souls/ProvidersView.aspx")%>' class="not-underline">
                    <div style="display: inline-block;" class="infoBlock3-grey infoPanel" runat="server" id="pnlProvidersView">
                        Поставщиков: <b style='color: green'><asp:Label runat="server" ID="lblProvidersView"></asp:Label></b>
                    </div>
                </a>
            
                <a href='<%= Page.ResolveUrl("~/ManagerUI/Menu/Souls/DriversView.aspx")%>' class="not-underline">
                    <div style="display: inline-block;" class="infoBlock3-grey infoPanel" runat="server" id="pnlDrivers" Visible="false">
                       Акт. водителей: <b style='color: blue'><asp:Label runat="server" ID="lblDrivers"></asp:Label></b>
                    </div>
                </a>
                
                <a href='<%= Page.ResolveUrl("~/ManagerUI/Menu/Souls/ManagersView.aspx")%>' class="not-underline">
                    <div style="display: inline-block;" class="infoBlock3-grey infoPanel" runat="server" id="pnlManagers" Visible="false">
                        Сотрудников: <b style='color: blue'><asp:Label runat="server" ID="lblManagers"></asp:Label></b>
                    </div>
                </a>

                <a href='<%= Page.ResolveUrl("~/ManagerUI/Menu/Content/NewsView.aspx")%>' class="not-underline">
                    <div style="display: inline-block;" class="infoBlock3-grey infoPanel" runat="server" id="pnlNews" Visible="false">
                        Новостей: <b style='color: blue'><asp:Label runat="server" ID="lblNews"></asp:Label></b>
                    </div>
                </a>
        
                <a href='<%= Page.ResolveUrl("~/ManagerUI/Menu/Souls/FeedbacksView.aspx")%>' class="not-underline">
                    <div style="display: inline-block;" class="infoBlock3-grey infoPanel" runat="server" id="pnlFeedback" Visible="false">
                        Откр. обращений: <b style='color: red'><asp:Label runat="server" ID="lblFeedback"></asp:Label></b>
                    </div>
                </a>

                <a href='<%= Page.ResolveUrl("~/ManagerUI/Menu/Content/ErrorsLogView.aspx")%>' class="not-underline">
                        <div style="display: inline-block;" class="infoBlock3-grey infoPanel" runat="server" id="pnlErrors" Visible="false">
                        Ошибок: <b style='color: red'><asp:Label runat="server" ID="lblErrors"></asp:Label></b>
                    </div>
                </a>
        
                <a href='<%= Page.ResolveUrl("~/ManagerUI/Menu/Content/LogsView.aspx")%>' class="not-underline">
                    <div style="display: inline-block;" class="infoBlock3-grey infoPanel" runat="server" id="pnlLogs" Visible="false">
                        Действий: <b style='color: orangered'><asp:Label runat="server" ID="lblLogs"></asp:Label></b>
                    </div>
                </a>
                
                <a href='<%= Page.ResolveUrl("~/ManagerUI/Menu/Content/ApiLogsView.aspx")%>' class="not-underline">
                    <div style="display: inline-block;" class="infoBlock3-grey infoPanel" runat="server" id="pnlApiLogs" Visible="false">
                        Запр. к API: <b style='color: orangered'><asp:Label runat="server" ID="lblApiLogs"></asp:Label></b>
                    </div>
                </a>
            </div>
        </div>
    </div>
    
</asp:Content>
