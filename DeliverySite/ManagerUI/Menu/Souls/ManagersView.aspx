<%@ Page Title="" Language="C#" MasterPageFile="~/ManagerMasterPage.master" AutoEventWireup="true" CodeBehind="ManagersView.aspx.cs" Inherits="Delivery.ManagerUI.Menu.Souls.ManagersView" %>
<%@ Import Namespace="Delivery.BLL" %> <%@ Import Namespace="Delivery.BLL.StaticMethods" %>
<%@ Import Namespace="Delivery.BLL" %> <%@ Import Namespace="Delivery.BLL.StaticMethods" %>
<%@ Import Namespace="Delivery.BLL.Helpers" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div>
        <asp:HyperLink ID="hlManagerCreate" CssClass="createItemHyperLink" runat="server" NavigateUrl="~/ManagerUI/Menu/Souls/ManagerEdit.aspx">Добавить сотрудника</asp:HyperLink>
        <h3 class="h3custom" style="margin-top: 0;">Список сотрудников</h3>
    </div>
        
    <div>
        <asp:Panel runat="server" ID="SearchPanel" CssClass="mainFont" DefaultButton="btnSearch">
            <div style="text-align: center;">Параметры выдачи для привязанных заявок </div>
            <table class="table searchPanel" style="margin-left:auto; margin-right: auto;">
                <tr>
                    <td>
                        с:&nbsp;<asp:TextBox runat="server" ID="stbDate1" CssClass="searchField form-control" Width="80px" EnableViewState="False"></asp:TextBox>
                        &nbsp;по:&nbsp;<asp:TextBox runat="server" ID="stbDate2" CssClass="searchField form-control" Width="80px" EnableViewState="False"></asp:TextBox>
                    </td>
                    <td style="text-align: right;">
                        <asp:Button runat="server" Text="Применить" CssClass="btn btn-default" ID="btnSearch"/>    
                    </td>
                </tr>
            </table>
        </asp:Panel><br/>

        <asp:ListView runat="server" ID="lvAllManager">
        <LayoutTemplate>
            <table runat="server" id="Table1" class="table tableViewClass tableClass">
                <tr style="background-color: #EECFBA">
                    <th>
                        ID
                    </th>
                    <th>
                        ФИО
                    </th>
                    <th>
                        Логин
                    </th>
                    <th>
                        Конт. инф.
                    </th>
                     <th>
                        Статус
                    </th>
                    <th>
                        Роль
                    </th>
                    <th>
                        IPWL
                    </th>
                    <th>
                        Привяз. клнт.
                    </th>
                    <th>
                        Привяз. заявок
                    </th>
                    <th>
                        Дата регистрации
                    </th>
                    <th>
                    </th>
                </tr>
                <tr runat="server" id="itemPlaceholder"></tr>
            </table>
        </LayoutTemplate>
        <ItemTemplate>
            <tr id="Tr2" runat="server" class='<%# String.Format("{0}", UsersHelper.UserColoredStatusRows(Eval("Status").ToString()))%>' style="text-align: center;">
                <td id="Td5" runat="server">
                    <asp:HyperLink ID="HyperLink1" runat="server" NavigateUrl='<%#"~/ManagerUI/Menu/Souls/ManagerView.aspx?id="+Eval("ID") %>'>
                        <asp:Label ID="lblID" runat="server" Text='<%# Eval("ID") %>' />
                    </asp:HyperLink>
                </td>
                
                <td id="Td1" runat="server">
                    <asp:HyperLink ID="HyperLink2" runat="server" NavigateUrl='<%#"~/ManagerUI/Menu/Souls/ManagerView.aspx?id="+Eval("ID") %>'>
                        <asp:Label ID="lblName" runat="server" Text='<%#Eval("Family") %>' />&nbsp;
                        <asp:Label ID="lblRegion" runat="server" Text='<%#Eval("Name") %>' />
                    </asp:HyperLink>
                </td>
                
                <td id="Td8" runat="server">
                    <asp:Label ID="Label4" runat="server" Text='<%#Eval("Login") %>' />
                </td>


                <td id="Td4" runat="server">
                    <asp:Label ID="lblTrack" runat="server" Text='<%#Eval("Email") %>' />
                    <div>
                        <i>л.н.:</i> <asp:Label CssClass="phoneMine" ID="Label8" runat="server" Text='<%#Eval("Phone") %>' />
                    </div>
                    <div>
                        <i>д.н.:</i> <asp:Label CssClass="phoneHome" ID="Label2" runat="server" Text='<%#Eval("PhoneHome") %>' />
                    </div>
                    <div>
                        <i>р.н1:</i> <asp:Label CssClass="phoneWorkOne" ID="Label3" runat="server" Text='<%#Eval("PhoneWorkOne") %>' />
                    </div>
                    <div>
                        <i>р.н2:</i> <asp:Label CssClass="phoneWorkTwo" ID="Label6" runat="server" Text='<%#Eval("PhoneWorkTwo") %>' />
                    </div>
                    <div>
                        <i>skype:</i> <asp:Label CssClass="skypeManager" ID="Label7" runat="server" Text='<%#Eval("Skype") %>' />
                    </div>
                </td>

                <td id="Td6" runat="server">
                    <asp:Label ID="Label1" runat="server" Text='<%# UsersHelper.UserStatusToText(Convert.ToInt32(Eval("Status")))%>' />
                </td>
                
                 <td id="Td7" runat="server">
                    <asp:Label ID="lblRole" runat="server" Text='<%# UsersHelper.RoleToRuss(Eval("Role").ToString())%>' />
                </td>
                
                <td id="Td11" runat="server">
                    <asp:Label ID="Label5" runat="server" Text='<%# OtherMethods.CheckboxFlag(Eval("AccessOnlyByWhiteList").ToString())%>' />
                </td>

                <td id="Td10" runat="server">
                    <asp:Label ID="lblLinkedClientsCount" runat="server" />
                </td>
                
                <td id="Td9" runat="server">
                    <asp:Label ID="lblLinkedUsersTicketCount" runat="server" />
                </td>

                <td id="Td0" runat="server">
                    <asp:Label ID="lblCreateDate" runat="server" Text='<%# Eval("CreateDate") %>' />
                </td>

                <td id="Td3" runat="server"  style="font-size: 12px;">
                     <asp:HiddenField runat="server" ID="hfRole" Value='<%#Eval("Role") %>'/>
                     <asp:HyperLink ID="lbEdit" runat="server" NavigateUrl='<%#"~/ManagerUI/Menu/Souls/ManagerEdit.aspx?id="+Eval("ID") %>'>Изменить</asp:HyperLink><br/>
                     <asp:LinkButton ID="lbDelete" runat="server" OnClick="lbDelete_Click" OnClientClick="return confirmDelete();" CommandArgument='<%#Eval("ID") %>'>Удалить</asp:LinkButton>
                </td>
            </tr>
        </ItemTemplate>
    </asp:ListView>
       <div class="infoBlock" style="margin-bottom: 0;">
            <div class="pager">
                <asp:Label ID="lblPage" runat="server">Страница:</asp:Label>
                <asp:DataPager ID="lvDataPager" runat="server" PagedControlID="lvAllManager" PageSize="25">
                    <Fields>
                        <asp:NumericPagerField ButtonType="Link" />
                    </Fields>
                </asp:DataPager>  
            </div>
        </div>
    </div>
    
    <script>
        $(function () {
            $("#<%= stbDate1.ClientID %>")
                .datepicker({ nextText: "", prevText: "", changeMonth: true, changeYear: true })
                .mask("99-99-9999");
            $("#<%= stbDate2.ClientID %>")
                .datepicker({ nextText: "", prevText: "", changeMonth: true, changeYear: true })
                .mask("99-99-9999");

            $(".phoneMine").each(function () {
                if ($(this).text().length == 0) {
                    $(this).parent("div").hide();
                }
            });

            $(".phoneHome").each(function() {
                if ($(this).text().length == 0) {
                    $(this).parent("div").hide();
                }
            });

            $(".phoneWorkOne").each(function () {
                if ($(this).text().length == 0) {
                    $(this).parent("div").hide();
                }
            });

            $(".phoneWorkTwo").each(function () {
                if ($(this).text().length == 0) {
                    $(this).parent("div").hide();
                }
            });

            $(".skypeManager").each(function () {
                if ($(this).text().length == 0) {
                    $(this).parent("div").hide();
                }
            });
        });
    </script>
</asp:Content>
