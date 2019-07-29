<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Comment.ascx.cs" Inherits="Delivery.ManagerUI.Controls.Comment" %>
<%@ Import Namespace="Delivery.AppServices" %>
<%@ Import Namespace="Delivery.BLL" %> <%@ Import Namespace="Delivery.BLL.StaticMethods" %>
<div class="noteDiv" style="width: 100%; margin-bottom: 10px; font-weight: normal; font-style: normal; text-align: left;">
    <asp:Label runat="server" ID="lblCommentHistory"></asp:Label> 
</div>
<asp:TextBox ID="tbComment" Width="190px" runat="server" TextMode="MultiLine" Rows="1"
    onblur='<%# String.Format("SaveComment({0},\"{1}\",\"{2}\",\"{3}\",\"{4}\",\"{5}\")", TicketID, AppKey, ListViewControlFullID, UserID, UserIP, PageName)%>'
    onfocus='<%# String.Format("ClearLabelStatusComment({0},\"{1}\")", TicketID, ListViewControlFullID)%>' />
<asp:Label runat="server" ID="lblSaveCommentStatus"></asp:Label> 
