<%@ Page Title="" Language="C#" MasterPageFile="~/ManagerMasterPage.master" AutoEventWireup="true" CodeBehind="MoneyDeliveryView.aspx.cs" Inherits="Delivery.ManagerUI.Menu.Finance.MoneyGruzobozView" %>
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
    <h3 class="h3custom" style="margin-top: 0;">Сумма "За услугу"</h3>
    <div>
        <div style="text-align: center; margin-bottom: 15px;">
            Сумма "За услуг" по заявкам со статусом 
            "<%= TicketStatusesResources.Processed %>", 
            "<%= TicketStatusesResources.Completed %>", 
            "<%= TicketStatusesResources.Return_InStock %>", 
            "<%= TicketStatusesResources.Delivered %>", 
            "<%= TicketStatusesResources.Exchange_InStock %>", 
            "<%= TicketStatusesResources.DeliveryFromClient_InStock %>"
        </div>
        <asp:Panel runat="server" ID="SearchPanel" CssClass="mainFont"  DefaultButton="btnCalc">
            <table class="table searchPanel" style="margin-left:auto; margin-right: auto;">
                <tr>
                    <td style="text-align: center;">
                        Дата отправки с:&nbsp;<asp:TextBox runat="server" ID="stbDeliveryDate1" CssClass="searchField form-control" Width="70px"></asp:TextBox>
                        &nbsp;по:&nbsp;<asp:TextBox runat="server" ID="stbDeliveryDate2" CssClass="searchField form-control" Width="70px"></asp:TextBox>
                        <asp:Button runat="server" Text="Сброс" CssClass="btn btn-default" ID="btnReload"/>
                        <asp:Button runat="server" Text="Считать" CssClass="btn btn-default" ID="btnCalc"/>    
                    </td>
                </tr>
            </table>
        </asp:Panel>
       <asp:Panel class="infoBlock2" runat="server" ID="pnlResultPanel">
            <i>По данным критериям поиска сумма "За услугу" составляет:</i> <b><asp:Label runat="server" ID="lblGruzobozCostOver" Text="0"></asp:Label></b> BLR;
        </asp:Panel>
        
    </div>

</asp:Content>
