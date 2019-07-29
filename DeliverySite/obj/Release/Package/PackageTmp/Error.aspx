<%@ Page Title="" Language="C#" MasterPageFile="~/EmptyMasterPage.master" AutoEventWireup="true" CodeBehind="Error.aspx.cs" Inherits="Delivery.Error" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div style="text-align: center;">
        <asp:Label runat="server" ID="lblErrorText"></asp:Label><br/><br/>
        <asp:Button ID="btnBack" OnClientClick="window.history.go(-1);return false;" runat="server" CssClass="btn btn-default" Text='Назад'/>
    </div>
</asp:Content>
