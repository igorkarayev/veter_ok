<%@ Page Title="" Language="C#" MasterPageFile="~/ManagerMasterPage.master" AutoEventWireup="true" CodeBehind="EmailNotificationsView.aspx.cs" Inherits="Delivery.ManagerUI.Menu.Content.EmailNotificatonsView" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <h3 class="h3custom" style="margin-top: 0;">Список email-уведомлений</h3>
    <div>
        <asp:ListView runat="server" ID="lvAllEmailNotifications">
        <LayoutTemplate>
            <table runat="server" id="Table1" class="table tableViewClass tableClass">
                <tr style="background-color: #EECFBA">
                    <th style="text-align: left;">
                        Название
                    </th>
                    <th style="text-align: left;">
                        Содержание
                    </th>
                    <th>
                        Заголовок
                    </th>
                    <th>
                        Дата изменения
                    </th>
                </tr>
                <tr runat="server" id="itemPlaceholder"></tr>
            </table>
        </LayoutTemplate>
        <ItemTemplate>
            <tr id="Tr2" runat="server" style="background-color: #F7F8F9; text-align: center;">
                
                <td id="Td7" runat="server" style="text-align: left;">
                    <asp:HyperLink ID="LinkButton1" runat="server" NavigateUrl='<%#"~/ManagerUI/Menu/Content/EmailNotificationsEdit.aspx?id="+Eval("ID") %>'>
                        <asp:Label ID="Label3" runat="server" Text='<%#Eval("Name") %>' />
                    </asp:HyperLink>
                </td>
                
                <td id="Td1" runat="server" style="text-align: left;">
                    <asp:Label ID="lblDescription" runat="server" Text='<%# Server.HtmlEncode(Eval("Description").ToString()) %>' />
                </td>

                <td id="Td4" runat="server">
                    <asp:Label ID="lblTitle" runat="server" Text='<%# Server.HtmlEncode(Eval("Title").ToString()) %>' />
                </td>
                
                <td id="Td5" runat="server">
                    <asp:Label ID="Label1" runat="server" Text='<%#Eval("ChangeDate") %>' />
                </td>
            </tr>
        </ItemTemplate>
        <AlternatingItemTemplate>
            <tr id="Tr2" runat="server" style="background-color: #EBEEF2; text-align: center;">

                <td id="Td7" runat="server" style="text-align: left;">
                    <asp:HyperLink ID="LinkButton1" runat="server" NavigateUrl='<%#"~/ManagerUI/Menu/Content/EmailNotificationsEdit.aspx?id="+Eval("ID") %>'>
                        <asp:Label ID="Label3" runat="server" Text='<%#Eval("Name") %>' />
                    </asp:HyperLink>
                </td>
                
                <td id="Td1" runat="server" style="text-align: left;">
                    <asp:Label ID="lblDescription" runat="server" Text='<%# Server.HtmlEncode(Eval("Description").ToString()) %>' />
                </td>

                <td id="Td4" runat="server">
                    <asp:Label ID="lblTitle" runat="server" Text='<%# Server.HtmlEncode(Eval("Title").ToString()) %>' />
                </td>
                
                <td id="Td5" runat="server">
                    <asp:Label ID="Label1" runat="server" Text='<%#Eval("ChangeDate") %>' />
                </td>
            </tr>
        </AlternatingItemTemplate>
    </asp:ListView>
          <div class="infoBlock" style="margin-bottom: 0;">
            <div class="pager">
                <asp:Label ID="lblPage" runat="server">Страница:</asp:Label>
                <asp:DataPager ID="lvDataPager" runat="server" PagedControlID="lvAllEmailNotifications" PageSize="25">
                    <Fields>
                        <asp:NumericPagerField ButtonType="Link" />
                    </Fields>
                </asp:DataPager>  
            </div>
        </div>
    </div>
</asp:Content>
