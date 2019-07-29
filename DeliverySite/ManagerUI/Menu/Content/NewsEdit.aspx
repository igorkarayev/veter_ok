<%@ Page validateRequest="false" Title="" Language="C#" MasterPageFile="~/ManagerMasterPage.master" AutoEventWireup="true" CodeBehind="NewsEdit.aspx.cs" Inherits="Delivery.ManagerUI.Menu.Content.NewsEdit" %>
<%@ Register TagPrefix="CKEditor" Namespace="CKEditor.NET" Assembly="CKEditor.NET, Version=3.6.4.0, Culture=neutral, PublicKeyToken=e379cdf2f8354999" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <h3 class="h3custom" style="margin-top: 0;"><%# ActionText %> новости</h3>

    <div class="loginError" id="errorDiv" style="width: 90%">
        <asp:Label runat="server" ID="lblNotif" ForeColor="White"></asp:Label>
    </div>

    <table class="table" style="width: 90%">
            <tr>
                <td>
                    <span style="vertical-align: top">Заголовок:</span>
                </td>
                <td>
                    <asp:TextBox ID="tbTitle" CssClass="form-control" runat="server" width="100%"/>
                </td>
            </tr>
            <tr id="ForViewing">
                <td colspan="2">
                    Отображать в попапе, если не просмотрена просмотра <asp:CheckBox ID="cbForViewing" runat="server"/>
                    <asp:HiddenField runat="server" ID="hfForViewing"/>
                </td>
            </tr>
            <tr>
                <td>
                    <span style="vertical-align: top">Тип новости:</span>
                </td>
                <td>
                    <asp:DropDownList ID="ddlNewsTypeID" CssClass="multi-control" runat="server" style="width: 170px;"/>
                </td>
            </tr>
            <tr>
                <td style="vertical-align: top">
                    <span>Содержание:</span>
                </td>
                <td>
                    <CKEditor:CKEditorControl ID="tbBody" runat="server"></CKEditor:CKEditorControl>
                </td>
            </tr>
            <tr>
                <td colspan="2" style="padding-top: 20px;">
                    <asp:Button ID="btnCreate" class="btn btn-default btn-right" runat="server" Text='<%# ButtonText %>' ValidationGroup="LoginGroup" style="margin-left: 30px;"/>
                    <asp:Button ID="btnBack" OnClientClick="window.history.go(-1);return false;" class="btn btn-default btn-right" runat="server" Text='Назад'/>
                </td>
            </tr>
    </table>
    
    <script type="text/javascript">
        $(function () {
            CKEDITOR.replace('<%=tbBody.ClientID %>', { filebrowserImageUploadUrl: '../Upload.ashx' });

            /** Условия для отображение блока "За доставку" **/
            if ($("#<%= ddlNewsTypeID.ClientID %>").val() == "0") {
                $("#<%= cbForViewing.ClientID %>").removeAttr('checked');
                $("#ForViewing").hide();
            } else {
                $("#ForViewing").show();
                if ($("#<%= hfForViewing.ClientID %>").val() == "1") {
                    $("#<%= cbForViewing.ClientID %>").prop("checked", true);
                }
            }

            $("#<%= ddlNewsTypeID.ClientID %>").change(function () {
                if ($(this).val() == "0") {
                    $("#<%= cbForViewing.ClientID %>").removeAttr('checked');
                     $("#ForViewing").hide();
                 } else {
                    $("#ForViewing").show();
                    if ($("#<%= hfForViewing.ClientID %>").val() == "1") {
                        $("#<%= cbForViewing.ClientID %>").prop("checked", true);
                    }
                 }
            });
            
            if ($('#<%= lblNotif.ClientID%>').html() == "") {
                $('#errorDiv').hide();
            } else {
                $('#errorDiv').show();
            }
        });
    </script>
</asp:Content>
