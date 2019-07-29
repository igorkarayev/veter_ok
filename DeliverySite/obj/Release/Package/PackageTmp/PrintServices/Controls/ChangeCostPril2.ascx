<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ChangeCostPril2.ascx.cs" Inherits="DeliverySite.PrintServices.Controls.ChangeCostPril2" %>

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
        $('<%# String.Format("{0}_ChangeCost_{1}_Td6CostTextBox_{1}", ListViewControlFullID, TicketID) %>').on('input', function () {            
            var price = parseFloat(this.value.trim().replace(",", ".").replace(" ", "").replace(/&nbsp;/gi, ''));
            this.defaultValue = this.value;

            $(this).closest("tr").children("[id *= 'lvAllPrint_Td7']").html(toMoney(price.toFixed(2)).replace(".", ","));
            $(this).closest("tr").children("[id *= 'lvAllPrint_Td10']").html(toMoney(price.toFixed(2)).replace(".", ","));

            var newSum = parseFloat(0);
            $("tr[id*='lvAllPrint_Tr2']:not('.hidden')").each(function (index, value) {
                var lox = parseFloat($(this).closest("tr").children("[id *= 'lvAllPrint_Td7']").html().trim().replace(",", ".").replace(" ", "").replace(/&nbsp;/gi, '')).toFixed(2);
                newSum = parseFloat(parseFloat(newSum) + parseFloat($(this).closest("tr").children("[id *= 'lvAllPrint_Td7']").html().trim().replace(",", ".").replace(" ", "").replace(/&nbsp;/gi, '')));              
            });            

            $("#lblOverCost").text(toMoney(newSum.toFixed(2)).replace(".", ","));
            $("#lblOverCost2").text(toMoney(newSum.toFixed(2)).replace(".", ","));
            $("#lblOverCost3").text(toMoney(newSum.toFixed(2)).replace(".", ","));

            $.ajax({
                type: "POST",                
                url: "../AppServices/UpdateFields.asmx/UpdateRusCost",
                data: ({
                    cost: newSum.toFixed(2).replace(".",",").toString()
                }),
                success: function (response) {
                    $("#lblCostWord").text(response.children[0].textContent);
                }
            });
        });
    });    
</script>
<asp:TextBox BorderStyle="None" runat="server" id="Td6CostTextBox" Text='<%# Val %>'/>
                                                    

