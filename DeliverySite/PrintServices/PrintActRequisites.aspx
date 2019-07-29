<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PrintActRequisites.aspx.cs" Inherits="Delivery.PrintServices.PrintActRequisites "%>
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
				
                $('#dateAct').html($('#InputDateAct').val());
                $('#numberAct').html($('#InputNumberAct').val());
               
				$('#inputdata').show();

				$('#Form1').hide();				

                Popup($(elem).html());

                
			}

			function Popup(data) {
				var mywindow = window.open('', 'Акт выполненных услуг', 'width="7.8cm"');
				mywindow.document.write(data);
				mywindow.print();
				mywindow.close();

                var reportbody = '<!DOCTYPE html><html><head><meta charset="utf-8"><title>Заказ-поручение</title></head><body >' + data + '</body></html>';
                $.ajax({
                    type: "POST",
                    url: "../AppServices/ReportService.asmx/SaveReport",
                    data: ({
                        body: '<style>.big {font-weight: bold;}.printZP {border-collapse: collapse;}.printZP td{border: 1px solid black !important;padding: 2px;height: 15px;font-size: 8px;}</style>' + reportbody,
                        reporttype: "5",
                        driverid: "<%= UserID %>",
                        drivername: "<%= UserName %>",
                        appkey: "<%= AppKey %>",
                        documentdate: document.getElementById('dateAct').innerHTML
                    })
                });


				return true;
			}
		</script>
		
	</head>
	<body>
		<div>
			<div style="width: 29.7cm; position: relative;" class="header">
				Страница печати <b>акта выполненных работ</b>. Все, что следует за кнопкой "Печать" будет распечатано.
				<hr class="styleHR"/>
				<form id="Form1" runat="server">
					Акт выполненных услуг : <asp:TextBox runat="server" ID="InputNumberAct" style="width: 80px;" CssClass="input"></asp:TextBox> &nbsp;&nbsp;&nbsp;
					от : <asp:TextBox runat="server" ID="InputDateAct" style="width: 80px;" CssClass="input"></asp:TextBox>					
				</form>
				<span id="inputdata" onclick="InputData()" style="color: red; cursor: pointer; display: none;">Ввести данны повторно</span>
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
				<div style="font-size: 10pt; margin-top: 2cm; margin-left: 10cm; margin-bottom: 1cm; font-weight: bold;">
                    Акт выполненных услуг №<span runat="server" ID="numberAct">_</span> от <asp:label runat="server" ID="dateAct"></asp:label> <br />
                    к договору № <asp:label runat="server" ID="numberContract"></asp:label> от <asp:label runat="server" ID="dateContract"></asp:label>
				</div>
                <div style="display: inline-block;width: 10cm;font-size: 8pt; margin-left: 1cm; position: absolute;">
                    <b>Исполнитель и его адрес:</b> <br />
                    <asp:label runat="server" ID="nameExecuter"></asp:label> <asp:label runat="server" ID="infoExecuter"></asp:label> <br />
                    УНП <asp:label runat="server" ID="UNPExecuter"></asp:label>
                </div>
                <div style="display: inline-block; font-size: 8pt; margin-left: 14cm;">
                    <b>Заказчик и его адрес</b> <br />
                    <asp:label runat="server" ID="customerCompanyName"></asp:label> Республика Беларусь <br />
                    <asp:label runat="server" ID="customerCompanyAddress"></asp:label> <br />
                    р/с <asp:label runat="server" ID="customerRasShet"></asp:label> <br />
                    в <asp:label runat="server" ID="customerBankName"></asp:label> , <asp:label runat="server" ID="customerBankAddress"></asp:label><br />
                    тел/факс <asp:label runat="server" ID="customerContactPhoneNumbers"></asp:label> <br />
                    УНП <asp:label runat="server" ID="customerUNP"></asp:label>
                </div>
				<asp:ListView runat="server" ID="lvAllPrint">
					<LayoutTemplate>
							<table runat="server" class="totalCost" style="width: 28.7cm; margin-top: 0.5cm; margin-left: 1cm;">
							<tr style="background-color: #EECFBA;">
								<th style="width: 1cm;">
									№
								</th>
								<th style="width: 6cm;">
									Наименование работ, услуг
								</th>
								<th style="width: 3.5cm;">
									Дата
								</th>
								<th style="width: 3cm;">
									№ накладной
								</th>
								<th style="width: 3cm;">
									Дата доставки
								</th>
								<th style="width: 3cm;">
									№ заявки
								</th>
								<th style="width: 5cm;">
									Пункт выгрузки
								</th>
								<th style="">
									Цена услуг, руб
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

                            <td id="Td14" runat="server" style="font-size: 8pt;">
								<asp:Label ID="Label14" runat="server" Text='Доставка груза' />
							</td>

                            <td id="Td4" runat="server" style="font-size: 8pt;">
								<asp:Label ID="Label21" runat="server" Text='<%# Eval("Date") %>' />
							</td>

                            <td id="Td11" runat="server" style="font-size: 8pt;">
								<asp:Label ID="Label16" runat="server" Text='<%#Eval("Ttn")%>' />
							</td>

                            <td id="Td15" runat="server" style="font-size: 8pt;">
								<asp:Label ID="Label17" runat="server" Text='<%#Eval("SendDate")%>' />
							</td>

							<td id="Td2" runat="server" style="font-size: 8pt;">
								<asp:Label ID="Label3" runat="server" Text='<%# Eval("SecureID") %>' />
							</td>

                            <td id="Td16" runat="server" style="font-size: 8pt;">
								<asp:Label ID="Label18" runat="server" Text='<%# Eval("Sender") %>' />
							</td>

                            <td id="Td17" runat="server" style="font-size: 8pt;">
								<asp:Label ID="Label19" runat="server" Text='<%# Eval("GruzobozCost") %>' />
							</td>
						</tr>
                        
						
					</ItemTemplate>
				</asp:ListView>

                <table class="totalCost" style="border-top: unset; margin-left: 1cm; width: 28.7cm;">
                    <tr>
                        <td id="Td1" runat="server" style="font-size: 12pt; font-weight: bold !important; border-top: none; width: 24.64cm;">
                            Стоимость без НДС, руб.
                        </td>
                        <td id="Td2" runat="server" style="font-size: 12pt; font-weight: bold !important; border-top: none; text-align: center;">
                            <asp:Label ID="LabelSum1" runat="server"/>
                        </td>
                    </tr>
                    <tr>
                        <td id="Td3" runat="server" style="font-size: 12pt; font-weight: bold !important;">
                            НДС
                        </td>
                        <td id="Td4" runat="server" style="font-size: 12pt; text-align: center;">
                            без НДС
                        </td>
                    </tr>
                    <tr>
                        <td id="Td5" runat="server" style="font-size: 12pt; font-weight: bold !important;">
                            Всего с НДС, руб.
                        </td>
                        <td id="Td6" runat="server" style="font-size: 12pt; font-weight: bold !important; text-align: center;">
                            <asp:Label ID="LabelSum2" runat="server"/>
                        </td>
                    </tr>
                </table>

                <div style="margin-left: 1cm; font-size: 8pt; margin-bottom: 1cm;">
                    <span>Исполнитель оказал Заказчику услуги надлежащего качества своевременно и в полном  объеме. Стороны взаимных претензий не имеют.</span>
                </div>                
                <div style="display: inline-block;  margin-left: 1.5cm; font-size: 9pt;">
                    <b>Исполнитель:</b>                    
                </div>
                <div style="display: inline-block; margin-left: 13cm; font-size: 9pt;"> 
                    <b>Заказчик:</b>
                </div>
				
			</div>

			<div style="width:29.7cm;" class="footer">
				<input type="button" value="Назад" onclick="window.history.go(-1); return false;" class="btn" style="width: 14%; margin-right: 1%" />
				<input type="button" value="Печать" onclick="PrintElem('#thisPrint')" class="btn" style="width: 84%" />
			</div>
		</div>
	</body>
</html>
