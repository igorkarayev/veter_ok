<%@ Page Title="" Language="C#" MasterPageFile="~/ManagerMasterPage.master" AutoEventWireup="true" CodeBehind="FeedbacksView.aspx.cs" Inherits="Delivery.ManagerUI.Menu.Souls.FeedbacksView" Async="true" %>
<%@ Import Namespace="Delivery.BLL.Helpers" %> 
<%@ Import Namespace="Delivery.BLL.StaticMethods" %> 
<%@ Import Namespace="Delivery.DAL.DataBaseObjects" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div>
        <h3 class="h3custom" style="margin-top: 0;">Список обращений</h3>
    </div>
    
    <asp:Panel runat="server" ID="SearchPanel" CssClass="mainFont" DefaultButton="btnSearch">
        <table class="table searchPanel" style="margin-left:auto; margin-right: auto;">
            <tr>
                <td>
                    ID: <asp:TextBox runat="server" ID="stbID" CssClass="searchField form-control" style="width: 20px;"></asp:TextBox>
                    Тип: <asp:DropDownList runat="server" ID="sddlType" CssClass="searchField ddl-control" Width="110px"></asp:DropDownList>
                    Приоритет: <asp:DropDownList runat="server" ID="sddlPriority" CssClass="searchField ddl-control" Width="110px"></asp:DropDownList>
                    Статус: <asp:DropDownList runat="server" ID="sddlStatus" CssClass="searchField ddl-control" Width="110px"></asp:DropDownList>
                    <asp:Button runat="server" Text="Сброс" CssClass="btn btn-default" ID="btnReload" style="margin-top: -5px"/>
                    <asp:Button runat="server" Text="Искать" CssClass="btn btn-default" ID="btnSearch" style="margin-top: -5px"/>  
                    &nbsp;&nbsp;&nbsp;<i class="small-informer">Найдено:<b> <asp:Label runat="server" ID="lblAllResult" ></asp:Label></b></i> 
                </td>
            </tr>
        </table>
    </asp:Panel><br/>    
        
    <div>
        <asp:ListView runat="server" ID="lvAllFeedback">
        <LayoutTemplate>
            <table runat="server" id="Table1" class="table tableViewClass tableClass">
                <tr style="background-color: #EECFBA">
                    <th style="width: 10px">
                        ID
                    </th>

                    <th>
                        Тема
                    </th>

                    <th style="width: 100px">
                        Клиент
                    </th>
                    
                    <th style="width: 200px">
                        Тип
                    </th>

                    <th style="width: 30px">
                        Важность
                    </th>

                    <th style="width: 30px">
                        Статус
                    </th>
                    
                    <th style="width: 10px">
                        Комм.
                    </th>
                    
                    <th style="width: 80px">
                        Создано
                    </th>
                    
                    <th style="width: 80px">
                        Обновлено
                    </th>
                </tr>
                <tr runat="server" id="itemPlaceholder"></tr>
            </table>
        </LayoutTemplate>
        <ItemTemplate>
            <tr id="Tr2" runat="server" class='<%# FeedbackHelper.ColoredPriorityAndStatusRows(Eval("PriorityID").ToString(), Eval("StatusID").ToString()) %>' style="text-align: center;">
                
                <td id="Td9" runat="server" style="text-align: center;">
                    <asp:HyperLink ID="HyperLink2" runat="server" NavigateUrl='<%#"~/ManagerUI/Menu/Souls/FeedbackView.aspx?id="+Eval("ID") %>'>
                        <asp:Label ID="Label8" style="color: black" runat="server" Text='<%#Eval("ID") %>' />
                    </asp:HyperLink>
                </td>

                <td id="Td2" runat="server" style="text-align: left; padding-left: 15px;">
                    <asp:HyperLink ID="LinkButton1" runat="server" NavigateUrl='<%#"~/ManagerUI/Menu/Souls/FeedbackView.aspx?id="+Eval("ID") %>'>
                        <asp:Label ID="lblName" runat="server" Text='<%#Eval("Title") %>' />
                    </asp:HyperLink>
                </td>
                
                <td id="Td7" runat="server">
                    <asp:HyperLink ID="HyperLink1" runat="server" NavigateUrl='<%#"~/ManagerUI/Menu/Souls/ClientEdit.aspx?id="+Eval("UserID") %>'>
                        <asp:Label ID="Label5" runat="server" Text='<%# UsersHelper.UserIDToFullName(Eval("UserID").ToString()) %>' />
                    </asp:HyperLink>
                </td>

                <td id="Td6" runat="server">
                    <asp:Label ID="Label4" runat="server" Text='<%# Feedback.Types.FirstOrDefault(u=>u.Key == Convert.ToInt32(Eval("TypeID"))).Value %>' />
                </td>
                
                <td id="Td1" runat="server">
                    <asp:Label ID="Label1" runat="server" Text='<%# Feedback.Priorities.FirstOrDefault(u=>u.Key == Convert.ToInt32(Eval("PriorityID"))).Value %>' />
                </td>
                
                <td id="Td4" runat="server">
                    <asp:Label ID="Label2" runat="server" Text='<%# Feedback.Statuses.FirstOrDefault(u=>u.Key == Convert.ToInt32(Eval("StatusID"))).Value %>' />
                </td>
                
                <td id="Td8" runat="server">
                    <asp:Label ID="Label7" runat="server" Text='<%# Eval("CommetsCount") %>' />
                </td>

                <td id="Td5" runat="server">
                    <asp:Label ID="Label3" runat="server" Text='<%# OtherMethods.DateConvert(Eval("CreateDate").ToString()) %>' />
                </td>

                <td id="Td3" runat="server">
                    <asp:Label ID="Label6" runat="server" Text='<%# OtherMethods.DateConvert(Eval("ChangeDate").ToString()) %>' />
                </td>
            </tr>
        </ItemTemplate>
        <EmptyDataTemplate>
            <asp:ListView runat="server" ID="lvAllTickets">  
                <LayoutTemplate>
                        <asp:CheckBox ID="chkboxSelectAll" runat="server"/>
                        <div runat="server" id="itemPlaceholder"></div>
                </LayoutTemplate>
            </asp:ListView>
            <div style="width: 100%; text-align: center; padding: 15px; color: red">Обращений не найдено.</div>
        </EmptyDataTemplate>
       
    </asp:ListView>
        <div class="infoBlock" style="margin-bottom: 0;">
            <div class="pager">
                <asp:Label ID="lblPage" runat="server">Страница:</asp:Label>
                <asp:DataPager ID="lvDataPager" runat="server" PagedControlID="lvAllFeedback" PageSize="25">
                    <Fields>
                        <asp:NumericPagerField ButtonType="Link" />
                    </Fields>
                </asp:DataPager>  
            </div>
        </div>
    </div>
</asp:Content>

