<%@ Page Title="" Language="C#" MasterPageFile="~/UserMasterPage.master" AutoEventWireup="true" CodeBehind="FeedbackCreate.aspx.cs" Inherits="Delivery.UserUI.FeedbackCreate"  Async="true"%>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <script>
        $(function(){            
            $('#<%= ddlType.ClientID %>').change(function () {
                if ($(this).val() == "2") {
                    $('#category-text').show();
                } else {
                    $('#category-text').hide();
                }
            });
            
            if ($('#<%= lblError.ClientID%>').html() == "") {
                $('#errorDiv').hide();
            } else {
                $('#errorDiv').show();
            }

            $('.bbcode').markItUp(myBBCodeSettings);

            $('.bbcode').keydown(function (e) {
                if (e.ctrlKey && e.keyCode == 13) {
                    $("#<%=btnCreate.ClientID%>").click();
                }
            });
        });
    </script>
    <style>
        .markItUpEditor {
            border:1px solid #ccc !important;
            font-size:14px !important;
            font-family: Arial !important;
        }
    </style>
    <h3 class="h3custom" style="margin-top: 0;">Создание обращения</h3>
    <div class="loginError" id="errorDiv" style="width: 90%; display: none;">
        <asp:Label runat="server" ID="lblError" ForeColor="White"></asp:Label>
    </div>
    <div class="single-input-fild" style="margin-bottom: 20px; width: 50%; display: inline-block;  vertical-align: top; padding-left: 5%;">
        <div class="form-group">
            Тип обращения<br/>
            <asp:DropDownList ID="ddlType" runat="server" width="100%" CssClass="ddl-control" style="width: 50%;"/>
        </div>
        
        <div class="form-group cost-text-unvisible">
            Приоритет обращения<br/>
            <asp:DropDownList ID="ddlPriority" runat="server" width="100%" CssClass="ddl-control" style="width: 50%;"/>
        </div>
        
        <div class="form-group cost-text-unvisible">
            Тема
            <asp:TextBox ID="tbTitle" runat="server" width="100%" CssClass="form-control" style="width: 96%;" placeholder="Введите заголовок обращения"/>
        </div>
        
        <div class="form-group cost-text-unvisible">
            <asp:TextBox ID="tbBody" runat="server" TextMode="MultiLine" CssClass="multi-control bbcode" style="width: 100%; height: 200px; padding: 10px;" placeholder="Содержание обращения"/>
        </div>

        <div class="form-group cost-text-unvisible">
            Картинка
            <asp:FileUpload ID="fuImage" runat="server" CssClass="form-control" style="width: 99%; padding: 5px 5px 10px 5px;"/>
        </div>

        <div class="form-group cost-text-unvisible" style="margin-top: 10px;">
            <asp:Button ID="btnCreate" runat="server" Text='Отправить' CssClass="btn btn-default btn-right" style="margin-left: 30px;" ValidationGroup="LoginGroup"/>
            <asp:Button ID="btnBack" OnClientClick="window.history.go(-1);return false;" runat="server" CssClass="btn btn-default btn-right" Text='Назад'/>
        </div>
    </div>
    
    <div class="single-input-fild" style="width: 35%; display: inline-block; vertical-align: top; padding-left: 5%; margin-bottom: 20px;">
        <div style="padding-left: 20px; text-align: justify; font-style:italic">
            <div class="cost-text-unvisible">
                Через обращения вы можете связаться с нашими сотрудниками в случае возникновения  <span style="border-bottom: 1px dotted black">проблемы\идеи\жалобы</span>. <br/><br/>
                После отправки обращения наши сотрудники будут уведомлены об этом и в кратчайшие срок ответят вам. <br/><br/>
                Весь диалог с сотрудниками будет вестись  <span style="border-bottom: 1px dotted black">внутри сайта</span>. 
                Так вы всегда можете просмотреть все свои обращения и комментарии к ним, ответить на комментарий менеджера.<br/>
                Обращения, которые были не активны (не комментировались или не имели комментариев) через 21 день автоматически закрываются.
            </div>
            
            <div style="display: none; margin-top: 25px;" class="cost-text-unvisible" id="category-text">
                В содержании введите типовые для вашего груза характеристики, а также название требуемой категории:
                <span style="border-bottom: 1px dotted black">ДЛИНА х ШИРИНА х ВЫСОТА и ВЕС</span>
                <br>
                <br>
                Так же напишите<span style="border-bottom: 1px dotted black">особенности перевозки груза</span>
                <br>
                (к примеру:<b>хрупкие грузы</b>) 
            </div>
        </div>
    </div>
    
    <asp:CustomValidator ID="CustomValidator3" ControlToValidate="tbTitle" ClientValidationFunction="validateIfEmpty" EnableClientScript="True" ValidationGroup="LoginGroup" ValidateEmptyText="True" runat="server"  Display="None" ErrorMessage="Вы не ввели тему обращения" ></asp:CustomValidator>
    <asp:CustomValidator ID="CustomValidator4" ControlToValidate="tbBody" ClientValidationFunction="validateIfEmpty" EnableClientScript="True" ValidationGroup="LoginGroup" ValidateEmptyText="True" runat="server"  Display="None" ErrorMessage="Вы не ввели содержание обращения" ></asp:CustomValidator>
    
    
    <asp:ValidationSummary ID="ValidationSummary1" runat="server" ValidationGroup="LoginGroup" ShowMessageBox="true" ShowSummary="false"/>
</asp:Content>
