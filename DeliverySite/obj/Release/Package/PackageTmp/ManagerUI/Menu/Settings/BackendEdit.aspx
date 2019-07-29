<%@ Page ValidateRequest="false" Title="" Language="C#" MasterPageFile="~/ManagerMasterPage.master" AutoEventWireup="true" CodeBehind="BackendEdit.aspx.cs" Inherits="Delivery.ManagerUI.Menu.Settings.BackendEdit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <h3 class="h3custom" style="margin-top: 0;">Изменение конфигураций</h3>
    <div class="single-input-fild">
        <div class="form-group">
            <i>Описание:</i> <asp:Label runat="server" ID="lblDescription"></asp:Label>
        </div>

        <div class="form-group">
            <i>Тег:</i> <asp:Label runat="server" ID="lblTag"></asp:Label>
        </div>

        <asp:Panel ID="pnlChangeDate" runat="server" Visible="False">
            <div class="form-group">
                <i>Изменен:</i> <asp:Label runat="server" ID="lblChangeDate"></asp:Label>
            </div>
        </asp:Panel>
        
        <div class="form-group">
            <label for="<%= tbValue.ClientID %>">Значение тэга</label>
            <asp:TextBox ID="tbValue" runat="server" style="width:100%; height: 50px;" CssClass="searchField form-control" TextMode="MultiLine"/>
        </div>
        <div>
            <asp:Button ID="btnCreate" runat="server" Text='Сохранить' CssClass="btn btn-default btn-right" style="margin-left: 30px;"  ValidationGroup="LoginGroup"/>
            <asp:Button ID="btnBack" OnClientClick="window.history.go(-1);return false;" runat="server" CssClass="btn btn-default btn-right" Text='Назад'/>
        </div>
    </div>
    
    <asp:CustomValidator ID="CustomValidator2" ControlToValidate="tbValue" runat="server"  ClientValidationFunction="validateIfEmpty" EnableClientScript="True" ValidationGroup="LoginGroup" ValidateEmptyText="True" Display="None" ErrorMessage="Значение тега не может быть пустым"></asp:CustomValidator>
    <asp:ValidationSummary ID="ValidationSummary1" runat="server" ValidationGroup="LoginGroup" ShowMessageBox="true" ShowSummary="false"/>
</asp:Content>
