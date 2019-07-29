<%@ Page Title="" Language="C#" MasterPageFile="~/ManagerMasterPage.master" AutoEventWireup="true" CodeBehind="CarsView.aspx.cs" Inherits="Delivery.ManagerUI.Menu.Souls.CarsView" %>
<%@ Import Namespace="Delivery.BLL.Helpers" %>
<%@ Import Namespace="Delivery.BLL.StaticMethods" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
	<script type="text/javascript">
		$(function () {
			if ($('#<%= lblError.ClientID%>').html() == "") {
				$('#errorDiv').hide();
			} else {
				$('#errorDiv').show();
			}
		});
   </script>

	<div>
		<asp:HyperLink ID="HyperLink1" runat="server" CssClass="createItemHyperLink" NavigateUrl="~/ManagerUI/Menu/Souls/CarEdit.aspx">Добавить автомобиль</asp:HyperLink>
		<h3 class="h3custom" style="margin-top: 0;">Список автомобилей</h3>
	</div>

	<asp:Panel runat="server" ID="SearchPanel" CssClass="mainFont" DefaultButton="btnSearch">
		<table class="table searchPanel" style="margin-left:auto; margin-right: auto;">
			<tr>
				<td>
					AID: <asp:TextBox runat="server" ID="stbAID" CssClass="searchField form-control" style="width: 25px;"></asp:TextBox>
					Модель: <asp:TextBox runat="server" ID="stbModel" CssClass="searchField form-control" style="width: 90px;"></asp:TextBox> 
					Номер: <asp:TextBox runat="server" ID="stbNumber" CssClass="searchField form-control" style="width: 70px;"></asp:TextBox> 
					Тип владельца: <asp:DropDownList runat="server" ID="sddlType" CssClass="searchField ddl-control" Width="140px"></asp:DropDownList>
					<asp:Button runat="server" Text="Сброс" CssClass="btn btn-default" ID="btnReload"/>
					<asp:Button runat="server" Text="Искать" CssClass="btn btn-default" ID="btnSearch"/>  
					&nbsp;&nbsp;&nbsp;<i class="small-informer">Найдено:<b> <asp:Label runat="server" ID="lblAllResult"></asp:Label></b></i> 
				</td>
			</tr>
		</table>
	</asp:Panel><br/>    

	<div class="loginError" style="width: 90%; margin-left: auto; margin-right: auto; margin-top: 0px;" id="errorDiv">
		<asp:Label ID="lblError" runat="server" />
	</div>

	<div>
		<asp:ListView runat="server" ID="lvAllCars">
		<LayoutTemplate>
			<table runat="server" id="Table1" class="table tableViewClass tableClass">
				<tr style="background-color: #EECFBA">
					<th>
						АID
					</th>
					<th>
						Модель
					</th>
					<th>
						Номер
					</th>
					<th>
						Тип владельца
					</th>
					<th>
					</th>
				</tr>
				<tr runat="server" id="itemPlaceholder"></tr>
			</table>
		</LayoutTemplate>
		<ItemTemplate>
			<tr id="Tr2" runat="server" style="background-color: #F7F8F9; text-align: center;">
				<td id="Td7" runat="server">
					<asp:Label ID="lblID" runat="server" Text='<%#Eval("ID") %>' />
				</td>

				<td id="Td2" runat="server" style="text-align: left;">
					<asp:HyperLink runat="server" ID="hldriver" NavigateUrl='<%#"~/ManagerUI/Menu/Souls/CarView.aspx?id="+Eval("ID") + "&"+ CarsHelper.BackCarLinkBuilder(stbAID.Text, stbModel.Text,
							stbNumber.Text, sddlType.SelectedValue) %>'>
						<asp:Label ID="lblName" runat="server" Text='<%#Eval("Model") %>' />
					</asp:HyperLink>
				</td>
				
				<td id="Td1" runat="server">
					<asp:Label ID="lblRegion" runat="server" Text='<%#Eval("Number") %>' />
				</td>
				
				<td id="Td5" runat="server">
					<asp:Label ID="lblCost" runat="server" Text='<%#CarsHelper.CarTypeToString(Eval("TypeID").ToString()) %>' />
				</td>
				
				<td id="Td3" runat="server"  style="font-size: 12px;">
					 <asp:HyperLink ID="LinkButton1" runat="server" NavigateUrl='<%#"~/ManagerUI/Menu/Souls/CarEdit.aspx?id="+Eval("ID") + "&"+ CarsHelper.BackCarLinkBuilder(stbAID.Text, 
							stbModel.Text, stbNumber.Text, sddlType.SelectedValue) %>'>Изменить</asp:HyperLink><br/>
					 <asp:LinkButton ID="lbDelete" runat="server" OnClick="lbDelete_Click" OnClientClick="return confirmDelete();" CommandArgument='<%#Eval("ID") %>'>Удалить</asp:LinkButton>
				</td>
			</tr>
		</ItemTemplate>
		<AlternatingItemTemplate>
			<tr id="Tr2" runat="server" style="background-color: #EBEEF2; text-align: center;">
				<td id="Td7" runat="server">
					<asp:Label ID="lblID" runat="server" Text='<%#Eval("ID") %>' />
				</td>

				<td id="Td2" runat="server" style="text-align: left;">
					<asp:HyperLink runat="server" ID="hldriver" NavigateUrl='<%#"~/ManagerUI/Menu/Souls/CarView.aspx?id="+Eval("ID") + "&"+ CarsHelper.BackCarLinkBuilder(stbAID.Text, stbModel.Text,
							stbNumber.Text, sddlType.SelectedValue) %>'>
						<asp:Label ID="lblName" runat="server" Text='<%#Eval("Model") %>' />
					</asp:HyperLink>
				</td>
				
				<td id="Td1" runat="server">
					<asp:Label ID="lblRegion" runat="server" Text='<%#Eval("Number") %>' />
				</td>
				
				<td id="Td5" runat="server">
					<asp:Label ID="lblCost" runat="server" Text='<%#CarsHelper.CarTypeToString(Eval("TypeID").ToString()) %>' />
				</td>
				
				<td id="Td3" runat="server"  style="font-size: 12px;">
					 <asp:HyperLink ID="LinkButton1" runat="server" NavigateUrl='<%#"~/ManagerUI/Menu/Souls/CarEdit.aspx?id="+Eval("ID") + "&"+ CarsHelper.BackCarLinkBuilder(stbAID.Text, stbModel.Text,
							stbNumber.Text, sddlType.SelectedValue) %>'>Изменить</asp:HyperLink><br/>
					 <asp:LinkButton ID="lbDelete" runat="server" OnClick="lbDelete_Click" OnClientClick="return confirmDelete();" CommandArgument='<%#Eval("ID") %>'>Удалить</asp:LinkButton>
				</td>
			</tr>
		</AlternatingItemTemplate>
	</asp:ListView>
		<div class="infoBlock" style="margin-bottom: 0;">
			<div class="pager">
				<asp:Label ID="lblPage" runat="server">Страница:</asp:Label>
				<asp:DataPager ID="lvDataPager" runat="server" PagedControlID="lvAllCars" PageSize="25">
					<Fields>
						<asp:NumericPagerField ButtonType="Link" />
					</Fields>
				</asp:DataPager>  
			</div>
		</div>
	</div>
</asp:Content>
