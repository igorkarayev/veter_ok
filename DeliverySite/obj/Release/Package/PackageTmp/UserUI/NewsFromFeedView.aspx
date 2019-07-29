<%@ Page Title="" Language="C#" MasterPageFile="~/UserMasterPage.master" AutoEventWireup="true" CodeBehind="NewsFromFeedView.aspx.cs" Inherits="Delivery.UserUI.NewsFromFeedView" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="blueTitle">
        <asp:Label ID="lblTitle" runat="server" />
    </div>
               
    <div style="width: 100%; text-align: justify; margin-bottom: 10px;">
        <div ID="lblBody" runat="server"/>
    </div>

    <div style="font-size: 11px; width: 49%; display: inline-block; text-align: left; margin: 8px 0; font-style: italic">
        Опубликована <asp:Label ID="lblCreateDate" runat="server" />
    </div>
    
    <div style="width: 49%; text-align: right; display: inline-block; font-weight: bold; font-size: 12px;">
        <asp:HyperLink CssClass="labelLink" ID="LinkButton1" runat="server"  OnClick="javascript:history.go(-1);">назад</asp:HyperLink>
    </div>
    
    <script>
        var unreadNewsNotDisplay = true;
    </script>

</asp:Content>
