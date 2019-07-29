<%@ Page Title="" Language="C#" MasterPageFile="~/ManagerMasterPage.master" AutoEventWireup="true" CodeBehind="ChangePasswords.aspx.cs" Inherits="Delivery.ManagerUI.Menu.Settings.ChangePasswords" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <script type="text/javascript">
        $(function () {
            if ($('#<%= lblError.ClientID%>').html() == "") {
                $('#<%= errorDiv.ClientID%>').hide();
            } else {
                $('#<%= errorDiv.ClientID%>').show();
            }
        });
    </script>
    <h3 class="h3custom" style="margin-top: 0;">Изменение паролей пользователей и работников</h3>
    <div class="single-input-fild" style="margin-bottom: 10px; margin-top: 10px;">
        <asp:Panel runat="server" style="width: 100%;" id="errorDiv">
	        <asp:Label ID="lblError" runat="server" style="color: #ffffff !important;"/>
	    </asp:Panel>
        
        
        <div class="form-group">
            <label for="<%= tbUID.ClientID %>">Введите UID</label>
            <asp:TextBox ID="tbUID" CssClass="form-control" runat="server" placeholder="UID пользователя или работника"/>
        </div>

        <div class="form-group">
            <label for="<%= tbNewPassword.ClientID %>">Введите пароль</label>
            <asp:TextBox ID="tbNewPassword" TextMode="Password" CssClass="form-control" runat="server" placeholder="Новый пароль"/>
        </div>

        <div style="margin-bottom: 40px; margin-top: 15px;">
            <asp:Button ID="btnChange" runat="server" Text='Изменить' ValidationGroup="LoginGroup" OnClick="btnChange_OnClick" CssClass="btn btn-default btn-right" style="margin-left: 30px;"/>
            <asp:Button ID="btnBack" OnClientClick="window.history.go(-1);return false;" runat="server" CssClass="btn btn-default btn-right" Text='Назад'/>
        </div>
        
    </div>
    <asp:CustomValidator ID="CustomValidator1" ControlToValidate="tbUID" ClientValidationFunction="validateNotEmptyNumber" EnableClientScript="True" ValidationGroup="LoginGroup" ValidateEmptyText="True" runat="server"  Display="None" ErrorMessage="Вы не ввели UID или ввели его неверно" ></asp:CustomValidator>
    <asp:CustomValidator ID="CustomValidator18" ControlToValidate="tbNewPassword" ClientValidationFunction="validateIfEmpty" EnableClientScript="True" ValidationGroup="LoginGroup" ValidateEmptyText="True" runat="server"  Display="None" ErrorMessage="Вы не ввели новый пароль" ></asp:CustomValidator>
    <asp:ValidationSummary ID="ValidationSummary2" runat="server" ValidationGroup="LoginGroup" ShowMessageBox="true" ShowSummary="false"/>

</asp:Content>
