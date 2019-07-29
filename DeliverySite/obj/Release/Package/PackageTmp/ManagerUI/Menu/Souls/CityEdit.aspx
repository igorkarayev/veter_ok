<%@ Page Title="" Language="C#" MasterPageFile="~/ManagerMasterPage.master" AutoEventWireup="true" CodeBehind="CityEdit.aspx.cs" Inherits="Delivery.ManagerUI.Menu.Souls.CityEdit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <!--script>
        $(function() {
            /*$("#<!--%= tbDistance.ClientID%>").maskMoney({ allowNegative: true });
            $("#<!--%= tbDistanceFromCity.ClientID%>").maskMoney({ allowNegative: true });*/
        });
    </script-->
    <h3 class="h3custom" style="margin-top: 0;"><%# ActionText %> населенного пункта</h3>
    <table class="table" style="margin-left: auto; margin-right: auto;">
            <tr>
                <td>
                    <span>SOATO</span>
                </td>
                <td>
                    <asp:TextBox ID="tbSOATO" runat="server" CssClass="form-control" width="30%"/>
                </td>
            </tr>
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
                    <span>Область</span>
                </td>
                <td>
                    <asp:DropDownList ID="ddlRegion" CssClass="ddl-control" runat="server" width="100%"/>
                </td>
            </tr>
            <tr>
                <td>
                    <span>Район</span>
                </td>
                <td>
                    <asp:DropDownList ID="ddlDistrict" CssClass="ddl-control" runat="server" width="100%"/>
                </td>
            </tr>
            <tr>
                <td>
                    <span>Расстояние перевозки (в км.)</span>
                </td>
                <td>
                    <asp:TextBox ID="tbDistance" runat="server" CssClass="form-control" width="10%"/>
                </td>
            </tr>
            <tr>
                <td style="vertical-align: top">
                    Расстояние перевозки от ближ. <br/> основного нас. пункта (в км.)
                </td>
                <td>
                    <asp:TextBox ID="tbDistanceFromCity" runat="server" CssClass="form-control" width="10%"/><br/>
                    <div style="font-style:italic; font-size: 12px;">
                        если 0 и не основной нас.пун. - калькулятор не считает стоимость
                    </div>
                    <div style="font-style:italic; font-size: 12px;">
                        если -1 - то нас.пун. является "нас.пунктом - попутчиком" и стоимость за откл. = 0
                    </div>
                </td>
            </tr>
            <tr>
                <td>
                    <span>Основной город</span>
                </td>
                <td>
                    <asp:CheckBox ID="cbIsMainCity" runat="server"/>
                </td>
            </tr>
            <tr>
                <td colspan="3" style="padding-top: 20px;">
                    <asp:Button ID="btnCreate" class="btn btn-default btn-right" runat="server" Text='<%# ButtonText %>' ValidationGroup="LoginGroup" style="margin-left: 30px;"/>
                    <asp:Button ID="btnBack" OnClientClick="window.history.go(-1);return false;" class="btn btn-default btn-right" runat="server" Text='Назад'/>
                </td>
            </tr>
    </table>

    <asp:CustomValidator ID="CustomValidator5" ControlToValidate="tbName" runat="server"  ClientValidationFunction="validateIfEmpty" EnableClientScript="True" ValidationGroup="LoginGroup" ValidateEmptyText="True" Display="None" ErrorMessage="Вы не ввели название населенного пункта"></asp:CustomValidator>
    <asp:CustomValidator ID="CustomValidator2" ControlToValidate="tbDistance" runat="server"  ClientValidationFunction="validateNotEmptyNumber" EnableClientScript="True" ValidationGroup="LoginGroup" ValidateEmptyText="True" Display="None" ErrorMessage="Вы не ввели дни доставки"></asp:CustomValidator>

    <asp:ValidationSummary ID="ValidationSummary1" runat="server" ValidationGroup="LoginGroup" ShowMessageBox="true" ShowSummary="false"/>
</asp:Content>
