<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PrintCheck.aspx.cs" Inherits="Delivery.PrintServices.PrintCheck"%>
<%@ Import Namespace="System.Security.Policy" %>
<%@ Import Namespace="System.Web.Optimization" %>
<%@ Import Namespace="Delivery.BLL" %> <%@ Import Namespace="Delivery.BLL.StaticMethods" %>
<%@ Import Namespace="Delivery.BLL.Helpers" %>

<!DOCTYPE html>

<html>
	<head>
		<title>Печать чеков <%= BackendHelper.TagToValue("page_title_part") %></title>
		<link rel="icon" type="image/png" href="<%=ResolveClientUrl("~/Styles/Images/favicon.png") %>" />
		<%: Scripts.Render("~/js/jquery") %>
		<link rel="stylesheet" href="<%=ResolveClientUrl("~/Styles/print.css") %>">
		<script type="text/javascript">
			function PrintElem(elem) {
				Popup($(elem).html());
			}

			function Popup(data) {
				var mywindow = window.open('', 'Чеки', 'width="7.8cm"');
				mywindow.document.write(data);
				mywindow.print();
				mywindow.close();
				return true;
			}
		</script>
		<script type="text/javascript">
			$(function () {
				if ($('#<%= lblError.ClientID%>').html() == "") {
				$('#errorDiv').hide();
			} else {
				$('#errorDiv').show();
			}
		});
	</script>
	</head>
	<body>
		<div >
			<div class="loginError" id="errorDiv" style="width: 90%;">
				<asp:Label runat="server" ID="lblError" ForeColor="White"></asp:Label>
			</div>  
			<div style="width:7.8cm;" class="header">
				Страница печати <b>чеков</b><br/> Все, что следует за кнопкой "Печать" будет распечатано.
				<hr class="styleHR"/>
				<input type="button" value="Назад" onclick='<%= String.Format("window.location.replace(\"http://{0}/ManagerUI/UserTicketNotProcessedView.aspx\"); return false;",BackendHelper.TagToValue("current_admin_app_address"))%>' style="width: 24%; margin-right: 1%;" class="btn"/>
				<input type="button" value="Печать" onclick="PrintElem('#thisPrint')" class="btn" style="width: 73%;" />
			</div>		
			<div id="thisPrint" class="this-print" style="width:7.8cm;">
				<div style="margin-top:10px;">================================</div>
				<asp:ListView runat="server" ID="lvAllPrint">
					<LayoutTemplate>
							<div runat="server" id="itemPlaceholder"></div>
					</LayoutTemplate>
					<ItemTemplate>
						<div style="font-family: sans-serif; width:7.4cm; margin-bottom: 10px; margin-top: 10px;">
							ID: <b><asp:Label ID="lblSecureID" runat="server" Text='<%#Eval("SecureID") %>' /></b> / Дата: <b><asp:Label runat="server" Text='<%# DateTime.Now.ToString("dd.MM.yyyy") %>' ID="lblDateTimeNow" /></b> <br/><br/>
						    UID: <b><asp:Label ID="Label4" runat="server" Text='<%# Eval("UserID").ToString() %>' /></b> <br/>
							Город: <b><asp:Label ID="Label1" runat="server" Text='<%# CityHelper.CityIDToCityName(Eval("CityID").ToString()) %>' /></b> <br/>
							Груз: <b><asp:Label ID="Label2" runat="server"  Text='<%# OtherMethods.GoodsStringFromTicketID(Eval("ID").ToString()) %>' /></b> <br/>
							Сумма: <b><asp:Label ID="Label3" runat="server" Text='<%# MoneyMethods.OveralCostForCheck(Eval("AssessedCost").ToString(), Eval("DeliveryCost").ToString()) %>' /></b> руб.<br/>
							Подпись _______________
							<div style="margin-top:10px; font-weight: 10px !important; font-family: Time New Roman;">================================</div>
						</div>
					</ItemTemplate>
				</asp:ListView>
				
				
				
			</div>
			<div style="width:7.8cm;" class="footer">
				<input type="button" value="Назад" onclick='<%= String.Format("window.location.replace(\"http://{1}/{0}\"); return false;",BackLink,BackendHelper.TagToValue("current_admin_app_address"))%>' style="width: 24%;  margin-right: 1%;" class="btn"/>
				<input type="button" value="Печать" onclick="PrintElem('#thisPrint')" style="width: 73%;" class="btn"/>
			</div>
		</div>
	</body>
</html>
