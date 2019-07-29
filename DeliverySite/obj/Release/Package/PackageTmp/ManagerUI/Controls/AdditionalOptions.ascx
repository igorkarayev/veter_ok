<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="AdditionalOptions.ascx.cs" Inherits="Delivery.ManagerUI.Controls.AdditionalOptions" %>
<script>
    $(function () {
        //инициализация переменных
        var cbCheckedOut = $('<%# String.Format("{0}_AdditionalOptions_{1}_cbCheckedOut_{1}", ListViewControlFullID, TicketID) %>');
        var cbPhoned = $('<%# String.Format("{0}_AdditionalOptions_{1}_cbPhoned_{1}", ListViewControlFullID, TicketID) %>');
        var cbBilled = $('<%# String.Format("{0}_AdditionalOptions_{1}_cbBilled_{1}", ListViewControlFullID, TicketID) %>');

        //инициализация чекбоксов при первой загрузке страницы КОНЕЦ
        cbCheckedOut.click(function () {
            <%# String.Format("SaveCheckboxCheckedOut({0}, \"{1}\", \"{2}\", \"{3}\", \"{4}\", \"{5}\");", TicketID, AppKey, ListViewControlFullID, UserID, UserIP, PageName) %>  //сохраняем значение
            var cbPhonedIsChecked = $(this).parent().parent().find(".cbPhoned").children("input").is(":checked");
            var cbCheckedOutIsChecked = $(this).is(":checked");

            if (cbCheckedOutIsChecked && cbPhonedIsChecked) {
                $(this).parent().parent().parent().parent().css("background-image", "url('../../../styles/images/bg-line-3.png')");
            }

            if (!cbCheckedOutIsChecked && cbPhonedIsChecked) {
                $(this).parent().parent().parent().parent().css("background-image", "url('../../../styles/images/bg-line-1.png')");
            }

            if (cbCheckedOutIsChecked && !cbPhonedIsChecked) {
                $(this).parent().parent().parent().parent().css("background-image", "url('../../../styles/images/bg-line-2.png')");
            }

            if (!cbCheckedOutIsChecked && !cbPhonedIsChecked) {
                $(this).parent().parent().parent().parent().css("background-image", "none");
            }
        });

        cbPhoned.click(function () {
            <%# String.Format("SaveCheckboxPhoned({0}, \"{1}\", \"{2}\", \"{3}\", \"{4}\", \"{5}\");", TicketID, AppKey, ListViewControlFullID, UserID, UserIP, PageName) %>  //сохраняем значение
            var cbPhonedIsChecked = $(this).is(":checked");
            var cbCheckedOutIsChecked = $(this).parent().parent().find(".cbCheckedOut").children("input").is(":checked");

            if (cbCheckedOutIsChecked && cbPhonedIsChecked) {
                $(this).parent().parent().parent().parent().css("background-image", "url('../../../styles/images/bg-line-3.png')");
            }

            if (!cbCheckedOutIsChecked && cbPhonedIsChecked) {
                $(this).parent().parent().parent().parent().css("background-image", "url('../../../styles/images/bg-line-1.png')");
            }

            if (cbCheckedOutIsChecked && !cbPhonedIsChecked) {
                $(this).parent().parent().parent().parent().css("background-image", "url('../../../styles/images/bg-line-2.png')");
            }

            if (!cbCheckedOutIsChecked && !cbPhonedIsChecked) {
                $(this).parent().parent().parent().parent().css("background-image", "none");
            }
        });

        cbBilled.click(function () {
            <%# String.Format("SaveCheckboxBilled({0}, \"{1}\", \"{2}\", \"{3}\", \"{4}\", \"{5}\");", TicketID, AppKey, ListViewControlFullID, UserID, UserIP, PageName) %> //сохраняем значение
            var cbIsBilledChecked = $(this).is(":checked");
            if (cbIsBilledChecked) {
                $(this).parent().parent().parent().addClass("billed-selected");
            } else {
                $(this).parent().parent().parent().removeClass("billed-selected");
            }
        });
    });
</script>
<div style='width: 80px; text-align: right;'>
    <span style="margin-top: -3px; font-size: 8px; color: red;">чек пробит</span><asp:CheckBox runat="server" ID="cbCheckedOut" CssClass="cbCheckedOut"/>
    <br/>
    <span style="margin-top: -3px; font-size: 9px; color: red;">дозвон.</span><asp:CheckBox runat="server" ID="cbPhoned" CssClass="cbPhoned"/>
    <br/>
    <span style="margin-top: -3px; font-size: 9px; color: red;">счет выст.</span><asp:CheckBox runat="server" ID="cbBilled"/>
</div>
<asp:HiddenField runat="server" ID="hfCheckedOut" Value='<%# Eval("CheckedOut") %>'/>
<asp:HiddenField runat="server" ID="hfPhoned" Value='<%# Eval("Phoned") %>'/>
<asp:HiddenField runat="server" ID="hfBilled" Value='<%# Eval("Billed") %>'/>
