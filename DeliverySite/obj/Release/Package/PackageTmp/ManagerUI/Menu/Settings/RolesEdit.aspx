<%@ Page Title="" Language="C#" MasterPageFile="~/ManagerMasterPage.master" AutoEventWireup="true" CodeBehind="RolesEdit.aspx.cs" Inherits="Delivery.ManagerUI.Menu.Settings.RolesEdit" %>
<%@ Import Namespace="Delivery.Resources" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <h3 class="h3custom" style="margin-top: 0;"><%# ActionText %> роли</h3>
    <div class="single-input-fild" style="width: 550px;">
        <style>
            .role-label {
                width: 470px;
                display: inline-block;
            }
            .role-label-two {
                width: 455px;
                display: inline-block;
                padding-top: 5px;
            }
        </style>
        <div class="form-group">
            <label>Название роли <asp:Label runat="server" ID="lblIsBase"></asp:Label></label>
            <asp:TextBox ID="tbName" runat="server" width="100%" CssClass="searchField form-control"/>
        </div>

        <div class="form-group">
            <label>На русском</label>
            <asp:TextBox ID="tbNameOnRuss" runat="server" width="100%" CssClass="searchField form-control"/>
        </div>
    
        <div style="padding-left: 30px; margin-bottom: 10px;">
            <b style="margin-left: -30px;">Видимость таблицы заявок</b>
            <div class="form-group roles-edit">
                <label class="role-label">UID" <span style="font-size: 9px; font-style: italic"></span></label>
                <asp:CheckBox ID="cbTableUID" runat="server" />
            </div>
            <div class="form-group roles-edit">
                <label class="role-label">НД" <span style="font-size: 9px; font-style: italic"></span></label>
                <asp:CheckBox ID="cbTableND" runat="server" />
            </div>
            <div class="form-group roles-edit">
                <label class="role-label">Профиль" <span style="font-size: 9px; font-style: italic"></span></label>
                <asp:CheckBox ID="cbTableProfile" runat="server" />
            </div>
            <div class="form-group roles-edit">
                <label class="role-label">Грузы" <span style="font-size: 9px; font-style: italic"></span></label>
                <asp:CheckBox ID="cbTableGoods" runat="server" />
            </div>
            <div class="form-group roles-edit">
                <label class="role-label">КК" <span style="font-size: 9px; font-style: italic"></span></label>
                <asp:CheckBox ID="cbTableKK" runat="server" />
            </div>
            <div class="form-group roles-edit">
                <label class="role-label">Отпр. из" <span style="font-size: 9px; font-style: italic"></span></label>
                <asp:CheckBox ID="cbTableSendFrom" runat="server" />
            </div>
            <div class="form-group roles-edit">
                <label class="role-label">Нпр." <span style="font-size: 9px; font-style: italic"></span></label>
                <asp:CheckBox ID="cbTableDirection" runat="server" />
            </div>
            <div class="form-group roles-edit">
                <label class="role-label">Город получ." <span style="font-size: 9px; font-style: italic"></span></label>
                <asp:CheckBox ID="cbTableRecieverCity" runat="server" />
            </div>
            <div class="form-group roles-edit">
                <label class="role-label">Адр. получателя" <span style="font-size: 9px; font-style: italic"></span></label>
                <asp:CheckBox ID="cbTableRecieverAddr" runat="server" />
            </div>
            <div class="form-group roles-edit">
                <label class="role-label">Получатель" <span style="font-size: 9px; font-style: italic"></span></label>
                <asp:CheckBox ID="cbTableReciever" runat="server" />
            </div>
            <div class="form-group roles-edit">
                <label class="role-label">Примечания" <span style="font-size: 9px; font-style: italic"></span></label>
                <asp:CheckBox ID="cbTableNotes" runat="server" />
            </div>
            <div class="form-group roles-edit">
                <label class="role-label">Комментарии" <span style="font-size: 9px; font-style: italic"></span></label>
                <asp:CheckBox ID="cbTableComments" runat="server" />
            </div>
            <div class="form-group roles-edit">
                <label class="role-label">Дополнит. опции" <span style="font-size: 9px; font-style: italic"></span></label>
                <asp:CheckBox ID="cbTableOtherOptions" runat="server" />
            </div>
            <div class="form-group roles-edit">
                <label class="role-label">Дата приема" <span style="font-size: 9px; font-style: italic"></span></label>
                <asp:CheckBox ID="cbTableRecieveDate" runat="server" />
            </div>
            <div class="form-group roles-edit">
                <label class="role-label">Дата отпр." <span style="font-size: 9px; font-style: italic"></span></label>
                <asp:CheckBox ID="cbTableSendDate" runat="server" />
            </div>
            <div class="form-group roles-edit">
                <label class="role-label">Статус" <span style="font-size: 9px; font-style: italic"></span></label>
                <asp:CheckBox ID="cbTableStatus" runat="server" />
            </div>
            <div class="form-group roles-edit">
                <label class="role-label">DID" <span style="font-size: 9px; font-style: italic"></span></label>
                <asp:CheckBox ID="cbTableDID" runat="server" />
            </div>
            <div class="form-group roles-edit">
                <label class="role-label">Оцен./Согл." <span style="font-size: 9px; font-style: italic"></span></label>
                <asp:CheckBox ID="cbTableAgreedCost" runat="server" />
            </div>
            <div class="form-group roles-edit">
                <label class="role-label">За услугу" <span style="font-size: 9px; font-style: italic"></span></label>
                <asp:CheckBox ID="cbTableCost" runat="server" />
            </div>
            <div class="form-group roles-edit">
                <label class="role-label">ПН" <span style="font-size: 9px; font-style: italic"></span></label>
                <asp:CheckBox ID="cbTablePN" runat="server" />
            </div>
            <div class="form-group roles-edit">
                <label class="role-label">Вес" <span style="font-size: 9px; font-style: italic"></span></label>
                <asp:CheckBox ID="cbTableWeight" runat="server" />
            </div>
        </div>
        
        <div style="padding-left: 30px; margin-bottom: 10px;">
            <b style="margin-left: -30px;">Контент</b>
            <div class="form-group roles-edit">
                <label class="role-label">Страница "Новости" <span style="font-size: 9px; font-style: italic">(меню)</span></label>
                <asp:CheckBox ID="cbPageNewsView" runat="server" />
                <div style="padding-left: 15px;">
                    <label class="role-label-two">создание/редактирование новостей</label>
                    <asp:CheckBox ID="cbPageNewsEdit" runat="server" />
                    <label class="role-label-two">удаление новостей</label>
                    <asp:CheckBox ID="cbActionNewsDelete" runat="server" />
                </div>
            </div>
        
            <div class="form-group roles-edit">
                <label class="role-label">Страница "Страницы" <span style="font-size: 9px; font-style: italic">(меню)</span></label>
                <asp:CheckBox ID="cbPagePagesView" runat="server" />
                <div style="padding-left: 15px;">
                    <label class="role-label-two">редактирование страниц</label>
                    <asp:CheckBox ID="cbPagePagesEdit" runat="server" />
                </div>
            </div>
        
            <div class="form-group roles-edit">
                <label class="role-label">Страница "Уведомления" <span style="font-size: 9px; font-style: italic">(меню)</span></label>
                <asp:CheckBox ID="cbPageNotificationsView" runat="server" />
                <div style="padding-left: 15px;">
                    <label class="role-label-two">редактирование уведомлений</label>
                    <asp:CheckBox ID="cbPageNotificationsEdit" runat="server" />
                </div>
            </div>
        
            <div class="form-group roles-edit">
                <label class="role-label">Страница "Письма" <span style="font-size: 9px; font-style: italic">(меню)</span></label>
                <asp:CheckBox ID="cbPageEmailNotificationsView" runat="server" />
                <div style="padding-left: 15px;">
                    <label class="role-label-two">редактирование писем</label>
                    <asp:CheckBox ID="cbPageEmailNotificationsEdit" runat="server" />
                </div>
            </div>
        
            <div class="form-group roles-edit">
                <label class="role-label">Страница "Логи ошибок" <span style="font-size: 9px; font-style: italic">(меню)</span></label>
                <asp:CheckBox ID="cbPageErrorsLogView" runat="server" />
                <div style="padding-left: 15px;">
                    <label class="role-label-two">подробный просмотр ошибок</label>
                    <asp:CheckBox ID="cbPageErrorsLogEdit" runat="server" />
                    <label class="role-label-two">удаление ошибок</label>
                    <asp:CheckBox ID="cbActionErrorsLogDelete" runat="server" />
                </div>
            </div>
        
            <div class="form-group roles-edit">
                <label class="role-label">Страница "Логи действий" <span style="font-size: 9px; font-style: italic">(меню)</span></label>
                <asp:CheckBox ID="cbPageLogsView" runat="server" />
            </div>
            
            <div class="form-group roles-edit">
                <label class="role-label">Страница "Логи запросов к API" <span style="font-size: 9px; font-style: italic">(меню)</span></label>
                <asp:CheckBox ID="cbPageApiLogView" runat="server" />
                <div style="padding-left: 15px;">
                    <label class="role-label-two">управление правами пользователя на использование API</label>
                    <asp:CheckBox ID="cbAllowBlockingAddApiAccess" runat="server" />
                </div>
            </div>
        </div>
        


        <div style="padding-left: 30px; margin-bottom: 10px;">
            <b style="margin-left: -30px;">Сущности</b>
            
            <div class="form-group roles-edit">
                <label class="role-label">Страница "Категории" <span style="font-size: 9px; font-style: italic">(меню)</span></label>
                <asp:CheckBox ID="cbPageCategoryView" runat="server" />
                <div style="padding-left: 15px;">
                    <label class="role-label-two">создание/редактирование категории</label>
                    <asp:CheckBox ID="cbPageCategoryEdit" runat="server" />
                    <label class="role-label-two">удаление категории</label>
                    <asp:CheckBox ID="cbActionCategoryDelete" runat="server" />
                </div>
            </div>

            <div class="form-group roles-edit">
                <label class="role-label">Страница "Наименования" <span style="font-size: 9px; font-style: italic">(меню)</span></label>
                <asp:CheckBox ID="cbPageTitlesView" runat="server" />
                <div style="padding-left: 15px;">
                    <label class="role-label-two">создание/редактирование наименования</label>
                    <asp:CheckBox ID="cbPageTitlesEdit" runat="server" />
                    <label class="role-label-two">удаление наименования</label>
                    <asp:CheckBox ID="cbActionTitlesDelete" runat="server" />
                </div>
            </div>
        
            <div class="form-group roles-edit">
                <label class="role-label">Страница "Города" <span style="font-size: 9px; font-style: italic">(меню)</span></label>
                <asp:CheckBox ID="cbPageCityView" runat="server" />
                <div style="padding-left: 15px;">
                    <label class="role-label-two">создание/редактирование городов</label>
                    <asp:CheckBox ID="cbPageCityEdit" runat="server" />
                    <label class="role-label-two">удаление городов</label>
                    <asp:CheckBox ID="cbActionCityDelete" runat="server" />
                </div>
            </div>
            
            <div class="form-group roles-edit">
                <label class="role-label">Страница "Районы" <span style="font-size: 9px; font-style: italic">(меню)</span></label>
                <asp:CheckBox ID="cbPageDistrictsView" runat="server" />
                <div style="padding-left: 15px;">
                    <label class="role-label-two">редактирование районов</label>
                    <asp:CheckBox ID="cbPageDistrictEdit" runat="server" />
                </div>
            </div>
        
            <div class="form-group roles-edit">
                <label class="role-label">Страница "Направления" <span style="font-size: 9px; font-style: italic">(меню)</span></label>
                <asp:CheckBox ID="cbPageTracksView" runat="server" />
                <div style="padding-left: 15px;">
                    <label class="role-label-two">создание/редактирование направлений</label>
                    <asp:CheckBox ID="cbPageTracksEdit" runat="server" />
                    <label class="role-label-two">удаление направлений</label>
                    <asp:CheckBox ID="cbActionTracksDelete" runat="server" />
                </div>
            </div>
        
            <div class="form-group roles-edit">
                <label class="role-label">Страница "Склады" <span style="font-size: 9px; font-style: italic">(меню)</span></label>
                <asp:CheckBox ID="cbPageWarehousesView" runat="server" />
                <div style="padding-left: 15px;">
                    <label class="role-label-two">создание/редактирование складов</label>
                    <asp:CheckBox ID="cbPageWarehouseEdit" runat="server" />
                    <label class="role-label-two">удаление складов</label>
                    <asp:CheckBox ID="cbActionDeleteWarehouses" runat="server" />
                </div>
            </div>

            <div class="form-group roles-edit">
                <label class="role-label">Страница "Водители" <span style="font-size: 9px; font-style: italic">(меню)</span></label>
                <asp:CheckBox ID="cbPageDriversView" runat="server" />
                <div style="padding-left: 15px;">
                    <label class="role-label-two">просмотр водителя</label>
                    <asp:CheckBox ID="cbPageDriverView" runat="server" />
                    <label class="role-label-two">создание/редактирование водителей</label>
                    <asp:CheckBox ID="cbPageDriversEdit" runat="server" />
                    <label class="role-label-two">удаление водителей</label>
                    <asp:CheckBox ID="cbActionDriversDelete" runat="server" />
                </div>
            </div>
        
            <div class="form-group roles-edit">
                <label class="role-label">Страница "Автомобили" <span style="font-size: 9px; font-style: italic">(меню)</span></label>
                <asp:CheckBox ID="cbPageCarsView" runat="server" />
                <div style="padding-left: 15px;">
                    <label class="role-label-two">просмотр автомобиля</label>
                    <asp:CheckBox ID="cbPageCarView" runat="server" />
                    <label class="role-label-two">создание/редактирование автомобиля</label>
                    <asp:CheckBox ID="cbPageCarEdit" runat="server" />
                    <label class="role-label-two">удаление автомобилей</label>
                    <asp:CheckBox ID="cbActionCarsDelete" runat="server" />
                </div>
            </div>

            <div class="form-group roles-edit">
                <label class="role-label">Страница "Сотрудники" <span style="font-size: 9px; font-style: italic">(меню)</span></label>
                <asp:CheckBox ID="cbPageManagersView" runat="server" />
                <div style="padding-left: 15px;">
                    <label class="role-label-two">просмотр сотрудника</label>
                    <asp:CheckBox ID="cbPageManagerView" runat="server" />
                    <label class="role-label-two">создание/редактирование сотрудников</label>
                    <asp:CheckBox ID="cbPageManagersEdit" runat="server" />
                    <label class="role-label-two">удаление сотрудников</label>
                    <asp:CheckBox ID="cbActionManagersDelete" runat="server" />
                </div>
            </div>
            
            <div class="form-group roles-edit">
                <label class="role-label">Страница "Клиенты" <span style="font-size: 9px; font-style: italic">(меню и возможность просмотра клиентов)</span></label>
                <asp:CheckBox ID="cbPageClientsView" runat="server" />
                <div style="padding-left: 15px;">
                    <label class="role-label-two">редактирование клиентов</label>
                    <asp:CheckBox ID="cbPageClientsEdit" runat="server" />
                    <label class="role-label-two">создание клиентов</label>
                    <asp:CheckBox ID="cbPageClientsCreate" runat="server" />
                    <label class="role-label-two">удаление клиентов</label>
                    <asp:CheckBox ID="cbActionClientsDelete" runat="server" />
                    <label class="role-label-two">управление ответственными менеджерами</label>
                    <asp:CheckBox ID="cbActionAddManagerToUser" runat="server" />
                    <label class="role-label-two">управление ответственными менеджерами по продажам</label>
                    <asp:CheckBox ID="cbActionAddSalesManagerToUser" runat="server" />
                    <label class="role-label-two">добавление\удаление категорий клиенту</label>
                    <asp:CheckBox ID="cbActionCategoryAssignToUser" runat="server" />
                    <label class="role-label-two">возможность активировать\блокировать клиента</label>
                    <asp:CheckBox ID="cbActionClientActivateBlock" runat="server" />
                    <label class="role-label-two">промотр стат. информации по клиенту</label>
                    <asp:CheckBox ID="cbViewClientStatInfo" runat="server" />
                    <label class="role-label-two">видимость профиля клиента на страницах заявок</label>
                    <asp:CheckBox ID="cbPageUserProfileView" runat="server" />
                    <label class="role-label-two">редактирование профиля</label>
                    <asp:CheckBox ID="cbPageUserProfileEdit" runat="server" />
                    <label class="role-label-two">удаление профиля</label>
                    <asp:CheckBox ID="cbActionUserProfileDelete" runat="server" />
                    <label class="role-label-two">управление статусами профилей клиента</label>
                    <asp:CheckBox ID="cbActionUserProfileChangeStatus" runat="server" />
                </div>
            </div>
            
            <div class="form-group roles-edit">
                <label class="role-label">Страница "Обращения" <span style="font-size: 9px; font-style: italic">(меню)</span></label>
                <asp:CheckBox ID="cbPageFeedbacksView" runat="server" />
                <div style="padding-left: 15px;">
                    <label class="role-label-two">полный доступ ко всем обращениям (втч удаление)</label>
                    <asp:CheckBox ID="cbFeedbackFullAccess" runat="server" />
                    <label class="role-label-two">доступ/ответственность за "Предложение"</label>
                    <asp:CheckBox ID="cbFeedback0" runat="server" />
                    <label class="role-label-two">доступ/ответственность за "Жалоба"</label>
                    <asp:CheckBox ID="cbFeedback1" runat="server" />
                    <label class="role-label-two">доступ/ответственность за "Заявка на добавление наименования"</label>
                    <asp:CheckBox ID="cbFeedback2" runat="server" />
                    <label class="role-label-two">доступ/ответственность за "Обращение в тех. отдел"</label>
                    <asp:CheckBox ID="cbFeedback3" runat="server" />
                    <label class="role-label-two">доступ/ответственность за "Обращение в бухгалтерию"</label>
                    <asp:CheckBox ID="cbFeedback4" runat="server" />
                    <label class="role-label-two">доступ/ответственность за "Обращение к менеджерам"</label>
                    <asp:CheckBox ID="cbFeedback5" runat="server" />
                    <label class="role-label-two">доступ/ответственность за "Обращение по вопросам цен, отклонений"</label>
                    <asp:CheckBox ID="cbFeedback6" runat="server" />
                </div>
            </div>
            
            <div class="form-group roles-edit">
                <label class="role-label">Страница "Поставщики"</label>
                <div style="padding-left: 15px;">
                    <label class="role-label-two">создание\редактирование поставщиков</label>
                    <asp:CheckBox ID="cbActionProviderEdit" runat="server" />
                    <label class="role-label-two">удаление поставщиков</label>
                    <asp:CheckBox ID="cbActionProviderDelete" runat="server" />
                </div>
            </div>
        </div>
        
        

        <div style="padding-left: 30px; margin-bottom: 10px;">
            <b style="margin-left: -30px;">Заявки</b>
            <div class="form-group roles-edit">
                <label class="role-label">Страница "Заявки Минск" <span style="font-size: 9px; font-style: italic">(меню)</span></label>
                <asp:CheckBox ID="cbPageUserTicketByDeliveryOnMinsk" runat="server" />
            </div>
        
            <div class="form-group roles-edit">
                <label class="role-label">Страница "Заявки РБ" <span style="font-size: 9px; font-style: italic">(меню)</span></label>
                <asp:CheckBox ID="cbPageUserTicketByDeliveryOnBelarus" runat="server" />
            </div>

            <div class="form-group roles-edit">
                <label class="role-label">Страница "Мои заявки" <span style="font-size: 9px; font-style: italic">(меню)</span></label>
                <asp:CheckBox ID="cbPageMyTickets" runat="server" />
            </div>

            <div class="form-group roles-edit">
                <label class="role-label">Страница "Все заявки" <span style="font-size: 9px; font-style: italic">(меню)</span></label>
                <asp:CheckBox ID="cbPageUserTicketView" runat="server" />
            </div>
        
            <div class="form-group roles-edit">
                <label class="role-label">Страница "Необработанные" <span style="font-size: 9px; font-style: italic">(меню)</span></label>
                <asp:CheckBox ID="cbPageUserTicketNotProcessedView" runat="server" />
            </div>

            <div class="form-group roles-edit">
                <label class="role-label">Страница редактирование заявок</label>
                <asp:CheckBox ID="cbPageUserTicketEdit" runat="server" />
            </div>
            
            <div class="form-group roles-edit">
                <label class="role-label"><i>1)</i> Запрет на редактирование информации вне блока "Информация, заполняемая менеджером" <span style="font-size: 9px; font-style: italic; color: red;">(распространяется на пункт <b>8</b> )</span></label>
                <asp:CheckBox ID="cbActionDisallowTicketChangeWithoutManagerInfo" runat="server" />
            </div>
            
            <div class="form-group roles-edit">
                <label class="role-label"><i>2)</i> Запрет на полное редактирование полей заявок в статусах отличных от "<%= TicketStatusesResources.NotProcessed %>", "<%= TicketStatusesResources.InStock %>", "<%= TicketStatusesResources.Transfer_InStock %>", "<%= TicketStatusesResources.Cancel %>", "<%= TicketStatusesResources.Cancel_InStock %>" на странице редактирования заявки</label>
                <asp:CheckBox ID="cbActionDisallowEditSomeFieldInTickets" runat="server" />
            </div>

            <div class="form-group roles-edit">
                <label class="role-label"><i>3)</i> Удаление заявки со страницы редактирования</label>
                <asp:CheckBox ID="cbActionUserTicketDelete" runat="server" />
            </div> 
            
            <div class="form-group roles-edit">
                <label class="role-label"><i>5)</i> Запрет изменения статуса при статусе заявки "<%= TicketStatusesResources.Completed %>" <span style="font-size: 9px; font-style: italic; color: red;">(распространяется на пункт <b>4</b> )</span></label>
                <asp:CheckBox ID="cbActionCompletedStatus" runat="server" />
            </div>
            
            <div class="form-group roles-edit">
                <label class="role-label"><i>6)</i> Разрешение на изменения статуса заявки без водителя в "<%= TicketStatusesResources.Completed %>" </label>
                <asp:CheckBox ID="cbActionAllowChangeInCompletedWithoutDriver" runat="server" />
            </div>

            <div class="form-group roles-edit">
                <label class="role-label"><i>7)</i> Запрет изменения статуса заявок в статусе "<%= TicketStatusesResources.Processed %>" на статус отличный от "<%= TicketStatusesResources.Completed %>"</label>
                <asp:CheckBox ID="cbActionDisallowDeliveredToCompletedStatus" runat="server" />
            </div>
            
            <div class="form-group roles-edit">
                <label class="role-label"><i>8)</i> Разрешение на изменения курсов и стоимостей заявки на странице редактирования заявки</label>
                <asp:CheckBox ID="cbActionAllowChangeMoneyAndCourse" runat="server" />
            </div>
            
            <div class="form-group roles-edit">
                <label class="role-label"> Разрешение на произведение СМС-рассылки из панели "Города"</label>
                <asp:CheckBox ID="cbActionSendSmsBulk" runat="server" />
            </div>
            
            
            <div class="form-group roles-edit">
                <label class="role-label">Показать кнопку "Доставлено" в панели "Города" <span style="font-size: 9px; font-style: italic">(дает возможность переводить заявки на город в статус "доставлено")</span></label>
                <asp:CheckBox ID="cbActionDeliveredButtonInCity" runat="server" />
            </div>
        </div>
        
        

        <div style="padding-left: 30px; margin-bottom: 10px;">
            <b style="margin-left: -30px;">Финансы</b>
            <div class="form-group roles-edit">
                <label class="role-label">Страница "Приемка" <span style="font-size: 9px; font-style: italic">(меню)</span></label>
                <asp:CheckBox ID="cbPageMoneyView" runat="server" />
            </div>
        
            <div class="form-group roles-edit">
                <label class="role-label">Страница "Контроль водителя" <span style="font-size: 9px; font-style: italic">(меню)</span></label>
                <asp:CheckBox ID="cbPageMoneyDriverView" runat="server" />
            </div>
        
            <div class="form-group roles-edit">
                <label class="role-label">Страница "Сумма заявок "<%= TicketStatusesResources.Processed %>" <span style="font-size: 9px; font-style: italic">(меню)</span></label>
                <asp:CheckBox ID="cbPageMoneyDeliveredView" runat="server" />
            </div>
        
            <div class="form-group roles-edit">
                <label class="role-label">Страница "Сумма "За услугу" <span style="font-size: 9px; font-style: italic">(меню)</span></label>
                <asp:CheckBox ID="cbPageMoneyDeliveryView" runat="server" />
            </div>
            
            <div class="form-group roles-edit">
                <label class="role-label">Страница "Расчет" <span style="font-size: 9px; font-style: italic">(меню)</span></label>
                <asp:CheckBox ID="cbPageCalculationView" runat="server" />
            </div>
        </div>
        
        

        <div style="padding-left: 30px; margin-bottom: 10px;">
            <b style="margin-left: -30px;">Выдача</b>
            <div class="form-group roles-edit">
                <label class="role-label">Страница "Заявки к выдаче" <span style="font-size: 9px; font-style: italic">(меню)</span></label>
                <asp:CheckBox ID="cbPageIssuanceView" runat="server" />
            </div>

            <div class="form-group roles-edit">
                <label class="role-label">Страница "Новые Расчётные листы" <span style="font-size: 9px; font-style: italic">(меню)</span></label>
                <asp:CheckBox ID="cbPageNewIssuanceView" runat="server" />
            </div>
        
            <div class="form-group roles-edit">
                <label class="role-label">Страница "Расчетные листы" <span style="font-size: 9px; font-style: italic">(меню)</span></label>
                <asp:CheckBox ID="cbPageIssuanceListsView" runat="server" />
            </div>
        
            <div class="form-group roles-edit">
                <label class="role-label">Страница "Создать расч. лист" <span style="font-size: 9px; font-style: italic">(меню)</span>, редактирование расч. листа</label>
                <asp:CheckBox ID="cbPageIssuanceListsEdit" runat="server" />
            </div>
            
            <div class="form-group roles-edit">
                <label class="role-label">Просмотр расч. листа и его заявок</label>
                <asp:CheckBox ID="cbPageIssuanceListView" runat="server" />
            </div>
            
            <div class="form-group roles-edit">
                <label class="role-label">Удаление расч. листа</label>
                <asp:CheckBox ID="cbActionIssuanceListDelete" runat="server" />
            </div>
        </div>
        
        

        <div style="padding-left: 30px; margin-bottom: 10px;">
            <b style="margin-left: -30px;">Документы</b>
            <div class="form-group roles-edit">
                <label class="role-label"> Просмотр, редактирование и удаление расчетного листа <span style="font-size: 9px; font-style: italic">(меню)</span></label>
                <asp:CheckBox ID="cbRasch" runat="server" />
            </div>

            <div class="form-group roles-edit">
                <label class="role-label">Страница "Архив" <span style="font-size: 9px; font-style: italic">(меню)</span></label>
                <asp:CheckBox ID="cbPageReportsView" runat="server" />
            </div>
            
            <div class="form-group roles-edit">
                <label class="role-label">Редактирование документов</label>
                <asp:CheckBox ID="cbPageReportsEdit" runat="server" />
            </div>
            
            <div class="form-group roles-edit">
                <label class="role-label">Удаление документов</label>
                <asp:CheckBox ID="cbActionReportsDelete" runat="server" />
            </div>
            
            <div class="form-group roles-edit">
                <label class="role-label">Страница "Экспорт" <span style="font-size: 9px; font-style: italic">(меню и списки для рассылок)</span></label>
                <asp:CheckBox ID="cbPageReportsExport" runat="server" />
            </div>
            
            <div class="form-group roles-edit">
                <label class="role-label">Экспорт таблицы "Пользователи"</label>
                <asp:CheckBox ID="cbActionExportAllUsersInfo" runat="server" />
            </div>
            
            <div class="form-group roles-edit">
                <label class="role-label">Экспорт таблицы "Профили"</label>
                <asp:CheckBox ID="cbActionExportAllUsersProfilesInfo" runat="server" />
            </div>
            
            <div class="form-group roles-edit">
                <label class="role-label">Экспорт таблицы "Клиенты"</label>
                <asp:CheckBox ID="cbActionExportAllClientsInfo" runat="server" />
            </div>
            
            <div class="form-group roles-edit">
                <label class="role-label">Страница "Отправка КП" <span style="font-size: 9px; font-style: italic">(меню)</span></label>
                <asp:CheckBox ID="cbPageSendComProp" runat="server" />
            </div>
        </div>
        
        

        <div style="padding-left: 30px; margin-bottom: 10px;">
            <b style="margin-left: -30px;">Настройки</b>
            <div class="form-group roles-edit">
                <label class="role-label">Страница "Пароли" <span style="font-size: 9px; font-style: italic">(меню)</span></label>
                <asp:CheckBox ID="cbPageChangePasswords" runat="server" />
            </div>
        
            <div class="form-group roles-edit">
                <label class="role-label">Страница "Скидки" <span style="font-size: 9px; font-style: italic">(меню)</span></label>
                <asp:CheckBox ID="cbPageUserDiscountView" runat="server" />
            </div>
        
            <div class="form-group roles-edit">
                <label class="role-label">Страница "Роли" <span style="font-size: 9px; font-style: italic">(меню)</span></label>
                <asp:CheckBox ID="cbPageRolesView" runat="server" />
                <div style="padding-left: 15px;">
                    <label class="role-label-two">создание/редактирование ролей</label>
                    <asp:CheckBox ID="cbPageRolesEdit" runat="server" />
                    <label class="role-label-two">удаление ролей</label>
                    <asp:CheckBox ID="cbActionRolesDelete" runat="server" />
                </div>
            </div>
        
            <div class="form-group roles-edit">
                <label class="role-label">Страница "BackEnd" <span style="font-size: 9px; font-style: italic">(меню)</span></label>
                <asp:CheckBox ID="cbPageBackendView" runat="server" />
                <div style="padding-left: 15px;">
                    <label class="role-label-two">редактирование конфигураций</label>
                    <asp:CheckBox ID="cbPageBackendEdit" runat="server" />
                </div>
            </div>
        </div>
        
        

        <div style="padding-left: 30px; margin-bottom: 10px;">
            <b style="margin-left: -30px;">Юзверь</b>
            <div class="form-group roles-edit">
                <label class="role-label">Раздел одноразового юзера <span style="font-size: 9px; font-style: italic">(меню)</span></label>
                <asp:CheckBox ID="cbSectionUser" runat="server" />
            </div>
        </div>
        
        

        <div style="padding-left: 30px; margin-bottom: 10px;">
            <b style="margin-left: -30px;">Плавающее меню действий над заявками</b>
            <div class="form-group roles-edit">
                <label class="role-label">Пункт "Удалить"</label>
                <asp:CheckBox ID="cbActionGroupUserTicketDelete" runat="server" />
            </div>
            <div class="form-group roles-edit">
                <label class="role-label">Пункт "Добавить водителя"</label>
                <asp:CheckBox ID="cbActionDriverAdd" runat="server" />
            </div>
            <div class="form-group roles-edit">
                <label class="role-label">Пункт "Изменить статус"</label>
                <asp:CheckBox ID="cbActionStatusAdd" runat="server" />
            </div>
            <div class="form-group roles-edit">
                <label class="role-label">Пункт "Печать наклеек"</label>
                <asp:CheckBox ID="cbActionPrintVinil" runat="server" />
            </div>
            <div class="form-group roles-edit">
                <label class="role-label">Пункт "Печать чеков"</label>
                <asp:CheckBox ID="cbActionPrintCheck" runat="server" />
            </div>
            <div class="form-group roles-edit">
                <label class="role-label">Пункт "Печать карты (кур.)"</label>
                <asp:CheckBox ID="cbActionPrintMap" runat="server" />
            </div>
            <div class="form-group roles-edit">
                <label class="role-label">Пункт "Печать карты (мен.)"</label>
                <asp:CheckBox ID="cbActionPrintMapForManager" runat="server" />
            </div>
            <div class="form-group roles-edit">
                <label class="role-label">Пункт "Печать карты (кас.)"</label>
                <asp:CheckBox ID="cbActionPrintMapForCashier" runat="server" />
            </div>
            <div class="form-group roles-edit">
                <label class="role-label">Пункт "Печать заявок"</label>
                <asp:CheckBox ID="cbActionPrintTickets" runat="server" />
            </div>
            <div class="form-group roles-edit">
                <label class="role-label">Пункт "Печать заказ-поручений"</label>
                <asp:CheckBox ID="cbActionPrintZP" runat="server" />
            </div>
            <div class="form-group roles-edit">
                <label class="role-label">Пункт "Печать накладной"</label>
                <asp:CheckBox ID="cbActionPrintNakl" runat="server" />
            </div>
            <div class="form-group roles-edit">
                <label class="role-label">Пункт "Печать приложения 1" <span style="font-size: 9px; font-style: italic">(печать приложения 2 конфигурируется через BackEnd)</span></label>
                <asp:CheckBox ID="cbActionPrintNaklPril" runat="server" />
            </div>
            <div class="form-group roles-edit">
                <label class="role-label">Пункт "Печать путевого листа 1"</label>
                <asp:CheckBox ID="cbActionPrintPutFirst" runat="server" />
            </div>
            <div class="form-group roles-edit">
                <label class="role-label">Пункт "Печать путевого листа 2"</label>
                <asp:CheckBox ID="cbActionPrintPutSecond" runat="server" />
            </div>
             <div class="form-group roles-edit">
                <label class="role-label">Пункт "Печать акта приема-передачи"</label>
                <asp:CheckBox ID="cbActionPrintAORT" runat="server" />
            </div>
        </div>
        
        

        <div style="padding-left: 30px; margin-bottom: 10px;">
            <b style="margin-left: -30px;">Другие действия над заявками</b>
            <div class="form-group roles-edit">
                <label class="role-label">Изменение чекбокса "НН"</label>
                <asp:CheckBox ID="cbActionControlNN" runat="server" />
            </div>
            <div class="form-group roles-edit">
                <label class="role-label">Изменение чекбокса "БУ"</label>
                <asp:CheckBox ID="cbActionControlActiza" runat="server" />
            </div>
            <div class="form-group roles-edit">
                <label class="role-label">Изменение поля "Комментарий"</label>
                <asp:CheckBox ID="cbActionControlComment" runat="server" />
            </div>
            <div class="form-group roles-edit">
                <label class="role-label">Изменение чекбоксов "без опл.", "обратка"</label>
                <asp:CheckBox ID="cbActionControlMoneyCheckboxes" runat="server" />
            </div>
            <div class="form-group roles-edit">
                <label class="role-label">Изменение поля "За услугу"</label>
                <asp:CheckBox ID="cbActionControlGruzobozCost" runat="server" />
            </div>
            <div class="form-group roles-edit">
                <label class="role-label">Изменение чекбокса "ПН"</label>
                <asp:CheckBox ID="cbActionControlPN" runat="server" />
            </div>
            <div class="form-group roles-edit">
                <label class="role-label">Изменение поля "Вес"</label>
                <asp:CheckBox ID="cbActionControlWeight" runat="server" />
            </div>
            <div class="form-group roles-edit">
                <label class="role-label">Изменение чекбокса "Проверено"</label>
                <asp:CheckBox ID="cbActionCheckedOutCheckbox" runat="server" />
            </div>
            <div class="form-group roles-edit">
                <label class="role-label">Изменение чекбокса "Дозвонились"</label>
                <asp:CheckBox ID="cbActionPhonedCheckbox" runat="server" />
            </div>
            <div class="form-group roles-edit">
                <label class="role-label">Изменение чекбокса "Счет выставлен"</label>
                <asp:CheckBox ID="cbActionBilledCheckbox" runat="server" />
            </div>
        </div>
        
        
        <div style="padding-left: 30px; margin-bottom: 10px;">
            <b style="margin-left: -30px;">Права на видимость статусов</b>
            <div class="form-group roles-edit">
                <label class="role-label">Не обработан</label>
                <asp:CheckBox ID="cbStatusNotProcessed" runat="server" />
            </div>
            <div class="form-group roles-edit">
                <label class="role-label">На складе</label>
                <asp:CheckBox ID="cbStatusInStock" runat="server" />
            </div>
            <div class="form-group roles-edit">
                <label class="role-label">В пути</label>
                <asp:CheckBox ID="cbStatusOnTheWay" runat="server" />
            </div>
            <div class="form-group roles-edit">
                <label class="role-label">Обработано</label>
                <asp:CheckBox ID="cbStatusProcessed" runat="server" />
            </div>
            <div class="form-group roles-edit">
                <label class="role-label">Доставлено</label>
                <asp:CheckBox ID="cbStatusDelivered" runat="server" />
            </div>
            <div class="form-group roles-edit">
                <label class="role-label">Завершено</label>
                <asp:CheckBox ID="cbStatusCompleted" runat="server" />
            </div>
            <div class="form-group roles-edit">
                <label class="role-label">Перенос (у курьера)</label>
                <asp:CheckBox ID="cbStatusTransferInCourier" runat="server" />
            </div>
            <div class="form-group roles-edit">
                <label class="role-label">Отказ (у курьера)</label>
                <asp:CheckBox ID="cbStatusRefusingInCourier" runat="server" />
            </div>
            <div class="form-group roles-edit">
                <label class="role-label">Обмен (у курьера)</label>
                <asp:CheckBox ID="cbStatusExchangeInCourier" runat="server" />
            </div>
            <div class="form-group roles-edit">
                <label class="role-label">Доставка от клиента (у курьера)</label>
                <asp:CheckBox ID="cbStatusDeliveryFromClientInCourier" runat="server" />
            </div>
            <div class="form-group roles-edit">
                <label class="role-label">Перенос (на складе)</label>
                <asp:CheckBox ID="cbStatusTransferInStock" runat="server" />
            </div>
            <div class="form-group roles-edit">
                <label class="role-label">Возврат (на складе)</label>
                <asp:CheckBox ID="cbStatusReturnInStock" runat="server" />
            </div>
            <div class="form-group roles-edit">
                <label class="role-label">Отмена (на складе)</label>
                <asp:CheckBox ID="cbStatusCancelInStock" runat="server" />
            </div>
            <div class="form-group roles-edit">
                <label class="role-label">Обмен (на складе)</label>
                <asp:CheckBox ID="cbStatusExchangeInStock" runat="server" />
            </div>
            <div class="form-group roles-edit">
                <label class="role-label">Доставка от клиента (на складе)</label>
                <asp:CheckBox ID="cbStatusDeliveryFromClientInStock" runat="server" />
            </div>
            <div class="form-group roles-edit">
                <label class="role-label">Отмена</label>
                <asp:CheckBox ID="cbStatusCancel" runat="server" />
            </div>
            <div class="form-group roles-edit">
                <label class="role-label">Отказ (в пути)</label>
                <asp:CheckBox ID="cbStatusRefusalOnTheWay" runat="server" />
            </div>
            <div class="form-group roles-edit">
                <label class="role-label">Отказ (по адресу)</label>
                <asp:CheckBox ID="cbStatusRefusalByAddress" runat="server" />
            </div>
            <div class="form-group roles-edit">
                <label class="role-label">К загрузке</label>
                <asp:CheckBox ID="cbStatusUpload" runat="server" />
            </div>
        </div>
        
            

        <div style="margin-top: 25px;">
            <asp:Button ID="btnCreate" runat="server" Text='<%# ButtonText %>' CssClass="btn btn-default btn-right" style="margin-left: 30px;"/>
            <asp:Button ID="btnBack" OnClientClick="window.history.go(-1);return false;" runat="server" CssClass="btn btn-default btn-right" Text='Назад'/>
        </div>
    </div>
    <script>
        $(function () {
            $(".role-label").click(function () {
                var checkbox = $(this).next("input");
                if (checkbox.is(':checked')) {
                    checkbox.prop('checked', false);
                } else {
                    checkbox.prop('checked', true);
                }
            });

            $(".role-label-two").click(function () {
                var checkbox = $(this).next("input");
                if (checkbox.is(':checked')) {
                    checkbox.prop('checked', false);
                } else {
                    checkbox.prop('checked', true);
                }
            });
        }); 
    </script>
</asp:Content>
