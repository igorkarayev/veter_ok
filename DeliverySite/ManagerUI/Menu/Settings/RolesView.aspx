<%@ Page Title="" Language="C#" MasterPageFile="~/ManagerMasterPage.master" AutoEventWireup="true" CodeBehind="RolesView.aspx.cs" Inherits="Delivery.ManagerUI.Menu.Settings.RolesView" %>
<%@ Import Namespace="Delivery.BLL" %> <%@ Import Namespace="Delivery.BLL.StaticMethods" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
      <div>
          <asp:HyperLink ID="HyperLink1" runat="server" CssClass="createItemHyperLink" NavigateUrl="~/ManagerUI/Menu/Settings/RolesEdit.aspx">Добавить роль</asp:HyperLink>
          <h3 class="h3custom" style="margin-top: 0;">Список ролей сотрудников</h3>
      </div>
        
    <div>
        <asp:ListView runat="server" ID="lvAllRoles">
        <LayoutTemplate>
            <table runat="server" id="Table1" class="table tableViewClass tableClass">
                <tr style="background-color: #EECFBA">
                    <th style="width: 150px">
                        Название
                    </th>
                    <th style="width: 150px">
                        На русском
                    </th>
                    <th>
                    </th>
                    <th style="width: 30px">
                        Человек
                    </th>
                    <th style="width: 30px">
                    </th>
                </tr>
                <tr runat="server" id="itemPlaceholder"></tr>
            </table>
        </LayoutTemplate>
        <ItemTemplate>
            <tr id="Tr2" runat="server" style="background-color: #F7F8F9; text-align: center;">
                <td id="Td2" runat="server">
                    <asp:Label ID="lblName" runat="server" Text='<%#Eval("Name") %>' />
                    <asp:HiddenField ID="hfIsBase" runat="server" Value='<%#Eval("IsBase") %>' />
                </td>
                <td id="Td5" runat="server">
                    <asp:Label ID="Label3" runat="server" Text='<%#Eval("NameOnRuss") %>' />
                </td>

                <td>
                    
                </td>
                
                <td>
                    <asp:Label ID="lblUserInRoleCount" runat="server" Text='<%#Eval("NameOnRuss") %>' />
                </td>

                <td id="Td3" runat="server"  style="font-size: 12px; width: 100px;">
                     <asp:HyperLink ID="lbChange" runat="server" NavigateUrl='<%#"~/ManagerUI/Menu/Settings/RolesEdit.aspx?id="+Eval("ID") %>'>Изменить</asp:HyperLink><br/>
                     <asp:LinkButton ID="lbDelete" runat="server" OnClick="lbDelete_Click" OnClientClick="return confirmDelete();" CommandArgument='<%#Eval("ID") %>'>Удалить</asp:LinkButton>
                </td>
            </tr>
        </ItemTemplate>
        <AlternatingItemTemplate>
            <tr id="Tr2" runat="server" style="background-color: #EBEEF2; text-align: center;">
                <td id="Td2" runat="server">
                    <asp:Label ID="lblName" runat="server" Text='<%#Eval("Name") %>' />
                    <asp:HiddenField ID="hfIsBase" runat="server" Value='<%#Eval("IsBase") %>' />
                </td>
                
                <td id="Td5" runat="server">
                    <asp:Label ID="Label3" runat="server" Text='<%#Eval("NameOnRuss") %>' />
                </td>

                <td>
                    
                </td>
                
                <td>
                    <asp:Label ID="lblUserInRoleCount" runat="server" Text='<%#Eval("NameOnRuss") %>' />
                </td>

                <td id="Td3" runat="server"  style="font-size: 12px; width: 100px;">
                     <asp:HyperLink ID="lbChange" runat="server" NavigateUrl='<%#"~/ManagerUI/Menu/Settings/RolesEdit.aspx?id="+Eval("ID") %>'>Изменить</asp:HyperLink><br/>
                     <asp:LinkButton ID="lbDelete" runat="server" OnClick="lbDelete_Click" OnClientClick="return confirmDelete();" CommandArgument='<%#Eval("ID") %>'>Удалить</asp:LinkButton>
                </td>
            </tr>
        </AlternatingItemTemplate>
    </asp:ListView>
        <div class="infoBlock" style="margin-bottom: 0;">
            <div class="pager">
                <asp:Label ID="lblPage" runat="server">Страница:</asp:Label>
                <asp:DataPager ID="lvDataPager" runat="server" PagedControlID="lvAllRoles" PageSize="25" OnPreRender="lvDataPager_PreRender">
                    <Fields>
                        <asp:NumericPagerField ButtonType="Link" />
                    </Fields>
                </asp:DataPager>  
            </div>
        </div>
    </div>
</asp:Content>
