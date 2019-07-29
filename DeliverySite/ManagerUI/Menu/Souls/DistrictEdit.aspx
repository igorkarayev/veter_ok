<%@ Page Title="" Language="C#" MasterPageFile="~/ManagerMasterPage.master" AutoEventWireup="true" CodeBehind="DistrictEdit.aspx.cs" Inherits="Delivery.ManagerUI.Menu.Souls.DistrictEdit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <h3 class="h3custom" style="margin-top: 0;">Редактирование района</h3>
    <table class="table" style="margin-left: auto; margin-right: auto;">
            <tr>
                <td>
                    <span>Название</span>
                </td>
                <td>
                    <asp:TextBox ID="tbName" runat="server" CssClass="form-control" width="100%"/>
                </td>
            </tr>
            <tr>
                <td>
                    <span>Направление</span>
                </td>
                <td>
                    <asp:DropDownList ID="ddlTrack" CssClass="ddl-control" runat="server" width="100%"/>
                </td>
            </tr>
            <tr>
                <td>
                    <span>Доставка в пн.</span>
                </td>
                <td>
                    <asp:CheckBox ID="cbMonday" runat="server" width="10%"/>
                </td>
            </tr>
            <tr>
                <td>
                    <span>Доставка во вт.</span>
                </td>
                <td>
                    <asp:CheckBox ID="cbTuesday" runat="server" width="10%"/>
                </td>
            </tr>
            <tr>
                <td>
                    <span>Доставка в ср.</span>
                </td>
                <td>
                    <asp:CheckBox ID="cbWednesday" runat="server" width="10%"/>
                </td>
            </tr>
            <tr>
                <td>
                    <span>Доставка в чт.</span>
                </td>
                <td>
                    <asp:CheckBox ID="cbThursday" runat="server" width="10%"/>
                </td>
            </tr>
            <tr>
                <td>
                    <span>Доставка в пт.</span>
                </td>
                <td>
                    <asp:CheckBox ID="cbFriday" runat="server" width="10%"/>
                </td>
            </tr>
            <tr>
                <td>
                    <span>Доставка в сб.</span>
                </td>
                <td>
                    <asp:CheckBox ID="cbSaturday" runat="server" width="10%"/>
                </td>
            </tr>
            <tr>
                <td>
                    <span>Доставка в вс.</span>
                </td>
                <td>
                    <asp:CheckBox ID="cbSunday" runat="server" width="10%"/>
                </td>
            </tr>
            <tr>
                <td>
                    <span>Срок доставки</span>
                </td>
                <td>
                    <asp:TextBox ID="tbDeliveryTerms" runat="server" CssClass="form-control" width="10%"/><span style="font-style:italic; font-size: 12px;">дней</span>
                </td>
            </tr>
            <tr>
                <td colspan="3" style="padding-top: 20px;">
                    <asp:Button ID="btnCreate" class="btn btn-default btn-right" runat="server" Text='Сохранить' ValidationGroup="LoginGroup" style="margin-left: 30px;"/>
                    <asp:Button ID="btnBack" OnClientClick="window.history.go(-1);return false;" class="btn btn-default btn-right" runat="server" Text='Назад'/>
                </td>
            </tr>
    </table>

    <asp:CustomValidator ID="CustomValidator2" ControlToValidate="tbDeliveryTerms" runat="server"  ClientValidationFunction="validateNotEmptyNumber" EnableClientScript="True" ValidationGroup="LoginGroup" ValidateEmptyText="True" Display="None" ErrorMessage="Вы не ввели срок доставки, либо ввели его не верно"></asp:CustomValidator>
    <asp:CustomValidator ID="CustomValidator1" ControlToValidate="tbName" runat="server"  ClientValidationFunction="validateIfEmpty" EnableClientScript="True" ValidationGroup="LoginGroup" ValidateEmptyText="True" Display="None" ErrorMessage="Вы не ввели имя района"></asp:CustomValidator>

    <asp:ValidationSummary ID="ValidationSummary1" runat="server" ValidationGroup="LoginGroup" ShowMessageBox="true" ShowSummary="false"/>
</asp:Content>
