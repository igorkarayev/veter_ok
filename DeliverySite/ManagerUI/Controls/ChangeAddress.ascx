<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ChangeAddress.ascx.cs" Inherits="DeliverySite.ManagerUI.Controls.ChangeAddress" %>
<%@ Import Namespace="Delivery.AppServices" %>
<%@ Import Namespace="Delivery.BLL" %> <%@ Import Namespace="Delivery.BLL.StaticMethods" %>
<script>
    var showEdit = function (id) {
        var elem = $('#edit_' + id);
        if ($(elem).css('display') == 'none')
            $(elem).show();
        else
            $(elem).hide();
        return false;
    }
</script>
<div style='width: 100%; height: 100%; text-align: right;'>
    <asp:HiddenField ID="_ticketID" runat="server"/>
    <asp:Button ID="btnEdit" runat="server" OnClientClick='<%# "return showEdit(" + TicketID + ");" %>' Text="Изменить"/>
    <div ID='edit_<%# TicketID %>' style="display: none">
        <table>
		    <tr>
			    <td>
				    <asp:DropDownList ID="ddlSenderStreetPrefix" Width="50px" runat="server" CssClass="ddl-control" style="padding: 0; height: 28px;">
					    <asp:ListItem text="ул." value="ул."/>
					    <asp:ListItem text="аллея" value="аллея"/>
					    <asp:ListItem text="бул." value="бул."/>
					    <asp:ListItem text="дор." value="дор."/>
					    <asp:ListItem text="линия" value="линия"/>
					    <asp:ListItem text="маг." value="маг."/>
					    <asp:ListItem text="мик-н" value="мик-н"/>
					    <asp:ListItem text="наб." value="наб."/>
					    <asp:ListItem text="пер." value="пер."/>
					    <asp:ListItem text="пл." value="пл."/>
					    <asp:ListItem text="пр." value="пр."/>
					    <asp:ListItem text="пр-кт" value="пр-кт"/>
					    <asp:ListItem text="ряд" value="ряд"/>
					    <asp:ListItem text="тракт" value="тракт"/>
					    <asp:ListItem text="туп." value="туп."/>
					    <asp:ListItem text="ш." value="ш."/>
				    </asp:DropDownList>
			    </td>
			    <td colspan="2">
				    <asp:TextBox ID="tbSenderStreetName" runat="server" width="130px" CssClass="form-control"/>
			    </td>
            </tr>
            <tr>
                <td>
				    <span>дом: </span><asp:TextBox ID="tbSenderStreetNumber" runat="server" width="25px" CssClass="form-control"/>
			    </td>
			    <td>
                    <span>корпус:</span> <asp:TextBox ID="tbSenderHousing" runat="server" width="15px" CssClass="form-control"/>
                </td>
			    <td>
				    <span>квартира: </span><asp:TextBox ID="tbSenderApartmentNumber" runat="server" width="25px" CssClass="form-control"/>
			    </td>
		    </tr>
	    </table>
        <asp:Button ID="btnSave" runat="server" Text="ok" OnClick="btnSaveClick" />
    </div>
</div>