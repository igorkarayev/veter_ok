<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PrintTicketsSmall.aspx.cs" Inherits="Delivery.PrintServices.PrintTicketsSmall "%>
<%@ Import Namespace="System.Web.Optimization" %>
<%@ Import Namespace="Delivery.BLL" %> <%@ Import Namespace="Delivery.BLL.StaticMethods" %>
<%@ Import Namespace="Delivery.BLL.Helpers" %>

<!DOCTYPE html>

<html>
	<head>
		<title>Печать заявок (малый формат) <%= BackendHelper.TagToValue("page_title_part") %></title>
		<link rel="icon" type="image/png" href="<%=ResolveClientUrl("~/Styles/Images/favicon.png") %>" />
		<%: Scripts.Render("~/js/jquery") %>
		<link rel="stylesheet" href="<%=ResolveClientUrl("~/Styles/print.css") %>">
		<script type="text/javascript">
			function PrintElem(elem) {
				Popup($(elem).html());
			}

			function Popup(data) {
				var mywindow = window.open('', 'Карта', 'width="21.0cm"');
				mywindow.document.write(data);
				mywindow.print();
				mywindow.close();
				return true;
			}
		</script>
	</head>
	<body>
		<div style="width:21cm;" class="header">
			Страница печати заявок (малый формат). Все, что следует за кнопкой "Печать" будет распечатано.
			<hr class="styleHR"/>
			<input type="button" value="Назад" onclick="window.history.go(-1); return false;" class="btn" style="width: 14%; margin-right: 1%" />
			<input type="button" value="Печать" onclick="PrintElem('#thisPrint')" class="btn" style="width: 84%;"/>
		</div>
		
			<div id="thisPrint" style="width: 21cm" class="this-print">
				<style>
					body {
						font-family: sans-serif;
						font-size: 12px;
					}
					 .mapTable td, .mapTable th{
						 border: 1px solid black;
						 padding: 2px;
						 font-size: 12px;
					 }
					.mapTable{
						border-collapse: collapse;
					}
					.big {
						font-weight: bold;
					}
				</style>
				<asp:ListView runat="server" ID="lvAllPrint">
					<LayoutTemplate>
						<table style="width:21cm !important;" class="mapTable">
							<tr style="background-color: #EECFBA;">
								<th>
									ID
								</th>
								<th>
									Направление
								</th>
								<th>
									Отметка
								</th>
							</tr>
							<tr runat="server" id="itemPlaceholder">
							</tr>
						</table>
							
					</LayoutTemplate>
					<ItemTemplate>
						<tr id="Tr2" runat="server" style="background-color: #F7F8F9; text-align: center;">
							<td id="Td4" runat="server">
								<asp:Label ID="lblSecureID" runat="server" Text='<%#Eval("ID") %>' />
							</td>

							<td id="Td3" runat="server">
								 <asp:Label ID="Label3" runat="server" Text='<%# CityHelper.CityToTrack(Convert.ToInt32(Eval("CityID")), Eval("ID").ToString()) %>' />
							</td>
				
							<td id="Td13" runat="server" style="width: 60%">
								 &nbsp;
							</td>
						</tr>
					</ItemTemplate>
				</asp:ListView>
			</div>

			<div style="width:21cm;" class="footer">
				<input type="button" value="Назад" onclick="window.history.go(-1); return false;" class="btn" style="width: 14%; margin-right: 1%" />
				<input type="button" value="Печать" onclick="PrintElem('#thisPrint')" class="btn" style="width: 84%" />
			</div>
	</body>
</html>
