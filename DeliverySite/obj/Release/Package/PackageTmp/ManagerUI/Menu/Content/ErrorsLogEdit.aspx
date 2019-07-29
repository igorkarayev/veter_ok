<%@ Page Title="" Language="C#" MasterPageFile="~/ManagerMasterPage.master" AutoEventWireup="true" CodeBehind="ErrorsLogEdit.aspx.cs" Inherits="Delivery.ManagerUI.Menu.Content.ErrorsLogEdit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <h3 class="h3custom" style="margin-top: 0;">Просмотр ошибки</h3>
     <table class="table">
            <tr>
                <td>
                    <span>Время:</span>
                </td>
                <td>
                    <asp:Label ID="lblDate" runat="server" width="100%"/>
                </td>
            </tr>
            <tr>
                <td>
                    <span>IP:</span>
                </td>
                <td>
                    <asp:Label ID="lblIP" runat="server" width="100%"/>
                </td>
            </tr>
            <tr>
                <td>
                    <span>Тип:</span>
                </td>
                <td>
                    <asp:Label ID="lblType" runat="server" width="100%"/>
                </td>
            </tr>
            <tr>
                <td style="vertical-align: top">
                    <span>Описание:</span>
                </td>
                <td>
                    <asp:TextBox ID="tbStackTrase" runat="server" CssClass="multi-control" Columns="100" Rows="20" TextMode="MultiLine"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td style="text-align: left; padding-top: 20px" colspan="2">
                    <asp:Button ID="btnBack" OnClientClick="window.history.go(-1);return false;" runat="server" class="btn btn-default btn-right" Text='Назад'/>
                </td>
            </tr>
    </table>
</asp:Content>
