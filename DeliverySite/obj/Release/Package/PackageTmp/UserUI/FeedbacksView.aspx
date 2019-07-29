<%@ Page Title="" Language="C#" MasterPageFile="~/UserMasterPage.master" AutoEventWireup="true" CodeBehind="FeedbacksView.aspx.cs" Inherits="Delivery.UserUI.FeedbacksView" Async="true" %>
<%@ Import Namespace="Delivery.BLL" %> <%@ Import Namespace="Delivery.BLL.Helpers" %> <%@ Import Namespace="Delivery.BLL.StaticMethods" %> <%@ Import Namespace="Delivery.DAL.DataBaseObjects" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
      <div>
          <asp:HyperLink ID="HyperLink1" runat="server" CssClass="createItemHyperLink" NavigateUrl="~/UserUI/FeedbackCreate.aspx">Добавить обращение</asp:HyperLink>
          <h3 class="h3custom" style="margin-top: 0;">Список обращений</h3>
      </div>
        
    <div>
        <asp:ListView runat="server" ID="lvAllFeedback">
        <LayoutTemplate>
            <table runat="server" id="Table1" class="tableViewClass tableClass">
                <tr style="background-color: #EECFBA">
                    <th style="width: 10px">
                        ID
                    </th>

                    <th>
                        Тема
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
                    
                    <th style="width: 80px">
                        Создано
                    </th>
                    
                    <th style="width: 80px">
                        Обновлено
                    </th>

                    <th style="width: 30px">
                        Новых комментов
                    </th>
                </tr>
                <tr runat="server" id="itemPlaceholder"></tr>
            </table>
        </LayoutTemplate>
        <ItemTemplate>
            <tr id="Tr2" runat="server" class='<%# FeedbackHelper.ColoredPriorityAndStatusRows(Eval("PriorityID").ToString(), Eval("StatusID").ToString()) %>' style="text-align: center;">
                <td id="Td8" runat="server">
                    <asp:HyperLink ID="HyperLink2" runat="server" NavigateUrl='<%#"~/UserUI/FeedbackView.aspx?id="+Eval("ID") %>'>
                        <asp:Label ID="Label7" runat="server" Text='<%#Eval("ID") %>' style="color: black" />
                    </asp:HyperLink>
                </td>
                
                <td id="Td2" runat="server" style="text-align: left; padding-left: 15px;">
                    <asp:HyperLink ID="LinkButton1" runat="server" NavigateUrl='<%#"~/UserUI/FeedbackView.aspx?id="+Eval("ID") %>'>
                        <asp:Label ID="lblName" runat="server" Text='<%#Eval("Title") %>' />
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

                <td id="Td5" runat="server">
                    <asp:Label ID="Label3" runat="server" Text='<%# OtherMethods.DateConvert(Eval("CreateDate").ToString()) %>' />
                </td>

                <td id="Td3" runat="server">
                    <asp:Label ID="Label6" runat="server" Text='<%# OtherMethods.DateConvert(Eval("ChangeDate").ToString()) %>' />
                </td>

                <td id="Td7" runat="server">
                   <asp:Label ID="Label5" runat="server" Text='<%# FeedbackHelper.CommentCountFilter(Convert.ToInt32(Eval("NewCommetsCount"))) %>' />
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
                <asp:DataPager ID="lvDataPager" runat="server" PagedControlID="lvAllFeedback" PageSize="25" OnPreRender="lvDataPager_PreRender">
                    <Fields>
                        <asp:NumericPagerField ButtonType="Link" />
                    </Fields>
                </asp:DataPager>  
            </div>
        </div>
    </div>
</asp:Content>

