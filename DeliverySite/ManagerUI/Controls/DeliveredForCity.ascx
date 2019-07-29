<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="DeliveredForCity.ascx.cs" Inherits="Delivery.ManagerUI.Controls.DeliveredForCity" %>
<script>
    $(function () {
        $('#<%= lblDelivered.ClientID %>').click(function () {
            <%# String.Format("SaveDeliveredForCity({0}, \"{1}\", \"{2}\", \"{3}\", \"{4}\", \"{5}\");", CityRowId, AppKey, ListViewControlFullID, UserID, UserIP, PageName) %>  //сохраняем значение
        });
    });
</script>
<style>
    .labelLink {
        color: #5291ff !important;
    }
    .labelLink:hover {
        text-decoration: underline !important;
    }
</style>
<span class="labelLink" runat="server" ID="lblDelivered">доставлено</span>
