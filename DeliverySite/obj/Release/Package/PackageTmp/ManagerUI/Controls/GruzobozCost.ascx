<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="GruzobozCost.ascx.cs" Inherits="Delivery.ManagerUI.Controls.GruzobozCost" %>
<%@ Import Namespace="Delivery.BLL.StaticMethods" %>
<asp:TextBox ID="tbGruzobozCost" Width="60px" runat="server" CssClass="moneyMask"
    onblur='<%# String.Format("SaveGruzobozCost({0},\"{1}\",\"{2}\",\"{3}\",\"{4}\",\"{5}\")", TicketID, AppKey, ListViewControlFullID, SessionUserID, SessionUserIP, PageName)%>'
    onfocus='<%# String.Format("ClearLabelStatusGruzobozCost({0},\"{1}\")", TicketID, ListViewControlFullID)%>' />
<div style="font-size: 10px; color: olive">скидка: <asp:Label runat="server" ID="lblDiscount" Text='<%# MoneyMethods.UserDiscount(UserID) %>'></asp:Label>%</div>
<asp:Label runat="server" ID="lblSaveGruzobozCostStatus"></asp:Label> 
