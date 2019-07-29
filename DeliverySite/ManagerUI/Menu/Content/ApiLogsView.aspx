<%@ Page Title="" Language="C#" MasterPageFile="~/ManagerMasterPage.master" AutoEventWireup="true" CodeBehind="ApiLogsView.aspx.cs" Inherits="Delivery.ManagerUI.Menu.Content.ApiLogsView" %>
<%@ Import Namespace="Delivery.BLL" %> <%@ Import Namespace="Delivery.BLL.StaticMethods" %>
<%@ Import Namespace="Delivery.BLL.Helpers" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <script>
        $(function () {
            $.datepicker.setDefaults($.datepicker.regional['ru']);
            $("#<%= stbCreateDate1.ClientID %>")
                .datepicker({ nextText: "", prevText: "", changeMonth: true, changeYear: true })
                .mask("99-99-9999");
            $("#<%= stbCreateDate2.ClientID %>")
                 .datepicker({ nextText: "", prevText: "", changeMonth: true, changeYear: true })
                 .mask("99-99-9999");
             $(".moneyMask").maskMoney();
         });
    </script>
    <div>
        <h3 class="h3custom" style="margin-top: 0;">Лог запросов к API</h3>
    </div>
    <div runat="server" ID="divOpenApi" Visible="False" style="text-align: center; color: red; font-size: 14px; font-weight: bold; padding-bottom: 15px;">
        API открыто. Статистика в логах не собирается. Для включения авторизации в API и сбора статистики измените back-end тег "allow_unauth_api_request" 
    </div>
    <div>
        <asp:Panel runat="server" ID="SearchPanel" CssClass="mainFont" DefaultButton="btnSearch">
            <table class="table searchPanel" style="margin-left:auto; margin-right: auto;">
                <tr>
                    <td>
                        <table class="table" style="margin-top: 10px;">
                            <tr>
                                <td>
                                    ID:
                                </td>
                                <td>
                                    <asp:TextBox runat="server" ID="stbID" CssClass="searchField form-control" Width="80px" AccessKey="c"></asp:TextBox>
                                </td>
                            </tr>
                             <tr>
                                <td>
                                    Создан с:
                                </td>
                                <td>
                                    <asp:TextBox runat="server" ID="stbCreateDate1" CssClass="searchField form-control" style="width: 80px;" EnableViewState="False"></asp:TextBox>
                                </td>
                            </tr>
                             <tr>
                                <td>
                                    Создан по:
                                </td>
                                <td>
                                    <asp:TextBox runat="server" ID="stbCreateDate2" CssClass="searchField form-control" style="width: 80px;" EnableViewState="False"></asp:TextBox>
                                </td>
                            </tr>
                        </table>
                    </td>

                    <td>
                        <table class="table" style="margin-top: 10px;">
                            <tr>
                                <td>
                                    UID:
                                </td>
                                <td>
                                    <asp:TextBox runat="server" ID="stbUID" CssClass="searchField form-control" AccessKey="c" EnableViewState="False"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    UIP:
                                </td>
                                <td>
                                    <asp:TextBox runat="server" ID="stbUIP" CssClass="searchField form-control" AccessKey="i" EnableViewState="False"></asp:TextBox>
                                </td>
                            </tr>
                            
                            <tr>
                                <td>
                                    ApiKey:
                                </td>
                                <td>
                                    <asp:TextBox runat="server" ID="stbApiKey" CssClass="searchField form-control" AccessKey="u" EnableViewState="False"></asp:TextBox>
                                </td>
                            </tr>
                        </table>
                    </td>
                        
                    <td>
                        <table class="table" style="margin-top: 10px;">
                            <tr>
                                <td>
                                     Тип API:
                                </td>
                                <td>
                                    <asp:DropDownList runat="server" ID="sddApiType" CssClass="searchField ddl-control"  Width="160px" AccessKey="d"></asp:DropDownList>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    Имя API:
                                </td>
                                <td>
                                    <asp:DropDownList runat="server" ID="sddApiName" CssClass="searchField ddl-control"  Width="160px" AccessKey="d"></asp:DropDownList>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    Обработчик:
                                </td>
                                <td>
                                    <asp:DropDownList runat="server" ID="sddMethodName" CssClass="searchField ddl-control"  Width="160px" AccessKey="d"></asp:DropDownList>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td colspan="4" style="text-align: right;">
                        <asp:Button runat="server" Text="Сброс" CssClass="btn btn-default" ID="btnReload"/>
                        <asp:Button runat="server" Text="Искать" CssClass="btn btn-default" ID="btnSearch"/> 
                        &nbsp;&nbsp;&nbsp;<i class="small-informer">Найдено:<b> <asp:Label runat="server" ID="lblAllResult"></asp:Label></b></i>    
                    </td>
                </tr>
            </table>
        </asp:Panel>
        
        <span style="font-size: 10px; display: inline-block; margin: 0px 0 0 13px;">статистика за выбранный период</span>
        <div class="searchPanel" style="margin: 0px 0 15px 0; font-size: 12px; padding: 10px;">
            <table class="table" style="width: 100%;">
                <tr>
                    <td style="vertical-align: top; width: 37%">
                        <table class="table">
                            <tr>
                                <td style="padding-right: 10px;">
                                    Кол-во клиентов API
                                </td>
                                <td style="font-weight: bold;">
                                    <asp:Label runat="server" ID="lblUsersCount"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td style="vertical-align: top">
                                    Общий размер ответов
                                </td>
                                <td>
                                    <asp:Label runat="server" ID="lblInfoCount"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    Использовано ключей
                                </td>
                                <td style="font-weight: bold;">
                                    <asp:Label runat="server" ID="lblApiKeysCount"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    Общее кол-во запросов
                                </td>
                                <td style="font-weight: bold;">
                                    <asp:Label runat="server" ID="lblQueriesCount"></asp:Label>
                                </td>
                            </tr>
                        </table>
                    </td>
                    <td style="width: 2%">
                        &nbsp;
                    </td>
                    <td style="vertical-align: top; width: 28%">
                        <i>Топ-4 клиентов по кол-ву запросов</i>
                        <ol style="margin-bottom: 0">
                            <li>
                                <asp:Label runat="server" ID="lblFirstByQuery"></asp:Label>
                            </li>
                            <li>
                                <asp:Label runat="server" ID="lblSecondByQuery"></asp:Label>
                            </li>
                            <li>
                                <asp:Label runat="server" ID="lblThirdByQuery"></asp:Label>
                            </li>
                            <li>
                                <asp:Label runat="server" ID="lblFourthByQuery"></asp:Label>
                            </li>
                        </ol>
                    </td>
                    <td style="width: 2%">
                        &nbsp;
                    </td>
                    <td style="vertical-align: top; width: 28%">
                        <i>Топ-4 клиентов разм. ответов</i>
                        <ol style="margin-bottom: 0">
                            <li>
                                <asp:Label runat="server" ID="lblFirstByInfo"></asp:Label>
                            </li>
                            <li>
                                <asp:Label runat="server" ID="lblSecondByInfo"></asp:Label>
                            </li>
                            <li>
                                <asp:Label runat="server" ID="lblThirdByInfo"></asp:Label>
                            </li>
                            <li>
                                <asp:Label runat="server" ID="lblFourthByInfo"></asp:Label>
                            </li>
                        </ol> 
                    </td>
                </tr>
            </table>
        </div>
                
        <asp:ListView runat="server" ID="lvAllErrors">
        <LayoutTemplate>
            <table runat="server" id="Table1" class="table tableViewClass tableClass">
                <tr style="background-color: #EECFBA">
                    <th>
                        Дата
                    </th>
                    <th>
                        Кто
                    </th>
                    <th>
                        Тип API
                    </th>
                    <th>
                        Имя API
                    </th>
                    <th>
                        Обработчик
                    </th>
                    <th>
                        Размер ответа
                    </th>
                    <th>
                        Ключ запроса
                    </th>
                    <th style="width: 150px;">
                        Вх. параметры
                    </th>
                </tr>
                <tr runat="server" id="itemPlaceholder"></tr>
            </table>
        </LayoutTemplate>
        <ItemTemplate>
            <tr id="Tr2" runat="server" style="background-color: #F7F8F9; text-align: center;">
                <td id="Td1" runat="server">
                    <asp:Label ID="lblRegion" runat="server" Text='<%# Eval("CreateDate") %>' />
                </td>
                
                <td id="Td7" runat="server">
                    <asp:HyperLink runat="server" NavigateUrl='<%# String.Format("~/ManagerUI/Menu/Souls/ClientEdit.aspx?id={0}",Eval("UserID").ToString()) %>'>
                        <asp:Label ID="lblCost" runat="server" Text='<%# UsersHelper.UserIDToFullName(Eval("UserID").ToString()) %>' />
                    </asp:HyperLink><br/>
                    <span style="font-size: 10px; font-style: italic;">(ip: <%#Eval("UserIP")%>)</span>
                </td>
                
                <td id="Td3" runat="server">
                    <asp:Label ID="Label5" runat="server" Text='<%# Eval("ApiType") %>' />
                </td>
                
                <td id="Td4" runat="server">
                    <asp:Label ID="Label7" runat="server" Text='<%# Eval("ApiName") %>' />
                </td>
                
                <td id="Td6" runat="server">
                    <asp:Label ID="Label1" runat="server" Text='<%# Eval("MethodName") %>' />
                </td>
                
                <td id="Td5" runat="server">
                    <asp:Label ID="Label3" runat="server" Text='<%# Eval("ResponseBodyLenght") %>' />
                </td>
                
                <td id="Td8" runat="server">
                    <asp:Label ID="Label4" runat="server" Text='<%# Eval("ApiKey") %>' />
                </td>

                <td id="Td2" runat="server">
                    <div style="width: 150px; overflow: auto">
                        <asp:Label ID="Label2" runat="server" Text='<%# Eval("IncomingParameters") %>' />
                    </div>
                </td>
            </tr>
        </ItemTemplate>
        <AlternatingItemTemplate>
            <tr id="Tr2" runat="server" style="background-color: #EBEEF2; text-align: center;">
                <td id="Td1" runat="server">
                    <asp:Label ID="lblRegion" runat="server" Text='<%# Eval("CreateDate") %>' />
                </td>
                
                <td id="Td7" runat="server">
                    <asp:HyperLink runat="server" NavigateUrl='<%# String.Format("~/ManagerUI/Menu/Souls/ClientEdit.aspx?id={0}",Eval("UserID").ToString()) %>'>
                        <asp:Label ID="lblCost" runat="server" Text='<%# UsersHelper.UserIDToFullName(Eval("UserID").ToString()) %>' />
                    </asp:HyperLink><br/>
                    <span style="font-size: 10px; font-style: italic;">(ip: <%#Eval("UserIP")%>)</span>
                </td>
                
                <td id="Td3" runat="server">
                    <asp:Label ID="Label5" runat="server" Text='<%# Eval("ApiType") %>' />
                </td>
                
                <td id="Td4" runat="server">
                    <asp:Label ID="Label7" runat="server" Text='<%# Eval("ApiName") %>' />
                </td>
                
                <td id="Td6" runat="server">
                    <asp:Label ID="Label1" runat="server" Text='<%# Eval("MethodName") %>' />
                </td>
                
                <td id="Td5" runat="server">
                    <asp:Label ID="Label3" runat="server" Text='<%# Eval("ResponseBodyLenght") %>' />
                </td>
                
                <td id="Td8" runat="server">
                    <asp:Label ID="Label4" runat="server" Text='<%# Eval("ApiKey") %>' />
                </td>

                <td id="Td2" runat="server">
                    <div style="width: 150px; overflow: auto">
                        <asp:Label ID="Label2" runat="server" Text='<%# Eval("IncomingParameters") %>' />
                    </div>
                </td>
            </tr>
        </AlternatingItemTemplate>
        <EmptyDataTemplate>
            <div style="width: 100%; text-align: center; padding: 15px; color: red">По данным критериям ничего не найдено...</div>
        </EmptyDataTemplate>
    </asp:ListView>
    <div class="infoBlock" style="margin-bottom: 0;">
            <div class="pager">
                <asp:Label ID="lblPage" Visible="True" runat="server">Страница:</asp:Label>
                <asp:DataPager ID="lvDataPager" Visible="True" runat="server" PagedControlID="lvAllErrors" PageSize="25">
                    <Fields>
                        <asp:NumericPagerField ButtonType="Link" />
                    </Fields>
                </asp:DataPager>  
            </div>
        </div>
    </div>
</asp:Content>
