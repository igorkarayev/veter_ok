<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PrintZP.aspx.cs" Inherits="Delivery.PrintServices.PrintZP "%>
<%@ Import Namespace="System.Web.Optimization" %>
<%@ Import Namespace="Delivery.BLL" %> <%@ Import Namespace="Delivery.BLL.StaticMethods" %>
<%@ Import Namespace="Delivery.BLL.Helpers" %>
<%@Register Tagname="GoodsList" Tagprefix ="grb" src="~/PrintServices/Controls/GoodsListForZP.ascx" %> 
<!DOCTYPE html>

<html>
	<head>
		<title>Печать зак.пор. <%= BackendHelper.TagToValue("page_title_part") %></title>
		<link rel="icon" type="image/png" href="<%=ResolveClientUrl("~/Styles/Images/favicon.png") %>" />
		<%: Scripts.Render("~/js/jquery") %>
		<link rel="stylesheet" href="<%=ResolveClientUrl("~/Styles/print.css") %>">
		<style>.notprint{display: none;}</style>
		<script type="text/javascript">
			function InputData() {
				$('#Form1').show();
				$('.putevoi').hide();
				$('.nakl').hide();
				$('#inputdata').hide();
			}

			function PrintElem(elem) {
				$('.putevoi').html($('#Inpputevoi').val());
				$('.nakl').show();
				$('.nakl').html($('#Inpnaklnumber').val());
				$('#Form1').hide();
				$('.putevoi').show();
				$('#inputdata').show();
				
				$.ajax({
					type: "POST",
					url: "../AppServices/SaveAjaxService.asmx/SavePrintNaklParams",
					data: ({
						putevoi: $('#Inpputevoi').val(),
						appkey: "<%= AppKey %>",
						naklnumber: $('#Inpnaklnumber').val(),
						naklseria: "",
						nakldate: ""
					})
				});
				Popup($(elem).html());
			}

			function Popup(data) {
				var mywindow = window.open('', 'Заказ-поручение', 'width="7.8cm"');
				mywindow.document.write(data);
				mywindow.print();
				mywindow.close();

				var reportbody = '<!DOCTYPE html><html><head><meta charset="utf-8"><title>Заказ-поручение</title></head><body >' + data + '</body></html>';
				$.ajax({
					type: "POST",
					url: "../AppServices/ReportService.asmx/SaveReport",
					data: ({
						body: '<style>.big {font-weight: bold;}.printZP {border-collapse: collapse;}.printZP td{border: 1px solid black !important;padding: 2px;height: 15px;font-size: 8px;}</style>' + reportbody,
						reporttype: "1",
						driverid: "<%= DriverID %>",
						drivername: "<%= DriverName %>",
						appkey: "<%= AppKey %>",
						documentdate: "<%= AdmissionDate %>"
					})
				});

				return true;
			}
		</script>
	</head>
	<body>
		<div>
			<div style="width:20.7cm;" class="header">
				Страница печати <b>заказа-поручения</b>. Все, что следует за кнопкой "Печать" будет распечатано.
				<hr class="styleHR"/>
				<form id="Form1" runat="server">
					Номер путевого листа: <asp:TextBox runat="server" ID="Inpputevoi" style="width: 80px;" CssClass="input"></asp:TextBox> &nbsp;&nbsp;&nbsp;
					Номер накладной: <asp:TextBox runat="server" ID="Inpnaklnumber" style="width: 80px;" CssClass="input"></asp:TextBox>
					<asp:CheckBox runat="server" ID="cbWithUr" OnCheckedChanged="cbWithUr_CheckedChanged" AutoPostBack="true"/> с юр. лицами
				</form>
				<span id="inputdata" onclick="InputData()" style="color: red; cursor: pointer; display: none;">Ввести данны повторно</span>
				<hr class="styleHR"/>
				<input type="button" value="Назад" onclick="window.history.go(-1); return false;" class="btn" style="width: 14%; margin-right: 1%" />
				<input type="button" value="Печать" onclick="PrintElem('#thisPrint')" class="btn" style="width: 84%" />
			</div>		

			<div id="thisPrint" class="this-print" style="width:20.7cm;">
				<style>
					.big {
						font-weight: bold;
					}
			
					.printZP {
						border-collapse: collapse;
					}

					.printZP td{
						border: 1px solid black !important;
						padding: 2px;
						height: 15px;
						font-size: 8px;
					}

				</style>
			   <asp:ListView runat="server" ID="lvAllPrint">
					<LayoutTemplate>
						<div runat="server" id="itemPlaceholder"></div>
					</LayoutTemplate>
					<ItemTemplate>
						<div id="Tr2" runat="server" style="width:20.7cm; height: 14.85cm !important; overflow: hidden !important; font-size: 8px !important; font-family: sans-serif">
							
							<div style=" margin-top: 5px; width: 7cm; display: inline-block">
								<%= BackendHelper.TagToValue("zp_legal_entity") %> 
                                ЗАКАЗ-ПОРУЧЕНИЕ к ТТН №<span class="nakl"></span><br/>
								(наименование (фамилия, собственное имя, <br/>
								(если  таковое имеется) перевозчика (штамп (печать)
							</div>
							
							<div style=" margin-top: 5px; margin-left: 1cm; width: 3cm; display: inline-block; vertical-align: top;">
								<span style="font-size: 12px; font-weight: bold;"><%# Eval("SecureID") %> от <%# OtherMethods.DateConvertWithDot(Eval("AdmissionDate").ToString()) %></span>

							</div>
							
							<div style=" margin-top: 5px; margin-left: 0cm; width:9.5cm; display: inline-block; vertical-align: top;">
								УТВЕРЖДЕН <br/>
								Постановление Министерства транспорта и коммуникаций Республики
								Беларусь 23.08.2000 № 19
							</div>
							
							
							<div style=" margin-top: 0px; width: 11cm; display: inline-block; vertical-align: top">
								Автомобиль <%# DriversHelper.DriverIDToCarZP(Eval("DriverID").ToString())%> к путевому листу № <span class="putevoi"></span><br/>
								<div style="margin-left: 80px; margin-top: -4px;">(марка, регистрационный знак)</div>
							</div>
							
							<div style=" margin-top: 0px; margin-left: 0cm; width: 9.5cm; display: inline-block">
								СОГЛАСОВАНО <br/>
								Заместитель Министра финансов Республики Беларусь  А.Б.Радкевич 11.08.2000
							</div>
							

							<div style=" margin-top: -5px; width: 20.5cm; display: inline-block; vertical-align: top">
								Заказчик    <asp:Label ID="Label10" runat="server" Text='<%# (Eval("FirstName").ToString()) %>' /> &nbsp;
											<asp:Label ID="Label17" runat="server" Text='<%# (Eval("LastName").ToString()) %>' /> &nbsp;
											<asp:Label ID="Label18" runat="server" Text='<%# (Eval("ThirdName").ToString()) %>' />
							</div>
							
							
							<div style=" margin-top: -0px; width: 20.5cm; display: inline-block; vertical-align: top">
								Водитель <%# DriversHelper.DriverIDToNameZP(Eval("DriverID").ToString())%> &nbsp;&nbsp;&nbsp; Грузчики _______ &nbsp;&nbsp;&nbsp; Срок исполнения заказа &nbsp;&nbsp;&nbsp; _______	__ ч __ мин
							</div>
							
							
							<div style=" margin-top: -0px; width: 100%; display: inline-block; vertical-align: top">
								<div style="display: inline-block; width: 12.5cm; vertical-align: top">
									Пункт погрузки <%# TicketsHelper.SenderAddress(Eval("ID").ToString()) %> &nbsp;&nbsp;&nbsp; Пункт разгрузки <%# CityHelper.CityIDToCityName(Eval("CityID").ToString()) %>, <%# String.Format("{1} {0}",Eval("RecipientStreet"),Eval("RecipientStreetPrefix")) %>  <%# OtherMethods.GetRecipientAddressWithoutStreet(Eval("ID").ToString()) %>
								</div>
								<div style="font-size: 8px; display: inline-block; width: 8.0cm; vertical-align: top">
									Расстояние перевозки <%# CityHelper.CityIDToDistance(Eval("CityID").ToString(), Eval("ID").ToString()) %> км &nbsp;&nbsp;&nbsp; Груз для доставки принял <u><%# DriversHelper.DriverIDToNameZP(Eval("DriverId").ToString(), true)%></u><br/>
									<div style="margin-left: 140px;">(должность, фамилия, инициалы, подпись)</div>
								</div>
							</div>

							<grb:GoodsList id="GoodsList" runat="server" TicketID='<%# Eval("ID").ToString() %>'/>

							<div style="margin-left: 50px; margin-top: -10px;">ОЦЕНКА СТОИМОСТИ УСЛУГ</div>
							<table runat="server" id="Table1" class="printZP" style="width: 100%">
								<tr>
									<td style="width: 4cm;">
										Наименование услуги
									</td>
									<td style="width: 4.2cm;">
										Стоимость
									</td>
									<td style="width: 4.2cm;">
										Ставка НДС &lt;*&gt;, %
									</td>
									<td style="width: 4.2cm;">
										Сумма НДС
									</td>
									<td style="width: 4.2cm;">
										Стоимость услуги с НДС   
									</td>
								</tr>
								<tr>
									<td>
										Перевозка
									</td>
									<td  style="text-align: right">
										<%# 
											MoneyMethods.MoneySeparator(Convert.ToDecimal(Eval("GruzobozCost").ToString()) / Convert.ToDecimal(1  + Convert.ToInt32(StavkaNDS)/ 100))
										%>
									</td>
									<td  style="text-align: center">
										<%#
                                            StavkaNDS
										%>
									</td>
									<td style="text-align: right">
										 <%# MoneyMethods.MoneySeparator(Convert.ToDecimal(Eval("GruzobozCost").ToString()) - Convert.ToDecimal(Eval("GruzobozCost").ToString()) / Convert.ToDecimal(1  + Convert.ToInt32(StavkaNDS)/ 100))%>
									</td>
									<td style="text-align: right">
										<%# MoneyMethods.MoneySeparator(Eval("GruzobozCost").ToString())%>
									</td>
								</tr>
							</table>
							<div style="margin-top: 0px;">
								<div style="display: inline-block; width: 13cm; vertical-align: top;padding-top: 5px">
									Всего стоимость услуг с НДС <u><%# MoneyHelper.ToRussianString(Convert.ToDouble(Eval("GruzobozCost").ToString())) %></u>
								</div>
								<div style="display: inline-block; width: 7.5cm">
									<table style="margin-top: 0px; margin-left: -4px; font-size: 10px; width: 100%">
										<tr>
											<td>
												Заказчик ____________
											</td>
											<td>
												Кассир _____________
											</td>
										</tr>
										<tr>
											<td style="padding-left: 60px;">
												<div style="margin-top: -7px">(подпись)</div>
											</td>
											<td style="padding-left: 40px;">
												<div style="margin-top: -7px">(подпись (штамп)</div>
											</td>
										</tr>
									</table>
								</div>
							</div>
							<div style="margin-left: 50px; margin-top: -10px;">РАБОТА АВТОМОБИЛЯ</div>
							<table class="printZP" style="width: 100%">
								<tr>
									<td style="width: 4.4cm">
										Автомобиль
									</td>
									<td style="width: 1.8cm">
										Время (часов, мин)
									</td>
									<td style="width: 1.8cm">
										Дата (число, месяц)
									</td>
									<td style="border-top: 1px solid white !important; width: 2cm; border-bottom: 1px solid white !important;">
										
									</td>
									<td style="width: 3.5cm">
										Наименование услуги
									</td>
									<td style="width: 1.5cm">
										Стоимость
									</td>
									<td style="width: 1cm">
										Ставка  НДС, %
									</td>
									<td style="width: 1cm">
										Сумма НДС
									</td>
									<td style="width: 2cm">
										Стоим. усл. с НДС
									</td>
									<td style="width: 2cm">
										Подлежит к  допл., возв.
									</td>
								</tr>
								<tr>
									<td style="height: 20px;">
										
									</td>
									<td>
										
									</td>
									<td>
										
									</td>
									<td style="border-top: 1px solid white !important; border-bottom: 1px solid white !important;">
										
									</td>
									<td>
										
									</td>
									<td>
										
									</td>
									<td>
										
									</td>
									<td>
										
									</td>
									<td>
										
									</td>
									<td>
										
									</td>
								</tr>
								<tr>
									<td style="height: 50px !important; margin-top: 5px; width: 8cm; border-bottom: 1px dashed black !important; border-left: none !important; border-right: none !important; vertical-align: middle" colspan="3">
										Груз получил____________________(подпись заказчика)       
									</td>
									<td style="border-top: 1px solid white !important; width: 2cm; border-bottom: 1px dashed black !important; border-left: none !important; border-right: none !important;">
										&nbsp;
									</td>
									<td style="width: 11cm; border-bottom: 1px dashed black !important; border-left: none !important; border-right: none !important;" colspan="6">
										Кассир _______________(подпись (штамп)<span style="padding-left: 15px">Заказчик ______________(подпись)</span>
									</td>
								</tr>
							</table>
							<div style="text-align: center; font-weight: bold; margin-top: 15px; font-size: 10px;">
								Акт приемки оказанных услуг
							</div>
							<div>
								Заказчик и Исполнитель составили настоящий акт о том, что услуги по доставке груза выполнены надлежащим образом. 
								Закачзик к качеству оказанных услуг и доставленному грузу претензий не имеет.
							</div>
							<table>
								<tr style="border-bottom: 2px dashed black !important;">
									<td style="height: 20px !important; margin-top: 5px; width: 8cm; border-bottom: 2px dashed black !important; border-left: none !important; border-right: none !important; vertical-align: middle" colspan="3">
										Заказчик____________________(подпись)       
									</td>
									<td style="border-top: 1px solid white !important; width: 2cm; border-bottom: 2px dashed black !important; border-left: none !important; border-right: none !important;">
										&nbsp;
									</td>
									<td style="width: 11cm; border-bottom: 2px dashed black !important; border-left: none !important; border-right: none !important;" colspan="6">
										<span>Исполнитель____________________(подпись)</span>
									</td>
								</tr>
							</table>
						</div>
					</ItemTemplate>
				</asp:ListView>
			</div>
			<div style="width:20.7cm;" class="footer">
				<input type="button" value="Назад" onclick="window.history.go(-1); return false;" class="btn" style="width: 14%; margin-right: 1%" />
				<input type="button" value="Печать" onclick="PrintElem('#thisPrint')" class="btn" style="width: 84%" />
			</div>
		</div>
	</body>
</html>
