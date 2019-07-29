<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="DeliveryCostForMoneyView.ascx.cs" Inherits="Delivery.ManagerUI.Controls.DeliveryCostForMoneyView" %>
<%@ Import Namespace="Delivery.AppServices" %>
<%@ Import Namespace="Delivery.BLL" %> <%@ Import Namespace="Delivery.BLL.StaticMethods" %>
<script>
    $(function () {
        //инициализация переменных
        var deliveryCost = $('<%# String.Format("{0}_DeliveryCostForMoneyView_{1}_hfDeliveryCost_{1}", ListViewControlFullID, TicketID) %>').val();
        var cbWithoutMoney = $('<%# String.Format("{0}_DeliveryCostForMoneyView_{1}_cbWithoutMoney_{1}", ListViewControlFullID, TicketID) %>');
        var lblDeliveryCosts = $('<%# String.Format("{0}_DeliveryCostForMoneyView_{1}_lblDeliveryCosts_{1}", ListViewControlFullID, TicketID) %>');

        //инициализация чекбоксов при первой загрузке страницы СТАРТ
        if (cbWithoutMoney.is(':checked')) {
            lblDeliveryCosts.html("0");
        }

        //инициализация чекбоксов при первой загрузке страницы КОНЕЦ
        cbWithoutMoney.click(function () {
            if ($(this).is(':checked')) {
                lblDeliveryCosts.html("0");
            } else {
                lblDeliveryCosts.html(deliveryCost.replace(/(\d)(?=(\d\d\d)+([^\d]|$))/g, '$1 '));
            }

            //если страница приемки денег - подключаем рассчеты сумм.
            var pageName = '<%# PageName%>';
            if (pageName == "MoneyView") {
                CalculateDifference(<%# TicketID %>, false);
                CalculateAgreedAssessedWithDeliveryCost();
            }
            <%# String.Format("SaveDeliveryCost({0}, \"{1}\", \"{2}\", \"{3}\", \"{4}\", \"{5}\");", TicketID, AppKey, ListViewControlFullID, UserID, UserIP, PageName) %>  //сохраняем значение
        });
    });
</script>
<div style='width: 100%; text-align: right;'>
    <asp:Label ID="lblDeliveryCosts" runat="server" CssClass="courseLabel data-deliveryCost" style="" Text='<%# MoneyMethods.MoneySeparator(Eval("DeliveryCost").ToString()) %>'/>
    <br/>
    <span style="margin-top: -3px; font-size: 9px; color: red;">беcпл.</span><asp:CheckBox runat="server" ID="cbWithoutMoney" />
</div>
<asp:HiddenField runat="server" ID="hfDeliveryCost" Value='<%# Eval("DeliveryCost") %>'/>
