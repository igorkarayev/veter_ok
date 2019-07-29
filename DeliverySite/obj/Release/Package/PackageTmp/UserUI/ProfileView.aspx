<%@ Page Title="" Language="C#" MasterPageFile="~/UserMasterPage.master" AutoEventWireup="true" CodeBehind="ProfileView.aspx.cs" Inherits="Delivery.UserUI.ProfileView" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <h3 class="h3custom" style="margin-top: 0;">Просмотр профиля пользователя</h3>
    <div class="single-text-fild" style="margin-bottom: 0px;">
        <asp:Label ID="Label4" runat="server" Width="35%" CssClass="labelForSpan">Статус</asp:Label>
        <asp:Label ID="lblStatus" runat="server" Width="55%" CssClass="spanForLabel"/>
    </div>
    
    <asp:Panel runat="server" ID="pnlStatusDescription" Visible="False">
        <div class="single-text-fild" style="margin-bottom: 0px;">
            <asp:Label ID="Label6" runat="server" Width="35%" CssClass="labelForSpan">Расшифровка статуса</asp:Label>
            <asp:Label ID="lblStatusDescription" runat="server" Width="55%" CssClass="spanForLabel"/>
        </div>
    </asp:Panel>

    <asp:Panel runat="server" ID="pnlFiz" >
        <div class="single-text-fild">
            <div class="text-field-group">
                <asp:Label runat="server" Width="35%" CssClass="labelForSpan">Фамилия</asp:Label>
                <asp:Label ID="lblFirstName" runat="server" Width="55%" CssClass="spanForLabel"/>
            </div>
            <div class="text-field-group">
                <asp:Label runat="server" Width="35%" CssClass="labelForSpan">Имя</asp:Label>
                <asp:Label ID="lblLastName" runat="server" Width="55%" CssClass="spanForLabel"/>
            </div>
            <div class="text-field-group">
                <asp:Label runat="server" Width="35%" CssClass="labelForSpan">Отчество</asp:Label>
                <asp:Label ID="lblThirdName" runat="server" Width="55%" CssClass="spanForLabel"/>
            </div>
            <div class="text-field-group">
                <asp:Label runat="server" Width="35%" CssClass="labelForSpan">Серия и номер паспорта</asp:Label>
                <asp:Label ID="lblPassportNumber" runat="server" Width="55%" CssClass="spanForLabel"/>
            </div>
            <div class="text-field-group">
                <asp:Label runat="server" Width="35%" CssClass="labelForSpan">Кем выдан</asp:Label>
                <asp:Label ID="lblPassportData" runat="server" Width="55%" CssClass="spanForLabel"/>
            </div>
            <div class="text-field-group">
                <asp:Label runat="server" Width="35%" CssClass="labelForSpan">Адрес проживания</asp:Label>
                <asp:Label ID="lblAddress" runat="server" Width="55%" CssClass="spanForLabel"/>
            </div>
            <div>
                <asp:Button ID="Button1" OnClientClick="window.history.go(-1);return false;" runat="server" CssClass="btn btn-default btn-right" Text='Назад' style="margin-right: 1%; margin-top: 0px;"/>
            </div>
        </div>
    </asp:Panel>
    
    <asp:Panel runat="server" ID="pnlUr" Visible="false">
        <div class="single-text-fild">
            <div class="text-field-group">
                <asp:Label ID="Label1" runat="server" Width="35%" CssClass="labelForSpan">ФИО директора</asp:Label>
                <asp:Label ID="lblDirectorFIO" runat="server" Width="55%" CssClass="spanForLabel"/>
            </div>
            <div class="text-field-group">
                <asp:Label ID="Label3" runat="server" Width="35%" CssClass="labelForSpan">Телефон директора</asp:Label>
                <asp:Label ID="lblDirectorPhone" runat="server" Width="55%" CssClass="spanForLabel"/>
            </div>
            <div class="text-field-group">
                <asp:Label ID="Label2" runat="server" Width="35%" CssClass="labelForSpan">Контактное лицо</asp:Label>
                <asp:Label ID="lblContactPersonFIO" runat="server" Width="55%" CssClass="spanForLabel"/>
            </div>
            <div class="text-field-group">
                <asp:Label ID="Label5" runat="server" Width="35%" CssClass="labelForSpan">Контактные телефоны</asp:Label>
                <asp:Label ID="lblContactPersonPhones" runat="server" Width="55%" CssClass="spanForLabel"/>
            </div>
            <div class="text-field-group">
                <asp:Label ID="Label9" runat="server" Width="35%" CssClass="labelForSpan">Название компании</asp:Label>
                <asp:Label ID="lblCompanuName" runat="server" Width="55%" CssClass="spanForLabel"/>
            </div>
            <div class="text-field-group">
                <asp:Label ID="Label11" runat="server" Width="35%" CssClass="labelForSpan">Юридический адрес</asp:Label>
                <asp:Label ID="lblCompanyAddress" runat="server" Width="55%" CssClass="spanForLabel"/>
            </div>
            <div class="text-field-group">
                <asp:Label ID="Label13" runat="server" Width="35%" CssClass="labelForSpan">Расчетный счет</asp:Label>
                <asp:Label ID="lblRS" runat="server" Width="55%" CssClass="spanForLabel"/>
            </div>
            <div class="text-field-group">
                <asp:Label ID="Label15" runat="server" Width="35%" CssClass="labelForSpan">УНП</asp:Label>
                <asp:Label ID="lblUNP" runat="server" Width="55%" CssClass="spanForLabel"/>
            </div>
            <div class="text-field-group">
                <asp:Label ID="Label17" runat="server" Width="35%" CssClass="labelForSpan">Название банка</asp:Label>
                <asp:Label ID="lblBankName" runat="server" Width="55%" CssClass="spanForLabel"/>
            </div>
            <div class="text-field-group">
                <asp:Label ID="Label19" runat="server" Width="35%" CssClass="labelForSpan">Адрес банка</asp:Label>
                <asp:Label ID="lblBankAddress" runat="server" Width="55%" CssClass="spanForLabel"/>
            </div>
            <div class="text-field-group">
                <asp:Label ID="Label21" runat="server" Width="35%" CssClass="labelForSpan">Код банка</asp:Label>
                <asp:Label ID="lblBankCode" runat="server" Width="55%" CssClass="spanForLabel"/>
            </div>
            <div>
                <asp:Button ID="Button2" OnClientClick="window.history.go(-1);return false;" runat="server" CssClass="btn btn-default btn-right" Text='Назад' style="margin-right: 1%; margin-top: 0px;"/>
            </div>
        </div>
    </asp:Panel>
</asp:Content>
