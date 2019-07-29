<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="BoxesNumberToPril2.ascx.cs" Inherits="Delivery.PrintServices.Controls.BoxesNumberToPril2" %>
<%@ Import Namespace="Delivery.AppServices" %>
<%@ Import Namespace="Delivery.BLL" %> <%@ Import Namespace="Delivery.BLL.StaticMethods" %>
<asp:Label runat="server" ID="lblPril2BoxesNumber" CssClass="boxes-number-topril2-tosumm" onclick='<%# String.Format("ShowTbPril2BoxesNumber({0},\"{1}\")", TicketID, ListViewControlFullID)%>'></asp:Label>
<asp:TextBox ID="tbPril2BoxesNumber" CssClass="boxes-number-topril2" style="display: none;" Width="20px" runat="server" 
    onblur='<%# String.Format("SaveBoxesNumberToPril2({0},\"{1}\",\"{2}\",\"{3}\",\"{4}\",\"{5}\")", TicketID, AppKey, ListViewControlFullID, UserID, UserIP, PageName)%>'
    onfocus='<%# String.Format("ClearLabelStatusPril2BoxesNumberToPril2({0},\"{1}\")", TicketID, ListViewControlFullID)%>' />
<asp:Label runat="server" CssClass="notPrint" ID="lblSavePril2BoxesNumberStatus"></asp:Label> 
