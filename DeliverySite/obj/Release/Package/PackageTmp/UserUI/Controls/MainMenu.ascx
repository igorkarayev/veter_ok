<%@ Control Language="C#" Inherits="Delivery.UserUI.Controls.MainMenu" AutoEventWireup="true" %>
<script>
    $(function() {
        if ($("#ulTicketCreate").find("a").length <= 0) {
            $("#ulTicketCreate").css("display", "none");
        }
        
        $("ul.menu li").hover(function () {
            $(this).find("ul").addClass("activeMenuFix");
        }, function () {
            $(this).find("ul").removeClass("activeMenuFix");
        });
    });
    function WantPayment() {
        $.ajax({
            type: "POST",
            dataType: "xml",
            url: "../AppServices/MailService.asmx/WantPayment",
            data: ({
                userid: "<%= UserID %>",
                    appkey: "<%= AppKey %>"
                }),
            success: function (xml) {
                var status = "no";
                $(xml).find('string').each(function () {
                    status = $(this).text();
                });
                if (status == "ok-now") {
                    jAlert('<center style="color: green; font-size: 14px;">Запрос на расчет успешно отправлен кассирам. ' +
                        'Для улучшения качества обслуживания по своевременному расчету по Вашим заявкам, ввиду: погодных условий, ' +
                        'несвоевременной явки клиентов и создания очередей, с 14.01 по 31.01.2016 года внесены изменения в графике работы бухгалтерии.<br/>' +
                        '• Заявки, оформленные до 13.00 – расчет на завтра </br>' +
                        '• Заявки, оформленные после 13.00 – расчет на послезавтра  </br>' +
                        'В связи с этим предположительный день расчета - <b>завтра</b> (за исключением выходных и праздников)</center>', 'закрыть');
                } else {
                    if (status == "ok-tommorow") {
                        jAlert('<center style="color: green; font-size: 14px;">Запрос на расчет успешно отправлен кассирам. ' +
                        'Для улучшения качества обслуживания по своевременному расчету по Вашим заявкам, ввиду: погодных условий, ' +
                        'несвоевременной явки клиентов и создания очередей, с 14.01 по 31.01.2016 года внесены изменения в графике работы бухгалтерии.<br/>' +
                        '• Заявки, оформленные до 13.00 – расчет на завтра </br>' +
                        '• Заявки, оформленные после 13.00 – расчет на послезавтра  </br>' +
                        'В связи с этим предположительный день расчета - <b>послезавтра</b> (за исключением выходных и праздников)</center>', 'закрыть');
                    } else {
                        jAlert('<center style="color: red; font-weight: bold; font-size: 14px;">Ошибка!</center>', 'закрыть');
                    }
                }
            },
            error: function (result) {
                jAlert('<center style="color: red; font-weight: bold; font-size: 14px;">Ошибка!</center>', 'закрыть');
            }
        });
        return false;
    };
</script>
<ul class="menu">
    <li>
         <asp:HyperLink runat="server" ID="hlMain" Text="Главная" NavigateUrl="~/UserUI/Default.aspx"></asp:HyperLink>
    </li>
        
    <li id="liNewsFeed">
            <asp:HyperLink runat="server" ID="hlNewsFeed" Text="Новости" NavigateUrl="~/UserUI/NewsFeedView.aspx"></asp:HyperLink>
    </li> 

    <li>
        <asp:HyperLink runat="server" ID="hlProfile" Text="Профили" NavigateUrl="~/UserUI/ProfilesView.aspx"></asp:HyperLink>
        <ul>
            <li runat="server" ID="liProfileCreate">
                <asp:HyperLink runat="server" ID="hlProfileCreate" Text="Создать профиль" NavigateUrl="~/UserUI/ProfileEdit.aspx"></asp:HyperLink> 
            </li>            
        </ul>
    </li>
    <li runat="server" id="liTickets">
        <asp:HyperLink runat="server" ID="hlTickets" Text="Заявки" NavigateUrl="~/UserUI/TicketView.aspx"></asp:HyperLink>
        <ul ID="ulTicketCreate">
            <li runat="server" ID="liTicketCreate">
                <asp:HyperLink runat="server" ID="hlTicketCreate" Text="Создать заявку" NavigateUrl="~/UserUI/TicketEdit.aspx"></asp:HyperLink>
            </li>
            
        </ul>
    </li>
    <li id="liWantPayment" runat="server">
            <asp:HyperLink runat="server" ID="hlWantPayment" Text="Запросить расчет" NavigateUrl="#" onclick="return jConfirm('Вы точно хотите запросить расчет?', 'Да', 'Нет', function(r) { if(r){ WantPayment(); }});"></asp:HyperLink>
    </li> 
    <li runat="server" id="liFeedback">
        <asp:HyperLink runat="server" NavigateUrl="~/UserUI/FeedbacksView.aspx" ID="hlFeedback" Text="Поддержка"></asp:HyperLink>
    </li>
    <li runat="server" id="liDeveloper">
        <asp:HyperLink runat="server"  ID="hlDeveloper" NavigateUrl="~/UserUI/Developer.aspx"></asp:HyperLink>
    </li>
    <li>
        <asp:HyperLink runat="server"  ID="hlFAQ"  NavigateUrl="~/UserUI/FAQ.aspx"></asp:HyperLink>
    </li>
    <li runat="server" ID="liEditProfilesSender">
        <asp:HyperLink runat="server" ID="hlEditProfilesSender" Text="Контрагенты" NavigateUrl="~/UserUI/ProfilesEditSender.aspx"></asp:HyperLink> 
    </li>
</ul>


<ul class="menu" style="float: right">
    <li id="liSite">
            <asp:HyperLink CssClass="siteLinkText" runat="server" ID="hlSite" Text="Сайт" NavigateUrl="" Target="new"></asp:HyperLink>
    </li>

    <li>
        <asp:HyperLink runat="server" CssClass="manager-cabinet-link" Visible="false" ID="hlManagerUI" Text="Кабинет сотрудника" NavigateUrl="~/ManagerUI/Default.aspx"></asp:HyperLink>
    </li>

    <li id="liUserUI">
        <asp:LinkButton CssClass="exit-link" runat="server" ID="HyperLink1" Text="Выйти" OnClick="Logoff"/>
    </li>
</ul>