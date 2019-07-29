<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="CityOrder.ascx.cs" Inherits="Delivery.ManagerUI.Controls.CityOrder" %>
<script>
    $(function () {
        $('#<%= tbCityOrder.ClientID %>').change(function () {
            <%# String.Format("SaveCityOrder(\"{0}\", \"{1}\", \"{2}\", \"{3}\", \"{4}\", \"{5}\", \"{6}\", \"{7}\");", AppKey, ListViewControlFullID, UserID, UserIP, PageName, CityIdValue, DriverIdValue, CityRowId) %>  //сохраняем значение
        });
    });
</script>
<asp:TextBox ID="tbCityOrder" style="font-weight: normal; width: 25px;" runat="server"/>
