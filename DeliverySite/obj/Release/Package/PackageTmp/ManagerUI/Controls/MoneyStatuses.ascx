<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="MoneyStatuses.ascx.cs" Inherits="Delivery.ManagerUI.Controls.MoneyStatuses" %>
<%@ Import Namespace="Delivery.Resources" %>
<script>
    $(function () {
        $('<%# String.Format("{0}_MoneyStatuses_{1}_ddlMoneyStatuses_{1}", ListViewControlFullID, TicketID) %>').change(function () {
            var flag = true;
            flag = <%# String.Format("SaveMoneyStatuses({0},\"{1}\",\"{2}\",\"{3}\",\"{4}\",\"{5}\")", TicketID, AppKey, ListViewControlFullID, UserID, UserIP, PageName) %>;
            if (flag) {
                if ($(this).val() == 3) {
                    $("<%= ListViewControlFullID%>_Tr2_<%= TicketID%>").attr('class', 'yellowRow');
                    $("<%= ListViewControlFullID%>_tbReceivedBLR_<%= TicketID %>").val("0");
                    $("<%= ListViewControlFullID%>_tbReceivedUSD_<%= TicketID %>").val("0");
                    $("<%= ListViewControlFullID%>_tbReceivedEUR_<%= TicketID %>").val("0");
                    $("<%= ListViewControlFullID%>_tbReceivedRUR_<%= TicketID %>").val("0");
                }

                if ($(this).val() == 5) {
                    $("<%= ListViewControlFullID%>_Tr2_<%= TicketID%>").attr('class', 'greenRow');
                }

                if ($(this).val() == 7) {
                    $("<%= ListViewControlFullID%>_Tr2_<%= TicketID%>").attr('class', 'grayRow');
                    $("<%= ListViewControlFullID%>_tbReceivedBLR_<%= TicketID %>").val("0");
                    $("<%= ListViewControlFullID%>_tbReceivedUSD_<%= TicketID %>").val("0");
                    $("<%= ListViewControlFullID%>_tbReceivedEUR_<%= TicketID %>").val("0");
                    $("<%= ListViewControlFullID%>_tbReceivedRUR_<%= TicketID %>").val("0");
                }

                if ($(this).val() == 12) {
                    $("<%= ListViewControlFullID%>_Tr2_<%= TicketID%>").attr('class', 'turquoiseRow');
                }

                if ($(this).val() == 4 || $(this).val() == 11) {
                    $("<%= ListViewControlFullID%>_Tr2_<%= TicketID%>").attr('class', 'yellowRow');
                }

                if ($(this).val() == 5) {
                    $("<%= ListViewControlFullID%>_Tr2_<%= TicketID%>").attr('class', 'greenRow');
                }

                if ($(this).val() == 7 || $(this).val() == 14 || $(this).val() == 8 || $(this).val() == 17 || $(this).val() == 18) {
                    $("<%= ListViewControlFullID%>_Tr2_<%= TicketID%>").attr('class', 'grayRow');
                }

                if ($(this).val() == 13 || $(this).val() == 15 || $(this).val() == 16) {
                    $("<%= ListViewControlFullID%>_Tr2_<%= TicketID%>").attr('class', 'orangeRow');
                }
            }
        });
    });
</script>
<asp:DropDownList ID="ddlMoneyStatuses" Width="63px" runat="server">
    <asp:listitem text="4" value="4"/>
    <asp:listitem text="3" value="3"/>
    <asp:listitem text="5" value="5"/>
    <asp:listitem text="12" value="12"/>
    <asp:listitem text="11" value="11"/>
    <asp:listitem text="7" value="7"/>
    <asp:listitem text="13" value="13"/>
    <asp:listitem text="14" value="14"/>
    <asp:listitem text="8" value="8"/>
    <asp:listitem text="15" value="15"/>    
    <asp:listitem text="16" value="16"/>
    <asp:listitem text="17" value="17"/>
    <asp:listitem text="18" value="18"/>

</asp:DropDownList>
<asp:Label runat="server" ID="lblSaveMoneyStatusesStatus"></asp:Label>
