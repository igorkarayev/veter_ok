﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PrintVinilOnPaper.aspx.cs" Inherits="Delivery.PrintServices.PrintVinilOnPaper"%>
<%@ Import Namespace="System.Web.Optimization" %>
<%@ Import Namespace="Delivery.BLL" %> <%@ Import Namespace="Delivery.BLL.StaticMethods" %>
<%@ Import Namespace="Delivery.BLL.Helpers" %>

<!DOCTYPE html>

<html>
	<head>
		<title>Печать наклеек на А4 <%= BackendHelper.TagToValue("page_title_part") %></title>

		<%: Scripts.Render("~/js/jquery") %>
		<link rel="stylesheet" href="<%=ResolveClientUrl("~/Styles/print.css") %>">
		<script type="text/javascript">
			function PrintElem(elem) {
				Popup($(elem).html());
			}
			function Popup(data) {
				var mywindow = window.open('', 'Наклейки', 'width="100%"');
				mywindow.document.write(data);
				mywindow.print();
				mywindow.close();
				return true;
			}
		</script>
	</head>
	<body>
		<div >
			<div class="loginError" id="errorDiv" style="width: 90%;">
				<asp:Label runat="server" ID="lblError" ForeColor="White"></asp:Label>
			</div>  
			<div style="width:21cm;" class="header">
				Страница печати <b>наклеек на А4</b><br/> Все, что следует за кнопкой "Печать" будет распечатано.
				<hr class="styleHR"/>
				<input type="button" value="Назад" onclick="window.history.go(-1); return false;" style="width: 24%; margin-right: 1%;" class="btn"/>
				<input type="button" value="Печать" onclick="PrintElem('#thisPrint')" class="btn" style="width: 73%;" />
			</div>		
			<div id="thisPrint" class="this-print" style="width:21cm;">
				
				<asp:ListView runat="server" ID="lvAllPrint">
					<LayoutTemplate>
							<div runat="server" id="itemPlaceholder"></div>
					</LayoutTemplate>
					<ItemTemplate>
					   
						<div style="text-align: center; font-family: sans-serif; font-size: 23px; width:21cm; padding-bottom: 20px; padding-top: 20px; border-bottom: 2px dotted black; overflow: hidden">
							<div style="width:20%; float: left; vertical-align: middle;">
								<span style="font-size: 20px !important;">ID: <b><asp:Label ID="lblSecureID" runat="server" Text='<%#Eval("SecureID") %>' /></b></span>
							</div>
							<div style="width:75%; float: left; text-align: left; border-left: 3px double black; padding-left: 5px;">
								<span style="font-size: 20px !important;">
									напр. <asp:Label ID="Label4" runat="server" Text='<%# CityHelper.CityToTrack(Convert.ToInt32(Eval("CityID")), Eval("ID").ToString()) %>' />, 
								</span>
								<span style="font-size: 18px !important; font-weight: bolder; line-height: 16px;">
									<u><asp:Label ID="Label1" runat="server" Text='<%# CityHelper.CityIDToCityName(Eval("CityID").ToString()) %>' /></u>, 
								</span>
								<b style="font-size: 20px !important;">
									<asp:Label ID="Label2" runat="server" Text='<%# String.Format("{1} {0}",Eval("RecipientStreet"), Eval("RecipientStreetPrefix")) %>' />, 
								</b>
								<span style="font-size: 20px !important;">
									коробок: <b><asp:Label ID="Label3" runat="server" Text='<%# OtherMethods.BoxesLabel(Eval("BoxesItem").ToString(), Eval("BoxesNumber").ToString()) %>' /></b>
								</span>
							</div>
							
							
						</div>	
					</ItemTemplate>
				</asp:ListView>
				
				
				
			</div>
			<div style="width:21cm;" class="footer">
				<input type="button" value="Назад" onclick="window.history.go(-1); return false;" style="width: 24%;  margin-right: 1%;" class="btn"/>
				<input type="button" value="Печать" onclick="PrintElem('#thisPrint')" style="width: 73%;" class="btn"/>
			</div>
		</div>
	</body>
</html>
