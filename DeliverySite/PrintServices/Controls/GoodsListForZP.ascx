<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="GoodsListForZP.ascx.cs" Inherits="Delivery.PrintServices.Controls.GoodsListForZP" %>
<%@ Import Namespace="Delivery.BLL" %> <%@ Import Namespace="Delivery.BLL.StaticMethods" %>

<script type="text/javascript">
    $(function () {
        
    });
</script>

<asp:Panel runat="server" ID="pnlNewTickets" Visible="False">
    <div style="margin-left: 50px; margin-top: 0px;">СВЕДЕНИЯ О ГРУЗЕ</div>
    <asp:ListView runat="server" ID="lvAllGoods1"  DataKeyNames="ID" ClientIDMode="Predictable" 
            ClientIDRowSuffix="ID">
        <LayoutTemplate>
            <table class="printZP validateTable" style="width: 50%; float: left;" runat="server">
                <tr>
                    <td style="width: 6cm;">
                        Наименование груза <asp:Label runat="server" Visible="false" ID="lblNumber" CssClass="notprint"></asp:Label> <asp:Label runat="server" Visible="false" ID="lblCost" CssClass="notprint"></asp:Label>
                    </td>
                    <td style="width: 1cm;">
                        Единица
                    </td>
                    <td style="width: 1cm;">
                        Кол-во
                    </td>
                    <td style="width: 2.5cm;">
                        Стоимость
                    </td>
                </tr>
                <tr runat="server" id="itemPlaceholder"></tr>
            </table>
        </LayoutTemplate>
        <ItemTemplate>
            <tr>
                <td style="height: 25px;" class="goodNameTd">
                    <div style="min-width: 20%; display: inline-block; min-height: 10px;" class="goodNameString"><%#OtherMethods.DeleteBkt(Eval("Description").ToString()) %>  <%#Eval("Model") %> </div>
                    <input type="text" class="goodNameTextBox" style="display: none; width: 80%; font-size: 10px;"/>
                </td>
                <td class="goodShtukTd">
                    <%# OtherMethods.Shtuk(Eval("Description").ToString()) %>
                </td>
                <td class="goodNumberTd">
                    <div id="numberBoxes" style="min-width: 20%; display: inline-block; min-height: 10px;" class="goodNumberString">
                        <%# OtherMethods.GoodsNumber(Eval("Number").ToString()) %>
                    </div>
                    <input type="text" class="goodNumberTextBox" style="display: none; width: 80%; font-size: 10px;"/>
                </td>
                <td style="text-align: right" class="goodMoneyTd">
                    <div style="min-width: 20%; display: inline-block; min-height: 10px;" class="goodMoneyString">
                        <%# MoneyMethods.MoneySeparator(OtherMethods.GoodsNumber(Eval("Cost").ToString())) %>
                    </div>
                    <input type="text" class="goodMoneyTextBox" style="display: none; width: 80%; font-size: 10px;"/>
                </td>
            </tr>
        </ItemTemplate>
    </asp:ListView>

    <asp:ListView runat="server" ID="lvAllGoods2"  DataKeyNames="ID" ClientIDMode="Predictable" 
            ClientIDRowSuffix="ID">
        <LayoutTemplate>
            <table class="printZP validateTable" style="width: 50%;">
                <tr style="">
                    <td style="width: 6cm; border-left: none !important">
                        Наименование груза
                    </td>
                    <td style="width: 1cm;">
                        Единица
                    </td>
                    <td style="width: 1cm;">
                        Кол-во
                    </td>
                    <td style="width: 2.5cm;">
                        Стоимость
                    </td>
                </tr>
                <tr runat="server" id="itemPlaceholder"></tr>
                <tr>
                    <td style="text-align: right; padding-right: 5px; border-bottom: none !important; border-left: none !important;" colspan="3">
                        Общая оценочная стоимость
                    </td>
                    <td style="text-align: right">
                        <asp:Label runat="server" ID="lblOveralGoodsCostInListView"></asp:Label>
                    </td>
                </tr>
            </table>
        </LayoutTemplate>
        <ItemTemplate>
            <tr>
                <td style="height: 25px; border-left: none !important" class="goodNameTd">
                    <div style="min-width: 20%; display: inline-block; min-height: 10px;" class="goodNameString"><%#OtherMethods.DeleteBkt(Eval("Description").ToString()) %>  <%#Eval("Model") %> </div>
                    <input type="text" class="goodNameTextBox" style="display: none; width: 80%; font-size: 10px;"/>
                </td>
                <td class="goodShtukTd">
                    <%# OtherMethods.Shtuk(Eval("Description").ToString()) %>
                </td>
                <td class="goodNumberTd">
                    <div style="min-width: 20%; display: inline-block; min-height: 10px;" class="goodNumberString">
                        <%# OtherMethods.GoodsNumber(Eval("Number").ToString()) %>
                    </div>
                    <input type="text" class="goodNumberTextBox" style="display: none; width: 80%; font-size: 10px;"/>
                </td>
                <td style="text-align: right" class="goodMoneyTd">
                    <div id="CostGood" style="min-width: 20%; display: inline-block; min-height: 10px;" class="goodMoneyString">
                        <%# MoneyMethods.MoneySeparator(OtherMethods.GoodsNumber(Eval("Cost").ToString())) %>
                    </div>
                    <input type="text" class="goodMoneyTextBox" style="display: none; width: 80%; font-size: 10px;"/>
                </td>
            </tr>
        </ItemTemplate>
    </asp:ListView>
</asp:Panel>






<asp:Panel runat="server" ID="pnlOldTickets" Visible="False">
    <div style="margin-left: 50px; margin-top: 0px;">СВЕДЕНИЯ О ГРУЗЕ</div>
    <table runat="server" id="Table4" class="printZP" style="width: 100%">
        <tr>
            <td style="width: 6cm;">
                Наименование груза
            </td>
            <td style="width: 1cm;">
                Единица
            </td>
            <td style="width: 1cm;">
                Кол-во
            </td>
            <td style="width: 2.5cm;">
                Стоимость
            </td>
            <td style="width: 6cm;">
                Наименование груза
            </td>
            <td style="width: 1cm;">
                Единица
            </td>
            <td style="width: 1cm;">
                Кол-во
            </td>
            <td style="width: 2.5cm;">
                Стоимость
            </td>
        </tr>
        <tr>
            <td style="height: 25px;">
                <asp:Label runat="server" ID="lblGoods1Description"></asp:Label>
            </td>
            <td>
                <asp:Label runat="server" ID="lblGoods1DescriptionShtuk"></asp:Label>
            </td>
            <td>
                <asp:Label runat="server" ID="lblGoods1Number"></asp:Label>
            </td>
            <td style="text-align: right">
                <asp:Label runat="server" ID="lblGoods1Cost"></asp:Label>
            </td>
            <td>
                <asp:Label runat="server" ID="lblGoods4Description"></asp:Label>
            </td>
            <td>
                <asp:Label runat="server" ID="lblGoods4DescriptionShtuk"></asp:Label>
            </td>
            <td>
                <asp:Label runat="server" ID="lblGoods4Number"></asp:Label>
            </td>
            <td style="text-align: right">
                <asp:Label runat="server" ID="lblGoods4Cost"></asp:Label>
            </td>
        </tr>
        <tr>
            <td style="height: 25px;">
                <asp:Label runat="server" ID="lblGoods2Description"></asp:Label>
            </td>
            <td>
                <asp:Label runat="server" ID="lblGoods2DescriptionShtuk"></asp:Label>
            </td>
            <td>
                <asp:Label runat="server" ID="lblGoods2Number"></asp:Label>
            </td>
            <td style="text-align: right">
                <asp:Label runat="server" ID="lblGoods2Cost"></asp:Label>
            </td>
            <td>
                <asp:Label runat="server" ID="lblGoods5Description"></asp:Label>
            </td>
            <td>
                <asp:Label runat="server" ID="lblGoods5DescriptionShtuk"></asp:Label>
            </td>
            <td>
                <asp:Label runat="server" ID="lblGoods5Number"></asp:Label>
            </td>
            <td style="text-align: right">
                <asp:Label runat="server" ID="lblGoods5Cost"></asp:Label>
            </td>
        </tr>
        <tr>
            <td style="height: 25px;">
                <asp:Label runat="server" ID="lblGoods3Description"></asp:Label>
            </td>
            <td>
                <asp:Label runat="server" ID="lblGoods3DescriptionShtuk"></asp:Label>
            </td>
            <td>
                <asp:Label runat="server" ID="lblGoods3Number"></asp:Label>
            </td>
            <td style="text-align: right">
                <asp:Label runat="server" ID="lblGoods3Cost"></asp:Label>
            </td>
            <td>
                <asp:Label runat="server" ID="lblGoods6Description"></asp:Label>
            </td>
            <td>
                <asp:Label runat="server" ID="lblGoods6DescriptionShtuk"></asp:Label>
            </td>
            <td>
                <asp:Label runat="server" ID="lblGoods6Number"></asp:Label>
            </td>
            <td style="text-align: right">
                <asp:Label runat="server" ID="lblGoods6Cost"></asp:Label>
            </td>
        </tr>
            <tr>
            <td style="text-align: right; padding-right: 5px; border-bottom: none !important; border-left: none !important;" colspan="7">
                Общая оценочная стоимость
            </td>
            <td style="text-align: right">
                <asp:Label runat="server" ID="lblOveralGoodsCost"></asp:Label>
            </td>
        </tr>
    </table>
</asp:Panel>