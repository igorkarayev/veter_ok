<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PrintPut3.aspx.cs" Inherits="Delivery.PrintServices.PrintPut3 "%>
<%@ Import Namespace="System.Web.Optimization" %>
<%@ Import Namespace="Delivery.BLL" %> <%@ Import Namespace="Delivery.BLL.StaticMethods" %> <%@ Import Namespace="Delivery.BLL.Helpers" %>

<!DOCTYPE html>

<html>
	<head>
		<title>Печать пут.лист. #3 <%= BackendHelper.TagToValue("page_title_part") %></title>
		<link rel="icon" type="image/png" href="<%=ResolveClientUrl("~/Styles/Images/favicon.png") %>" />
		<%: Scripts.Render("~/js/jquery") %>
		<script src='<%=ResolveClientUrl("~/Scripts/CustomScripts/ajax-save-functions.js") %>' ></script>
		<link rel="stylesheet" href="<%=ResolveClientUrl("~/Styles/print.css") %>">
		<script type="text/javascript">
			function InputData() {
				$('#Form1').show();
				$('#putevoi').hide();
				$('#date').hide();
				$('#Inputdata').hide();
			}
			function PrintElem(elem) {
				$('#putevoi').html($('#Inpputevoi').val());
				
				$('#putevoi').show();
				
				$('#date').html($('#Inpdate').val());
				$('#date').show();

				$('#Form1').hide();
				$('#Inputdata').show();

				$.ajax({
					type: "POST",
					url: "../AppServices/SaveAjaxService.asmx/SavePrintNaklParams",
					data: ({
						nakldate: $('#Inpdate').val(),
						putevoi: $('#Inpputevoi').val(),
						appkey: "<%= AppKey %>",
						naklnumber: "",
						naklseria: ""
					})
				});
				Popup($(elem).html());
			}

			function Popup(data) {
				var mywindow = window.open('', 'Путевой 3', 'width="21.0cm"');
				mywindow.document.write(data);
				mywindow.print();
				mywindow.close();

				var reportbody = data;

				$.ajax({
					type: "POST",
					url: "../AppServices/ReportService.asmx/SaveReport",
					data: ({
						body: '<style>body {font-family: sans-serif;font-size: 10px;line-height: 2;}table {border-collapse: collapse;}.table td, table th {border: 1px solid black;padding: 0px;margin: 0px;height: 20px;}.table th {font-weight: 400;}.tdPadding td{height: 20px;}td, th {font-size: 10px;}.thcenter th, .thcenter td {text-align: center;}</style>' + reportbody,
						reporttype: "4",
						driverid: "<%= DriverID %>",
						drivername: "<%= DriverName %>",
						appkey: "<%= AppKey %>",
						documentdate: $('#Inpdate').val()
					})
				});
				return true;
			}
		</script>
	</head>
	<body>
		<div style="width:29.5cm !important;" class="header">
			Страница печати <b>путевого листа #3</b>. Все, что следует за кнопкой "Печать" будет распечатано.
			<hr class="styleHR"/>
			<form id="Form1" runat="server">
				Дата: <asp:TextBox id="Inpdate" CssClass="input" style="width: 130px;" runat="server"></asp:TextBox> &nbsp;&nbsp;&nbsp;
				Номер путевого листа: <asp:TextBox CssClass="input" runat="server" style="width: 80px;" id="Inpputevoi" ></asp:TextBox>
			</form>
			<span id="Inputdata" onclick="InputData()" style="color: red; cursor: pointer; display: none;">Ввести данны повторно</span>
			<hr class="styleHR"/>
			<input type="button" value="Печать" onclick="PrintElem('#thisPrint')" class="btn" />
		</div>		
		
			<div id="thisPrint" class="this-print" style=" width:29.5cm;">
				<style>
					body {
						font-family: sans-serif;
						font-size: 10px;
						line-height: 2;
					}
					table {
						border-collapse: collapse;
					}
					 .table td, table th {
						 border: 1px solid black;
						 padding: 0px;
						 margin: 0px;
						 height: 20px;
					 }
					 .table th {
						 font-weight: 400;
					 }
					 .tdPadding td{
						 height: 20px;
					 }

					 td, th {
						 font-size: 10px;
					 }
					 .thcenter th, .thcenter td {
						 text-align: center;
					 }
				</style>
				<div style="width: 29.7cm; height: 36cm">
					<div style="height: 21cm">
						
						<div style="width: 30%; text-align: center;">
							<span style="text-decoration: underline;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<%= BackendHelper.TagToValue("put_legal_entity") %>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</span><br/>
							<div style="font-style: italic; font-size: 10px; margin-top: -10px;">(наименование перевозчика)штамп)печать)</div>
						</div>
						<table style="width: 100%">
							<tr>
								<td style="width: 45%">
									<div style="text-align: center; font-size: 16px; font-weight: bold;">
										Путевой лист автомобиля № <span id="putevoi"></span><br/>
										<span id="date"></span>
									</div>

									<table class="table  thcenter" style="width: 100%">
										<tr>
											<td colspan="4">
												<b>Автомобиль, прицеп, полуприцеп</b>
											</td>
										</tr>
										<tr>
											<td>
												Марка автомобиля, прицепа, полуприцепа
											</td>
											<td>
												Регистрационный знак
											</td>
											<td>
												Гаражный номер
											</td>
											<td>
												Код марки
											</td>
										</tr>
										<tr>
											<td>
												1
											</td>
											<td>
												2
											</td>
											<td>
												3
											</td>
											<td>
												4
											</td>
										</tr>
										<tr>
											<td>
												<asp:Label runat="server" ID="lblCarModel"></asp:Label>
											</td>
											<td>
												<asp:Label runat="server" ID="lblCarPassport"></asp:Label>
											</td>
											<td>
											
											</td>
											<td>
											
											</td>
										</tr>
										<tr>
											<td>
												&nbsp;
											</td>
											<td>
											
											</td>
											<td>
											
											</td>
											<td>
											
											</td>
										</tr>
									</table>
									<br/>
									<table class="table  thcenter" style="width: 100%">
										<tr>
											<td colspan="4">
												<b>Водитель</b>
											</td>
										</tr>
										<tr>
											<td>
												Фамилия, инициалы
											</td>
											<td>
												Табельный номер, класс
											</td>
											<td>
												Номер водительского удостоверения
											</td>
											<td>
												Водитель по состоянию здоровья к управлению допущен.
											</td>
										</tr>
										<tr>
											<td>
												5
											</td>
											<td>
												6
											</td>
											<td>
												7
											</td>
											<td>
												8
											</td>
										</tr>
										<tr>
											<td>
												<asp:Label runat="server" ID="lblDriverFIO"></asp:Label>
											</td>
											<td>
											
											</td>
											<td>
												<asp:Label runat="server" ID="lblDriverPassport"></asp:Label>
											</td>
											<td>
											
											</td>
										</tr>
										<tr>
											<td>
												&nbsp;
											</td>
											<td>
											
											</td>
											<td>
											
											</td>
											<td>
											
											</td>
										</tr>
									</table>
								</td>
							
								<td style="width: 1%">
									&nbsp;
								</td>
							
								<td style="width: 45%">
									<table class="table  thcenter" style="width: 100%">
										<tr>
											<td colspan="6">
												<b>Работа водителя и автомобиля</b>
											</td>
										</tr>
										<tr>
											<td rowspan="2">
												Операция
											</td>
											<td rowspan="2">
												Показания спидометра
											</td>
											<td colspan="2">
												Дата (число, месяц), время (ч, мин)
											</td>
											<td colspan="2">
												Время работы, ч
											</td>
										</tr>
										<tr>
											<td>
												по графику
											</td>
											<td>
												фактически
											</td>
											<td>
												двигателя
											</td>
											<td>
												спецоборудования
											</td>
										</tr>
										<tr>
											<td>
												9
											</td>
											<td>
												10
											</td>
											<td>
												11
											</td>
											<td>
												12
											</td>
											<td>
												13
											</td>
											<td>
												14
											</td>
										</tr>
										<tr>
											<td>
												Выезд на линию
											</td>
											<td>
											
											</td>
											<td>
											
											</td>
											<td>
											
											</td>
											<td>
												-
											</td>
											<td>
												-
											</td>
										</tr>
										<tr>
											<td>
												Возвращение с линии
											</td>
											<td>
											
											</td>
											<td>
											
											</td>
											<td>
											
											</td>
											<td>
												-
											</td>
											<td>
												-
											</td>
										</tr>
									</table>
									<br/>
									<table class="table  thcenter" style="width: 100%">
										<tr>
											<td colspan="8">
												<b>Движение топливно-смазочных материалов (ТСМ)</b>
											</td>
										</tr>
										<tr>
											<td colspan="6">
												Заправка ТСМ
											</td>
											<td colspan="2">
												Остаток топлива, л
											</td>
										</tr>
										<tr>
											<td>
												дата (число, месяц)
											</td>
											<td>
												пункт (страна) заправки
											</td>
											<td>
												марка ТСМ
											</td>
											<td>
												код марки
											</td>
											<td>
												количество, л
											</td>
											<td>
												подпись (штамп) уполномоченного лица (номер чека АЗС)
											</td>
											<td>
												при выезде
											</td>
											<td>
												при возвращении
											</td>
										</tr>
										<tr>
											<td>
												15
											</td>
											<td>
												16
											</td>
											<td>
												17
											</td>
											<td>
												18
											</td>
											<td>
												19
											</td>
											<td>
												20
											</td>
											<td>
												21
											</td>
											<td>
												22
											</td>
										</tr>
										<tr>
											<td>
											
											</td>
											<td>
											
											</td>
											<td>
											
											</td>
											<td>
											
											</td>
											<td>
												-
											</td>
											<td>
											
											</td>
											<td>
												-
											</td>
											<td>
												-
											</td>
										</tr>
										<tr>
											<td>
											
											</td>
											<td>
											
											</td>
											<td>
											
											</td>
											<td>
											
											</td>
											<td>
												-
											</td>
											<td>
											
											</td>
											<td colspan="2">
												Подписи (штамп)
											</td>
										</tr>
										<tr>
											<td>
											
											</td>
											<td>
											
											</td>
											<td>
											
											</td>
											<td>
											
											</td>
											<td>
												-
											</td>
											<td>
											
											</td>
											<td>
												механик
											</td>
											<td>
												механик
											</td>
										</tr>
										<tr>
											<td>
											
											</td>
											<td>
											
											</td>
											<td>
											
											</td>
											<td>
											
											</td>
											<td>
												-
											</td>
											<td>
											
											</td>
											<td>
											
											</td>
											<td>
											
											</td>
										</tr>
									</table>
								</td>
							</tr>
						</table>
					
						<br/>
						<table class="table  thcenter">
							<tr>
								<td colspan="9">
									<b>Задание водителю</b>
								</td>
							</tr>
							<tr>
								<td rowspan="2">
									Заказчик
								</td>
								<td colspan="2">
									Дата (число, месяц), время (ч, мин)
								</td>
								<td rowspan="2">
									Пункт отправления (адрес места погрузки)
								</td>
								<td rowspan="2">
									Пункт назначения (адрес места разгрузки)
								</td>
								<td rowspan="2">
									Расстояние, км
								</td>
								<td rowspan="2">
									Наименование груза
								</td>
								<td rowspan="2">
									Вес груза, т
								</td>
								<td rowspan="2">
									Количество ездок с грузом, ч
								</td>
							</tr> 
							<tr>
								<td>
									прибытия
								</td>
								<td>
								
								</td>
							</tr> 
							<tr>
								<td>
									23
								</td>
								<td>
									24
								</td>
								<td>
									25
								</td>
								<td>
									26
								</td>
								<td>
									27
								</td>
								<td>
									28
								</td>
								<td>
									29
								</td>
								<td>
									30
								</td>
								<td>
									31
								</td>
							</tr>  
							<tr>
								<td rowspan="<%= RowSpanNumb + 1 %>">
									<%= BackendHelper.TagToValue("put_legal_entity") %>, <%= BackendHelper.TagToValue("official_address") %>
								</td>
								
							</tr>
							<%= CityStringToHTML %>
						</table>
					
						<div>Опоздания, простои в пути и прочие отметки ____________________________________________________________</div>
					
						<table style="width: 100%">
							<tr>
								<td style="width: 32%">
									<div>
										Водительское удостоверение проверил, задание выдал.
									</div>
									<div>Выдать топлива ______________ л.</div>
									<div>Подпись (штамп) диспетчера _________</div>
									<div>Сопровождающие лица ________________</div>
								</td>
							
							

								<td style="width: 32%">
									<div>
										Автомобиль технически исправен. Выезд разрешен.
									</div>
									<div>Подпись (штамп) механика ___________</div>
									<div>Автомобиль в технически исправном состоянии принял.</div>
									<div>Подпись водителя ___________________</div>
								</td>
							
							

								<td style="width: 32%">
									<div>
										Автомобиль сдал. 
									</div>
									<div>Подпись водителя ___________________________</div>
									<div>Автомобиль принял.</div>
									<div>Подпись (штамп) механика ___________________</div>
								</td>
							</tr>
						</table>
					</div>
					
					<div style="height: 21cm">
						<table class="table  thcenter" style="width: 100%; padding-top: 20px;">
							<tr>
								<td colspan="5">
									<b>Выполнение задания</b>
								</td>
							</tr>
							<tr>
								<td colspan="5">
									<b>Движение по участкам маршрута</b>
								</td>
							</tr>
							<tr>
								<td>
									32
								</td>
								<td>
									33
								</td>
								<td>
									34
								</td>
								<td>
									35
								</td>
								<td>
									36
								</td>
							</tr>
							<tr>
								<td>
									№ ездки
								</td>
								<td>
									Начальный пункт
								</td>
								<td>
									Конечный пункт
								</td>
								<td>
									Пробег, км
								</td>
								<td>
									Масса груза,кг
								</td>
							</tr>
							<tr>
								<td>1</td>
								<td></td>
								<td></td>
								<td></td>
								<td></td>
							</tr>
							<tr>
								<td>2</td>
								<td></td>
								<td></td>
								<td></td>
								<td></td>
							</tr>
							<tr>
								<td>3</td>
								<td></td>
								<td></td>
								<td></td>
								<td></td>
							</tr>
							<tr>
								<td>4</td>
								<td></td>
								<td></td>
								<td></td>
								<td></td>
							</tr>
							<tr>
								<td>5</td>
								<td></td>
								<td></td>
								<td></td>
								<td></td>
							</tr>
							<tr>
								<td>6</td>
								<td></td>
								<td></td>
								<td></td>
								<td></td>
							</tr>
							<tr>
								<td>7</td>
								<td></td>
								<td></td>
								<td></td>
								<td></td>
							</tr>
							<tr>
								<td>8</td>
								<td></td>
								<td></td>
								<td></td>
								<td></td>
							</tr>
							<tr>
								<td>9</td>
								<td></td>
								<td></td>
								<td></td>
								<td></td>
							</tr>
							<tr>
								<td>10</td>
								<td></td>
								<td></td>
								<td></td>
								<td></td>
							</tr>
							<tr>
								<td>11</td>
								<td></td>
								<td></td>
								<td></td>
								<td></td>
							</tr>
							<tr>
								<td>12</td>
								<td></td>
								<td></td>
								<td></td>
								<td></td>
							</tr>
							<tr>
								<td>13</td>
								<td></td>
								<td></td>
								<td></td>
								<td></td>
							</tr>
							<tr>
								<td>14</td>
								<td></td>
								<td></td>
								<td></td>
								<td></td>
							</tr>
							<tr>
								<td>15</td>
								<td></td>
								<td></td>
								<td></td>
								<td></td>
							</tr>
							<tr>
								<td>16</td>
								<td></td>
								<td></td>
								<td></td>
								<td></td>
							</tr>
							<tr>
								<td>17</td>
								<td></td>
								<td></td>
								<td></td>
								<td></td>
							</tr>
							<tr>
								<td>18</td>
								<td></td>
								<td></td>
								<td></td>
								<td></td>
							</tr>
							<tr>
								<td>19</td>
								<td></td>
								<td></td>
								<td></td>
								<td></td>
							</tr>
							<tr>
								<td colspan="3" style="text-align: right">Общий пробег</td>
								<td colspan="2"></td>
							</tr>
							<tr>
								<td colspan="5">
									<br/>
									Подпись и печать(штамп) заказчика_________________________
								</td>
							</tr>
						</table>
					</div>

				</div>
			</div>
			<div style="width:29.5cm;" class="footer">
				<input type="button" value="Печать" onclick="PrintElem('#thisPrint')" class="btn" />
			</div>
	</body>
</html>
