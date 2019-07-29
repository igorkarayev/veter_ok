<%@ Page Title="" Language="C#" MasterPageFile="~/ManagerMasterPage.master" AutoEventWireup="true" CodeBehind="CityView.aspx.cs" Inherits="Delivery.ManagerUI.Menu.Souls.CityView" %>
<%@ Import Namespace="Delivery.BLL" %>
<%@ Import Namespace="Delivery.BLL.Helpers" %>
<%@ Import Namespace="Delivery.BLL.StaticMethods" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <style>
        /* #234 */
         .autocomplete-suggestions {
             width: 700px !important;
             margin-left: -70px;
         }
    </style>
    <script type="text/javascript">
        $(function () {
            /** Autocomplete для городов СТАРТ **/
            $('#<%= stbCityName.ClientID%>').autocomplete({
                minChars: '2',
                params: {
                    appkey: '<%= AppKey%>'
                },
                serviceUrl: '../../../AppServices/AutocompliteAPI.asmx/GetCityForAutocompliteJSON',
                onSelect: function (suggestion) {
                    $('#<%= hfCityID.ClientID%>').val(suggestion.data);
                }
            });
            /** Autocomplete для городов КОНЕЦ **/
        });
    </script>
    <style type="text/css">
        .ui-autocomplete {
           
            font-size: 12px;
        }
    </style>
    <div>
        <asp:HyperLink ID="HyperLink1" runat="server" CssClass="createItemHyperLink" NavigateUrl="~/ManagerUI/Menu/Souls/CityEdit.aspx">Добавить населенный пункт</asp:HyperLink>
        <h3 class="h3custom" style="margin-top: 0;">Список населенных пунктов</h3>
    </div>
    <div>
        <asp:Panel runat="server" ID="SearchPanel" CssClass="mainFont" DefaultButton="btnSearch">
            <table class="table searchPanel"  style="margin-left:auto; margin-right: auto; width: 90%">
                <tr>
                    <td style="width: 210px;">
                        Район <asp:DropDownList runat="server" ID="sddlDistricts" CssClass="ddl-control" style="width: 150px;"></asp:DropDownList>
                    </td>
                    <td style="width:30px;">
                        Название:
                    </td>
                    <td>
                        <asp:TextBox runat="server" ID="stbCityName" CssClass="form-control"></asp:TextBox>
                        <asp:HiddenField runat="server" ID="hfCityID"/>
                    </td>
                    <td style="width:30px;">
                        &nbsp;
                    </td>
                    <td style="width:30px;">
                        <asp:Button runat="server" Text="Сброс" CssClass="btn btn-default" ID="btnReload"/>
                    </td>
                    <td style="width:30px;">
                        <asp:Button runat="server" Text="Искать" CssClass="btn btn-default" ID="btnSearch"/>    
                    </td>
                    <td>
                        <i class="small-informer">Найдено:<b> <asp:Label runat="server" ID="lblAllResult"></asp:Label></b></i> 
                    </td>
                </tr>
            </table>
        </asp:Panel><br/>
        <div style="font-size: 12px; font-style: italic; text-align: center">При выборе конкретного нас. пункта из выпадающего списка - вы найдете только его, однако при вводе неполного названия 
            без выбора из списка вам выдаст результат всех вхождений в названия населенных пунктов того, что написано в поисковой строке. Работает только при первом запросе после входа в это меню, либо первый раз после сброса</div><br/>
        <asp:ListView runat="server" ID="lvAllCity">
        <LayoutTemplate>
            <table runat="server" id="Table1" class="table tableViewClass tableClass">
                <tr style="background-color: #EECFBA">
                    <th>
                        SOATO
                    </th>
                    <th>
                        Населенный пункт
                    </th>
                    <th>
                        Область
                    </th>
                    <th>
                        Район
                    </th>
                    <th>
                        Направление
                    </th>
                    <th>
                        Расстояние (км.)
                    </th>
                    <th>
                        Расстояние до ближ. осн. (км.)
                    </th>
                    <th>
                        Осн.
                    </th>
                    <th>
                    </th>
                </tr>
                <tr runat="server" id="itemPlaceholder"></tr>
            </table>
        </LayoutTemplate>
        <ItemTemplate>
            <tr id="Tr2" runat="server" style="background-color: #F7F8F9; text-align: center;">
                <td id="Td10" runat="server">
                    <asp:Label ID="Label8" runat="server" Text='<%#Eval("SOATO") %>' />
                </td>

                <td id="Td2" runat="server">
                    <asp:Label ID="lblName" runat="server" Text='<%#Eval("Name") %>' />
                </td>
                
                <td id="Td1" runat="server">
                    <asp:Label ID="lblRegion" runat="server" Text='<%# CityHelper.RegionIDToRegionName(Convert.ToInt32(Eval("RegionID"))) %>' />
                </td>
                
                <td id="Td8" runat="server">
                    <asp:Label ID="Label3" runat="server" Text='<%# CityHelper.DistrictIDToDistrictName(Convert.ToInt32(Eval("DistrictID"))) %>' />
                </td>

                <td id="Td4" runat="server">
                    <asp:Label ID="lblTrack" runat="server" Text='<%# OtherMethods.TrackToText(Convert.ToInt32(Eval("TrackID"))) %>' />
                </td>

                <td id="Td7" runat="server">
                    <asp:Label ID="Label2" runat="server" Text='<%#Eval("Distance").ToString() %>' />
                </td>
                
                <td id="Td9" runat="server">
                    <asp:Label ID="Label7" runat="server" Text='<%#Eval("DistanceFromCity").ToString() %>' />
                </td>
                
                <td id="Td11" runat="server">
                    <asp:Label ID="Label5" runat="server" Text='<%# OtherMethods.CheckboxFlag(Eval("IsMainCity").ToString())%>' />
                </td>

                <td id="Td3" runat="server"  style="font-size: 12px;">
                     <asp:HyperLink ID="LinkButton1" runat="server" NavigateUrl='<%#"~/ManagerUI/Menu/Souls/CityEdit.aspx?id="+Eval("ID") %>'>Изменить</asp:HyperLink><br/>
                     <asp:LinkButton ID="lbDelete" runat="server" OnClick="lbDelete_Click" OnClientClick="return confirmDelete();" CommandArgument='<%#Eval("ID") %>'>Удалить</asp:LinkButton>
                </td>
            </tr>
        </ItemTemplate>
        <AlternatingItemTemplate>
            <tr id="Tr2" runat="server" style="background-color: #EBEEF2; text-align: center;">
                <td id="Td10" runat="server">
                    <asp:Label ID="Label8" runat="server" Text='<%#Eval("SOATO") %>' />
                </td>

                <td id="Td2" runat="server">
                    <asp:Label ID="lblName" runat="server" Text='<%#Eval("Name") %>' />
                </td>
                
                <td id="Td1" runat="server">
                    <asp:Label ID="lblRegion" runat="server" Text='<%# CityHelper.RegionIDToRegionName(Convert.ToInt32(Eval("RegionID"))) %>' />
                </td>
                
                <td id="Td8" runat="server">
                    <asp:Label ID="Label3" runat="server" Text='<%# CityHelper.DistrictIDToDistrictName(Convert.ToInt32(Eval("DistrictID"))) %>' />
                </td>

                <td id="Td4" runat="server">
                    <asp:Label ID="lblTrack" runat="server" Text='<%# OtherMethods.TrackToText(Convert.ToInt32(Eval("TrackID"))) %>' />
                </td>

                <td id="Td7" runat="server">
                    <asp:Label ID="Label2" runat="server" Text='<%#Eval("Distance").ToString() %>' />
                </td>
                
                <td id="Td9" runat="server">
                    <asp:Label ID="Label7" runat="server" Text='<%#Eval("DistanceFromCity").ToString() %>' />
                </td>
                
                <td id="Td11" runat="server">
                    <asp:Label ID="Label5" runat="server" Text='<%# OtherMethods.CheckboxFlag(Eval("IsMainCity").ToString())%>' />
                </td>

                <td id="Td3" runat="server"  style="font-size: 12px;">
                     <asp:HyperLink ID="LinkButton1" runat="server" NavigateUrl='<%#"~/ManagerUI/Menu/Souls/CityEdit.aspx?id="+Eval("ID") %>'>Изменить</asp:HyperLink><br/>
                     <asp:LinkButton ID="lbDelete" runat="server" OnClick="lbDelete_Click" OnClientClick="return confirmDelete();" CommandArgument='<%#Eval("ID") %>'>Удалить</asp:LinkButton>
                </td>
            </tr>
        </AlternatingItemTemplate>
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
