<%@ Page Title="" Language="C#" MasterPageFile="~/EmptyMasterPage.master" AutoEventWireup="true" CodeBehind="UserNotification.aspx.cs" Inherits="Delivery.UserNotification" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div style="text-align: center">
        <h2><asp:Label runat="server" ID="lblNotifTitle"></asp:Label></h2>
    </div>
    <hr style="margin-top: 0;">
    <div style="overflow: hidden; margin-bottom: 30px;">
        <asp:Label runat="server" ID="lblNotifBody"></asp:Label><br/>
        <asp:Button ID="btnToMainPage" runat="server" CssClass="btn btn-default" Text='На главную' style="width: 130px; float: right; font-size: 18px;"/>
        <asp:Button ID="btnBack" OnClientClick="window.history.go(-1);return false;" runat="server" CssClass="btn btn-default btn-right" style="margin-right: 15px; width: 100px; float: right; font-size: 18px;" Text='Назад'/>
    </div>
    
    <hr style="margin-top: 0;">
</asp:Content>
