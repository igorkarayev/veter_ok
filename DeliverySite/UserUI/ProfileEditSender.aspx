<%@ Page Title="" Language="C#" MasterPageFile="~/UserMasterPage.master" AutoEventWireup="true" CodeBehind="ProfileEditSender.aspx.cs" Inherits="DeliverySite.UserUI.ProfileEditSender" %>
<%@ Import Namespace="Delivery.BLL.StaticMethods" %>
<%@ Import Namespace="Delivery.BLL.Helpers" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <style>        
         .profileVisible {
             
         }

         .profileNotVisible {
             display: none;
         }

         .autocomplete-suggestions {
             width: 700px !important;
         }         
    </style>

    <h3 class="h3custom" style="margin-top: 0;"><%= actionText %> профиля контрагента <%= profileName %></h3>

    <table class="tableClass" id="tableProfile"  style="margin: auto; width: 43%;">        
        <tr>
            <td>
                Новый профиль 
            </td>
        </tr>
        <tr>
            <td>
                <asp:TextBox runat="server" ID="tbNewProfile" Width="80%" CssClass="form-control"/>
                <asp:Label ForeColor="Red" Visible="false" ID="lbError" Text="Введите название профиля" runat="server"></asp:Label>
            </td>            
        </tr>
    </table>

    <table id="tableCreateProfile" class="tableClass" style="margin: auto; width: 43%;">
        <tr>
            <td class="paddingTable valignTable citybox" style="width: 100%; border-top: 3px solid #ebeef2; padding-top: 10px;" colspan="2">
                <div style="margin-bottom: 10px;">
                    Отправляем в (адрес пункта разгрузки):   
                </div>
                <asp:TextBox runat="server" ID="tbCity" Width="80%" CssClass="form-control"/>
                <div class="notVisible" id="surcharge">
                    доплата за отклонение:
                    <asp:Label runat="server" style="font-weight: bold; margin-right: 20px;" ID="lblCityCost"/>
                </div>
                <div class="notVisible" id="delivery-days">
                    дни доставки (с нашего склада): <asp:Label runat="server" style="font-weight: bold;" ID="lblCityDeliveryDate"/>
                </div>
                <div class="notVisible" id="delivery-terms">
                    срок доставки (с нашего склада):
                    <asp:Label runat="server" style="font-weight: bold; margin-right: 20px;" ID="lblCityDeliveryTerms"/>
                </div>
                <asp:HiddenField runat="server" ID="hfCityID"/>
            </td>
        </tr>
            
        <tr>
            <td class="valignTable" style="padding-top: 7px;">
                <span style="float: right;">Адрес доставки:</span>
            </td>
            <td class="paddingTable">
                <table>
                    <tr>
                        <td>
                            <asp:DropDownList ID="ddlRecipientStreetPrefix" Width="50px" runat="server" CssClass="ddl-control" style="padding: 0; height: 28px;">
                                <asp:listitem text="ул." value="ул."/>
                                <asp:listitem text="аллея" value="аллея"/>
                                <asp:listitem text="бул." value="бул."/>
                                <asp:listitem text="дор." value="дор."/>
                                <asp:listitem text="линия" value="линия"/>
                                <asp:listitem text="маг." value="маг."/>
                                <asp:listitem text="мик-н" value="мик-н"/>
                                <asp:listitem text="наб." value="наб."/>
                                <asp:listitem text="пер." value="пер."/>
                                <asp:listitem text="пл." value="пл."/>
                                <asp:listitem text="пр." value="пр."/>
                                <asp:listitem text="пр-кт" value="пр-кт"/>
                                <asp:listitem text="ряд" value="ряд"/>
                                <asp:listitem text="тракт" value="тракт"/>
                                <asp:listitem text="туп." value="туп."/>
                                <asp:listitem text="ш." value="ш."/>
                            </asp:DropDownList>
                        </td>
                        <td>
                            <asp:TextBox ID="tbRecipientStreet" runat="server" width="130px" CssClass="form-control"/>
                        </td>
                    </tr>
                    <tr>
                        <td>дом: </td>
                        <td><asp:TextBox ID="tbRecipientStreetNumber" runat="server" width="25px" CssClass="form-control"/></td>
                    </tr>
                    <tr>
                        <td style="width: 50px;">
                            <span>корпус:</span>
                        </td>
                        <td>
                            <asp:TextBox ID="tbRecipientKorpus" runat="server" width="15px" CssClass="form-control"/>
                        </td>
                    </tr>
                    <tr>
                        <td>квартира: </td>
                        <td><asp:TextBox ID="tbRecipientKvartira" runat="server" width="25px" CssClass="form-control"/></td>
                    </tr>
                </table>
            </td>
        </tr>

        <tr>
            <td colspan="2" style="width: 100%; border-top: 3px solid #ebeef2; padding-top: 10px;">
                    
            </td>
        </tr>
        <tr>
            <td class="valignTable" >
                <span style="float: right;">Дата отправки:</span>
            </td>
            <td class="paddingTable">
                <asp:TextBox ID="tbDeliveryDate" runat="server" style="width: 75px" CssClass="form-control"/>
            </td>
        </tr>            
        <tr>
            <td class="valignTable">
                <span style="float: right;">Телефоны получателя:</span>
            </td>
            <td class="paddingTable" style="padding-left: 10px;">
                <div style="padding-bottom: 5px;">
                    #1: <asp:TextBox ID="tbRecipientPhone" runat="server" Width="120px" CssClass="form-control"/>
                </div>
                <div>
                    #2: <asp:TextBox ID="tbRecipientPhone2" runat="server" Width="120px" CssClass="form-control"/> 
                </div>
            </td>
        </tr>
        <tr>
            <td class="valignTable">
                <span>Фамилия получателя:</span>
            </td>
            <td class="paddingTable">
                <asp:TextBox ID="tbRecipientFirstName" runat="server" width="95%" CssClass="form-control"/>
            </td>
        </tr>
        <tr>
            <td class="valignTable">
                <span>Имя получателя:</span>
            </td>
            <td class="paddingTable">
                <asp:TextBox ID="tbRecipientLastName" runat="server" width="95%" CssClass="form-control"/>
            </td>
        </tr>
        <tr>
            <td class="valignTable">
                <span>Отчество получателя:</span>
            </td>
            <td class="paddingTable">
                <asp:TextBox ID="tbRecipientThirdName" runat="server" width="95%" CssClass="form-control"/>
            </td>
        </tr>
        <tr>
            <td>

            </td>
            <td style="text-align: center; padding-left: 130px;">
                <asp:Button ID="btnCreate" runat="server" CssClass="btn btn-default" ValidationGroup="LoginGroup"/>
            </td>
        </tr>
    </table>   
    
    <script>
        var availableUserProfiles = <%= AvailableUserProfiles %>;        

        $(function () {
            $("#tableProfile").addClass("<%= tableProfileClass %>");            

            /** Инициализация масок и виджетов **/
            $.datepicker.setDefaults($.datepicker.regional['ru']);
            $("#<%= tbRecipientPhone.ClientID %>").mask("+375 (99) 999-99-99");
            $("#<%= tbRecipientPhone2.ClientID %>").mask("+375 (99) 999-99-99");
            $(".moneyMask").maskMoney();
            $("#<%= tbDeliveryDate.ClientID %>").datepicker({
                nextText: "",
                prevText: "",
                changeMonth: true,
                changeYear: true,
                /*beforeShowDay: function (date) {
                    var dayOfWeek = date.getDay();
                    return $.inArray(dateStr,forbiddeneDates) == -1;
                }*/

            }).mask("99-99-9999");


            /** Autocomplete для городов СТАРТ **/
            $('#<%= tbCity.ClientID%>').autocomplete({
                minChars: '2',
                params: {
                    appkey: '<%= AppKey%>'
                },
                serviceUrl: '../AppServices/AutocompliteAPI.asmx/GetCityForAutocompliteJSON',
                onSelect: function (suggestion) {
                    $('#<%= hfCityID.ClientID%>').val(suggestion.data);
                    $('#<%= lblCityCost.ClientID%>').html(suggestion.cost);
                    $('#<%= lblCityDeliveryDate.ClientID%>').html(suggestion.deliverydate);
                    $('#<%= lblCityDeliveryTerms.ClientID%>').html(suggestion.deliveryterms);

                    if ($('#<%= lblCityCost.ClientID%>').html().length != 0) {
                        $('#surcharge').show();
                        $('#<%= lblCityCost.ClientID%>').show();
                    } else {
                        $('#surcharge').hide();
                        $('#<%= lblCityCost.ClientID%>').hide();
                    }

                    if ($('#<%= lblCityDeliveryDate.ClientID%>').html().length !== 0) {
                        $('#delivery-days').show();
                        $('#<%= lblCityDeliveryDate.ClientID%>').show();
                    } else {
                        $('#delivery-days').hide();
                        $('#<%= lblCityDeliveryDate.ClientID%>').hide();
                    }


                    if ($('#<%= lblCityDeliveryTerms.ClientID%>').html().length != 0) {
                        $('#delivery-terms').show();
                        $('#<%= lblCityDeliveryTerms.ClientID%>').show();
                    } else {
                        $('#delivery-terms').hide();
                        $('#<%= lblCityDeliveryTerms.ClientID%>').hide();
                    }

                    if ($('#<%= tbCity.ClientID%>').val().trim().length == 0 || $("#<%= hfCityID.ClientID%>").val().length == 0) {
                        $('#<%= tbCity.ClientID%>').css("background-color", "#ffdda8");
                    } else {
                        $('#<%= tbCity.ClientID%>').css("background-color", "white");
                    }
                }
            });            

            if ($('#<%= lblCityCost.ClientID%>').html().length != 0) {
                $('#surcharge').show();
                $('#<%= lblCityCost.ClientID%>').show();
            } else {
                $('#surcharge').hide();
                $('#<%= lblCityCost.ClientID%>').hide();
            }

            if ($('#<%= lblCityDeliveryDate.ClientID%>').html().length != 0) {
                $('#delivery-days').show();
                $('#<%= lblCityDeliveryDate.ClientID%>').show();
            } else {
                $('#delivery-days').hide();
                $('#<%= lblCityDeliveryDate.ClientID%>').hide();
            }


            if ($('#<%= lblCityDeliveryTerms.ClientID%>').html().length != 0) {
                $('#delivery-terms').show();
                $('#<%= lblCityDeliveryTerms.ClientID%>').show();
            } else {
                $('#delivery-terms').hide();
                $('#<%= lblCityDeliveryTerms.ClientID%>').hide();
            }
        });
    </script>
</asp:Content>

