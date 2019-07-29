<%@ Control Language="C#" Inherits="Delivery.ManagerUI.Controls.MainMenu" AutoEventWireup="true" CodeBehind="MainMenu.ascx.cs" %>
<script>
    $(function () {
        if ($("#liContent").find("a").length <= 1) {
            $("#liContent").css("display", "none");
        }
        
        if ($("#liSouls").find("a").length <= 1) {
            $("#liSouls").css("display", "none");
        }
        
        if ($("#liTickets").find("a").length <= 1) {
            $("#liTickets").css("display", "none");
        }
        
        if ($("#liFinance").find("a").length <= 1) {
            $("#liFinance").css("display", "none");
        }
        
        if ($("#liIssuance").find("a").length <= 1) {
            $("#liIssuance").css("display", "none");
        }
        
        if ($("#liSettings").find("a").length <= 1) {
            $("#liSettings").css("display", "none");
        }
        
        if ($("#liUserUI").find("a").length <= 0) {
            $("#liUserUI").css("display", "none");
        }
        
        if ($("#liDocuments").find("a").length <= 1) {
            $("#liDocuments").css("display", "none");
        }

        $("#liContent ul li:last-child").css("border", "none");
        $("#liSouls ul li:last-child").css("border", "none");
        $("#liTickets ul li:last-child").css("border", "none");
        $("#liFinance ul li:last-child").css("border", "none");
        $("#liIssuance ul li:last-child").css("border", "none");
        $("#liSettings ul li:last-child").css("border", "none");

        $("ul.menu li").hover(function() {
            $(this).find("ul").addClass("activeMenuFix");
        }, function(){
            $(this).find("ul").removeClass("activeMenuFix");
        });
    });
</script>
<ul class="menu">
    <li id="liDefault">
        <asp:HyperLink runat="server" ID="hlMain" Text="Главная" NavigateUrl="~/ManagerUI/Default.aspx"></asp:HyperLink>
        <ul runat="server">
            <li>
                 <asp:HyperLink runat="server" ID="hlAnonymousMessage" Text="Анонимное сообщение" NavigateUrl="~/ManagerUI/AnonymousMessage.aspx"></asp:HyperLink>
            </li>
        </ul>
    </li>
    
    <li id="liNewsFeed">
            <asp:HyperLink CssClass="" runat="server" ID="hlNewsFeed" Text="Новости" NavigateUrl="~/ManagerUI/Menu/NewsFeed/NewsFeedView.aspx"></asp:HyperLink>
    </li>
    
    <li id="liContent">
        <asp:HyperLink runat="server" ID="hlContent" Text="Контент"></asp:HyperLink>
        <ul runat="server">
            <li runat="server" id="liNewsView">
                <asp:HyperLink runat="server" ID="hlNews" Text="Новости" NavigateUrl="~/ManagerUI/Menu/Content/NewsView.aspx"></asp:HyperLink>
            </li>
            <li runat="server" id="liPagesView" style="border-bottom: 1px dotted white;">
                <asp:HyperLink runat="server" ID="hlPages" Text="Страницы" NavigateUrl="~/ManagerUI/Menu/Content/PagesView.aspx"></asp:HyperLink>
            </li>
            <li runat="server" id="liNotificationsView">
                <asp:HyperLink runat="server" ID="hlNotifications" Text="Уведомления сайта" NavigateUrl="~/ManagerUI/Menu/Content/NotificationsView.aspx"></asp:HyperLink>
            </li>
            <li runat="server" id="liEmailNotificationsView" style="border-bottom: 1px dotted white;">
                <asp:HyperLink runat="server" ID="hlEmailNotifications" Text="Email-уведомления" NavigateUrl="~/ManagerUI/Menu/Content/EmailNotificationsView.aspx"></asp:HyperLink>
            </li>
            <li runat="server" id="liErrorsLogView">
                <asp:HyperLink runat="server" ID="hlErrors" Text="Логи ошибок" NavigateUrl="~/ManagerUI/Menu/Content/ErrorsLogView.aspx"></asp:HyperLink>
            </li>
            <li runat="server" id="liLogsView">
                <asp:HyperLink runat="server" ID="hlLogs" Text="Логи действий" NavigateUrl="~/ManagerUI/Menu/Content/LogsView.aspx"></asp:HyperLink>
            </li> 
            <li runat="server" id="liApiLogs">
                <asp:HyperLink runat="server" ID="hlApiLogs" Text="Логи запросов к API" NavigateUrl="~/ManagerUI/Menu/Content/ApiLogsView.aspx"></asp:HyperLink>
            </li>
        </ul>
    </li>

    <li id="liSouls">
        <asp:HyperLink runat="server" ID="hlSouls" Text="Сущности"></asp:HyperLink>
        <ul>
            <li runat="server" ID="liCategoryView">
                <asp:HyperLink runat="server" ID="hlCategory" Text="Категории" NavigateUrl="~/ManagerUI/Menu/Souls/CategoryView.aspx"></asp:HyperLink>
            </li>
            <li runat="server" ID="liTitlesView"  style="border-bottom: 1px dotted white;">
                <asp:HyperLink runat="server" ID="hlTitles" Text="Наименования" NavigateUrl="~/ManagerUI/Menu/Souls/TitlesView.aspx"></asp:HyperLink>
            </li>
            <li runat="server" ID="liCityView">
                <asp:HyperLink runat="server" ID="hlCity" Text="Нас. пункты" NavigateUrl="~/ManagerUI/Menu/Souls/CityView.aspx"></asp:HyperLink>
            </li>
            <li runat="server" ID="li1">
                <asp:HyperLink runat="server" ID="HyperLink2" Text="Дни доставки нас. пунктов" NavigateUrl="~/ManagerUI/Menu/Souls/CityViewsSendDays.aspx"></asp:HyperLink>
            </li>
            <li runat="server" ID="liDistricts">
                <asp:HyperLink runat="server" ID="hlDistricts" Text="Районы" NavigateUrl="~/ManagerUI/Menu/Souls/DistrictsView.aspx"></asp:HyperLink>
            </li>
            <li runat="server" ID="liTracksView" >
                <asp:HyperLink runat="server" ID="hlTracks" Text="Направления" NavigateUrl="~/ManagerUI/Menu/Souls/TracksView.aspx"></asp:HyperLink>
            </li>
            <li runat="server" ID="liWarehouses" style="border-bottom: 1px dotted white;">
                <asp:HyperLink runat="server" ID="hlWarehouses" Text="Склады" NavigateUrl="~/ManagerUI/Menu/Souls/WarehousesView.aspx"></asp:HyperLink>
            </li>
            <li runat="server" ID="liRoutes" style="display: none">
                <asp:HyperLink Visible="False" runat="server" ID="hlRoutes" Text="Маршруты" NavigateUrl="~/ManagerUI/Menu/Souls/RoutesView.aspx"></asp:HyperLink>
            </li>
            <li runat="server" ID="liCarsView">
                <asp:HyperLink runat="server" ID="hlCars" Text="Автомобили" NavigateUrl="~/ManagerUI/Menu/Souls/CarsView.aspx"></asp:HyperLink>
            </li>
            <li runat="server" ID="liDriversView">
                <asp:HyperLink runat="server" ID="hlDrivers" Text="Водители" NavigateUrl="~/ManagerUI/Menu/Souls/DriversView.aspx"></asp:HyperLink>
            </li>
            <li runat="server" ID="liManagersView" style="border-bottom: 1px dotted white;">
                <asp:HyperLink runat="server" ID="hlManagers" Text="Сотрудники" NavigateUrl="~/ManagerUI/Menu/Souls/ManagersView.aspx"></asp:HyperLink>
            </li>
            <li runat="server" ID="liClientsView">
                <asp:HyperLink runat="server" ID="hlClients" Text="Клиенты" NavigateUrl="~/ManagerUI/Menu/Souls/ClientsView.aspx"></asp:HyperLink>
            </li>
            <li runat="server" ID="liFeedback">
                <asp:HyperLink runat="server" ID="hlFeedback" Text="Обращения" NavigateUrl="~/ManagerUI/Menu/Souls/FeedbacksView.aspx"></asp:HyperLink>
            </li>
            <li runat="server" ID="liProviders">
                <asp:HyperLink runat="server" ID="hlProviders" Text="Поставщики" NavigateUrl="~/ManagerUI/Menu/Souls/ProvidersView.aspx"></asp:HyperLink>
            </li>
        </ul>
    </li>
    

    <li id="liTickets">
        <asp:HyperLink runat="server" ID="hlTickets" Text="Заявки"></asp:HyperLink>
        <ul>
            <li runat="server" ID="liUserTicketByDeliveryOnMinsk">
                <asp:HyperLink runat="server" ID="hlUserTicketByDeliveryOnMinsk" Text="Заявки Минск" NavigateUrl="~/ManagerUI/Menu/Tickets/UserTicketByDeliveryOnMinsk.aspx"></asp:HyperLink>
            </li>
            <li runat="server" ID="liUserTicketByDeliveryOnBelarus">
                <asp:HyperLink runat="server" ID="hlUserTicketByDeliveryOnBelarus" Text="Заявки РБ" NavigateUrl="~/ManagerUI/Menu/Tickets/UserTicketByDeliveryOnBelarus.aspx"></asp:HyperLink>
            </li>
            <li runat="server" ID="liMyTickets">
                <asp:HyperLink runat="server" ID="hlMyTickets" Text="Мои заявки" NavigateUrl="~/ManagerUI/Menu/Tickets/UserTicketViewMy.aspx"></asp:HyperLink>
            </li>
            <li runat="server" ID="liUserTicketView">
                <asp:HyperLink runat="server" ID="hlTicketsAll" Text="Все заявки" NavigateUrl="~/ManagerUI/Menu/Tickets/UserTicketView.aspx"></asp:HyperLink>
            </li>
            <li runat="server" ID="liUserTicketNotProcessedView">
                <asp:HyperLink runat="server" ID="hlTicketsNotProcessed" Text="Необработанные" NavigateUrl="~/ManagerUI/Menu/Tickets/UserTicketNotProcessedView.aspx"></asp:HyperLink>
            </li>
        </ul>
    </li>

    <li id="liFinance">
        <asp:HyperLink runat="server" ID="hlMoney" Text="Финансы"></asp:HyperLink>
        <ul>
            <li runat="server" ID="liMoneyView">
                <asp:HyperLink runat="server" ID="hlMoneyPriemka" Text="Приемка" NavigateUrl="~/ManagerUI/Menu/Finance/MoneyView.aspx"></asp:HyperLink>
            </li>
            <li runat="server" ID="liMoneyDriverView">
                <asp:HyperLink runat="server" ID="hlDriverMoney" Text="Контроль водителя" NavigateUrl="~/ManagerUI/Menu/Finance/MoneyDriverView.aspx"></asp:HyperLink>
            </li>
            <li runat="server" ID="liMoneyDeliveredView">
                <asp:HyperLink runat="server" ID="hlAllDeliveryMoney" Text='Сумма заявок "СТАТУС"' NavigateUrl="~/ManagerUI/Menu/Finance/MoneyDeliveredView.aspx"></asp:HyperLink>
            </li>
            <li runat="server" ID="liMoneyGruzobozView">
                <asp:HyperLink runat="server" ID="hlAllGruzobozMoney" Text='Сумма "За услугу"' NavigateUrl="~/ManagerUI/Menu/Finance/MoneyDeliveryView.aspx"></asp:HyperLink>
            </li>
            <li runat="server" ID="liCalculation">
                <asp:HyperLink runat="server" ID="hlCalculation" Text="Расчет" NavigateUrl="~/ManagerUI/Menu/Finance/CalculationView.aspx"></asp:HyperLink>
            </li>
        </ul>
    </li>
    
    <li id="liIssuance">
        <asp:HyperLink runat="server" ID="hlIssuance" Text="Выдача"></asp:HyperLink>
        <ul>
            <li runat="server" ID="liIssuanceView">
                <asp:HyperLink runat="server" ID="hlIssuanceView" Text="Заявки к выдаче" NavigateUrl="~/ManagerUI/Menu/Issuance/IssuanceView.aspx"></asp:HyperLink>
            </li>
            <li runat="server" ID="liNewIssuanceView">
                <asp:HyperLink runat="server" ID="hlNewIssuanceView" Text="Новые Расчетные листы" NavigateUrl="~/ManagerUI/Menu/Issuance/NewIssuanceView.aspx"></asp:HyperLink>
            </li>
            <li runat="server" ID="liIssuanceListsView">
                <asp:HyperLink runat="server" ID="hlIssuanceListsView" Text="Расчетные листы" NavigateUrl="~/ManagerUI/Menu/Issuance/IssuanceListsView.aspx"></asp:HyperLink>
            </li>
            <li runat="server" ID="liIssuanceListsEdit">
                <asp:HyperLink runat="server" ID="hlIssuanceListsEdit" Text="Создать раcчетный лист" NavigateUrl="~/ManagerUI/Menu/Issuance/IssuanceListsEdit.aspx"></asp:HyperLink>
            </li>
        </ul>
    </li>

    <li id="liDocuments">
        <asp:HyperLink runat="server" ID="hlDocuments" Text="Документы"></asp:HyperLink>
        <ul>
            <li runat="server" ID="liReportsArchive" style="min-width: 105px;">
                <asp:HyperLink runat="server" ID="hlReportsArchive" Text="Архив" NavigateUrl="~/ManagerUI/Menu/Documents/ReportsArchiveView.aspx"></asp:HyperLink>
            </li>
            <li runat="server" ID="liReportsExport">
                <asp:HyperLink runat="server" ID="hlReportsExport" Text="Экспорт" NavigateUrl="~/ManagerUI/Menu/Documents/ReportsExport.aspx"></asp:HyperLink>
            </li>
            <li runat="server" ID="liSendComProp">
                <asp:HyperLink runat="server" ID="hlSendComProp" Text="Отправка КП" NavigateUrl="~/ManagerUI/Menu/Documents/SendComProp.aspx"></asp:HyperLink>
            </li> 
        </ul>
    </li>
    
    <li id="liSettings">
        <asp:HyperLink runat="server" ID="hlSettings" Text="Настройки"></asp:HyperLink>
        <ul>
            <li runat="server" ID="liChangePasswords">
                <asp:HyperLink runat="server" ID="hlChangePasswords" Text="Смена паролей" NavigateUrl="~/ManagerUI/Menu/Settings/ChangePasswords.aspx"></asp:HyperLink>
            </li>
            <li runat="server" ID="liUserDiscountView">
                <asp:HyperLink runat="server" ID="hlUsersDiscount" Text="Скидки клиентов" NavigateUrl="~/ManagerUI/Menu/Settings/UsersDiscountView.aspx"></asp:HyperLink>
            </li>
            <li runat="server" ID="liRolesView">
                <asp:HyperLink runat="server" ID="hlRoles" Text="Настройки ролей" NavigateUrl="~/ManagerUI/Menu/Settings/RolesView.aspx"></asp:HyperLink>
            </li>
            <li runat="server" ID="liBackendView">
                <asp:HyperLink runat="server" ID="hlBackend" CssClass="blueText" Text="Настройки BackEnd" NavigateUrl="~/ManagerUI/Menu/Settings/BackendView.aspx"></asp:HyperLink>
            </li>            
        </ul>
    </li>

    
</ul>

<ul class="menu" style="float: right">
    <li id="liSite">
            <asp:HyperLink CssClass="siteLinkText" runat="server" ID="hlSite" Text="Сайт" NavigateUrl="" Target="new"></asp:HyperLink>
    </li>
    <li id="liUserUI">
            <asp:HyperLink CssClass="controlLinkText" runat="server" ID="hlUserUI" Text="Кабинет клиента" NavigateUrl="~/UserUI/Default.aspx"></asp:HyperLink>
    </li>

    <li id="liUserUI">
        <asp:LinkButton style="color: red; font-weight: bold;" runat="server" ID="HyperLink1" Text="Выйти" OnClick="Logoff"/>
    </li>
</ul>