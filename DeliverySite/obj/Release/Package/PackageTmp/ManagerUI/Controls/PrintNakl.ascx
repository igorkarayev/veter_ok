<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="PrintNakl.ascx.cs" Inherits="Delivery.ManagerUI.Controls.PrintNakl" %>
<%@ Import Namespace="Delivery.AppServices" %>
<%@ Import Namespace="Delivery.BLL" %> <%@ Import Namespace="Delivery.BLL.StaticMethods" %>
<script>
    $(function () {
        $('<%# String.Format("{0}_PrintNakl_{1}_ddlPrintNakl_{1}", ListViewControlFullID, TicketID) %>').change(function () {
            <%# String.Format("SavePrintNakl({0},\"{1}\",\"{2}\",\"{3}\",\"{4}\",\"{5}\");", TicketID, AppKey, ListViewControlFullID, UserID, UserIP, PageName) %>  //сохраняем значение ПН
        });
    });
</script>
<asp:DropDownList ID="ddlPrintNakl" Width="50px" runat="server">
    <asp:listitem text="нет" value="0"/>
    <asp:listitem text="да" value="1"/>
</asp:DropDownList>
<asp:Label runat="server" ID="lblSavePrintNaklStatus"></asp:Label>
