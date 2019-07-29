<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="UserCategory.ascx.cs" Inherits="Delivery.ManagerUI.Controls.Clients.UserCategory" %>
<%@ Import Namespace="Delivery.BLL.Helpers" %>
<asp:ListView runat="server" ID="lvAllTracks">
    <LayoutTemplate>
        <table class="table" runat="server">
            <tr runat="server" id="itemPlaceholder"></tr>
        </table>
    </LayoutTemplate>
    <ItemTemplate>
        <tr id="Tr2" runat="server">
            <td id="Td2" runat="server" style="text-align: left;">
                <asp:HyperLink ID="HyperLink2" runat="server" NavigateUrl='<%#"~/ManagerUI/Menu/Souls/UserToCategoryView.aspx?id=" + ClientID %>'>
                    <asp:Label ID="lblName" runat="server" Text='<%#CategoryHelper.IdToName(Eval("CategoryID").ToString()) %>' />
                </asp:HyperLink>
            </td>
        </tr>
    </ItemTemplate>
</asp:ListView>
