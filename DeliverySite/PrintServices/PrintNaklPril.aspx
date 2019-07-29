<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PrintNaklPril.aspx.cs" Inherits="Delivery.PrintServices.PrintNaklPril "%>
<%@ Import Namespace="System.Web.Optimization" %>
<%@ Import Namespace="Delivery.BLL" %> <%@ Import Namespace="Delivery.BLL.StaticMethods" %> <%@ Import Namespace="Delivery.BLL.Helpers" %>

<!DOCTYPE html>

<html>
	<head>
		<title>Печать прил.накл. <%= BackendHelper.TagToValue("page_title_part") %></title>
		<link rel="icon" type="image/png" href="<%=ResolveClientUrl("~/Styles/Images/favicon.png") %>" />
		<%: Scripts.Render("~/js/jquery") %>
		<script src='<%=ResolveClientUrl("~/Scripts/CustomScripts/ajax-save-functions.js") %>' ></script>
		<link rel="stylesheet" href="<%=ResolveClientUrl("~/Styles/print.css") %>">
		<script type="text/javascript">
			function InputData() {
				$('#Form1').show();
				$('#naklnumber').hide();
				$('#seria').hide();
				$('#date').hide();
				$('#inputdata').hide();
			}
			
			function PrintElem(elem) {
				$('#naklnumber').html($('#Inpnaklnumber').val());
				$('#naklnumber').show();
				
				$('#seria').html($('#Inpseria').val());
				$('#seria').show();
				
				$('#date').html($('#Inpdate').val());
				$('#date').show();

				$('#Form1').hide();
				$('#inputdata').show();
				Popup($(elem).html());
				
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

			function Popup(data) {
				var mywindow = window.open('', 'Приложение к накладной', 'width="21.0cm"');
				mywindow.document.write(data);
				mywindow.print();
				mywindow.close();

				var reportbody = '<!DOCTYPE html><html><head><meta charset="utf-8"><title>Приложение к накладной</title></head><body >' + data + '</body></html>';
				$.ajax({
					type: "POST",
					url: "../AppServices/ReportService.asmx/SaveReport",
					data: ({
						body: '<style>body {font-family: sans-serif;font-size: 14px;}table {border-collapse: collapse;}table th{font-weight: normal;text-align: left;border: 1px solid black;vertical-align: top;background-color: whitesmoke;font-size: 14px;}table td{text-align: left;border: 1px solid black;vertical-align: top;font-size: 14px;}.mapTable{border-collapse: collapse;}.big {font-weight: bold;}.bottom-table td {font-size: 12px !important;}</style>' + reportbody,
						reporttype: "2",
						driverid: "<%= DriverID %>",
						drivername: "<%= DriverName %>",
						appkey: "<%= AppKey %>",
						documentdate: $('#Inpdate').val()
					})
				});

				$.ajax({
					type: "POST",
					url: "../AppServices/SynchronizationApi.asmx/SendSql",
					data: ({
						appkey: "<%= AppKey %>",
						ticketIdList: "<%= TicketIdList %>"
					})
				});
				return true;
			}
		</script>
	</head>
	<body>
		<div style="width:26cm !important;" class="header">
			Страница печати <b>приложения к накладной</b>. Все, что следует за кнопкой "Печать" будет распечатано.
			<hr class="styleHR"/>
			<form id="Form1" runat="server">
				Номер накладной: <asp:TextBox CssClass="input" runat="server" id="Inpnaklnumber" > </asp:TextBox>&nbsp;&nbsp;&nbsp;
				Серия накладной: <asp:TextBox CssClass="input" runat="server" id="Inpseria" style="width: 50px" ></asp:TextBox>&nbsp;&nbsp;&nbsp;
				Дата: <asp:TextBox CssClass="input" runat="server" id="Inpdate" style="width: 100px" ></asp:TextBox>   
				<asp:CheckBox runat="server" ID="cbWithUr" OnCheckedChanged="cbWithUr_CheckedChanged" AutoPostBack="true"/> с юр. лицами
			</form>
			<span id="inputdata" onclick="InputData()" style="color: red; cursor: pointer; display: none;">Ввести данны повторно</span>
			<hr class="styleHR"/>
			<input type="button" value="Назад" onclick="window.history.go(-1); return false;" class="btn" style="width: 14%; margin-right: 1%" />
			<input type="button" value="Печать" onclick="PrintElem('#thisPrint')" class="btn" style="width: 84%" />
		</div>
		
			<div id="thisPrint" class="this-print" style="width:26cm;">
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
				</style>
				
				Приложение №1 к накладной № <span id="naklnumber"></span>&nbsp;&nbsp; Серия <span id="seria"></span>&nbsp;от <span id="date"></span>г.
				<div id="Div1" style="width:26cm !important;  overflow: hidden; padding: 5px;"  runat="server">
					<table>
						<tr>
							<th style="padding: 0 3px;">
								№
							</th>
							<th style="width: 200px;">
								Документ
							</th>
							<th>
								Единица измерения
							</th>
							<th>
								Количе ство
							</th>
							<th>
								Цена, руб.
							</th>
							<th>
								Стоимость, руб.
							</th>
							<th>
								Ставка НДС
							</th>
							<th>
								Сумма НДС
							</th>
							<th>
								Стоимостьс НДС
							</th>
							<th>
								Количество грузовых мест
							</th>
							<th>
								Масса груза, кг
							</th>
							<th>
								Примеча ния
							</th>
						</tr>
						<tr>
							<td></td>
							<td>2</td>
							<td>3</td>
							<td>4</td>
							<td>5</td>
							<td>6</td>
							<td>7</td>
							<td>8</td>
							<td>9</td>
							<td>10</td>
							<td>11</td>
							<td>12</td>
						</tr>
							<asp:ListView runat="server" ID="lvAllPrint">
								<LayoutTemplate>
						 
											<tr runat="server" id="itemPlaceholder"></tr>
								
						   
							
								</LayoutTemplate>
								<ItemTemplate>
									<tr>
										<td>
											<%# Eval("PorID") %>
										</td>
										<td>
											Заказ-Поручение № <%# Eval("SecureID") %>
										</td>
										<td>
											Шт
										</td>
										<td>
											1
										</td>
										<td>
											<%# MoneyMethods.MoneySeparator(MoneyMethods.AgreedAssessedCosts(Eval("ID").ToString()))%>
										</td>
										<td>
											<%# MoneyMethods.MoneySeparator(MoneyMethods.AgreedAssessedCosts(Eval("ID").ToString()))%>
										</td>
										<td>
											Без НДС
										</td>
										<td>
											-
										</td>
										<td>
											<%# MoneyMethods.MoneySeparator(MoneyMethods.AgreedAssessedCosts(Eval("ID").ToString()))%>
										</td>
										<td>
											<%# Eval("BoxesNumber") %>
										</td>
										<td>
											<%# Eval("Weight") %>
										</td>
										<td>
								
										</td>
									
									</tr>  
								</ItemTemplate>
							</asp:ListView>
						 <tr>
							<th>
										
							</th>
							<th style="padding-bottom: 10px;">
								Итого
							</th>
							<th>
								х
							</th>
							<th>
								<asp:Label runat="server" ID="lblOverNumber"></asp:Label>
							</th>
							<th>
								<asp:Label runat="server" ID="lblOverCost2"></asp:Label>
							</th>
							<th>
								<asp:Label runat="server" ID="lblOverCost"></asp:Label>
							</th>
							<th>
								х
							</th>
							<th>
								х
							</th>
							<th>
								х
							</th>
							<th>
								<asp:Label runat="server" ID="lblOverBoxes"></asp:Label>
							</th>
							<th>
								<asp:Label runat="server" ID="lblOverWeight"></asp:Label>
							</th>
							<th>
										
							</th>
						</tr>
					</table>
				</div>
					<div>
						<b>Всего сумма НДС <u style="margin-left: 1.1cm">без НДС</u></b><br/><br/>
					</div>
					<div>
						<b>Всего стоимость с НДС <u><asp:Label runat="server" ID="lblCostWord"></asp:Label></u></b>
					</div>
				<br/>
					<table style=" width: 26cm;" class="bottom-table">
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

			<div style="width:26cm;" class="footer">
				<input type="button" value="Назад" onclick="window.history.go(-1); return false;" class="btn" style="width: 14%; margin-right: 1%" />
				<input type="button" value="Печать" onclick="PrintElem('#thisPrint')" class="btn" style="width: 84%" />
			</div>
	</body>
</html>
