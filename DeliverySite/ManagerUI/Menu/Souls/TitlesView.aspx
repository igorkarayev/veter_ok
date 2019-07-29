<%@ Page Title="" Language="C#" MasterPageFile="~/ManagerMasterPage.master" AutoEventWireup="true" CodeBehind="TitlesView.aspx.cs" Inherits="Delivery.ManagerUI.Menu.Souls.TitlesView" %>
<%@ Import Namespace="Delivery.BLL" %> 
<%@ Import Namespace="Delivery.BLL.StaticMethods" %> 
<%@ Import Namespace="Delivery.BLL.Helpers" %> 
<%@ Import Namespace="Delivery.WebServices.Objects" %>
<%@ Import Namespace="System.Collections.Generic" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div>
        <asp:HyperLink ID="HyperLink1" runat="server" CssClass="createItemHyperLink" NavigateUrl="~/ManagerUI/Menu/Souls/TitlesEdit.aspx">Добавить наименование</asp:HyperLink>
        <h3 class="h3custom" style="margin-top: 0;">Список наименований</h3>
    </div>
        
    <asp:Panel runat="server" ID="SearchPanel" CssClass="mainFont" DefaultButton="btnSearch">
        <table class="searchPanel" style="margin-left:auto; margin-right: auto;">
            <tr>
                <td>
                    Категория: <asp:DropDownList runat="server" ID="sddlCategory" CssClass="searchField ddl-control" Width="240px"></asp:DropDownList>
                    <asp:Button runat="server" Text="Сброс" CssClass="btn btn-default" ID="btnReload"/>
                    <asp:Button runat="server" Text="Искать" CssClass="btn btn-default" ID="btnSearch"/>  
                    &nbsp;&nbsp;&nbsp;<i class="small-informer">Найдено:<b> <asp:Label runat="server" ID="lblAllResult"></asp:Label></b></i> 
                </td>
            </tr>
        </table>
    </asp:Panel><br/>  

    <div>
        <asp:ListView runat="server" ID="lvAllTracks">
        <LayoutTemplate>
            <table runat="server" id="Table1" class="tableViewClass tableClass">
                <tr style="background-color: #EECFBA">
                    <th>
                        Наименование
                    </th>
                    
                    <th>
                        Категория
                    </th>
                    
                    <th>
                        Дверь / Дверь
                    </th>
                    
                    <th>
                        Склад / Дверь
                    </th>

                    <th style="width: 30px">
                        Нацен. коэфф.
                    </th>

                    <th style="width: 30px">
                        Может быть БУ
                    </th>
                    
                    <th style="width: 30px">
                        Доб. суммa БУ
                    </th>

                    <th style="width: 30px">
                        Мин. вес, кг
                    </th>

                    <th style="width: 30px">
                        Макс. вес, кг
                    </th>
                    <th style="width: 30px">
                        Прил.?
                    </th>
                    <th>
                    </th>
                </tr>
                <tr runat="server" id="itemPlaceholder"></tr>
            </table>
        </LayoutTemplate>
        <ItemTemplate>
            <tr id="Tr2" runat="server" style="background-color: #F7F8F9; text-align: center;">
                <td id="Td2" runat="server" style="text-align: left; padding-left: 10px;">
                    <asp:HyperLink ID="HyperLink2" runat="server" NavigateUrl='<%#"~/ManagerUI/Menu/Souls/TitlesEdit.aspx?id="+Eval("ID") %>'>
                        <asp:Label ID="lblName" runat="server" Text='<%#Eval("Name") %>' />
                    </asp:HyperLink>
                </td>
                
                <td id="Td1" runat="server" style="text-align: left; padding-left: 10px;">
                    <asp:Label ID="Label1" runat="server" Text='<%# CategoryHelper.IdToName(Eval("CategoryID").ToString()) %>' />
                </td>
                
                <td id="Td4" runat="server" style="text-align: left; padding-left: 10px;">
                    <asp:HiddenField runat="server" ID="hfCategoryName" Value='<%#Eval("Name") %>'/>
                    <asp:Label ID="lblDDPrice" runat="server" Text=''/>
                </td>
                
                <td id="Td6" runat="server" style="text-align: left; padding-left: 10px;">
                    <asp:Label ID="lblSDPrice" runat="server" Text=''/>
                    <asp:HiddenField runat="server" ID="hfWithoutAkciza" Value='<%#Eval("CanBeWithoutAkciza") %>'/>
                </td>

                <td id="Td5" runat="server">
                    <asp:Label ID="Label3" runat="server" Text='<%#Eval("MarginCoefficient") %>' />
                </td>
                
                <td id="Td11" runat="server">
                    <asp:Label ID="Label9" runat="server" Text='<%#OtherMethods.CheckboxView(Convert.ToInt32(Eval("CanBeWithoutAkciza"))) %>' />
                </td>
                
                <td id="Td7" runat="server">
                    <asp:Label ID="Label2" runat="server" Text='<%# MoneyMethods.MoneySeparator(Eval("AdditiveCostWithoutAkciza").ToString()) %>' />
                </td>

                <td id="Td8" runat="server">
                    <asp:Label ID="Label6" runat="server" Text='<%#Eval("WeightMin") %>' />
                </td>

                <td id="Td9" runat="server">
                    <asp:Label ID="Label7" runat="server" Text='<%#Eval("WeightMax") %>' />
                </td>
                
                <td id="Td10" runat="server">
                    <asp:Label ID="Label8" runat="server" Text='<%#OtherMethods.CheckboxView(Convert.ToInt32(Eval("Additive"))) %>' />
                </td>
               
                <td id="Td3" runat="server"  style="font-size: 12px; width: 100px;">
                     <asp:LinkButton ID="lbDelete" runat="server" OnClick="lbDelete_Click" OnClientClick="return confirmDelete();" CommandArgument='<%#Eval("ID") %>'>Удалить</asp:LinkButton>
                </td>
            </tr>
        </ItemTemplate>
        <AlternatingItemTemplate>
            <tr id="Tr2" runat="server" style="background-color: #EBEEF2; text-align: center;">
                <td id="Td2" runat="server" style="text-align: left; padding-left: 10px;">
                    <asp:HyperLink ID="LinkButton1" runat="server" NavigateUrl='<%#"~/ManagerUI/Menu/Souls/TitlesEdit.aspx?id="+Eval("ID") %>'>
                        <asp:Label ID="lblName" runat="server" Text='<%#Eval("Name") %>' />
                    </asp:HyperLink>
                </td>
                
                <td id="Td1" runat="server" style="text-align: left; padding-left: 10px;">
                    <asp:Label ID="Label1" runat="server" Text='<%# CategoryHelper.IdToName(Eval("CategoryID").ToString()) %>' />
                </td>
                
                <td id="Td4" runat="server" style="text-align: left; padding-left: 10px;">
                    <asp:HiddenField runat="server" ID="hfCategoryName" Value='<%#Eval("Name") %>'/>
                    <asp:Label ID="lblDdPrice" runat="server"/>
                </td>
                
                <td id="Td6" runat="server" style="text-align: left; padding-left: 10px;">
                    <asp:Label ID="lblSdPrice" runat="server" Text=''/><br/>
                    <asp:HiddenField runat="server" ID="hfWithoutAkciza" Value='<%#Eval("CanBeWithoutAkciza") %>'/>
                </td>

                <td id="Td5" runat="server">
                    <asp:Label ID="Label3" runat="server" Text='<%#Eval("MarginCoefficient") %>' />
                </td>
                
                <td id="Td11" runat="server">
                    <asp:Label ID="Label9" runat="server" Text='<%#OtherMethods.CheckboxView(Convert.ToInt32(Eval("CanBeWithoutAkciza"))) %>' />
                </td>
                
                <td id="Td7" runat="server">
                    <asp:Label ID="Label2" runat="server" Text='<%# MoneyMethods.MoneySeparator(Eval("AdditiveCostWithoutAkciza").ToString()) %>' />
                </td>
                
                <td id="Td8" runat="server">
                    <asp:Label ID="Label6" runat="server" Text='<%#Eval("WeightMin") %>' />
                </td>

                <td id="Td9" runat="server">
                    <asp:Label ID="Label7" runat="server" Text='<%#Eval("WeightMax") %>' />
                </td>
                
                <td id="Td10" runat="server">
                    <asp:Label ID="Label8" runat="server" Text='<%#OtherMethods.CheckboxView(Convert.ToInt32(Eval("Additive"))) %>' />
                </td>

                <td id="Td3" runat="server"  style="font-size: 12px; width: 100px;">
                     <asp:LinkButton ID="lbDelete" runat="server" OnClick="lbDelete_Click" OnClientClick="return confirmDelete();" CommandArgument='<%#Eval("ID") %>'>Удалить</asp:LinkButton>
                </td>
            </tr>
        </AlternatingItemTemplate>
    </asp:ListView>
        <div class="infoBlock" style="margin-bottom: 0;">
            <div class="pager">
                <asp:Label ID="lblPage" runat="server">Страница:</asp:Label>
                <asp:DataPager ID="lvDataPager" runat="server" PagedControlID="lvAllTracks" PageSize="1000" OnPreRender="lvDataPager_PreRender">
                    <Fields>
                        <asp:NumericPagerField ButtonType="Link" />
                    </Fields>
                </asp:DataPager>  
            </div>
        </div>
    </div>
</asp:Content>
