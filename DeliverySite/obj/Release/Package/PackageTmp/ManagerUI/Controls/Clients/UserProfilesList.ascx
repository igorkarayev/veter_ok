<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="UserProfilesList.ascx.cs" Inherits="Delivery.ManagerUI.Controls.Clients.UserProfilesList" %>
<%@ Import Namespace="Delivery.BLL.Helpers" %>


    <asp:ListView runat="server" ID="lvAllUserProfile">
        <LayoutTemplate>
            <table class="table profiles" style="width: 100%;">
                <tr class='profiles-table-header'>
                    <th style="text-align: right; font-size: 14px; font-weight: normal; background-color: white; width: 230px;">
                        Профили клиента:
                    </th>
                
                    <th style="width: 50px;">
                        Тип
                    </th>
                    <th style="width: 200px;">
                        Директор
                    </th>
                    <th style="width: 150px;">
                        Компания
                    </th>
                    <th style="width: 150px;">
                        Контактное лицо
                    </th>
                    <th style="">
                        Информация договора
                    </th>
                    <th style="width: 100px;">
                        Статус
                    </th>
                </tr>
                <tr runat="server" id="itemPlaceholder"></tr>
            </table>
        </LayoutTemplate>
        <ItemTemplate>
            <tr id="Tr2" runat="server" class='table-item-template' style="text-align: center;">
                <td style="width: 230px; background-color: white;">
                
                </td>
            

                <td id="Td7" runat="server" style="text-align: center" class='<%# UsersHelper.UserProfileColoredStatusRows(Status, Eval("StatusID").ToString()) %>'>
                    <asp:HyperLink ID="HyperLink1" runat="server" NavigateUrl='<%#"~/ManagerUI/Menu/Souls/ProfileEdit.aspx?id="+Eval("ID") %>'>
                        <asp:Label ID="lblDate" runat="server" Text='<%# UsersProfilesHelper.UserTypeToStr(Eval("TypeID").ToString()) %>' />
                    </asp:HyperLink>
                </td>

                <td id="Td0" runat="server" class='<%# UsersHelper.UserProfileColoredStatusRows(Status, Eval("StatusID").ToString()) %>'>
                    <asp:HyperLink ID="HyperLink2" runat="server" NavigateUrl='<%#"~/ManagerUI/Menu/Souls/ProfileEdit.aspx?id="+Eval("ID") %>'>
                        <asp:Label ID="Label1" runat="server" Text='<%# Eval("FirstName") %>' />&nbsp;
                        <asp:Label ID="Label4" runat="server" Text='<%# Eval("LastName") %>' />&nbsp;
                        <asp:Label ID="Label5" runat="server" Text='<%# Eval("ThirdName") %>' />
                        <div>
                            <asp:Label ID="Label6" runat="server" Text='<%# Eval("DirectorPhoneNumber") %>' />
                        </div>
                    </asp:HyperLink>
                </td>
                
                <td id="Td2" runat="server" class='<%# UsersHelper.UserProfileColoredStatusRows(Status, Eval("StatusID").ToString()) %>'>
                    <asp:Label ID="Label3" runat="server" Text='<%# Eval("CompanyName") %>' />
                </td>

                <td id="Td1" runat="server" class='<%# UsersHelper.UserProfileColoredStatusRows(Status, Eval("StatusID").ToString()) %>'>
                    <asp:Label ID="Label7" runat="server" Text='<%# UsersProfilesHelper.ContactFioToString(Eval("ContactPersonFIO").ToString()) %>' />
                    <div>
                        <asp:Label ID="Label2" runat="server" Text='<%# Eval("ContactPhoneNumbers").ToString().Replace(";","<br/>") %>' />
                    </div>
                </td>
                <td id="Td3" runat="server" class='<%# UsersHelper.UserProfileColoredStatusRows(Status, Eval("StatusID").ToString()) %>'>
                    <%# UsersProfilesHelper.GetAgreementData(Eval("AgreementDate").ToString(),Eval("AgreementNumber").ToString()) %>
                </td>
                <td id="Td4" runat="server" style="text-align: center" class='<%# UsersHelper.UserProfileColoredStatusRows(Status, Eval("StatusID").ToString()) %>'>
                    <%# UsersProfilesHelper.UserProfileStatusToText(Convert.ToInt32(Eval("StatusID").ToString())) %>
                </td>
            </tr>
        </ItemTemplate>
        <EmptyDataTemplate>
            <div style="height: 25px;"></div>
        </EmptyDataTemplate>
    </asp:ListView>
