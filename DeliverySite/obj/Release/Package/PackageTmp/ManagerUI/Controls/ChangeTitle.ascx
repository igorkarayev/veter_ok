<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ChangeTitle.ascx.cs" Inherits="DeliverySite.ManagerUI.Controls.ChangeTitle" %>
<%@ Import Namespace="Delivery.AppServices" %>
<%@ Import Namespace="Delivery.BLL" %> <%@ Import Namespace="Delivery.BLL.StaticMethods" %>
<script>
    $(function () {
        
    });
</script>
<div style='color: <%# OtherMethods.ChangeTitleColor(TicketID) %>;width: 100%; height: 100%; text-align: right;'>
    <asp:TextBox Height="70px" ID="tbChangeTitle" runat="server" TextMode="MultiLine" Rows="1" Width="144px"
        onblur='<%# String.Format("SaveChangeTitle({0},\"{1}\",\"{2}\",\"{3}\",\"{4}\",\"{5}\")", TicketID, AppKey, ListViewControlFullID, UserID, UserIP, PageName)%>'
        onfocus='<%# String.Format("ClearLabelStatusChangeTitle({0},\"{1}\")", TicketID, ListViewControlFullID)%>'
        style ='color: inherit' Text='<%# TextTitle %>'/>
    <asp:Label runat="server" ID="lblChangeTitle"></asp:Label> 
</div>