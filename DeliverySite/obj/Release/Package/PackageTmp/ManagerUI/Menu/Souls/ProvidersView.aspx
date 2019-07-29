<%@ Page Title="" Language="C#" MasterPageFile="~/ManagerMasterPage.master" AutoEventWireup="true" CodeBehind="ProvidersView.aspx.cs" Inherits="Delivery.ManagerUI.Menu.Souls.ProvidersView" %>
<%@ Import Namespace="Delivery.BLL.Helpers" %>
<%@ Import Namespace="Delivery.BLL.StaticMethods" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <script type="text/javascript">
        $(function () {
            $("#<%= stbContactPhone.ClientID %>").mask("+375 (99) 999-99-99");
        });
   </script>

    <div>
        <asp:HyperLink ID="HyperLink1" runat="server" CssClass="createItemHyperLink" NavigateUrl="~/ManagerUI/Menu/Souls/ProviderEdit.aspx">Добавить поставщика</asp:HyperLink>
        <h3 class="h3custom" style="margin-top: 0;">Список поставщиков</h3>
    </div>
    
    <asp:Panel runat="server" ID="SearchPanel" CssClass="mainFont" DefaultButton="btnSearch">
        <table class="table searchPanel" style="margin-left:auto; margin-right: auto;">
            <tr>
                <td>
                    ID: <asp:TextBox runat="server" ID="stbDID" CssClass="searchField form-control" style="width: 25px;"></asp:TextBox>
                    ФС: <asp:DropDownList runat="server" ID="sddlNamePrefix" CssClass="searchField ddl-control" Width="90px"></asp:DropDownList>
                    Название: <asp:TextBox runat="server" ID="stbFirstName" CssClass="searchField form-control" style="width: 325px;" ></asp:TextBox> 
                    Тел.: <asp:TextBox runat="server" ID="stbContactPhone" CssClass="searchField form-control"  style="width: 140px;"></asp:TextBox>
                </td>
            </tr>
            
            <tr>
                <td>
                    Продукция: <asp:TextBox runat="server" ID="stbTypesOfProducts" CssClass="searchField form-control" style="width: 455px;"></asp:TextBox>
                    <asp:Button runat="server" Text="Сброс" CssClass="btn btn-default" ID="btnReload"/>
                    <asp:Button runat="server" Text="Искать" CssClass="btn btn-default" ID="btnSearch"/>  
                    &nbsp;&nbsp;&nbsp;<i class="small-informer">Найдено:<b> <asp:Label runat="server" ID="lblAllResult"></asp:Label></b></i> 
                </td>
            </tr>
        </table>
    </asp:Panel><br/>    

    <div>
        <asp:ListView runat="server" ID="lvAllDrivers">
        <LayoutTemplate>
            <table runat="server" id="Table1" class="table tableViewClass tableClass">
                <tr style="background-color: #EECFBA">
                    <th>
                        ID
                    </th>
                    <th style="width: 160px;">
                        Название
                    </th>
                    <th style="width: 160px;">
                        Контакты
                    </th>
                    <th>
                        Список продукции
                    </th>
                    <th>
                        Адрес
                    </th>
                    <th>
                        Комментарий
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
                    <asp:Label ID="lblID" runat="server" Text='<%#Eval("ID") %>' />
                </td>

                <td id="Td2" runat="server" style="text-align: left;">
                    <asp:HyperLink runat="server" ID="hldriver" NavigateUrl='<%# "~/ManagerUI/Menu/Souls/ProviderEdit.aspx?id="+Eval("ID") %>'>
                        <asp:Label ID="lblName" runat="server" Text='<%# ProvidersHelper.ProviderNamePrefixToText(Convert.ToInt32(Eval("NamePrefix"))) %>' />&nbsp;
                        «<asp:Label ID="Label2" runat="server" Text='<%#Eval("Name") %>' />»
                    </asp:HyperLink>
                </td>
                
                <td id="Td6" runat="server">
                    <asp:Label ID="Label1" runat="server" Text='<%#Eval("ContactFIO") %>' /><br/>
                    <asp:Label ID="Label4" runat="server" Text='<%#Eval("ContactPhone") %>' />
                </td>
                
                <td id="Td1" runat="server">
                    <asp:Label ID="lblRegion" runat="server" Text='<%#Eval("TypesOfProducts") %>' />
                </td>

                <td id="Td4" runat="server">
                    <asp:Label ID="Label3" runat="server" Text='<%# CityHelper.CityIDToCityName(Eval("CityID").ToString()) %>' />,&nbsp;
                    <asp:Label ID="Label5" runat="server" Text='<%#Eval("Address") %>' />
                </td>
                
                <td id="Td5" runat="server">
                    <asp:Label ID="Label6" runat="server" Text='<%#Eval("Note") %>' />
                </td>
                
                <td id="Td3" runat="server"  style="font-size: 12px;">
                     <asp:HyperLink ID="LinkButton1" runat="server" NavigateUrl='<%#"~/ManagerUI/Menu/Souls/ProviderEdit.aspx?id="+Eval("ID") %>'>Изменить</asp:HyperLink><br/>
                     <asp:LinkButton ID="lbDelete" runat="server" OnClick="lbDelete_Click" OnClientClick="return confirmDelete();" CommandArgument='<%#Eval("ID") %>'>Удалить</asp:LinkButton>
                </td>
            </tr>
        </ItemTemplate>
        <AlternatingItemTemplate>
			<tr id="Tr2" runat="server" style="background-color: #EBEEF2; text-align: center;">
                <td id="Td7" runat="server">
                    <asp:Label ID="lblID" runat="server" Text='<%#Eval("ID") %>' />
                </td>

                <td id="Td2" runat="server" style="text-align: left;">
                    <asp:HyperLink runat="server" ID="hldriver" NavigateUrl='<%# "~/ManagerUI/Menu/Souls/ProviderEdit.aspx?id="+Eval("ID") %>'>
                        <asp:Label ID="lblName" runat="server" Text='<%# ProvidersHelper.ProviderNamePrefixToText(Convert.ToInt32(Eval("NamePrefix"))) %>' />&nbsp;
                        «<asp:Label ID="Label2" runat="server" Text='<%#Eval("Name") %>' />»
                    </asp:HyperLink>
                </td>
                
                <td id="Td6" runat="server">
                    <asp:Label ID="Label1" runat="server" Text='<%#Eval("ContactFIO") %>' /><br/>
                    <asp:Label ID="Label4" runat="server" Text='<%#Eval("ContactPhone") %>' />
                </td>
                
                <td id="Td1" runat="server">
                    <asp:Label ID="lblRegion" runat="server" Text='<%#Eval("TypesOfProducts") %>' />
                </td>

                <td id="Td4" runat="server">
                    <asp:Label ID="Label3" runat="server" Text='<%# CityHelper.CityIDToCityName(Eval("CityID").ToString()) %>' />,&nbsp;
                    <asp:Label ID="Label5" runat="server" Text='<%#Eval("Address") %>' />
                </td>
                
                <td id="Td5" runat="server">
                    <asp:Label ID="Label6" runat="server" Text='<%#Eval("Note") %>' />
                </td>
                
                <td id="Td3" runat="server"  style="font-size: 12px;">
                     <asp:HyperLink ID="LinkButton1" runat="server" NavigateUrl='<%#"~/ManagerUI/Menu/Souls/ProviderEdit.aspx?id="+Eval("ID") %>'>Изменить</asp:HyperLink><br/>
                     <asp:LinkButton ID="lbDelete" runat="server" OnClick="lbDelete_Click" OnClientClick="return confirmDelete();" CommandArgument='<%#Eval("ID") %>'>Удалить</asp:LinkButton>
                </td>
            </tr>
        </AlternatingItemTemplate>
    </asp:ListView>
        <div class="infoBlock" style="margin-bottom: 0;">
            <div class="pager">
                <asp:Label ID="lblPage" runat="server">Страница:</asp:Label>
                <asp:DataPager ID="lvDataPager" runat="server" PagedControlID="lvAllDrivers" PageSize="25">
                    <Fields>
                        <asp:NumericPagerField ButtonType="Link" />
                    </Fields>
                </asp:DataPager>  
            </div>
        </div>
    </div>
</asp:Content>
