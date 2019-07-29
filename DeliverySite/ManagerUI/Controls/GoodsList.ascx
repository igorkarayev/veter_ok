<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="GoodsList.ascx.cs" Inherits="Delivery.ManagerUI.Controls.GoodsList" %>
<%@ Import Namespace="Delivery.BLL" %> <%@ Import Namespace="Delivery.BLL.StaticMethods" %>
<asp:ListView runat="server" ID="lvAllGoods" Visible="False"  DataKeyNames="ID" ClientIDMode="Predictable" 
        ClientIDRowSuffix="ID">
    <LayoutTemplate>
        <div class="goodsContener">
            <div runat="server" id="itemPlaceholder"></div>
        </div>
    </LayoutTemplate>
    <ItemTemplate>
        <div runat="server" style="font-size: 11px;">
            <hr class="styleHR"/>
            <table class="table" style="border-collapse: collapse; padding: 0;margin:0;">
                <tr>
                    <td style="vertical-align: top; ">
                        <asp:Label runat="server" ID="lblDescription" Text='<%# Eval("Description") %>'></asp:Label>
                        <asp:Label runat="server" ID="lblModel" Text='<%# Eval("Model") %>'></asp:Label>,
                        <b><asp:Label runat="server" ID="lblNumber" Text='<%# Eval("Number") %>'></asp:Label>шт.</b>
                    </td>
                    <asp:Panel runat="server" ID="pnlWithoutAkciza"  Visible="False">
                        <td style="vertical-align: top">
                            <script>
                                $(function () {
                                    $('<%# String.Format("{0}_GoodsList_{1}_lvAllGoods_{1}_cbWithoutAkciza_{2}", ListViewControlFullID, TicketID, Eval("ID")) %>').click(function () {
                                        <%# String.Format("SaveCheckboxWithoutAkciza({0}, {1}, \"{2}\", \"{3}\", \"{4}\", \"{5}\", \"{6}\");", TicketID, Eval("ID"), AppKey, ListViewControlFullID, UserID, UserIP, PageName) %>  //сохраняем значение
                                    });
                                });
                            </script>
                            <div style="font-size: 9px; color: red">БУ</div>
                            <asp:CheckBox runat="server" ID="cbWithoutAkciza" />
                            <asp:Label runat="server" ID="lblStatus"/>
                            <asp:HiddenField runat="server" ID="hfGoodsID" Value='<%# Eval("ID") %>'/>
                            <asp:HiddenField runat="server" ID="hfWithoutAkciza" Value='<%# Eval("WithoutAkciza") %>'/>
                        </td>
                    </asp:Panel>
                </tr>
            </table>
        </div>
    </ItemTemplate>
</asp:ListView>
<asp:Panel runat="server" ID="pnlOldTickets" Visible="False"  style="padding-left: 10px; text-align: left;">
    <asp:Label runat="server" ID="lblResult" ></asp:Label>
</asp:Panel>