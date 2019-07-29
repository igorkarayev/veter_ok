<%@ Page Title="" Language="C#" MasterPageFile="~/ManagerMasterPage.master" AutoEventWireup="true" CodeBehind="DriverView.aspx.cs" Inherits="Delivery.ManagerUI.Menu.Souls.DriverView" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <h3 class="h3custom" style="margin-top: 0;">Просмотр информации о водителе</h3>
    <div class="single-text-fild">
        <div class="text-field-group">
            <asp:Label ID="Label1" runat="server" Width="35%" CssClass="labelForSpan">ID</asp:Label>
            <asp:Label ID="lblID" runat="server" Width="55%" CssClass="spanForLabel"/>
        </div>
        <div class="text-field-group">
            <asp:Label ID="Label7" runat="server" Width="35%" CssClass="labelForSpan">Статус</asp:Label>
            <asp:Label ID="lblStatus" runat="server" Width="55%" CssClass="spanForLabel"/>
        </div>
        <div class="text-field-group">
            <asp:Label ID="Label9" runat="server" Width="35%" CssClass="labelForSpan">Машина</asp:Label>
            <asp:HyperLink runat="server" ID="hlCar">
                <asp:Label ID="lblCar" runat="server" Width="55%" CssClass="spanForLabel"/>
            </asp:HyperLink>
        </div>
        
        <hr class="styleHR2"/>

        <div class="text-field-group">
            <asp:Label ID="Label2" runat="server" Width="35%" CssClass="labelForSpan">ФИО</asp:Label>
            <asp:Label ID="lblFIO" runat="server" Width="55%" CssClass="spanForLabel"/>
        </div>
        <div class="text-field-group">
            <asp:Label ID="Label6" runat="server" Width="35%" CssClass="labelForSpan">Телефон #1</asp:Label>
            <asp:Label ID="lblPhoneOne" runat="server" Width="55%" CssClass="spanForLabel"/>
        </div>
        <div class="text-field-group">
            <asp:Label ID="Label8" runat="server" Width="35%" CssClass="labelForSpan">Телефон #2</asp:Label>
            <asp:Label ID="lblPhoneTwo" runat="server" Width="55%" CssClass="spanForLabel"/>
        </div>
        <div class="text-field-group">
            <asp:Label ID="Label11" runat="server" Width="35%" CssClass="labelForSpan">Домашний телефон</asp:Label>
            <asp:Label ID="lblHomePhone" runat="server" Width="55%" CssClass="spanForLabel"/>
        </div>
        <div class="text-field-group">
            <asp:Label ID="Label5" runat="server" Width="35%" CssClass="labelForSpan">Домашний адрес</asp:Label>
            <asp:Label ID="lblHomeAddress" runat="server" Width="55%" CssClass="spanForLabel"/>
        </div>
        <div class="text-field-group">
            <asp:Label ID="Label13" runat="server" Width="35%" CssClass="labelForSpan">Дата рождения</asp:Label>
            <asp:Label ID="lblBirthDay" runat="server" Width="55%" CssClass="spanForLabel"/>
        </div>
        <div class="text-field-group">
            <asp:Label ID="Label15" runat="server" Width="35%" CssClass="labelForSpan">ФИО родственника</asp:Label>
            <asp:Label ID="lblContactPersonFIO" runat="server" Width="55%" CssClass="spanForLabel"/>
        </div>
        <div class="text-field-group">
            <asp:Label ID="Label17" runat="server" Width="35%" CssClass="labelForSpan">Телефон родственника</asp:Label>
            <asp:Label ID="lblContactPersonPhone" runat="server" Width="55%" CssClass="spanForLabel"/>
        </div>
        
        <hr class="styleHR2"/>

        <div class="text-field-group">
            <asp:Label ID="Label19" runat="server" Width="35%" CssClass="labelForSpan">Паспорт</asp:Label>
            <asp:Label ID="lblPassportData" runat="server" Width="55%" CssClass="spanForLabel"/>
        </div>
        <div class="text-field-group">
            <asp:Label ID="Label4" runat="server" Width="35%" CssClass="labelForSpan">Личный код</asp:Label>
            <asp:Label ID="lblPersonalNumber" runat="server" Width="55%" CssClass="spanForLabel"/>
        </div>
        <div class="text-field-group">
            <asp:Label ID="Label21" runat="server" Width="35%" CssClass="labelForSpan">Выдавший орган</asp:Label>
            <asp:Label ID="lblROVD" runat="server" Width="55%" CssClass="spanForLabel"/>
        </div>
        <div class="text-field-group">
            <asp:Label ID="Label23" runat="server" Width="35%" CssClass="labelForSpan">Дата выдачи</asp:Label>
            <asp:Label ID="lblDateOfIssue" runat="server" Width="55%" CssClass="spanForLabel"/>
        </div>
        <div class="text-field-group">
            <asp:Label ID="Label25" runat="server" Width="35%" CssClass="labelForSpan">Действителен до</asp:Label>
            <asp:Label ID="lblValidity" runat="server" Width="55%" CssClass="spanForLabel"/>
        </div>
        <div class="text-field-group">
            <asp:Label ID="Label27" runat="server" Width="35%" CssClass="labelForSpan">Адрес прописки</asp:Label>
            <asp:Label ID="lblRegistrationAddress" runat="server" Width="55%" CssClass="spanForLabel"/>
        </div>
        
        <hr class="styleHR2"/>
        
        <div class="text-field-group">
            <asp:Label ID="Label3" runat="server" Width="35%" CssClass="labelForSpan">Номер ВУ</asp:Label>
            <asp:Label ID="lblDriverPassport" runat="server" Width="55%" CssClass="spanForLabel"/>
        </div>
        <div class="text-field-group">
            <asp:Label ID="Label29" runat="server" Width="35%" CssClass="labelForSpan">Дата выдачи ВУ</asp:Label>
            <asp:Label ID="lblDriverPassportDateOfIssue" runat="server" Width="55%" CssClass="spanForLabel"/>
        </div>
        <div class="text-field-group">
            <asp:Label ID="Label31" runat="server" Width="35%" CssClass="labelForSpan">Действительно до</asp:Label>
            <asp:Label ID="lblDriverPassportValidity" runat="server" Width="55%" CssClass="spanForLabel"/>
        </div>
        <div class="text-field-group">
            <asp:Label ID="Label33" runat="server" Width="35%" CssClass="labelForSpan">Дата выдачи мед. справки</asp:Label>
            <asp:Label ID="lblMedPolisDateOfIssue" runat="server" Width="55%" CssClass="spanForLabel"/>
        </div>
        <div class="text-field-group">
            <asp:Label ID="Label35" runat="server" Width="35%" CssClass="labelForSpan">Действительна до</asp:Label>
            <asp:Label ID="lblMedPolisValidity" runat="server" Width="55%" CssClass="spanForLabel"/>
        </div>

        <div>
            <asp:Button ID="btnEdit" OnClick="btnEdit_click"  CssClass="btn btn-default btn-right"  runat="server" Text='Редактировать' style="margin-left: 35px;"/> 
            <asp:Button ID="Button1" OnClientClick="window.history.go(-1);return false;" runat="server" CssClass="btn btn-default btn-right" Text='Назад'/>
        </div>
    </div>
</asp:Content>
