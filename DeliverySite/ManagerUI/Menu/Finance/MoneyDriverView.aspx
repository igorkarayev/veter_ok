<%@ Page Title="" Language="C#" MasterPageFile="~/ManagerMasterPage.master" AutoEventWireup="true" CodeBehind="MoneyDriverView.aspx.cs" Inherits="Delivery.ManagerUI.Menu.Finance.MoneyDriverView" %>
<%@ Import Namespace="Delivery.Resources" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <script type="text/javascript">
        $(function () {
            $.datepicker.setDefaults($.datepicker.regional['ru']);
            $("#<%= stbDeliveryDate1.ClientID %>")
                .datepicker({ nextText: "", prevText: "", changeMonth: true, changeYear: true })
                .mask("99-99-9999");
            $("#<%= stbDeliveryDate2.ClientID %>")
                .datepicker({ nextText: "", prevText: "", changeMonth: true, changeYear: true })
                .mask("99-99-9999");
            $(".moneyMask").maskMoney();

        });
    </script>
    <h3 class="h3custom" style="margin-top: 0;">Контроль водителя</h3>
    <div>
        <div style="text-align: center; margin-bottom: 15px;">В списке водителей отображаются только те водители, у которых есть заявки со статусом "<%= TicketStatusesResources.Processed %>" либо , "<%= TicketStatusesResources.Completed %>".</div>
        <asp:Panel runat="server" ID="SearchPanel" CssClass="mainFont"  DefaultButton="btnCalc">
            <table class="table searchPanel" style="margin-left:auto; margin-right: auto;">
                <tr>
                    <td>
                        Водители:&nbsp;<asp:DropDownList runat="server" ID="sddlDrivers" CssClass="searchField ddl-control" Width="180px"></asp:DropDownList>
                    </td>
                    <td style="text-align: right;">
                        Дата отправки с:&nbsp;<asp:TextBox runat="server" ID="stbDeliveryDate1" CssClass="searchField form-control" Width="70px"></asp:TextBox>
                        &nbsp;по:&nbsp;<asp:TextBox runat="server" ID="stbDeliveryDate2" CssClass="searchField form-control" Width="70px"></asp:TextBox>
                        <asp:Button runat="server" Text="Сброс" CssClass="btn btn-default" ID="btnReload"/>
                        <asp:Button runat="server" Text="Контроль" CssClass="btn btn-default" ID="btnCalc"/>    
                    </td>
                </tr>
            </table>
        </asp:Panel>
       <asp:Panel class="infoBlock2" runat="server" ID="pnlResultPanel">
            <i>Данным водителем привезено:</i>
                                                <b><asp:Label runat="server" ID="lblReceivedUSDOver" Text="0"></asp:Label></b> USD;
                                                <b><asp:Label runat="server" ID="lblReceivedEUROver" Text="0"></asp:Label></b> EUR;
                                                <b><asp:Label runat="server" ID="lblReceivedRUROver" Text="0"></asp:Label></b> RUR;
                                                <b><asp:Label runat="server" ID="lblReceivedBLROver" Text="0"></asp:Label></b> BLR;<br/>
           <i>В пересчете на руб.по курсам клиента это:</i>
                                                <b><asp:Label runat="server" ID="lblAllReceivedInBLR" Text="0"></asp:Label></b> BLR;<br/>
           <i>Сумма согласованных/оценочных + за доставку (то что должно было получиться):</i>
                                                <b><asp:Label runat="server" ID="lblAgreedAssessedCost" Text="0"></asp:Label></b> BLR;<br/>
           <i>Разница:</i>
                                                <b><asp:Label runat="server" ID="lblDifference" Text="0"></asp:Label></b> BLR;<br/>
        </asp:Panel>
        
    </div>

</asp:Content>
