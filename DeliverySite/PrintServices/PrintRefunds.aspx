<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PrintRefunds.aspx.cs" Inherits="Delivery.PrintServices.PrintRefunds "%>
<%@ Import Namespace="System.Web.Optimization" %>
<%@ Import Namespace="Delivery.BLL" %> <%@ Import Namespace="Delivery.BLL.StaticMethods" %>
<%@ Import Namespace="Delivery.BLL.Helpers" %>

<!DOCTYPE html>

<html>
	<head>
		<title>Печать информации оп возвратам <%= BackendHelper.TagToValue("page_title_part") %></title>
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
			}

			function Popup(data) {
				var mywindow = window.open('', 'Возвраты', 'width="7.8cm"');
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
				Страница печати <b>информации по возвратам</b>. Все, что следует за кнопкой "Печать" будет распечатано
				<hr class="styleHR"/>
				<input type="button" value="Назад" onclick="window.history.go(-1); return false;" class="btn" style="width: 14%; margin-right: 1%" />
				<input type="button" value="Печать" onclick="PrintElem('#thisPrint')" class="btn" style="width: 84%;"/>
			</div>	
				
			<div id="thisPrint" style="width: 29.7cm" class="this-print">
				<style>
					body {
						font-family: sans-serif;
						font-size: 10px;
					}
					 .mapTable td, .mapTable th{
						 border: 1px solid #000;
						 padding: 5px 10px;
						 font-size: 10px;
					 }
					.mapTable{
						border-collapse: collapse;
					}
					.big {
						font-weight: bold;
					}

					.grayRow {
						background-color: #cccccc;
					}
				</style>
				<asp:ListView runat="server" ID="lvAllPrint">
					<LayoutTemplate>
							<table runat="server" class="mapTable" style="width: 29.7cm">
							<tr style="background-color: #EECFBA;">
								<th style="width: 2cm;">
									ID
								</th>
								<th>
									Грузы
								</th>
								<th style="width: 3cm;">
									Принята
								</th>
								<th style="width: 3cm;">
									Возвращена
								</th>
								<th style="width: 3cm;">
									Завершена
								</th>
								<th style="width: 3cm;">
									оц.\сог. стоим.
								</th>
							</tr>
							<tr runat="server" id="itemPlaceholder"></tr>
						</table>
					</LayoutTemplate>
					<ItemTemplate>
						<tr id="Tr2" runat="server" 
							style="text-align: center;" 
							class='<%# TicketsHelper.IfCompletedTicketToPrintRefunds(Eval("CompletedDate").ToString()) %>'>
							<td id="Td2" runat="server" style="font-size: 8pt; font-weight: bold !important;">
								<asp:Label ID="Label3" runat="server" Text='<%# Eval("SecureID") %>' />
							</td>
				
							<td id="Td13" runat="server">
								 <asp:Label ID="Label1" runat="server" Text='<%# OtherMethods.GoodsStringFromTicketID(Eval("ID").ToString()) %>' />
							</td>
							
							<td id="Td6" runat="server">
								 <%# Eval("AdmissionDate") %>
							</td>

							<td id="Td5" runat="server">
								<%# Eval("ReturnDate") %>
							</td>
				
							<td id="Td4" runat="server">
								<%# Eval("CompletedDate") %>
							</td>

							<td id="Td8" runat="server" style="font-size: 8pt; font-weight: bold !important;">
								<asp:Label ID="Label2" runat="server" CssClass="big" Text='<%# MoneyMethods.MoneySeparator(MoneyMethods.AgreedAssessedCosts(Eval("ID").ToString())) %>' />
							</td>
						</tr>
					</ItemTemplate>
				</asp:ListView>
			</div>

			<div style="width:29.7cm;" class="footer">
				<input type="button" value="Назад" onclick="window.history.go(-1); return false;" class="btn" style="width: 14%; margin-right: 1%" />
				<input type="button" value="Печать" onclick="PrintElem('#thisPrint')" class="btn" style="width: 84%" />
			</div>
		</div>
	</body>
</html>
