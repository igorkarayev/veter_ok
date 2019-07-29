<%@ Page Title="" Language="C#" MasterPageFile="~/ManagerMasterPage.master" AutoEventWireup="true" CodeBehind="NotificationsView.aspx.cs" Inherits="Delivery.ManagerUI.Menu.Content.NotificatonsView" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <h3 class="h3custom" style="margin-top: 0;">Настраиваемые уведомления и ошибки сайта</h3>
    <div>
        <asp:ListView runat="server" ID="lvAllNotifications">
        <LayoutTemplate>
            <table runat="server" id="Table1" class="table tableViewClass tableClass">
                <tr style="background-color: #EECFBA">
                    <th>
                        ID
                    </th>
                    <th>
                        Дата изменения
                    </th>
                    <th>
                        Содержание
                    </th>
                    <th>
                        Описание
                    </th>
                    <th>
                    </th>
                </tr>
                <tr runat="server" id="itemPlaceholder"></tr>
            </table>
        </LayoutTemplate>
        <ItemTemplate>
            <tr id="Tr2" runat="server" style="background-color: #F7F8F9; text-align: center;">
                <td id="Td5" runat="server">
                    <asp:Label ID="Label1" runat="server" Text='<%#Eval("ID") %>' />
                </td>
                <td id="Td2" runat="server">
                    <asp:Label ID="lblChangeDate" runat="server" Text='<%#Eval("ChangeDate") %>' />
                </td>
                
                <td id="Td1" runat="server" style=" word-break:break-all;">
                    <asp:Label ID="lblDescription" runat="server" Text='<%# Server.HtmlEncode(Eval("Description").ToString()) %>' />
                </td>

                <td id="Td4" runat="server">
                    <asp:Label ID="lblTitle" runat="server" Text='<%# Server.HtmlEncode(Eval("DescriptionStatic").ToString()) %>' />
                </td>
                
                <td id="Td3" runat="server"  style="text-align: right; font-size: 12px;">
                     <asp:HyperLink ID="LinkButton1" runat="server" NavigateUrl='<%#"~/ManagerUI/Menu/Content/NotificationsEdit.aspx?id="+Eval("ID") %>'>Изменить</asp:HyperLink><br/>
                </td>
            </tr>
        </ItemTemplate>
        <AlternatingItemTemplate>
            <tr id="Tr2" runat="server" style="background-color: #EBEEF2; text-align: center;">
                <td id="Td5" runat="server">
                    <asp:Label ID="Label1" runat="server" Text='<%#Eval("ID") %>' />
                </td>
                <td id="Td2" runat="server">
                    <asp:Label ID="lblChangeDate" runat="server" Text='<%#Eval("ChangeDate") %>' />
                </td>
                
                <td id="Td1" runat="server" style=" word-break:break-all;">
                    <asp:Label ID="lblDescription" runat="server" Text='<%# Server.HtmlEncode(Eval("Description").ToString()) %>' />
                </td>

                <td id="Td4" runat="server">
                    <asp:Label ID="lblTitle" runat="server" Text='<%# Server.HtmlEncode(Eval("DescriptionStatic").ToString()) %>' />
                </td>
                
                <td id="Td3" runat="server"  style="text-align: right; font-size: 12px;">
                     <asp:HyperLink ID="LinkButton1" runat="server" NavigateUrl='<%#"~/ManagerUI/Menu/Content/NotificationsEdit.aspx?id="+Eval("ID") %>'>Изменить</asp:HyperLink><br/>
                </td>
            </tr>
        </AlternatingItemTemplate>
    </asp:ListView>
         <div class="infoBlock" style="margin-bottom: 0;">
            <div class="pager">
                <asp:Label ID="lblPage" runat="server">Страница:</asp:Label>
                <asp:DataPager ID="lvDataPager" runat="server" PagedControlID="lvAllNotifications" PageSize="25">
                    <Fields>
                        <asp:NumericPagerField ButtonType="Link" />
                    </Fields>
                </asp:DataPager>  
            </div>
        </div>
    </div>
</asp:Content>
