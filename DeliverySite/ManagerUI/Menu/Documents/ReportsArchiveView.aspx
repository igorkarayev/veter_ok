<%@ Page Title="" Language="C#" MasterPageFile="~/ManagerMasterPage.master" AutoEventWireup="true" CodeBehind="ReportsArchiveView.aspx.cs" Inherits="Delivery.ManagerUI.Menu.Documents.ReportsArchiveView" %>
<%@ Import Namespace="Delivery.BLL.StaticMethods" %> 
<%@ Import Namespace="Delivery.BLL.Helpers" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <script type="text/javascript">
        $(function() {
            $.datepicker.setDefaults($.datepicker.regional['ru']);
            $("#<%= stbCreateDate1.ClientID %>")
                .datepicker({ nextText: "", prevText: "", changeMonth: true, changeYear: true })
                .mask("99-99-9999");
            $("#<%= stbDocumentDate1.ClientID %>")
                .datepicker({ nextText: "", prevText: "", changeMonth: true, changeYear: true })
                .mask("99-99-9999");
            $("#<%= stbCreateDate2.ClientID %>")
                .datepicker({ nextText: "", prevText: "", changeMonth: true, changeYear: true })
                .mask("99-99-9999");
            $("#<%= stbDocumentDate2.ClientID %>")
                .datepicker({ nextText: "", prevText: "", changeMonth: true, changeYear: true })
                .mask("99-99-9999");
        });
    </script>
   <h3 class="h3custom" style="margin-top: 0;">Архив документов</h3>
    <div style="min-height: 300px;">
        <asp:Panel runat="server" ID="SearchPanel" CssClass="mainFont" DefaultButton="btnSearch">
            <table class="table searchPanel" style="margin-left:auto; margin-right: auto;">
                <tr>
                    <td>
                        Дата создания: c

                        <asp:TextBox runat="server" ID="stbCreateDate1" CssClass="searchField form-control" Width="75px"></asp:TextBox>
                        по
                        <asp:TextBox runat="server" ID="stbCreateDate2" CssClass="searchField form-control" Width="75px"></asp:TextBox>
                    </td>                   
                    <td style="text-align: right;">
                        &nbsp;&nbsp;&nbsp;<i class="small-informer">Найдено:<b> <asp:Label runat="server" ID="lblAllResult"></asp:Label></b></i> 
                    </td>
                </tr>
                <tr>
                    <td>
                        Дата документа: с

                        <asp:TextBox runat="server" ID="stbDocumentDate1" CssClass="searchField form-control" Width="75px"></asp:TextBox>
                        по
                        <asp:TextBox runat="server" ID="stbDocumentDate2" CssClass="searchField form-control" Width="75px"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td>
                        DriverID:
                        <asp:TextBox runat="server" ID="stbDID" CssClass="searchField form-control"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td>
                       
                        Пут. #1 <asp:CheckBox runat="server" ID="cbPutevoi1"/>

                        Пут. #2 <asp:CheckBox runat="server" ID="cbPutevoi2"/>

                        <asp:Panel runat="server" ID="pnlPutevoi3" style="display: inline-block">
                            Пут. #3 <asp:CheckBox runat="server" ID="cbPutevoi3"/>
                        </asp:Panel>

                        ЗП #1 <asp:CheckBox runat="server" ID="cbZP"/>
                        
                        Прил. #1 <asp:CheckBox runat="server" ID="cbNaklPlil"/>

                        Акты <asp:CheckBox runat="server" ID="CbAct"/>

                        Расч. листы <asp:CheckBox runat="server" ID="cbRasch"/>
                        
                    </td>
                    <td style="text-align: right;">
                        <asp:Button runat="server" Text="Сброс" CssClass="btn btn-default" ID="btnReload"/>
                        <asp:Button runat="server" Text="Искать" CssClass="btn btn-default" ID="btnSearch"/>    
                    </td>
                </tr>
                
            </table>
        </asp:Panel><br/>

        <asp:ListView runat="server" ID="lvAllUsers">
        <LayoutTemplate>
            <table runat="server" id="Table1" class="table tableViewClass tableClass">
                <tr class="table-header">
                    <th style="width: 40px;">
                        ID
                    </th>
                    <th style="width: 40px;">
                        DID
                    </th>
                    <th>
                        ФИО
                    </th>
                    <th style="width: 240px;">
                        Тип документа
                    </th>
                    <th style="width: 110px;">
                        Дата документа
                    </th>
                    <th style="width: 140px;">
                        Дата создания
                    </th>
                    <th style="width: 80px;">
                        Действие
                    </th>
                </tr>
                <tr runat="server" id="itemPlaceholder"></tr>
            </table>
        </LayoutTemplate>
        <ItemTemplate>
            <tr id="Tr2" runat="server" class='<%# ReportsHelper.ColoredReportRows(Eval("ReportType").ToString()) %>' style="text-align: center;">
                <td id="Td4" runat="server">
                    <asp:HyperLink runat="server" id="HyperLink2" NavigateUrl='<%# "~/AppServices/ReportService.asmx/ViewReport?reportid=" + Eval("ID") + "&appkey=4555eTT6g@3" %>' Target="new">
                        <asp:Label ID="Label3" runat="server" Text='<%# Eval("ID") %>' />
                    </asp:HyperLink>
                </td>
                <td id="Td0" runat="server">
                    <asp:Label ID="lblDate" runat="server" Text='<%# Eval("DriverID") %>' />
                </td>
                <td id="Td7" runat="server" style="text-align: left;">
                    <asp:Label ID="lblID" runat="server" Text='<%# Eval("DriverName") %>' />
                </td>
                <td id="Td2" runat="server">
                    <asp:Label ID="Label2" runat="server" Text='<%# ReportsHelper.ReportConverter(Convert.ToInt32(Eval("ReportType").ToString())) %>' />
                </td>
                <td id="Td5" runat="server">
                    <asp:Label ID="Label4" runat="server" Text='<%# OtherMethods.DateConvert(Eval("DocumentDate").ToString()) %>' />
                </td>
                <td id="Td1" runat="server">
                    <asp:Label ID="Label1" runat="server" Text='<%# OtherMethods.DateTimeConvert(Eval("CreateDate").ToString()) %>' />
                </td>
                <td id="Td3" runat="server">
                    <asp:HyperLink runat="server" id="HyperLink1" NavigateUrl='<%# "~/AppServices/ReportService.asmx/ViewReport?reportid=" + Eval("ID") + "&appkey=4555eTT6g@3" %>' Target="new">
                        <asp:Image ID="iView" runat="server" ImageUrl="~/Styles/Images/View.png" Width="20px" style="margin-bottom:1px;"/>
                    </asp:HyperLink>
                    <asp:HyperLink runat="server" id="hlEdit" NavigateUrl='<%# "~/ManagerUI/Menu/Documents/ReportsArchiveEdit.aspx?id=" + Eval("ID") %>'>
                        <asp:Image ID="ibEdit" runat="server" ImageUrl="~/Styles/Images/edit.png" CssClass="imageAction" />
                    </asp:HyperLink>
                    <asp:ImageButton ID="ibDelete" runat="server" ImageUrl="~/Styles/Images/Delete.png" CssClass="imageAction" CommandName="Delete" CommandArgument='<%# Eval("ID") %>' OnClientClick="return confirmDelete();"/>
                </td>
            </tr>
        </ItemTemplate>
        <EmptyDataTemplate>
            <asp:ListView runat="server" ID="lvAllTickets">  
                <LayoutTemplate>
                        <asp:CheckBox ID="chkboxSelectAll" runat="server"/>
                        <div runat="server" id="itemPlaceholder"></div>
                </LayoutTemplate>
            </asp:ListView>
            <div style="width: 100%; text-align: center; padding: 15px; color: red">Документов на данную дату не найдено...</div>
        </EmptyDataTemplate>
    </asp:ListView>
    <div class="infoBlock" style="margin-bottom: 0;">
        <div class="pager">
            <asp:Label ID="lblPage" runat="server">Страница:</asp:Label>
            <asp:DataPager ID="lvDataPager" runat="server" PagedControlID="lvAllUsers" PageSize="100">
                <Fields>
                    <asp:NumericPagerField ButtonType="Link" />
                </Fields>
            </asp:DataPager>  
        </div>
    </div>
    </div>
</asp:Content>
