<%@ Page validateRequest="false" Title="" Language="C#" MasterPageFile="~/ManagerMasterPage.master" Async="true" AutoEventWireup="true" CodeBehind="AnonymousMessage.aspx.cs" Inherits="Delivery.ManagerUI.AnonymousMessage" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div style="overflow: hidden">

        <h3 class="h3custom" style="margin-top: 0;">Страница отправки анонимного сообщения начальству</h3>
        <div class="single-input-fild" style="width: 400px; display: inline-block; float: left; margin-left: 50px;">
            <div class="form-group">
                <label for="<%= tbSubject.ClientID %>">Заголовок сообщения</label>
                <asp:TextBox id="tbSubject" runat="server" CssClass="form-control" placeholder="Введите тему жалобы" Width="97%"/>
            </div>
            <div class="form-group">
                <label for="<%= tbBody.ClientID %>">Содержание сообщения</label>
                <asp:TextBox id="tbBody" runat="server" CssClass="multi-control" TextMode="Multiline" Rows="7"/>
            </div>
            <div style="padding-top: 10px;">
                <asp:Button ID="btnSend" class="btn btn-default btn-right" runat="server" Text='Отправить' ValidationGroup="LoginGroup" style="margin-left: 30px;"/>
                <asp:Button ID="btnBack" OnClientClick="window.history.go(-1);return false;" class="btn btn-default btn-right" runat="server" Text='Назад'/>
            </div>
        </div>
        <div style="display: inline-block; width: 400px; float: left; margin-left: 60px; margin-top: 30px;">
            Анонимные сообщения предназначены для того, что бы наши сотрудники могли поделится мыслями с начальством в анонимном режиме. <br/><br/>
            Информация о том, кто писал сообщение <u>нигде не хранится</u>. Анонимность ограничивается лишь стилем вашего текста. <br/><br/>
            Вот список email-адресов, на которые приходят ваши сообщения: <i><b><asp:Label runat="server" ID="lblEmalList"></asp:Label></b></i>
        </div>
        <asp:CustomValidator ID="CustomValidator1" ControlToValidate="tbSubject" ClientValidationFunction="validateIfEmpty" EnableClientScript="True" ValidationGroup="LoginGroup" ValidateEmptyText="True" runat="server"  Display="None" ErrorMessage="Вы не ввели заголовок жалобы" ></asp:CustomValidator>
        <asp:CustomValidator ID="CustomValidator18" ControlToValidate="tbBody" ClientValidationFunction="validateIfEmpty" EnableClientScript="True" ValidationGroup="LoginGroup" ValidateEmptyText="True" runat="server"  Display="None" ErrorMessage="Вы не ввели содержание жалобы" ></asp:CustomValidator>
        <asp:ValidationSummary ID="ValidationSummary1" runat="server" ValidationGroup="LoginGroup" ShowMessageBox="true" ShowSummary="false"/>
    </div>
</asp:Content>
