<%@ Page Title="" Language="C#" MasterPageFile="~/ManagerMasterPage.master" AutoEventWireup="true" CodeBehind="DistrictsView.aspx.cs" Inherits="Delivery.ManagerUI.Menu.Souls.DistrictsView" %>
<%@ Import Namespace="Delivery.BLL" %>
<%@ Import Namespace="Delivery.BLL.Helpers" %>
<%@ Import Namespace="Delivery.BLL.StaticMethods" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div>
        <h3 class="h3custom" style="margin-top: 0;">Список районов</h3>
    </div>
    <div>
        <asp:ListView runat="server" ID="lvAllCity">
            <LayoutTemplate>
                <table runat="server" id="Table1" class="table tableViewClass tableClass">
                    <tr style="background-color: #EECFBA">
                        <th>
                            Район
                        </th>
                        <th>
                            Напр.
                        </th>
                        <th>
                            Пн.
                        </th>
                        <th>
                            Вт.
                        </th>
                        <th>
                            Ср.
                        </th>
                        <th>
                            Чт.
                        </th>
                        <th>
                            Пт.
                        </th>
                        <th>
                            Сб.
                        </th>
                        <th>
                            Вс.
                        </th>
                        <th style="width: 70px;">
                            Срок доставки (дней)
                        </th>
                        <th>
                        </th>
                    </tr>
                    <tr runat="server" id="itemPlaceholder"></tr>
                </table>
            </LayoutTemplate>
            <ItemTemplate>
                <tr id="Tr2" runat="server" style="background-color: #F7F8F9; text-align: center;">
                    <td id="Td2" runat="server" style="padding-left: 20px; text-align: left;">
                        <asp:HyperLink runat="server" ID="hlCity" NavigateUrl='<%#"~/ManagerUI/Menu/Souls/CityView.aspx?district="+Eval("ID") %>'>
                            <asp:Label ID="lblName" runat="server" Text='<%#Eval("Name") %>' />
                        </asp:HyperLink>
                    </td>
                    
                    <td id="Td4" runat="server">
                        <asp:Label ID="Label1" runat="server" Text='<%# OtherMethods.TrackToText(Convert.ToInt32(Eval("TrackID"))) %>' />
                    </td>
                
                    <td id="Td1" runat="server">
                        <asp:Label ID="Label9" runat="server" Text='<%# OtherMethods.CheckboxFlag(Eval("Monday").ToString()) %>' />
                    </td>
                
                    <td id="Td12" runat="server">
                        <asp:Label ID="Label4" runat="server" Text='<%# OtherMethods.CheckboxFlag(Eval("Tuesday").ToString()) %>' />
                    </td>

                    <td id="Td13" runat="server">
                        <asp:Label ID="Label10" runat="server" Text='<%# OtherMethods.CheckboxFlag(Eval("Wednesday").ToString()) %>' />
                    </td>

                    <td id="Td14" runat="server">
                        <asp:Label ID="Label11" runat="server" Text='<%# OtherMethods.CheckboxFlag(Eval("Thursday").ToString()) %>' />
                    </td>

                    <td id="Td15" runat="server">
                        <asp:Label ID="Label12" runat="server" Text='<%# OtherMethods.CheckboxFlag(Eval("Friday").ToString()) %>' />
                    </td>

                    <td id="Td16" runat="server">
                        <asp:Label ID="Label13" runat="server" Text='<%# OtherMethods.CheckboxFlag(Eval("Saturday").ToString()) %>' />
                    </td>

                    <td id="Td17" runat="server">
                        <asp:Label ID="Label14" runat="server" Text='<%# OtherMethods.CheckboxFlag(Eval("Sunday").ToString()) %>' />
                    </td>

                    <td id="Td18" runat="server">
                        <asp:Label ID="Label15" runat="server" Text='<%#Eval("DeliveryTerms") %>' />
                    </td>

                    <td id="Td3" runat="server"  style="font-size: 12px;">
                         <asp:HyperLink ID="LinkButton1" runat="server" NavigateUrl='<%#"~/ManagerUI/Menu/Souls/DistrictEdit.aspx?id="+Eval("ID") %>'>Изменить</asp:HyperLink><br/>
                    </td>
                </tr>
            </ItemTemplate>
            <AlternatingItemTemplate>
                <tr id="Tr2" runat="server" style="background-color: #EBEEF2; text-align: center;">
                    <td id="Td2" runat="server" style="padding-left: 20px; text-align: left;">
                        <asp:HyperLink runat="server" ID="hlCity" NavigateUrl='<%#"~/ManagerUI/Menu/Souls/CityView.aspx?district="+Eval("ID") %>'>
                            <asp:Label ID="lblName" runat="server" Text='<%#Eval("Name") %>' />
                        </asp:HyperLink>
                    </td>
                
                    <td id="Td4" runat="server">
                        <asp:Label ID="Label1" runat="server" Text='<%# OtherMethods.TrackToText(Convert.ToInt32(Eval("TrackID"))) %>' />
                    </td>

                    <td id="Td1" runat="server">
                        <asp:Label ID="Label9" runat="server" Text='<%# OtherMethods.CheckboxFlag(Eval("Monday").ToString()) %>' />
                    </td>
                
                    <td id="Td12" runat="server">
                        <asp:Label ID="Label4" runat="server" Text='<%# OtherMethods.CheckboxFlag(Eval("Tuesday").ToString()) %>' />
                    </td>

                    <td id="Td13" runat="server">
                        <asp:Label ID="Label10" runat="server" Text='<%# OtherMethods.CheckboxFlag(Eval("Wednesday").ToString()) %>' />
                    </td>

                    <td id="Td14" runat="server">
                        <asp:Label ID="Label11" runat="server" Text='<%# OtherMethods.CheckboxFlag(Eval("Thursday").ToString()) %>' />
                    </td>

                    <td id="Td15" runat="server">
                        <asp:Label ID="Label12" runat="server" Text='<%# OtherMethods.CheckboxFlag(Eval("Friday").ToString()) %>' />
                    </td>

                    <td id="Td16" runat="server">
                        <asp:Label ID="Label13" runat="server" Text='<%# OtherMethods.CheckboxFlag(Eval("Saturday").ToString()) %>' />
                    </td>

                    <td id="Td17" runat="server">
                        <asp:Label ID="Label14" runat="server" Text='<%# OtherMethods.CheckboxFlag(Eval("Sunday").ToString()) %>' />
                    </td>

                    <td id="Td18" runat="server">
                        <asp:Label ID="Label15" runat="server" Text='<%#Eval("DeliveryTerms") %>' />
                    </td>

                    <td id="Td3" runat="server"  style="font-size: 12px;">
                         <asp:HyperLink ID="LinkButton1" runat="server" NavigateUrl='<%#"~/ManagerUI/Menu/Souls/DistrictEdit.aspx?id="+Eval("ID") %>'>Изменить</asp:HyperLink><br/>
                    </td>
                </tr>
            </AlternatingItemTemplate>
        </asp:ListView>
    </div>
</asp:Content>
