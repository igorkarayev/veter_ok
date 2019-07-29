<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PrintMap.aspx.cs" Inherits="Delivery.PrintServices.PrintMap "%>
<%@ Import Namespace="System.Web.Optimization" %>
<%@ Import Namespace="Delivery.BLL" %> <%@ Import Namespace="Delivery.BLL.StaticMethods" %>
<%@ Import Namespace="Delivery.BLL.Helpers" %>

<!DOCTYPE html>

<html>
	<head>
		<title>Печать карты курьерам <%= BackendHelper.TagToValue("page_title_part") %></title>
		<link rel="icon" type="image/png" href="<%=ResolveClientUrl("~/Styles/Images/favicon.png") %>" />
		<%: Scripts.Render("~/js/jquery") %>
		<link rel="stylesheet" href="<%=ResolveClientUrl("~/Styles/print.css") %>">
		<script type="text/javascript">
			function InputData() {
				$('#Form1').show();
				$('#date').hide();
				$('#inputdata').hide();
			}

			function PrintElem(elem) {
				$('#date').html($('#Inpdate').val());
				$('#date').show();
				$('#inputdata').show();

				$('#Form1').hide();
				Popup($(elem).html());

				$.ajax({
					type: "POST",
					url: "../AppServices/SaveAjaxService.asmx/SaveMapParams",
					data: ({
						appkey: "<%= AppKey %>",
						mapdate: $('#Inpdate').val()
					})
				});
			}

			function Popup(data) {
				var mywindow = window.open('', 'Карта', 'width="7.8cm"');
				mywindow.document.write(data);
				mywindow.print();
				mywindow.close();

				if ("<%= DriverName %>") {
					var reportbody = '<!DOCTYPE html><html><head><meta charset="utf-8"><title>Карта</title></head><body >' + data + '</body></html>';
					$.ajax({
						type: "POST",
						url: "../AppServices/MailService.asmx/SentMap",
						data: ({
							body: '<style>body {font-family: sans-serif;font-size: 6pt;}.mapTable td, .mapTable th{border: 1px solid #000;padding: 0px;font-size: 6pt;}.mapTable{border-collapse: collapse;}.big {font-weight: bold;}</style>' + reportbody,
							drivername: "<%= DriverName %>",
							appkey: "<%= AppKey %>"
						})
					});
				}

				return true;
			}
		</script>
		
	</head>
	<body>
		<div>
			<div style="width: 29.7cm;" class="header">
				Страница печати <b>карты для курьеров</b>. Все, что следует за кнопкой "Печать" будет распечатано
				<hr class="styleHR"/>
				<form id="Form1" runat="server">
					Дата: <asp:TextBox CssClass="input" runat="server" id="Inpdate" style="width: 100px" ></asp:TextBox>  
				</form>
				<span id="inputdata" onclick="InputData()" style="color: red; cursor: pointer; display: none;">Ввести данны повторно</span>
				<hr class="styleHR"/>
				<input type="button" value="Назад" onclick="window.history.go(-1); return false;" class="btn" style="width: 14%; margin-right: 1%" />
				<input type="button" value="Печать" onclick="PrintElem('#thisPrint')" class="btn" style="width: 84%;"/>
			</div>	
				
			<div id="thisPrint" style="width: 29.7cm" class="this-print">
				<style>
					body {
						font-family: sans-serif;
						font-size: 6pt;
					}
					 .mapTable td, .mapTable th{
						 border: 1px solid #000;
						 padding: 2px;
						 font-size: 6pt;
					 }
					.mapTable{
						border-collapse: collapse;
					}
					.big {
						font-weight: bold;
					}
                    .bigfont {
                        font-size: 9pt;
                    }
				</style>
				<div style="font-size: 16px; font-weight: bold">
					<div style="font-size: 10px; margin-left: 0px; font-weight: normal;">
						<div style="width: 28%; padding: 5px; display: inline-block; vertical-align: top;">
							Направления: <asp:Label ID="lblTrack2" runat="server" style="font-weight: bold; font-size: 12px;"></asp:Label><br/>
							Оператор <asp:Label ID="lblOperatorName" runat="server"></asp:Label>: <asp:Label ID="lblOperatorPhone" runat="server" style="font-weight: bold; font-size: 12px;"></asp:Label>
						</div>
						<div style="width: 40%; padding: 5px; display: inline-block; vertical-align: top;">
							Оператор: <b><%= BackendHelper.TagToValue("operator_phone") %></b> Общие номера для справки: <b><%= BackendHelper.TagToValue("main_phones") %></b>
						</div>
					</div>
					<asp:Label ID="lblTrack" runat="server"></asp:Label> - <asp:Label ID="lblDriver" runat="server"></asp:Label> 
					
					<span><asp:Label runat="server" ID="lblDate" style="margin-left: 50px;"><span id="date"></span></asp:Label></span>
				</div>
				<asp:ListView runat="server" ID="lvAllPrint">
					<LayoutTemplate>
							<table runat="server" class="mapTable" style="width: 29.7cm">
							<tr style="background-color: #EECFBA;">
								<th style="width: 0.5cm;">
									#
								</th>
								<th style="width: 1cm;">
									ID/UID
								</th>
								<th style="width: 6cm;">
									Наименование
								</th>
								<th style="width: 0.5cm;">
									НН
								</th>
								<th style="width: 0.5cm;">
									К-во
								</th>
								<th style="width: 1.5cm;">
									Оцен./Согл. + за доставку
								</th>
								<th style="width: 3.5cm;">
									Город
								</th>
								<th style="width: 5cm">
									Полный адрес
								</th>
								<th style="width: 3cm;">
									Телефон
								</th>
								<th style="width: 3cm;">
									Примечание
								</th>
								<th style="width: 6.2cm;">
									Для заметок
								</th>
							</tr>
							<tr runat="server" id="itemPlaceholder"></tr>
						</table>
					</LayoutTemplate>
					<ItemTemplate>
						<tr id="Tr2" runat="server" 
							style="text-align: center;" 
							class='<%# OtherMethods.UrgentTicketForMapDistinguish(Convert.ToInt32(Eval("ID").ToString())) %>'>
							<td id="Td10" runat="server">
								<asp:Label ID="Label6" runat="server" Text='<%# Eval("PNumber") %>' />
							</td>
							<td id="Td2" runat="server" style="font-size: 8pt; font-weight: bold !important;">
								<asp:Label ID="Label3" runat="server" Text='<%# Eval("SecureID") + "/" + Eval("UserID") %>' />
							</td>
				
							<td id="Td13" runat="server">
								 <asp:Label ID="Label1" runat="server" Text='<%# OtherMethods.GoodsStringFromTicketID(Eval("ID").ToString()) %>' />
							</td>
							
							<td id="Td6" runat="server">
								 <asp:Label ID="Label11" runat="server" Text='<%# OtherMethods.NaklInMap(Eval("ID").ToString()) %>' />
							</td>

							<td id="Td5" runat="server" style="font-size: 8pt;">
								<asp:Label ID="Label7" runat="server" Text='<%#Eval("BoxesNumber") %>' />
							</td>
				
							<td id="Td8" runat="server" style="font-size: 8pt; font-weight: bold !important;">
								<asp:Label ID="Label2" runat="server" CssClass="big" Text='<%# MoneyMethods.MoneySeparator(MoneyMethods.AgreedAssessedDeliveryCosts(Eval("ID").ToString())) %>' />
							</td>

							<td id="Td9" runat="server">
                                <asp:Label ID="Label14" CssClass="big bigfont" runat="server" Text='<%# CityHelper.CityIDToCityName(Eval("CityID").ToString()) %>' />
								<asp:Label ID="Label4" runat="server" Text='<%# CityHelper.CityIDToFullDistrictName(Eval("CityID").ToString()) %>' />
								<asp:Label ID="Label10" runat="server" Text='<%# CityHelper.CityToTrackWithBrackets(Convert.ToInt32(Eval("CityID")), Eval("ID").ToString()) %>' />
							</td>
				
							<td id="Td1" runat="server"  style="font-size: 9pt;">
								<asp:Label ID="Label21" CssClass="big" runat="server" Text='<%# Eval("RecipientStreetPrefix") %>' />&nbsp;
								<asp:Label ID="lblStatusID" CssClass="big" runat="server" Text='<%# Eval("RecipientStreet") %>' />&nbsp;
								<asp:Label ID="Label12" CssClass="big" runat="server" Text='<%# OtherMethods.GetRecipientAddressWithoutStreet(Eval("ID").ToString()) %>' />
							</td>
				
							<td id="Td12" runat="server" style="font-size: 10pt; font-weight: bold !important;">
								<asp:Label ID="Label8" runat="server" Text='<%#  (Eval("RecipientPhone")).ToString().Replace("+375", "") %>' /><br/>
								<asp:Label ID="Label9" runat="server" Text='<%#  (Eval("RecipientPhoneTwo")).ToString().Replace("+375", "") %>' />
							</td>
							
							<td id="Td3" runat="server" style="width: 3cm;">
								 <asp:Label ID="Label5" runat="server" Text='<%# Eval("Note") %>' />
							</td>
							<td id="Td7" runat="server" style="text-align: left; font-weight: bold !important; padding-left: 10px; font-size: 16px; color: #666">
								 <asp:Label ID="Label13" runat="server" Text='<%#  OtherMethods.UrgentTicketForMap(Convert.ToInt32(Eval("ID").ToString())) %>' />
							</td>
						</tr>
					</ItemTemplate>
				</asp:ListView><br/>
				<span style="font-size: 14px;">Грузы принял в количестве <%= (Iterator-1).ToString() %>шт., претензий по упаковке грузов не имею  ____________</span>
				<br/><br/><br/><br/>
				<table  style="width: 100%">
					<tr>
						<td style="width: 33%">
							<table style="font-size: 14px;">
								<tr>
									<td style="height: 30px;">
										Общий пробег (км)
									</td>
									<td>
										____________________
									</td>
								</tr>
					
								<tr>
									<td style="height: 30px;">
										Расход топлива
									</td>
									<td>
										____________________
									</td>
								</tr>
							</table>
						</td>
						
						<td style="width: 33%">
							<table style="font-size: 14px;">
								<tr>
									<td style="height: 30px;">
										Сумма за телефон
									</td>
									<td>
										____________________
									</td>
								</tr>
					
								<tr>
									<td style="height: 30px;">
										Сумма за ночевку
									</td>
									<td>
										____________________
									</td>
								</tr>
							</table>
						</td>
						
						<td style="width: 33%">
							<table style="font-size: 14px;">
								<tr>
									<td style="height: 30px;">
										Точек посетил (штук)
									</td>
									<td>
										____________________
									</td>
								</tr>
					
								<tr>
									<td style="height: 30px;">
										Привез (онлайн)
									</td>
									<td>
										____________________
									</td>
								</tr>
							</table>
						</td>
					</tr>
				</table>
			</div>

			<div style="width:29.7cm;" class="footer">
				<input type="button" value="Назад" onclick="window.history.go(-1); return false;" class="btn" style="width: 14%; margin-right: 1%" />
				<input type="button" value="Печать" onclick="PrintElem('#thisPrint')" class="btn" style="width: 84%" />
			</div>
		</div>
	</body>
</html>
