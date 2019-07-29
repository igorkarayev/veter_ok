<%@ Page Title="" Language="C#" MasterPageFile="~/ManagerMasterPage.master" AutoEventWireup="true" CodeBehind="CalculationView.aspx.cs" Inherits="Delivery.ManagerUI.Menu.Finance.CalculationView" %>
<%@ Import Namespace="System.Net" %>
<%@ Import Namespace="Delivery.BLL.StaticMethods" %>
<%@ Import Namespace="Delivery.BLL.Helpers" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <script type="text/javascript">
        $(function () {
            $("#<%= stbRecipientPhone.ClientID %>").mask("+375 (99) 999-99-99");
                $("#<%= stbDeliveryDate1.ClientID %>")
                .datepicker({ nextText: "", prevText: "", changeMonth: true, changeYear: true })
                .mask("99-99-9999");
                $("#<%= stbDeliveryDate2.ClientID %>")
                    .datepicker({ nextText: "", prevText: "", changeMonth: true, changeYear: true })
                    .mask("99-99-9999");
            });
    </script>
    <h3 class="h3custom" style="margin-top: 0;">Расчет</h3>
    <div>
        <asp:Panel runat="server" ID="SearchPanel" CssClass="mainFont"  DefaultButton="btnSearch">
            <table class="table searchPanel" style="margin-left:auto; margin-right: auto;">
                <tr>
                    <td style="vertical-align: top; padding-top: 4px;">
                        UID:
                        <asp:TextBox runat="server" ID="stbUID" CssClass="searchField form-control" Width="40px"></asp:TextBox>
                        Отправки с:&nbsp;<asp:TextBox runat="server" ID="stbDeliveryDate1" CssClass="searchField form-control" Width="70px"></asp:TextBox>
                            &nbsp;по:&nbsp;<asp:TextBox runat="server" ID="stbDeliveryDate2" CssClass="searchField form-control" Width="70px"></asp:TextBox>    
                        Фам.:<asp:TextBox runat="server" ID="stbFamily" CssClass="searchField form-control" Width="120px"></asp:TextBox>
                        
                        <div style="margin-top: 9px; text-align: right;">
                            Тел.:<asp:TextBox runat="server" ID="stbRecipientPhone" CssClass="searchField form-control" Width="135px"></asp:TextBox>
                            <asp:Button runat="server" Text="Экспорт в .xls" CssClass="btn btn-default" ID="btnExport"/>
                            <asp:Button runat="server" Text="Сброс" CssClass="btn btn-default" ID="btnReload"/>
                            <asp:Button runat="server" Text="Искать" CssClass="btn btn-default" ID="btnSearch"/>
                            <i class="small-informer" >Найдено:<b> <asp:Label runat="server" ID="lblAllResult"></asp:Label></b></i>
                        </div>
                    </td>
                </tr>
            </table>
        </asp:Panel><br/>

        <asp:ListView runat="server" ID="lvAllTickets"  DataKeyNames="ID" ClientIDMode="Predictable" 
        ClientIDRowSuffix="ID">
            <LayoutTemplate>
                <table runat="server" id="Table1" class="table tableViewClass tableClass colorTable">
                    <tr style="background-color: #EECFBA;">
                        <th>
                            Дата отпр.
                        </th>
                        <th>
                            Профиль
                        </th>
                        <th>
                            ID/стат.
                        </th>
                        <th>
                            Оцен/Согл + за дост.
                        </th>
                        <th>
                            За услугу
                        </th>
                        <th>
                            Оцен/Согл + за дост. - за усл.
                        </th>
                        <th style="width: 150px;">
                            Примечание
                        </th>
                        <th style="width: 150px;">
                            Ком. менеджера
                        </th>
                        <th>
                            DID
                        </th>
                    </tr>
                    <tr runat="server" id="itemPlaceholder"></tr>
                </table>
            </LayoutTemplate>
            <ItemTemplate>
                <asp:TableRow ID="Tr2" runat="server" CssClass='<%# OtherMethods.TicketColoredStatusRows(Eval("StatusID").ToString()) %>'>

                    <asp:TableCell id="Td4" runat="server">
                        <asp:Label ID="Label10" runat="server" Text='<%# OtherMethods.DateConvert(Eval("DeliveryDate").ToString()) %>' />
                    </asp:TableCell>

                    <asp:TableCell id="Td3" runat="server">
                        <asp:HyperLink ID="HyperLink2" runat="server" NavigateUrl='<%#"~/ManagerUI/Menu/Souls/ProfileEdit.aspx?id="+Eval("UserProfileID") %>'>
                            <asp:Label ID="Label5" runat="server" Text='<%# UsersProfilesHelper.UserProfileIDToFullFamilyOrCompanyname(Eval("UserProfileID").ToString()) %>' />
                        </asp:HyperLink>
                    </asp:TableCell>
                
                    <asp:TableCell id="Td13" runat="server">
                         <asp:HyperLink ID="HyperLink1" runat="server" NavigateUrl='<%#"~/ManagerUI/Menu/Tickets/UserTicketEdit.aspx?id="+Eval("SecureID") + "&page=calculation&" + OtherMethods.LinkBuilder(String.Empty, 
                        String.Empty, String.Empty, String.Empty, String.Empty, String.Empty ,stbDeliveryDate1.Text, stbDeliveryDate2.Text, String.Empty)%>'>
                            <asp:Label ID="lblSecureID" runat="server" Text='<%#Eval("SecureID") %>' />
                            <asp:HiddenField runat="server" ID="hfID" Value='<%# Eval("ID") %>'/>
                        </asp:HyperLink><br/>
                        <asp:Label ID="Label3" runat="server" Text='<%# OtherMethods.TicketStatusToText(Eval("StatusID").ToString()) %>' />
                    </asp:TableCell>
                
                    <asp:TableCell id="TableCell1" runat="server">
                        <asp:Label ID="lblAgreedAssessedDeliveryCosts" runat="server"  
                            Text='<%# MoneyMethods.MoneySeparator(MoneyMethods.AgreedAssessedDeliveryCosts(Eval("ID").ToString())) %>'></asp:Label>
                    </asp:TableCell>

                    <asp:TableCell id="Td5" runat="server">
                       <asp:Label ID="Label1" runat="server"  
                            Text='<%# MoneyMethods.MoneySeparator(Convert.ToDecimal(Eval("GruzobozCost"))) %>'></asp:Label>
                    </asp:TableCell>

                    <asp:TableCell id="Td8" runat="server">
                        <asp:Label ID="Label2" runat="server"  
                            Text='<%# MoneyMethods.MoneySeparator(Convert.ToDecimal(MoneyMethods.AgreedAssessedDeliveryCosts(Eval("ID").ToString())) - Convert.ToDecimal(Eval("GruzobozCost"))) %>'></asp:Label>
                    </asp:TableCell>
                
                    <asp:TableCell id="Td1" runat="server">
                        <div class="noteDiv" style="width: 100%; margin-bottom: 10px; font-weight: normal; font-style: normal; text-align: left;">
                            <asp:Label ID="lblStatusID" runat="server" Text='<%# Eval("Note").ToString() %>' />
                        </div>
                    </asp:TableCell>
                    
                    <asp:TableCell id="TableCell4" runat="server">
                        <div class="noteDiv" style="width: 100%; margin-bottom: 10px; font-weight: normal; font-style: normal; text-align: left;">
                            <asp:Label ID="Label7" runat="server" Text='<%# WebUtility.HtmlDecode(Eval("Comment").ToString()) %>'></asp:Label>
                        </div>
                    </asp:TableCell>
                    
                    <asp:TableCell id="TableCell3" runat="server" style="text-align: center;">
                        <asp:HyperLink ID="HyperLink3" runat="server" NavigateUrl='<%#"~/ManagerUI/Menu/Souls/DriverView.aspx?id="+Eval("DriverID") %>'>
                            <asp:Label ID="Label9" runat="server" Text='<%# DriversHelper.DriverIdToName(Eval("DriverID").ToString()) %>' />
                        </asp:HyperLink>
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
                <div style="width: 100%; text-align: center; padding: 15px; color: red">По данным критериям ничего не найдено либо не произведен поиск...</div>
            </EmptyDataTemplate>
        </asp:ListView>
        <div class="infoBlock" style="margin-bottom: 0;">
            <div class="pager">
                <asp:Label ID="lblPage" runat="server">Страница:</asp:Label>
                <asp:DataPager ID="lvDataPager" runat="server" PagedControlID="lvAllTickets" PageSize="100">
                    <Fields>
                        <asp:NumericPagerField ButtonType="Link" />
                    </Fields>
                </asp:DataPager>  
            </div>
        </div>
    </div>
    
    <div class="floatMenuLeft lblStatus" style="margin-left: 0">
        <asp:Label runat="server" ID="lblStatus"></asp:Label>
    </div>

</asp:Content>
