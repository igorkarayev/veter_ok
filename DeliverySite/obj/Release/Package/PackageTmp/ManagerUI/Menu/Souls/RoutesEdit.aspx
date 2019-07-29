<%@ Page Title="" Language="C#" MasterPageFile="~/ManagerMasterPage.master" AutoEventWireup="true" CodeBehind="RoutesEdit.aspx.cs" Inherits="Delivery.ManagerUI.Menu.Souls.RoutesEdit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="single-input-fild">
        <div class="form-group">
            <label for="<%= tbName.ClientID %>">Название маршрута</label>
            <asp:TextBox ID="tbName" runat="server" width="100%" CssClass="searchField form-control"/>
        </div>
        <div>
            <asp:Button ID="btnCreate" runat="server" Text='<%# ButtonText %>' CssClass="btn btn-default btn-right" style="margin-left: 30px;"/>
            <asp:Button ID="btnBack" OnClientClick="window.history.go(-1);return false;" runat="server" CssClass="btn btn-default btn-right" Text='Назад'/>
        </div>
    </div>
</asp:Content>
