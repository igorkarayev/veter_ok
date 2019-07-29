<%@ Page Title="" Language="C#" MasterPageFile="~/UserMasterPage.master" AutoEventWireup="true" CodeBehind="Developer.aspx.cs" Inherits="Delivery.UserUI.Developer" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <p align="center"><b><font size="5">API-методы для заявок</font></b></p>
    <p><b><font size="4">Создание заявок</font></b></p>
    <p><font size="4"><font size="3">Method: POST</font></font></p>
    <p><font size="4"><font size="3">URL:&nbsp;</font></font>https://ветер.бел/WebServices/PublicAPI/TicketAPI.asmx/CreateTickets</p>
    <p><b>Headers</b></p>
    <ol>
	    <li><i>content-type:&nbsp;text/json</i></li>
	    <li><i>userid: на главной странице пользователя(<span style="color: #333333; font-family: &quot;Open Sans&quot;, sans-serif; font-size: 16px; background-color: #eeeeee;">№ кабинета</span>)</i></li>
	    <li><i>apikey: на главной странице пользователя(<font color="#333333" face="Open Sans, sans-serif"><span style="background-color: #eeeeee;">api-key</span></font>)</i></li>
        <li><i>tiсkets:&nbsp;(в JSON формате)</i></li>        
    </ol>

    <p style="padding-top: 20px;">JSON</p>
    <p>[{</p>
              <p  style="padding-left: 30px;">   "ProfileName": "   ООО ''Аквайт''",<b style="padding-left: 20px;">   Название профиля</b></p> 
              <p  style="padding-left: 30px;">   "CityID": "   11",<b style="padding-left: 20px;">   Населенный пункт(ID)</b></p>
              <p  style="padding-left: 30px;">   "StreetPrefix": "ул.",<b style="padding-left: 20px;">   Тип улицы</b></p>
              <p  style="padding-left: 30px;">   "StreetName": "Ленина",<b style="padding-left: 20px;">   Название улицы </b></p>
              <p  style="padding-left: 30px;">   "HouseNumber": "10",<b style="padding-left: 20px;">   Номер дома</b></p>
              <p  style="padding-left: 30px;">   "Korpus": "1",<b style="padding-left: 20px;">   Корпус</b></p>
              <p  style="padding-left: 30px;">   "Kvartira": "3",<b style="padding-left: 20px;">   Квартира</b></p>
              <p  style="padding-left: 30px;">   "SenderCityID": "   11",<b style="padding-left: 20px;">   Населенный пункт отправки(ID)</b></p>
              <p  style="padding-left: 30px;">   "SenderStreetPrefix": "ул.",<b style="padding-left: 20px;">   Тип улицы отправки</b></p>
              <p  style="padding-left: 30px;">   "SenderStreetName": "Ленина",<b style="padding-left: 20px;">   Название улицы отправки</b></p>
              <p  style="padding-left: 30px;">   "SenderHouseNumber": "10",<b style="padding-left: 20px;">   Номер дома отправки</b></p>
              <p  style="padding-left: 30px;">   "SenderKorpus": "1",<b style="padding-left: 20px;">   Корпус отправки</b></p>
              <p  style="padding-left: 30px;">   "SenderKvartira": "3",<b style="padding-left: 20px;">   Квартира отправки</b></p>
              <p  style="padding-left: 30px;">   "FirstName": "Иванов",<b style="padding-left: 20px;">   Имя</b></p>
              <p  style="padding-left: 30px;">   "SecondName": "Иван",<b style="padding-left: 20px;">   Фамилия</b></p>
              <p  style="padding-left: 30px;">   "ThirdName": "Иванович",<b style="padding-left: 20px;">   Отчество</b></p>
              <p  style="padding-left: 30px;">   "FirstTelefonNumber": "+375 (29) 111-34-44",<b style="padding-left: 20px;">   Номер телефона</b></p>
              <p  style="padding-left: 30px;">   "SecondTelefonNumber": "",<b style="padding-left: 20px;">   Дополнительный номер телефона</b></p>
              <p  style="padding-left: 30px;">   "RecieverCost": "20",<b style="padding-left: 20px;">   Сумма доставки за счет получателя</b></p>
              <p  style="padding-left: 30px;">   "BoxCount": "",<b style="padding-left: 20px;">   Количество коробок</b></p>
              <p  style="padding-left: 30px;">   "SendDate": "28.08.2018",<b style="padding-left: 20px;">   Дата отправки</b></p>
              <p  style="padding-left: 30px;">   "Comments": "",<b style="padding-left: 20px;">   Комментарии</b></p>
              <p  style="padding-left: 30px;">   "TTNSeria": "АА",<b style="padding-left: 20px;">   ТТН серия</b></p>
              <p  style="padding-left: 30px;">   "TTNNmber": "1234567",<b style="padding-left: 20px;">   ТТН номер</b></p>
              <p  style="padding-left: 30px;">   "OtherDocuments": "",<b style="padding-left: 20px;">   Другие документы</b></p>
              <p  style="padding-left: 30px;">   "PassportSeria": "ВВ",<b style="padding-left: 20px;">   Серия паспорта</b></p>
              <p  style="padding-left: 30px;">   "PassportNumber": "1234567",<b style="padding-left: 20px;">   Номер паспорта</b></p>
              <p  style="padding-left: 30px;">   "Goods": <b style="padding-left: 20px;">   Товары</b></p>
                <p style="padding-left: 30px;">   [{</p>
                    <p style="padding-left: 60px;">      "GoodName": "Товары  *(весом от 20 до 30 кг и размером до 100 см. по каждой из сторон.)",</p>
                    <p style="padding-left: 60px;">      "GoodModel": "СДМ-2500",</p>
                    <p style="padding-left: 60px;">      "GoodCost": "900,00",</p>
                    <p style="padding-left: 60px;">      "GoodCount": "1"</p>
                <p style="padding-left: 30px;">   }]</p>
            <p>}]</p>

    <p><i>​</i></p>

    <p align="center"><b><font size="5">API-методы для населенных пунктов</font></b></p>
    <p><b><font size="4">Список всех городов</font></b></p>
    <p><font size="4"><font size="3">Метод возвращает список всех городов&nbsp;и основную информацию о них(ID области, ID района)</font></font></p>
    <p><font size="4"><font size="3">Method: POST</font></font></p>
    <p><font size="4"><font size="3">URL:&nbsp;</font></font>https://ветер.бел/WebServices/PublicAPI/CityAPI.asmx/GetAllCityWithIdJSON</p>
    <p><b>Headers</b></p>
    <ol>
	    <li><i>content-type:&nbsp;application/json</i></li>
	    <li><i>userid: на главной странице пользователя(<span style="color: #333333; font-family: &quot;Open Sans&quot;, sans-serif; font-size: 16px; background-color: #eeeeee;">№ кабинета</span>)</i></li>
	    <li><i>apikey: на главной странице пользователя(<font color="#333333" face="Open Sans, sans-serif"><span style="background-color: #eeeeee;">api-key</span></font>)</i></li>
    </ol>
    <p><i>​</i></p>
    <p><b><font size="4">Список всех районов</font></b></p>
    <p><font size="4"><font size="3">Метод возвращает список всех районов, дни доставки и ID</font></font></p>
    <p><font size="4"><font size="3">Method: POST</font></font></p>
    <p><font size="4"><font size="3">URL:&nbsp;</font></font>https://ветер.бел/WebServices/PublicAPI/CityAPI.asmx/GetAllDistricsWithIdJSON</p>
    <p><b>Headers</b></p>
    <ol>
	    <li><i>content-type:&nbsp;application/json</i></li>
	    <li><i>userid: на главной странице пользователя(<span style="color: #333333; font-family: &quot;Open Sans&quot;, sans-serif; font-size: 16px; background-color: #eeeeee;">№ кабинета</span>)</i></li>
	    <li><i>apikey: на главной странице пользователя(<font color="#333333" face="Open Sans, sans-serif"><span style="background-color: #eeeeee;">api-key</span></font>)</i></li>
    </ol>
    <p>&nbsp;</p>
    <p><i>​</i><b><font size="4">Список всех областей</font></b></p>
    <p><font size="4"><font size="3">Метод возвращает список всех областей и их ID</font></font></p>
    <p><font size="4"><font size="3">Method: POST</font></font></p>
    <p><font size="4"><font size="3">URL:&nbsp;</font></font>https://ветер.бел/WebServices/PublicAPI/CityAPI.asmx/GetAllTracksWithIdJSON</p>
    <p><b>Headers</b></p>
    <ol>
	    <li><i>content-type:&nbsp;application/json</i></li>
	    <li><i>userid: на главной странице пользователя(<span style="color: #333333; font-family: &quot;Open Sans&quot;, sans-serif; font-size: 16px; background-color: #eeeeee;">№ кабинета</span>)</i></li>
	    <li><i>apikey: на главной странице пользователя(<font color="#333333" face="Open Sans, sans-serif"><span style="background-color: #eeeeee;">api-key</span></font>)</i></li>
    </ol>
    <p align="center"><i>​</i></p>
    <p align="center"><b><font size="5">API-методы для товаров</font></b></p>
    <p><b><font size="4">Список всех товаров</font></b></p>
    <p><font size="4"><font size="3">Метод возвращает список всех товаров и ID категории</font></font></p>
    <p><font size="4"><font size="3">Method: POST</font></font></p>
    <p><font size="4"><font size="3">URL:&nbsp;</font></font>https://ветер.бел/WebServices/PublicAPI/GoodsAPI.asmx/GetAllGoodsWithIdJSON</p>
    <p><b>Headers</b></p>
    <ol>
	    <li><i>content-type:&nbsp;application/json</i></li>
	    <li><i>userid: на главной странице пользователя(<span style="color: #333333; font-family: &quot;Open Sans&quot;, sans-serif; font-size: 16px; background-color: #eeeeee;">№ кабинета</span>)</i></li>
	    <li><i>apikey: на главной странице пользователя(<font color="#333333" face="Open Sans, sans-serif"><span style="background-color: #eeeeee;">api-key</span></font>)</i></li>
    </ol>
    <p><i>​</i></p>
    <p><b><font size="4">Список всех товаров</font></b></p>
    <p><font size="4"><font size="3">Метод возвращает список всех категорий товаров и их ID</font></font></p>
    <p><font size="4"><font size="3">Method: POST</font></font></p>
    <p><font size="4"><font size="3">URL:&nbsp;</font></font>https://ветер.бел/WebServices/PublicAPI/GoodsAPI.asmx/GetAllCategoriesWithIdJSON</p>
    <p><b>Headers</b></p>
    <ol>
	    <li><i>content-type:&nbsp;application/json</i></li>
	    <li><i>userid: на главной странице пользователя(<span style="color: #333333; font-family: &quot;Open Sans&quot;, sans-serif; font-size: 16px; background-color: #eeeeee;">№ кабинета</span>)</i></li>
	    <li><i>apikey: на главной странице пользователя(<font color="#333333" face="Open Sans, sans-serif"><span style="background-color: #eeeeee;">api-key</span></font>)</i></li>
    </ol>
    <p align="center"><i>​</i></p>
    <p align="center"><b style="text-align: center;"><font size="5">API-методы для профилей</font></b></p>
    <p><b><font size="4">Список всех профилей</font></b></p>
    <p><font size="4"><font size="3">Метод возвращает список всех профилей для текущего пользователя(userid)</font></font></p>
    <p><font size="4"><font size="3">Method: POST</font></font></p>
    <p><font size="4"><font size="3">URL:&nbsp;</font></font>https://ветер.бел/WebServices/UserAPI/UserProfileAPI.asmx/GetProfilesJSON</p>
    <p><b>Headers</b></p>
    <ol>
	    <li><i>content-type:&nbsp;application/json</i></li>
	    <li><i>userid: на главной странице пользователя(<span style="color: #333333; font-family: &quot;Open Sans&quot;, sans-serif; font-size: 16px; background-color: #eeeeee;">№ кабинета</span>)</i></li>
	    <li><i>apikey: на главной странице пользователя(<font color="#333333" face="Open Sans, sans-serif"><span style="background-color: #eeeeee;">api-key</span></font>)</i></li>
    </ol>
    <p><i>​</i></p>
</asp:Content>
