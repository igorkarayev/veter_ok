<%@ Page Title="" Language="C#" MasterPageFile="~/ManagerMasterPage.master" AutoEventWireup="true" CodeBehind="UserToCategoryView.aspx.cs" Inherits="Delivery.ManagerUI.Menu.Souls.UserToCategoryView" Async="true"%>
<%@ Import Namespace="Delivery.BLL.Helpers" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <h3 class="h3custom" style="margin-top: 0;">Добавление/удаление категорий клиента</h3>
    
    <div class="single-input-fild" style="margin: 30px auto 30px; width: 400px">
        Категории доступные клиенту: <asp:Label runat="server" ID="lblAllAvaliable" style="color: green" Visible="False">все</asp:Label>
        <asp:ListView runat="server" ID="lvAllTracks">
            <LayoutTemplate>
                <table runat="server" id="Table1" class="tableViewClass tableClass">
                    <tr style="background-color: #EECFBA">
                        <th style="text-align: left;">
                            Категория
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
                        <asp:HyperLink ID="HyperLink2" runat="server" NavigateUrl='<%#"~/ManagerUI/Menu/Souls/CategoryView.aspx?categoryid="+Eval("CategoryID") %>'>
                            <asp:Label ID="lblName" runat="server" Text='<%#CategoryHelper.IdToName(Eval("CategoryID").ToString()) %>' />
                        </asp:HyperLink>
                    </td>

                    <td id="Td3" runat="server"  style="font-size: 12px; width: 100px;">
                         <asp:LinkButton ID="lbDelete" runat="server" OnClick="lbDelete_Click" OnClientClick="return confirmDelete();" CommandArgument='<%#Eval("CategoryID") %>'>Удалить</asp:LinkButton>
                    </td>
                </tr>
            </ItemTemplate>
        </asp:ListView>
    </div>
    <div class="single-input-fild" style="width: 400px;">
        <div class="form-group">
            <label>Категория</label>
            <asp:DropDownList ID="ddlSections" runat="server" width="107%" CssClass="searchField ddl-control"/>
        </div>
        <div>
            <asp:Button ID="btnAdd" runat="server" Text='Добавить' CssClass="btn btn-default btn-right" style="margin-left: 30px;"/>
            <asp:Button ID="btnBack" OnClientClick="window.history.go(-1);return false;" runat="server" CssClass="btn btn-default btn-right" Text='Назад'/>
        </div>
    </div>
</asp:Content>
