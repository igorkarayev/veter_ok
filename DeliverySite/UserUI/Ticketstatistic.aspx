<%@ Page Title="" Language="C#" MasterPageFile="~/UserMasterPage.master" AutoEventWireup="true" CodeBehind="Ticketstatistic.aspx.cs" Inherits="DeliverySite.UserUI.Ticketstatistic" Async="true"%>

<asp:Content ContentPlaceHolderID="MainContent" runat="server">
    <script>
        $(function () {
            $.datepicker.setDefaults($.datepicker.regional['ru']);
            $("#<%= stbDeliveryDate1.ClientID %>")
                .datepicker({ nextText: "", prevText: "", changeMonth: true, changeYear: true })
                .mask("99-99-9999");
            $("#<%= stbDeliveryDate2.ClientID %>")
                .datepicker({ nextText: "", prevText: "", changeMonth: true, changeYear: true })
                .mask("99-99-9999");
        });
    </script>

     <table class="searchPanel" style="margin-left:auto; margin-right: auto;">
         <tr>
            <td>
                UID:
                <asp:TextBox runat="server" ID="stbUID" CssClass="searchField form-control" AccessKey="u" EnableViewState="False"></asp:TextBox>
            </td>
         </tr>
         <tr>
            <td colspan="3" style="text-align: left;">
                Статистика с &nbsp;с:&nbsp;<asp:TextBox runat="server" ID="stbDeliveryDate1" CssClass="searchField form-control" Width="80px" EnableViewState="False"></asp:TextBox>
                &nbsp;по:&nbsp;<asp:TextBox runat="server" ID="stbDeliveryDate2" CssClass="searchField form-control" Width="80px" EnableViewState="False"></asp:TextBox>
            </td>
         </tr>
         <tr>
             <td>
                 <asp:Button Text="Скачать файл статистики" ID="DownloadButton" CssClass="btn btn-default" runat="server"/>
             </td>
         </tr>
     </table>
</asp:Content>


