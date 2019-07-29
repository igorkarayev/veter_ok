<%@ Page Title="" Language="C#" MasterPageFile="~/ManagerMasterPage.master" AutoEventWireup="true" CodeBehind="PagesView.aspx.cs" Inherits="Delivery.ManagerUI.Menu.Content.PagesView" %>
<%@ Import Namespace="Delivery.BLL.StaticMethods" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <asp:HyperLink ID="HyperLink1" CssClass="createItemHyperLink" runat="server" NavigateUrl="~/ManagerUI/Menu/Content/PagesEdit.aspx">Добавить страницу</asp:HyperLink>
    <h3 class="h3custom" style="margin-top: 0;">Список страниц с контентом</h3>
    <div>
        <asp:ListView runat="server" ID="lvAllPages">
        <LayoutTemplate>
            <table runat="server" id="Table1" class="table tableViewClass tableClass">
                <tr style="background-color: #EECFBA">
                    <th style="text-align: left;">
                        Имя страницы
                    </th>
                    <th style="text-align: left;">
                        Заголовок (Title)
                    </th>
                    <th>
                        Содержание
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
                <td id="Td2" runat="server" style="text-align: left;">
                    <asp:HyperLink ID="HyperLink1" runat="server" NavigateUrl='<%#"~/ManagerUI/Menu/Content/PagesEdit.aspx?id="+Eval("ID") %>'>
                        <asp:Label ID="lblChangeDate" runat="server" Text='<%#Eval("PageName") %>' />
                    </asp:HyperLink>
                </td>
                
                <td id="Td5" runat="server" style="text-align: left;">
                    <asp:HyperLink ID="HyperLink2" runat="server" NavigateUrl='<%#"~/ManagerUI/Menu/Content/PagesEdit.aspx?id="+Eval("ID") %>'>
                        <asp:Label ID="Label2" runat="server" Text='<%#Eval("PageTitle") %>' />
                    </asp:HyperLink>
                </td>
                
                <td id="Td1" runat="server">
                    <asp:Label ID="lblDescription" runat="server" Text='<%# Server.HtmlEncode(OtherMethods.AdminPageCutter(Eval("Content").ToString())) %>' />
                </td>
                
                <td id="Td4" runat="server">
                    <asp:Label ID="Label1" runat="server" Text='<%# Server.HtmlEncode(Eval("ChangeDate").ToString()) %>' />
                </td>
            </tr>
        </ItemTemplate>
        <AlternatingItemTemplate>
            <tr id="Tr2" runat="server" style="background-color: #EBEEF2; text-align: center;">
                <td id="Td3" runat="server" style="text-align: left;">
                    <asp:HyperLink ID="HyperLink1" runat="server" NavigateUrl='<%#"~/ManagerUI/Menu/Content/PagesEdit.aspx?id="+Eval("ID") %>'>
                        <asp:Label ID="lblChangeDate" runat="server" Text='<%#Eval("PageName") %>' />
                    </asp:HyperLink>
                </td>
                
                <td id="Td5" runat="server" style="text-align: left;">
                    <asp:HyperLink ID="HyperLink2" runat="server" NavigateUrl='<%#"~/ManagerUI/Menu/Content/PagesEdit.aspx?id="+Eval("ID") %>'>
                        <asp:Label ID="Label2" runat="server" Text='<%#Eval("PageTitle") %>' />
                    </asp:HyperLink>
                </td>
                
                <td id="Td1" runat="server">
                    <asp:Label ID="lblDescription" runat="server" Text='<%# Server.HtmlEncode(OtherMethods.AdminPageCutter(Eval("Content").ToString())) %>' />
                </td>
                
                <td id="Td4" runat="server">
                    <asp:Label ID="Label1" runat="server" Text='<%# Server.HtmlEncode(Eval("ChangeDate").ToString()) %>' />
                </td>
            </tr>
        </AlternatingItemTemplate>
    </asp:ListView>
          <div class="infoBlock" style="margin-bottom: 0;">
            <div class="pager">
                <asp:Label ID="lblPage" runat="server">Страница:</asp:Label>
                <asp:DataPager ID="lvDataPager" runat="server" PagedControlID="lvAllPages" PageSize="25">
                    <Fields>
                        <asp:NumericPagerField ButtonType="Link" />
                    </Fields>
                </asp:DataPager>  
            </div>
        </div>
    </div>
</asp:Content>
