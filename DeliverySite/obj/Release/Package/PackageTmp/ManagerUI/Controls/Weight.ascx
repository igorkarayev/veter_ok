<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Weight.ascx.cs" Inherits="Delivery.ManagerUI.Controls.Weight" %>
<asp:TextBox ID="tbWeight" Width="60px" runat="server" 
    onblur='<%# String.Format("SaveWeight({0},\"{1}\",\"{2}\",\"{3}\",\"{4}\",\"{5}\")", TicketID, AppKey, ListViewControlFullID, UserID, UserIP, PageName)%>'
    onfocus='<%# String.Format("ClearLabelStatusWeight({0},\"{1}\")", TicketID, ListViewControlFullID)%>' />
<asp:Label runat="server" ID="lblSaveWeightStatus"></asp:Label> 
