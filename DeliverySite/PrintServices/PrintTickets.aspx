<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PrintTickets.aspx.cs" Inherits="Delivery.PrintServices.PrintTickets "%>
<%@ Import Namespace="System.Web.Optimization" %>
<%@ Import Namespace="Delivery.BLL" %> <%@ Import Namespace="Delivery.BLL.StaticMethods" %>
<%@ Import Namespace="Delivery.BLL.Helpers" %>

<!DOCTYPE html>

<html>
	<head>
		<title>Печать заявок <%= BackendHelper.TagToValue("page_title_part") %></title>
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
		<div style="width:26.1cm;" class="header">
			Страница печати <b>заявок</b>. Все, что следует за кнопкой "Печать" будет распечатано.
			<hr class="styleHR"/>
			<input type="button" value="Назад" onclick="window.history.go(-1); return false;" class="btn" style="width: 14%; margin-right: 1%" />
			<input type="button" value="Печать" onclick="PrintElem('#thisPrint')" class="btn" style="width: 84%;"/>
		</div>
		
			<div id="thisPrint" style="width: 26.1cm" class="this-print">
				<style>
					.mapTable td, .mapTable th{
						border: 1px solid #000;
						padding: 2px;
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
						<div style="width:26.0cm !important;  overflow: hidden;">
							<div runat="server" id="itemPlaceholder">
							</div>
						</div>
							
					</LayoutTemplate>
					<ItemTemplate>
						<div style="height: 19cm !important; font-size: 16px; font-family: sans-serif; width: 45%; display: inline-block;  overflow: hidden; padding: 5px; border: 1px solid black; display: inline-block" runat="server">
							
							<h3 class="h3custom">Общая информация</h3>
							<div style="margin-left: 30px;">
								<i>ID:</i> <b><asp:Label runat="server" ID="lblID" Text='<%#Eval("SecureID")%>'></asp:Label></b><br/>
								<i>Водитель:</i> <b><asp:Label runat="server" ID="Label18" Text='<%# DriversHelper.DriverIDToFioToPrint(Eval("DriverID").ToString())%>'></asp:Label></b><br/>
								<i>Пользователь №:</i> <b><asp:Label runat="server" ID="Label27" Text='<%# Eval("UserID").ToString()%>'></asp:Label></b><br/><br />
							</div>
							<h3 class="h3custom">Подробная информация</h3>
							<div style="margin-left: 30px;">
								<i>Пользователь:</i> <b><asp:Label runat="server" ID="Label28" Text='<%# UsersHelper.UserIDToFullName(Eval("UserID").ToString())%>'></asp:Label></b><br/>
								<i>Профиль:</i> <b><asp:Label runat="server" ID="Label3" Text='<%# UsersProfilesHelper.UserProfileIDToFullFamilyOrCompanyname(Eval("UserProfileID").ToString())%>'></asp:Label></b><br/>
								<i>Город:</i> <b><asp:Label runat="server" ID="Label4" Text='<%# CityHelper.CityIDToCityName(Eval("CityID").ToString()) %>'></asp:Label></b><br/>
								<i>Направление:</i> <b><asp:Label runat="server" ID="Label5" Text='<%# CityHelper.CityToTrack(Convert.ToInt32(Eval("CityID")), Eval("ID").ToString()) %>'></asp:Label></b><br/>
								<i>Груз:</i> <b><asp:Label runat="server" ID="Label6" Text='<%# OtherMethods.GoodsStringFromTicketID(Eval("ID").ToString()) %>'></asp:Label></b><br/>
								
							   
								<i>ФИО получателя:</i> <b>  <asp:Label ID="Label10" runat="server" Text='<%# (Eval("RecipientFirstName").ToString()) %>' /> &nbsp;
															<asp:Label ID="Label17" runat="server" Text='<%# (Eval("RecipientLastName").ToString()) %>' /> &nbsp;
															<asp:Label ID="Label16" runat="server" Text='<%# (Eval("RecipientThirdName").ToString()) %>' /></b><br/>
								<i>Адрес получателя:</i> <b><asp:Label ID="lblStatusID" runat="server" Text='<%# String.Format("{1} {0}",Eval("RecipientStreet"),Eval("RecipientStreetPrefix")) %>' /> <asp:Label ID="Label11" runat="server" Text='<%# OtherMethods.GetRecipientAddressWithoutStreet(Eval("ID").ToString()) %>' /></b><br/>
								<i>Телефоны получателя:</i> <b><asp:Label runat="server" ID="Label12" Text='<%# Eval("RecipientPhone") %>'></asp:Label>&nbsp;
															   <asp:Label runat="server" ID="Label19" Text='<%# Eval("RecipientPhoneTwo") %>'></asp:Label>
															</b><br/>
								<i>Примечания:</i> <b><asp:Label runat="server" ID="Label13" Text='<%# Eval("Note") %>'></asp:Label></b><br/>
								<i>Дата приема:</i> <b><asp:Label runat="server" ID="Label20" Text='<%# OtherMethods.DateConvert(Eval("AdmissionDate").ToString()) %>'></asp:Label></b><br/>
								<i>Дата отправки:</i> <b><asp:Label runat="server" ID="Label14" Text='<%# OtherMethods.DateConvert(Eval("DeliveryDate").ToString()) %>'></asp:Label></b><br/>  
								
								<i>Оценочная/согласованая стоимость:</i> <b><asp:Label runat="server" ID="Label7" Text='<%# MoneyHelper.ToRussianString(Convert.ToDouble(MoneyMethods.AgreedAssessedCosts(Eval("ID").ToString())), true) %>'></asp:Label></b><br/>
								<i>Общая стоимость:</i> <b><asp:Label runat="server" ID="Label25" Text='<%# MoneyHelper.ToRussianString(Convert.ToDouble(MoneyMethods.AgreedAssessedDeliveryCosts(Eval("ID").ToString())), true) %>'></asp:Label></b><br/>
								<i>За услугу:</i> <b><asp:Label runat="server" ID="Label26" Text='<%# MoneyHelper.ToRussianString(Convert.ToDouble(Eval("GruzobozCost").ToString()), true) %>'></asp:Label></b><br/>
								<i>Итого:</i> <b><asp:Label runat="server" ID="Label1" Text='<%# MoneyHelper.ToRussianString(Convert.ToDouble(MoneyMethods.AgreedAssessedDeliveryCosts(Eval("ID").ToString())) - Convert.ToDouble(Eval("GruzobozCost").ToString()), true) %>'></asp:Label></b><br/><br/>
							</div>
						</div>
					</ItemTemplate>
				</asp:ListView>
			</div>

			<div style="width:26.1cm;" class="footer">
				<input type="button" value="Назад" onclick="window.history.go(-1); return false;" class="btn" style="width: 14%; margin-right: 1%" />
				<input type="button" value="Печать" onclick="PrintElem('#thisPrint')" class="btn" style="width: 84%" />
			</div>
	</body>
</html>
