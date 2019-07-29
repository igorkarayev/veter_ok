<%@ Page Title="" Language="C#" MasterPageFile="~/ManagerMasterPage.master" AutoEventWireup="true" CodeBehind="CarEdit.aspx.cs" Inherits="Delivery.ManagerUI.Menu.Souls.CarEdit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <script type="text/javascript">
        function visiblField() {
            if ($('#<%= ddlType.ClientID %>').val() == "2") {
                $("#<%= pnlFiz.ClientID%>").hide();
                $("#<%= pnlUr.ClientID%>").show();
            } else {
                $("#<%= pnlUr.ClientID%>").hide();
                $("#<%= pnlFiz.ClientID%>").show();
            }
        }

        $(function () {
            if ($('#<%= lblError.ClientID%>').html() == "") {
                $('#errorDiv').hide();
            } else {
                $('#errorDiv').show();
            }
            
            $.datepicker.setDefaults($.datepicker.regional['ru']);
            $("#<%= tbValidity.ClientID %>").datepicker({ nextText: "", prevText: "", changeMonth: true, changeYear: true }).mask("99-99-9999");
            $("#<%= tbBirthDay.ClientID %>").datepicker({ nextText: "", prevText: "", changeMonth: true, changeYear: true }).mask("99-99-9999");
            $("#<%= tbDateOfIssue.ClientID %>").datepicker({ nextText: "", prevText: "", changeMonth: true, changeYear: true }).mask("99-99-9999");
            
            //показываем нужные поля для конкретного типа профиля
            visiblField();

            //показываем нужные поля при изменении типа профиля
            $('#<%= ddlType.ClientID %>').change(function () {
                visiblField();
            });
        });
    </script>
    <h3 class="h3custom" style="margin-top: 0;"><%# ActionText %> автомобиля</h3>
    
    <div class="loginError" style="width: 90%; margin-left: auto; margin-right: auto; margin-top: 0px;" id="errorDiv">
	    <asp:Label ID="lblError" runat="server" />
	</div>

    <div class="single-input-fild" style="width: 400px">

        <div class="form-group" id="divStatus" runat="server">
            <label>Тип владельца</label>
            <asp:DropDownList runat="server" CssClass="multi-control" ID="ddlType"/>
        </div>

        <div class="form-group" id="div5" runat="server">
            <label>Модель</label>
            <asp:TextBox ID="tbModel" CssClass="form-control" runat="server" width="95%"/>
        </div>
        
        <div class="form-group" id="div6" runat="server">
            <label>Номер</label>
            <asp:TextBox ID="tbNumber" CssClass="form-control" runat="server" width="95%"/>
            <asp:HiddenField runat="server" ID="hfNumber"></asp:HiddenField>
        </div>
        
        <asp:Panel runat="server" ID="pnlFiz" >
            <div class="form-group" id="div1" runat="server">
                <label>Фамилия владельца</label>
                <asp:TextBox ID="tbFirstName" CssClass="form-control" runat="server" width="95%"/>
            </div>
            
            <div class="form-group" id="div3" runat="server">
                <label>Имя владельца</label>
                <asp:TextBox ID="tbLastName" CssClass="form-control" runat="server" width="95%"/>
            </div>
            
            <div class="form-group" id="div4" runat="server">
                <label>Отчество владельца</label>
                <asp:TextBox ID="tbThirdName" CssClass="form-control" runat="server" width="95%"/>
            </div>
            
            <div class="form-group" id="div7" runat="server">
                <label>Серия паспорта:</label>
                <asp:TextBox ID="tbPassportSeria" CssClass="form-control" runat="server" style="width: 17px;"/>
            </div>
            
            <div class="form-group" id="div8" runat="server">
                <label>Номер паспорта:</label>
                <asp:TextBox ID="tbPassportNumber" CssClass="form-control" runat="server" style="width: 60px;"/>
            </div>
            
            <div class="form-group" id="div9" runat="server">
                <label>Личный код</label>
                <asp:TextBox ID="tbPersonalNumber" CssClass="form-control" runat="server" width="95%"/>
            </div>
            
            <div class="form-group" id="div10" runat="server">
                <label>Орган, выдавший паспорт</label>
                <asp:TextBox ID="tbROVD" CssClass="form-control" runat="server" TextMode="MultiLine" style="height: 100px;"/>
            </div>
            
            <div class="form-group" id="div11" runat="server">
                <label>Дата выдачи:</label>
                <asp:TextBox ID="tbDateOfIssue" CssClass="form-control" runat="server" style="width: 75px;"/>
            </div>
            
            <div class="form-group" id="div12" runat="server">
                <label>Действует до:</label>
                <asp:TextBox ID="tbValidity" CssClass="form-control" runat="server" style="width: 75px;"/>
            </div>
            
            <div class="form-group" id="div13" runat="server">
                <label>Прописка</label>
                <asp:TextBox ID="tbRegistrationAddress" CssClass="form-control" runat="server" TextMode="MultiLine" style="height: 100px;"/>
            </div>
            
            <div class="form-group" id="div14" runat="server">
                <label>День рождения:</label>
                <asp:TextBox ID="tbBirthDay" CssClass="form-control" runat="server" style="width: 75px;"/>
            </div>
        </asp:Panel>
        
        <asp:Panel runat="server" ID="pnlUr" >
            <div class="form-group" id="div2" runat="server">
                <label>Наименование компании</label>
                <asp:TextBox ID="tbCompanyName" CssClass="form-control" runat="server" width="95%"/>
            </div>
        </asp:Panel>

        <div class="form-group" style="margin-top: 10px;">
            <asp:Button ID="btnCreate" class="btn btn-default btn-right" runat="server" Text='<%# ButtonText %>' ValidationGroup="LoginGroup" style="margin-left: 30px;"/>
            <asp:Button ID="btnBack" OnClientClick="window.history.go(-1);return false;" class="btn btn-default btn-right" runat="server" Text='Назад'/>
        </div>
    </div>
    <asp:CustomValidator ID="CustomValidator1" ControlToValidate="tbModel" ClientValidationFunction="validateIfEmpty" EnableClientScript="True" ValidationGroup="LoginGroup" ValidateEmptyText="True" runat="server"  Display="None" ErrorMessage="Вы не ввели модель" ></asp:CustomValidator>
    <asp:CustomValidator ID="CustomValidator2" ControlToValidate="tbNumber" ClientValidationFunction="validateIfEmpty" EnableClientScript="True" ValidationGroup="LoginGroup" ValidateEmptyText="True" runat="server"  Display="None" ErrorMessage="Вы не ввели номер" ></asp:CustomValidator>
    <asp:ValidationSummary ID="ValidationSummary2" runat="server" ValidationGroup="LoginGroup" ShowMessageBox="true" ShowSummary="false"/>
</asp:Content>
