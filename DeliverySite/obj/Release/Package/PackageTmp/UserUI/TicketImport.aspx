<%@ Page Title="" Language="C#" MasterPageFile="~/UserMasterPage.master" AutoEventWireup="true" CodeBehind="TicketImport.aspx.cs" Inherits="Delivery.UserUI.TicketImport" Async="true"%>
<%@ Import Namespace="Delivery.BLL.Helpers" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <style>
        /* #234 */
         .noDisplay {
             display: none;
         }
    </style>

    <div>
        <asp:LinkButton OnClick="ImportXLS_Click" ID="HyperLink1" CssClass="createItemHyperLink" runat="server" >Импортировать</asp:LinkButton>        
        <h3 class="h3custom" style="margin-top: 0;">Импорт заявок</h3>
    </div>   
    <div>
        <asp:LinkButton OnClick="DownloadXLS_Click" runat="server" >Скачать пример</asp:LinkButton>
        <asp:LinkButton CssClass="noDisplay" OnClick="Useless_Click" runat="server" >ненужная кнопка</asp:LinkButton>
        <div style="margin-left: 15px; display: inline-block;">
            <asp:Label ForeColor="Blue" ID="labelStatus" runat="server"></asp:Label>
        </div>        
        <div style="float: right; display: inline-block;">
            <asp:FileUpload  ID="FileUploader" runat="server" />
        </div>
    </div>

    <asp:ListView ID="ListXML" runat="server">
        <LayoutTemplate>
            <table runat="server" id="Table1" class="tableViewClass tableClass colorTable">
                <tr class="table-header">
                    <th style="min-width: 340px;">
                        Ошибки
                    </th>
                    <th>
                        Имя профиля
                    </th>                        
                    <th>
                        Населенный пункт(ID)
                    </th>
                    <th>
                        Тип улицы
                    </th>
                    <th>
                        Название улицы 
                    </th>
                    <th>
                        Номер дома
                    </th>
                    <th>
                        Корпус
                    </th>
                    <th>
                        Квартира
                    </th>
                    <th>
                        Фамилия
                    </th>
                    <th>
                        Имя
                    </th>
                    <th>
                        Отчество
                    </th>
                    <th>
                        Контактный телефон
                    </th>
                    <th>
                        Доп. конт. телефон
                    </th>
                    <th>
                        Сумма доставки за счёт получателя
                    </th>
                    <th>
                        Товары
                    </th>
                    <th>
                        Кол-во коробок
                    </th>
                    <th>
                        Дата отправки
                    </th>
                    <th>
                        Комментарии
                    </th>
                    <th>
                        ТТН серия
                    </th>
                    <th>
                        ТТН номер
                    </th>
                    <th>
                        Другие документы
                    </th>
                    <th>
                        Серия паспорта
                    </th>
                    <th>
                        Номер паспорта
                    </th>
                </tr>
                <tr runat="server" id="itemPlaceholder"></tr>
            </table>
        </LayoutTemplate>
        <ItemTemplate>
            <asp:TableRow id="Tr2" runat="server" CssClass='<%# Eval("23").ToString()%>'>
                <asp:TableCell id="tcCost" runat="server">
                    <asp:Label ID="lbCost" runat="server" Text='<%# Eval("22") %>' />
                </asp:TableCell>
                
                <asp:TableCell runat="server">
                    <asp:Label runat="server" Text='<%# Eval("0").ToString() %>' />
                </asp:TableCell>
                
                <asp:TableCell runat="server">
                    <asp:Label runat="server" Text='<%# Eval("1").ToString() %>' />
                </asp:TableCell>
                
                <asp:TableCell runat="server">
                    <asp:Label runat="server" Text='<%# Eval("2").ToString() %>' />
                </asp:TableCell>
                
                <asp:TableCell runat="server">
                    <asp:Label runat="server" Text='<%# Eval("3").ToString() %>' />
                </asp:TableCell>
                
                <asp:TableCell runat="server">
                    <asp:Label runat="server" Text='<%# Eval("4").ToString() %>' />
                </asp:TableCell>
                
                <asp:TableCell runat="server">
                    <asp:Label runat="server" Text='<%# Eval("5").ToString() %>' />
                </asp:TableCell>
                
                <asp:TableCell runat="server">
                    <asp:Label runat="server" Text='<%# Eval("6").ToString() %>' />
                </asp:TableCell>
                
                <asp:TableCell runat="server">
                    <asp:Label runat="server" Text='<%# Eval("7").ToString() %>' />
                </asp:TableCell>
                
                <asp:TableCell runat="server">
                    <asp:Label runat="server" Text='<%# Eval("8").ToString() %>' />
                </asp:TableCell>
                
                <asp:TableCell runat="server">
                    <asp:Label runat="server" Text='<%# Eval("9").ToString() %>' />
                </asp:TableCell>
                
                <asp:TableCell runat="server">
                    <asp:Label runat="server" Text='<%# Eval("10").ToString() %>' />
                </asp:TableCell>
                
                <asp:TableCell runat="server">
                    <asp:Label runat="server" Text='<%# Eval("11").ToString() %>' />
                </asp:TableCell>
                
                <asp:TableCell runat="server">
                    <asp:Label runat="server" Text='<%# Eval("12").ToString() %>' />
                </asp:TableCell>
                
                <asp:TableCell runat="server">
                    <asp:Label runat="server" Text='<%# Eval("13").ToString() %>' />
                </asp:TableCell>
                
                <asp:TableCell runat="server">
                    <asp:Label runat="server" Text='<%# Eval("14").ToString() %>' />
                </asp:TableCell>
                
                <asp:TableCell runat="server">
                    <asp:Label runat="server" Text='<%# Eval("15").ToString() %>' />
                </asp:TableCell>
                
                <asp:TableCell runat="server">
                    <asp:Label runat="server" Text='<%# Eval("16").ToString() %>' />
                </asp:TableCell>
                
                <asp:TableCell runat="server">
                    <asp:Label runat="server" Text='<%# Eval("17").ToString() %>' />
                </asp:TableCell>
                
                <asp:TableCell runat="server">
                    <asp:Label runat="server" Text='<%# Eval("18").ToString() %>' />
                </asp:TableCell>
                
                <asp:TableCell runat="server">
                    <asp:Label runat="server" Text='<%# Eval("19").ToString() %>' />
                </asp:TableCell>
                
                <asp:TableCell runat="server">
                    <asp:Label runat="server" Text='<%# Eval("20").ToString() %>' />
                </asp:TableCell>
                
                <asp:TableCell runat="server">
                    <asp:Label runat="server" Text='<%# Eval("21").ToString() %>' />
                </asp:TableCell>
            </asp:TableRow>
        </ItemTemplate>
    </asp:ListView>
</asp:Content>
