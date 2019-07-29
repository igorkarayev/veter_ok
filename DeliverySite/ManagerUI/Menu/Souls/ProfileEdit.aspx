<%@ Page Title="" Language="C#" MasterPageFile="~/ManagerMasterPage.master" AutoEventWireup="true" CodeBehind="ProfileEdit.aspx.cs" Inherits="Delivery.ManagerUI.Menu.Souls.ProfileEdit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <script type="text/javascript">
        $(function () {
            $("#<%= tbDirectorPhoneNumber.ClientID %>").mask("+375 (99) 999-99-99");
            $("#<%= tbContactPhoneNumbers.ClientID %>").mask("+375 (99) 999-99-99");
            $("#<%= tbContactPhoneNumbers2.ClientID %>").mask("+375 (99) 999-99-99");
            $("#<%= tbContactPhoneNumbersFiz.ClientID %>").mask("+375 (99) 999-99-99");
            $("#<%= tbContactPhoneNumbers2Fiz.ClientID %>").mask("+375 (99) 999-99-99");
            $("#<%= tbPassportDate.ClientID %>")
                .datepicker({ nextText: "", prevText: "", changeMonth: true, changeYear: true })
                .mask("99-99-9999");
            $("#<%= tbAgreementDate.ClientID %>")
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

        });

        function validatePasportSeriaProfileEdit(sender, args) {
            var isValid;
            var myValue = args.Value;
            if ($('#<%= hfProfileType.ClientID %>').val() == "1") {
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
            if ($('#<%= hfProfileType.ClientID %>').val() == "1") {
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
    <h3 class="h3custom" style="margin-top: 0;">Редактирование профиля клиента #<asp:Label runat="server" ID="lblClientId"></asp:Label></h3>
    <table class="table" style="width: 650px; margin-left: auto; margin-right: auto;">
        <tr runat="server" ID="trProfileTypeLbl">
            <td class="valignTable" style="width: 190px;">
                <span>Тип профиля:</span>
            </td>
            <td class="paddingTable">
                <asp:Label runat="server" ID="lblProfileType" />
                <asp:HiddenField runat="server" ID="hfProfileType" />
            </td>
        </tr>
        
        <tr runat="server" ID="trProfileTypeDdl" Visible="False">
            <td class="valignTable" style="width: 190px;">
                <span>Тип профиля:</span>
            </td>
            <td class="paddingTable">
                <asp:DropDownList CssClass="ddl-control" runat="server" ID="ddlProfileType" AutoPostBack="False"  width="200px"/>
            </td>
        </tr>
        
        <tr>
            <td class="valignTable" style="width: 190px;">
                <span>Статус:</span>
            </td>
            <td class="paddingTable">
                <asp:Label runat="server" ID="lblStatus" />
            </td>
        </tr>
    </table>

    <asp:Panel runat="server" ID="pnlFiz" >
        <table class="table" style="width: 650px; margin-left: auto; margin-right: auto;">
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
                    #1: <asp:TextBox CssClass="form-control" ID="tbContactPhoneNumbersFiz" runat="server" Width="135px"/>
                    <br/><br/>
                    #2: <asp:TextBox CssClass="form-control" ID="tbContactPhoneNumbers2Fiz" runat="server" Width="135px"/>
                </td>
            </tr>
        </table>
    </asp:Panel>

    <asp:Panel runat="server" ID="pnlUr">
        <table class="table" style="width: 650px; margin-left: auto; margin-right: auto;">
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
                    #1: <asp:TextBox CssClass="form-control" ID="tbContactPhoneNumbers" runat="server" Width="135px"/>&nbsp;&nbsp;&nbsp;Совпадает с тел. директора <asp:CheckBox ID="cbContactPhoneNumber" runat="server"/>
                    <br/><br/>
                    #2: <asp:TextBox CssClass="form-control" ID="tbContactPhoneNumbers2" runat="server" Width="135px"/>
                </td>
            </tr>
        </table>
    </asp:Panel>
    
    <asp:Panel runat="server" ID="pnlReject">
        <table style="width: 650px; margin-left: auto; margin-right: auto;">
            <tr>
                    <td class="valignTable" style="width: 190px;">
                        <span>Причина отклонения или блокировки <i>(если требуется)</i>:</span>
                    </td>
                    <td class="paddingTable">
                        <asp:TextBox CssClass="multi-control" ID="tbRejectBlockedMessage" TextMode="MultiLine" runat="server" width="91%"/>
                    </td>
                </tr>
        </table>
    </asp:Panel>
     <table class="table" class="table" style="width: 650px; margin-left: auto; margin-right: auto;">
        <tr>
            <td>
                Номер договора: 
                <asp:TextBox ID="tbAgreementNumber" runat="server" CssClass="form-control" Width="150px"/>    
            </td>
            <td>
                 Дата заключения договора: 
                <asp:TextBox ID="tbAgreementDate" runat="server" CssClass="form-control" Width="75px"/>   
            </td>
        </tr>
    </table>

    <table class="table" style="width: 850px; margin-left: auto; margin-right: auto;  margin-top: 20px">
        <tr>
            <td colspan="2">
                <asp:Button ID="btnCreateDoc" runat="server" CssClass="btn btn-default btn-right" style="margin-left: 35px;" Text="Договор"/>
                <asp:Button runat="server" ID="btnEdit" CssClass="btn btn-default btn-right" style="margin-left: 35px;" ValidationGroup="LoginGroup" Text="Сохранить"/>
                <asp:Button ID="btnDeleteProfile" runat="server"  CssClass="btn btn-default btn-right" OnClientClick="return confirmDelete();" style="margin-left: 35px; background-color: #D38F87; border: 1px solid #CC493B" Text='Удалить профиль'/>
                <asp:Button ID="btnActivate" runat="server" ValidationGroup="LoginGroup" CssClass="btn btn-default btn-right" style="margin-left: 35px; background-color: #8DC6A0; border: 1px solid #0BA53E" Text='Активировать'/>
                <asp:Button ID="btnReject" runat="server"  CssClass="btn btn-default btn-right" style="margin-left: 35px; background-color: #D3D398; border: 1px solid #B2B210" Text='Отклонить'/>
                <asp:Button ID="btnBlock" runat="server"  CssClass="btn btn-default btn-right" style="margin-left: 35px; background-color: #D38F87; border: 1px solid #CC493B" Text='Заблокировать'/>
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
    <asp:CustomValidator ID="CustomValidator23" ControlToValidate="tbAgreementNumber" ClientValidationFunction="validateIfEmpty" EnableClientScript="True" ValidationGroup="LoginGroup" ValidateEmptyText="True" runat="server"  Display="None" ErrorMessage="Вы не ввели номер договора" ></asp:CustomValidator>
    <asp:CustomValidator ID="CustomValidator24" ControlToValidate="tbAgreementDate" ClientValidationFunction="validateIfEmpty" EnableClientScript="True" ValidationGroup="LoginGroup" ValidateEmptyText="True" runat="server"  Display="None" ErrorMessage="Вы не ввели дату подписания договора" ></asp:CustomValidator>
                                                                                                                                                                                                                                                                              
    <asp:ValidationSummary ID="ValidationSummary1" runat="server" ValidationGroup="LoginGroup" ShowMessageBox="true" ShowSummary="false"/>
</asp:Content>