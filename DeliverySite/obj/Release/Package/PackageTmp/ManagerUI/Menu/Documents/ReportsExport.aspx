<%@ Page Title="" Language="C#" MasterPageFile="~/ManagerMasterPage.master" AutoEventWireup="true" CodeBehind="ReportsExport.aspx.cs" Inherits="Delivery.ManagerUI.Menu.Documents.ReportsExport" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
   <h3 class="h3custom" style="margin-top: 0;">Экспорт данных</h3>
    <div style="min-height: 300px; width: 500px; margin-left: auto; margin-right: auto;" >
        <div class="infoBlock2-grey" style="text-align: left">
            <asp:Panel runat="server" ID="pnlActionExportUsersEmails">
                <h3 style="margin-bottom: 0; margin-top: 0;">Списки для рассылки</h3>
                <div style="padding-left: 15px;">
                    <asp:LinkButton ID="lbGetActivatedUsersEmails" style="color: #ff8400 !important"  runat="server">Скачать список емейлов активированных клиентов (*.txt)</asp:LinkButton><br/>
                    <asp:LinkButton ID="lbGetAllUsersEmails" style="color: #ff8400 !important"  runat="server">Скачать список емейлов всех клиентов (*.txt)</asp:LinkButton>
                </div>
            </asp:Panel>

            <asp:Panel runat="server" ID="pnlActionExportAllUsersInfo" Visible="False">
                <h3 style="margin-bottom: 0;">Данные по активированным клиентам</h3>
                <div style="padding-left: 15px;">
                    <asp:LinkButton ID="lbGetAllUsersInfo" style="color: #ff8400 !important"  runat="server">Скачать список клиентов *.xls</asp:LinkButton>
                </div>
            </asp:Panel>
            
            <asp:Panel runat="server" ID="pnlActionExportAllUsersProfilesInfo" Visible="False">
                <h3 style="margin-bottom: 0;">Данные по профилям активированных клиентов</h3>
                <div style="padding-left: 15px;">
                    <asp:LinkButton ID="lbGetAllUsersProfilesInfo" style="color: #ff8400 !important" runat="server">Скачать список профилей *.xls</asp:LinkButton>
                </div>
            </asp:Panel>
            
            <asp:Panel runat="server" ID="pnlRefundsInformation">
                <h3 style="margin-bottom: 0;">Информация по заявкам-возвратам</h3>
                Возвращены &nbsp;с:&nbsp;<asp:TextBox runat="server" ID="stbCreateFrom" CssClass="searchField form-control" Width="80px" EnableViewState="False"></asp:TextBox>
                        &nbsp;по:&nbsp;<asp:TextBox runat="server" ID="stbCreateTo" CssClass="searchField form-control" Width="80px" EnableViewState="False"></asp:TextBox>включ.
                <div style="padding-left: 15px;">
                    <asp:LinkButton ID="lbGetRefundsInformation" style="color: #ff8400 !important"  runat="server">Перейти на страницу печати выбранных возвратов</asp:LinkButton>
                </div>
            </asp:Panel>
        </div>
    </div>
    <script>
        $(function() {

            $.datepicker.setDefaults($.datepicker.regional['ru']);
            $("#<%= stbCreateFrom.ClientID %>")
                .datepicker({ nextText: "", prevText: "", changeMonth: true, changeYear: true })
                .mask("99-99-9999");
            $("#<%= stbCreateTo.ClientID %>")
                .datepicker({ nextText: "", prevText: "", changeMonth: true, changeYear: true })
                .mask("99-99-9999");
        });
    </script>
</asp:Content>
