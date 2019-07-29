<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="SelectToPril2.ascx.cs" Inherits="Delivery.PrintServices.Controls.SelectToPril2" %>
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
        $('<%# String.Format("{0}_SelectToPril2_{1}_cbNotPrintInPril2_{1}", ListViewControlFullID, TicketID) %>').click(function () {
            <%--<%# String.Format("SaveCheckboxPril2({0}, \"{1}\", \"{2}\", \"{3}\", \"{4}\", \"{5}\");", TicketID, AppKey, ListViewControlFullID, UserID, UserIP, PageName) %> --%> //сохраняем значение
            $(this).closest("tr").addClass("hidden");
            $(this).closest("tr").addClass("notPrint");

            var count = parseFloat($(this).closest("tr").children("[id *= 'lvAllPrint_Td5']").html().trim());
            var places = parseFloat($(this).closest("tr").children("[id *= 'lvAllPrint_Td11']").children()[1].value.trim());
            var curId = parseFloat($(this).closest("tr").children("[id *= 'lvAllPrint_Td2']").html().trim());
            var otherCount = $("tr[id*='lvAllPrint_Tr2']:not('.hidden')").length;

            var price = (parseFloat($(this).closest("tr").children("[id *= 'lvAllPrint_Td7']").text().trim().replace(",", ".").replace(" ", "").replace(/\u00a0/g, '').replace(/&nbsp;/gi, '')) / otherCount).toFixed(2);
            var remainPrice = (parseFloat($(this).closest("tr").children("[id *= 'lvAllPrint_Td7']").text().trim().replace(",", ".").replace(" ", "").replace(/\u00a0/g, '').replace(/&nbsp;/gi, '')) - (otherCount * price)).toFixed(2);
            
            var vaga = (parseFloat($(this).closest("tr").children("[id *= 'lvAllPrint_Td12']").children()[1].value.trim()) / otherCount).toFixed(2);
            var remainVaga = (parseFloat($(this).closest("tr").children("[id *= 'lvAllPrint_Td12']").children()[1].value.trim()) - (parseFloat(vaga) * otherCount)).toFixed(2);
            var numColumns = 0;

            var columnsAll = $("#lblOverNumber");                        
            $("#lblOverNumber").text(parseFloat(columnsAll.html().trim()) - count);

            $("tr[id*='lvAllPrint_Tr2']:not('.hidden')").each(function (index, value) {
                numColumns++;
            });
            numColumns = Math.floor(Math.random() * numColumns);

            $("tr[id*='lvAllPrint_Tr2']:not('.hidden')").each(function (index, value) {
                if (parseFloat($(this).children("[id *= 'lvAllPrint_Td2']").html().trim()) > curId) $(this).children("[id *= 'lvAllPrint_Td2']").html(parseFloat($(this).children("[id *= 'lvAllPrint_Td2']").html().trim()) - 1);
                if (index == numColumns) {                    
                    $(this).children("[id *= 'lvAllPrint_Td11']").children()[1].value = Number((parseFloat($(this).children("[id *= 'lvAllPrint_Td11']").children()[1].value.trim()) + places).toFixed(2));
                    $(this).children("[id *= 'lvAllPrint_Td11']").children()[1].defaultValue = $(this).children("[id *= 'lvAllPrint_Td11']").children()[1].value;
                }

                var resPrice = parseFloat($(this).children("[id *= 'lvAllPrint_Td7']").text().trim().replace(",", ".").replace(" ", "").replace(/\u00a0/g, '').replace(/&nbsp;/gi, '')) + parseFloat(price);
                if (index == 0 && remainPrice != null)
                {
                    resPrice = parseFloat(resPrice) + parseFloat(remainPrice);
                }
                
                $(this).children("[id *= 'lvAllPrint_Td6']").children()[1].defaultValue = toMoney(resPrice.toFixed(2)).replace(".", ",");
                $(this).children("[id *= 'lvAllPrint_Td6']").children()[1].value = toMoney(resPrice.toFixed(2)).replace(".", ",");
                $(this).children("[id *= 'lvAllPrint_Td7']").html(toMoney(resPrice.toFixed(2)).replace(".", ","));
                $(this).children("[id *= 'lvAllPrint_Td10']").html(toMoney(resPrice.toFixed(2)).replace(".", ","));
                if (index == 0 && remainVaga != null)   
                {
                    $(this).children("[id *= 'lvAllPrint_Td12']").children()[1].value = Number((parseFloat($(this).children("[id *= 'lvAllPrint_Td12']").children()[1].value.trim().replace(/\u00a0/g, '').replace(/&nbsp;/gi, '')) + parseFloat(vaga) + parseFloat(remainVaga)).toFixed(2));
                    $(this).children("[id *= 'lvAllPrint_Td12']").children()[1].defaultValue = $(this).children("[id *= 'lvAllPrint_Td12']").children()[1].value;
                }                                
                else
                {
                    $(this).children("[id *= 'lvAllPrint_Td12']").children()[1].value = Number((parseFloat($(this).children("[id *= 'lvAllPrint_Td12']").children()[1].value.trim().replace(/\u00a0/g, '').replace(/&nbsp;/gi, '')) + parseFloat(vaga)).toFixed(2));
                    $(this).children("[id *= 'lvAllPrint_Td12']").children()[1].defaultValue = $(this).children("[id *= 'lvAllPrint_Td12']").children()[1].value;
                }                    
            });
        });
    });
</script>
<asp:CheckBox runat="server" ID="cbNotPrintInPril2" CssClass="checkboxInListView"/>
<asp:Label runat="server" ID="lblStatus"/>
