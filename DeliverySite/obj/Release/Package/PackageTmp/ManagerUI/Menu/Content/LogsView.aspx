<%@ Page Title="" Language="C#" MasterPageFile="~/ManagerMasterPage.master" AutoEventWireup="true" CodeBehind="LogsView.aspx.cs" Inherits="Delivery.ManagerUI.Menu.Content.LogsView" %>
<%@ Import Namespace="Delivery.BLL" %> <%@ Import Namespace="Delivery.BLL.StaticMethods" %>
<%@ Import Namespace="Delivery.BLL.Helpers" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <script>
        $(function () {
            $.datepicker.setDefaults($.datepicker.regional['ru']);
            $("#<%= stbChangeDate1.ClientID %>")
                .datepicker({ nextText: "", prevText: "", changeMonth: true, changeYear: true })
                .mask("99-99-9999");
            $("#<%= stbChangeDate2.ClientID %>")
                 .datepicker({ nextText: "", prevText: "", changeMonth: true, changeYear: true })
                 .mask("99-99-9999");
             $(".moneyMask").maskMoney();
         });
    </script>
    <div>
        <h3 class="h3custom" style="margin-top: 0;">Лог действий пользователей</h3>
    </div>
    <div>
        <asp:Panel runat="server" ID="SearchPanel" CssClass="mainFont" DefaultButton="btnSearch">
            <table class="table searchPanel" style="margin-left:auto; margin-right: auto;">
                <tr>
                    <td>
                        <table class="table" style="margin-top: 10px;">
                            <tr>
                                <td>
                                    Кем изменено:
                                </td>
                                <td>
                                    <asp:DropDownList runat="server" ID="sddUsers" CssClass="searchField ddl-control" Width="160px" AccessKey="c"></asp:DropDownList>
                                </td>
                            </tr>
                             <tr>
                                <td>
                                    Изменен с:
                                </td>
                                <td>
                                    <asp:TextBox runat="server" ID="stbChangeDate1" CssClass="searchField form-control" style="width: 80px;" EnableViewState="False"></asp:TextBox>
                                </td>
                            </tr>
                             <tr>
                                <td>
                                    Изменен по:
                                </td>
                                <td>
                                    <asp:TextBox runat="server" ID="stbChangeDate2" CssClass="searchField form-control" style="width: 80px;" EnableViewState="False"></asp:TextBox>
                                </td>
                            </tr>
                        </table>
                    </td>

                    <td>
                        <table class="table" style="margin-top: 10px;">
                            
                            <tr>
                                <td>
                                    TID:
                                </td>
                                <td>
                                    <asp:TextBox runat="server" ID="stbTID" CssClass="searchField form-control" AccessKey="i" EnableViewState="False"></asp:TextBox>
                                </td>
                            </tr>
                            
                            <tr>
                                <td>
                                    ID:
                                </td>
                                <td>
                                    <asp:TextBox runat="server" ID="stbID" CssClass="searchField form-control" AccessKey="u" EnableViewState="False"></asp:TextBox>
                                </td>
                            </tr>

                            <tr>
                                <td>
                                    Стр.:
                                </td>
                                <td>
                                    <asp:DropDownList runat="server" ID="sddPages" CssClass="searchField ddl-control" Width="160px" AccessKey="s"></asp:DropDownList>
                                </td>
                            </tr>
                        </table>
                    </td>
                        
                    <td>
                        <table class="table" style="margin-top: 10px;">
                            <tr>
                                <td>
                                    Действие:
                                </td>
                                <td>
                                    <asp:DropDownList runat="server" ID="sddActions" CssClass="searchField ddl-control"  Width="160px" AccessKey="d"></asp:DropDownList>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    Сущность:
                                </td>
                                <td>
                                    <asp:DropDownList runat="server" ID="sddTable" CssClass="searchField ddl-control"  Width="160px" AccessKey="d"></asp:DropDownList>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    Параметр:
                                </td>
                                <td>
                                    <asp:DropDownList runat="server" ID="sddField" CssClass="searchField ddl-control"  Width="160px" AccessKey="d"></asp:DropDownList>
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
        </asp:Panel><br/>
                
        <asp:ListView runat="server" ID="lvAllErrors">
        <LayoutTemplate>
            <table runat="server" id="Table1" class="table tableViewClass tableClass">
                <tr style="background-color: #EECFBA">
                    <th>
                        Дата изменения
                    </th>
                    <th>
                        Кем изменено
                    </th>
                    <th>
                        Страница
                    </th>
                    <th>
                        ID
                    </th>
                    <th>
                        Действие
                    </th>
                    <th>
                        Сущность
                    </th>
                    <th>
                        Параметр
                    </th>
                    
                    <th>
                        Старое значение
                    </th>
                    <th>
                        Новое значение
                    </th>
                </tr>
                <tr runat="server" id="itemPlaceholder"></tr>
            </table>
        </LayoutTemplate>
        <ItemTemplate>
            <tr id="Tr2" runat="server" style="background-color: #F7F8F9; text-align: center;">
                <td id="Td1" runat="server">
                    <asp:Label ID="lblRegion" runat="server" Text='<%# Eval("DateTime") %>' />
                </td>
                
                <td id="Td7" runat="server">
                    <asp:HyperLink runat="server" NavigateUrl='<%# String.Format("~/ManagerUI/ManagerEdit.aspx?id={0}",Eval("UserID").ToString()) %>'>
                        <asp:Label ID="lblCost" runat="server" Text='<%# UsersHelper.UserIDToFullName(Eval("UserID").ToString()) %>' />
                    </asp:HyperLink><br/>
                    <span style="font-size: 10px; font-style: italic;">(ip: <%#Eval("UserIP")%>)</span>
                </td>
                
                <td id="Td3" runat="server">
                    <asp:Label ID="Label5" runat="server" Text='<%# LogsMethods.PageNameToRuss(Eval("PageName").ToString(), Eval("FieldID").ToString()) %>' />
                </td>
                
                <td id="Td4" runat="server">
                    <asp:HyperLink ID="HyperLink1" runat="server" Target="_new" NavigateUrl='<%# LogsMethods.FieldIDToTicketSecureID(Eval("TableName").ToString(), Convert.ToInt32(Eval("FieldID")), true, Eval("TicketFullSecureID").ToString(), Eval("TicketUserID").ToString()) %>'>
                        <asp:Label ID="lblTrack" runat="server" Text='<%# LogsMethods.FieldIDToTicketSecureID(Eval("TableName").ToString(), Convert.ToInt32(Eval("FieldID")), false, Eval("TicketFullSecureID").ToString(), Eval("TicketUserID").ToString()) %>' />
                    </asp:HyperLink>
                </td>
                
                <td id="Td6" runat="server">
                    <asp:Label ID="Label1" runat="server" Text='<%# LogsMethods.MethodNameToRuss(Eval("Method").ToString()) %>' />
                </td>

                <td id="Td2" runat="server">
                    <asp:Label ID="Label2" runat="server" Text='<%# LogsMethods.TableNameToRuss(Eval("TableName").ToString()) %>' />
                </td>
                
                <td id="Td5" runat="server">
                    <asp:Label ID="Label3" runat="server" Text='<%# LogsMethods.PropertyNameToRuss(Eval("TableName").ToString(), Eval("PropertyName").ToString()) %>' />
                </td>
                
                <td id="Td8" runat="server">
                    <asp:Label ID="Label4" runat="server" Text='<%# LogsMethods.OldNewValueToRuss(Eval("TableName").ToString(), Eval("PropertyName").ToString(), Eval("OldValue").ToString()) %>' />
                </td>
                
                <td id="Td9" runat="server">
                    <asp:Label ID="Label6" runat="server" Text='<%# LogsMethods.OldNewValueToRuss(Eval("TableName").ToString(), Eval("PropertyName").ToString(), Eval("NewValue").ToString()) %>' />
                </td>
                
            </tr>
        </ItemTemplate>
        <AlternatingItemTemplate>
            <tr id="Tr2" runat="server" style="background-color: #EBEEF2; text-align: center;">
                <td id="Td1" runat="server">
                    <asp:Label ID="lblRegion" runat="server" Text='<%# Eval("DateTime") %>' />
                </td>
                
                <td id="Td7" runat="server">
                    <asp:HyperLink ID="HyperLink2" runat="server" NavigateUrl='<%# String.Format("~/ManagerUI/ManagerEdit.aspx?id={0}",Eval("UserID").ToString()) %>'>
                        <asp:Label ID="lblCost" runat="server" Text='<%# UsersHelper.UserIDToFullName(Eval("UserID").ToString()) %>' />
                    </asp:HyperLink><br/>
                    <span style="font-size: 10px; font-style: italic;">(ip: <%#Eval("UserIP")%>)</span>
                </td>

                <td id="Td3" runat="server">
                    <asp:Label ID="Label5" runat="server" Text='<%# LogsMethods.PageNameToRuss(Eval("PageName").ToString(), Eval("FieldID").ToString()) %>' />
                </td>
                
                <td id="Td4" runat="server">
                    <asp:HyperLink ID="HyperLink1" runat="server" Target="_new" NavigateUrl='<%# LogsMethods.FieldIDToTicketSecureID(Eval("TableName").ToString(), Convert.ToInt32(Eval("FieldID")), true, Eval("TicketFullSecureID").ToString(), Eval("TicketUserID").ToString()) %>'>
                        <asp:Label ID="lblTrack" runat="server" Text='<%# LogsMethods.FieldIDToTicketSecureID(Eval("TableName").ToString(), Convert.ToInt32(Eval("FieldID")), false, Eval("TicketFullSecureID").ToString(), Eval("TicketUserID").ToString()) %>' />
                    </asp:HyperLink>
                </td>
                
                <td id="Td6" runat="server">
                    <asp:Label ID="Label1" runat="server" Text='<%# LogsMethods.MethodNameToRuss(Eval("Method").ToString()) %>' />
                </td>
                

                <td id="Td2" runat="server">
                    <asp:Label ID="Label2" runat="server" Text='<%# LogsMethods.TableNameToRuss(Eval("TableName").ToString()) %>' />
                </td>
                
                <td id="Td5" runat="server">
                    <asp:Label ID="Label3" runat="server" Text='<%# LogsMethods.PropertyNameToRuss(Eval("TableName").ToString(), Eval("PropertyName").ToString()) %>' />
                </td>
                
                <td id="Td8" runat="server">
                    <asp:Label ID="Label4" runat="server" Text='<%# LogsMethods.OldNewValueToRuss(Eval("TableName").ToString(), Eval("PropertyName").ToString(), Eval("OldValue").ToString()) %>' />
                </td>
                
                <td id="Td9" runat="server">
                    <asp:Label ID="Label6" runat="server" Text='<%# LogsMethods.OldNewValueToRuss(Eval("TableName").ToString(), Eval("PropertyName").ToString(), Eval("NewValue").ToString()) %>' />
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
