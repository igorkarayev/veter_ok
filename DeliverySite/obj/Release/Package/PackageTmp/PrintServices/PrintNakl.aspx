<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PrintNakl.aspx.cs" Inherits="Delivery.PrintServices.PrintNakl "%>
<%@ Import Namespace="System.Web.Optimization" %>
<%@ Import Namespace="Delivery.BLL" %> <%@ Import Namespace="Delivery.BLL.StaticMethods" %> <%@ Import Namespace="Delivery.BLL.Helpers" %>

<!DOCTYPE html>

<html>
	<head>
		<title>Печать наклaдной <%= BackendHelper.TagToValue("page_title_part") %></title>
		<link rel="icon" type="image/png" href="<%=ResolveClientUrl("~/Styles/Images/favicon.png") %>" />
		<%: Scripts.Render("~/js/jquery") %>
		<script src='<%=ResolveClientUrl("~/Scripts/CustomScripts/ajax-save-functions.js") %>' ></script>
		<link rel="stylesheet" href="<%=ResolveClientUrl("~/Styles/print.css") %>">
		<script type="text/javascript">
			function InputData() {
				$('#Form1').show();
				$('#naklnumber').hide();
				$('#putevoi').hide();
				$('#date').hide();
				$('#seria').hide();
				$('#inputdata').hide();
			}
			function PrintElem(elem) {
				$('#naklnumber').html($('#Inpnaklnumber').val());
				$('#naklnumber').show();
				
				$('#putevoi').html($('#Inpputevoi').val());
				$('#putevoi').show();
				
				$('#date').html($('#Inpdate').val());
				$('#date').show();
				
				$('#seria').html($('#Inpseria').val());
				$('#seria').show();

				$('#inputdata').show();
				$('#Form1').hide();

				$.ajax({
					type: "POST",
					url: "../AppServices/SaveAjaxService.asmx/SavePrintNaklParams",
					data: ({
						appkey: "<%= AppKey %>",
						naklnumber: $('#Inpnaklnumber').val(),
						naklseria: $('#Inpseria').val(),
						nakldate: $('#Inpdate').val(),
						putevoi: $('#Inpputevoi').val()
					})
				});
				Popup($(elem).html());
			}

			function Popup(data) {
				var mywindow = window.open('', 'Накладная', 'width="21.0cm"');
				mywindow.document.write(data);
				mywindow.print();
				mywindow.close();
				return true;
			}
		</script>
	</head>
	<body>
		<div style="width:26cm !important;" class="header">
			Страница печати <b>накладной</b>. Все, что следует за кнопкой "Печать" будет распечатано.
			<hr class="styleHR"/>
			<form id="Form1" runat="server">
				Дата: <asp:TextBox id="Inpdate"  style="width: 130px"  runat="server"/> &nbsp;&nbsp;
				Номер путевого листа: <asp:TextBox CssClass="input" runat="server" style="width: 80px" id="Inpputevoi" />&nbsp;&nbsp;
				Номер накладной: <asp:TextBox CssClass="input" runat="server"  style="width: 80px" id="Inpnaklnumber" />&nbsp;&nbsp;
				Серия накладной: <asp:TextBox CssClass="input" runat="server" style="width: 50px" id="Inpseria" />
				<asp:CheckBox runat="server" ID="cbWithUr" OnCheckedChanged="cbWithUr_CheckedChanged" AutoPostBack="true"/> с юр.
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
						line-height: 2;
					}
					table {
						border-collapse: collapse;
					}
					 .table td, table th {
						 border: 1px solid black;
					 }
					 .table th {
						 font-weight: 400;
					 }
					 .tdPadding td{
						 height: 20px;
					 }

					 td, th {
						 font-size: 12px;
					 }
					 .thcenter th, .thcenter td {
						 text-align: center;
					 }
				</style>
				<div style="width:26cm !important;  overflow: hidden;">
					<table style="margin-left: auto; margin-right: auto;" class="table">
						<tr>
							<td style="padding: 4px">Грузоотправитель</td>
							<td style="padding: 4px">Грузополучатель</td>
						</tr>
						<tr>
							<td style="text-align: center"><%= BackendHelper.TagToValue("ttn_sender_unp") %></td>
							<td style="text-align: center"><%= BackendHelper.TagToValue("ttn_receiver_unp") %></td>
						</tr>
					</table>
					<br/>
					<br/>
					<div style="font-size: 20px; font-weight: bold; padding-bottom: 20px;"><span id="date"></span>
						
					</div>  
					<div>
						<b>Автомобиль</b> <div style="display: inline-block; width: 9cm; text-decoration: underline"><asp:Label runat="server" ID="lblCar"></asp:Label></div> <b>Прицеп __________________________</b> <b>К путевому листу № <u><span id="putevoi"></span></u></b>
					</div>
					<div>
						<!--b>Владелец автомобиля</b>  <u style="margin-right: 3.3cm;"><!--%= BackendHelper.TagToValue("ttn_car_owner") %></u-->  
                        <b>Водитель</b> <u><asp:Label runat="server" ID="lblDriver"></asp:Label></u>
					</div>
					<div style="text-align: justify">
						<b>Заказчик автомобильной перевозки(плательщик)</b> _____________________________________________________________________________
					</div>
					<div>
						<b>Грузоотправитель</b> <u><%= BackendHelper.TagToValue("ttn_sender_name") %>, <%= BackendHelper.TagToValue("ttn_sender_address") %></u>
					</div>
					<div>
						<b style="margin-right: 0.2cm">Грузополучатель</b> <u><%= BackendHelper.TagToValue("ttn_receiver_name") %>, <%= BackendHelper.TagToValue("ttn_receiver_address") %></u>
					</div>
					<div  style="text-align: justify">
						<b>Основание отпуска</b> <u>Заказ-поручения согласно прил. 1</u> <b>Пункт погрузки</b> <u><%= BackendHelper.TagToValue("loading_point_address") %></u> <b>Пункт разгрузки</b> <u>Согласно заказ-поручений</u>
					</div>
					<div>
						<b>Переадресовка</b> ___________________________________________________________________________________________________________<br/>
						__________________________________________________________________________________________________________________________
					</div>
					<div style="text-align: center">I.ТОВАРНЫЙ РАЗДЕЛ</div>
					<table class="table">
						<tr class="thcenter">Всего стоимость
							<th>
								Наименование груза
							</th>
							<th>
								Ед. изме- рения
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
								Стоимость с НДС
							</th>
							<th style="width: 1cm">
								Кол-во грузо- вых мест
							</th>
							<th>
								Масса груза
							</th>
							<th style="width: 3cm">
								Примечания
							</th>
						</tr>
						<tr class="thcenter">
							<td>1</td>
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
						</tr>
						<tr>
							<th>
								Груз согласно приложения №1 на одном листе
							</th>
							<th>
								шт
							</th>
							<th>
								х
							</th>
							<th>
								х
							</th>
							<th>
								<asp:Label runat="server" ID="lblCost"></asp:Label>
							</th>
							<th>
								Без НДС
							</th>
							<th>
								-
							</th>
							<th>
								<asp:Label runat="server" ID="lblCost3"></asp:Label>
							</th>
							<th>
								<asp:Label runat="server" ID="lblBoxes2"></asp:Label>
							</th>
							<th>
								<asp:Label runat="server" ID="lblWeight"></asp:Label>
							</th>
							<th>
										
							</th>
						</tr>
						<tr>
							<th>
								Итого
							</th>
							<th>
								х
							</th>
							<th>
								x
							</th>
							<th>
								x
							</th>
							<th>
								<asp:Label runat="server" ID="lblCost2"></asp:Label>
							</th>
							<th>
								х
							</th>
							<th>
								х
							</th>
							<th>
								<asp:Label runat="server" ID="lblCost4"></asp:Label>
							</th>
							<th>
								<asp:Label runat="server" ID="lblBoxes3"></asp:Label>
							</th>
							<th>
								<asp:Label runat="server" ID="lblWeight2"></asp:Label>
							</th>
							<th>
										
							</th>
						</tr>
					</table>
					<div style="text-align: right; padding-right: 3cm">
						Количество поездок, заездов 1
					</div>
					<div>
						<b>Всего сумма НДС <u style="margin-left: 1.1cm">без НДС</u></b>
					</div>
					<div>
						<b>Всего стоимость с НДС <u><asp:Label runat="server" ID="lblCostWord"></asp:Label></u></b>
					</div>
					<table style="width: 100%">
						<tr>
							<td style="width: 50%; vertical-align: top;  border-bottom: 1px solid black">
								<div style="margin-bottom: 15px;">
									Всего масса груза <u  style="margin-left: 0.8cm"><asp:Label runat="server" ID="lblWeightWord"></asp:Label> килограмм&nbsp;&nbsp;&nbsp;</u> 
								</div>
								<div style="margin-bottom: 15px;">
									Отпуск разрешил <u style="margin-left: 0.9cm"><%= BackendHelper.TagToValue("ttn_released_person_position") %>, <%= BackendHelper.TagToValue("ttn_released_person_name") %></u>
								</div>
								<div>
									Сдал грузоотправитель <u style="margin-right: 50px"><%= BackendHelper.TagToValue("ttn_goods_gave_person_position") %>, <%= BackendHelper.TagToValue("ttn_goods_gave_person_name") %></u> № пломбы <u>_________</u>
								</div>
								<div style="margin-top: 2cm">
									Штамп (печать) грузоотправителя
								</div>
							</td>
							<td style="border-left: 1px solid black; border-bottom: 1px solid black">
								<div style="margin-bottom: 15px;">
									Всего количество грузовых мест <u><asp:Label runat="server" ID="lblBoxesWord"></asp:Label></u>
								</div>
								<div style="margin-bottom: 15px;">
									Груз к перевозке принял <u style="margin-left: 0.8cm">Водитель <asp:Label runat="server" ID="lblDriver3"></asp:Label></u>
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
					 
					<div>II.ПОГРУЗОЧНО-РАЗГРУЗОЧНЫЕ ОПЕРАЦИИ</div>
					<div style="display: inline-block; width: 18.5cm">
						<table class="table" style="width: 18cm">
							<tr class="thcenter">
								<td rowspan="2">Операция</td>
								<td rowspan="2">Исполнитель</td>
								<td rowspan="2">Способ (ручной, механизированный)</td>
								<td>Код</td>
								<td colspan="3">Дата, время (ч, мин)</td>
								<td colspan="2">Дополнительные операции</td>
								<td rowspan="2">Подпись</td>
							</tr>
							<tr>
								<td></td>
								<td>прибытия</td>
								<td>убытия</td>
								<td>простоя</td>
								<td>время</td>
								<td>наименование</td>
							</tr>
							<tr class="thcenter">
								<td></td>
								<td>12</td>
								<td>13</td>
								<td>14</td>
								<td>15</td>
								<td>16</td>
								<td>17</td>
								<td>18</td>
								<td>19</td>
								<td>20</td>
							</tr>
							<tr>
								<td>Погрузка</td>
								<td></td>
								<td>Ручная</td>
								<td></td>
								<td></td>
								<td></td>
								<td></td>
								<td></td>
								<td></td>
								<td></td>
							</tr>
							<tr>
								<td>Разгрузка</td>
								<td></td>
								<td>Ручная</td>
								<td></td>
								<td></td>
								<td></td>
								<td></td>
								<td></td>
								<td></td>
								<td></td>
							</tr>
						</table>
					</div>
					<div style="width: 7cm; display: inline-block;vertical-align: top; font-size: 12px;">
						Транспортные услуги ____________________<br/>
						______________________________________<br/>
						______________________________________<br/>
						______________________________________<br/>
						______________________________________<br/>
						______________________________________<br/>
					</div>
					<div>III.ПРОЧИЕ СВЕДЕНИЯ(заполняются перевозчиком)</div>
					<div style="display: inline-block; width: 18.5cm">
						<table class="table"  style="width: 18cm">
							<tr class="thcenter">
								<td colspan="5">Расстояние перевозки по группам дорог, км</td>
								<td rowspan="2">Код экспе- дирования</td>
								<td rowspan="2">За транспортные услуги</td>
								<td colspan="2">Поправочный коэффициент</td>
								<td rowspan="2">Штраф</td>
								<td rowspan="2"></td>
							</tr>
							<tr  class="thcenter">
								<td>всего</td>
								<td>в гор.</td>
								<td>I</td>
								<td>II</td>
								<td>III</td>
								<td>расцен-</td>
								<td>основной тариф</td>
							</tr>
							<tr class="thcenter">
								<td>21</td>
								<td>22</td>
								<td>23</td>
								<td>24</td>
								<td>25</td>
								<td>26</td>
								<td>27</td>
								<td>28</td>
								<td>29</td>
								<td>30</td>
								<td style="width: 1.2cm">31</td>
							</tr>
							<tr class="tdPadding">
								<td></td>
								<td></td>
								<td></td>
								<td></td>
								<td></td>
								<td></td>
								<td></td>
								<td></td>
								<td></td>
								<td></td>
								<td></td>
							</tr>
						</table>
					</div>
					<div style="width: 7cm; display: inline-block;vertical-align: top; font-size: 12px;">
						Отметки о составленых актах _____________<br/>
						______________________________________<br/>
						______________________________________<br/>
						______________________________________<br/>
						______________________________________<br/>
					</div>
					

					<br/><br/>
					<div style="display: inline-block; width: 18.5cm">
						<table class="table" style="width: 18cm">
							<tr class="thcenter">
								<td rowspan="2">Расчет стоимости</td>
								<td>За тон- ны</td>
								<td rowspan="2">За расстоя- ние перевозки</td>
								<td rowspan="2">За специ- альный транспорт</td>
								<td rowspan="2">За транс- портные услуги</td>
								<td rowspan="2">Погрузочно- разгрузочные работы,т</td>
								<td colspan="2">Сверхнормативный простой</td>
								<td rowspan="2">Прочие допла- ты</td>
								<td rowspan="2">Дополни- тельные ус- луги(экспе- дирование)</td>
								<td colspan="2">К оплате</td>
							</tr>
							<tr class="thcenter">
								<td></td>
								<td>погрузка</td>
								<td>разгрузка</td>
								<td>итого</td>
								<td>в том</td>
							</tr>
							<tr class="thcenter">
								<td></td>
								<td>32</td>
								<td>33</td>
								<td>34</td>
								<td>35</td>
								<td>36</td>
								<td>37</td>
								<td>38</td>
								<td>39</td>
								<td>40</td>
								<td style="width: 1.2cm">41</td>
								<td style="width: 1cm">42</td>
							</tr>
							<tr>
								<td>По заказу</td>
								<td></td>
								<td></td>
								<td></td>
								<td></td>
								<td></td>
								<td></td>
								<td></td>
								<td></td>
								<td></td>
								<td></td>
								<td></td>
							</tr>
							<tr>
								<td>Выполнено</td>
								<td></td>
								<td></td>
								<td></td>
								<td></td>
								<td></td>
								<td></td>
								<td></td>
								<td></td>
								<td></td>
								<td></td>
								<td></td>
							</tr>
							<tr>
								<td>Расценка</td>
								<td></td>
								<td></td>
								<td></td>
								<td></td>
								<td></td>
								<td></td>
								<td></td>
								<td></td>
								<td></td>
								<td></td>
								<td></td>
							</tr>
							<tr>
								<td>К оплате</td>
								<td></td>
								<td></td>
								<td></td>
								<td></td>
								<td></td>
								<td></td>
								<td></td>
								<td></td>
								<td></td>
								<td></td>
								<td></td>
							</tr>
						</table>
					</div>
					<div style="width: 7cm; display: inline-block;vertical-align: bottom; font-size: 12px; padding-bottom: 1cm">
						Таксировка ____________________________<br/>
						______________________________________<br/>
						______________________________________<br/>
						______________________________________<br/>
						______________________________________<br/>
					</div>
					<div>С грузом переданы документы: Товарно-транспортная накладная № <span id="naklnumber"></span> Серия <span id="seria"></span></div>
				</div>
			</div>
			<div style="width:26cm;" class="footer">
				<input type="button" value="Назад" onclick="window.history.go(-1); return false;" class="btn" style="width: 14%; margin-right: 1%" />
				<input type="button" value="Печать" onclick="PrintElem('#thisPrint')" class="btn" style="width: 84%" />
			</div>
	</body>
</html>
