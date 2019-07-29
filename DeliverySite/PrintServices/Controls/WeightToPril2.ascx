<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="WeightToPril2.ascx.cs" Inherits="Delivery.PrintServices.Controls.WeightToPril2" %>
<%@ Import Namespace="Delivery.AppServices" %>
<%@ Import Namespace="Delivery.BLL" %> <%@ Import Namespace="Delivery.BLL.StaticMethods" %>
<asp:Label runat="server" ID="lblPril2Weight" CssClass="weight-topril2-tosumm"></asp:Label> <%-- onclick='<%# String.Format("ShowTbPril2Weight({0},\"{1}\")", TicketID, ListViewControlFullID)%>' --%>
<asp:TextBox ID="tbPril2Weight" CssClass="weight-topril2" style="display: none;" Width="20px" runat="server" 
    onblur='<%# String.Format("SaveWeightToPril2({0},\"{1}\",\"{2}\",\"{3}\",\"{4}\",\"{5}\")", TicketID, AppKey, ListViewControlFullID, UserID, UserIP, PageName)%>'
    onfocus='<%# String.Format("ClearLabelStatusWeightToPril2({0},\"{1}\")", TicketID, ListViewControlFullID)%>' />
<asp:Label runat="server" ID="lblSavePril2WeightStatus"></asp:Label> 
