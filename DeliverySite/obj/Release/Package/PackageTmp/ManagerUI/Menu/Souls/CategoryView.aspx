<%@ Page Title="" Language="C#" MasterPageFile="~/ManagerMasterPage.master" AutoEventWireup="true" CodeBehind="CategoryView.aspx.cs" Inherits="Delivery.ManagerUI.Menu.Souls.SectionsView" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
      <div>
          <asp:HyperLink ID="HyperLink1" runat="server" CssClass="createItemHyperLink" NavigateUrl="~/ManagerUI/Menu/Souls/CategoryEdit.aspx">Добавить категорию</asp:HyperLink>
          <h3 class="h3custom" style="margin-top: 0;">Список категорий</h3>
      </div>
        
    <div>
        <asp:ListView runat="server" ID="lvAllTracks">
        <LayoutTemplate>
            <table runat="server" id="Table1" class="table tableViewClass tableClass">
                <tr style="background-color: #EECFBA">
                    <th style="text-align: left;">
                        Название
                    </th>

                    <th style="width: 30px;">
                        Кол-во наименований
                    </th>
                    
                    <th style="width: 30px;">
                    </th>
                </tr>
                <tr runat="server" id="itemPlaceholder"></tr>
            </table>
        </LayoutTemplate>
        <ItemTemplate>
            <tr id="Tr2" runat="server" style="background-color: #F7F8F9; text-align: center;">
                <td id="Td2" runat="server" style="text-align: left;">
                    <asp:HyperLink ID="HyperLink3" runat="server" NavigateUrl='<%#"~/ManagerUI/Menu/Souls/CategoryEdit.aspx?id="+Eval("ID") %>'>
                        <asp:Label ID="lblName" runat="server" Text='<%#Eval("Name") %>' />
                    </asp:HyperLink>
                </td>
                
                <td id="Td1" runat="server">
                    <asp:HyperLink ID="HyperLink2" runat="server" NavigateUrl='<%#"~/ManagerUI/Menu/Souls/TitlesView.aspx?categoryid="+Eval("ID") %>'>
                        <asp:Label ID="lblTitlesCount" runat="server" Text='<%#Eval("TitlesCount") %>' />
                    </asp:HyperLink>
                </td>

                <td id="Td3" runat="server"  style="font-size: 12px; width: 100px;">
                     <asp:LinkButton ID="lbDelete" runat="server" OnClick="lbDelete_Click" OnClientClick="return confirmDelete();" CommandArgument='<%#Eval("ID") %>'>Удалить</asp:LinkButton>
                </td>
            </tr>
        </ItemTemplate>
        <AlternatingItemTemplate>
            <tr id="Tr2" runat="server" style="background-color: #EBEEF2; text-align: center;">
                <td id="Td2" runat="server" style="text-align: left;">
                    <asp:HyperLink ID="LinkButton1" runat="server" NavigateUrl='<%#"~/ManagerUI/Menu/Souls/CategoryEdit.aspx?id="+Eval("ID") %>'>
                        <asp:Label ID="lblName" runat="server" Text='<%#Eval("Name") %>' />
                    </asp:HyperLink>
                </td>
                
                <td id="Td1" runat="server">
                    <asp:HyperLink ID="HyperLink2" runat="server" NavigateUrl='<%#"~/ManagerUI/Menu/Souls/TitlesView.aspx?categoryid="+Eval("ID") %>'>
                        <asp:Label ID="lblTitlesCount" runat="server" Text='<%#Eval("TitlesCount") %>' />
                    </asp:HyperLink>
                </td>
                
                <td id="Td3" runat="server"  style="font-size: 12px; width: 100px;">
                     <asp:LinkButton ID="lbDelete" runat="server" OnClick="lbDelete_Click" OnClientClick="return confirmDelete();" CommandArgument='<%#Eval("ID") %>'>Удалить</asp:LinkButton>
                </td>
            </tr>
        </AlternatingItemTemplate>
    </asp:ListView>
        <div class="infoBlock" style="margin-bottom: 0;">
            <div class="pager">
                <asp:Label ID="lblPage" runat="server">Страница:</asp:Label>
                <asp:DataPager ID="lvDataPager" runat="server" PagedControlID="lvAllTracks" PageSize="250" OnPreRender="lvDataPager_PreRender">
                    <Fields>
                        <asp:NumericPagerField ButtonType="Link" />
                    </Fields>
                </asp:DataPager>  
            </div>
        </div>
    </div>
</asp:Content>
