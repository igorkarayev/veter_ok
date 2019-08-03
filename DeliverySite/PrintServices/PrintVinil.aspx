﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PrintVinil.aspx.cs" Inherits="Delivery.PrintServices.PrintVinil"%>
<%@ Import Namespace="System.Web.Optimization" %>
<%@ Import Namespace="Delivery.BLL" %> <%@ Import Namespace="Delivery.BLL.StaticMethods" %>
<%@ Import Namespace="Delivery.BLL.Helpers" %>

<!DOCTYPE html>

<html>
	<head>
		<title>Печать наклеек <%= BackendHelper.TagToValue("page_title_part") %></title>

		<%: Scripts.Render("~/js/jquery") %>
		<script type="text/javascript">
			function PrintElem(elem) {
				Popup($(elem).html());
			}

			function Popup(data) {
				var mywindow = window.open('', 'Наклейки', 'width="7.8cm"');
				mywindow.document.write(data);
				mywindow.print();
				mywindow.close();

				return true;
			}
		</script>
        <style>
            .together {
                padding-bottom: 10px;
            }

            @media print {
				.together{
					page-break-inside: avoid;
				    margin-left: -27px;
				}
				/* ... the rest of the rules ... */
			}
        </style>
	</head>
	<body>
		<div style="width:7.4cm">	
			<div id="thisPrint" >
				<asp:ListView runat="server" ID="lvAllPrint">
					<LayoutTemplate>
							<div runat="server" id="itemPlaceholder"></div>
					</LayoutTemplate>
					<ItemTemplate>
					   
						<div class="together" style="text-align: center; font-family: sans-serif; font-size: 23px; width:7.4cm;">
							<span style="font-size: 25px !important;">ID: <b><asp:Label ID="lblSecureID" runat="server" Text='<%#Eval("SecureID") %>' /></b></span><br/>
							<span style="font-size: 20px !important;">прн: <b><asp:Label ID="Label6" runat="server" Text='<%#Eval("AdmissionDate") %>' /></b> / отп: <b><asp:Label ID="Label7" runat="server" Text='<%#Eval("DeliveryDate") %>' /></b></span><br/>
							<span style="font-size: 20px !important;">(напр. <asp:Label ID="Label4" runat="server" Text='<%# CityHelper.CityToTrack(Convert.ToInt32(Eval("CityID")), Eval("ID").ToString()) %>' />)</span> <span>&nbsp;</span> <br/>	
							<div style="height: 55px; font-size: 25px !important; font-weight: bolder; line-height: 20px;">
								<u><asp:Label ID="Label1" runat="server" Text='<%# CityHelper.CityIDToCityName(Eval("CityID").ToString()) %>' /></u>
							</div>
							 <b style="font-size: 20px !important;"><asp:Label ID="Label2" runat="server" Text='<%#Eval("FullRecipientStreet") %>' /></b> <span>&nbsp;</span> <br/>	
							<span style="font-size: 20px !important;"> uid: <b><asp:Label ID="Label5" runat="server" Text='<%#Eval("UserID") %>' /></b>&nbsp; КК: <b><asp:Label ID="Label3" runat="server" Text='<%# OtherMethods.BoxesLabel(Eval("BoxesItem").ToString(), Eval("BoxesNumber").ToString()) %>' /></b></span> <span>&nbsp;</span><br/>
						</div>	
					</ItemTemplate>
				</asp:ListView>
				
				
				
			</div>
		</div>
	</body>
</html>
