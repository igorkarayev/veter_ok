<%@ Page Title="" Language="C#" MasterPageFile="~/ManagerMasterPage.master" AutoEventWireup="true" CodeBehind="BackendView.aspx.cs" Inherits="Delivery.ManagerUI.Menu.Settings.BackendView" %>
<%@ Import Namespace="Delivery.BLL.StaticMethods" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <style>
        .tableViewClass td{
            text-align: left;
            padding: 6px 12px;
        }
    </style>
   <h3 class="h3custom" style="margin-top: 0;">Список настроек системы</h3>
    <div>
        <asp:Panel runat="server" ID="SearchPanel" CssClass="mainFont" DefaultButton="btnSearch">
            <table class="table searchPanel" style="margin-left:auto; margin-right: auto;">
                <tr>
                    <td>
                        <table class="table">
                            <tr>
                                <td>
                                    Тег:
                                </td>
                                <td>
                                    <asp:TextBox runat="server" ID="stbTag" CssClass="searchField form-control" Width="120px"></asp:TextBox>
                                </td>
                            </tr>
                        </table>
                    </td>
                    <td>
                        <table class="table">
                            <tr>
                                <td>
                                    Знач.:
                                </td>
                                <td>
                                    <asp:TextBox runat="server" ID="stbValue" CssClass="searchField form-control" Width="120px"></asp:TextBox>
                                </td>
                            </tr>
                        </table>
                    </td>
                    <td>
                        <table class="table">
                            <tr>
                                <td>
                                    Опис.:
                                </td>
                                <td>
                                    <asp:TextBox runat="server" ID="stbDescription" CssClass="searchField form-control" Width="120px"></asp:TextBox>
                                </td>
                            </tr>
                        </table>
                    </td>
                    <td style="text-align: right;">
                        <asp:Button runat="server" Text="Сброс" CssClass="btn btn-default" ID="btnReload"/>
                        <asp:Button runat="server" Text="Искать" CssClass="btn btn-default" ID="btnSearch"/>
                        &nbsp;&nbsp;&nbsp;<i class="small-informer">Найдено:<b> <asp:Label runat="server" ID="lblAllResult"></asp:Label></b></i>      
                    </td>
                </tr>
            </table>
        </asp:Panel><br/>

        <asp:ListView runat="server" ID="lvAllBackend">
        <LayoutTemplate>
            <table runat="server" id="Table1" class="table tableViewClass tableClass">
                <tr style="background-color: #EECFBA">
                    <th>
                        Тег
                    </th>
                    <th>
                        Значение
                    </th>
                    <th>
                        Описание
                    </th>
                    <th>
                        Изменен
                    </th>
                </tr>
                <tr runat="server" id="itemPlaceholder"></tr>
            </table>
        </LayoutTemplate>
        <ItemTemplate>
            <tr id="Tr2" runat="server" style="background-color: #F7F8F9; text-align: center;">
                <td id="Td5" runat="server">
                    <asp:HyperLink ID="HyperLink1" runat="server" NavigateUrl='<%# String.Format("~/ManagerUI/Menu/Settings/BackendEdit.aspx?id={0}&tag={1}&value={2}&description={3}", Eval("ID"), stbTag.Text,stbValue.Text,stbDescription.Text) %>'>
                        <asp:Label ID="Label1" runat="server" Text='<%#Eval("Tag") %>' />
                    </asp:HyperLink>
                </td>
                
                <td id="Td4" runat="server">
                    <asp:Label ID="lblTitle" runat="server" Text='<%# Eval("Value") %>' />
                </td>
                
                <td id="Td1" runat="server">
                    <asp:Label ID="lblDescription" runat="server" Text='<%# Eval("Description") %>' />
                </td>
                
                <td id="Td2" runat="server">
                    <asp:Label ID="Label2" runat="server" Text='<%# OtherMethods.DateConvert(Eval("ChangeDate").ToString()) %>' />
                </td>
            </tr>
        </ItemTemplate>
    </asp:ListView>
          <div class="infoBlock" style="margin-bottom: 0;">
            <div class="pager">
                <asp:Label ID="lblPage" runat="server">Страница:</asp:Label>
                <asp:DataPager ID="lvDataPager" runat="server" PagedControlID="lvAllBackend" PageSize="15">
                    <Fields>
                        <asp:NumericPagerField ButtonType="Link" />
                    </Fields>
                </asp:DataPager>  
            </div>
        </div>
    </div>
</asp:Content>
