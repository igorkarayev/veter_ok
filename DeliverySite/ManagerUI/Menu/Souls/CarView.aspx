  <%@ Page Title="" Language="C#" MasterPageFile="~/ManagerMasterPage.master" AutoEventWireup="true" CodeBehind="CarView.aspx.cs" Inherits="Delivery.ManagerUI.Menu.Souls.CarView" %>
<%@ Import Namespace="Delivery.BLL" %> <%@ Import Namespace="Delivery.BLL.StaticMethods" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <script type="text/javascript">
        function visiblField() {
            if ($('#<%= hfTypeID.ClientID %>').val() == "2") {
                $("#<%= pnlFiz.ClientID%>").hide();
                $("#<%= pnlUr.ClientID%>").show();
            } else {
                $("#<%= pnlUr.ClientID%>").hide();
                $("#<%= pnlFiz.ClientID%>").show();
            }
        }

        $(function () {
            //показываем нужные поля для конкретного типа профиля
            visiblField();
        });
    </script>
    <h3 class="h3custom" style="margin-top: 0;">Просмотр информации о автомобиле</h3>
    <div class="single-text-fild">
        <div class="text-field-group">
            <asp:Label ID="Label1" runat="server" Width="35%" CssClass="labelForSpan">ID</asp:Label>
            <asp:Label ID="lblID" runat="server" Width="55%" CssClass="spanForLabel"/>
        </div>

        <div class="text-field-group">
            <asp:Label ID="Label2" runat="server" Width="35%" CssClass="labelForSpan">Модель</asp:Label>
            <asp:Label ID="lblModel" runat="server" Width="55%" CssClass="spanForLabel"/>
        </div>

        <div class="text-field-group">
            <asp:Label ID="Label9" runat="server" Width="35%" CssClass="labelForSpan">Номер</asp:Label>
            <asp:Label ID="lblNumber" runat="server" Width="55%" CssClass="spanForLabel"/>
        </div>

        <div class="text-field-group">
            <asp:Label ID="Label7" runat="server" Width="35%" CssClass="labelForSpan">Водители</asp:Label>
            <asp:Label ID="lblDrivers" runat="server" Width="55%" CssClass="spanForLabel"/>
        </div>
        
        <div class="text-field-group">
            <asp:Label ID="Label12" runat="server" Width="35%" CssClass="labelForSpan">Тип лица владельца</asp:Label>
            <asp:Label ID="lblType" runat="server" Width="55%" CssClass="spanForLabel"/>
            <asp:HiddenField runat="server" ID="hfTypeID"/>
        </div>
        
        <asp:Panel runat="server" ID="pnlUr" >
            <div class="text-field-group">
                <asp:Label ID="Label3" runat="server" Width="35%" CssClass="labelForSpan">Компания</asp:Label>
                <asp:Label ID="lblCompanyName" runat="server" Width="55%" CssClass="spanForLabel"/>
            </div>
        </asp:Panel>

        <asp:Panel runat="server" ID="pnlFiz" >
            <div class="text-field-group">
                <asp:Label ID="Label4" runat="server" Width="35%" CssClass="labelForSpan">Фамилия владельца</asp:Label>
                <asp:Label ID="lblFirstName" runat="server" Width="55%" CssClass="spanForLabel"/>
            </div>
            
            <div class="text-field-group">
                <asp:Label ID="Label6" runat="server" Width="35%" CssClass="labelForSpan">Имя владельца</asp:Label>
                <asp:Label ID="lblLastName" runat="server" Width="55%" CssClass="spanForLabel"/>
            </div>
            
            <div class="text-field-group">
                <asp:Label ID="Label10" runat="server" Width="35%" CssClass="labelForSpan">Отчество владельца</asp:Label>
                <asp:Label ID="lblThirdName" runat="server" Width="55%" CssClass="spanForLabel"/>
            </div>
            
            <div class="text-field-group">
                <asp:Label ID="Label13" runat="server" Width="35%" CssClass="labelForSpan">Паспорт</asp:Label>
                <asp:Label ID="lblPassport" runat="server" Width="55%" CssClass="spanForLabel"/>
            </div>
            
            <div class="text-field-group">
                <asp:Label ID="Label15" runat="server" Width="35%" CssClass="labelForSpan">Личный код</asp:Label>
                <asp:Label ID="lblPersonalNumber" runat="server" Width="55%" CssClass="spanForLabel"/>
            </div>
            
            <div class="text-field-group">
                <asp:Label ID="Label17" runat="server" Width="35%" CssClass="labelForSpan">Прописка</asp:Label>
                <asp:Label ID="lblRegistrationAddress" runat="server" Width="55%" CssClass="spanForLabel"/>
            </div>
            
            <div class="text-field-group">
                <asp:Label ID="Label19" runat="server" Width="35%" CssClass="labelForSpan">Выдал</asp:Label>
                <asp:Label ID="lblROVD" runat="server" Width="55%" CssClass="spanForLabel"/>
            </div>
            
            <div class="text-field-group">
                <asp:Label ID="Label21" runat="server" Width="35%" CssClass="labelForSpan">Дата выдачи</asp:Label>
                <asp:Label ID="lblDateOfIssue" runat="server" Width="55%" CssClass="spanForLabel"/>
            </div>
            
            <div class="text-field-group">
                <asp:Label ID="Label23" runat="server" Width="35%" CssClass="labelForSpan">Действует до</asp:Label>
                <asp:Label ID="lblValidity" runat="server" Width="55%" CssClass="spanForLabel"/>
            </div>
            
            <div class="text-field-group">
                <asp:Label ID="Label25" runat="server" Width="35%" CssClass="labelForSpan">День рождения</asp:Label>
                <asp:Label ID="lblBirthDay" runat="server" Width="55%" CssClass="spanForLabel"/>
            </div>
        </asp:Panel>

        <div>
            <asp:Button ID="Button1" OnClientClick="window.history.go(-1);return false;" runat="server" CssClass="btn btn-default btn-right" Text='Назад' style="margin-right: 1%; margin-top: 0px;"/>
        </div>
    </div>
</asp:Content>
