<%@ Page Title="" Language="C#" MasterPageFile="~/ManagerMasterPage.master" AutoEventWireup="true" CodeBehind="NewsView.aspx.cs" Inherits="Delivery.ManagerUI.Menu.Content.NewsView" %>
<%@ Import Namespace="Delivery.BLL.Helpers" %>
<%@ Import Namespace="Delivery.BLL.StaticMethods" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div>
        <asp:HyperLink ID="HyperLink1" CssClass="createItemHyperLink" runat="server" NavigateUrl="~/ManagerUI/Menu/Content/NewsEdit.aspx">Добавить новость</asp:HyperLink>
        <h3 class="h3custom" style="margin-top: 0;">Список новостей</h3>
    </div>
        
    <div>
        <asp:ListView runat="server" ID="lvAllNews">
        <LayoutTemplate>
            <table runat="server" id="Table1" class="table tableViewClass tableClass">
                <tr style="background-color: #EECFBA">
                    <th>
                        Заголовок
                    </th>
                    <th>
                        Тип
                    </th>
                    <th>
                        Дата создания
                    </th>
                    <th>
                        Дата редактир.
                    </th>
                    <th style="width: 40px;">
                        В попап
                    </th>
                    <th>
                    </th>
                </tr>
                <tr runat="server" id="itemPlaceholder"></tr>
            </table>
        </LayoutTemplate>
        <ItemTemplate>
            <tr id="Tr2" runat="server" style="background-color: #F7F8F9; text-align: center;">

                <td id="Td4" runat="server" style="text-align: left; padding:0 15px;">
                    <asp:HyperLink ID="LinkButton1" runat="server" NavigateUrl='<%#"~/ManagerUI/Menu/Content/NewsEdit.aspx?id="+Eval("ID") %>'>
                        <asp:Label ID="lblTitle" runat="server" Text='<%#Server.HtmlEncode(Eval("Title").ToString()) %>' />
                    </asp:HyperLink>
                </td>

                <td id="Td5" runat="server">
                    <asp:Label ID="Label1" runat="server" Text='<%# NewsHelper.NewsTypeToText(Convert.ToInt32(Eval("NewsTypeID").ToString())) %>' />
                </td>
                
                <td id="Td2" runat="server">
                    <asp:Label ID="lblCreateDate" runat="server" Text='<%#Eval("CreateDate") %>' />
                </td>
                
                <td id="Td1" runat="server">
                    <asp:Label ID="lblChangeDate" runat="server" Text='<%#Eval("ChangeDate") %>' />
                </td>
                
                <td id="Td8" runat="server">
                    <asp:Label ID="Label2" runat="server" Text='<%# OtherMethods.CheckboxFlag(Eval("ForViewing").ToString())%>' />
                </td>

                <td id="Td3" runat="server"  style="text-align: right; font-size: 12px;">
                     <asp:LinkButton ID="lbDelete" runat="server" OnClick="lbDelete_Click" OnClientClick="return confirmDelete();" CommandArgument='<%#Eval("ID") %>'>Удалить</asp:LinkButton>
                </td>

            </tr>
        </ItemTemplate>
        <AlternatingItemTemplate>
            <tr id="Tr2" runat="server" style="background-color: #EBEEF2; text-align: center;">

                <td id="Td4" runat="server" style="text-align: left; padding:0 15px;">
                    <asp:HyperLink ID="LinkButton1" runat="server" NavigateUrl='<%#"~/ManagerUI/Menu/Content/NewsEdit.aspx?id="+Eval("ID") %>'>
                        <asp:Label ID="lblTitle" runat="server" Text='<%#Server.HtmlEncode(Eval("Title").ToString()) %>' />
                    </asp:HyperLink>
                </td>
                
                <td id="Td5" runat="server">
                    <asp:Label ID="Label1" runat="server" Text='<%# NewsHelper.NewsTypeToText(Convert.ToInt32(Eval("NewsTypeID").ToString())) %>' />
                </td>
                
                <td id="Td2" runat="server">
                    <asp:Label ID="lblCreateDate" runat="server" Text='<%#Eval("CreateDate") %>' />
                </td>
                
                <td id="Td1" runat="server">
                    <asp:Label ID="lblChangeDate" runat="server" Text='<%#Eval("ChangeDate") %>' />
                </td>
                
                <td id="Td8" runat="server">
                    <asp:Label ID="Label2" runat="server" Text='<%# OtherMethods.CheckboxFlag(Eval("ForViewing").ToString())%>' />
                </td>
                
                <td id="Td3" runat="server"  style="text-align: right; font-size: 12px;">
                     <asp:LinkButton ID="lbDelete" runat="server" OnClick="lbDelete_Click" OnClientClick="return confirmDelete();" CommandArgument='<%#Eval("ID") %>'>Удалить</asp:LinkButton>
                </td>
                
            </tr>
        </AlternatingItemTemplate>
    </asp:ListView>
         <div class="infoBlock" style="margin-bottom: 0;">
            <div class="pager">
                <asp:Label ID="lblPage" runat="server">Страница:</asp:Label>
                <asp:DataPager ID="lvDataPager" runat="server" PagedControlID="lvAllNews" PageSize="25">
                    <Fields>
                        <asp:NumericPagerField ButtonType="Link" />
                    </Fields>
                </asp:DataPager>  
            </div>
        </div>
    </div>
</asp:Content>
