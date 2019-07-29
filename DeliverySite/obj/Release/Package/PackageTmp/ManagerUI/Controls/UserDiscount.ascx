<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="UserDiscount.ascx.cs" Inherits="Delivery.ManagerUI.Controls.UserDiscount" %>
<%@ Import Namespace="Delivery.AppServices" %>
<%@ Import Namespace="Delivery.BLL" %> <%@ Import Namespace="Delivery.BLL.StaticMethods" %>
<asp:TextBox ID="tbUserDiscount" Width="25px" runat="server" 
    onblur='<%# String.Format("SaveUserDiscount({0},\"{1}\",\"{2}\",\"{3}\",\"{4}\",\"{5}\")", UserID, AppKey, ListViewControlFullID, CurentUserID, CurentUserIP, PageName)%>'
    onfocus='<%# String.Format("ClearLabelStatusUserDiscount({0},\"{1}\")", UserID, ListViewControlFullID)%>' />%<br/>
<asp:Label runat="server" ID="lblSaveUserDiscountStatus"></asp:Label> 
