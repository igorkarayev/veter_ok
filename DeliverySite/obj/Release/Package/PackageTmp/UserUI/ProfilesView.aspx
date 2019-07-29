<%@ Page Title="" Language="C#" MasterPageFile="~/UserMasterPage.master" AutoEventWireup="true" CodeBehind="ProfilesView.aspx.cs" Inherits="Delivery.UserUI.ProfilesView" %>
<%@ Import Namespace="Delivery.BLL.StaticMethods" %>
<%@ Import Namespace="Delivery.BLL.Helpers" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div>
        <asp:HyperLink ID="HyperLink1" CssClass="createItemHyperLink" runat="server" NavigateUrl="~/UserUI/ProfileEdit.aspx">Создать новый профиль</asp:HyperLink>
        <h3 class="h3custom" style="margin-top: 0;">Ваши профили</h3>
    </div>    
    <div>
        <asp:ListView runat="server" ID="lvAllProfile">
        <LayoutTemplate>
            <table runat="server" id="Table1" class="tableViewClass tableClass">
                <tr class="table-header">
                    <th>
                        Тип
                    </th>
                    <th>
                        ФИО
                    </th>
                    <th>
                        Компания
                    </th>
                    <th>
                        Статус
                    </th>
                    <th style="width: 20px;">
                        По умолч.
                    </th>
                    <th style="width: 70px;">
                    </th>
                </tr>
                <tr runat="server" id="itemPlaceholder"></tr>
            </table>
        </LayoutTemplate>
        <ItemTemplate>
            <asp:TableRow id="Tr2" runat="server" style="text-align: center;" CssClass='<%# OtherMethods.ColoredProfileStatusRows(Eval("StatusID").ToString())%>' >
                <asp:TableCell id="Td4" runat="server">
                    <asp:Label ID="Label3" runat="server" Text='<%# UsersProfilesHelper.UserTypeToStr(Eval("TypeID").ToString()) %>' />
                </asp:TableCell>

                <asp:TableCell id="TableCell2" runat="server">
                    <asp:HyperLink ID="HyperLink1" runat="server" NavigateUrl='<%#"~/UserUI/ProfileView.aspx?id="+Eval("ID") %>'>
                        <asp:Label ID="lblLastName" runat="server" Text='<%#Eval("LastName") %>' />&nbsp;
                        <asp:Label ID="lblThirdName" runat="server" Text='<%#Eval("ThirdName") %>' />&nbsp;
                        <asp:Label ID="lblFirstName" runat="server" Text='<%#Eval("FirstName") %>' />
                    </asp:HyperLink>
                </asp:TableCell>
                
                <asp:TableCell id="Td5" runat="server">
                    <asp:Label ID="Label4" runat="server" Text='<%#Eval("CompanyName") %>' />
                </asp:TableCell>
                
                <asp:TableCell id="TableCell1" runat="server">
                    <asp:Label ID="Label5" runat="server" Text='<%# UsersProfilesHelper.UserProfileStatusToText(Convert.ToInt32(Eval("StatusID")))%>' />
                    <asp:HiddenField ID="hfStatus" runat="server" Value='<%# Eval("StatusID")%>' />
                </asp:TableCell>

                <asp:TableCell id="Td6" runat="server">
                    <asp:Label ID="lblIsDefault" runat="server" Text='<%# OtherMethods.CheckboxFlag(Eval("IsDefault").ToString()) %>' />
                </asp:TableCell>
                
                <asp:TableCell id="Td3" runat="server"  style="font-size: 12px;">
                     <asp:LinkButton ID="lbDefault" runat="server" OnClick="lbDefault_Click" CommandArgument='<%#Eval("ID") %>'>По умолч.<br/></asp:LinkButton>
                     <asp:HyperLink ID="hlChange" runat="server" NavigateUrl='<%#"~/UserUI/ProfileEdit.aspx?id="+Eval("ID") %>'>Изменить<br/></asp:HyperLink>
                     <asp:LinkButton ID="lbDelete" runat="server" OnClick="lbDelete_Click" OnClientClick="return confirmDelete();" CommandArgument='<%#Eval("ID") %>'>Удалить</asp:LinkButton>
                </asp:TableCell>
            </asp:TableRow>
        </ItemTemplate>
        <EmptyDataTemplate>
            <asp:ListView runat="server" ID="lvAllTickets">  
                <LayoutTemplate>
                        <asp:CheckBox ID="chkboxSelectAll" runat="server"/>
                        <div runat="server" id="itemPlaceholder"></div>
                </LayoutTemplate>
            </asp:ListView>
            <div style="width: 100%; text-align: center; padding: 15px; color: red">Пока нет ни одного профиля. Создайте новый профиль.</div>
        </EmptyDataTemplate>
    </asp:ListView>
        <div class="infoBlock" style="margin-bottom: 0;">
            <div class="pager">
                <asp:Label ID="lblPage" runat="server">Страница:</asp:Label>
                <asp:DataPager ID="lvDataPager" runat="server" PagedControlID="lvAllProfile" PageSize="25">
                    <Fields>
                        <asp:NumericPagerField ButtonType="Link" />
                    </Fields>
                </asp:DataPager>  
            </div>
        </div>
    </div>
</asp:Content>
