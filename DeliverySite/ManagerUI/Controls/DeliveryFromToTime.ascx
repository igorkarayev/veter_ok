<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="DeliveryFromToTime.ascx.cs" Inherits="Delivery.ManagerUI.Controls.DeliveryFromToTime" %>
<%@ Import Namespace="Delivery.BLL.StaticMethods" %>
<script>
    $(function () {
        $('#<%= tbOvdFrom.ClientID %>').timepicker({ 'timeFormat': 'H:i' });
        $('#<%= tbOvdTo.ClientID %>').timepicker({ 'timeFormat': 'H:i' });
        $('#<%= tbOvdFrom.ClientID %>').change(function() {
            <%# String.Format("SaveDeliveryDateFrom({0}, \"{1}\", \"{2}\", \"{3}\", \"{4}\", \"{5}\");", CityRowId, AppKey, ListViewControlFullID, UserID, UserIP, PageName) %>  //сохраняем значение
        });

        $('#<%= tbOvdTo.ClientID %>').change(function () {
            <%# String.Format("SaveDeliveryDateTo({0}, \"{1}\", \"{2}\", \"{3}\", \"{4}\", \"{5}\");", CityRowId, AppKey, ListViewControlFullID, UserID, UserIP, PageName) %>  //сохраняем значение
        });
    });
</script>
с  <asp:TextBox runat="server" ID="tbOvdFrom" CssClass="time" Text='<%# OtherMethods.FullDateTimeConvertForCity(OvdFrom) %>' style="width: 35px;" EnableViewState="False"></asp:TextBox> 
по <asp:TextBox runat="server" ID="tbOvdTo" CssClass="time"  Text='<%# OtherMethods.FullDateTimeConvertForCity(OvdTo) %>'  style="width: 35px;" EnableViewState="False"></asp:TextBox>
