<%@ Page Title="" Language="C#" MasterPageFile="~/ManagerMasterPage.master" AutoEventWireup="true" Async="true" CodeBehind="ClientEdit.aspx.cs" Inherits="Delivery.ManagerUI.Menu.Souls.ClientEdit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
	<script type="text/javascript">
		$(function () {
			$.datepicker.setDefaults($.datepicker.regional['ru']);
			$("#<%= tbPhone.ClientID %>").mask("+375 (99) 999-99-99");

		});

		$(function () {
			if ($.trim($("#<%= lblStatus.ClientID %>").html()) != "") {
				$('.lblStatus').show();
				setTimeout(function () { $('.lblStatus').hide(); }, 10000);
			}
		});
   </script>
	<div class="floatMenuLeft lblStatus" style="margin-left: 0">
	  <asp:Label runat="server" ID="lblStatus"></asp:Label>
	</div>

	<h3 class="h3custom" style="margin-top: 0;">Редактирование клиента</h3>
	<div class="single-input-fild" style="width: 700px; margin-bottom: 0px;">
		<table class="table">
			<tr>
				<td style="width: 220px; padding-right: 50px; vertical-align: top;">
					<div class="form-group">
						<span  style="font-style: italic">UID:</span> 
						<asp:Label ID="lblID" runat="server" style="font-weight: bold"/>
					</div>

					<div class="form-group">
						<span  style="font-style: italic">Email:</span> 
						<asp:Label ID="lblEmail" runat="server" style="font-weight: bold"/>
					</div>

					<div class="form-group">
						<span  style="font-style: italic">Логин:</span>  
						<asp:Label ID="lblLogin" runat="server" />
					</div>
					
					<div class="form-group">
						<span  style="font-style: italic">Дата регистрации:</span>  
						<asp:Label ID="lblRegistartionDate" runat="server"/>
					</div>

					<div class="form-group">
						<span  style="font-style: italic">Статус:</span> 
						<asp:Label ID="lblStatusClient" runat="server"/>
					</div>
					
					<div class="form-group">
						<span  style="font-style: italic">Доступ к API:</span> 
						<asp:Label ID="lblApi" runat="server"/>
					</div>
	
					<div class="form-group">
						<asp:HyperLink runat="server" ID="hlSales" CssClass="btn btn-default link-btn" style="width: 200px;"/>
					</div>

					<div class="form-group" runat="server" ID="divChangePassword">
						<asp:HyperLink runat="server" ID="hlChangePassword" CssClass="btn btn-default link-btn" Text="Cменить пароль" style="width: 200px;"/>
					</div>

					<div class="form-group">
						<asp:HyperLink ID="hlAddSection" runat="server" CssClass="btn btn-default link-btn" Text='Категории клиента' style="width: 200px;"/>                 
					</div>
					
					<asp:Button ID="btnSendPrice" runat="server" OnClientClick="return confirmDelete();" Text='Выслать прайс' CssClass="btn btn-default" style="width: 225px; margin-bottom: 15px;"/>
					<asp:Button ID="btnActivation" Visible="False" runat="server" CssClass="btn btn-default " Text='Активировать клиента' style="width: 225px; margin-bottom: 15px; background-color: #8DC6A0; border: 1px solid #0BA53E"/>
					<asp:Button ID="btnBlock" Visible="False" runat="server" CssClass="btn btn-default " Text='Заблокировать клиента' style="width: 225px; margin-bottom: 15px; background-color: #D38F87; border: 1px solid #CC493B"/>
					<asp:Button ID="btnAllowApi" Visible="False" runat="server" CssClass="btn btn-default " Text='Открыть доступ к API' style="width: 225px; margin-bottom: 15px; background-color: #8DC6A0; border: 1px solid #0BA53E"/>
					<asp:Button ID="btnDisallowApi" Visible="False" runat="server" CssClass="btn btn-default " Text='Закрыть доступ к API' style="width: 225px; margin-bottom: 15px; background-color: #D38F87; border: 1px solid #CC493B"/>	
					<asp:Button runat="server" ID="btnDeleteClient" OnClientClick="return confirmDelete();" CssClass="btn btn-default link-btn" Text="Удалить клиента" style="width: 225px; margin-bottom: 15px; background-color: #D38F87; border: 1px solid #CC493B"/>
					
					<div class="form-group">
						<span  style="font-style: italic">Комментарии:</span> 
						<div style="background-color: #eeeeee; font-size: 12px; padding: 5px 10px; border-radius: 5px; border: 1px solid #ccc;">
							<asp:Label ID="lblNote" runat="server"/>
						</div>
					</div>
				</td>



				<td style="vertical-align: top">
					
					<div class="form-group">
						<div style="display: inline-block; width: 64%; vertical-align:top;">
							<div class="form-group">
								<div style="display: inline-block; width: 80px;">
									Имя: 
								</div>
								<asp:TextBox ID="tbName" runat="server" CssClass="form-control" style="width: 135px"/>
							</div>
	
							<div class="form-group">
								<div style="display: inline-block; width: 80px;">
									Фамилия: 
								</div>
								<asp:TextBox ID="tbFamily" runat="server" CssClass="form-control" style="width: 135px"/>
							</div>
	
							<div class="form-group">
								<div style="display: inline-block; width: 80px;">
									Телефон: 
								</div>
								<asp:TextBox ID="tbPhone" runat="server" CssClass="form-control" style="width: 135px;"/>
							</div>
						</div>
						<div style="display: inline-block; width: 35%; text-align: right">
							<asp:Image runat="server" ID="imgGravatar" style="width: 120px;"/>
						</div>
					</div>

					

					<div class="form-group">
						Статус проработки:
						<asp:DropDownList runat="server" CssClass="ddl-control" ID="ddlStatusStady" style="width: 50%"/>
					</div>
					
					<div class="form-group">
						Дата сл. контакта:
						<asp:TextBox runat="server" CssClass="form-control" ID="tbContactDate" style="width: 65px"/>
					</div>
		
					<div class="form-group">
						Особый клиент 
						<asp:CheckBox runat="server" ID="cbIsSpecialClient"/>
					</div>
					
					<div class="form-group">
						"Красный" клиент 
						<asp:CheckBox runat="server" ID="cbIsRedClient"/>
					</div>
					
					<div class="form-group">
						<label>Ответственный менеджер</label>
						<asp:DropDownList ID="ddlManager" runat="server" width="100%" CssClass="searchField ddl-control"/>
					</div>
					
					<div class="form-group">
						<label>Ответственный менеджер по продажам</label>
						<asp:DropDownList ID="ddlSalesManager" runat="server" width="100%" CssClass="searchField ddl-control"/>
					</div>
					<div class="form-group" style="margin-top: 20px;">
						<asp:Button ID="btnCreate" runat="server" CssClass="btn btn-default btn-right" Text='Сохранить данные' style="margin-left: 30px;"/>
						<asp:Button ID="btnBack" OnClientClick="window.history.go(-1);return false;"  CssClass="btn btn-default btn-right"  runat="server" Text='Назад' style="margin-left: 30px;"/> 
						
						
					</div>
				</td>
			</tr>
		</table>
	</div>
	<div runat="server" id="trApiOpenNotif" Visible="False" style="color: darkred; font-style: italic; font-size: 11px; text-align: right;">
		Функция закрытия доступа клиента к API работает только при соответствующем значении в back-end теге "allow_unauth_api_request" 
	</div>
	
<script type="text/javascript">
	$(function () {
		$("#<%# tbContactDate.ClientID%>").datepicker({ nextText: "", prevText: "", changeMonth: true, changeYear: true }).mask("99-99-9999");
	});
</script>
</asp:Content>
