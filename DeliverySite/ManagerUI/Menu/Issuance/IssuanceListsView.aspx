<%@ Page Title="" Language="C#" MasterPageFile="~/ManagerMasterPage.master" AutoEventWireup="true" CodeBehind="IssuanceListsView.aspx.cs" Inherits="Delivery.ManagerUI.Menu.Issuance.IssuanceListsView" %>
<%@ Import Namespace="Delivery.BLL.Helpers" %> 
<%@ Import Namespace="Delivery.BLL.StaticMethods" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <script type="text/javascript">
        $(function() {
            if ($('#<%= lblError.ClientID%>').html() == "") {
                $('#errorDiv').hide();
            } else {
                $('#errorDiv').show();
            }
        });
    </script>

    <div>
        <asp:HyperLink ID="HyperLink1" runat="server" CssClass="createItemHyperLink" NavigateUrl="~/ManagerUI/Menu/Issuance/IssuanceListsEdit.aspx">Создать расчетный лист</asp:HyperLink>
        <h3 class="h3custom" style="margin-top: 0;">Список расчетных листов</h3>
    </div>
    <div class="loginError" id="errorDiv" style="width: 90%;">
        <asp:Label runat="server" ID="lblError" ForeColor="White"></asp:Label>
    </div>   
    <div>
        <asp:ListView runat="server" ID="lvAllCity">
        <LayoutTemplate>
            <table runat="server" id="Table1" class="table tableViewClass tableClass">
                <tr style="background-color: #EECFBA">
                    <th>
                        ID
                    </th>
                    <th>
                        UID
                    </th>
                    <th>
                        Дата создания
                    </th>
                    <th>
                        Дата расчета
                    </th>
                    <th>
                        Комментарий
                    </th>
                    <th>
                        Статус
                    </th>
                    <th>
                    </th>
                </tr>
                <tr runat="server" id="itemPlaceholder"></tr>
            </table>
        </LayoutTemplate>
        <ItemTemplate>
            <tr id="Tr2" runat="server" style="text-align: center;" class='<%# OtherMethods.ColoredIssuanceStatusRows(Eval("IssuanceListsStatusID").ToString()) %>'>
                <td id="Td2" runat="server">
                    <asp:HyperLink ID="LinkButton1" runat="server" NavigateUrl='<%#"~/ManagerUI/Menu/Issuance/IssuanceListView.aspx?id="+Eval("ID") %>'>
                        <asp:Label ID="lblName" runat="server" Text='<%#Eval("ID") %>' />
                    </asp:HyperLink>
                </td>
                
                <td id="Td1" runat="server">
                    <asp:HyperLink ID="HyperLink4" runat="server" NavigateUrl='<%#"~/ManagerUI/Menu/Souls/ClientEdit.aspx?id="+Eval("UserID") %>'>
                        <asp:Label ID="Label5" runat="server" Text='<%#Eval("UserID") %>' />
                    </asp:HyperLink>
                </td>

                <td id="Td4" runat="server">
                    <asp:Label ID="lblTrack" runat="server" Text='<%# OtherMethods.DateConvert(Eval("CreateDate").ToString()) %>' />
                </td>
                
                <td id="Td5" runat="server">
                    <asp:Label ID="lblCost" runat="server" Text='<%# OtherMethods.DateConvert(Eval("IssuanceDate").ToString()) %>' />
                </td>
                
                <td id="Td6" runat="server">
                    <asp:Label ID="Label1" runat="server" Text='<%#Eval("Comment") %>' />
                </td>

                <td id="Td7" runat="server">
                    <asp:Label ID="Label2" runat="server" Text='<%# IssuanceListsHelper.IssuanceStatusToText(Eval("IssuanceListsStatusID").ToString()) %>' />
                    <asp:HiddenField ID="hfIssuanceListsStatusID" runat="server" Value='<%# Eval("IssuanceListsStatusID") %>' />
                </td>
                
                <td id="Td3" runat="server"  style="font-size: 12px;">
                    <asp:HyperLink ID="HyperLink3" runat="server" NavigateUrl='<%#"~/ManagerUI/Menu/Issuance/IssuanceListView.aspx?id="+Eval("ID") %>'>
                        Просмотр
                    </asp:HyperLink><br/>
                    <asp:HyperLink ID="HyperLink2" runat="server" NavigateUrl='<%#"~/ManagerUI/Menu/Issuance/IssuanceListsEdit.aspx?id="+Eval("ID") %>'>
                        Редактировать
                    </asp:HyperLink><br/>
                    <asp:LinkButton ID="lbClose" runat="server" OnClick="lbClose_Click" CommandArgument='<%#Eval("ID") %>'>Закрыть</asp:LinkButton>
                    <asp:LinkButton ID="lbOpen" runat="server" OnClick="lbReOpen_Click" CommandArgument='<%#Eval("ID") %>'>Переоткрыть</asp:LinkButton><br/>
                    <asp:LinkButton ID="lbDelete" runat="server" ForeColor="Red" Visible="<%# IsDeleteButtonVisible %>" OnClick="lbDelete_Click" CommandArgument='<%#Eval("ID") %>'>Удалить</asp:LinkButton>
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
            <div style="width: 100%; text-align: center; padding: 15px; color: red">По данным критериям ничего не найдено...</div>
        </EmptyDataTemplate>
    </asp:ListView>
    <div class="infoBlock" style="margin-bottom: 0;">
            <div class="pager">
                <asp:Label ID="lblPage" runat="server">Страница:</asp:Label>
                <asp:DataPager ID="lvDataPager" runat="server" PagedControlID="lvAllCity" PageSize="25">
                    <Fields>
                        <asp:NumericPagerField ButtonType="Link" />
                    </Fields>
                </asp:DataPager>  
            </div>
        </div>
    </div>
</asp:Content>
