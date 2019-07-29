using Delivery.BLL.Helpers;
using Delivery.BLL.StaticMethods;
using Delivery.DAL.DataBaseObjects;
using DeliverySite.Resources;
using System;
using Delivery.Resources;

namespace Delivery.BLL.Filters
{
    public class TicketsFilter
    {
        public static string DriverChangeFilter(ref Tickets ticket, string currentDriverIdString, string currentStatusIdString, string oldStatusIdString, string newDriverIdString)
        {
            #region Обработка входящих параметров
            int? oStatusId;
            int? cStatusId;
            int? cDriverId;
            int? nDriverId;
            try
            {
                oStatusId = Convert.ToInt32(oldStatusIdString);
                cStatusId = Convert.ToInt32(currentStatusIdString);
                cDriverId = Convert.ToInt32(currentDriverIdString);
                nDriverId = Convert.ToInt32(newDriverIdString);
            }
            catch (Exception)
            {
                return "Неверный формат входных данных!";
            }
            #endregion

            #region  Присвоение нового водителя заявке
            ticket.DriverID = nDriverId;
            #endregion

            #region  Обновление связанных данных
            //если текущий водитель изменяется на существующего нового, и заявка в текущем статусе "На складе", "Перенос (на складе)", то меняем текущий статус заявки на новый "В пути", а старый статус на текущий
            if (cDriverId != nDriverId && nDriverId != 0 && (cStatusId == 2 || cStatusId == 4))
            {
                ticket.StatusID = 3;
                ticket.StatusIDOld = cStatusId;
            }

            //если текущий водитель изменяется на "не назначен", то меняем текущий статус на старый, а старый на текущий
            if (cDriverId != 0 && nDriverId == 0)
            {
                ticket.StatusID = oStatusId;
                ticket.StatusIDOld = cStatusId;
            }
            #endregion

            return null; //возвращаем null, что равно отсутствию ошибок
        }

        public static string StatusChangeFilter(ref Tickets ticket, string cDriverIdString, string cStatusIdString, string cStatusDescription, string cAdmissionDateString, string nStatusDescription, string nStatusIdString, string nDeliveryDateString, Roles currentRole)
        {
            #region Обработка входящих параметров
            int? cStatusId;
            int? nStatusId;
            int? cDriverId;
            DateTime nDeliveryDate = Convert.ToDateTime("01.01.0001 0:00:00");
            try
            {
                cStatusId = Convert.ToInt32(cStatusIdString);
                nStatusId = Convert.ToInt32(nStatusIdString);
                cDriverId = Convert.ToInt32(cDriverIdString);
            }
            catch (Exception)
            {
                return "Неверный формат входных данных!";
            }

            try
            {
                if (nStatusId == 4 || nStatusId == 11)
                {
                    nDeliveryDate = Convert.ToDateTime(nDeliveryDateString);
                }
            }
            catch (Exception)
            {
                return "Неверный формат входных данных (d)!";
            }
            #endregion

            #region  Проверки на доступность обновления заявки
            //если заявка изменяется из текущего статуса "Завершено" в любой другой новый, то проверяем права пользователя на изменение заявок в текущем статусе "Завершено"
            if (cStatusId == 6 && nStatusId != 6 && currentRole.ActionCompletedStatus == 1)
            {
                return String.Format("У вас нет прав на изменение статуса заявки в статусе \"{0}\"!", TicketStatusesResources.Completed);
            }

            #region Проверка на права выставления конкретного статуса
            if (cStatusId != 1 && nStatusId == 1 && currentRole.StatusNotProcessed != 1)
            {
                return String.Format("У вас нет прав выставлять статус \"{0}\"",
                        TicketStatusesResources.NotProcessed);
            }

            if (cStatusId != 2 && nStatusId == 2 && currentRole.StatusInStock != 1)
            {
                return String.Format("У вас нет прав выставлять статус \"{0}\"",
                        TicketStatusesResources.InStock);
            }

            if (cStatusId != 3 && nStatusId == 3 && currentRole.StatusOnTheWay != 1)
            {
                return String.Format("У вас нет прав выставлять статус \"{0}\"",
                        TicketStatusesResources.OnTheWay);
            }

            if (cStatusId != 4 && nStatusId == 4 && currentRole.StatusTransferInStock != 1)
            {
                return String.Format("У вас нет прав выставлять статус \"{0}\"",
                        TicketStatusesResources.Transfer_InStock);
            }

            if (cStatusId != 5 && nStatusId == 5 && currentRole.StatusProcessed != 1)
            {
                return String.Format("У вас нет прав выставлять статус \"{0}\"",
                        TicketStatusesResources.Processed);
            }

            if (cStatusId != 6 && nStatusId == 6 && currentRole.StatusCompleted != 1)
            {
                return String.Format("У вас нет прав выставлять статус \"{0}\"",
                        TicketStatusesResources.Completed);
            }

            if (cStatusId != 7 && nStatusId == 7 && currentRole.StatusRefusingInCourier != 1)
            {
                return String.Format("У вас нет прав выставлять статус \"{0}\"",
                        TicketStatusesResources.Refusing_InCourier);
            }

            if (cStatusId != 8 && nStatusId == 8 && currentRole.StatusReturnInStock != 1)
            {
                return String.Format("У вас нет прав выставлять статус \"{0}\"",
                        TicketStatusesResources.Return_InStock);
            }

            if (cStatusId != 9 && nStatusId == 9 && currentRole.StatusCancelInStock != 1)
            {
                return String.Format("У вас нет прав выставлять статус \"{0}\"",
                        TicketStatusesResources.Cancel_InStock);
            }

            if (cStatusId != 10 && nStatusId == 10 && currentRole.StatusCancel != 1)
            {
                return String.Format("У вас нет прав выставлять статус \"{0}\"",
                        TicketStatusesResources.Cancel);
            }

            if (cStatusId != 11 && nStatusId == 11 && currentRole.StatusTransferInCourier != 1)
            {
                return String.Format("У вас нет прав выставлять статус \"{0}\"",
                        TicketStatusesResources.Transfer_InCourier);
            }

            if (cStatusId != 12 && nStatusId == 12 && currentRole.StatusDelivered != 1)
            {
                return String.Format("У вас нет прав выставлять статус \"{0}\"",
                        TicketStatusesResources.Delivered);
            }

            if (cStatusId != 13 && nStatusId == 13 && currentRole.StatusExchangeInCourier != 1)
            {
                return String.Format("У вас нет прав выставлять статус \"{0}\"",
                        TicketStatusesResources.Exchange_InCourier);
            }

            if (cStatusId != 14 && nStatusId == 14 && currentRole.StatusDeliveryFromClientInCourier != 1)
            {
                return String.Format("У вас нет прав выставлять статус \"{0}\"",
                        TicketStatusesResources.DeliveryFromClient_InCourier);
            }

            if (cStatusId != 15 && nStatusId == 15 && currentRole.StatusExchangeInStock != 1)
            {
                return String.Format("У вас нет прав выставлять статус \"{0}\"",
                        TicketStatusesResources.Exchange_InStock);
            }

            if (cStatusId != 16 && nStatusId == 16 && currentRole.StatusDeliveryFromClientInStock != 1)
            {
                return String.Format("У вас нет прав выставлять статус \"{0}\"",
                        TicketStatusesResources.DeliveryFromClient_InStock);
            }

            if (cStatusId != 17 && nStatusId == 17 && currentRole.StatusRefusalOnTheWay != 1)
            {
                return String.Format("У вас нет прав выставлять статус \"{0}\"",
                        TicketStatusesResources.Refusal_OnTheWay);
            }

            if (cStatusId != 18 && nStatusId == 18 && currentRole.StatusRefusalByAddress != 1)
            {
                return String.Format("У вас нет прав выставлять статус \"{0}\"",
                        TicketStatusesResources.Refusal_ByAddress);
            }

            if (cStatusId != 19 && nStatusId == 19 && currentRole.StatusUpload != 1)
            {
                return String.Format("У вас нет прав выставлять статус \"{0}\"",
                        TicketStatusesResources.Upload);
            }
            #endregion

            //если заявка изменяется из текущего статуса "Обработана" в новый статус, отличный от "Завершено", то проверяем не запрещено ли это пользователю правами
            if (cStatusId == 5 && nStatusId != 5 && nStatusId != 6 && currentRole.ActionDisallowDeliveredToCompletedStatus == 1)
            {
                return String.Format("У вас нет прав  изменять статусы заявок в статусе \"{1}\" на статусы отличные от статуса \"{0}\"!",
                        TicketStatusesResources.Completed,
                        TicketStatusesResources.Processed);
            }

            //если заявка изменяется из текущего статуса "Не обработана" в новый статусы "на складе" и "перенос на складе"
            if (cStatusId == 1 && nStatusId != 1 && (nStatusId == 2 || nStatusId == 4))
            {
                return String.Format("Нельзя изменять статусы заявок в статусе \"{0}\" на статусы \"{1}\" и \"{2}\"! Для перевода заявок на склад используйте печать чеков.",
                        TicketStatusesResources.NotProcessed,
                        TicketStatusesResources.InStock,
                        TicketStatusesResources.Transfer_InStock);
            }

            /*при изменении заявки без водителя на новый статус, отличный от ("Не обработан", "На складе", "Перенос (на складе)", "Отмена", "Отмена (на складе)",
             * "Завершено", "Обмен (на складе)", "Доставка от клиента (на складе)") возвращаем ошибку*/
            if (cDriverId == 0 && nStatusId != 1 && nStatusId != 2 && nStatusId != 3 && nStatusId != 4 && nStatusId != 6 && nStatusId != 9 && nStatusId != 10 && nStatusId != 15 && nStatusId != 16)
            {
                return "Не все статусы обновлены! У некоторых заявок отсутствует водитель!";
            }

            /*если водитель не назначен, то проверяем права на изменение заявки без водителя в новый статус "Завершено"*/
            if (cDriverId == 0 && nStatusId == 6 && cStatusId != 6 && currentRole.ActionAllowChangeInCompletedWithoutDriver != 1)
            {
                return String.Format("У вас нет прав изменять статусы заявок без водителя в статус \"{0}\"", TicketStatusesResources.Completed);
            }
            #endregion

            #region Присвоение нового и старого статуса заявке (если все проверки пройдены)
            //если текущий статус не равен новому - записываем изменения статуса
            if (cStatusId != nStatusId)
            {
                ticket.StatusIDOld = cStatusId;
                ticket.StatusID = nStatusId;
            }
            //note: при изменении статуса у новой заявки старый в логи не пишется, так как заявка создается уже со старым статусом "Не обработана"
            #endregion

            #region  Обновление связанных данных
            //если новая расшифровка статуса не пустая, то записываем её (работает только для заявок в определенных ниже статусах)
            if (!String.IsNullOrEmpty(nStatusDescription) && (nStatusId == 4 || nStatusId == 7 || nStatusId == 8 || nStatusId == 9 || nStatusId == 10 || nStatusId == 11))
            {
                ticket.StatusDescription = nStatusDescription;
            }

            /*если новая расшифровка всё еще пустая - записываем старую если она есть 
            *(нужно, так как при пакетном назначении заявкам расшифровки статуса не возможно получить старый статус каждой заявки в текстбокс)*/
            if (String.IsNullOrEmpty(nStatusDescription))
            {
                ticket.StatusDescription = cStatusDescription;
            }

            //если текущая дата приема на склад отсутствует и текущие статусы заявок "На складе" или "Перенос (на складе)", то записываем в дата приема текущее время
            if ((String.IsNullOrEmpty(cAdmissionDateString) || cAdmissionDateString == "01.01.0001 0:00:00")
                && (nStatusId == 2 || nStatusId == 4))
            {
                ticket.AdmissionDate = DateTime.Now;
            }

            //если новый статус "Перенос (на складе)" или "Перенос (у курьера)", то записываем новую дату переноса
            if (nStatusId == 4 || nStatusId == 11)
            {
                ticket.DeliveryDate = nDeliveryDate;
            }

            //если текущий статус изменен на новый "Завершено", то записываем дату завершения (дата перевода заявки в статус "Завершено")
            if (nStatusId == 6 && cStatusId != 6)
            {
                ticket.CompletedDate = DateTime.Now;
            }

            //если текущий статус изменен на новый "Обработано", то записываем дату обработки (дату перевода заявки в новый статус "Обработано")
            // и шлем письма, указанные в бекенде, если заявки перешли в "Обработанно" для определнного клиента (АГС)
            if (nStatusId == 5 && cStatusId != 5)
            {
                ticket.ProcessedDate = DateTime.Now;
                var tempTicket = new Tickets { ID = ticket.ID };
                tempTicket.GetById();
                if (tempTicket.UserID == 794)
                {
                    var emailIist = BackendHelper.TagToValue("ags_event_email_list").Split(new[] { ',' });
                    EmailMethods.MailSendHTML("АГС - новая заявка в статусе 'Обработано'", String.Format("АГС, заявка перешла в статус 'Обработано'<br/>" +
                                                                                                                        "ID заявки: <b>{0}</b>", tempTicket.SecureID), emailIist);
                }
            }

            //если текущий статус изменен на новый "Перенос (на складе)", то шлем письмо на заданые емейлы и обнуляем водителя
            if (nStatusId == 4 && cStatusId != 4)
            {
                var email = BackendHelper.TagToValue("transfer_in_stock_event_email");
                var tempTicket = new Tickets { ID = ticket.ID };
                tempTicket.GetById();
                EmailMethods.MailSendHTML("Новый перевод заявки в статус 'Перенос (на складе)'", String.Format("Новый перевод заявки в статус 'Перенос (на складе)'<br/>" +
                                                                                                                             "ID заявки: <b>{0}</b>", tempTicket.SecureID), email);
                ticket.DriverID = 0;
            }

            //если текущий статус "Не обработано" изменен на новый "На складе" или "Перенос (на складе)", и текущее время между 21.00 и 24.00, то шлем письмо на заданые емейлы
            var cHour = DateTime.Now.Hour;
            if ((nStatusId == 2 || nStatusId == 4) && cStatusId == 1 && cHour >= 21 && cHour <= 24)
            {
                var email = BackendHelper.TagToValue("late_entries_event_email");
                var tempTicket = new Tickets { ID = ticket.ID };
                tempTicket.GetById();
                EmailMethods.MailSendHTML("Поздний принос груза на склад", String.Format("Еще один груз принесли после 21.00<br/>" +
                                                                                                                             "ID заявки: <b>{0}</b><br/>" +
                                                                                                                             "ID пользователя: <b>{1}</b><br/>", tempTicket.SecureID, tempTicket.UserID), email);
            }

            //если текущий статус меняется на новый "В пути" или на "Отказ (у курьера)" - меняем привезенное бабло на 0 (для кассы)
            if (nStatusId == 3 || nStatusId == 7)
            {
                ticket.ReceivedEUR = 0;
                ticket.ReceivedBLR = 0;
                ticket.ReceivedUSD = 0;
                ticket.ReceivedRUR = 0;
            }

            //если заявка переходит из статусов "На складе" или "Не обработано" в статусы "Отмена" или "Отмена на складе"
            if ((cStatusId == 2 || cStatusId == 1) && (nStatusId == 9 || nStatusId == 10))
            {
                var tempTicket = new Tickets { ID = ticket.ID };
                tempTicket.GetById();
                string[] emailList = new string[2];
                var managerEmail = UsersHelper.GetUserAttachedManager(Convert.ToInt32(tempTicket.UserID)).Email;
                var salesManagerEmail = UsersHelper.GetUserAttachedSalesManager(Convert.ToInt32(tempTicket.UserID)).Email;
                if (!String.IsNullOrEmpty(managerEmail))
                    emailList[0] = managerEmail;
                if (!String.IsNullOrEmpty(salesManagerEmail))
                    emailList[1] = managerEmail;
                EmailMethods.MailSendHTML("Отмена заявки", String.Format("Заявка перешла в статус '{0}'<br/><br/>" +
                                                                                        "Заявка: <b>{1}</b> (ID: {2})<br/>" +
                                                                                        "Отправитель: <b>{3}</b> (UID: {4})<br/>" +
                                                                                        "Профиль: <b>{5}</b> тел.:{6}<br/>" +
                                                                                        "Город доставки: <b>{7}</b> (CID: {8})<br/>" +
                                                                                        "Получатель: <b>{9}</b> (тел: {10})<br/><br/>" +
                                                                                        "Причина отмены: <b>{11}</b><br/>",
                                                                                        OtherMethods.TicketStatusToText(tempTicket.StatusID.ToString()),
                                                                                        tempTicket.SecureID,
                                                                                        GoodsHelper.GoodsToString(tempTicket.FullSecureID),
                                                                                        UsersHelper.UserIDToFullName(tempTicket.UserID.ToString()),
                                                                                        tempTicket.UserID,
                                                                                        UsersProfilesHelper.UserProfileIDToFullFamilyOrCompanyname(tempTicket.UserProfileID.ToString()),
                                                                                        UsersProfilesHelper.UserProfileToPhone(tempTicket.UserProfileID.ToString()),
                                                                                        CityHelper.CityIDToCityNameWithotCustom(tempTicket.CityID.ToString()),
                                                                                        tempTicket.CityID,
                                                                                        tempTicket.RecipientFirstName + " " + tempTicket.RecipientLastName + " " + tempTicket.RecipientThirdName,
                                                                                        tempTicket.RecipientPhone + ";" + tempTicket.RecipientPhoneTwo,
                                                                                        tempTicket.StatusDescription
                                                                                        ), emailList);
            }

            //если заявка переходит из статуса "В пути" в статусы отличные от "Завершено", "Доставлено", "Обработано", "В пути"
            if (cStatusId == 3 && nStatusId != 6 && nStatusId != 12 && nStatusId != 5 && nStatusId != 3)
            {
                var tempTicket = new Tickets { ID = ticket.ID };
                tempTicket.GetById();
                string[] emailList = new string[2];
                var managerEmail = UsersHelper.GetUserAttachedManager(Convert.ToInt32(tempTicket.UserID)).Email;
                var salesManagerEmail = UsersHelper.GetUserAttachedSalesManager(Convert.ToInt32(tempTicket.UserID)).Email;
                if (!String.IsNullOrEmpty(managerEmail))
                    emailList[0] = managerEmail;
                if (!String.IsNullOrEmpty(salesManagerEmail))
                    emailList[1] = managerEmail;
                EmailMethods.MailSendHTML("Изменен статус заявки в пути", String.Format("Заявка в пути перешла в статус '{0}'<br/><br/>" +
                                                                                        "Заявка: <b>{1}</b> (ID: {2})<br/>" +
                                                                                        "Отправитель: <b>{3}</b> (UID: {4})<br/>" +
                                                                                        "Профиль: <b>{5}</b> тел.:{6}<br/>" +
                                                                                        "Город доставки: <b>{7}</b> (CID: {8})<br/>" +
                                                                                        "Получатель: <b>{9}</b> (тел: {10})<br/><br/>" +
                                                                                        "Курьер: <b>{11}</b><br/>",
                                                                                        OtherMethods.TicketStatusToText(tempTicket.StatusID.ToString()),
                                                                                        tempTicket.SecureID,
                                                                                        GoodsHelper.GoodsToString(tempTicket.FullSecureID),
                                                                                        UsersHelper.UserIDToFullName(tempTicket.UserID.ToString()),
                                                                                        tempTicket.UserID,
                                                                                        UsersProfilesHelper.UserProfileIDToFullFamilyOrCompanyname(tempTicket.UserProfileID.ToString()),
                                                                                        UsersProfilesHelper.UserProfileToPhone(tempTicket.UserProfileID.ToString()),
                                                                                        CityHelper.CityIDToCityNameWithotCustom(tempTicket.CityID.ToString()),
                                                                                        tempTicket.CityID,
                                                                                        tempTicket.RecipientFirstName + " " + tempTicket.RecipientLastName + " " + tempTicket.RecipientThirdName,
                                                                                        tempTicket.RecipientPhone + ";" + tempTicket.RecipientPhoneTwo,
                                                                                        DriversHelper.DriverIdToName(tempTicket.DriverID.ToString())
                                                                                        ), emailList);
            }

            //если текущий статус изменен на новый "Отказ (в пути)", то величину "За услугу (GruzobozCost)" делим попалам
            if (nStatusId == 17 && cStatusId != 17)
            {
                var tempTicket = new Tickets { ID = ticket.ID };
                tempTicket.GetById();
                if (tempTicket.GruzobozCost != null)
                    ticket.GruzobozCost = MoneyMethods.MoneyRounder100(Convert.ToDecimal(tempTicket.GruzobozCost / 2));
            }

            //фиксирование в заявке даты возврата, если заявка перешла из старого статуса в новый статус 8, 15, 16, 9
            if ((nStatusId == 8 && cStatusId != 8) || (nStatusId == 9 && cStatusId != 9) || (nStatusId == 15 && cStatusId != 15) || (nStatusId == 16 && cStatusId != 16))
            {
                ticket.ReturnDate = DateTime.Now;
            }
            #endregion

            return null; //возвращаем null, что равно отсутствию ошибок
        }

        public static String OverChangeFilter(ref Tickets ticket, string cDriverIdString, string cStatusIdString, string cStatusDescription, string cAdmissionDateString, string nStatusDescription, string nStatusIdString, string nDeliveryDate, string nDriverIdString, string oStatusIdString, Roles curentRole)
        {
            string result = null;
            var statusError = StatusChangeFilter(ref ticket, cDriverIdString, cStatusIdString, cStatusDescription, cAdmissionDateString,
                nStatusDescription, nStatusIdString, nDeliveryDate, curentRole);
            var driverError = DriverChangeFilter(ref ticket, cDriverIdString, cStatusIdString, oStatusIdString, nDriverIdString);
            if (statusError != null)
                result += String.Format("{0} (s)<br/>", statusError);
            if (driverError != null)
                result += String.Format("{0} (d)<br/>", driverError);
            return result; //тут возвращаем ошибку или null
        }
    }
}