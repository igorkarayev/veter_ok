<%@ Page Title="" Language="C#" MasterPageFile="~/ManagerMasterPage.master" AutoEventWireup="true" CodeBehind="UsersDiscountView.aspx.cs" Inherits="Delivery.ManagerUI.Menu.Settings.UsersDiscountView" %>
<%@ Import Namespace="Delivery.AppServices" %>
<%@ Import Namespace="Delivery.BLL" %> <%@ Import Namespace="Delivery.BLL.StaticMethods" %>
<%@Register Tagprefix ="grb" Tagname="UserDiscount" src="~/ManagerUI/Controls/UserDiscount.ascx" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <script type="text/javascript">
        $(function () {
            $("#<%= stbRecipientPhone.ClientID %>").mask("+375 (99) 999-99-99");
        });
    </script>
   <h3 class="h3custom" style="margin-top: 0;">Скидки пользователей</h3>
    <div>
        <asp:Panel runat="server" ID="SearchPanel" CssClass="mainFont" DefaultButton="btnSearch">
            <table class="table searchPanel" style="margin-left:auto; margin-right: auto;">
                <tr>
                    <td>
                        
                        <table class="table">
                            <tr>
                                <td>
                                    UID:
                                </td>
                                <td>
                                    <asp:TextBox runat="server" ID="stbUID" CssClass="searchField form-control"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    Фам.:
                                </td>
                                <td>
                                    <asp:TextBox runat="server" ID="stbFamily" CssClass="searchField form-control"></asp:TextBox>
                                </td>
                            </tr>
                        </table>
                    </td>
                    <td>
                       <table class="table">
                            <tr>
                                <td>
                                    Тел.:
                                </td>
                                <td>
                                    <asp:TextBox runat="server" ID="stbRecipientPhone" CssClass="searchField form-control" Width="120px"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    Email:
                                </td>
                                <td>
                                    <asp:TextBox runat="server" ID="stbEmail" CssClass="searchField form-control" Width="120px"></asp:TextBox>
                                </td>
                            </tr>
                        </table>
                    </td>
                    <td>
                       <table class="table">
                            <tr>
                                <td>
                                    Статус:
                                </td>
                                <td>
                                    <asp:DropDownList runat="server" ID="sddlStatus" CssClass="searchField ddl-control" Width="140px"></asp:DropDownList>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    Курс:
                                </td>
                                <td>
                                    <asp:DropDownList runat="server" ID="sddlCourse" CssClass="searchField ddl-control" Width="140px"></asp:DropDownList>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td colspan="4" style="text-align: right;">
                        <asp:Button runat="server" Text="Сброс" CssClass="btn btn-default" ID="btnReload"/>
                        <asp:Button runat="server" Text="Искать" CssClass="btn btn-default" ID="btnSearch"/>    
                    </td>
                </tr>
            </table>
        </asp:Panel><br/>

        <asp:ListView runat="server" ID="lvAllUsers" DataKeyNames="ID" ClientIDMode="Predictable" 
        ClientIDRowSuffix="ID">
        <LayoutTemplate>
            <table runat="server" id="Table1" class="table tableViewClass tableClass">
                <tr class="table-header">
                    <th>
                        UID
                    </th>
                    <th>
                        Email
                    </th>
                    <th>
                        Имя
                    </th>
                    <th>
                        Фамилия
                    </th>
                    <th>
                        Скидка %
                    </th>
                </tr>
                <tr runat="server" id="itemPlaceholder"></tr>
            </table>
        </LayoutTemplate>
        <ItemTemplate>
            <tr id="Tr2" runat="server" class="table-item-template" style="text-align: center;">
                
                <td id="Td7" runat="server">
                    <asp:Label ID="lblID" runat="server" Text='<%# Eval("ID") %>' />
                </td>
                
                <td id="Td3" runat="server">
                    <asp:Label ID="Label1" runat="server" Text='<%# Eval("Email") %>' />
                </td>

                <td id="Td2" runat="server">
                    <asp:Label ID="lblName" runat="server" Text='<%#Eval("Name") %>' />
                </td>
                
                <td id="Td1" runat="server">
                    <asp:Label ID="lblRegion" runat="server" Text='<%#Eval("Family") %>' />
                </td>

                <td id="Td4" runat="server">
                    <grb:UserDiscount 
                            ID="UserDiscount" 
                            runat="server" 
                            UserID='<%# Eval("ID").ToString() %>'
                            UserDiscountValue='<%# Eval("Discount").ToString() %>'
                            PageName='UsersDiscountView'
                            ListViewControlFullID='#ctl00_MainContent_lvAllUsers'/>
                </td>
                
            </tr>
        </ItemTemplate>
        <AlternatingItemTemplate>
            <tr id="Tr2" runat="server" class="table-alternativ-item-template" style="text-align: center;">
                
                <td id="Td7" runat="server">
                    <asp:Label ID="lblID" runat="server" Text='<%# Eval("ID") %>' />
                </td>
                
                <td id="Td3" runat="server">
                    <asp:Label ID="Label1" runat="server" Text='<%# Eval("Email") %>' />
                </td>
                
                <td id="Td2" runat="server">
                    <asp:Label ID="lblName" runat="server" Text='<%#Eval("Name") %>' />
                </td>
                
                <td id="Td1" runat="server">
                    <asp:Label ID="lblRegion" runat="server" Text='<%#Eval("Family") %>' />
                </td>

                <td id="Td4" runat="server">
                    <grb:UserDiscount 
                            ID="UserDiscount" 
                            runat="server" 
                            UserID='<%# Eval("ID").ToString() %>'
                            UserDiscountValue='<%# Eval("Discount").ToString() %>'
                            PageName='UsersDiscountView'
                            ListViewControlFullID='#ctl00_MainContent_lvAllUsers'/>
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
            <asp:DataPager ID="lvDataPager" runat="server" PagedControlID="lvAllUsers" PageSize="25">
                <Fields>
                    <asp:NumericPagerField ButtonType="Link" />
                </Fields>
            </asp:DataPager>  
        </div>
    </div>
    </div>
</asp:Content>
