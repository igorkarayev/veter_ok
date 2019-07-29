<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PrintMapForCashier.aspx.cs" Inherits="Delivery.PrintServices.PrintMapForCashier "%>
<%@ Import Namespace="System.Web.Optimization" %>
<%@ Import Namespace="Delivery.BLL" %> <%@ Import Namespace="Delivery.BLL.StaticMethods" %>
<%@ Import Namespace="Delivery.BLL.Helpers" %>

<!DOCTYPE html>

<html>
	<head>
		<title>Печать карты менеджерам <%= BackendHelper.TagToValue("page_title_part") %></title>
		<link rel="icon" type="image/png" href="<%=ResolveClientUrl("~/Styles/Images/favicon.png") %>" />
		<%: Scripts.Render("~/js/jquery") %>
		<link rel="stylesheet" href="<%=ResolveClientUrl("~/Styles/print.css") %>">
		<script type="text/javascript">
			function PrintElem(elem) {
				Popup($(elem).html());
			}

			function Popup(data) {
				var mywindow = window.open('', 'Карта', 'width="7.8cm"');
				mywindow.document.write(data);
				mywindow.print();
				mywindow.close();
				return true;
			}
		</script>
		
	</head>
	<body>
		<div>
			<div style="width: 29.7cm;" class="header">
				Страница печати <b>карты для кассирорв</b>. Все, что следует за кнопкой "Печать" будет распечатано.<hr class="styleHR"/>
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
						 padding: 0px;
						 font-size: 6pt;
					 }
					.mapTable{
						border-collapse: collapse;
					}
					.big {
						font-weight: bold;
					}
				</style>
				<div style="font-size: 16px; font-weight: bold">
					<div style="font-size: 10px; margin-left: 0px; font-weight: normal;">
						<div style="width: 28%; padding: 5px; display: inline-block; vertical-align: top;">
							Направления: <asp:Label ID="lblTrack2" runat="server" style="font-weight: bold; font-size: 12px;"></asp:Label><br/>
							Оператора <asp:Label ID="lblOperatorName" runat="server"></asp:Label>: <asp:Label ID="lblOperatorPhone" runat="server" style="font-weight: bold; font-size: 12px;"></asp:Label>
						</div>
						<div style="width: 30%; padding: 5px; display: inline-block; vertical-align: top;">
							Общие номера для справки: <b><%= BackendHelper.TagToValue("main_phones") %></b>
						</div>
					</div>
					<asp:Label ID="lblTrack" runat="server"></asp:Label> - <asp:Label ID="lblDriver" runat="server"></asp:Label> (<asp:Label ID="lblDriverPhone" runat="server"></asp:Label> )
					
					<span><asp:Label runat="server" ID="lblDate" style="margin-left: 50px;"></asp:Label></span>
				</div>
				<asp:ListView runat="server" ID="lvAllPrint">
					<LayoutTemplate>
							<table runat="server" class="mapTable" style="width: 100%">
							<tr style="background-color: #EECFBA;">
								<th>
									#
								</th>
								<th>
									За услугу
								</th>
								<th>
									ID
								</th>
								<th style="width: 2.5cm">
									Отправитель
								</th>
								<th>
									Наименование
								</th>
								<th style="width: 1cm">
									НН
								</th>
								<th style="width: 1cm">
									К-во
								</th>
								<th style="width: 50px;">
									Оцен./Согл. + за доставку
								</th>
								<th style="width: 60px;">
									Курс
								</th>
								<th style="width: 70px;">
									Город
								</th>
								<th style="width: 110px;">
									Адрес
								</th>
								 <th>
									Дом
								</th>
								<th style="width: 100px;">
									Контакт
								</th>
								<th style="width: 150px;">
									Примечание
								</th>
							</tr>
							<tr runat="server" id="itemPlaceholder"></tr>
						</table>
					</LayoutTemplate>
					<ItemTemplate>
						<tr id="Tr2" runat="server" style="background-color: #F7F8F9; text-align: center;">
							<td id="Td10" runat="server">
								<asp:Label ID="Label6" runat="server" style="padding: 0 5px;" Text='<%# Eval("PNumber") %>' />
							</td>
							<td id="Td11" runat="server">
								<asp:Label ID="Label15" runat="server" style="padding: 0 5px;" Text='<%# MoneyMethods.MoneySeparator(Eval("GruzobozCost").ToString()) %>' />
							</td>
							<td id="Td4" runat="server">
								<asp:Label ID="lblSecureID" runat="server" Text='<%#Eval("SecureID") %>' />
							</td>
							<td id="Td7" runat="server">
								 <asp:Label ID="Label9" runat="server" Text='<%# OtherMethods.GetProfileData(Eval("UserProfileID").ToString()) %>' /><br/>
								<asp:Label ID="Label13" runat="server" Text='<%# OtherMethods.GetProfileContactPhone(Eval("UserProfileID").ToString()).Replace(";","<br/>") %>' />
							</td>
							<td id="Td13" runat="server">
								 <asp:Label ID="Label1" runat="server" Text='<%# OtherMethods.GoodsStringFromTicketID(Eval("ID").ToString()) %>' />
							</td>
							
							<td id="Td6" runat="server">
								 <asp:Label ID="Label11" runat="server" Text='<%# OtherMethods.NaklInMap(Eval("ID").ToString()) %>' />
							</td>

							<td id="Td5" runat="server">
								<asp:Label ID="Label7" runat="server" Text='<%#Eval("BoxesNumber") %>' />
							</td>
				
							<td id="Td8" runat="server">
								<asp:Label ID="Label2" runat="server" CssClass="big" Text='<%# MoneyMethods.MoneySeparator(MoneyMethods.AgreedAssessedDeliveryCosts(Eval("ID").ToString())) %>' />
							</td>
								
							<td id="Td2" runat="server">
								<asp:Label ID="Label3" runat="server" Text='<%# OtherMethods.GetRecipientCourses(Eval("ID").ToString()) %>' />
							</td>

							<td id="Td9" runat="server">
								<asp:Label ID="Label4" runat="server" Text='<%# CityHelper.CityIDToCityNameForMap(Eval("CityID").ToString()) %>' />
								<asp:Label ID="Label10" runat="server" Text='<%# CityHelper.CityToTrackWithBrackets(Convert.ToInt32(Eval("CityID")), Eval("ID").ToString()) %>' />
							</td>
				
							<td id="Td1" runat="server">
								<asp:Label ID="Label21" runat="server" Text='<%# Eval("RecipientStreetPrefix") %>' />&nbsp;
								<asp:Label ID="lblStatusID" runat="server" Text='<%# Eval("RecipientStreet") %>' />
							</td>

							<td id="Td0" runat="server">
								<asp:Label ID="Label12" runat="server" Text='<%# OtherMethods.GetRecipientAddressWithoutStreet(Eval("ID").ToString()) %>' />
							</td>
				
							<td id="Td12" runat="server">
								<asp:Label ID="Label8" runat="server" Text='<%#  (Eval("RecipientPhone")).ToString().Replace("+375", "") %>' /><br/>
								<asp:Label ID="Label14" runat="server" Text='<%#  (Eval("RecipientPhoneTwo")).ToString().Replace("+375", "") %>' />
							</td>
							
							<td id="Td3" runat="server">
								 <asp:Label ID="Label5" runat="server" Text='<%# Eval("Note") %>' />
							</td>
						</tr>
					</ItemTemplate>
				</asp:ListView>
				Общая сумма за услугу: <asp:Label runat="server" ID="lblGruzobozCost"></asp:Label> руб. , за доставку: <asp:Label runat="server" ID="lblDeliveryCost"></asp:Label> руб.
			</div>
			
			<div style="width:29.7cm;" class="footer">
				<input type="button" value="Назад" onclick="window.history.go(-1); return false;" class="btn" style="width: 14%; margin-right: 1%" />
				<input type="button" value="Печать" onclick="PrintElem('#thisPrint')" class="btn" style="width: 84%" />
			</div>
		</div>
	</body>
</html>
