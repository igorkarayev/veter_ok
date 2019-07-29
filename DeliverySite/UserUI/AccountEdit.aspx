<%@ Page Title="" Language="C#" MasterPageFile="~/UserMasterPage.master" AutoEventWireup="true" CodeBehind="AccountEdit.aspx.cs" Inherits="Delivery.UserUI.AccountEdit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <script type="text/javascript">
        $(function () {
            $("#<%= tbContactPhoneNumbers.ClientID %>").mask("+375 (99) 999-99-99");
        });
        $(function () {
            if ($('#<%= lblError.ClientID%>').html() == "") {
                $('#errorDiv').hide();
            } else {
                $('#errorDiv').show();
            }
        });
    </script>

    <div class="single-input-fild">
	    <h3  class="blueTitle">Редактирование профиля</h3>
        <div class="form-group">
            <table style="width: 109%">
                <tr>
                    <td>
                        <asp:Image runat="server" ID="imgGravatar"/>
                    </td>
                    <td style="vertical-align: top; text-align: right">
                        <div style="padding-bottom: 145px">Ваш UID: <b><asp:Label runat="server" ID="lblUID"></asp:Label></b></div>
                        <asp:HyperLink runat="server" ID="hlGravatarEdit" Text="изменить аватар" Target="blank"></asp:HyperLink>
                    </td>
                </tr>
            </table>
        </div>
        <div class="form-group">
            <label for="<%= tbLogin.ClientID %>">Ваш логин</label>
            <asp:TextBox ID="tbLogin" CssClass="form-control"  runat="server" />
        </div>
        <div class="form-group">
            <label for="<%= tbEmail.ClientID %>">Ваш e-mail</label>
            <asp:TextBox ID="tbEmail" CssClass="form-control"  runat="server" />
        </div>
        <div class="form-group">
            <label for="<%= tbContactPhoneNumbers.ClientID %>">Ваш телефон</label>
            <asp:TextBox ID="tbContactPhoneNumbers" CssClass="form-control"  runat="server" />
        </div>

        <div>
            <asp:Button ID="btnEdit" style="margin-left: 30px" Text="Изменить" runat="server" CssClass="btn btn-default btn-right" ValidationGroup="LoginGroup"/>
            <asp:Button ID="btnBack" OnClientClick="window.history.go(-1);return false;" CssClass="btn btn-default btn-right" runat="server" Text='Назад'/>
        </div>
    </div>
    <div class="loginError" style="width: 80%; margin-left: auto; margin-right: auto; margin-top: -40px;" id="errorDiv">
	    <asp:Label ID="lblError" runat="server" />
	</div>
    <asp:CustomValidator ID="CustomValidator1" ControlToValidate="tbContactPhoneNumbers"    ClientValidationFunction="validateIfEmpty"          EnableClientScript="True" ValidationGroup="LoginGroup" ValidateEmptyText="True" runat="server"  Display="None" ErrorMessage="Вы не ввели номер телефона" ></asp:CustomValidator>
    <asp:CustomValidator ID="CustomValidator2" ControlToValidate="tbLogin"                  ClientValidationFunction="validateIfEmpty"          EnableClientScript="True" ValidationGroup="LoginGroup" ValidateEmptyText="True" runat="server"  Display="None" ErrorMessage="Вы не ввели логин" ></asp:CustomValidator>
    <asp:CustomValidator ID="CustomValidator4" ControlToValidate="tbEmail" ClientValidationFunction="validateIfEmptyAndEmail" EnableClientScript="True" ValidationGroup="LoginGroup" ValidateEmptyText="True" runat="server"  Display="None" ErrorMessage="Вы не ввели емейл либо ввели его неверно" ></asp:CustomValidator>
    
    <asp:ValidationSummary ID="ValidationSummary1" runat="server" ValidationGroup="LoginGroup" ShowMessageBox="true" ShowSummary="false"/>
</asp:Content>
