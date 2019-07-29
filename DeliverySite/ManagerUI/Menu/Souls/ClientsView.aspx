<%@ Page Title="" Language="C#" MasterPageFile="~/ManagerMasterPage.master" AutoEventWireup="true" CodeBehind="ClientsView.aspx.cs" Inherits="Delivery.ManagerUI.Menu.Souls.ClientsView" %>
<%@ Import Namespace="Delivery.BLL.Helpers" %>
<%@ Import Namespace="Delivery.BLL.StaticMethods" %>
<%@Register Tagprefix ="grb" Tagname="UserProfilesList" src="~/ManagerUI/Controls/Clients/UserProfilesList.ascx" %> 
<%@Register Tagprefix ="grb" Tagname="Comment" src="~/ManagerUI/Controls/Clients/Comment.ascx" %> 
<%@Register Tagprefix ="grb" Tagname="UserCategory" src="~/ManagerUI/Controls/Clients/UserCategory.ascx" %> 
<%@Register Tagprefix ="grb" Tagname="ContactDate" src="~/ManagerUI/Controls/Clients/ContactDate.ascx" %> 
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <style>
        .profiles-table-header th{
            border-radius: 0px !important;
            background-color: #ddd;
            color: #333;
            text-align: left;
            padding: 5px 10px;
        }
        .profiles {
            margin-top: -2px;
            margin-bottom: 25px;
        }

        .profiles td{
            text-align: left;
        }
    </style>
    <script type="text/javascript">
        $(function () {
            $("#<%= stbRecipientPhone.ClientID %>").mask("+375 (99) 999-99-99");
            $("#<%= stbDate1.ClientID %>")
            .datepicker({ nextText: "", prevText: "", changeMonth: true, changeYear: true })
            .mask("99-99-9999");
            $("#<%= stbDate2.ClientID %>")
                .datepicker({ nextText: "", prevText: "", changeMonth: true, changeYear: true })
                .mask("99-99-9999");
            $("#<%= stbContactDate.ClientID %>")
                .datepicker({ nextText: "", prevText: "", changeMonth: true, changeYear: true })
                .mask("99-99-9999");
        });
    </script>
   <asp:HyperLink ID="hlClientAdd" runat="server" CssClass="createItemHyperLink" NavigateUrl="~/ManagerUI/Menu/Souls/ClientCreate.aspx">Добавить клиента</asp:HyperLink>
   <h3 class="h3custom" style="margin-top: 0;">Клиенты</h3>
    <div>
        <asp:Panel runat="server" ID="SearchPanel" CssClass="mainFont" DefaultButton="btnSearch">
            <table class="table searchPanel" style="margin-left:auto; margin-right: auto; margin-bottom: 12px;">
                <tr>
                    <td style="vertical-align: top;">
                        
                        <table class="table">
                            <tr>
                                <td>
                                    UID:
                                </td>
                                <td>
                                    <asp:TextBox runat="server" ID="stbUID" CssClass="searchField form-control" Width="35px"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    Фам.:
                                </td>
                                <td>
                                    <asp:TextBox runat="server" ID="stbFamily" CssClass="searchField form-control" Width="135px"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    Комп.:
                                </td>
                                <td>
                                    <asp:TextBox runat="server" ID="stbCompanyName" CssClass="searchField form-control" Width="135px"></asp:TextBox>
                                </td>
                            </tr>
                        </table>
                    </td>
                    <td style="vertical-align: top;">
                       <table class="table">
                            <tr>
                                <td>
                                    Тел.:
                                </td>
                                <td>
                                    <asp:TextBox runat="server" ID="stbRecipientPhone" CssClass="searchField form-control" Width="135px"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    Email:
                                </td>
                                <td>
                                    <asp:TextBox runat="server" ID="stbEmail" CssClass="searchField form-control" Width="135px"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    Skype:
                                </td>
                                <td>
                                    <asp:TextBox runat="server" ID="stbSkype" CssClass="searchField form-control" Width="135px"></asp:TextBox>
                                </td>
                            </tr>
                        </table>
                    </td>
                    <td style="vertical-align: top;">
                       <table class="table">
                            <tr>
                                <td>
                                    Статус клиента:
                                </td>
                                <td>
                                    <asp:DropDownList runat="server" ID="sddlStatus" CssClass="searchField ddl-control" Width="155px"></asp:DropDownList>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    Статус проработки:
                                </td>
                                <td>
                                    <asp:DropDownList runat="server" ID="sddlStatusStudy" CssClass="searchField ddl-control" Width="155px"></asp:DropDownList>
                                </td>
                            </tr>
                           <tr>
                               <td>
                                    Отв. МПП:</td>
                                <td>
                                    <asp:DropDownList runat="server" ID="sddlSalesManager" CssClass="searchField ddl-control" Width="155px"></asp:DropDownList>
                                </td>
                           </tr>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td colspan="3" style="text-align: left;">
                        Дата прозвона:&nbsp;<asp:TextBox runat="server" ID="stbContactDate" CssClass="searchField form-control" Width="80px" EnableViewState="False"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    
                    <td colspan="3" style="text-align: right;">
                        <div>
                            <div style="float: left;" runat="server" ID="spnlStaticData">
                                <div style="text-align: left; font-size: 11px; color: #666;">
                                    параметры для выдачи стат. информации:
                                </div>
                                <div style="padding-left: 25px;">
                                    Отправлены с:&nbsp;<asp:TextBox runat="server" ID="stbDate1" CssClass="searchField form-control" Width="80px" EnableViewState="False"></asp:TextBox>
                                    &nbsp;по:&nbsp;<asp:TextBox runat="server" ID="stbDate2" CssClass="searchField form-control" Width="80px" EnableViewState="False"></asp:TextBox>
                                </div>
                                <asp:HiddenField runat="server" ID="hfDeliveryDate1"/>
                                <asp:HiddenField runat="server" ID="hfDeliveryDate2"/>
                            </div>
                            <div style="float: right; margin-top: 11px;">
                                <asp:Button runat="server" Text="Сброс" CssClass="btn btn-default" ID="btnReload"/>
                                <asp:Button runat="server" Text="Искать" CssClass="btn btn-default" ID="btnSearch"/>    
                                &nbsp;&nbsp;&nbsp;<i class="small-informer">Найдено:<b> <asp:Label runat="server" ID="lblAllResult"></asp:Label></b></i> 
                            </div>
                        </div>
                    </td>
                </tr>
            </table>
        </asp:Panel>
        
        <asp:HiddenField runat="server" ID="hfSortDirection" EnableViewState="False"/>
        <asp:HiddenField runat="server" ID="hfSortExpression" EnableViewState="False"/>
        
        <div style="font-size: 11px" runat="server" ID="lblStaticData">
            <b>*</b> - статистические данные
        </div>

        <asp:ListView runat="server" ID="lvAllClients"  DataKeyNames="ID" ClientIDMode="Predictable" 
        ClientIDRowSuffix="ID">
            <LayoutTemplate>
                <table runat="server" id="tableResult" class="table tableBorderRadius tableViewClass tableClass allClients">
                    <tr>
                        <th style="width: 30px;">
                            <asp:LinkButton runat="server" Text="UID" ID="lbsID" CommandArgument="ID"  OnClick="lvAllClients_OnSortingButtonClick"/>
                        </th>
                        <th style="width: 100px;">
                            ФИО
                        </th>
                        <th style="width: 100px;">
                            Отв. МПП
                        </th>
                    
                        <th style="width: 150px;">
                            Категории тов.
                        </th>

                        <th  style="width: 110px;">
                            Конт. данные
                        </th>
                    
                        <th style="width: 110px;" runat="server" ID="thStatInfoSegment">
                            Сегмент*
                        </th>
                    
                        <th  style="width: 60px;" runat="server" ID="thStatInfoCount">
                            <asp:LinkButton runat="server" Text="К-во заявок*" ID="lbsTicketsCount" CommandArgument="TicketsCount"  OnClick="lvAllClients_OnSortingButtonClick"/>
                        </th>
                    
                        <th  style="width: 60px;" runat="server" ID="thStatInfoAverage">
                            <asp:LinkButton runat="server" Text="Ср. стоим. заявки*" ID="lbsAverageCount" CommandArgument="AverageCost"  OnClick="lvAllClients_OnSortingButtonClick"/>
                        </th>
                    
                        <th  style="width: 60px;" runat="server" ID="thStatInfoSumm">
                            <asp:LinkButton runat="server" Text="Выручка*" ID="lbsSummGruzobozCost" CommandArgument="SummGruzobozCost"  OnClick="lvAllClients_OnSortingButtonClick"/>
                        </th>
                    
                        <th  style="width: 160px;">
                            Комментарий
                        </th>

                        <th  style="width: 110px;">
                            Дата сл. контакта
                        </th>

                        <th  style="width: 110px;">
                            Статус проработки
                        </th>

                        <th>
                            Статус клиента
                        </th>
                    
                    </tr>
                    <tr runat="server" id="itemPlaceholder"></tr>
                </table>
            </LayoutTemplate>
            <ItemTemplate>
                <asp:TableRow 
                        id="Tr2" 
                        runat="server"
                        CssClass='<%# String.Format("{0} {1}",
                                        OtherMethods.SpecialClientTrClass(Eval("SpecialClient").ToString()), 
                                        UsersHelper.UserColoredStatusRows(Eval("Status").ToString()))%>'>
                
                    <asp:TableCell id="Td7" runat="server">
                        <asp:HyperLink ID="HyperLink2" runat="server" NavigateUrl='<%#"~/ManagerUI/Menu/Souls/ClientEdit.aspx?id="+Eval("ID") %>'>
                            <asp:Label ID="lblID" runat="server" Text='<%#Eval("ID") %>' />
                        </asp:HyperLink>
                    </asp:TableCell>

                    <asp:TableCell id="TableCell1" runat="server">
                        <asp:HyperLink ID="HyperLink4" runat="server" NavigateUrl='<%#"~/ManagerUI/Menu/Souls/ClientEdit.aspx?id="+Eval("ID") %>'>
                            <asp:Label ID="lblName" runat="server" Text='<%#Eval("Family") %>' />&nbsp;
                            <asp:Label ID="lblRegion" runat="server" Text='<%#Eval("Name") %>' />
                        </asp:HyperLink>
                    </asp:TableCell>
                
                    <asp:TableCell id="TableCell2" runat="server">
                        <asp:HyperLink ID="HyperLink1" runat="server" NavigateUrl='<%#"~/ManagerUI/Menu/Souls/ManagerEdit.aspx?id="+Eval("SalesManagerID") %>'>
                            <asp:Label ID="Label1" runat="server" Text='<%# UsersHelper.SalesManagerName( Eval("SalesManagerID").ToString()) %>' />
                        </asp:HyperLink>
                    </asp:TableCell>
                
                
                    <asp:TableCell id="TableCell4" runat="server">
                        <grb:UserCategory 
                            ID="UserCategory" 
                            runat="server" 
                            ClientID='<%# Eval("ID").ToString() %>'/>
                    </asp:TableCell>

                
                    <asp:TableCell id="TableCell6" runat="server" style="text-align: left;">
                        <div style="font-weight: bold;">
                            <asp:Label ID="lblPhone" runat="server" Text='<%#Eval("Phone") %>' />
                        </div>
                        <div>
                            <a href='<%# string.Format("mailto:{0}",Eval("Email")) %>'>
                                <asp:Label ID="lblTrack" runat="server" Text='<%#Eval("Email") %>' />
                            </a>
                        </div>
                        <div>
                            <asp:Label ID="lblSkype" runat="server" Text='<%#Eval("Skype") %>' />
                        </div>
                    </asp:TableCell>

                
                    <asp:TableCell id="tdStatInfoSegment" runat="server">
                        <asp:Label ID="Label5" Text='<%# UsersHelper.Segment(Eval("TicketsCount").ToString(), Eval("SummGruzobozCost").ToString()) %>' runat="server" />
                    </asp:TableCell>

                
                    <asp:TableCell id="tdStatInfoCount" runat="server">
                        <asp:HyperLink runat="server" NavigateUrl='<%# string.Format("~/ManagerUI/Menu/Tickets/UserTicketView.aspx?stateSave=true&uid={0}&deliveryDate1={1}&deliveryDate2={2}", Eval("ID"), hfDeliveryDate1.Value, hfDeliveryDate2.Value) %>'>
                            <asp:Label ID="lblLinkedTicketCount" Text='<%# UsersHelper.TicketsCountFormater(Eval("TicketsCount").ToString()) %>' runat="server" />
                        </asp:HyperLink>
                    </asp:TableCell>

                
                    <asp:TableCell id="tdStatInfoAverage" runat="server">
                        <asp:Label ID="Label4" Text='<%# MoneyMethods.MoneySeparator(Eval("AverageCost").ToString()) %>' runat="server" />
                    </asp:TableCell>

                
                    <asp:TableCell id="tdStatInfoSumm" runat="server">
                        <asp:Label ID="Label8" Text='<%# MoneyMethods.MoneySeparator(Eval("SummGruzobozCost").ToString()) %>' runat="server" />
                    </asp:TableCell>


                     <asp:TableCell id="TableCell7" runat="server">
                        <grb:Comment 
                            ID="Comment" 
                            runat="server" 
                            ClientID='<%# Eval("ID").ToString() %>'
                            CommentValue='<%# Eval("Note").ToString() %>'
                            ListViewControlFullID='#ctl00_MainContent_lvAllClients'
                            PageName = "ClientsView"/>
                    </asp:TableCell>
                    

                    <asp:TableCell id="TableCell12" runat="server">
                        <grb:ContactDate 
                            ID="ContactDate" 
                            runat="server" 
                            ClientID='<%# Eval("ID").ToString() %>'
                            ClientStatus='<%# Eval("Status").ToString() %>'
                            DateValue='<%# Eval("ContactDate").ToString() %>'
                            ListViewControlFullID='#ctl00_MainContent_lvAllClients'
                            PageName = "ClientsView"/>
                    </asp:TableCell>

                
                    <asp:TableCell id="TableCell14" runat="server">
                        <asp:Label ID="Label2" runat="server" Text='<%# UsersHelper.UserStatusStadyToText(Convert.ToInt32(Eval("StatusStady")))%>' />
                    </asp:TableCell>

                    <asp:TableCell id="TableCell15" runat="server">
                        <asp:Label ID="Label3" runat="server" Text='<%# UsersHelper.UserStatusToText(Convert.ToInt32(Eval("Status")))%>' />
                    </asp:TableCell>
                </asp:TableRow>
                <asp:TableRow id="Td19" runat="server">
                    <asp:TableCell id="TableCell16" runat="server" ColumnSpan="15" style="padding: 0; border: 0;">
                        <grb:UserProfilesList 
                            ID="UserProfilesList" 
                            runat="server" 
                            UserId='<%# Eval("ID").ToString() %>'
                            Status='<%# Eval("Status").ToString() %>'/>
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
                <div style="width: 100%; text-align: center; padding: 15px; color: red">По данным критериям ничего не найдено...</div>
            </EmptyDataTemplate>
        </asp:ListView>
        <div class="infoBlock" style="margin-bottom: 0;">
            <div class="pager">
                <asp:Label ID="lblPage" runat="server">Страница:</asp:Label>
                <asp:DataPager ID="lvDataPager" runat="server" PagedControlID="lvAllClients" PageSize="50">
                    <Fields>
                        <asp:NumericPagerField ButtonType="Link" />
                    </Fields>
                </asp:DataPager>  
            </div>
        </div>
    </div>
</asp:Content>
