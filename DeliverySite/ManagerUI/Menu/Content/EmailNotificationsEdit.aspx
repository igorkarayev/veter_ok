﻿<%@ Page validateRequest="false" Title="" Language="C#" MasterPageFile="~/ManagerMasterPage.master" AutoEventWireup="true" CodeBehind="EmailNotificationsEdit.aspx.cs" Inherits="Delivery.ManagerUI.Menu.Content.EmailNotificationsEdit"   %>
<%@ Register TagPrefix="CKEditor" Namespace="CKEditor.NET" Assembly="CKEditor.NET, Version=3.6.4.0, Culture=neutral, PublicKeyToken=e379cdf2f8354999" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
<script type="text/javascript">
            $(function () {
                CKEDITOR.replace('<%=tbBody.ClientID %>', { filebrowserImageUploadUrl: '../Upload.ashx' });
        });
</script>
    <h3 class="h3custom" style="margin-top: 0;">Редактирование электронного сообщения</h3>
    <table class="table" style="width: 97%;">
        <tr>
            <td valign="top">
                <asp:Label runat="server" ID="Label1">Описание:</asp:Label>
            </td>
            <td>
                <i><asp:Label runat="server" ID="lblDescriptionMore"></asp:Label></i>
            </td>
        </tr>
        <tr>
            <td valign="top">
                <asp:Label runat="server" ID="lblTitle">Заголовок:</asp:Label>
            </td>
            <td>
                <asp:TextBox ID="tbTitle"  CssClass="form-control" runat="server" width="100%"/>
            </td>
        </tr>
        <tr>
            <td valign="top">
                <span>Содержание:</span>
            </td>
            <td>
                <CKEditor:CKEditorControl ID="tbBody" runat="server"></CKEditor:CKEditorControl>
            </td>
        </tr>
        <tr>
            <td colspan="2" style="padding-top: 20px;">
                <asp:Button ID="btnCreate" runat="server" class="btn btn-default btn-right" Text='<%# ButtonText %>' style="margin-left: 30px;"/>
                <asp:Button ID="btnBack" OnClientClick="window.history.go(-1);return false;" class="btn btn-default btn-right" runat="server" Text='Назад'/>
            </td>
        </tr>
    </table>

</asp:Content>
