<%@ Page Title="" Language="C#" MasterPageFile="~/ManagerMasterPage.master" AutoEventWireup="true" CodeBehind="MoneyDeliveredView.aspx.cs" Inherits="Delivery.ManagerUI.Menu.Finance.MoneyDeliveredView" %>
<%@ Import Namespace="Delivery.Resources" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <h3 class="h3custom" style="margin-top: 0;">Сумма заявок "<%= TicketStatusesResources.Processed %>"</h3>
    <div>
        <div style="text-align: center; margin-bottom: 15px;">Сумма всех приввезенных заявок со статусом "<%= TicketStatusesResources.Processed %>"</div>

        <asp:Panel class="infoBlock2" style="text-align: left; width: 300px; margin-left: auto; margin-right: auto;" runat="server" ID="pnlResultPanel">
            <ul>
                <li>
                    <b><asp:Label runat="server" ID="lblReceivedUSDOver" Text="0"></asp:Label></b> USD;
                </li>
                <li>
                    <b><asp:Label runat="server" ID="lblReceivedEUROver" Text="0"></asp:Label></b> EUR;
                </li>
                <li>
                     <b><asp:Label runat="server" ID="lblReceivedRUROver" Text="0"></asp:Label></b> RUR;
                </li>
                <li>
                    <b><asp:Label runat="server" ID="lblReceivedBLROver" Text="0"></asp:Label></b> BLR;
                </li>
            </ul>
        </asp:Panel>
        
    </div>

</asp:Content>
