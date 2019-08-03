<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PrintActORT.aspx.cs" Inherits="Delivery.PrintServices.PrintActORT "%>
<%@ Import Namespace="System.Web.Optimization" %>
<%@ Import Namespace="Delivery.BLL" %> <%@ Import Namespace="Delivery.BLL.StaticMethods" %>
<%@ Import Namespace="Delivery.BLL.Helpers" %>

<!DOCTYPE html>

<html>
	<head>
		<title>Печать акта приема-передачи</title>
		<link rel="icon" type="image/png" href="<%=ResolveClientUrl("~/Styles/Images/favicon.png") %>" />
		<%: Scripts.Render("~/js/jquery") %>
		<link rel="stylesheet" href="<%=ResolveClientUrl("~/Styles/print.css") %>">
		<script type="text/javascript">
			            
			function PrintElem(elem) {
                var mywindow = window.open('', 'Акт приема-передачи', 'width="7.8cm"');
                mywindow.document.write($(elem).html());
                mywindow.print();
                mywindow.close();
			}
		</script>
		
	</head>
	<body>
		<div>
			<div style="width: 29.7cm; position: relative;" class="header">
				Страница печати <b>акта приема-передачи</b>. Все, что следует за кнопкой "Печать" будет распечатано.				
				<hr class="styleHR"/>
				<input type="button" value="Назад" onclick="window.history.go(-1); return false;" class="btn" style="width: 14%; margin-right: 1%" />
				<input type="button" value="Печать" onclick="PrintElem('#thisPrint')" class="btn" style="width: 84%" />
			</div>	
				
			<div id="thisPrint" style="width: 29.7cm" class="this-print">
				<style>
					body {
						font-family: sans-serif;
						font-size: 6pt;
					}
					 .totalCost td{
						 border: 1px solid #000;
						 padding: 0.2pt;
						 font-size: 6pt;
					 }
                     .totalCost th{
						 border: 1px solid #000;
						 padding: 0.2pt;
						 font-size: 8pt;
                         font-weight: bold;
					 }
					.totalCost{
						border-collapse: collapse;                        
					}
					.big {
						font-weight: bold;
					}
				</style>
                <h4 style="margin: 1cm 0cm 0.5cm 1.3cm; font-size: 16pt;">
                    Акт передачи груза к перевозке от №<asp:Label runat="server" ID="LabelID"></asp:Label> кабинета в <%= BackendHelper.TagToValue("official_name") %>
                </h4>
				<div style="width: 30%; padding: 5px; display: inline-block; vertical-align: top;">
					Общие номера для справки: <b><%= BackendHelper.TagToValue("main_phones") %></b>
				</div>
				<asp:ListView runat="server" ID="lvAllPrint">
					<LayoutTemplate>
							<table runat="server" class="totalCost" style="width: 29.7cm;">
							<tr style="background-color: #EECFBA;">
								<th style="width: 0.7cm;">
									
								</th>
								<th style="width: 0.7cm;">
									За услугу
								</th>
                                <th style="width: 1.5cm;">
									ID
								</th>
                                <th style="width: 3cm;">
									Отправитель
								</th>
                                <th style="width: 4cm;">
									Наименование
								</th>
                                <th style="width: 1.2cm;">
									К-во
								</th>
                                <th style="width: 1.6cm;">
									Оцен./Согл. + за доставку
								</th>
                                <th style="width: 1.2cm;">
									Стоимость доставки  
								</th>
                                <th style="width: 1.5cm;">
									Стоимость товара
								</th>
                                <th style="width: 4.5cm;">
									Город
								</th>
                                <th>
									Примечание
								</th>
							</tr>
							<tr runat="server" id="itemPlaceholder"></tr>
						</table>
					</LayoutTemplate>
					<ItemTemplate>                        
                        <tr id="Tr2" runat="server" 
						style="text-align: center;" 
						class='<%# OtherMethods.UrgentTicketForMapDistinguish(Convert.ToInt32(Eval("ID").ToString())) %>'>
							<td id="Td1" runat="server">
								<asp:Label ID="Label1" runat="server" Text='<%# Eval("PNumber") %>' />
							</td>

                            <td id="Td2" runat="server" style="font-size: 8pt;">
								<asp:Label ID="Label2" runat="server" style="padding: 0 5px;" Text='<%# MoneyMethods.MoneySeparator(Eval("GruzobozCost").ToString()) %>' />
							</td>

                            <td id="Td3" runat="server" style="font-size: 8pt;">
								<asp:Label ID="Label3" runat="server" Text='<%# Eval("SecureID") %>' />
							</td>

                            <td id="Td4" runat="server" style="font-size: 8pt;">
								<asp:Label ID="Label4" runat="server" Text='<%# OtherMethods.GetProfileData(Eval("UserProfileID").ToString()) %>' /><br/>
								<asp:Label ID="Label13" runat="server" Text='<%# OtherMethods.GetProfileContactPhone(Eval("UserProfileID").ToString()).Replace(";","<br/>") %>' />
							</td>

                            <td id="Td5" runat="server" style="font-size: 8pt;">
								<asp:Label ID="Label5" runat="server" Text='<%# Eval("Goods") %>' />
							</td>

							<td id="Td6" runat="server" style="font-size: 8pt;">
								<asp:Label ID="Label6" runat="server" Text='<%# Eval("BoxesNumber") %>' />
							</td>

                            <td id="Td7" runat="server" style="font-size: 8pt;">
								<asp:Label ID="Label7" runat="server" CssClass="big" Text='<%# MoneyMethods.MoneySeparator(MoneyMethods.AgreedAssessedDeliveryCosts(Eval("ID").ToString())) %>' />
							</td>

                            <td id="Td8" runat="server" style="font-size: 8pt;">
								<asp:Label ID="Label8" runat="server" style="padding: 0 5px;" Text='<%# MoneyMethods.MoneySeparator(Eval("DeliveryCost").ToString()) %>' />
							</td>

                            <td id="Td9" runat="server" style="font-size: 8pt;">
								<asp:Label ID="Label9" runat="server" Text='<%# MoneyMethods.MoneySeparator(Eval("AssessedCost").ToString()) %>' />
							</td>

                            <td id="Td10" runat="server" style="font-size: 8pt;">
								<asp:Label ID="Label10" runat="server" Text='<%# CityHelper.CityIDToCityNameForMap(Eval("CityID").ToString()) %>' />
								<asp:Label ID="Label12" runat="server" Text='<%# CityHelper.CityToTrackWithBrackets(Convert.ToInt32(Eval("CityID")), Eval("ID").ToString()) %>' />
							</td>

                            <td id="Td11" runat="server" style="font-size: 8pt;">
								<asp:Label ID="Label11" runat="server" Text='<%# Eval("Note") %>' />
							</td>
						</tr>                       
						
					</ItemTemplate>
				</asp:ListView>
                <div>
                    Общая сумма за услугу: <asp:Label runat="server" ID="LabelSum"></asp:Label> руб.
                </div>
                <div style="margin-left: 1cm; font-size: 9pt; display: inline-block;">
                    <p>ОТГРУЗИЛ:___________________</p>
                    <p>(отдел доставки _______________)</p>
                </div>                
                <div style="display: inline-block;  margin-left: 3cm; font-size: 9pt;">
                    <p>ПРИНЯЛ К ПЕРЕВОЗКЕ:___________________</p>
                    <p>(водитель ТК Грундекс)</p>                   
                </div>
                <div style="display: inline-block; margin-left: 3cm; font-size: 9pt;"> 
                    <p>ПРОВЕРИЛ:___________________</p>
                    <p>(кассир _______________)</p>
                </div>
				
			</div>

			<div style="width:29.7cm;" class="footer">
				<input type="button" value="Назад" onclick="window.history.go(-1); return false;" class="btn" style="width: 14%; margin-right: 1%" />
				<input type="button" value="Печать" onclick="PrintElem('#thisPrint')" class="btn" style="width: 84%" />
			</div>
		</div>
	</body>
</html>
