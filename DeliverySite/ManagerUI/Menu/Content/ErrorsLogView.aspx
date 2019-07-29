<%@ Page Title="" Language="C#" MasterPageFile="~/ManagerMasterPage.master" AutoEventWireup="true" CodeBehind="ErrorsLogView.aspx.cs" Inherits="Delivery.ManagerUI.Menu.Content.ErrorsLogView" %>
<%@ Import Namespace="Delivery.BLL" %> <%@ Import Namespace="Delivery.BLL.StaticMethods" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div>
        <asp:LinkButton ID="HyperLink1" runat="server" CssClass="createItemHyperLink" OnClick="lbDeleteAll_Click" OnClientClick="return confirm('Вы уверены?');">Очистить весь лог</asp:LinkButton>
        <h3 class="h3custom" style="margin-top: 0;">Лог системных ошибок</h3>
    </div>
    <div>    
        <asp:ListView runat="server" ID="lvAllErrors">
        <LayoutTemplate>
            <table runat="server" id="Table1" class="table tableViewClass tableClass">
                <tr style="background-color: #EECFBA">
                    <th>
                        Дата
                    </th>
                    <th>
                        IP
                    </th>
                    <th>
                        Полное описание (StackTrase)
                    </th>
                    <th>
                        Тип ошибки
                    </th>
                    <th>
                        
                    </th>
                    
                </tr>
                <tr runat="server" id="itemPlaceholder"></tr>
            </table>
        </LayoutTemplate>
        <ItemTemplate>
            <tr id="Tr2" runat="server" style="background-color: #F7F8F9; text-align: center;">
                <td id="Td1" runat="server">
                    <asp:HyperLink ID="LinkButton1" runat="server" NavigateUrl='<%#"~/ManagerUI/Menu/Content/ErrorsLogEdit.aspx?id="+Eval("ID") %>'>
                        <asp:Label ID="lblRegion" runat="server" Text='<%# Eval("Date") %>' />
                    </asp:HyperLink>
                </td>
                
                <td id="Td7" runat="server">
                    <asp:Label ID="lblCost" runat="server" Text='<%#Eval("IP") %>' />
                </td>
                
                <td id="Td6" runat="server">
                    <asp:Label ID="Label1" runat="server" Text='<%# OtherMethods.ErrorCuter(Eval("StackTrase").ToString()) %>' />
                </td>

                <td id="Td4" runat="server">
                    <asp:Label ID="lblTrack" runat="server" Text='<%# Eval("ErrorType") %>' />
                </td>
                
                <td id="Td3" runat="server"  style="font-size: 12px;">
                    <asp:LinkButton ID="lbDelete" runat="server" OnClick="lbDelete_Click" CommandArgument='<%#Eval("ID") %>' OnClientClick="return confirm('Вы уверены?');">Удалить</asp:LinkButton>
                </td>
            </tr>
        </ItemTemplate>
        <AlternatingItemTemplate>
            <tr id="Tr2" runat="server" style="background-color: #EBEEF2; text-align: center;">
                <td id="Td1" runat="server">
                    <asp:HyperLink ID="LinkButton1" runat="server" NavigateUrl='<%#"~/ManagerUI/Menu/Content/ErrorsLogEdit.aspx?id="+Eval("ID") %>'>
                        <asp:Label ID="lblRegion" runat="server" Text='<%# Eval("Date") %>' />
                    </asp:HyperLink>
                </td>
                
                <td id="Td7" runat="server">
                    <asp:Label ID="lblCost" runat="server" Text='<%#Eval("IP") %>' />
                </td>
                
                <td id="Td6" runat="server">
                    <asp:Label ID="Label1" runat="server" Text='<%# OtherMethods.ErrorCuter(Eval("StackTrase").ToString()) %>' />
                </td>

                <td id="Td4" runat="server">
                    <asp:Label ID="lblTrack" runat="server" Text='<%# Eval("ErrorType") %>' />
                </td>
                
                <td id="Td3" runat="server"  style="font-size: 12px;">
                    <asp:LinkButton ID="lbDelete" runat="server" OnClick="lbDelete_Click" CommandArgument='<%#Eval("ID") %>' OnClientClick="return confirm('Вы уверены?');">Удалить</asp:LinkButton>
                </td>
            </tr>
        </AlternatingItemTemplate>
        <EmptyDataTemplate>
            <div style="width: 100%; text-align: center; padding: 15px; color: red">По данным критериям ничего не найдено...</div>
        </EmptyDataTemplate>
    </asp:ListView>
    <div class="infoBlock" style="margin-bottom: 0;">
            <div class="pager">
                <asp:Label ID="lblPage" runat="server">Страница:</asp:Label>
                <asp:DataPager ID="lvDataPager" runat="server" PagedControlID="lvAllErrors" PageSize="25">
                    <Fields>
                        <asp:NumericPagerField ButtonType="Link" />
                    </Fields>
                </asp:DataPager>  
            </div>
        </div>
    </div>
</asp:Content>
