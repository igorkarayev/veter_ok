<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PrintNaklPril2.aspx.cs" Inherits="Delivery.PrintServices.PrintNaklPril2 "%>
<%@ Register TagPrefix="grb" TagName="SelectToPril2" Src="~/PrintServices/Controls/SelectToPril2.ascx" %>
<%@ Register TagPrefix="grb" TagName="WeightToPril2" Src="~/PrintServices/Controls/WeightToPril2.ascx" %>
<%@ Register TagPrefix="grb" TagName="BoxesNumberToPril2" Src="~/PrintServices/Controls/BoxesNumberToPril2.ascx" %>
<%@ Register TagPrefix="grb" TagName="ChangeCostPril2" Src="~/PrintServices/Controls/ChangeCostPril2.ascx" %>
<%@ Register TagPrefix="grb" TagName="ChangeBoxesNumber" Src="~/PrintServices/Controls/ChangeBoxesNumber.ascx" %>
<%@ Register TagPrefix="grb" TagName="ChangeWeight" Src="~/PrintServices/Controls/ChangeWeight.ascx" %>
<%@ Import Namespace="System.Web.Optimization" %>
<%@ Import Namespace="Delivery.BLL" %> <%@ Import Namespace="Delivery.BLL.StaticMethods" %>
<%@ Import Namespace="Delivery.BLL.Helpers" %>

<!DOCTYPE html>
<html>
	<head>
		<title>Печать прил.накл. #2 <%= BackendHelper.TagToValue("page_title_part") %></title>
		<link rel="icon" type="image/png" href="<%=ResolveClientUrl("~/Styles/Images/favicon.png") %>" />
		<%: Scripts.Render("~/js/jquery") %>
		<script src='<%=ResolveClientUrl("~/Scripts/CustomScripts/ajax-save-functions.js") %>' ></script>
		<link rel="stylesheet" href="<%=ResolveClientUrl("~/Styles/print.css") %>">
		<script type="text/javascript">
			$(function() {
				SummPril2BoxesNumber();
				SummPril2Weight();
			});
			
			function InputData() {
				$('#dopData').show();
				$('#naklnumber').hide();
				$('#seria').hide();
				$('#date').hide();
				$('#inputdata').hide();
			}
			
			function PrintElem(elem, isNormal) {
				$(".boxes-number-topril2").each(function () {
					$(this).hide();
					$(this).prev().show();
				}); //скрываем все открытые инпуты столбца
				
				$(".weight-topril2").each(function () {
					$(this).hide();
					$(this).prev().show();
				}); //скрываем все открытые инпуты столбца
												
				$('#naklnumber').html($('#Inpnaklnumber').val());
				$('#naklnumber').show();
				
				$('#seria').html($('#Inpseria').val());
				$('#seria').show();
				
				$('#date').html($('#Inpdate').val());
				$('#date').show();

				$('#dopData').hide();
				$('#inputdata').show();
				if (isNormal) {
					Popup($(elem).html());
				} else {
					Popup2($(elem).html());
				}				
				
				$.ajax({
					type: "POST",
					url: "../AppServices/SaveAjaxService.asmx/SavePrintNaklParams",
					data: ({
						putevoi: "",
						appkey: "<%= AppKey %>",
						naklnumber: $('#Inpnaklnumber').val(),
						naklseria: $('#Inpseria').val(),
						nakldate: $('#Inpdate').val()
					})
				});
				
            }

            function SendZP2() {
                var cost = new Array();
                var id = new Array();
                var numberBoxes = new Array();
                
                $("tr[id*='lvAllPrint_Tr2']:not('.hidden')").each(function (index, value) {
                    var a = parseFloat($(this).children("[id *= 'lvAllPrint_Td7']").text().trim().replace(" ", "").replace(/\u00a0/g, '').replace(/&nbsp;/gi, '').replace(",","."));
                    var b = $(this).children("[id *= 'lvAllPrint_Td14']").text().trim().replace(" ", "").replace(/\u00a0/g, '').replace(/&nbsp;/gi, '');
                    var c = parseFloat($(this).children("[id *= 'lvAllPrint_Td5']").text().trim().replace(" ", "").replace(/\u00a0/g, '').replace(/&nbsp;/gi, '').replace(",", "."));
                                        
                    cost.push(a);
                    id.push(b);
                    numberBoxes.push(c);
                });
                
                $.ajax({
                    type: "POST",                    
                    url: "../AppServices/CallPrint.asmx/CallPrintZP2",
                    data: ({
                        cost: JSON.stringify(cost),
                        id: JSON.stringify(id),
                        numberBoxes: JSON.stringify(numberBoxes)
                    }),
                    success: function (response) {
                        var ccc = response.children[0].textContent;
                        location.href = ccc;
                    }
                });

            }

			function Popup(data) {
				var mywindow = window.open('', 'Приложение к накладной', 'width="21.0cm"');
                mywindow.document.write('<html><head><title>Приложение к накладной</title><style>.notPrint2{display: none;} .replace{width: 30cm !important;}</style>');
                mywindow.document.write('</head><style>body td span{color: black !important;}</style><body >');
                mywindow.document.write(data);
                mywindow.document.write('</body></html>');
				mywindow.print();
				mywindow.close();
				return true;
			}

			function Popup2(data) {
				var mywindow = window.open('', 'Приложение к накладной', 'width="21.0cm"');
				mywindow.document.write('<html><head><title>Приложение к накладной</title><style>.notprintNotNormal{display: none;} .replace{width: 30cm !important;}</style>');
				mywindow.document.write('</head><style>body td span{color: black !important;}</style><body >');
				mywindow.document.write(data);
				mywindow.document.write('</body></html>');
				mywindow.print();
				mywindow.close();
				return true;
			}
			
			function CheckCheckboxes() {
				var i = 0;
				var j = 0;
				$(".checkboxInListView input:checkbox").each(function () {
					i++;
				});

				$(".checkboxInListView input:checkbox").each(function () {
					if ($(this).is(':checked')) {
						j++;
					}
				});

				if (i === j) {
					alert("Нельзя выбрать все заявки!");
					return false;
				} else {
					return true;
				}
			}
		</script>
        <style>
            .inp {
                width: 50px;
            }
        </style>
 </head>
	<body>
        <form id="Form1" runat="server">
			<div runat="server" style="width:26cm !important;" class="header">
				Страница печати <b>приложения к накладной #2</b>. Все, что следует за кнопкой "Печать" будет распечатано.
				<hr class="styleHR"/>
				<div id="dopData">
					Номер накладной: <asp:TextBox CssClass="input" runat="server" id="Inpnaklnumber" > </asp:TextBox>&nbsp;&nbsp;&nbsp;
					Серия накладной: <asp:TextBox CssClass="input" runat="server" id="Inpseria" style="width: 50px" ></asp:TextBox>&nbsp;&nbsp;&nbsp;
					Дата: <asp:TextBox  runat="server" id="Inpdate" CssClass="input" style="width: 100px" ></asp:TextBox>   
					<asp:CheckBox runat="server" ID="cbWithUr" OnCheckedChanged="cbWithUr_CheckedChanged" AutoPostBack="true"/> с юр. лицами
				</div>
				<span id="inputdata" onclick="InputData()" style="color: red; cursor: pointer; display: none;">Ввести данны повторно</span>
				<hr class="styleHR"/>
				<input type="button" value="Назад к заявкам" onclick="window.history.go(-1); return false;" class="btn" style="width: 14%; margin-right: 1%; margin-bottom: 5px;" />
				<input type="button" value="Печать с ценой за доставку" onclick="PrintElem('#thisPrint', false)" class="btn" style="width: 41%; margin-bottom: 5px;  margin-right: 1%;" />
				<input type="button" value="Печать в документы" onclick="PrintElem('#thisPrint', true)" class="btn" style="width: 41%; margin-bottom: 5px;" />
				<asp:Button type="button" Text="Пересчитать" OnClick="btnReload_Click" OnClientClick="return CheckCheckboxes();" style="width: 49%; margin-right: 1%" runat="server" ID="btnReload" CssClass="btn"/>
				<asp:Button type="button" Text="Вернуть все заявки" OnClick="btnReset_Click" style="width: 49%;" runat="server" ID="btnReset" CssClass="btn"/>
			</div>
		
				<div id="thisPrint" class="this-print replace" style="width:30cm;">
					<style>
					    body {
					        font-family: sans-serif;
					        font-size: 14px;
					    }
					    table {
					        border-collapse: collapse;
					    }
					    table th{
					        font-weight: normal;
					        text-align: left;
					        border: 1px solid black;
					        vertical-align: top;
					        background-color: whitesmoke;
					        font-size: 14px;
					    }

					    table td{
					        text-align: left;
					        border: 1px solid black;
					        vertical-align: top;
					        font-size: 14px;
					    }
					    .mapTable{
					        border-collapse: collapse;
					    }
					    .big {
					        font-weight: bold;
					    }
					    .bottom-table td {
					        font-size: 12px !important;
					    }
					    
                        @media print {
					        .notPrint { display:none; }
					    }
                                                
                        .notDisplay {
                            display: none;
                        }					
					</style>
				
					Приложение №1 к накладной № <span id="naklnumber"></span>&nbsp;&nbsp; Серия <span id="seria"></span>&nbsp;от <span id="date"></span>г.
					<div id="Div1" class="replace" style="width:30cm;  overflow: hidden; padding: 5px;"  runat="server">
						<table>
							<tr>
                                <th class="notPrint notprintNotNormal"></th>
								<th style="padding: 0 3px;">
									№
								</th>
								<th style="width: 200px;">
									Документ
								</th>
								<th>
									Единица измерения
								</th>
								<th class="notprintNotNormal">
									Количе ство
								</th>
								<th class="notprintNotNormal">
									Цена, руб.
								</th>
								<th class="notprintNotNormal">
									Стоимость, руб.
								</th>
								<th class="notprintNotNormal">
									Ставка НДС
								</th>
								<th class="notprintNotNormal">
									Сумма НДС
								</th>
								<th class="notprintNotNormal">
									Стоимость<br /> с НДС
								</th>
								<th class="notprintNotNormal">
									Количество <br /> груз. мест
									<!--div style="width: 100%; text-align: center; font-size: 12px; font-style: italic; font-weight: bold" class="notPrint">
										р.: <asp:Label runat="server" ID="lblExcessBoxesNumber" CssClass="boxes-number-summ"></asp:Label>
									</!--div-->
								</th>
								<th class="notprintNotNormal">
									Масса груза, кг
									<!--div style="width: 100%; text-align: center; font-size: 12px; font-style: italic; font-weight: bold"class="notPrint">
										р.: <asp:Label runat="server" ID="lblExcessWeight" CssClass="weight-summ"></asp:Label>
									</div-->
								</th>
								<th class="notPrint notprintNotNormal">
									Примеча ния
								</th>   
                                <th class="notDisplay notPrint notprintNotNormal">
									ID
								</th> 
							</tr>
							<tr>
                                <td class="notPrint notprintNotNormal"></td>
								<td class="notprintNotNormal"></td>
								<td></td>
								<td>2</td>
								<td>3</td>
								<td class="notprintNotNormal">4</td>
								<td class="notprintNotNormal">5</td>
								<td class="notprintNotNormal">6</td>
								<td class="notprintNotNormal">7</td>
								<td class="notprintNotNormal">8</td>
								<td class="notprintNotNormal">9</td>
								<td class="notprintNotNormal">10</td>
								<td class="notPrint notprintNotNormal">11</td>  
                                <td class="notDisplay notPrint notprintNotNormal"></td>
							</tr>
								<asp:ListView runat="server" ID="lvAllPrint" DataKeyNames="ID" ClientIDMode="Predictable" 
									ClientIDRowSuffix="ID">
									<LayoutTemplate>
												<tr runat="server" id="itemPlaceholder"></tr>
									</LayoutTemplate>
									<ItemTemplate>
										<asp:TableRow id="Tr2" runat="server">
                                            <asp:TableCell id="Td1" runat="server" CssClass="notPrint notprintNotNormal">
												<grb:SelectToPril2 
													ID="SelectToPril2" 
													runat="server" 
													TicketID='<%# Eval("ID").ToString() %>'
													ListViewControlFullID='#lvAllPrint'
													PageName = "PrintNaklPril2"/>
											</asp:TableCell>

											<asp:TableCell id="Td2" runat="server">
												<%# Eval("PorID") %>
											</asp:TableCell>

											<asp:TableCell id="Td3" runat="server">
												Заказ-Поручение № <%# Eval("SecureID") %>
											</asp:TableCell>

											<asp:TableCell id="Td4" runat="server">
												Шт <span class="notPrint2">(<%#
														MoneyMethods.MoneySeparator(
															MoneyMethods.GruzobozCostLoweringPercentage(
																Eval("GruzobozCost").ToString()
															)
														)
													%>)</span>
											</asp:TableCell>

											<asp:TableCell CssClass="notprintNotNormal" id="Td5" runat="server">
												1
											</asp:TableCell>

											<asp:TableCell CssClass="notprintNotNormal" id="Td6" runat="server">												
                                                <grb:ChangeCostPril2
                                                    ID="ChangeCost"
                                                    runat="server"
                                                    TicketID='<%# Eval("ID").ToString() %>'
                                                    ListViewControlFullID='#lvAllPrint'
                                                    Val='<%# MoneyMethods.MoneySeparator(Eval("Pril2CostOrCost").ToString())%>'
                                                    PageName="PrintNaklPril2" />
											</asp:TableCell>

											<asp:TableCell CssClass="notprintNotNormal" id="Td7" runat="server">
												<%# MoneyMethods.MoneySeparator(Eval("Pril2CostOrCost").ToString())%>
											</asp:TableCell>

											<asp:TableCell CssClass="notprintNotNormal" id="Td8" runat="server">
												Без НДС
											</asp:TableCell>

											<asp:TableCell CssClass="notprintNotNormal" id="Td9" runat="server">
												-
											</asp:TableCell>

											<asp:TableCell CssClass="notprintNotNormal" id="Td10" runat="server">
											    <%# MoneyMethods.MoneySeparator(Eval("Pril2CostOrCost").ToString())%>
											</asp:TableCell>

											<asp:TableCell CssClass="notprintNotNormal" id="Td11" runat="server">											    
                                                <grb:ChangeBoxesNumber
                                                    ID="ChangeBoxes"
                                                    runat="server"
                                                    TicketID='<%# Eval("ID").ToString() %>'
                                                    ListViewControlFullID='#lvAllPrint'
                                                    Boxes='<%# Eval("BoxesNumber").ToString() %>'
                                                    PageName="PrintNaklPril2" />                                                
											</asp:TableCell>

											<asp:TableCell CssClass="notprintNotNormal" id="Td12" runat="server">											    
                                                <grb:ChangeWeight
                                                    ID="ChangeWeight"
                                                    runat="server"
                                                    TicketID='<%# Eval("ID").ToString() %>'
                                                    ListViewControlFullID='#lvAllPrint'
                                                    Weight='<%# Eval("Weight").ToString() %>'
                                                    PageName="PrintNaklPril2" />											    
											</asp:TableCell>

											<asp:TableCell CssClass="notPrint notprintNotNormal" id="Td13" runat="server">
								
											</asp:TableCell>

                                            <asp:TableCell CssClass="notDisplay notPrint notprintNotNormal" id="Td14" runat="server">
                                                <%# Eval("ID") %>
                                            </asp:TableCell>
										</asp:TableRow>  
									</ItemTemplate>
								</asp:ListView>
							 <tr>
                                <th class="notPrint notprintNotNormal">
										
								</th>
								<th class="notprintNotNormal">
										
								</th>
								<th class="notprintNotNormal" style="padding-bottom: 10px;">
									Итого
								</th>
								<th class="notprintNotNormal">
									х
								</th>
								<th class="notprintNotNormal">
									<asp:Label runat="server" ID="lblOverNumber"></asp:Label>
								</th>
								<th class="notprintNotNormal">
									<asp:Label runat="server" ID="lblOverCost2"></asp:Label>
								</th>
								<th class="notprintNotNormal">
									<asp:Label runat="server" ID="lblOverCost"></asp:Label>
								</th>
								<th class="notprintNotNormal">
									х
								</th>
								<th class="notprintNotNormal">
									х
								</th>
								<th class="notprintNotNormal">
									<asp:Label runat="server" ID="lblOverCost3"></asp:Label>
								</th>
								<th class="notprintNotNormal">
									<asp:Label runat="server" ID="lblOverBoxes" CssClass="boxes-number-try-summ"></asp:Label>
								</th>
								<th class="notprintNotNormal">
									<asp:Label runat="server" ID="lblOverWeight" CssClass="weight-try-summ"></asp:Label>
								</th>
								<th class="notPrint notprintNotNormal">
										
								</th>
							</tr>
							<tr class="notprint" style="display:none;">
								<th colspan="15">
									Итого за услугу: <asp:Label runat="server" ID="overGruzobozCost"></asp:Label>
								</th>
							</tr>
						</table>
					</div>
					<div class="notprintNotNormal">
						<b>Всего сумма НДС <u style="margin-left: 1.1cm">без НДС</u></b><br/><br/>
					</div>
					<div class="notprintNotNormal">
						<b>Всего стоимость с НДС <u><asp:Label runat="server" ID="lblCostWord"></asp:Label></u></b>
					</div>
					<br/>
					<table style=" width: 26cm;" class="bottom-table notprintNotNormal">
							<tr>
								<td style="width: 50%; vertical-align: top;  border-top: none; border-left: none; border-bottom: none;">
									<div style="margin-bottom: 15px;">
										Всего масса груза <u  style="margin-left: 0.8cm"><asp:Label runat="server" ID="lblWeightWord"></asp:Label> килограмм&nbsp;&nbsp;&nbsp;</u> 
									</div>
									<div style="margin-bottom: 15px;">
									    Отпуск разрешил <u style="margin-left: 0.9cm"><%= BackendHelper.TagToValue("ttnp_released_person_position") %>, <%= BackendHelper.TagToValue("ttnp_released_person_name") %></u>
								    </div>
								    <div>
									    Сдал грузоотправитель <u style="margin-right: 50px"><%= BackendHelper.TagToValue("ttnp_goods_gave_person_position") %>, <%= BackendHelper.TagToValue("ttnp_goods_gave_person_name") %></u> № пломбы <u>_________</u>
								    </div>
									<div style="margin-top: 2cm">
										Штамп (печать) грузоотправителя
									</div>
								</td>
								<td style="border-left: 1px solid black;  border-top: none; border-right: none; border-bottom: none;">
									<div style="margin-bottom: 15px;">
										Всего количество грузовых мест <u><asp:Label runat="server" ID="lblBoxesWord"></asp:Label></u>
									</div>
									<div style="margin-bottom: 15px;">
										Груз к перевозке принял <u style="margin-left: 0.8cm">Водитель <asp:Label runat="server" ID="lblDriver"></asp:Label></u>
									</div>
									<div style="margin-bottom: 15px;">
										По доверенности <u>________________________</u> выданной<u>______________</u>
									</div>
									<div style="margin-bottom: 15px;">
										Принял грузополучатель <u>Водитель <asp:Label runat="server" ID="lblDriver2"></asp:Label></u>
									</div>
									<div>
										№ пломбы <u>________________</u>
									</div>
									<div style="margin-top: 0.7cm">
										Штамп (печать) грузополучателя
									</div>
								</td>
							</tr>
						</table>
				</div>
				<div runat="server" style="width:26cm;" class="footer">
					<input type="button" value="Назад к заявкам" onclick="window.history.go(-1); return false;" class="btn" style="width: 14%; margin-right: 1%; margin-bottom: 5px;" />
					<input type="button" value="Печать с ценой за доставку" onclick="PrintElem('#thisPrint', false)" class="btn" style="width: 41%; margin-bottom: 5px;  margin-right: 1%;" />
					<input type="button" value="Печать в документы" onclick="PrintElem('#thisPrint', true)" class="btn" style="width: 41%; margin-bottom: 5px;" />  					
					<a href='<%= OtherMethods.LinkPut3(IdList) %>' style="text-decoration: none; display: inline-block; width: 49%; margin-bottom: 5px;" class="btn notDisplay" ID="btnPrintPut3" target="_blank">Открыть путевой лист #3</a>
					<asp:Button type="button" ID="btnCSVStart" Text="Начать цикл печати документов" cssclass="btn notDisplay" style="width: 29%;"  runat="server"/>
					<asp:Button type="button" ID="btnCSVAdd" Text="Добавить текущий путевой в файл синхронизации" cssclass="btn notDisplay" style="width: 40%;"  runat="server"/>
					<asp:Button type="button" ID="btnCSVEnd" Text="Завершить цикл печати документов" cssclass="btn notDisplay" style="width: 29%;"  runat="server"/>                    
                    <input type="button" value="Заказ-поручение 2" onclick="SendZP2()" class="btn" style="width: 41%; margin-bottom: 5px;" />  
				</div>
		</form>
	</body>
</html>
