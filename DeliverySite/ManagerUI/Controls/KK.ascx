<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="KK.ascx.cs" Inherits="DeliverySite.ManagerUI.Controls.KK" %>
<asp:TextBox ID="tbKK" Width="60px" runat="server" 
    onblur='<%# String.Format("SaveKK({0},\"{1}\",\"{2}\",\"{3}\",\"{4}\",\"{5}\")", TicketID, AppKey, ListViewControlFullID, UserID, UserIP, PageName)%>'
    onfocus='<%# String.Format("ClearLabelStatusKK({0},\"{1}\")", TicketID, ListViewControlFullID)%>' />
<asp:Label runat="server" ID="lblSaveKKStatus"></asp:Label> 