<%@ Page Title="" Language="C#" MasterPageFile="~/ManagerMasterPage.master" AutoEventWireup="true" CodeBehind="CategoryEdit.aspx.cs" Inherits="Delivery.ManagerUI.Menu.Souls.SectionEdit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <h3 class="h3custom" style="margin-top: 0;"><%# ActionText %> категории</h3>
    <div class="single-input-fild">
        <div class="form-group">
            <label>Название</label>
            <asp:TextBox ID="tbName" runat="server" width="100%" CssClass="searchField form-control"/>
        </div>
        <div>
            <asp:Button ID="btnCreate" runat="server" Text='<%# ButtonText %>' CssClass="btn btn-default btn-right" style="margin-left: 30px;"/>
            <asp:Button ID="btnBack" OnClientClick="window.history.go(-1);return false;" runat="server" CssClass="btn btn-default btn-right" Text='Назад'/>
        </div>
    </div>
</asp:Content>
