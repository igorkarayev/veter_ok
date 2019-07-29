using Delivery.BLL.Helpers;
using Delivery.DAL.DataBaseObjects;
using System;
using System.Net;

namespace Delivery.BLL.StaticMethods
{
    public class LogsMethods
    {
        public static string TableNameToRuss(string tableName)
        {
            if (tableName == "tickets")
            {
                return "Заявкa";
            }

            if (tableName == "goods")
            {
                return "Груз";
            }

            if (tableName == "users")
            {
                return "Пользователь";
            }

            if (tableName == "usersprofiles")
            {
                return "Профиль";
            }

            if (tableName == "drivers")
            {
                return "Водители";
            }

            if (tableName == "cars")
            {
                return "Автомобили";
            }

            if (tableName == "feedback")
            {
                return "Обращения";
            }

            if (tableName == "warehouses")
            {
                return "Склады";
            }

            if (tableName == "category")
            {
                return "Категории";
            }

            return tableName;
        }



        public static string PropertyNameToRuss(string tableName, string propertyName)
        {
            #region таблица заявок
            if (tableName == "tickets")
            {
                if (propertyName == "StatusID")
                {
                    return "Статус";
                }

                if (propertyName == "StatusIDOld")
                {
                    return "Пред. статус";
                }

                if (propertyName == "DriverID")
                {
                    return "Водитель";
                }

                if (propertyName == "AssessedCost")
                {
                    return "Оцнчн. стоим.";
                }

                if (propertyName == "CityID")
                {
                    return "Город";
                }

                if (propertyName == "RecipientFirstName")
                {
                    return "Фам. получ.";
                }

                if (propertyName == "RecipientLastName")
                {
                    return "Имя получ.";
                }

                if (propertyName == "RecipientThirdName")
                {
                    return "Отч. получ.";
                }

                if (propertyName == "DeliveryCost")
                {
                    return "За доставку";
                }

                if (propertyName == "CourseRUR")
                {
                    return "Курс RUR";
                }

                if (propertyName == "CourseUSD")
                {
                    return "Курс USD";
                }

                if (propertyName == "CourseEUR")
                {
                    return "Курс EUR";
                }

                if (propertyName == "AgreedCost")
                {
                    return "Согл. стоимость";
                }

                if (propertyName == "BoxesNumber")
                {
                    return "Колич. коробок";
                }

                if (propertyName == "GruzobozCost")
                {
                    return "За услугу";
                }

                if (propertyName == "Note")
                {
                    return "Примечание";
                }

                if (propertyName == "PassportNumber")
                {
                    return "Номер пасп. получ.";
                }

                if (propertyName == "PassportSeria")
                {
                    return "Серия. пасп. получ.";
                }

                if (propertyName == "RecipientStreet")
                {
                    return "Улица получ.";
                }

                if (propertyName == "RecipientStreetNumber")
                {
                    return "№ дома получ.";
                }

                if (propertyName == "RecipientKorpus")
                {
                    return "Корпус получ.";
                }

                if (propertyName == "RecipientKvartira")
                {
                    return "Квартира получ.";
                }

                if (propertyName == "RecipientStreet")
                {
                    return "Улица получ.";
                }

                if (propertyName == "RecipientPhone")
                {
                    return "Конт.№1 получ.";
                }

                if (propertyName == "RecipientPhoneTwo")
                {
                    return "Конт.№2 получ.";
                }

                if (propertyName == "TrackIDUser")
                {
                    return "Направление";
                }

                if (propertyName == "StatusDescription")
                {
                    return "Расш. статуса";
                }

                if (propertyName == "AdmissionDate")
                {
                    return "Дата приема";
                }

                if (propertyName == "DeliveryDate")
                {
                    return "Дата отправки";
                }

                if (propertyName == "Comment")
                {
                    return "Комментарий";
                }

                if (propertyName == "PrintNakl")
                {
                    return "Печ. накл.";
                }

                if (propertyName == "PrintNaklInMap")
                {
                    return "Налич. накл.";
                }

                if (propertyName == "Weight")
                {
                    return "Вес";
                }

                if (propertyName == "CompletedDate")
                {
                    return "Дата завершения";
                }

                if (propertyName == "ReceivedUSD")
                {
                    return "Получено USD";
                }

                if (propertyName == "ReceivedEUR")
                {
                    return "Получено EUR";
                }

                if (propertyName == "ReceivedRUR")
                {
                    return "Получено RUR";
                }

                if (propertyName == "ReceivedBLR")
                {
                    return "Получено BLR";
                }

                if (propertyName == "IsExchange")
                {
                    return "Обратка";
                }

                if (propertyName == "WithoutMoney")
                {
                    return "Без оплаты";
                }

                if (propertyName == "ProcessedDate")
                {
                    return "Дата обрабоки";
                }

                if (propertyName == "CheckedOut")
                {
                    return "Проверено";
                }

                if (propertyName == "Phoned")
                {
                    return "Дозвонились";
                }

                if (propertyName == "Billed")
                {
                    return "Счет выставлен";
                }

                if (propertyName == "OvDateFrom")
                {
                    return "Время доставки с";
                }

                if (propertyName == "OvDateTo")
                {
                    return "Время доставки по";
                }
            }
            #endregion

            #region таблица грузов
            if (tableName == "goods")
            {
                if (propertyName == "Description")
                {
                    return "Наименование";
                }

                if (propertyName == "Model")
                {
                    return "Модель";
                }

                if (propertyName == "Number")
                {
                    return "Штук груза";
                }

                if (propertyName == "Cost")
                {
                    return "Стмст. груза";
                }

                if (propertyName == "WithoutAkciza")
                {
                    return "БУ";
                }
            }
            #endregion

            #region таблица пользователей
            if (tableName == "users")
            {
                if (propertyName == "Name")
                {
                    return "Имя";
                }

                if (propertyName == "SpecialClient")
                {
                    return "Спец. клиент";
                }

                if (propertyName == "Status")
                {
                    return "Статус";
                }

                if (propertyName == "Phone")
                {
                    return "Телефон";
                }

                if (propertyName == "Family")
                {
                    return "Фамилия";
                }

                if (propertyName == "IsCourse")
                {
                    return "Может выст. курсы";
                }

                if (propertyName == "Discount")
                {
                    return "Скидка";
                }

                if (propertyName == "Password")
                {
                    return "Пароль";
                }

                if (propertyName == "Login")
                {
                    return "Логин";
                }

                if (propertyName == "Email")
                {
                    return "E-mail";
                }

                if (propertyName == "Role")
                {
                    return "Роль";
                }

                if (propertyName == "BirthDay")
                {
                    return "День рождения";
                }

                if (propertyName == "Validity")
                {
                    return "Срок действ. пасп.";
                }

                if (propertyName == "RegistrationAddress")
                {
                    return "Прописка.";
                }

                if (propertyName == "ROVD")
                {
                    return "Орган, выдавший пасп.";
                }

                if (propertyName == "PersonalNumber")
                {
                    return "Личный код";
                }

                if (propertyName == "PassportNumber")
                {
                    return "Номер паспорта";
                }

                if (propertyName == "PassportSeria")
                {
                    return "Серия паспорта";
                }

                if (propertyName == "DateOfIssue")
                {
                    return "Дата выдачи пас.";
                }

                if (propertyName == "ApiKey")
                {
                    return "Ключ API";
                }

                if (propertyName == "AllowApi")
                {
                    return "Разрешить API";
                }
            }
            #endregion

            #region таблица профилей
            if (tableName == "usersprofiles")
            {
                if (propertyName == "ContactPersonFIO")
                {
                    return "ФИО контактного лица";
                }

                if (propertyName == "RejectBlockedMessage")
                {
                    return "Пояснение к статусу";
                }

                if (propertyName == "ContactPhoneNumbers")
                {
                    return "Контактные телефоны";
                }

                if (propertyName == "Address")
                {
                    return "Адрес";
                }

                if (propertyName == "PassportDate")
                {
                    return "Дата выдачи пасп.";
                }

                if (propertyName == "PassportData")
                {
                    return "Кем выдан";
                }

                if (propertyName == "PassportSeria")
                {
                    return "Серия паспорта";
                }

                if (propertyName == "PassportNumber")
                {
                    return "Номер паспорта";
                }

                if (propertyName == "ThirdName")
                {
                    return "Отчество";
                }

                if (propertyName == "LastName")
                {
                    return "Имя";
                }

                if (propertyName == "FirstName")
                {
                    return "Фамилия";
                }

                if (propertyName == "BankCode")
                {
                    return "Код банка";
                }

                if (propertyName == "BankAddress")
                {
                    return "Адрес банка";
                }

                if (propertyName == "BankName")
                {
                    return "Имя банка";
                }

                if (propertyName == "UNP")
                {
                    return "УНП";
                }

                if (propertyName == "RasShet")
                {
                    return "Рас.счет";
                }

                if (propertyName == "CompanyAddress")
                {
                    return "Адрес компании";
                }

                if (propertyName == "DirectorPhoneNumber")
                {
                    return "Телефон директора";
                }

                if (propertyName == "StatusID")
                {
                    return "Статус";
                }
            }
            #endregion

            #region таблица водителей
            if (tableName == "drivers")
            {
                if (propertyName == "StatusID")
                {
                    return "Статус";
                }

                if (propertyName == "FirstName")
                {
                    return "Фамилия";
                }

                if (propertyName == "LastName")
                {
                    return "Имя";
                }

                if (propertyName == "PhoneTwo")
                {
                    return "Телефон #2";
                }

                if (propertyName == "PhoneOne")
                {
                    return "Телефон #1";
                }

                if (propertyName == "DriverPassport")
                {
                    return "Номер ВУ";
                }

                if (propertyName == "ThirdName")
                {
                    return "Отчество";
                }

                if (propertyName == "CarID")
                {
                    return "ID авто";
                }

                if (propertyName == "MedPolisValidity")
                {
                    return "Мед. спр. действительна до";
                }

                if (propertyName == "MedPolisDateOfIssue")
                {
                    return "Медс. спр. дата выдачи";
                }

                if (propertyName == "DriverPassportValidity")
                {
                    return "ВУ действительно до";
                }

                if (propertyName == "DriverPassportDateOfIssue")
                {
                    return "ВУ дата выдачи";
                }

                if (propertyName == "DateOfIssue")
                {
                    return "Дата выдачи паспорта";
                }

                if (propertyName == "BirthDay")
                {
                    return "День рождения";
                }

                if (propertyName == "Validity")
                {
                    return "Паспорт действителен до";
                }

                if (propertyName == "ContactPersonFIO")
                {
                    return "ФИО конт. лица";
                }

                if (propertyName == "ContactPersonPhone")
                {
                    return "Телефон конт. лица";
                }

                if (propertyName == "HomePhone")
                {
                    return "Домашний телефон";
                }

                if (propertyName == "HomeAddress")
                {
                    return "Домашний адрес";
                }

                if (propertyName == "RegistrationAddress")
                {
                    return "Прописка";
                }

                if (propertyName == "ROVD")
                {
                    return "Выдавший паспорт орган";
                }

                if (propertyName == "PersonalNumber")
                {
                    return "Личный код";
                }

                if (propertyName == "PassportNumber")
                {
                    return "Номер паспорта";
                }

                if (propertyName == "PassportSeria")
                {
                    return "Серия паспорта";
                }
            }
            #endregion

            #region таблица авто
            if (tableName == "cars")
            {
                if (propertyName == "TypeID")
                {
                    return "Тип владельца";
                }

                if (propertyName == "Model")
                {
                    return "Модель";
                }

                if (propertyName == "Number")
                {
                    return "Номер";
                }
            }
            #endregion

            #region таблица обращений
            if (tableName == "feedback")
            {
                if (propertyName == "StatusID")
                {
                    return "Статус";
                }
            }
            #endregion

            #region таблица складов
            if (tableName == "warehouses")
            {
                if (propertyName == "ApartmentNumber")
                {
                    return "Квартира";
                }

                if (propertyName == "Housing")
                {
                    return "Корпус";
                }

                if (propertyName == "StreetNumber")
                {
                    return "Номер дома";
                }

                if (propertyName == "StreetName")
                {
                    return "Название улицы";
                }

                if (propertyName == "StreetPrefix")
                {
                    return "Префикс улицы";
                }

                if (propertyName == "Name")
                {
                    return "Имя";
                }

                if (propertyName == "CityID")
                {
                    return "ID нас. пункта";
                }
            }
            #endregion

            #region таблица категорий
            if (tableName == "category")
            {
                if (propertyName == "Name")
                {
                    return "Название категории";
                }
            }
            #endregion

            return propertyName;
        }

        public static string OldNewValueToRuss(string tableName, string propertyName, string value)
        {
            #region таблица заявок
            if (tableName == "tickets")
            {
                if (propertyName == "CourseRUR" ||
                    propertyName == "CourseUSD" ||
                    propertyName == "CourseEUR" ||
                    propertyName == "DeliveryCost" ||
                    propertyName == "AssessedCost" ||
                    propertyName == "AgreedCost" ||
                    propertyName == "ReceivedUSD" ||
                    propertyName == "ReceivedEUR" ||
                    propertyName == "ReceivedEUR" ||
                    propertyName == "ReceivedBLR" ||
                    propertyName == "GruzobozCost")
                {
                    return MoneyMethods.MoneySeparator(value);
                }

                if (propertyName == "DriverID")
                {
                    return DriversHelper.DriverIDToFioToPrint(value);
                }

                if (propertyName == "StatusIDOld" || propertyName == "StatusID")
                {
                    return OtherMethods.TicketStatusToText(value);
                }

                if (propertyName == "CityID")
                {
                    return CityHelper.CityIDToCityNameWithotCustom(value);
                }

                if (propertyName == "TrackIDUser")
                {
                    return OtherMethods.TrackToText(Convert.ToInt32(value));
                }

                if (propertyName == "DeliveryDate")
                {
                    return value.Remove(value.Length - 8);
                }

                if (propertyName == "Comment")
                {
                    return WebUtility.HtmlDecode(value);
                }

                if (propertyName == "PrintNakl"
                    || propertyName == "PrintNaklInMap"
                    || propertyName == "IsExchange"
                    || propertyName == "WithoutMoney"
                    || propertyName == "CheckedOut"
                    || propertyName == "Phoned")
                {
                    return value == "0" ? "нет" : "да";
                }

                if (propertyName == "OvDateFrom" || propertyName == "OvDateTo")
                {
                    return String.IsNullOrEmpty(value) ? String.Empty : Convert.ToDateTime(value).ToString("HH:mm");
                }
            }
            #endregion

            #region таблица пользователей
            if (tableName == "users")
            {
                if (propertyName == "SpecialClient" || propertyName == "IsCourse" || propertyName == "AllowApi")
                {
                    return value == "0" ? "нет" : "да";
                }

                if (propertyName == "Status")
                {
                    return UsersHelper.UserStatusToText(Convert.ToInt32(value));
                }

                if (propertyName == "Role")
                {
                    return UsersHelper.RoleToRuss(value);
                }

                if (propertyName == "Discount")
                {
                    return String.Format("{0}%", value);
                }

                if (propertyName == "Password")
                {
                    return String.Empty;
                }

                if (propertyName == "Validity" && value.Length >= 8)
                {
                    return value.Remove(value.Length - 8);
                }

                if (propertyName == "BirthDay" && value.Length >= 8)
                {
                    return value.Remove(value.Length - 8);
                }

                if (propertyName == "DateOfIssue" && value.Length >= 8)
                {
                    return value.Remove(value.Length - 8);
                }
            }
            #endregion

            #region таблица грузов
            if (tableName == "goods")
            {
                if (propertyName == "Cost")
                {
                    return MoneyMethods.MoneySeparator(value);
                }

                if (propertyName == "WithoutAkciza")
                {
                    return value == "0" ? "нет" : "да";
                }
            }
            #endregion

            #region таблица профилей
            if (tableName == "usersprofiles")
            {
                if (propertyName == "StatusID")
                {
                    return UsersProfilesHelper.UserProfileStatusToText(Convert.ToInt32(value));
                }
            }
            #endregion

            #region таблица водителй
            if (tableName == "drivers")
            {
                if (propertyName == "StatusID" && !String.IsNullOrEmpty(value))
                {
                    return DriversHelper.DriverStatusToText(Convert.ToInt32(value));
                }

                if (value.Length >= 8 && (propertyName == "DateOfIssue"
                    || propertyName == "Validity"
                    || propertyName == "BirthDay"
                    || propertyName == "DriverPassportDateOfIssue"
                    || propertyName == "DriverPassportValidity"
                    || propertyName == "MedPolisDateOfIssue"
                    || propertyName == "MedPolisValidity"))
                {
                    return value.Remove(value.Length - 8);
                }
            }
            #endregion

            #region таблица авто
            if (tableName == "cars")
            {
                if (propertyName == "TypeID" && !String.IsNullOrEmpty(value))
                {
                    return CarsHelper.CarTypeToFullString(Convert.ToInt32(value));
                }
            }
            #endregion

            #region таблица категорий
            if (tableName == "category")
            {
                if (propertyName == "Name")
                {
                    return value;
                }
            }
            #endregion

            return value;
        }

        public static string FieldIDToTicketSecureID(string tableName, int fieldID, bool link, string fullsecureid, string userID)
        {
            if (tableName == "tickets")
            {
                if (!String.IsNullOrEmpty(fullsecureid))
                {
                    return link ? "~/ManagerUI/Menu/Tickets/UserTicketEdit.aspx?id=" + fullsecureid : fullsecureid.Remove(7) + "<br/><span style=\"font-size: 10px; font-style: italic; margin-left: 5px;  color: black;\">tid: " + fieldID + "<br> uid: " + userID + "</span>";
                }
                else
                {
                    var tickets = new Tickets { ID = fieldID };
                    tickets.GetById();
                    return link ? "~/ManagerUI/Menu/Tickets/UserTicketEdit.aspx?id=" + tickets.SecureID : tickets.SecureID + "<br/><span style=\"font-size: 10px; font-style: italic; margin-left: 5px;  color: black;\">tid: " + fieldID + "<br> uid: " + userID + "</span>";
                }
            }

            if (tableName == "goods")
            {
                return link ? "~/ManagerUI/Menu/Tickets/UserTicketEdit.aspx?id=" + fullsecureid : fullsecureid.Remove(7) + "<br/><span style=\"font-size: 10px; font-style: italic; margin-left: 5px; color: black;\">tid: " + fieldID + "</span>";
            }

            if (tableName == "usersprofiles")
            {
                return link ? "#" : fieldID + "<br><span style=\"font-size: 10px; font-style: italic; margin-left: 5px; color: black;\"> uid: " + userID + "</span>";
            }

            if (tableName == "drivers")
            {
                return link ? "~/ManagerUI/Menu/Souls/DriversEdit.aspx?id=" + fieldID : fieldID.ToString();
            }

            if (tableName == "cars")
            {
                return link ? "~/ManagerUI/Menu/Souls/CarEdit.aspx?id=" + fieldID : fieldID.ToString();
            }

            if (tableName == "feedback")
            {
                return link ? "~/ManagerUI/Menu/Souls/FeedbackView.aspx?id=" + fieldID : fieldID.ToString();
            }

            return link ? String.Empty : fieldID.ToString();
        }


        public static string MethodNameToRuss(string methodName)
        {
            if (methodName == "Update")
            {
                return "Обновление";
            }

            if (methodName == "Create")
            {
                return "Создание";
            }

            if (methodName == "Delete")
            {
                return "Удаление";
            }

            return methodName;
        }


        public static string PageNameToRuss(string pageName, string fieldID)
        {
            if (pageName == "UserTicketEdit")
            {
                if (!String.IsNullOrEmpty(fieldID))
                {
                    var tickets = new Tickets { ID = Convert.ToInt32(fieldID) };
                    tickets.GetById();
                    return
                        "<span style=\"font-size: 10px; font-style: italic; margin-left: 5px; color: green;\">м </span><a href=\"../ManagerUI/Menu/Tickets/UserTicketEdit.aspx?id=" +
                        tickets.FullSecureID + "\" target = \"_new\">Ред. заявки</a>";
                }
                else
                {
                    return "Ред. заявки";
                }
            }

            if (pageName == "UserTicketView")
            {
                return !String.IsNullOrEmpty(fieldID) ? "<span style=\"font-size: 10px; font-style: italic; margin-left: 5px; color: green;\">м </span><a href=\"../ManagerUI/Menu/Tickets/UserTicketView.aspx\" target = \"_new\">Все заявки</a>" : "Все заявки";
            }

            if (pageName == "UserTicketViewMy")
            {
                return !String.IsNullOrEmpty(fieldID) ? "<span style=\"font-size: 10px; font-style: italic; margin-left: 5px; color: green;\">м </span><a href=\"../ManagerUI/Menu/Tickets/UserTicketViewMy.aspx\" target = \"_new\">Мои заявки</a>" : "Мои заявки";
            }

            if (pageName == "TicketView")
            {
                return !String.IsNullOrEmpty(fieldID) ? "<span style=\"font-size: 10px; font-style: italic; margin-left: 5px; color: blue;\">ю </span><a href=\"../UserUI/TicketView.aspx\" target = \"_new\">Все заявки</a>" : "Все заявки";
            }

            if (pageName == "ProfilesView")
            {
                return !String.IsNullOrEmpty(fieldID) ? "<span style=\"font-size: 10px; font-style: italic; margin-left: 5px; color: blue;\">ю </span><a href=\"../UserUI/ProfilesView.aspx\" target = \"_new\">Все профили</a>" : "Все профили";
            }

            if (pageName == "UserTicketNotProcessedView")
            {
                return !String.IsNullOrEmpty(fieldID) ? "<span style=\"font-size: 10px; font-style: italic; margin-left: 5px; color: green;\">м </span><a href=\"../ManagerUI/Menu/Tickets/UserTicketNotProcessedView.aspx\" target = \"_new\">Необработанные</a>" : "Необработанные";
            }

            if (pageName == "UserTicketByDeliveryDateView")
            {
                return !String.IsNullOrEmpty(fieldID) ? "<span style=\"font-size: 10px; font-style: italic; margin-left: 5px; color: green;\">м </span><a href=\"../ManagerUI/Menu/Tickets/ManagerUI/Menu/Souls/UserTicketByDeliveryDateView.aspx\" target = \"_new\">Едут завтра</a>" : "Едут завтра";
            }

            if (pageName == "UserTicketByDeliveryDateViewTwo")
            {
                return !String.IsNullOrEmpty(fieldID) ? "<span style=\"font-size: 10px; font-style: italic; margin-left: 5px; color: green;\">м </span><a href=\"../ManagerUI/Menu/Tickets/UserTicketByDeliveryDateViewTwo.aspx\" target = \"_new\">Едут послезавтра итд</a>" : "Едут послезавтра итд";
            }

            if (pageName == "MoneyView")
            {
                return !String.IsNullOrEmpty(fieldID) ? "<span style=\"font-size: 10px; font-style: italic; margin-left: 5px; color: green;\">м </span><a href=\"../ManagerUI/Menu/Tickets/MoneyView.aspx\" target = \"_new\">Приемка</a>" : "Приемка";
            }

            if (pageName == "PrintCheck")
            {
                return !String.IsNullOrEmpty(fieldID) ? "<span style=\"font-size: 10px; font-style: italic; margin-left: 5px; color: black;\">п </span>Печать чеков" : "Печать чеков";
            }

            if (pageName == "ClientEdit")
            {
                return !String.IsNullOrEmpty(fieldID) ? String.Format("<span style=\"font-size: 10px; font-style: italic; margin-left: 5px; color: green;\">м </span><a href=\"../ManagerUI/Menu/Souls/ClientEdit.aspx?id={0}\" target = \"_new\">Ред. клиента</a>", fieldID) : "Ред. клиента";
            }

            if (pageName == "UsersDiscountView")
            {
                return !String.IsNullOrEmpty(fieldID) ? "<span style=\"font-size: 10px; font-style: italic; margin-left: 5px; color: green;\">м </span><a href=\"../ManagerUI/Menu/Souls/UsersDiscountView.aspx\" target = \"_new\">Скидки</a>" : "Скидки";
            }

            if (pageName == "ChangePasswords")
            {
                return !String.IsNullOrEmpty(fieldID) ? "<span style=\"font-size: 10px; font-style: italic; margin-left: 5px; color: green;\">м </span><a href=\"../ManagerUI/Menu/Souls/ChangePasswords.aspx\" target = \"_new\">Пароли</a>" : "Пароли";
            }

            if (pageName == "ClientsView")
            {
                return !String.IsNullOrEmpty(fieldID) ? "<span style=\"font-size: 10px; font-style: italic; margin-left: 5px; color: green;\">м </span><a href=\"../ManagerUI/Menu/Souls/ClientsView.aspx\" target = \"_new\">Клиенты</a>" : "Клиенты";
            }

            if (pageName == "ManagerEdit")
            {
                return !String.IsNullOrEmpty(fieldID) ? String.Format("<span style=\"font-size: 10px; font-style: italic; margin-left: 5px; color: green;\">м </span><a href=\"../ManagerUI/Menu/Souls/ManagerEdit.aspx?id={0}\" target = \"_new\">Ред. сотрудника</a>", fieldID) : "Ред. сотрудника";
            }

            if (pageName == "ManagersView")
            {
                return !String.IsNullOrEmpty(fieldID) ? "<span style=\"font-size: 10px; font-style: italic; margin-left: 5px; color: green;\">м </span><a href=\"../ManagerUI/Menu/Souls/ManagersView.aspx\" target = \"_new\">Сотрудники</a>" : "Сотрудники";
            }

            if (pageName == "ProfileEdit")
            {
                return !String.IsNullOrEmpty(fieldID) ? String.Format("<span style=\"font-size: 10px; font-style: italic; margin-left: 5px; color: green;\">м </span><a href=\"../ManagerUI/Menu/Souls/ProfileEdit.aspx?id={0}\" target = \"_new\">Ред. профиля</a>", fieldID) : "Ред. профиля";
            }

            if (pageName == "DriversEdit")
            {
                return !String.IsNullOrEmpty(fieldID) ? String.Format("<span style=\"font-size: 10px; font-style: italic; margin-left: 5px; color: green;\">м </span><a href=\"../ManagerUI/Menu/Souls/DriversEdit.aspx?id={0}\" target = \"_new\">Ред. водителя</a>", fieldID) : "Ред. водителя";
            }

            if (pageName == "DriversView")
            {
                return !String.IsNullOrEmpty(fieldID) ? "<span style=\"font-size: 10px; font-style: italic; margin-left: 5px; color: green;\">м </span><a href=\"../ManagerUI/Menu/Souls/DriversView.aspx\" target = \"_new\">Водители</a>" : "Водители";
            }

            if (pageName == "CarEdit")
            {
                return !String.IsNullOrEmpty(fieldID) ? String.Format("<span style=\"font-size: 10px; font-style: italic; margin-left: 5px; color: green;\">м </span><a href=\"../ManagerUI/Menu/Souls/CarEdit.aspx?id={0}\" target = \"_new\">Ред. авто</a>", fieldID) : "Ред. авто";
            }

            if (pageName == "CarsView")
            {
                return !String.IsNullOrEmpty(fieldID) ? "<span style=\"font-size: 10px; font-style: italic; margin-left: 5px; color: green;\">м </span><a href=\"../ManagerUI/Menu/Souls/CarsView.aspx\" target = \"_new\">Автомобили</a>" : "Автомобили";
            }

            if (pageName == "FeedbackView")
            {
                return !String.IsNullOrEmpty(fieldID) ?
                    String.Format(
                    "<span style=\"font-size: 10px; font-style: italic; margin-left: 5px; color: green;\">м </span><a href=\"../ManagerUI/Menu/Souls/FeedbackView.aspx?id={0}\" target = \"_new\">Ред. обращения</a>", fieldID) : "Ред. обращения";
            }

            if (pageName == "WarehouseEdit")
            {
                return !String.IsNullOrEmpty(fieldID) ? String.Format("<span style=\"font-size: 10px; font-style: italic; margin-left: 5px; color: green;\">м </span><a href=\"../ManagerUI/Menu/Souls/WarehouseEdit.aspx?id={0}\" target = \"_new\">Ред. склада</a>", fieldID) : "Ред. склада";
            }

            if (pageName == "CateegoryView")
            {
                return !String.IsNullOrEmpty(fieldID) ? "<span style=\"font-size: 10px; font-style: italic; margin-left: 5px; color: green;\">м </span><a href=\"../ManagerUI/Menu/Souls/CategoryView.aspx\" target = \"_new\">Категории</a>" : "Категории";
            }

            if (pageName == "CateegoryEdit")
            {
                return !String.IsNullOrEmpty(fieldID) ? String.Format("<span style=\"font-size: 10px; font-style: italic; margin-left: 5px; color: green;\">м </span><a href=\"../ManagerUI/Menu/Souls/CategoryEdit.aspx?id={0}\" target = \"_new\">Ред. категорию</a>", fieldID) : "Ред. категорию";
            }

            return pageName;
        }
    }
}