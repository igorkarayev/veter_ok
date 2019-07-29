﻿<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ChangeBoxesNumber.ascx.cs" Inherits="DeliverySite.PrintServices.Controls.ChangeBoxesNumber" %>
<script>
    function toMoney(number) {
        var temp = number.substr(0, number.lastIndexOf('.'));
        var res = "";
        for (var i = temp.length - 1; i >= 0; i--) {
            res = temp[i] + ((i < temp.length - 1 && (temp.length - 1 - i) % 3 == 0) ? " " : "") + res;
        }

        return res + number.substr(number.lastIndexOf('.'));
    }

    $(function () {
        $('<%# String.Format("{0}_ChangeBoxes_{1}_Td6BoxesNumberTextBox_{1}", ListViewControlFullID, TicketID) %>').on('input', function () {            
            var price = parseFloat(this.value.trim().replace(",", ".").replace(" ", "").replace(/&nbsp;/gi, ''));
            this.defaultValue = this.value;

            var newSum = parseFloat(0);
            $("tr[id*='lvAllPrint_Tr2']:not('.hidden')").each(function (index, value) {
                var lox = $(this).closest("tr").children("[id *= 'lvAllPrint_Td11']").children()[1];
                var lox2 = parseFloat($(this).closest("tr").children("[id *= 'lvAllPrint_Td11']").children()[1].value.trim().replace(",", ".").replace(" ", "").replace(/&nbsp;/gi, ''));
                newSum = parseFloat(parseFloat(newSum) + parseFloat($(this).closest("tr").children("[id *= 'lvAllPrint_Td11']").children()[1].value.trim().replace(",", ".").replace(" ", "").replace(/&nbsp;/gi, '')));              
            });            

            $("#lblOverBoxes").text(newSum);

            $.ajax({
                type: "POST",
                url: "../AppServices/UpdateFields.asmx/UpdateRusNumber",
                data: ({
                    cost: newSum.toFixed(2).replace(".", ",").toString()
                }),
                success: function (response) {
                    $("#lblBoxesWord").text(response.children[0].textContent);
                }
            });
        });
    });    
</script>

<asp:TextBox CssClass="inp" BorderStyle="None" runat="server" id="Td6BoxesNumberTextBox" Text='<%# Boxes %>'/>