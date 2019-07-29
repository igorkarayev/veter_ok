<%@ Page Title="" Language="C#" MasterPageFile="~/ManagerMasterPage.master" AutoEventWireup="true" CodeBehind="DriversView.aspx.cs" Inherits="Delivery.ManagerUI.Menu.Souls.DriversView" %>
<%@ Import Namespace="Delivery.BLL.Helpers" %>
<%@ Import Namespace="Delivery.BLL.StaticMethods" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <script type="text/javascript">
        $(function () {
            $("#<%= stbPhone.ClientID %>").mask("+375 (99) 999-99-99");
        });
   </script>

    <div>
        <asp:HyperLink ID="HyperLink1" runat="server" CssClass="createItemHyperLink" NavigateUrl="~/ManagerUI/Menu/Souls/DriversEdit.aspx">Добавить водителя</asp:HyperLink>
        <h3 class="h3custom" style="margin-top: 0;">Список водителей</h3>
    </div>
    
    <asp:Panel runat="server" ID="SearchPanel" CssClass="mainFont" DefaultButton="btnSearch">
        <table class="table searchPanel" style="margin-left:auto; margin-right: auto;">
            <tr>
                <td>
                    DID: <asp:TextBox runat="server" ID="stbDID" CssClass="searchField form-control" style="width: 25px;"></asp:TextBox>
                    Фам.: <asp:TextBox runat="server" ID="stbFirstName" CssClass="searchField form-control"></asp:TextBox> 
                    Тел.: <asp:TextBox runat="server" ID="stbPhone" CssClass="searchField form-control"  style="width: 140px;"></asp:TextBox>
                    Статус: <asp:DropDownList runat="server" ID="sddlStatus" CssClass="searchField ddl-control" Width="140px"></asp:DropDownList>
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
                        DID
                    </th>
                    <th>
                        ФИО
                    </th>
                    <th>
                        Номер ВУ
                    </th>
                    <th>
                        Автомобиль
                    </th>
                    <th>
                        Телефоны
                    </th>
                    <th>
                    </th>
                </tr>
                <tr runat="server" id="itemPlaceholder"></tr>
            </table>
        </LayoutTemplate>
        <ItemTemplate>
            <tr id="Tr2" runat="server" class='<%# String.Format("{0}", DriversHelper.DriverColoredStatusRows(Eval("StatusID").ToString()))%>' style="text-align: center;">
                <td id="Td7" runat="server">
                    <asp:Label ID="lblID" runat="server" Text='<%#Eval("ID") %>' />
                </td>

                <td id="Td2" runat="server" style="text-align: left;">
                    <asp:HyperLink runat="server" ID="hldriver" NavigateUrl='<%#"~/ManagerUI/Menu/Souls/DriverView.aspx?id="+Eval("ID") + "&"+ DriversHelper.BackDriverLinkBuilder(stbDID.Text, 
                            stbPhone.Text, sddlStatus.SelectedValue, stbFirstName.Text) %>'>
                        <asp:Label ID="lblName" runat="server" Text='<%#Eval("FirstName") %>' />&nbsp;
                        <asp:Label ID="Label2" runat="server" Text='<%#Eval("LastName") %>' />&nbsp;
                        <asp:Label ID="Label3" runat="server" Text='<%#Eval("ThirdName") %>' />&nbsp;
                    </asp:HyperLink>
                </td>
                
                <td id="Td1" runat="server">
                    <asp:Label ID="lblRegion" runat="server" Text='<%#Eval("DriverPassport") %>' />
                </td>

                <td id="Td4" runat="server">
                    <asp:HyperLink ID="LinkButton2" runat="server" NavigateUrl='<%#"~/ManagerUI/Menu/Souls/CarView.aspx?id="+Eval("CarID")%>'>
                        <asp:Label ID="lblTrack" runat="server" Text='<%#CarsHelper.CarIdToModelName(Eval("CarID").ToString()) %>' />
                    </asp:HyperLink>
                </td>
                
                 <td id="Td6" runat="server">
                    <asp:Label ID="Label1" runat="server" Text='<%#Eval("PhoneOne") %>' /><br/>
                    <asp:Label ID="Label4" runat="server" Text='<%#Eval("PhoneTwo") %>' />
                </td>
                
                <td id="Td3" runat="server"  style="font-size: 12px;">
                     <asp:HyperLink ID="LinkButton1" runat="server" NavigateUrl='<%#"~/ManagerUI/Menu/Souls/DriversEdit.aspx?id="+Eval("ID") + "&"+ DriversHelper.BackDriverLinkBuilder(stbDID.Text, 
                            stbPhone.Text, sddlStatus.SelectedValue, stbFirstName.Text) %>'>Изменить</asp:HyperLink><br/>
                     <asp:LinkButton ID="lbDelete" runat="server" OnClick="lbDelete_Click" OnClientClick="return confirmDelete();" CommandArgument='<%#Eval("ID") %>'>Удалить</asp:LinkButton>
                </td>
            </tr>
        </ItemTemplate>
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
