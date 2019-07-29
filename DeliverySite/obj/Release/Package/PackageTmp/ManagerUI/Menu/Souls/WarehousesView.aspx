<%@ Page Title="" Language="C#" MasterPageFile="~/ManagerMasterPage.master" AutoEventWireup="true" CodeBehind="WarehousesView.aspx.cs" Inherits="Delivery.ManagerUI.Menu.Souls.WarehousesView" %>
<%@ Import Namespace="Delivery.BLL.Helpers" %>
<%@ Import Namespace="Delivery.BLL.StaticMethods" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div>
        <asp:HyperLink ID="HyperLink1" runat="server" CssClass="createItemHyperLink" NavigateUrl="~/ManagerUI/Menu/Souls/WarehouseEdit.aspx">Добавить склад</asp:HyperLink>
        <h3 class="h3custom" style="margin-top: 0;">Список складов</h3>
    </div>  

    <div>
        <asp:ListView runat="server" ID="lvAllCars">
        <LayoutTemplate>
            <table runat="server" id="Table1" class="tableViewClass tableClass">
                <tr style="background-color: #EECFBA">
                    <th style="width: 200px;">
                        Название
                    </th>
                    <th style="width: 300px;">
                        Адрес
                    </th>
                    <th>
                        Дата создания
                    </th>
                    <th>
                        Дата изменения
                    </th>
                    <th>
                    </th>
                </tr>
                <tr runat="server" id="itemPlaceholder"></tr>
            </table>
        </LayoutTemplate>
        <ItemTemplate>
            <tr id="Tr2" runat="server" style="background-color: #F7F8F9; text-align: center;">
                <td id="Td7" runat="server">
                    <asp:Label ID="lblID" runat="server" Text='<%#Eval("Name") %>' />
                    <asp:HiddenField ID="hfWarehouseId" runat="server" Value='<%# Eval("ID") %>'/>
                </td>

                <td id="Td2" runat="server" style="text-align: left;">
                    <asp:Label runat="server" ID="lblAddress"><%# WarehousesHelper.WarehouseAddress(Eval("ID").ToString()) %></asp:Label>
                </td>
                
                <td id="Td1" runat="server">
                    <asp:Label ID="lblRegion" runat="server" Text='<%#Eval("CreateDate") %>' />
                </td>
                
                <td id="Td5" runat="server">
                    <asp:Label ID="lblCost" runat="server" Text='<%#Eval("ChangeDate") %>' />
                </td>
                
                <td id="Td3" runat="server"  style="font-size: 12px;">
                     <asp:HyperLink ID="LinkButton1" runat="server" NavigateUrl='<%#"~/ManagerUI/Menu/Souls/WarehouseEdit.aspx?id="+Eval("ID")%>'>Изменить</asp:HyperLink><br/>
                     <asp:LinkButton ID="lbDelete" runat="server" OnClick="lbDelete_Click" OnClientClick="return confirmDelete();" CommandArgument='<%#Eval("ID") %>'>Удалить</asp:LinkButton>
                </td>
            </tr>
        </ItemTemplate>
        <AlternatingItemTemplate>
            <tr id="Tr2" runat="server" style="background-color: #EBEEF2; text-align: center;">
                <td id="Td7" runat="server">
                    <asp:Label ID="lblID" runat="server" Text='<%#Eval("Name") %>' />
                    <asp:HiddenField ID="hfWarehouseId" runat="server" Value='<%# Eval("ID") %>'/>
                </td>

                <td id="Td2" runat="server" style="text-align: left;">
                    <asp:Label runat="server" ID="lblAddress"><%# WarehousesHelper.WarehouseAddress(Eval("ID").ToString()) %></asp:Label>
                </td>
                
                <td id="Td1" runat="server">
                    <asp:Label ID="lblRegion" runat="server" Text='<%#Eval("CreateDate") %>' />
                </td>
                
                <td id="Td5" runat="server">
                    <asp:Label ID="lblCost" runat="server" Text='<%#Eval("ChangeDate") %>' />
                </td>
                
                <td id="Td3" runat="server"  style="font-size: 12px;">
                     <asp:HyperLink ID="LinkButton1" runat="server" NavigateUrl='<%#"~/ManagerUI/Menu/Souls/WarehouseEdit.aspx?id="+Eval("ID")%>'>Изменить</asp:HyperLink><br/>
                     <asp:LinkButton ID="lbDelete" runat="server" OnClick="lbDelete_Click" OnClientClick="return confirmDelete();" CommandArgument='<%#Eval("ID") %>'>Удалить</asp:LinkButton>
                </td>
            </tr>
        </AlternatingItemTemplate>
    </asp:ListView>
        <div class="infoBlock" style="margin-bottom: 0;">
            <div class="pager">
                <asp:Label ID="lblPage" runat="server">Страница:</asp:Label>
                <asp:DataPager ID="lvDataPager" runat="server" PagedControlID="lvAllCars" PageSize="25">
                    <Fields>
                        <asp:NumericPagerField ButtonType="Link" />
                    </Fields>
                </asp:DataPager>  
            </div>
        </div>
    </div>
</asp:Content>
