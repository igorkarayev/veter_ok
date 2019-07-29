<%@ Page MaintainScrollPositionOnPostback="true" Language="C#" MasterPageFile="~/UserMasterPage.master" AutoEventWireup="true" CodeBehind="TicketView.aspx.cs" Inherits="Delivery.UserUI.TicketView" %>
<%@ Import Namespace="Delivery.BLL" %> <%@ Import Namespace="Delivery.BLL.Helpers" %> <%@ Import Namespace="Delivery.BLL.StaticMethods" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div>
        <asp:HyperLink ID="hlCreateNewTicket" CssClass="createItemHyperLink" runat="server" NavigateUrl="~/UserUI/TicketEdit.aspx">Создать новую заявку</asp:HyperLink>
        <h3 class="h3custom" style="margin-top: 0;">Ваши заявки</h3>
    </div>
    <div>
        <asp:Panel runat="server" ID="pnlSearchPanel" CssClass="mainFont" DefaultButton="btnSearch">
            <table class="searchPanel" style="margin-left:auto; margin-right: auto; margin-bottom: 15px;">
                <tr>
                    <td>
                        ID:
                    </td>
                    <td>
                        <asp:TextBox runat="server" ID="stbID" CssClass="searchField form-control" AccessKey="u"></asp:TextBox>
                    </td>
                    <td>
                        Статус:
                    </td>
                    <td>
                        <asp:DropDownList runat="server" ID="sddlStatus" CssClass="searchField ddl-control" AccessKey="i" Width="146px"></asp:DropDownList>
                    </td>
                    <td colspan="3" style="text-align: right">
                        <asp:Button runat="server" Text="Сброс" CssClass="btn btn-default" ID="btnReload"/>
                        <asp:Button runat="server" Text="Искать" CssClass="btn btn-default" ID="btnSearch"/> 
                        &nbsp;&nbsp;&nbsp;<i class="small-informer">Найдено:<b> <asp:Label runat="server" ID="lblAllResult"></asp:Label></b></i>    
                    </td>
                </tr>
                <tr>
                    <td colspan="3" style="text-align: left;">
                        Отправка &nbsp;с:&nbsp;<asp:TextBox runat="server" ID="stbDeliveryDate1" CssClass="searchField form-control" Width="80px" EnableViewState="False"></asp:TextBox>
                        &nbsp;по:&nbsp;<asp:TextBox runat="server" ID="stbDeliveryDate2" CssClass="searchField form-control" Width="80px" EnableViewState="False"></asp:TextBox>включ.
                    </td>
                </tr>
                <tr>
                    <td colspan="4">
                        Отпр. из:
                        <asp:TextBox runat="server" ID="stbSenderCity" CssClass="searchField form-control" style="width: 83%" AccessKey="c"></asp:TextBox>
                        <asp:HiddenField runat="server" ID="shfSenderCityID"/>
                    </td>
                </tr>
                <tr>
                    <td colspan="4">
                        Отпр. в:&nbsp;&nbsp;
                        <asp:TextBox runat="server" ID="stbCity" CssClass="searchField form-control" style="width: 83%" AccessKey="c"></asp:TextBox>
                        <asp:HiddenField runat="server" ID="shfCityID"/>
                    </td>
                </tr>
            </table>
        </asp:Panel>
        
        <asp:Panel CssClass="floatMenu" runat="server" style="padding: 0" ID="Panel2">
            <asp:Button runat="server" Text="Печать актов приема-передачи для выделенных заявок на А4" CssClass="btn btn-default printButtonTop" ID="btnPrintActORT2"/> 
            <asp:Button runat="server" Text="Печать актов возврата для выделенных заявок на А4" CssClass="btn btn-default printButtonTop" ID="btnPrintReturn1"/> 
        </asp:Panel>        

        <asp:Label runat="server" ID="lblError" ForeColor="red"></asp:Label>

        <asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager>

    <asp:UpdatePanel runat="server" ID="updatePanelCheckBoxes" UpdateMode="Conditional">
        <ContentTemplate>
            <asp:ListView runat="server" ID="lvAllTickets">
            <LayoutTemplate>
                <table runat="server" id="Table1" class="tableViewClass tableClass colorTable">
                    <tr class="table-header">
                        <th>
                            <asp:CheckBox ID="chkboxSelectAll" runat="server"/>
                        </th>
                        <th>
                            <asp:LinkButton Text="ID" runat="server" ID="SortBySecureID" CommandArgument="SortBySecureID">
                            
                            </asp:LinkButton>
                        </th>
                        <th>
                            Наименование груза
                        </th>
                        <th>
                            Контрагент
                        </th>
                        <th>                          
                            Стоимость с дост.
                        </th>
                        <th>
                            <asp:LinkButton Text="За услугу" runat="server" ID="SortByGruzobozCost" CommandArgument="SortByGruzobozCost">
                            
                            </asp:LinkButton>                        
                        </th>
                        <th>
                            <asp:LinkButton Text="Тип профиля" runat="server" ID="SortByType" CommandArgument="SortByType">
                            
                            </asp:LinkButton>                        
                        </th>
                        <th>
                            <asp:LinkButton Text="Дата создания" runat="server" ID="SortByCreateDate" CommandArgument="SortByCreateDate">
                            
                            </asp:LinkButton>                          
                        </th>
                         <th>
                            <asp:LinkButton Text="Дата приема" runat="server" ID="SortByAdmissionDate" CommandArgument="SortByAdmissionDate">
                            
                            </asp:LinkButton>  
                        </th>
                        <th>
                            <asp:LinkButton Text="Дата отправки" runat="server" ID="SortByDeliveryDate" CommandArgument="SortByDeliveryDate">
                            
                            </asp:LinkButton>
                        </th>
                        <th>
                            <asp:LinkButton Text="Город получателя" runat="server" ID="SortByCity" CommandArgument="SortByCity">
                            
                            </asp:LinkButton>
                        </th>
                        <th>
                            <asp:LinkButton Text="Статус" runat="server" ID="SortByStatus" CommandArgument="SortByStatus">
                            
                            </asp:LinkButton>
                        </th>
                        <th>
                            Удалить
                        </th>
                    </tr>
                    <tr runat="server" id="itemPlaceholder"></tr>
                </table>
            </LayoutTemplate>

            <ItemTemplate>            
                <asp:TableRow id="Tr2" runat="server" CssClass='<%# 
                    OtherMethods.TicketColoredStatusRows(
                        TicketsHelper.StatusReplacement(
                            TicketsHelper.DeferredProcessedStatus(Eval("StatusID").ToString(), Eval("StatusIDOld").ToString(), Eval("ProcessedDate").ToString())
                        )
                    ) %>'>
                    <asp:TableCell id="TableCell2" runat="server">                    
                        <asp:CheckBox ID="cbSelect" AutoPostBack="true" runat="server" CssClass="selectItem" ViewStateMode="Enabled" OnCheckedChanged="chkBox_OnCheckChange"/>                                        
                    </asp:TableCell>
                
                    <asp:TableCell id="Td4" runat="server">
                        <asp:HyperLink ID="LinkButton1" runat="server" NavigateUrl='<%#"~/UserUI/TicketEdit.aspx?id="+ OtherMethods.SecureIDOrFullSecureID(Eval("ID").ToString()) %>'>
                            <b><asp:Label ID="lblSecureID" runat="server" Text='<%#Eval("SecureID") %>' /></b>
                        </asp:HyperLink>
                        <asp:HiddenField runat="server" ID="hfID" Value='<%# Eval("SecureID") %>'/>
                        <asp:HiddenField runat="server" ID="hfIDdef" Value='<%# Eval("ID") %>'/>
                    </asp:TableCell>
                
                    <asp:TableCell id="Td2" runat="server">
                        <asp:Label ID="lblGoodsDescription" runat="server" Text='<%# OtherMethods.GoodsStringFromTicketID(Eval("ID").ToString()) %>' />
                    </asp:TableCell>

                    <asp:TableCell id="TableCell4" runat="server">
                        <asp:Label ID="lblProfileName" runat="server" Text='<%# DeliverySite.BLL.Helpers.SenderProfilesHelper.GetProfileNameByID(Eval("SenderProfileID").ToString()) %>' />
                    </asp:TableCell>
                
                    <asp:TableCell id="tcCost" runat="server">
                        <asp:Label ID="lbCost" runat="server" Text='<%# Eval("OverallCost") %>' />
                    </asp:TableCell>
                
                    <asp:TableCell id="tcDeliveryCost" runat="server">
                        <asp:Label ID="lbDeliveryCost" runat="server" Text='<%# Eval("GruzobozCost").ToString() %>' />
                    </asp:TableCell>

                    <asp:TableCell id="tcDeliveryType" runat="server">
                        <asp:Label ID="lbDeliveryType" runat="server" Text='<%# Eval("ProfileType").ToString() %>' />
                    </asp:TableCell>
                
                    <asp:TableCell id="Td7" runat="server">
                        <asp:Label ID="Label3" runat="server" Text='<%# OtherMethods.DateConvert(Eval("CreateDate").ToString()) %>' />
                    </asp:TableCell>
                
                    <asp:TableCell id="TableCell1" runat="server">
                        <asp:Label ID="Label1" runat="server" Text='<%# OtherMethods.DateConvert(Eval("AdmissionDate").ToString()) %>' />
                    </asp:TableCell>

                    <asp:TableCell id="Td0" runat="server">
                        <asp:Label ID="lblDeliveryDate" runat="server" Text='<%# OtherMethods.DateConvert(Eval("DeliveryDate").ToString()) %>' />
                    </asp:TableCell>

                    <asp:TableCell id="TableCell3" runat="server">
                        <asp:Label ID="lblSenderCity" runat="server" Text='<%# CityHelper.CityIDToCityName(Eval("CityID").ToString()) %>' />
                    </asp:TableCell>

                    <asp:TableCell id="Td1" runat="server">
                        <asp:Label ID="lblStatusText" runat="server" Text='<%# 
                        OtherMethods.TicketStatusToText(
                            TicketsHelper.StatusReplacement(
                                TicketsHelper.DeferredProcessedStatus(Eval("StatusID").ToString(), Eval("StatusIDOld").ToString(), Eval("ProcessedDate").ToString())
                            )
                        ) %>' />
                    </asp:TableCell>
                
                    <asp:TableCell id="Td3" runat="server"  style="font-size: 12px;">
                         <asp:LinkButton ID="lbDelete" runat="server" OnClick="lbDelete_Click" CommandArgument='<%#Eval("ID") %>'>Удалить</asp:LinkButton>
                        <asp:HiddenField runat="server" ID="lblStatusID" Value='<%# Eval("StatusID") %>'></asp:HiddenField>
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
                <div style="width: 100%; text-align: center; padding: 15px; color: red">Ни одной заявки не найдено...</div>
            </EmptyDataTemplate>
        </asp:ListView>

        <div style="margin-left: 360px; display: inline-block; width: 60px; text-align: center;">
            <asp:Label runat="server" ID="CheckedLbCost" Text='<%# MoneyMethods.MoneySeparator(CheckedLbCostSum) %>'></asp:Label>
        </div>
    
        <div style="display: inline-block; width: 60px; padding-left: 20px; text-align: center;">
            <asp:Label runat="server" ID="CheckedDeliveryCost" Text='<%# MoneyMethods.MoneySeparator(CheckedDeliveryCostSum) %>'></asp:Label>
        </div>

        </ContentTemplate>    
    </asp:UpdatePanel> 

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
    
    <asp:Panel CssClass="floatMenu" runat="server" style="padding: 0" ID="PrintButtons1">
        <asp:Button runat="server" Text="Печать наклеек для выделенных заявок на А4" CssClass="btn btn-default" ID="btnPrintVinil"/> 
        <asp:Button runat="server" Text="Печать актов приема-передачи для выделенных заявок на А4" CssClass="btn btn-default" ID="btnPrintActORT1"/>
        <asp:Button runat="server" Text="Печать актов возврата для выделенных заявок на А4" CssClass="btn btn-default" ID="btnPrintReturn2"/> 
        <asp:Button runat="server" Text="Печать наклеек для термопринтера" CssClass="btn btn-default" ID="btnPrintVinilTermo"/> 
    </asp:Panel>

    <script> 
        $(function() {
            $('#ctl00_MainContent_lvAllTickets_chkboxSelectAll').change(function () {
                var checkboxes = $(this).closest('table').find('.selectItem input:checkbox');
                if ($(this).is(':checked')) {
                    checkboxes.attr('checked', 'checked');
                } else {
                    checkboxes.removeAttr('checked');
                }
            });

            $("#<%= stbDeliveryDate1.ClientID %>")
                .datepicker({ nextText: "", prevText: "", changeMonth: true, changeYear: true })
                .mask("99-99-9999");
            $("#<%= stbDeliveryDate2.ClientID %>")
                .datepicker({ nextText: "", prevText: "", changeMonth: true, changeYear: true })
                .mask("99-99-9999");

            /** Autocomplete для городов СТАРТ **/
            $('#<%= stbCity.ClientID%>').autocomplete({
                minChars: '2',
                params: {
                    appkey: '<%= AppKey%>'
                },
                serviceUrl: '../../../AppServices/AutocompliteAPI.asmx/GetCityForAutocompliteJSON',
                onSelect: function (suggestion) {
                    $('#<%= shfCityID.ClientID%>').val($('#<%= shfCityID.ClientID%>').val() + ";" + suggestion.data);

                },
                delimiter: ";"
            });

            $('#<%= stbCity.ClientID%>').click(function () {
                if ($(this).val().trim() == "") {
                    $('#<%= shfCityID.ClientID%>').val("");
                }
            });

            $('#<%= stbCity.ClientID%>').keyup(function () {
                if ($(this).val().trim() == "") {
                    $('#<%= shfCityID.ClientID%>').val("");
                }
            });


            $('#<%= stbSenderCity.ClientID%>').autocomplete({
                minChars: '2',
                params: {
                    appkey: '<%= AppKey%>'
                },
                serviceUrl: '../../../AppServices/AutocompliteAPI.asmx/GetCityForAutocompliteJSON',
                onSelect: function (suggestion) {
                    $('#<%= shfSenderCityID.ClientID%>').val($('#<%= shfSenderCityID.ClientID%>').val() + ";" + suggestion.data);

                },
                delimiter: ";"
            });

            $('#<%= stbSenderCity.ClientID%>').click(function () {
                if ($(this).val().trim() == "") {
                    $('#<%= shfSenderCityID.ClientID%>').val("");
                }
            });

            $('#<%= stbSenderCity.ClientID%>').keyup(function () {
                if ($(this).val().trim() == "") {
                    $('#<%= shfSenderCityID.ClientID%>').val("");
                }
            });
            /** Autocomplete для городов КОНЕЦ **/
        });
        
        $(function () {
            $(window).scroll(function () {
                var top = $(document).scrollTop();
                if (top > 300 && $(".tableViewClass").height() >= $(window).height())
                    $(".floatMenu").css({ top: '0', position: 'fixed', borderBottom: '#025070 2px solid', borderRight: '#025070 2px solid', borderLeft: '#025070 2px solid', backgroundColor: '#0C72AF', borderRadius: '0 0 10px 10px', padding: '10px' });
                else
                    $(".floatMenu").css({ position: 'relative', border: 'none', padding: '0px', borderRadius: '0', backgroundColor: 'transparent' });
            });
        });
    </script>

    <style>
        .printButtonTop{
           margin-bottom: 10px;
        }
    </style>

</asp:Content>
