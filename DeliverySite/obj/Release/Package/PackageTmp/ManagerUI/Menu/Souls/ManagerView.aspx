<%@ Page Title="" Language="C#" MasterPageFile="~/ManagerMasterPage.master" AutoEventWireup="true" CodeBehind="ManagerView.aspx.cs" Inherits="Delivery.ManagerUI.Menu.Souls.ManagerView" %>
<%@ Import Namespace="Delivery.BLL" %> <%@ Import Namespace="Delivery.BLL.StaticMethods" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <h3 class="h3custom" style="margin-top: 0;">Просмотр информации о сотруднике</h3>
    <div class="single-input-fild" style="width: 520px">
        <table class="table">
            <tr>
                <td style="width: 180px; padding-right: 50px; vertical-align: top;">
                    <div class="form-group">
                        <div>
                            <asp:Image runat="server" ID="imgGravatar"/>
                        </div>
                    </div>

                    <div class="form-group" style="text-align: center" runat="server" ID="divChangePassword">
                        <asp:HyperLink runat="server" ID="hlChangePassword">сменить пароль</asp:HyperLink>
                    </div>
                </td>
                <td style="vertical-align: top">
                    <div class="form-group">
                        <i>ID: </i>
                        <asp:Label ID="lblID" runat="server" style="width: 100%; font-weight: bold;"/>
                    </div>

                    <div class="form-group">
                        <i>Имя: </i>
                        <span style="font-weight: bold;"><asp:Label ID="lblName" runat="server" style="width: 100%"/></span>
                    </div>
    
                    <div class="form-group">
                        <i>Фамилия:</i>
                        <span style="font-weight: bold;"><asp:Label ID="lblFamily" runat="server" style="width: 100%"/></span>
                    </div>
                    
                    <div class="form-group">
                        <i>Логин:</i>
                        <span style="font-weight: bold;"><asp:Label ID="lblLogin" runat="server" style="width: 100%"/></span>
                    </div>
                    
                    <div class="form-group">
                        <i>Email:</i>
                        <span style="font-weight: bold;"><asp:Label ID="lblEmail" runat="server" style="width: 100%"/></span>
                    </div>

                    <hr class="styleHR2"/>
    
                    <div class="form-group">
                        <i>Роль:</i>
                        <span style="font-weight: bold;"><asp:Label runat="server" ID="lblRole" style="width: 100%"/></span>
                    </div>
                    
                    <div class="form-group">
                        <i>Статус:</i>
                        <span style="font-weight: bold;"><asp:Label ID="lblStatus" runat="server" style="width: 100%"/></span>
                    </div>

                    <div class="form-group">
                        <i>Доступ по WhiteList:</i>
                        <span style="font-weight: bold;"><asp:Label runat="server" ID="lblWhiteList" style="width: 100%"/></span>
                    </div>

                    <hr class="styleHR2"/>
                    
                    <div class="form-group" id="address">
                        <i>Адрес проживания:</i>
                        <span style="font-weight: bold;"><asp:Label runat="server" ID="lblAddress" style="width: 100%"/></span>
                    </div>
                    
                    <div class="form-group">
                        <i>Личный телефон:</i>
                        <span style="font-weight: bold;"><asp:Label runat="server" ID="lblPhone" style="width: 100%"/></span>
                    </div>
                    
                    <div class="form-group" id="homePhone">
                        <i>Домашний телефон:</i>
                        <span style="font-weight: bold;"><asp:Label runat="server" ID="lblPhoneHome" style="width: 100%"/></span>
                    </div>
                    
                    <div class="form-group" id="workPhone">
                        <i>Рабочий телефон #1:</i>
                        <span style="font-weight: bold;"><asp:Label runat="server" ID="lblPhoneWorkOne" style="width: 100%"/></span>
                    </div>
                    
                    <div class="form-group" id="workPhone2">
                        <i>Рабочий телефон #2:</i>
                        <span style="font-weight: bold;"><asp:Label runat="server" ID="lblPhoneWorkTwo" style="width: 100%"/></span>
                    </div>
                    
                    <div class="form-group" id="workPhone2">
                        <i>Рабочий skype:</i>
                        <span style="font-weight: bold;"><asp:Label runat="server" ID="lblSkype" style="width: 100%"/></span>
                    </div>
                    
                    <div class="form-group"  id="birthDay">
                        <i>Дата рождения:</i>
                        <span style="font-weight: bold;"><asp:Label ID="lblBirthDay" runat="server" style="width: 100%"/></span>
                    </div>
                    
                    <hr class="styleHR2"/>
                    
                    <i style="margin-left: -20px; margin-bottom: 15px; display: block">Паспортные данные:</i>
                    <div class="form-group" id="passport">
                        <i>Серия и номер паспорта:</i>
                        <span style="font-weight: bold;"><asp:Label runat="server" ID="lblPassport" style="width: 100%"/></span>
                    </div>
                    
                    <div class="form-group" id="personalNumber">
                        <i>Личный номер:</i>
                        <span style="font-weight: bold;"><asp:Label runat="server" ID="lblPersonalNumber" style="width: 100%"/></span>
                    </div>
                    
                    <div class="form-group" id="ROVD">
                        <i>Выдавший орган:</i>
                        <span style="font-weight: bold;"><asp:Label runat="server" ID="lblROVD" style="width: 100%"/></span>
                    </div>
                    
                    <div class="form-group" id="validity">
                        <i>Дата выдачи:</i>
                        <span style="font-weight: bold;"><asp:Label runat="server" ID="lblDateOfIssue" style="width: 100%"/></span>
                    </div>

                    <div class="form-group" id="validity">
                        <i>Действителен до:</i>
                        <span style="font-weight: bold;"><asp:Label runat="server" ID="lblValidity" style="width: 100%"/></span>
                    </div>
                    
                    <div class="form-group" id="registration">
                        <i>Адрес прописки:</i>
                        <span style="font-weight: bold;"><asp:Label runat="server" ID="lblRegistration" style="width: 100%"/></span>
                    </div>
                </td>
            </tr>
        </table>
        
        <div style="text-align: right; margin-top: 25px; margin-right: 27px;">
            <asp:Button ID="btnEdit" OnClick="btnEdit_click"  CssClass="btn btn-default btn-right"  runat="server" Text='Редактировать' style="margin-left: 35px;"/> 
            <asp:Button ID="btnBack" OnClientClick="window.history.go(-1);return false;"  CssClass="btn btn-default btn-right"  runat="server" Text='Назад' />        
        </div>
    </div>
    <script>
        $(function() {
            if ($("#address span").text().length == 0) {
                $("#address").hide();
            }

            if ($("#homePhone span").text().length == 0) {
                $("#homePhone").hide();
            }

            if ($("#workPhone span").text().length == 0) {
                $("#workPhone").hide();
            }

            if ($("#workPhone2 span").text().length == 0) {
                $("#workPhone2").hide();
            }

            if ($("#passport span").text().length == 0) {
                $("#passport").hide();
            }

            if ($("#personalNumber span").text().length == 0) {
                $("#personalNumber").hide();
            }

            if ($("#ROVD span").text().length == 0) {
                $("#ROVD").hide();
            }

            if ($("#validity span").text().length == 0) {
                $("#validity").hide();
            }

            if ($("#registration span").text().length == 0) {
                $("#registration").hide();
            }

            if ($("#birthDay span").text().length == 0) {
                $("#birthDay").hide();
            }
        });
    </script>
</asp:Content>
