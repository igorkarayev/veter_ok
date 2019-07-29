<%@ Page Title="" Language="C#" MasterPageFile="~/ManagerMasterPage.master" AutoEventWireup="true" CodeBehind="SendComProp.aspx.cs" Async="true" Inherits="Delivery.ManagerUI.Menu.Documents.SendComProp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
	
	<h3 class="h3custom" style="margin-top: 0;">Отправка коммерческого предложения</h3>
	
	<div style="text-align: center;">
		<asp:Label CssClass="loginNotError lblStatus" style="display: none;" runat="server" ID="lblStatus"></asp:Label>
	</div>

	<div style="margin-top: 10px; width: 500px; margin-left: auto; margin-right: auto;" >
		<div class="form-group">
			Е-mail получателя:<br/>
			<asp:TextBox runat="server" ID="tbEmail" CssClass="form-control" style="width: 250px;"/>
		</div>
		<div class="form-group">
			Тема:
			<asp:TextBox runat="server" ID="tbSubject" CssClass="form-control"/>
		</div>
	</div>
	
	<span style="font-size: 18px; margin-left: 20px; font-style: italic">Категории, доступные клиенту</span>
	<hr class="styleHR2" style="margin-top: 5px;"/>
	<div style="color: darkred; font-style: italic; font-size: 11px; text-align: left; margin-top: -15px; margin-left: 20px; ">
		Если не выбрана ни одна категория, то клиенту будут доступны все наименования
	</div>
	<div class="single-input-fild" style="width: 90%; margin-bottom: 30px;">
		<asp:ListView runat="server" ID="lvAllCategory">
			<LayoutTemplate>
				<div runat="server" id="itemPlaceholder"></div>
			</LayoutTemplate>
			<ItemTemplate>
				<div id="Tr2" runat="server" style="width: 32%; display: inline-block">
					<asp:CheckBox runat="server" ID="cbCategory"/>
					<asp:HiddenField ID="hfCategoryId" runat="server" Value='<%# Eval("ID") %>' />
					<asp:HyperLink ID="HyperLink2" runat="server" NavigateUrl='<%#"~/ManagerUI/Menu/Souls/TitlesView.aspx?categoryid=" + Eval("ID") %>' CssClass="alternative">
						<asp:Label ID="lblName" runat="server" Text='<%# Eval("Name") %>' />
					</asp:HyperLink>
				</div>
			</ItemTemplate>
		</asp:ListView>
	</div>
	<hr class="styleHR2" style="margin-top: 5px;"/>
	<div style=" width: 500px; margin-left: auto; margin-right: auto;" >
		<div class="form-group" style="margin-top: 15px;">
			<asp:Button runat="server" ID="btnSendComProp" Text="Отправить"  ValidationGroup="LoginGroup" CssClass="btn btn-default btn-right"/>
		 </div>
	</div>
	<script>
		$(function () {
			if ($.trim($("#<%= lblStatus.ClientID %>").html()) !== "") {
				$('.lblStatus').show();
				setTimeout(function () { $('.lblStatus').hide(); }, 10000);
			}
		});
	</script>
	
	<asp:CustomValidator ID="CustomValidator5" ControlToValidate="tbEmail" runat="server"  ClientValidationFunction="validateIfEmpty" EnableClientScript="True" ValidationGroup="LoginGroup" ValidateEmptyText="True" Display="None" ErrorMessage="Вы не ввели email получателя"></asp:CustomValidator>
	<asp:CustomValidator ID="CustomValidator1" ControlToValidate="tbSubject" runat="server"  ClientValidationFunction="validateIfEmpty" EnableClientScript="True" ValidationGroup="LoginGroup" ValidateEmptyText="True" Display="None" ErrorMessage="Вы не ввели тему сообщения"></asp:CustomValidator>
	<asp:ValidationSummary ID="ValidationSummary1" runat="server" ValidationGroup="LoginGroup" ShowMessageBox="true" ShowSummary="false"/>

</asp:Content>
