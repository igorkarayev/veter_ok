<%@ Page Title="" Language="C#" MasterPageFile="~/UserMasterPage.master" AutoEventWireup="true" CodeBehind="ProfileEdit.aspx.cs" Inherits="Delivery.UserUI.ProfileEdit" Async="true"%>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <script type="text/javascript">
        function visiblField() {
            //if UserProfile = fiz
            if ($('#<%= ddlProfileType.ClientID %>').val() == "1") {  
                $("#<%= pnlFiz.ClientID%>").show();
                $("#<%= pnlUr.ClientID%>").hide();
                if ($("#<%= tbFirstName2.ClientID %>").val() == "") {
                    $("#<%= tbFirstName2.ClientID %>").val("1");
                }
                if ($("#<%= tbLastName2.ClientID %>").val() == "") {
                    $("#<%= tbLastName2.ClientID %>").val("1");
                }
                if ($("#<%= tbThirdName2.ClientID %>").val() == "") {
                    $("#<%= tbThirdName2.ClientID %>").val("1");
                }
                if ($("#<%= tbDirectorPhoneNumber.ClientID %>").val() == "") {
                    $("#<%= tbDirectorPhoneNumber.ClientID %>").val("1");
                }
                if ($("#<%= tbCompanuName.ClientID %>").val() == "") {
                    $("#<%= tbCompanuName.ClientID %>").val("1");
                }
                if ($("#<%= tbCompanyAddress.ClientID %>").val() == "") {
                    $("#<%= tbCompanyAddress.ClientID %>").val("1");
                }
                if ($("#<%= tbPostAddress.ClientID %>").val() == "") {
                    $("#<%= tbPostAddress.ClientID %>").val("1");
                }
                if ($("#<%= tbRS.ClientID %>").val() == "") {
                    $("#<%= tbRS.ClientID %>").val("1");
                }
                if ($("#<%= tbUNP.ClientID %>").val() == "") {
                    $("#<%= tbUNP.ClientID %>").val("1");
                }
                if ($("#<%= tbBankName.ClientID %>").val() == "") {
                    $("#<%= tbBankName.ClientID %>").val("1");
                }
                if ($("#<%= tbBankCode.ClientID %>").val() == "") {
                    $("#<%= tbBankCode.ClientID %>").val("1");
                }
                if ($("#<%= tbBankAddress.ClientID %>").val() == "") {
                    $("#<%= tbBankAddress.ClientID %>").val("1");
                }
                if ($("#<%= tbContactPersonFIO.ClientID %>").val() == "") {
                    $("#<%= tbContactPersonFIO.ClientID %>").val("1");
                }
                if ($("#<%= tbContactPhoneNumbers.ClientID %>").val() == "") {
                    $("#<%= tbContactPhoneNumbers.ClientID %>").val("1");
                }
                
                if ($("#<%= tbFirstName.ClientID %>").val() == "1") {
                    $("#<%= tbFirstName.ClientID %>").val("");
                }
                if ($("#<%= tbLastName.ClientID %>").val() == "1") {
                    $("#<%= tbLastName.ClientID %>").val("");
                }
                if ($("#<%= tbThirdName.ClientID %>").val() == "1") {
                    $("#<%= tbThirdName.ClientID %>").val("");
                }
                if ($("#<%= tbPassportSeria.ClientID %>").val() == "1") {
                    $("#<%= tbPassportSeria.ClientID %>").val("");
                }
                if ($("#<%= tbPassportNumber.ClientID %>").val() == "1") {
                    $("#<%= tbPassportNumber.ClientID %>").val("");
                }
                if ($("#<%= tbPassportData.ClientID %>").val() == "1") {
                    $("#<%= tbPassportData.ClientID %>").val("");
                }
                if ($("#<%= tbPassportDate.ClientID %>").val() == "02.04.1992") {
                    $("#<%= tbPassportDate.ClientID %>").val("");
                }
                if ($("#<%= tbAddress.ClientID %>").val() == "1") {
                    $("#<%= tbAddress.ClientID %>").val("");
                }
                if ($("#<%= tbContactPhoneNumbersFiz.ClientID %>").val() == "1") {
                    $("#<%= tbContactPhoneNumbersFiz.ClientID %>").val("");
                }
            } else {
                $("#<%= pnlUr.ClientID%>").show();
                $("#<%= pnlFiz.ClientID%>").hide();
                if ($("#<%= tbFirstName.ClientID %>").val() == "") {
                    $("#<%= tbFirstName.ClientID %>").val("1");
                }
                if ($("#<%= tbLastName.ClientID %>").val() == "") {
                    $("#<%= tbLastName.ClientID %>").val("1");
                }
                if ($("#<%= tbThirdName.ClientID %>").val() == "") {
                    $("#<%= tbThirdName.ClientID %>").val("1");
                }
                if ($("#<%= tbPassportSeria.ClientID %>").val() == "") {
                    $("#<%= tbPassportSeria.ClientID %>").val("1");
                }
                if ($("#<%= tbPassportNumber.ClientID %>").val() == "") {
                    $("#<%= tbPassportNumber.ClientID %>").val("1");
                }
                if ($("#<%= tbPassportData.ClientID %>").val() == "") {
                    $("#<%= tbPassportData.ClientID %>").val("1");
                }
                if ($("#<%= tbPassportDate.ClientID %>").val() == "") {
                    $("#<%= tbPassportDate.ClientID %>").val("02.04.1992");
                }
                if ($("#<%= tbAddress.ClientID %>").val() == "") {
                    $("#<%= tbAddress.ClientID %>").val("1");
                }
                if ($("#<%= tbContactPhoneNumbersFiz.ClientID %>").val() == "") {
                    $("#<%= tbContactPhoneNumbersFiz.ClientID %>").val("1");
                }
                
                if ($("#<%= tbFirstName2.ClientID %>").val() == "1") {
                    $("#<%= tbFirstName2.ClientID %>").val("");
                }
                if ($("#<%= tbLastName2.ClientID %>").val() == "1") {
                    $("#<%= tbLastName2.ClientID %>").val("");
                }
                if ($("#<%= tbThirdName2.ClientID %>").val() == "1") {
                    $("#<%= tbThirdName2.ClientID %>").val("");
                }
                if ($("#<%= tbDirectorPhoneNumber.ClientID %>").val() == "1") {
                    $("#<%= tbDirectorPhoneNumber.ClientID %>").val("");
                }
                if ($("#<%= tbCompanuName.ClientID %>").val() == "1") {
                    $("#<%= tbCompanuName.ClientID %>").val("");
                }
                if ($("#<%= tbCompanyAddress.ClientID %>").val() == "1") {
                    $("#<%= tbCompanyAddress.ClientID %>").val("");
                }
                if ($("#<%= tbPostAddress.ClientID %>").val() == "1") {
                    $("#<%= tbPostAddress.ClientID %>").val("");
                }
                if ($("#<%= tbRS.ClientID %>").val() == "1") {
                    $("#<%= tbRS.ClientID %>").val("");
                }
                if ($("#<%= tbUNP.ClientID %>").val() == "1") {
                    $("#<%= tbUNP.ClientID %>").val("");
                }
                if ($("#<%= tbBankName.ClientID %>").val() == "1") {
                    $("#<%= tbBankName.ClientID %>").val("");
                }
                if ($("#<%= tbBankCode.ClientID %>").val() == "1") {
                    $("#<%= tbBankCode.ClientID %>").val("");
                }
                if ($("#<%= tbBankAddress.ClientID %>").val() == "1") {
                    $("#<%= tbBankAddress.ClientID %>").val("");
                }
                if ($("#<%= tbContactPersonFIO.ClientID %>").val() == "1") {
                    $("#<%= tbContactPersonFIO.ClientID %>").val("");
                }
                if ($("#<%= tbContactPhoneNumbers.ClientID %>").val() == "1") {
                    $("#<%= tbContactPhoneNumbers.ClientID %>").val("");
                }
            }
        }
        $(function () {
            $("#<%= tbDirectorPhoneNumber.ClientID %>").mask("+375 (99) 999-99-99");
            $("#<%= tbContactPhoneNumbers.ClientID %>").mask("+375 (99) 999-99-99");
            $("#<%= tbContactPhoneNumbers2.ClientID %>").mask("+375 (99) 999-99-99");
            $("#<%= tbContactPhoneNumbersFiz.ClientID %>").mask("+375 (99) 999-99-99");
            $("#<%= tbContactPhoneNumbers2Fiz.ClientID %>").mask("+375 (99) 999-99-99");
            $("#<%= tbPassportDate.ClientID %>")
                .datepicker({ nextText: "", prevText: "", changeMonth: true, changeYear: true })
                .mask("99-99-9999");
            
            if ($("#<%= tbContactPhoneNumbers.ClientID %>").val() == $("#<%= tbDirectorPhoneNumber.ClientID %>").val() && $("#<%= tbContactPhoneNumbers.ClientID %>").val() != "") {
                $("#<%= cbContactPhoneNumber.ClientID %>").prop('checked', true);
            }
            
            $("#<%= cbContactPhoneNumber.ClientID %>").click(function () {
                if (this.checked) {
                    $("#<%= tbContactPhoneNumbers.ClientID %>").val($("#<%= tbDirectorPhoneNumber.ClientID %>").val());
                } else {
                    $("#<%= tbContactPhoneNumbers.ClientID %>").val("");
                }
            });
            
            //показываем нужные поля для конкретного типа профиля
            visiblField();

            //показываем нужные поля при изменении типа профиля
            $('#<%= ddlProfileType.ClientID %>').change(function () {
                visiblField();
            });
        });
        
        function validatePasportSeriaProfileEdit(sender, args) {
            var isValid;
            var myValue = args.Value;
            if ($('#<%= ddlProfileType.ClientID %>').val() == "1") {
                if (myValue == "" || myValue.length != 2 || myValue.match(/\d+/g)) {
                    isValid = false;
                    validateChangeColor(sender, isValid);
                } else {
                    isValid = true;
                    validateChangeColor(sender, isValid);
                }
            } else {
                isValid = true;
            }
            args.IsValid = isValid;
        }

        function validatePasportNumberProfileEdit(sender, args) {
            var isValid;
            var myValue = args.Value;
            if ($('#<%= ddlProfileType.ClientID %>').val() == "1") {
                if (myValue != "" && myValue.length == 7 && myValue.match(/^\d+$/)) {
                    isValid = true;
                    validateChangeColor(sender, isValid);
                } else {
                    validateChangeColor(sender, isValid);
                    isValid = false;
                }
            } else {
                isValid = true;
            }
            args.IsValid = isValid;
        }
    </script>
    <h3 class="h3custom" style="margin-top: 0;"><%= ActionText %> профиля</h3>
    <table style="width: 650px; margin-left: auto; margin-right: auto;">
        <tr>
            <td class="valignTable" style="width: 190px;">
                <span>Тип профиля:</span>
            </td>
            <td class="paddingTable">
                <asp:DropDownList CssClass="ddl-control" runat="server" ID="ddlProfileType" AutoPostBack="False"  width="200px"/>
            </td>
        </tr>
    </table>
    
    <asp:Panel runat="server" ID="pnlStatus" Visible="false">
        <table style="width: 650px; margin-left: auto; margin-right: auto;">
            <tr>
                    <td class="valignTable" style="width: 190px; vertical-align: top">
                        <span>Статус:</span>
                    </td>
                    <td class="paddingTable" style="vertical-align: top;">
                        <asp:Label ID="lblStatus" runat="server" width="91%"/>
                    </td>
                </tr>
        </table>
    </asp:Panel>

    <asp:Panel runat="server" ID="pnlFiz" >
        <table style="width: 650px; margin-left: auto; margin-right: auto;">
            <tr>
                <td class="valignTable" style="width: 190px;">
                    <span>Фамилия:</span>
                </td>
                <td class="paddingTable">
                    <asp:TextBox CssClass="form-control" ID="tbFirstName" runat="server" width="91%"/>
                </td>
            </tr>
            <tr>
                <td class="valignTable">
                    <span>Имя:</span>
                </td>
                <td class="paddingTable">
                    <asp:TextBox CssClass="form-control" ID="tbLastName" runat="server" width="91%"/>
                </td>
            </tr>
            <tr>
                <td class="valignTable">
                    <span>Отчество:</span>
                </td>
                <td class="paddingTable">
                    <asp:TextBox CssClass="form-control" ID="tbThirdName" runat="server" width="91%"/>
                </td>
            </tr>
            <tr>
                <td class="valignTable">
                    <span>Паспортные данные:</span>
                </td>
                <td class="paddingTable">
                    серия <asp:TextBox CssClass="form-control" ID="tbPassportSeria" runat="server" width="20px"/> 
                    номер <asp:TextBox CssClass="form-control" ID="tbPassportNumber" runat="server" width="60px"/> 
                </td>
            </tr>
            <tr>
                <td class="valignTable">
                    <span>Кем выдан:</span>
                </td>
                <td class="paddingTable">
                    <asp:TextBox CssClass="multi-control" ID="tbPassportData" runat="server" width="95%" TextMode="MultiLine" Rows="2" Columns="40"/>
                </td>
            </tr>
            <tr>
                <td class="valignTable">
                    <span>Когда выдан:</span>
                </td>
                <td class="paddingTable">
                    <asp:TextBox CssClass="form-control" ID="tbPassportDate" runat="server" Width="75px"/>
                </td>
            </tr>
            <tr>
                <td class="valignTable">
                    <span>Адрес проживания:</span>
                </td>
                <td class="paddingTable">
                    <asp:TextBox CssClass="multi-control" ID="tbAddress" runat="server" width="95%" TextMode="MultiLine" Rows="2" Columns="40"/>
                </td>
            </tr>
            <tr>
                <td class="valignTable">
                    <span>Контактные телефоны:</span>
                </td>
                <td class="paddingTable">
                    #1: <asp:TextBox CssClass="form-control" ID="tbContactPhoneNumbersFiz" runat="server" Width="130px"/>
                    <br/><br/>
                    #2: <asp:TextBox CssClass="form-control" ID="tbContactPhoneNumbers2Fiz" runat="server" Width="130px"/>
                </td>
            </tr>
        </table>
    </asp:Panel>

    <asp:Panel runat="server" ID="pnlUr">
        <table style="width: 650px; margin-left: auto; margin-right: auto;">
            <tr>
                <td class="valignTable" style="width: 190px;">
                    <span>Фамилия директора:</span>
                </td>
                <td class="paddingTable">
                    <asp:TextBox CssClass="form-control" ID="tbFirstName2" runat="server" width="91%"/>
                </td>
            </tr>
            <tr>
                <td class="valignTable">
                    <span>Имя директора:</span>
                </td>
                <td class="paddingTable">
                    <asp:TextBox CssClass="form-control" ID="tbLastName2" runat="server" width="91%"/>
                </td>
            </tr>
            <tr>
                <td class="valignTable">
                    <span>Отчество директора:</span>
                </td>
                <td class="paddingTable">
                    <asp:TextBox CssClass="form-control" ID="tbThirdName2" runat="server" width="91%"/>
                </td>
            </tr>
            <tr>
                <td class="valignTable">
                    <span>Телефон директора:</span>
                </td>
                <td class="paddingTable">
                    <asp:TextBox CssClass="form-control" ID="tbDirectorPhoneNumber" runat="server" Width="130px"/>
                </td>
            </tr>
            <tr>
                <td class="valignTable">
                    <span>Название компании:</span>
                </td>
                <td class="paddingTable">
                    <asp:TextBox CssClass="form-control" ID="tbCompanuName" runat="server" width="91%"/>
                </td>
            </tr>
            <tr>
                <td class="valignTable">
                    <span>Юридический адрес:</span>
                </td>
                <td class="paddingTable">
                    <asp:TextBox CssClass="multi-control" ID="tbCompanyAddress" runat="server" width="95%" TextMode="MultiLine" Rows="2" Columns="40"/>
                </td>
            </tr>
            <tr>
                <td class="valignTable">
                    <span>Контактный адрес:</span>
                </td>
                <td class="paddingTable">
                    <asp:TextBox CssClass="multi-control" ID="tbPostAddress" runat="server" width="95%" TextMode="MultiLine" Rows="2" Columns="40"/>
                </td>
            </tr>
            <tr>
                <td class="valignTable">
                    <span>Расчетный счет:</span>
                </td>
                <td class="paddingTable">
                    <asp:TextBox CssClass="form-control" ID="tbRS" runat="server" width="50%"/>
                </td>
            </tr>
            <tr>
                <td class="valignTable">
                    <span>УНП:</span>
                </td>
                <td class="paddingTable">
                    <asp:TextBox CssClass="form-control" ID="tbUNP" runat="server" width="50%"/>
                </td>
            </tr>
            <tr>
                <td class="valignTable">
                    <span>Название банка:</span>
                </td>
                <td class="paddingTable">
                    <asp:TextBox CssClass="form-control" ID="tbBankName" runat="server" width="91%"/>
                </td>
            </tr>
            <tr>
                <td class="valignTable">
                    <span>Адрес банка:</span>
                </td>
                <td class="paddingTable">
                    <asp:TextBox CssClass="multi-control" ID="tbBankAddress" runat="server" width="95%" TextMode="MultiLine" Rows="2" Columns="40"/>
                </td>
            </tr>
            <tr>
                <td class="valignTable">
                    <span>Код банка:</span>
                </td>
                <td class="paddingTable">
                    <asp:TextBox CssClass="form-control" ID="tbBankCode" runat="server" width="25%"/>
                </td>
            </tr>
            <tr>
                <td class="valignTable">
                    <span>ФИО контактного лица:</span>
                </td>
                <td class="paddingTable">
                    <asp:TextBox CssClass="form-control" ID="tbContactPersonFIO" runat="server" width="91%"/>
                </td>
            </tr>
            <tr>
                <td class="valignTable">
                    <span>Контактные телефоны:</span>
                </td>
                <td class="paddingTable">
                    #1: <asp:TextBox CssClass="form-control" ID="tbContactPhoneNumbers" runat="server" Width="130px"/>&nbsp;&nbsp;&nbsp;Совпадает с тел. директора <asp:CheckBox ID="cbContactPhoneNumber" runat="server"/>
                    <br/><br/>
                    #2: <asp:TextBox CssClass="form-control" ID="tbContactPhoneNumbers2" runat="server" Width="130px"/>
                </td>
            </tr>
        </table>
    </asp:Panel>

    <asp:Panel runat="server" ID="pnlReject" Visible="false">
        <table style="width: 650px; margin-left: auto; margin-right: auto;">
            <tr>
                    <td class="valignTable" style="width: 190px; vertical-align: top">
                        <span>Причина отклонения или блокировки:</span>
                    </td>
                    <td class="paddingTable" style="vertical-align: top; color: red;">
                        <asp:Label ID="lblRejectBlockedMessage" runat="server" width="91%"/>
                    </td>
                </tr>
        </table>
    </asp:Panel>

    <table style="width: 650px; margin-left: auto; margin-right: auto;">
        <tr>
            <td colspan="2">
                <asp:Button runat="server" ID="btnEdit" CssClass="btn btn-default btn-right" style="margin-left: 35px;" ValidationGroup="LoginGroup"/>
                <asp:Button ID="btnBack" OnClientClick="window.history.go(-1);return false;" runat="server"  CssClass="btn btn-default btn-right" Text='Назад'/>
            </td>
        </tr>
    </table>
    
    <asp:CustomValidator ID="CustomValidator11" ControlToValidate="tbLastName" ClientValidationFunction="validateIfEmpty" EnableClientScript="True" ValidationGroup="LoginGroup" ValidateEmptyText="True" runat="server"  Display="None" ErrorMessage="Вы не ввели фамилию" ></asp:CustomValidator>
    <asp:CustomValidator ID="CustomValidator3" ControlToValidate="tbFirstName" ClientValidationFunction="validateIfEmpty" EnableClientScript="True" ValidationGroup="LoginGroup" ValidateEmptyText="True" runat="server"  Display="None" ErrorMessage="Вы не ввели имя" ></asp:CustomValidator>
    <asp:CustomValidator ID="CustomValidator4" ControlToValidate="tbThirdName" ClientValidationFunction="validateIfEmpty" EnableClientScript="True" ValidationGroup="LoginGroup" ValidateEmptyText="True" runat="server"  Display="None" ErrorMessage="Вы не ввели отчество" ></asp:CustomValidator>
    <asp:CustomValidator ID="CustomValidator2" ControlToValidate="tbPassportSeria" ClientValidationFunction="validatePasportSeriaProfileEdit" EnableClientScript="True" ValidationGroup="LoginGroup" ValidateEmptyText="True" runat="server"  Display="None" ErrorMessage="Серия паспорта должна содержать 2 буквы" ></asp:CustomValidator>
    <asp:CustomValidator ID="CustomValidator1" ControlToValidate="tbPassportNumber" ClientValidationFunction="validatePasportNumberProfileEdit" EnableClientScript="True" ValidationGroup="LoginGroup" ValidateEmptyText="True" runat="server"  Display="None" ErrorMessage="Номер паспорта должн содержать 7 цифр" ></asp:CustomValidator>
    <asp:CustomValidator ID="CustomValidator7" ControlToValidate="tbPassportData" ClientValidationFunction="validateIfEmpty" EnableClientScript="True" ValidationGroup="LoginGroup" ValidateEmptyText="True" runat="server"  Display="None" ErrorMessage="Вы не ввели орган, выдавший паспорт" ></asp:CustomValidator>
    <asp:CustomValidator ID="CustomValidator5" ControlToValidate="tbPassportDate" ClientValidationFunction="validateIfEmpty" EnableClientScript="True" ValidationGroup="LoginGroup" ValidateEmptyText="True" runat="server"  Display="None" ErrorMessage="Вы не ввели дату выдачи паспорта" ></asp:CustomValidator>
    <asp:CustomValidator ID="CustomValidator6" ControlToValidate="tbContactPhoneNumbersFiz" ClientValidationFunction="validateIfEmpty" EnableClientScript="True" ValidationGroup="LoginGroup" ValidateEmptyText="True" runat="server"  Display="None" ErrorMessage="Вы не ввели контактный телефон" ></asp:CustomValidator>
    
    <asp:CustomValidator ID="CustomValidator8" ControlToValidate="tbLastName2" ClientValidationFunction="validateIfEmpty" EnableClientScript="True" ValidationGroup="LoginGroup" ValidateEmptyText="True" runat="server"  Display="None" ErrorMessage="Вы не ввели фамилию директора" ></asp:CustomValidator>
    <asp:CustomValidator ID="CustomValidator9" ControlToValidate="tbFirstName2" ClientValidationFunction="validateIfEmpty" EnableClientScript="True" ValidationGroup="LoginGroup" ValidateEmptyText="True" runat="server"  Display="None" ErrorMessage="Вы не ввели имя директора" ></asp:CustomValidator>
    <asp:CustomValidator ID="CustomValidator10" ControlToValidate="tbThirdName2" ClientValidationFunction="validateIfEmpty" EnableClientScript="True" ValidationGroup="LoginGroup" ValidateEmptyText="True" runat="server"  Display="None" ErrorMessage="Вы не ввели отчество директора" ></asp:CustomValidator>
    <asp:CustomValidator ID="CustomValidator12" ControlToValidate="tbDirectorPhoneNumber" ClientValidationFunction="validateIfEmpty" EnableClientScript="True" ValidationGroup="LoginGroup" ValidateEmptyText="True" runat="server"  Display="None" ErrorMessage="Вы не ввели телефон директора" ></asp:CustomValidator>
    <asp:CustomValidator ID="CustomValidator13" ControlToValidate="tbCompanuName" ClientValidationFunction="validateIfEmpty" EnableClientScript="True" ValidationGroup="LoginGroup" ValidateEmptyText="True" runat="server"  Display="None" ErrorMessage="Вы не ввели название компании" ></asp:CustomValidator>
    <asp:CustomValidator ID="CustomValidator14" ControlToValidate="tbCompanyAddress" ClientValidationFunction="validateIfEmpty" EnableClientScript="True" ValidationGroup="LoginGroup" ValidateEmptyText="True" runat="server"  Display="None" ErrorMessage="Вы не ввели юр. адрес компании" ></asp:CustomValidator>
    <asp:CustomValidator ID="CustomValidator15" ControlToValidate="tbPostAddress" ClientValidationFunction="validateIfEmpty" EnableClientScript="True" ValidationGroup="LoginGroup" ValidateEmptyText="True" runat="server"  Display="None" ErrorMessage="Вы не ввели контактный адрес" ></asp:CustomValidator>
    <asp:CustomValidator ID="CustomValidator16" ControlToValidate="tbRS" ClientValidationFunction="validateNotEmptyNumber" EnableClientScript="True" ValidationGroup="LoginGroup" ValidateEmptyText="True" runat="server"  Display="None" ErrorMessage="Вы неверно ввели расчетный счет" ></asp:CustomValidator>
    <asp:CustomValidator ID="CustomValidator17" ControlToValidate="tbUNP" ClientValidationFunction="validateNotEmptyNumber" EnableClientScript="True" ValidationGroup="LoginGroup" ValidateEmptyText="True" runat="server"  Display="None" ErrorMessage="Вы неверно ввели УНП" ></asp:CustomValidator>
    <asp:CustomValidator ID="CustomValidator18" ControlToValidate="tbBankName" ClientValidationFunction="validateIfEmpty" EnableClientScript="True" ValidationGroup="LoginGroup" ValidateEmptyText="True" runat="server"  Display="None" ErrorMessage="Вы не ввели название банка" ></asp:CustomValidator>
    <asp:CustomValidator ID="CustomValidator19" ControlToValidate="tbBankAddress" ClientValidationFunction="validateIfEmpty" EnableClientScript="True" ValidationGroup="LoginGroup" ValidateEmptyText="True" runat="server"  Display="None" ErrorMessage="Вы не ввели адрес банка" ></asp:CustomValidator>
    <asp:CustomValidator ID="CustomValidator20" ControlToValidate="tbBankCode" ClientValidationFunction="validateNotEmptyNumber" EnableClientScript="True" ValidationGroup="LoginGroup" ValidateEmptyText="True" runat="server"  Display="None" ErrorMessage="Вы неверно ввели код банка" ></asp:CustomValidator>
    <asp:CustomValidator ID="CustomValidator21" ControlToValidate="tbContactPersonFIO" ClientValidationFunction="validateIfEmpty" EnableClientScript="True" ValidationGroup="LoginGroup" ValidateEmptyText="True" runat="server"  Display="None" ErrorMessage="Вы не ввели ФИО контактного лица" ></asp:CustomValidator>
    <asp:CustomValidator ID="CustomValidator22" ControlToValidate="tbContactPhoneNumbers" ClientValidationFunction="validateIfEmpty" EnableClientScript="True" ValidationGroup="LoginGroup" ValidateEmptyText="True" runat="server"  Display="None" ErrorMessage="Вы не ввели контактный телефон" ></asp:CustomValidator>
                                                                                                                                                                                                                                                                            
    <asp:ValidationSummary ID="ValidationSummary1" runat="server" ValidationGroup="LoginGroup" ShowMessageBox="true" ShowSummary="false"/>
</asp:Content>
