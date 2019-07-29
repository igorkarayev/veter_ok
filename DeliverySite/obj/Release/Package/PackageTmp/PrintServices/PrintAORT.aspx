<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PrintAORT.aspx.cs" Inherits="Delivery.PrintServices.PrintAORT "%>
<%@ Import Namespace="System.Web.Optimization" %>
<%@ Import Namespace="Delivery.BLL" %> <%@ Import Namespace="Delivery.BLL.StaticMethods" %>
<%@ Import Namespace="Delivery.BLL.Helpers" %>
<%@Register Tagname="GoodsList" Tagprefix ="grb" src="~/PrintServices/Controls/GoodsListForZP.ascx" %> 
<!DOCTYPE html>

<html>
	<head>
		<title>Печать акта приема-передачи <%= BackendHelper.TagToValue("page_title_part") %></title>
		<link rel="icon" type="image/png" href="<%=ResolveClientUrl("~/Styles/Images/favicon.png") %>" />
		<%: Scripts.Render("~/js/jquery") %>
		<link rel="stylesheet" href="<%=ResolveClientUrl("~/Styles/print.css") %>">
		<style>.notprint{display: none;}</style>
		<script type="text/javascript">
			function InputData() {
				$(".ifexist").show();
				$(".ifexistlabel").hide();
			}

			function PrintElem(elem) {
				$(".ifexist").hide();
				$(".ifexistlabel").show();
				$("#inputdata").show();
				$(".ifexist").each(function() {
					if ($(this).is(":checked")) {
						$(this).next("span").html("&#10004;");
					} else {
						$(this).next("span").html("");
					}
				});
				Popup($(elem).html());
			}

			function Popup(data) {
				var mywindow = window.open('', 'Акт приема-передачи', 'width="7.8cm"');
				mywindow.document.write(data);
				mywindow.print();
				mywindow.close();

				return true;
			}
		</script>
	</head>
	<body>
		<div>
			<div style="width:20.7cm;" class="header">
				Страница печати <b>акта приема-передачи</b>. Все, что следует за кнопкой "Печать" будет распечатано.
				<hr class="styleHR"/>
				<span id="inputdata" onclick="InputData()" style="color: red; cursor: pointer; display: none;">Ввести наличие повторно</span>
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
					table {
						border-collapse: collapse;
					}
					td {
						border: 1px solid #999;
						padding: 3px 6px;
					}
				</style>
				<div id="Tr2" runat="server" style="width:20.7cm; min-height: 29.5cm !important; overflow: hidden !important; font-size: 14px !important; font-family: 'Time New Roman'">
					<div style="padding: 15px;">
						<div style="text-align: center; font-size: 20px; font-weight: bold; margin-top: 10px; margin-bottom: 20px;">
							Акт приема-передачи
						</div>
							
						<div style="width: 49%; display: inline-block">
							г.Минск
						</div>
							
						<div style="width: 49%; display: inline-block; text-align: right;  margin-bottom: 20px;">
							<%= DateTime.Now.ToString("dd.MM.yyyyг.") %>
						</div>
						
						<div>
							Мы, нижеподписавшиеся:  с одной стороны <span style="text-decoration: underline"><%= BackendHelper.TagToValue("official_name") %></span><br/><br/>

							с  другой стороны <span style="text-decoration: underline"><%= CompanyName ?? ProfileFio %></span><br/><br/>
							произвели прием – передачу следующих грузов:<br/><br/>
						</div>
						<table>
							<tr>
								<th style="width: 10%;">
									# заявки
								</th>
								<th style="width: 40%;">
									Наименование груза
								</th>
								<th style="width: 5%;">
									Наличие
								</th>
								<th  style="width: 45%;">
									Комментарий
								</th>
							</tr>
							<asp:ListView runat="server" ID="lvAllPrint">
								<LayoutTemplate>
									<tr runat="server" id="itemPlaceholder">
										
									</tr>
								</LayoutTemplate>
								<ItemTemplate>
									<tr>
										<td style="text-align: center;">
											<%# Eval("TicketFullSecureID").ToString().Remove(7, Eval("TicketFullSecureID").ToString().Length-7) %>
										</td>
										<td>
											<%# Eval("Description") %> <u><%# Eval("Model") %></u> <b style="font-size: 12px;"><%# Eval("Number") %>шт.</b>
										</td>
										<td style="text-align: center;">
											<input type="checkbox" class="ifexist"/>
											<span class="ifexistlabel" style="display: none"></span>
										</td>
										<td>
											
										</td>
									</tr>
								</ItemTemplate>
							</asp:ListView>
						</table><br/><br/>
						
						<div>
							к договору № <span style="border-bottom: 1px solid black; width: 150px; display: inline-block; text-align: center"><%= UserAgrimentNumber %></span> от <span style="border-bottom: 1px solid black; width: 150px; display: inline-block; text-align: center"><%= UserAgrimentDate %></span><br/><br/>
						</div>
						
						<div>
							Стороны претензий друг к другу не имеют.<br/><br/>
						</div>

						<div>
							<div style="width: 49%; display: inline-block">
								Сдал:   
							</div>
							<div style="width: 49%; display: inline-block; text-align: right;">
								___________________________________/__________________
							</div><br/><br/>
						</div>
						
						<div>
							<div style="width: 49%; display: inline-block">
								Принял:   
							</div>
							<div style="width: 49%; display: inline-block; text-align: right;">
								___________________________________/__________________
							</div>
						</div>
					</div>
				</div>
			</div>
			<div style="width:20.7cm;" class="footer">
				<input type="button" value="Назад" onclick="window.history.go(-1); return false;" class="btn" style="width: 14%; margin-right: 1%" />
				<input type="button" value="Печать" onclick="PrintElem('#thisPrint')" class="btn" style="width: 84%" />
			</div>
		</div>
	</body>
</html>
