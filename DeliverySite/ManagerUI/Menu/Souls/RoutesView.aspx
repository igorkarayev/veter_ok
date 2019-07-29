<%@ Page Title="" Language="C#" MasterPageFile="~/ManagerMasterPage.master" AutoEventWireup="true" CodeBehind="RoutesView.aspx.cs" Inherits="Delivery.ManagerUI.Menu.Souls.RoutesView" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
      <div>
          <asp:HyperLink ID="HyperLink1" runat="server" CssClass="createItemHyperLink" NavigateUrl="~/ManagerUI/RoutesEdit.aspx">Добавить маршрут</asp:HyperLink>
          <h3 class="h3custom" style="margin-top: 0;">Список маршрутов</h3>
      </div>
        
    <div>
        <asp:ListView runat="server" ID="lvAllTracks">
        <LayoutTemplate>
            <table runat="server" id="Table1" class="table tableViewClass tableClass">
                <tr style="background-color: #EECFBA">
                    <th>
                        Маршрут
                    </th>
                    <th>
                    </th>
                </tr>
                <tr runat="server" id="itemPlaceholder"></tr>
            </table>
        </LayoutTemplate>
        <ItemTemplate>
            <tr id="Tr2" runat="server" style="background-color: #F7F8F9; text-align: center;">
                <td id="Td2" runat="server">
                    <asp:Label ID="lblName" runat="server" Text='<%#Eval("Name") %>' />
                </td>
               
                <td id="Td3" runat="server"  style="font-size: 12px; width: 100px;">
                     <asp:HyperLink ID="LinkButton1" runat="server" NavigateUrl='<%#"~/ManagerUI/RoutesEdit.aspx?id="+Eval("ID") %>'>Изменить</asp:HyperLink><br/>
                     <asp:LinkButton ID="lbDelete" runat="server" OnClick="lbDelete_Click" CommandArgument='<%#Eval("ID") %>'>Удалить</asp:LinkButton>
                </td>
            </tr>
        </ItemTemplate>
        <AlternatingItemTemplate>
            <tr id="Tr2" runat="server" style="background-color: #EBEEF2; text-align: center;">
                <td id="Td2" runat="server">
                    <asp:Label ID="lblName" runat="server" Text='<%#Eval("Name") %>' />
                </td>
                
                <td id="Td3" runat="server"  style="font-size: 12px; width: 100px;">
                     <asp:HyperLink ID="LinkButton1" runat="server" NavigateUrl='<%#"~/ManagerUI/RoutesEdit.aspx?id="+Eval("ID") %>'>Изменить</asp:HyperLink><br/>
                     <asp:LinkButton ID="lbDelete" runat="server" OnClick="lbDelete_Click" CommandArgument='<%#Eval("ID") %>'>Удалить</asp:LinkButton>
                </td>
            </tr>
        </AlternatingItemTemplate>
    </asp:ListView>
        <div class="infoBlock" style="margin-bottom: 0;">
            <div class="pager">
                <asp:Label ID="lblPage" runat="server">Страница:</asp:Label>
                <asp:DataPager ID="lvDataPager" runat="server" PagedControlID="lvAllTracks" PageSize="25">
                    <Fields>
                        <asp:NumericPagerField ButtonType="Link" />
                    </Fields>
                </asp:DataPager>  
            </div>
        </div>
    </div>
</asp:Content>
