<%@ Page validateRequest="false" Language="C#" MasterPageFile="~/ManagerMasterPage.master" AutoEventWireup="true" CodeBehind="PagesEdit.aspx.cs" Inherits="Delivery.ManagerUI.Menu.Content.PagesEdit"   %>
<%@ Register TagPrefix="CKEditor" Namespace="CKEditor.NET" Assembly="CKEditor.NET, Version=3.6.4.0, Culture=neutral, PublicKeyToken=e379cdf2f8354999" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
<script type="text/javascript">
        $(function () {
            CKEDITOR.replace('<%=tbContent.ClientID %>', { filebrowserImageUploadUrl: '../Upload.ashx' });
    });
</script>
    <h3 class="h3custom" style="margin-top: 0;">Редактирование страницы <%= PageName %></h3>
    <table class="table" style="width: 100%;">
        <tr>
            <td valign="top">
                <asp:Label runat="server" ID="lblName">Имя страницы:</asp:Label>
            </td>
            <td>
                <asp:TextBox ID="tbPageName" CssClass="form-control" runat="server" style="width: 97%;"/>
            </td>
        </tr>
        <tr>
            <td valign="top">
                <asp:Label runat="server" ID="lblTitle">Заголовок (Title):</asp:Label>
            </td>
            <td>
                <asp:TextBox ID="tbPageTitle" CssClass="form-control" runat="server" style="width: 97%;"/>
            </td>
        </tr>
        <tr>
            <td valign="top">
                <span>Содержание:</span>
            </td>
            <td>
                <CKEditor:CKEditorControl ID="tbContent" runat="server"></CKEditor:CKEditorControl>
            </td>
        </tr>
        <tr>
            <td colspan="2" style="padding-top: 20px; padding-right: 30px;">
                <asp:Button ID="btnCreate" class="btn btn-default btn-right" runat="server" Text='<%# ButtonText %>' ValidationGroup="LoginGroup" style="margin-left: 30px;"/>
                <asp:Button ID="btnBack" OnClientClick="window.history.go(-1);return false;" class="btn btn-default btn-right" runat="server" Text='Назад'/>
            </td>
        </tr>
    </table>
</asp:Content>
