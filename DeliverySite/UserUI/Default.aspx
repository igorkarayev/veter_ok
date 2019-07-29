<%@ Page Title="" Language="C#" MasterPageFile="~/UserMasterPage.master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="Delivery.UserUI.Default" %>
<%@ Import Namespace="Delivery.Resources" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div style="display: inline-block; width: 49%; padding-left: 50px;">
        <div class="customer-info">
            <div class="line greating">
                Вы вошли как <i style="font-weight: bold"><asp:Label runat="server" ID="lblUserName"></asp:Label>!</i>
            </div>
            <div class="line">
                № кабинета:
                <span class="cabinet-id">
                    <asp:Label runat="server" ID="lblUID"></asp:Label>
                </span>
            </div>
            <div class="line">
                Логин:
                <span style="font-weight: bold; font-style: italic;">
                    <asp:HyperLink runat="server" ID="hlAccountEdit3" NavigateUrl="~/UserUI/AccountEdit.aspx">
                        <asp:Label runat="server" ID="lblLogin"></asp:Label>
                    </asp:HyperLink>
                </span>
            </div>
            <div class="line">
                E-mail:
                <span style="font-weight: bold; font-style: italic;">
                    <asp:HyperLink runat="server" ID="hlAccountEdit4" NavigateUrl="~/UserUI/AccountEdit.aspx">
                        <asp:Label runat="server" ID="lblEmail"></asp:Label>
                    </asp:HyperLink>
                </span>
            </div>
            <div runat="server" id="tdDiscount" class="line">
                Ваша скидка:
                <span style="font-weight: bold; font-style: italic;">
                    <asp:Label runat="server" ForeColor="Green" ID="lblDiscount"></asp:Label>
                </span>
            </div>
            <div runat="server" id="trApiKey" Visible="False" style="margin-top: 10px;" class="line">
                API Key:
                <asp:Label runat="server" ID="lblApiKey" style="font-weight: bold; margin-left: 10px;"></asp:Label>
            </div>            
            <div runat="server" id="trLogistian" Visible="False" style="margin-top: 10px;" class="line">
                Ваш менеджер:
                <asp:Label runat="server" ID="lblLogistian" style="font-weight: bold; margin-left: 10px;"></asp:Label>
            </div>
            <div runat="server" id="trManager" Visible="False" style="margin-top: 10px;" class="line">
                Ваш логист:
                <asp:Label runat="server" ID="lblManager" style="font-weight: bold; margin-left: 10px;"></asp:Label>
            </div>
        </div>
            
    </div>
                
    <div class="client-info">
        <div style="font-size: 14px; font-weight: bold; padding-bottom: 10px;">Информация для отправителей</div>
        <div style="padding-left: 15px; padding-bottom: 10px;">
            <i>1.</i> Скачайте <asp:HyperLink ID="HyperLink2" runat="server"  NavigateUrl="~/OtherFiles/veter-ok_manual_for_client.pdf">инструкцию для отправителей</asp:HyperLink>;<br/>
            <i>2.</i> Ознакомьтесь с <asp:HyperLink ID="HyperLink1" runat="server"  NavigateUrl="~/UserUI/FAQ.aspx">разделом "Вопрос/Ответ"</asp:HyperLink>;<br/>
            <i>3.</i> Ознакомьтесь с <asp:HyperLink ID="HyperLink3" runat="server"  NavigateUrl="~/UserUI/Developer.aspx">инструкцией для разработчиков</asp:HyperLink>.<br/>
                        
        </div>
        Для изменения регистрационных данных кликните на свой аватар или номер телефона.
    </div>
</asp:Content>
