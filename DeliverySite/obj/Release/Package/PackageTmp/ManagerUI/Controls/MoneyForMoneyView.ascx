<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="MoneyForMoneyView.ascx.cs" Inherits="Delivery.ManagerUI.Controls.MoneyForMoneyView" %>
<%@ Import Namespace="Delivery.AppServices" %>
<%@ Import Namespace="Delivery.BLL" %> <%@ Import Namespace="Delivery.BLL.StaticMethods" %>
<script>
    $(function () {
        //инициализация переменных
        var agreedAssessedCost = $('<%# String.Format("{0}_MoneyForMoneyView_{1}_hfAgreedAssessedCosts_{1}", ListViewControlFullID, TicketID) %>').val();
        var cbWithoutMoney = $('<%# String.Format("{0}_MoneyForMoneyView_{1}_cbWithoutMoney_{1}", ListViewControlFullID, TicketID) %>');
        var cbIsExchange = $('<%# String.Format("{0}_MoneyForMoneyView_{1}_cbIsExchange_{1}", ListViewControlFullID, TicketID) %>');
        var lblAgreedAssessedCosts = $('<%# String.Format("{0}_MoneyForMoneyView_{1}_lblAgreedAssessedCosts_{1}", ListViewControlFullID, TicketID) %>');
        var exchangeCost = 0;

        //инициализация чекбоксов при первой загрузке страницы СТАРТ
        if (cbWithoutMoney.is(':checked')) {
            lblAgreedAssessedCosts.html("0");
        }

        if (cbIsExchange.is(':checked')) {
            exchangeCost = 0 - parseFloat(lblAgreedAssessedCosts.html().replace(/\s*/g, '').replace(/&nbsp;/g, ''));
            lblAgreedAssessedCosts.html(exchangeCost.toString().replace(/(\d)(?=(\d\d\d)+([^\d]|$))/g, '$1 '));
            cbWithoutMoney.attr("disabled", "disabled");
        }

        //инициализация чекбоксов при первой загрузке страницы КОНЕЦ
        cbWithoutMoney.click(function () {
            if ($(this).is(':checked')) {
                lblAgreedAssessedCosts.html("0");
                cbIsExchange.attr("disabled", "disabled");
            } else {
                lblAgreedAssessedCosts.html(agreedAssessedCost);
                cbIsExchange.removeAttr("disabled");
            }

            //если страница приемки денег - подключаем рассчеты сумм.
            var pageName = '<%# PageName%>';
            if ( pageName == "MoneyView") {
                CalculateDifference(<%# TicketID %>, false);
                CalculateAgreedAssessedWithDeliveryCost();
            }
            
            <%# String.Format("SaveCheckboxWithoutMoney({0}, \"{1}\", \"{2}\", \"{3}\", \"{4}\", \"{5}\");", TicketID, AppKey, ListViewControlFullID, UserID, UserIP, PageName) %>  //сохраняем значение
        });

        cbIsExchange.click(function () {
            if ($(this).is(':checked')) {
                exchangeCost = 0 - agreedAssessedCost.replace(/\s*/g, '');
                lblAgreedAssessedCosts.html(exchangeCost.toString().replace(/(\d)(?=(\d\d\d)+([^\d]|$))/g, '$1 '));
                cbWithoutMoney.attr("disabled", "disabled");
            } else {
                lblAgreedAssessedCosts.html(agreedAssessedCost);
                cbWithoutMoney.removeAttr("disabled");
            }
            //если страница приемки денег - подключаем рассчеты сумм.
            var pageName = '<%# PageName%>';
            if (pageName == "MoneyView") {
                CalculateDifference(<%# TicketID %>, false);
                CalculateAgreedAssessedWithDeliveryCost();
            }
            <%# String.Format("SaveCheckboxIsExchange({0}, \"{1}\", \"{2}\", \"{3}\", \"{4}\", \"{5}\");", TicketID, AppKey, ListViewControlFullID, UserID, UserIP, PageName) %>  //сохраняем значение
        });
    });
</script>
<div style='color: <%# OtherMethods.AgreedAssessedDeliveryCostsColor(TicketID) %>;width: 100%; height: 100%; text-align: right;'>
    <asp:TextBox ID="tbAgreedAssessedCosts" runat="server" CssClass="moneyMask" 
        onblur='<%# String.Format("SaveAgreedCost({0},\"{1}\",\"{2}\",\"{3}\",\"{4}\",\"{5}\")", TicketID, AppKey, ListViewControlFullID, UserID, UserIP, PageName)%>'
        onfocus='<%# String.Format("ClearLabelStatusAgreedCost({0},\"{1}\")", TicketID, ListViewControlFullID)%>'
        style ='color: inherit' Text='<%# MoneyMethods.MoneySeparator(MoneyMethods.AgreedAssessedCosts(TicketID)) %>'/>
    <asp:Label runat="server" ID="lblSaveAgreedCostStatus"></asp:Label> 
    <br/>
    <span style="margin-top: -3px; font-size: 9px; color: red;">без опл.</span><asp:CheckBox runat="server" ID="cbWithoutMoney" />
    <br/>
    <span style="margin-top: -3px; font-size: 9px; color: red;">обратка</span><asp:CheckBox runat="server" ID="cbIsExchange" />
</div>
<asp:HiddenField runat="server" ID="hfWithoutMoney" Value='<%# Eval("WithoutMoney") %>'/>
<asp:HiddenField runat="server" ID="hfIsExchange" Value='<%# Eval("IsExchange") %>'/>
<asp:HiddenField runat="server" ID="hfAgreedAssessedCosts" Value='<%# MoneyMethods.MoneySeparator(MoneyMethods.AgreedAssessedCosts(TicketID)) %>'/>
