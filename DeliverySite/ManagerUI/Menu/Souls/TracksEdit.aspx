<%@ Page Title="" Language="C#" MasterPageFile="~/ManagerMasterPage.master" AutoEventWireup="true" CodeBehind="TracksEdit.aspx.cs" Inherits="Delivery.ManagerUI.Menu.Souls.TracksEdit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <h3 class="h3custom" style="margin-top: 0;"><%# ActionText %> направления</h3>
    <div class="single-input-fild">
        <div class="form-group">
            <label for="<%= tbName.ClientID %>">Название направления</label>
            <asp:TextBox ID="tbName" runat="server" width="100%" CssClass="searchField form-control"/>
        </div>
        <div class="form-group">
            <label>Ответственный менеджер</label>
            <asp:DropDownList ID="ddlManager" runat="server" width="100%" CssClass="searchField ddl-control"/>
        </div>
        <div style="padding-top: 20px;">
            <asp:Button ID="btnCreate" cssclass="btn btn-default btn-right" runat="server" Text='<%# ButtonText %>' ValidationGroup="LoginGroup" style="margin-left: 30px;"/>
            <asp:Button ID="btnBack" OnClientClick="window.history.go(-1);return false;" cssclass="btn btn-default btn-right" runat="server" Text='Назад'/>
        </div>
        
        <asp:CustomValidator ID="CustomValidator11" ControlToValidate="tbName" ClientValidationFunction="validateIfEmpty" EnableClientScript="True" ValidationGroup="LoginGroup" ValidateEmptyText="True" runat="server"  Display="None" ErrorMessage="Вы не ввели название направления" ></asp:CustomValidator>
        
        <asp:ValidationSummary ID="ValidationSummary1" runat="server" ValidationGroup="LoginGroup" ShowMessageBox="true" ShowSummary="false"/>
    </div>
</asp:Content>
