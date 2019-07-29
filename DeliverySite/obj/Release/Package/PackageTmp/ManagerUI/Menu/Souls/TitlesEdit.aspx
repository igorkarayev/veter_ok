<%@ Page Title="" Language="C#" MasterPageFile="~/ManagerMasterPage.master" AutoEventWireup="true" CodeBehind="TitlesEdit.aspx.cs" Inherits="Delivery.ManagerUI.Menu.Souls.TitlesEdit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <h3 class="h3custom" style="margin-top: 0;"><%# ActionText %> наименования</h3>
    <div class="single-input-fild">
        <div class="form-group">
            <label>Наименование</label>
            <asp:TextBox ID="tbName" runat="server" width="100%" CssClass="searchField form-control"/>
        </div>
        <div class="form-group">
            <label>Наценочный коэффициент</label>
            <asp:TextBox ID="tbMarginCoefficient" runat="server" width="100%" CssClass="searchField form-control"/>
        </div>
        <div class="form-group">
            <label>Минимальный вес, кг</label>
            <asp:TextBox ID="tbWeightMin" runat="server" width="100%" CssClass="searchField form-control"/>
        </div>
        <div class="form-group">
            <label>Максимальный вес, кг</label>
            <asp:TextBox ID="tbWeightMax" runat="server" width="100%" CssClass="searchField form-control"/>
        </div>
        <div class="form-group">
            <label>Приложение?</label>
            <asp:CheckBox ID="cbAdditive" runat="server"/>
        </div>
        <div class="form-group">
            <label>Может быть БУ</label>
            <asp:CheckBox ID="cbCanBeWithoutAkciza" runat="server"/>
        </div>
        <div class="form-group">
            <label>Добавочная сумма б\у</label>
            <asp:TextBox ID="tbAdditiveCostWithoutAkciza" runat="server" width="100%" CssClass="searchField form-control"/>
        </div>
        <div class="form-group">
            <label>Категория</label>
            <asp:DropDownList ID="ddlCategory" runat="server" CssClass="ddl-control" width="100%"/>
        </div>
        <div>
            <asp:Button ID="btnCreate" runat="server" Text="<%# ButtonText %>" CssClass="btn btn-default btn-right" style="margin-left: 30px;" ValidationGroup="LoginGroup"/>
            <asp:Button ID="btnDelete" runat="server" Text="Удалить" OnClientClick="return confirmDelete();" CssClass="btn btn-default btn-right" style="margin-left: 30px;"/>
            <asp:Button ID="btnBack" OnClientClick="window.history.go(-1);return false;" runat="server" CssClass="btn btn-default btn-right" Text="Назад"/>
        </div>
    </div>

    <script>
        $(function() {
            $("#<%# tbAdditiveCostWithoutAkciza.ClientID %>").val("0");
        });
    </script>
    
    <asp:CustomValidator ID="CustomValidator1" ControlToValidate="tbName" ClientValidationFunction="validateIfEmpty" EnableClientScript="True" ValidationGroup="LoginGroup" ValidateEmptyText="True" runat="server"  Display="None" ErrorMessage="Вы не ввели логи (почтовый адрес)" ></asp:CustomValidator>
    <asp:CustomValidator ID="CustomValidator18" ControlToValidate="tbMarginCoefficient" ClientValidationFunction="validateIfEmpty" EnableClientScript="True" ValidationGroup="LoginGroup" ValidateEmptyText="True" runat="server"  Display="None" ErrorMessage="Вы не ввели пароль" ></asp:CustomValidator>
    <asp:CustomValidator ID="CustomValidator2" ControlToValidate="tbWeightMin" ClientValidationFunction="validateIfEmpty" EnableClientScript="True" ValidationGroup="LoginGroup" ValidateEmptyText="True" runat="server"  Display="None" ErrorMessage="Вы не ввели пароль" ></asp:CustomValidator>
    <asp:CustomValidator ID="CustomValidator3" ControlToValidate="tbWeightMax" ClientValidationFunction="validateIfEmpty" EnableClientScript="True" ValidationGroup="LoginGroup" ValidateEmptyText="True" runat="server"  Display="None" ErrorMessage="Вы не ввели пароль" ></asp:CustomValidator>
    <asp:CustomValidator ID="CustomValidator4" ControlToValidate="tbAdditiveCostWithoutAkciza" ClientValidationFunction="validateIfEmpty" EnableClientScript="True" ValidationGroup="LoginGroup" ValidateEmptyText="True" runat="server"  Display="None" ErrorMessage="Вы не ввели пароль" ></asp:CustomValidator>
    <asp:ValidationSummary ID="ValidationSummary1" runat="server" ValidationGroup="LoginGroup" ShowMessageBox="true" ShowSummary="false"/>
</asp:Content>