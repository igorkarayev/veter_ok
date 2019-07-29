 <%@ Page Title="" Language="C#" MasterPageFile="~/UserMasterPage.master" AutoEventWireup="true" CodeBehind="ProfilesEditSender.aspx.cs" Inherits="DeliverySite.UserUI.ProfilesEditSender" %>
<%@ Import Namespace="Delivery.BLL.StaticMethods" %>
<%@ Import Namespace="Delivery.BLL.Helpers" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">    
    <h3 class="h3custom" style="margin-top: 0;">Редактировать профили контрагентов</h3>

    <asp:ListView runat="server" ID="lvAllProfiles">
        <LayoutTemplate>
            <table runat="server" id="Table1" class="tableViewClass tableClass">
                <tr class="table-header">                    
                    <th>
                        Название контрагента
                    </th>
                    <th>
                        Город отправки
                    </th>                    
                    <th>
                        Функции
                    </th>  
                </tr>
                <tr runat="server" id="itemPlaceholder"></tr>
            </table>
        </LayoutTemplate>
        <ItemTemplate>
            <asp:TableRow id="Tr2" runat="server" style="text-align: center;" >
                <asp:TableCell id="TableCell2" runat="server">
                    <asp:Label ID="Label1" runat="server" Text='<%# Eval("ProfileName").ToString() %>' />
                </asp:TableCell>
                
                <asp:TableCell id="Td5" runat="server">
                    <asp:Label ID="Label4" runat="server" Text='<%# CityHelper.CityIDToCityName(Eval("CityID").ToString()) %>' />
                </asp:TableCell>                
                
                <asp:TableCell id="TableCell3" runat="server">
                    <asp:LinkButton ID="lbEdit" runat="server" OnClick="lbEdit_Click" CommandArgument='<%#Eval("ID") %>'>Редактировать<br/></asp:LinkButton>                    
                    <asp:LinkButton ID="lbDelete" runat="server" OnClick="lbDelete_Click" CommandArgument='<%#Eval("ID") %>'>Удалить</asp:LinkButton>                    
                </asp:TableCell>                
            </asp:TableRow>
        </ItemTemplate>
    </asp:ListView>

    <div style="float: right;">
        <asp:Button Width="110px" ID="btnCreate" runat="server" Text="Создать" CssClass="btn btn-default"/>
    </div>    
</asp:Content>
