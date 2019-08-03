using Delivery.BLL;
using Delivery.BLL.Helpers;
using Delivery.BLL.StaticMethods;
using Delivery.DAL;
using Delivery.DAL.DataBaseObjects;
using Delivery.WebServices.Objects;
using DeliverySite.WebServices.PublicAPI;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;

namespace DeliverySite.BLL.StaticMethods
{
    public class TicketsForAPI
    {
        DataTable selectProfilesDataSet;
        DataManager dm = new DataManager();
        List<string> RecipientStreetPrefixes;
        DataTable warehouses = new DataTable();
        string patternCost = @"^\d+,\d{2}$";
        string patternPhone = @"^\+\d{3} [(]{1}\d{2}[)] \d{3}-\d{2}-\d{2}$";
        string patternDate = @"\d{2}.\d{2}.\d{4}$";
        string patternTTNSeria = @"^[a-zA-Zа-яА-Я]{2}$";
        string patternTTNNumber = @"^\d{7}$";

        List<string> rowsError = new List<string>();

        public TicketsForAPI()
        {            
            warehouses = dm.QueryWithReturnDataSet("SELECT * FROM `warehouses`").Tables[0];
            RecipientStreetPrefixes = new List<string>()
            { "ул.", "аллея", "бул.", "дор.",
                "линия", "маг.", "мик-н", "наб.", "пер.",
                "пл.", "пр.", "пр-кт", "ряд", "тракт", "туп.", "ш." };
        }

        public List<TicketsCreateResult> CreateTickets(List<TicketToCreate> tickets, int id)
        {
            bool canCreateProdut = false;
            List<TicketsCreateResult> result = new List<TicketsCreateResult>();
            selectProfilesDataSet = dm.QueryWithReturnDataSet(string.Format("SELECT * FROM `usersprofiles` WHERE `UserID` = {0} AND `StatusID` = 1", id)).Tables[0];
            ////////////////

            string userDiscount;
            int userID;

            int rowsCount = 0;
            int rowsSuccessCount = 0;

            var user = new Users
            {
                ID = id
            };
            user.GetById();

            userID = user.ID;

            if (user.Role != Users.Roles.User.ToString())
            {
                userDiscount = "0";
                userID = 1;
            }
            else
            {
                userDiscount = user.Discount.ToString();
            }

            canCreateProdut = user.CreateProduct == 1;

            int num = 0;
            foreach (TicketToCreate row in tickets)
            {
                bool norm = true;
                List<string> errors = new List<string>();
                Tickets ticket = new Tickets();
                int? profileType = null;

                TicketsCreateResult resultTicket = new TicketsCreateResult();
                resultTicket.number = num.ToString();
                num++;

                /// проверка профиля
                foreach (DataRow rowProfile in selectProfilesDataSet.Rows)
                {
                    string selectProfileName = String.IsNullOrEmpty(rowProfile["CompanyName"].ToString())
                        ? String.Format("{0} {1}", rowProfile["FirstName"], rowProfile["LastName"])
                        : rowProfile["CompanyName"].ToString();
                    
                    if (selectProfileName == row.ProfileName)
                    {
                        ticket.UserProfileID = Convert.ToInt32(rowProfile["ID"].ToString());
                        ticket.UserID =
                            UsersHelper.GetUserIdByUserProfileId(Convert.ToInt32(ticket.UserProfileID));

                        profileType = Convert.ToInt32(rowProfile["TypeID"].ToString());
                        if (profileType == 2)
                        {
                            ticket.PrintNaklInMap = 1;
                            ticket.PrintNakl = 0;
                        }
                        else
                        {
                            ticket.PrintNaklInMap = 0;
                            ticket.PrintNakl = 1;
                        }

                        break;
                    }
                }
                if (ticket.UserProfileID == null)
                {
                    norm = false;
                    errors.Add("неверный профиль");
                }
                /// проверка ID города
                ///   
                int cityId;
                if (Int32.TryParse(row.CityID, out cityId) != false)
                {
                    if (CityHelper.CityIDToCityName(row.CityID.ToString()) != null)
                    {
                        ticket.CityID = Convert.ToInt32(row.CityID.ToString());
                    }
                    else
                    {
                        norm = false;
                        errors.Add("неверный ID города");
                    }
                }
                else
                {
                    norm = false;
                    errors.Add("неверный ID города");
                }
                ///
                /// Проверка типа улицы
                if (row.StreetPrefix == null || RecipientStreetPrefixes.Contains(row.StreetPrefix))
                {
                    ticket.RecipientStreetPrefix = row.StreetPrefix;
                }
                else
                {
                    norm = false;
                    errors.Add("неверный префикс улицы");
                }

                ///
                /// Проверка района
                if (row.StreetName != string.Empty)
                    ticket.RecipientStreet = row.StreetName;
                else
                {
                    norm = false;
                    errors.Add("не введена улица");
                }
                ///
                ///
                int houseNumber;
                if (Int32.TryParse(row.HouseNumber, out houseNumber))
                {
                    ticket.RecipientStreetNumber = houseNumber.ToString();
                }
                else
                {
                    norm = false;
                    errors.Add("неверный номер дома");
                }
                /// Заполнение корпуса и квартиры
                if (row.Korpus != string.Empty)
                    ticket.RecipientKorpus = row.Korpus;
                if (row.Kvartira != string.Empty)
                    ticket.RecipientKvartira = row.Kvartira;
                ///
                /// Проверка фамилии, имени и отчества
                if (row.FirstName == null || row.SecondName == null || row.ThirdName == null 
                    || row.FirstName.Length < 2 || row.SecondName.Length < 2 || row.ThirdName.Length < 2)
                {
                    norm = false;
                    errors.Add("неверные ФИО");
                }
                else
                {
                    ticket.RecipientFirstName = row.FirstName;
                    ticket.RecipientLastName = row.SecondName;
                    ticket.RecipientThirdName = row.ThirdName;
                }
                ///
                /// Проверка номеров телефона
                string input1 = row.FirstTelefonNumber;
                string input2 = row.SecondTelefonNumber;

                int numMatches1 = 0;
                if (input1 != null)
                    foreach (Match m in Regex.Matches(input1, patternPhone))
                        numMatches1++;
                int numMatches2 = 0;
                if (input2 != null)
                    foreach (Match m in Regex.Matches(input2, patternPhone))
                        numMatches2++;

                if (row.FirstTelefonNumber == string.Empty || numMatches1 != 1)
                {
                    norm = false;
                    errors.Add("неверный 1 номер телефона");
                }
                else
                {
                    ticket.RecipientPhone = row.FirstTelefonNumber;
                }

                if (row.SecondTelefonNumber != string.Empty && numMatches2 != 1)
                {
                    norm = false;
                    errors.Add("неверный 2 номер телефона");
                }
                else
                {
                    if (row.SecondTelefonNumber != string.Empty)
                        ticket.RecipientPhoneTwo = row.SecondTelefonNumber;
                }
                ///
                /// Проверка стоимости доставки для получателя
                string patternCost = @"^\d+,\d{2}$";

                if (row.RecieverCost != string.Empty)
                {
                    //if (Regex.Matches(row.ItemArray[12].ToString(), patternCost).Count == 1)
                    try
                    {
                        ticket.DeliveryCost = Convert.ToDecimal(row.RecieverCost);
                    }
                    catch
                    {
                        norm = false;
                        errors.Add("неверная стоимость для получателя");
                    }
                }
                else
                    ticket.DeliveryCost = 0;
                ///
                /// Проверка товаров
                List<Goods> goods = new List<Goods>();
                if (row.Goods == null || row.Goods.Count == 0)
                {
                    norm = false;
                    errors.Add("не введены товары");
                }
                else
                {
                    foreach (TicketGood good in row.Goods)
                    {
                        if (string.IsNullOrEmpty(good.GoodName) || string.IsNullOrEmpty(good.GoodModel) || string.IsNullOrEmpty(good.GoodCount) || string.IsNullOrEmpty(good.GoodCost))
                        {
                            norm = false;
                            errors.Add("неверно заполнены товары");
                            goods = new List<Goods>();
                            break;
                        }
                        string goodDesc = good.GoodName;
                        string goodModel = good.GoodModel;
                        string goodCost = good.GoodCost;
                        string goodCount = good.GoodCount;
                        ///
                        var titleForCheck = new Titles();
                        titleForCheck.Name = goodDesc.Trim();
                        titleForCheck.GetByName();
                        if (titleForCheck.ID == 0 && !canCreateProdut)
                        {
                            norm = false;
                            errors.Add("неверно заполнены товары: товар не найден");
                            goods = new List<Goods>();
                            break;
                        }

                        int count;
                        if (Int32.TryParse(goodCount, out count) == false)
                        {
                            norm = false;
                            errors.Add("неверно заполнены товары: не верное количество");
                            goods = new List<Goods>();
                            break;
                        }

                        if (Regex.Matches(goodCost, patternCost).Count != 1)
                        {
                            norm = false;
                            errors.Add("неверно заполнены товары: не верная цена");
                            goods = new List<Goods>();
                            break;
                        }

                        Goods goodCreate = new Goods()
                        {
                            Description = OtherMethods.BeInUseReplace(goodDesc).Trim(),
                            Model = goodModel.Trim(),
                            Number = Convert.ToInt32(goodCount),
                            Cost = Convert.ToDecimal(goodCost)
                        };
                        goods.Add(goodCreate);
                    }
                    ///
                    /// Получение стоимости за товары
                    ticket.AssessedCost = 0;
                    foreach (Goods good in goods)
                    {
                        ticket.AssessedCost += good.Number * good.Cost;
                    }
                    ///
                    /// Проверка количества грузовых мест
                    int boxesNumber;
                    if (row.BoxCount != string.Empty)
                    {
                        if (Int32.TryParse(row.BoxCount, out boxesNumber) != false)
                            ticket.BoxesNumber = boxesNumber;
                        else
                        {
                            norm = false;
                            errors.Add("неверно введено количество коробок");
                        }
                    }
                    else
                    {
                        ticket.BoxesNumber = 1;
                    }
                }
                ///
                /// Проверка даты отправки
                //if (Regex.Matches(row.ItemArray[15].ToString(), patternDate).Count == 1)
                //{
                DateTime date1;
                try
                {
                    date1 = Convert.ToDateTime(Convert.ToDateTime(row.SendDate).ToString("dd-MM-yyyy"));

                    DateTime date2 = Convert.ToDateTime(DateTime.Now.AddDays(1).ToString("dd-MM-yyyy"));

                    if (ticket.CityID != null)
                    {
                        var city = new City()
                        {
                            ID = Convert.ToInt32(row.CityID)
                        };
                        city.GetById();

                        //var city = allCity.First(u => u.ID == Convert.ToInt32(ticket.CityID));
                        var district = new Districts { ID = Convert.ToInt32(city.DistrictID) };
                        district.GetById();
                        var selectDayOfWeek = Convert.ToDateTime(Convert.ToDateTime(row.SendDate).ToString("dd-MM-yyyy")).DayOfWeek;
                        if ((selectDayOfWeek == DayOfWeek.Monday && district.Monday != 1)
                            || (selectDayOfWeek == DayOfWeek.Tuesday && district.Tuesday != 1)
                            || (selectDayOfWeek == DayOfWeek.Wednesday && district.Wednesday != 1)
                            || (selectDayOfWeek == DayOfWeek.Thursday && district.Thursday != 1)
                            || (selectDayOfWeek == DayOfWeek.Friday && district.Friday != 1)
                            || (selectDayOfWeek == DayOfWeek.Saturday && district.Saturday != 1)
                            || (selectDayOfWeek == DayOfWeek.Sunday && district.Sunday != 1))
                        {
                            norm = false;
                            errors.Add("отправка возможна только в дни отправки");
                        }

                        if (date1 < date2 && ticket.CityID != 11)
                        {
                            norm = false;
                            errors.Add("отправка в города отличные от Минска возможна только на следующий день после создания заявки!");
                        }

                        if (date1 < Convert.ToDateTime(DateTime.Now.ToString("dd-MM-yyyy")) && ticket.CityID == 11)
                        {
                            norm = false;
                            errors.Add("доставка по Минску возможна только с текущего дня и далее!");
                        }

                        ticket.DeliveryDate = date1;
                    }
                }
                catch (Exception)
                {
                    norm = false;
                    errors.Add("неверная дата");
                }
                //}
                //else
                //{
                //norm = false;
                //errors.Add("неверный формат даты отправки");
                //}
                ///
                /// Комментарии
                if (row.Comments != string.Empty)
                    ticket.Note = row.Comments;
                ///
                /// Проверка данных документов
                if (profileType == 2 || profileType == 3)
                {
                    if (Regex.Matches(row.TTNSeria, patternTTNSeria).Count == 1)
                    {
                        ticket.TtnSeria = row.TTNSeria;
                    }
                    else
                    {
                        norm = false;
                        errors.Add("неверно введена ТТН серия");
                    }

                    if (Regex.Matches(row.TTNNmber, patternTTNNumber).Count == 1)
                    {
                        ticket.TtnNumber = row.TTNNmber;
                    }
                    else
                    {
                        norm = false;
                        errors.Add("неверно введен ТТН номер");
                    }
                }
                else
                {
                    ticket.TtnNumber = string.Empty;
                    ticket.TtnSeria = string.Empty;
                }

                ticket.OtherDocuments = row.OtherDocuments;
                ///
                /// Проверка серии и номера паспорта
                if(!string.IsNullOrEmpty(ticket.PassportNumber) || !string.IsNullOrEmpty(ticket.PassportSeria))
                {
                    if (Regex.Matches(row.PassportSeria, patternTTNSeria).Count == 1)
                        ticket.PassportSeria = row.PassportSeria;
                    else
                    {
                        norm = false;
                        errors.Add("неверно введена серия паспорта");
                    }

                    if (Regex.Matches(row.PassportNumber, patternTTNNumber).Count == 1)
                        ticket.PassportNumber = row.PassportNumber;
                    else
                    {
                        norm = false;
                        errors.Add("неверно введен номер паспорта");
                    }
                }                
                ///
                /// Подсчет стоимости за услугу
                List<GoodsFromAPI> goodsAPI = new List<GoodsFromAPI>();
                foreach (Goods good in goods)
                {
                    GoodsFromAPI goodAPI = new GoodsFromAPI()
                    {
                        Description = good.Description,
                        Number = good.Number
                    };
                    goodsAPI.Add(goodAPI);
                }

                if (goodsAPI.Count > 0)
                {
                    var gruzobozCost = Calculator.Calculate(goodsAPI, Convert.ToInt32(ticket.CityID), userID, Convert.ToInt32(ticket.UserProfileID), profileType.ToString(), ticket.AssessedCost.ToString(), Convert.ToInt32(userDiscount));
                    //если стоимость за услугу не конвертируется в decimal - значит записываем 0
                    try
                    {
                        ticket.GruzobozCost = Decimal.Parse(gruzobozCost);
                    }
                    catch (Exception)
                    {
                        ticket.GruzobozCost = 0;
                    }
                    ///
                    /// Расчет веса
                    ticket.Weight = GoodsHelper.GoodsWeight(goodsAPI);
                }

                ///
                /// Заполнение полей склада
                if (warehouses.Rows.Count == 0)
                {
                    norm = false;
                    errors.Add("ошибка склада");
                }
                DataRow rowWareHouses = warehouses.Rows[0];

                if(ticket.SenderCityID != null || !string.IsNullOrEmpty(ticket.SenderHousing) || !string.IsNullOrEmpty(ticket.SenderStreetName)
                    || !string.IsNullOrEmpty(ticket.SenderStreetNumber) || !string.IsNullOrEmpty(ticket.SenderStreetPrefix) || !string.IsNullOrEmpty(ticket.SenderApartmentNumber))
                {
                    ticket.SenderApartmentNumber = rowWareHouses["ApartmentNumber"].ToString();
                    ticket.SenderCityID = Convert.ToInt32(rowWareHouses["CityID"].ToString());
                    ticket.SenderHousing = rowWareHouses["Housing"].ToString();
                    ticket.SenderStreetName = rowWareHouses["StreetName"].ToString();
                    ticket.SenderStreetNumber = rowWareHouses["StreetNumber"].ToString();
                    ticket.SenderStreetPrefix = rowWareHouses["StreetPrefix"].ToString();
                }
                else
                {
                    if (Int32.TryParse(row.SenderCityID, out cityId) != false)
                    {
                        if (CityHelper.CityIDToCityName(row.SenderCityID.ToString()) != null)
                        {
                            ticket.SenderCityID = Convert.ToInt32(row.SenderCityID.ToString());
                        }
                        else
                        {
                            norm = false;
                            errors.Add("неверный ID города отправки");
                        }
                    }
                    else
                    {
                        norm = false;
                        errors.Add("неверный ID города");
                    }
                    ///
                    /// Проверка типа улицы
                    if (RecipientStreetPrefixes.Contains(row.SenderStreetPrefix))
                    {
                        ticket.SenderStreetPrefix = row.SenderStreetPrefix;
                    }
                    else
                    {
                        norm = false;
                        errors.Add("неверный префикс улицы отправки");
                    }

                    ///
                    /// Проверка района
                    if (row.SenderStreetName != string.Empty)
                        ticket.SenderStreetName = row.SenderStreetName;
                    else
                    {
                        norm = false;
                        errors.Add("не введена улица отправки");
                    }
                    ///
                    ///
                    if (Int32.TryParse(row.SenderHouseNumber, out houseNumber))
                    {
                        ticket.SenderStreetNumber = houseNumber.ToString();
                    }
                    else
                    {
                        norm = false;
                        errors.Add("неверный номер дома отправки");
                    }
                    /// Заполнение корпуса и квартиры
                    if (row.SenderKorpus != string.Empty)
                        ticket.SenderHousing = row.SenderKorpus;
                    if (row.Kvartira != string.Empty)
                        ticket.SenderApartmentNumber = row.SenderKvartira;
                }


                ///
                /// СОЗДАНИЕ ЗАЯВКИ
                /// 

                string error = string.Empty;
                foreach (string errorMsg in errors)
                {
                    error += errorMsg;
                    error += ", ";
                }
                rowsError.Add(error);
                            
                if (norm == true)
                {
                    try
                    {
                        rowsSuccessCount++;

                        //записываем хеши для заявки
                        if (user.Role != Users.Roles.User.ToString())
                        {
                            ticket.SecureID = OtherMethods.CreateUniqId("1" + DateTime.Now.ToString("yyMdHms") + tickets.IndexOf(row));
                            ticket.FullSecureID = OtherMethods.CreateFullUniqId("1" + DateTime.Now.ToString("yyMdHms") + tickets.IndexOf(row));
                        }
                        else
                        {
                            ticket.SecureID = OtherMethods.CreateUniqId(user.ID + DateTime.Now.ToString("yyMdHms") + tickets.IndexOf(row));
                            ticket.FullSecureID = OtherMethods.CreateFullUniqId(user.ID + DateTime.Now.ToString("yyMdHms") + tickets.IndexOf(row));
                        }

                        resultTicket.ID = ticket.SecureID;


                        for (int j = 0; j < goods.Count; j++) //сохраняем заявки
                        {
                            goods[j].TicketFullSecureID = ticket.FullSecureID;
                            goods[j].Create();
                        }
                        ticket.Create();
                        #region Отправка емейла логистам

                        var logistRb = BackendHelper.TagToValue("logist_rb_email");
                        var logistMinsk = BackendHelper.TagToValue("logist_minsk_email");
                        var isMinskRegion = BackendHelper.TagToValue("logist_minsk_region");
                        var senderCity = new City { ID = Convert.ToInt32(ticket.SenderCityID) };
                        senderCity.GetById();

                        if (isMinskRegion != "true")
                        {
                            if (!string.IsNullOrEmpty(logistRb) && senderCity.DistrictID != 10)
                            {
                                try
                                {

                                    EmailMethods.MailSend("Создана новая заявка с забором в РБ",
                                    String.Format("Заявка #{0}, {1}", ticket.SecureID, senderCity.Name), logistRb, true);
                                }
                                catch (Exception)
                                {
                                    // ignored
                                }
                            }
                            else
                            {
                                if (!string.IsNullOrEmpty(logistMinsk) && !(ticket.SenderCityID == 11 && ticket.SenderStreetName.ToLower() == BackendHelper.TagToValue("loading_point_street").ToLower() &&
                                    ticket.SenderStreetNumber == BackendHelper.TagToValue("loading_point_street_number")))
                                {
                                    try
                                    {
                                        EmailMethods.MailSend("Создана новая заявка с забором в Минске и районе",
                                        String.Format("Заявка #{0}, {1}", ticket.SecureID, senderCity.Name), logistMinsk, true);
                                    }
                                    catch (Exception)
                                    {
                                        // ignored
                                    }
                                }
                            }
                        }
                        else
                        {
                            if (!string.IsNullOrEmpty(logistRb) && senderCity.RegionID != 1)
                            {
                                try
                                {
                                    EmailMethods.MailSend("Создана новая заявка с забором в РБ",
                                    String.Format("Заявка #{0}, {1}", ticket.SecureID, senderCity.Name), logistRb, true);
                                }
                                catch (Exception)
                                {
                                    // ignored
                                }
                            }
                            else
                            {
                                if (!string.IsNullOrEmpty(logistMinsk) && !(ticket.SenderCityID == 11 && ticket.SenderStreetName.ToLower() == BackendHelper.TagToValue("loading_point_street") &&
                                    ticket.SenderStreetNumber == BackendHelper.TagToValue("loading_point_street_number")))
                                {
                                    try
                                    {
                                        EmailMethods.MailSend("Создана новая заявка с забором в Минске и области",
                                        String.Format("Заявка #{0}, {1}", ticket.SecureID, senderCity.Name), logistMinsk, true);
                                    }
                                    catch (Exception)
                                    {
                                        // ignored
                                    }
                                }
                            }
                        }
                        #endregion

                        resultTicket.desc = "Заявка создана";
                    }
                    catch(Exception ex)
                    {
                        resultTicket.desc = "Заявка не создана";
                    }
                }
                else
                {
                    resultTicket.desc = error;
                }

                result.Add(resultTicket);
            }          
        

            ////////////////

            return result;
        }
    }
}