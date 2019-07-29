<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="PrintNaklInMap.ascx.cs" Inherits="Delivery.ManagerUI.Controls.PrintNaklInMap" %>
<%@ Import Namespace="Delivery.AppServices" %>
<%@ Import Namespace="Delivery.BLL" %> <%@ Import Namespace="Delivery.BLL.StaticMethods" %>
<script>
    $(function () {
        if ($('<%# String.Format("{0}_PrintNaklInMap_{1}_ddlPrintNaklInMap_{1}", ListViewControlFullID, TicketID) %>').val() == "1") {
            $('<%# String.Format("{0}_PrintNakl_{1}_ddlPrintNakl_{1}", ListViewControlFullID, TicketID) %>').attr('disabled', 'disabled');
        }

        $('<%# String.Format("{0}_PrintNaklInMap_{1}_ddlPrintNaklInMap_{1}", ListViewControlFullID, TicketID) %>').change(function () {
            if ($(this).val() == "1") {
                $('<%# String.Format("{0}_PrintNakl_{1}_ddlPrintNakl_{1}", ListViewControlFullID, TicketID) %>').attr('disabled', 'disabled'); //делаем ПН неизменяемым
                $('<%# String.Format("{0}_PrintNakl_{1}_ddlPrintNakl_{1}", ListViewControlFullID, TicketID) %>').val("0"); //выставляем ПН в "нет"
                <%# String.Format("SavePrintNaklInMap({0},\"{1}\",\"{2}\",\"{3}\",\"{4}\",\"{5}\");", TicketID, AppKey, ListViewControlFullID, UserID, UserIP, PageName) %>  //сохраняем значение ТТН
                <%# String.Format("SavePrintNakl({0},\"{1}\",\"{2}\",\"{3}\",\"{4}\",\"{5}\");", TicketID, AppKey, ListViewControlFullID, UserID, UserIP, PageName) %>  //сохраняем значение ПН
            } else {
                $('<%# String.Format("{0}_PrintNakl_{1}_ddlPrintNakl_{1}", ListViewControlFullID, TicketID) %>').removeAttr('disabled'); //делаем ПН изменяемым
                <%# String.Format("SavePrintNaklInMap({0},\"{1}\",\"{2}\",\"{3}\",\"{4}\",\"{5}\");", TicketID, AppKey, ListViewControlFullID, UserID, UserIP, PageName) %> //сохраняем значение ТТН
            }
        });

        $('<%# String.Format("{0}_PrintNaklInMap_{1}_ddlAvailableOtherDocuments_{1}", ListViewControlFullID, TicketID) %>').change(function () {
            <%# String.Format("SavePrintNaklInMap({0},\"{1}\",\"{2}\",\"{3}\",\"{4}\",\"{5}\");", TicketID, AppKey, ListViewControlFullID, UserID, UserIP, PageName) %> //сохраняем значение ТТН
        });
    });
</script>
<div style="text-align: left;">
    <span style="font-size: 10px; font-weight: normal;">ТТН:</span> <asp:DropDownList ID="ddlPrintNaklInMap" Width="50px" runat="server" >
        <asp:listitem text="нет" value="0"/>
        <asp:listitem text="да" value="1"/>
    </asp:DropDownList><br/>
    <span style="font-size: 10px; font-weight: normal;">ПД:</span> <asp:DropDownList ID="ddlAvailableOtherDocuments" Width="50px" runat="server" >
        <asp:listitem text="нет" value="0"/>
        <asp:listitem text="да" value="1"/>
    </asp:DropDownList>
    <asp:Label runat="server" ID="lblSavePrintNaklInMapStatus"></asp:Label>
</div>

