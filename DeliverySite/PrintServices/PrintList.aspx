<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PrintList.aspx.cs" Inherits="Delivery.PrintServices.PrintList "%>
<%@ Import Namespace="System.Web.Optimization" %>
<%@ Import Namespace="System.Net" %>
<%@ Import Namespace="Delivery.BLL" %> <%@ Import Namespace="Delivery.BLL.StaticMethods" %>
<%@ Import Namespace="Delivery.BLL.Helpers" %>
<!DOCTYPE html>

<html>
    <head>
		<title>Печать расчётного листа <%= BackendHelper.TagToValue("page_title_part") %></title>
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
                var ccc = $('#Inpdate').val();
				$('#date').html($('#Inpdate').val());
				$('#date').show();
				$('#inputdata').show();

				$('#Form1').hide();
				Popup($(elem).html());

				$.ajax({
					type: "POST",
					url: "../AppServices/SaveAjaxService.asmx/SaveMapParams",
					data: ({
						appkey: "<%= AppKey %>"						
					})
				});
			}

			function Popup(data) {
				var mywindow = window.open('', 'Карта', 'width="7.8cm"');
				mywindow.document.write(data);
				mywindow.print();
				mywindow.close();
				
				var reportbody = '<!DOCTYPE html><html><head><meta charset="utf-8"><title>Карта</title></head><body >' + data + '</body></html>';
				$.ajax({
					type: "POST",
					url: "../AppServices/MailService.asmx/SentMap",
					data: ({
						body: '<style>body {font-family: sans-serif;font-size: 6pt;}.mapTable td, .mapTable th{border: 1px solid #000;padding: 0px;font-size: 6pt;}.mapTable{border-collapse: collapse;}.big {font-weight: bold;}</style>' + reportbody,
						drivername: "",
						appkey: "<%= AppKey %>"
					})
                });

                $.ajax({
                    type: "POST",
                    url: "../AppServices/ReportService.asmx/SaveReport",
                    data: ({
                        body: '<style>.big {font-weight: bold;}.printZP {border-collapse: collapse;}.printZP td{border: 1px solid black !important;padding: 2px;height: 15px;font-size: 8px;}</style>' + reportbody,
                        reporttype: "6",
                        driverid: "<%= UserId %>",
                        drivername: "<%= UserName %>",
                        appkey: "<%= AppKey %>",
                        documentdate: document.getElementById('dateAct').innerHTML
                    })
                });
				
				return true;
			}
		</script>
		<style>
            a {
                text-decoration: none;
            }
		</style>
	</head>
	<body>
		<div>
			<div style="width:20.7cm;" class="header">
				Страница печати <b>расчётного листа</b>. Все, что следует за кнопкой "Печать" будет распечатано.
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

					.printZP td {
						border: 1px solid black !important;
						padding: 2px;
						height: 15px;
						font-size: 8px;
					}

                    .notVisible {
                        display: none;
                    }
				</style>
			   <asp:ListView runat="server" ID="lvAllTickets"  DataKeyNames="ID" ClientIDMode="Predictable" 
                ClientIDRowSuffix="ID">
                    <LayoutTemplate>
                        <table runat="server" border="1" id="tableResult" class="table tableViewClass tableClass colorTable">
                            <tr style="background-color: #EECFBA;">
                                <th>
                                    Дата отпр.
                                </th>
                                <th>
                                    Профиль
                                </th>
                                <th>
                                    ID/стат.
                                </th>
                                <th>
                                    Оцен/Согл + за дост.
                                </th>
                                <th>
                                    За услугу
                                </th>
                                <th>
                                    Оцен/Согл + за дост. - за усл.
                                </th>
                                <th>
                                    Примечание
                                </th>
                            </tr>
                            <tr runat="server" id="itemPlaceholder"></tr>
                        </table>
                    </LayoutTemplate>
                    <ItemTemplate>
                        <asp:TableRow ID="Tr2" runat="server" CssClass='<%# OtherMethods.TicketColoredStatusRows(Eval("StatusID").ToString()) %>'>
                            <asp:TableCell id="Td4" runat="server">
                                <asp:Label ID="Label10" runat="server" Text='<%# OtherMethods.DateConvert(Eval("DeliveryDate").ToString()) %>' />
                            </asp:TableCell>

                            <asp:TableCell id="Td3" runat="server">
                                <asp:HyperLink ID="HyperLink2" runat="server" NavigateUrl='<%#"~/ManagerUI/Menu/Souls/ProfileEdit.aspx?id="+Eval("UserProfileID") %>'>
                                    <asp:Label ID="Label5" runat="server" Text='<%# UsersProfilesHelper.UserProfileIDToFullFamilyOrCompanyname(Eval("UserProfileID").ToString()) %>' />
                                </asp:HyperLink>
                            </asp:TableCell>
                
                            <asp:TableCell id="Td13" runat="server">
                                 <asp:HyperLink ID="HyperLink1" runat="server" NavigateUrl='<%#"~/ManagerUI/Menu/Tickets/UserTicketEdit.aspx?id="+Eval("SecureID") + "&page=calculation&" + OtherMethods.LinkBuilder(String.Empty, 
                                String.Empty, String.Empty, String.Empty, String.Empty, String.Empty , String.Empty, String.Empty, String.Empty)%>'>
                                    <asp:Label ID="lblSecureID" runat="server" Text='<%#Eval("SecureID") %>' />
                                </asp:HyperLink><br/>
                                <asp:Label ID="Label3" runat="server" Text='<%# OtherMethods.TicketStatusToText(Eval("StatusID").ToString()) %>' />
                            </asp:TableCell>
                
                            <asp:TableCell id="TableCell1" runat="server">
                                <asp:Label ID="lblAgreedAssessedDeliveryCosts" runat="server"  
                                    Text='<%# MoneyMethods.MoneySeparator(MoneyMethods.AgreedAssessedDeliveryCosts(Eval("ID").ToString())) %>'></asp:Label>
                            </asp:TableCell>

                            <asp:TableCell id="Td5" runat="server">
                               <asp:Label ID="Label1" runat="server"  
                                    Text='<%# MoneyMethods.MoneySeparator(Convert.ToDecimal(Eval("GruzobozCost"))) %>'></asp:Label>
                            </asp:TableCell>

                            <asp:TableCell id="Td8" runat="server">
                                <asp:Label ID="Label2" runat="server"  
                                    Text='<%# MoneyMethods.MoneySeparator(Convert.ToDecimal(MoneyMethods.AgreedAssessedDeliveryCosts(Eval("ID").ToString())) - Convert.ToDecimal(Eval("GruzobozCost"))) %>'></asp:Label>
                            </asp:TableCell>
               
                            <asp:TableCell id="Td1" runat="server">
                                <div class="noteDiv" style="width: 100%; margin-bottom: 10px; font-weight: normal; font-style: normal; text-align: left;">
                                    <asp:Label ID="lblStatusID" runat="server" Text='<%# Eval("Note").ToString() %>' />
                                </div>
                            </asp:TableCell>

                        </asp:TableRow>
                    </ItemTemplate>
        
                    <EmptyDataTemplate>
                        <asp:ListView runat="server" ID="lvAllTickets">  
                            <LayoutTemplate>
                                    <asp:CheckBox ID="chkboxSelectAll" runat="server"/>
                                    <div runat="server" id="itemPlaceholder"></div>
                            </LayoutTemplate>
                        </asp:ListView>
                        <div style="width: 100%; text-align: center; padding: 15px; color: red">По данным критериям ничего не найдено либо не произведен поиск...</div>
                    </EmptyDataTemplate>
                </asp:ListView>

                <i>Всего за услугу:</i> <b style="color: green"><asp:Label runat="server" ID="lblOverGruzobozCost" Text="0"></asp:Label></b> BLR<br/>
                <i>Итого к выдаче по расчетному листу:</i>  <asp:Label runat="server" ID="lblReceivedBLRUser" Text="0"></asp:Label>
			</div>
			<div style="width:20.7cm;" class="footer">
				<input type="button" value="Назад" onclick="window.history.go(-1); return false;" class="btn" style="width: 14%; margin-right: 1%" />
				<input type="button" value="Печать" onclick="PrintElem('#thisPrint')" class="btn" style="width: 84%" />
			</div>
            <asp:label CssClass="notVisible" runat="server" ID="dateAct"></asp:label>
		</div>
	</body>
</html>
