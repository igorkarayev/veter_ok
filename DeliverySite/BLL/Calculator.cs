using Delivery.BLL.Helpers;
using Delivery.BLL.StaticMethods;
using Delivery.DAL;
using Delivery.DAL.DataBaseObjects;
using Delivery.WebServices.Objects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Delivery.BLL
{
    public class Calculator
    {
        #region Properties
        private static decimal? UserDiscount { get; set; } //скидка клиента
        private static bool IsDiscountCity { get; set; } //флаг помечающий скидочный ли нас. пункт (false - не скидочный)
        private static decimal? DeviationCost { get; set; } //добавочная стоимость за отклонение (характеристика нас. пункта)
        private static bool IsMixedTypesOfTitles { get; set; } //флаг помечающий смещены ли типы наименований (присутствуют ли добавочные и недобавочные наименования в рамках заявки)
        private static decimal UseStavka { get; set; } //Используемая в расчетах ставка
        private static double? MainCost { get; set; } //основная стоимость доставки без учета отклонения и скидки клиента
        private static bool IsTickedHaveWithoutAkcizaGods { get; set; } //флаг помечающий есть ли в заявке БУ грузы
        private static double CoefficientFiz { get; set; } //коефициент для физ-лица (зависит от города - скидочный\не скидочный)

        #endregion;
        //просчет без юзер ID
        public static string Calculate(List<GoodsFromAPI> goodsList, Int32 cityID, string userProfileType, string assessedCost = null)
        {
            //userProfileType must be "2" - ur or "1" - fiz or "3" - market
            return Calculate(goodsList, cityID, null, userProfileType);
        }

        public static string Calculate(List<GoodsFromAPI> goodsList, Int32 cityID, Int32? userID, Int32? userProfileID, string userProfileType, string assessedCost = null, int? userDiscount = null, bool isUdorozanie = false)
        {
            string resultCost = new UserCost().GetCostByUserID(userID, userProfileID);
            if (resultCost != null)
                return resultCost;

            #region Инициализация переменных
            var goodsCount = goodsList.Count;
            var summArray = new double?[goodsCount];
            var minDeliveryCost = Convert.ToDecimal(BackendHelper.TagToValue("min_delivery_cost"));
            var minDeliveryCostMinsk = Convert.ToDecimal(BackendHelper.TagToValue("min_delivery_cost_minsk"));
            var minDeliveryCostMinskRegion = Convert.ToDecimal(BackendHelper.TagToValue("min_delivery_cost_minsk_region"));
            var discountCityIdsString = BackendHelper.TagToValue("discount_city_id");
            var coefficientWithoutAkciza = Convert.ToDouble(BackendHelper.TagToValue("coefficient_without_akciza")); //множительный коэфициент подоражания члена промежуточной суммы, если груз БУ
            var coefficientInternetMarket = Convert.ToDouble(BackendHelper.TagToValue("coefficient_internet_market")); //множительный коэфициент добавочной стоимости на основе оценочной стоимости заявки
            var coefficientDeviationCost = Convert.ToDouble(BackendHelper.TagToValue("coefficient_deviation_cost")); //множительный коэфициент добавочной стоимости отклонения от основного города
            IsMixedTypesOfTitles = true; //по умолчанию считаем что в заявке есть и добавочные и не добавочные наименования
            UserDiscount = 0;  // по умолчанию скидка клиента равна 0
            if (userDiscount != null)
                UserDiscount = userDiscount;
            UseStavka = Convert.ToDecimal(BackendHelper.TagToValue("baz_stavka")); //по умолчанию используемая в расчетах ставка равна базовой общей ставке
            MainCost = 0; //по умолчанию базовая стоимость доставки 0 (может быть изменено)
            #endregion

            #region Инициализация флаг скидочного нас. пункта "IsDiscountCity"
            IsDiscountCity = discountCityIdsString.Split(new[] { ',' }).Select(Int32.Parse).ToList().Contains(cityID);
            #endregion

            #region Инициализация флаг наличия БУ груза в заявке
            IsTickedHaveWithoutAkcizaGods = goodsList.Any(u => u.WithoutAkciza == 1);
            #endregion

            #region Считывание скидки клиента "UserDiscount", если клиент задан b=и не равен одноразовуму
            if (userID != 0 && userID != null && userID != 1)
            {
                var user = new Users { ID = Convert.ToInt32(userID) };
                user.GetById();
                UserDiscount = user.Discount;
            }
            #endregion

            #region Считывание суммы за отклонение (характеристика населенного пункта)
            if (string.IsNullOrEmpty(userProfileType))
            {
                return "ProfileNotSelected";
            }

            //Если нас. пункт не задан - прекращаем калькуляции с соответствующим сообщением
            if (cityID == 0)
                return "CityNotInicialized";
            var city = new City { ID = cityID };
            city.GetById();

            //Если нас. пункт не существует - прекращаем калькуляции с соответствующим сообщением
            if (city.ID == 0)
                return "OutOfCity";

            //Если город-попутчик, то отклонение = 0
            if (city.DistanceFromCity == -1)
                DeviationCost = 0;
            else
                DeviationCost = city.DistanceFromCity * Convert.ToDecimal(coefficientDeviationCost);

            //Если нас. пункт не основной и расстояние до ближ. осн. = 0 - прекращаем калькуляции с соответствующим сообщением о том, что не можем посчитать отклонение
            if (city.IsMainCity == 0 && city.DistanceFromCity == 0)
            {
                if (BackendHelper.TagToValue("calculator_send_email_if_error") == "true")
                    EmailMethods.MailSendHTML("Не могу просчитать отклонение для нас. пункта",
                        string.Format("Не могу просчитать отклонение для нас. пункта <b>{0}</b> (id:{1}), UID: {2}", city.Name, city.ID, userID),
                        BackendHelper.TagToValue("calculator_send_email_if_error_email_list").Split(new[] { ',' }));
                return "DeviationIsUnknown";
            }
            #endregion

            #region Инициализация использующегося в расчетах коэффициента "CoefficientFiz" 
            CoefficientFiz = Convert.ToDouble(IsDiscountCity ? BackendHelper.TagToValue("coefficient_fiz_discount_city") : BackendHelper.TagToValue("coefficient_fiz"));
            #endregion

            #region Инициализация использующейся в расчетах ставки "UseStavka" (последовательность действий ВАЖНА!)
            //Если выбраный город доставки из Минской области - ставка по минской области
            if (city.RegionID == 1)
                UseStavka = Convert.ToDecimal(BackendHelper.TagToValue("baz_stavka_minsk_region"));
            //Если выбраный город доставки Минск - ставка по минску
            if (city.ID == 11)
                UseStavka = Convert.ToDecimal(BackendHelper.TagToValue("baz_stavka_minsk"));
            #endregion

            #region Предварительная проверка грузов на соответствие наименованиям (в случае, если хоть один груз вне наименований - прекращаем калькуляцию)
            var ii = 0;
            foreach (GoodsFromAPI goods in goodsList)
            {
                var titleForCheck = new Titles();
                //Если приходит "пустой" (без названия) груз - прекращаем калькуляции с соответствующим сообщением
                if (string.IsNullOrEmpty(goods.Description))
                    return "DescriptionIsNull";
                //Если количество груза отсутствует или = 0 - прекращаем калькуляции с соответствующим сообщением
                if (goods.Number == null || goods.Number == 0)
                    return "GoodsNumberIsNull";
                titleForCheck.Name = goods.Description.Trim();
                titleForCheck.GetByName();
                //Если наименование не найдено по названию груза - прекращаем калькуляции с соответствующим сообщением
                if (titleForCheck.ID == 0)
                {
                    if (BackendHelper.TagToValue("calculator_send_email_if_error") == "true")
                        EmailMethods.MailSendHTML("Заявка на несуществующее наименование",
                            string.Format("Не могу просчитать стоимость доставки - такого наименования нет в системе <b>\"{0}\"</b>, UID: {1}", titleForCheck.Name, userID),
                            BackendHelper.TagToValue("calculator_send_email_if_error_email_list").Split(new[] { ',' }));
                    return "OutOfTitles";
                }

                var dm = new DataManager();
                //Если наименование недоступно клиенту - прекращаем калькуляции с соответствующим сообщением
                var ifUserHaveAssignSection = dm.QueryWithReturnDataSet(string.Format("SELECT * FROM `userstocategory` WHERE `UserID` = {0}", userID));
                if (ifUserHaveAssignSection.Tables[0].Rows.Count != 0)
                {
                    var ifTitleAvaliableForUser =
                        dm.QueryWithReturnDataSet(
                            string.Format(
                                "SELECT * FROM `titles` C WHERE C.`CategoryID` IN (SELECT `CategoryID` FROM `userstocategory` WHERE `UserID` = {0}) AND C.`Name` = \"{1}\" ORDER BY C.`Name`",
                                userID,
                                titleForCheck.Name)).Tables[0].Rows.Count;
                    if (ifTitleAvaliableForUser == 0)
                        return "TitleNotAvaliable";
                }
                //Eсли наименование найдено - проверяем и фиксируем тот факт, является ли груз добавочным (который не учитывается при калькуляции)
                goods.IsAdditional = titleForCheck.Additive;
                goods.Coefficient = titleForCheck.MarginCoefficient;
                goods.AdditiveCostWithoutAkciza = titleForCheck.AdditiveCostWithoutAkciza;
                //Правка безакцизных коэффициентов в зависимости от того, поддерживает ли наименование наличие безакцизных грузов
                if (titleForCheck.CanBeWithoutAkciza != 1)
                    goods.WithoutAkciza = 0;
                ii++;
            }
            //Если все наименования добавочные или все наименования недобавочные  - нет смеси типов наименований
            if (goodsList.Sum(u => u.IsAdditional) == goodsList.Count() || goodsList.Sum(u => u.IsAdditional) == 0)
            {
                IsMixedTypesOfTitles = false;
            }
            #endregion

            #region Расчет членов промежуточной суммы в зависимости от наличия в заявке БУ грузов
            var i = 0;
            foreach (GoodsFromAPI goods in goodsList)
            {

                //Если БУ груз есть в заявке - его член суммы уваличивается в "coefficientWithoutAkciza" раз
                if (goods.WithoutAkciza == 1)
                {
                    //Если наименование отмечено как б\у и "AdditiveCostWithoutAkciza" не равна 0, то не использовать "множительный коэфициент подоражания члена промежуточной суммы, если груз БУ" из backend, а использовать "AdditiveCostWithoutAkciza" из свойств наименования
                    if (goods.AdditiveCostWithoutAkciza != 0)
                    {
                        summArray[i] = goods.Coefficient * Convert.ToInt32(UseStavka) * Convert.ToInt32(goods.Number) + goods.AdditiveCostWithoutAkciza;
                    }
                    else
                    {
                        summArray[i] = goods.Coefficient * Convert.ToInt32(UseStavka) * Convert.ToInt32(goods.Number) * coefficientWithoutAkciza;
                    }
                }
                else
                {
                    summArray[i] = goods.Coefficient * Convert.ToInt32(UseStavka) * Convert.ToInt32(goods.Number);
                }

                #region Если имеет место смесь наименований и текущий груз соответствует добавочному наименованию - зануляем его стоимость доставки в любом случае
                if (IsMixedTypesOfTitles && goods.IsAdditional == 1)
                {
                    summArray[i] = 0;
                }
                #endregion
                i++;
            }
            #endregion

            #region Расчет основной стоимости доставки
            //Заявка с максимальным коэффициентом
            var goodsWithMaxCoefficient = goodsList.OrderByDescending(x => x.Coefficient).First();

            #region Расчет #1 - тип профиля "2" (Юр. лицо)
            if (userProfileType == "2")
            {
                MainCost = goodsWithMaxCoefficient.Coefficient * Convert.ToInt32(UseStavka) + (summArray.Sum() - goodsWithMaxCoefficient.Coefficient * Convert.ToInt32(UseStavka)) / 2;
            }
            #endregion

            #region Расчет #2 - тип профиля "3" (Интернет-магазин)
            if (userProfileType == "3" && !string.IsNullOrEmpty(assessedCost))
            {
                MainCost = goodsWithMaxCoefficient.Coefficient * Convert.ToInt32(UseStavka) + (summArray.Sum() - goodsWithMaxCoefficient.Coefficient * Convert.ToInt32(UseStavka)) / 2 + Convert.ToDouble(assessedCost) * coefficientInternetMarket;
            }
            //Если профиль интернет-магазин, но не пришла оценочная стоимость - прекращаем калькуляцию и выводим соответствующее сообщение
            if (userProfileType == "3" && string.IsNullOrEmpty(assessedCost))
                return "InternetMarketAssessedCostException";
            #endregion

            #region Расчет #3 - тип профиля "1" (Физ. лицо)
            //Если профиль "Физ. лицо"
            if (userProfileType == "1")
            {
                MainCost = CoefficientFiz*(goodsWithMaxCoefficient.Coefficient * Convert.ToInt32(UseStavka) + (summArray.Sum() - goodsWithMaxCoefficient.Coefficient * Convert.ToInt32(UseStavka)) / 2);
                //Если профиль "Физ. лицо" и в заяве есть Б\У грузы
                if (IsTickedHaveWithoutAkcizaGods)
                {
                    //Расчет добавочного члена с наибольшим коэфициентом, если он БУ\не-БУ
                    var goodsMaxMemberForFizWithoutAkciza = goodsWithMaxCoefficient.Coefficient * Convert.ToInt32(UseStavka);
                    if (goodsWithMaxCoefficient.WithoutAkciza == 1)
                    {
                        //Если наименование отмечено как б\у и "AdditiveCostWithoutAkciza" не равна 0, то не использовать "множительный коэфициент подоражания члена промежуточной суммы, если груз БУ" из backend, а использовать "AdditiveCostWithoutAkciza" из свойств наименования
                        if (goodsWithMaxCoefficient.AdditiveCostWithoutAkciza != 0)
                        {
                            goodsMaxMemberForFizWithoutAkciza = goodsWithMaxCoefficient.Coefficient * Convert.ToInt32(UseStavka) + goodsWithMaxCoefficient.AdditiveCostWithoutAkciza;
                        }
                        else
                        {
                            goodsMaxMemberForFizWithoutAkciza = goodsWithMaxCoefficient.Coefficient * Convert.ToInt32(UseStavka) * coefficientWithoutAkciza;
                        }
                    }

                    MainCost = CoefficientFiz * (goodsMaxMemberForFizWithoutAkciza + (summArray.Sum() - goodsMaxMemberForFizWithoutAkciza) / 2);
                }
            }

            #endregion

            #endregion

            //Добавление удоражания
            var mp = BackendHelper.TagToValue("multiple_percent");
            if (isUdorozanie && mp != null)
            {
                double mpVal = 0;
                Double.TryParse(mp, out mpVal);
                MainCost += MainCost * mpVal / 100;
            }
            //Добавление скидки
            var mainCostWithDiscount = Convert.ToDecimal((Convert.ToDecimal(MainCost) * (100 - UserDiscount)) / 100);
            //Добавление отклонения 
            var mainCostWithDiscountWithDeviation = mainCostWithDiscount + Convert.ToDecimal(DeviationCost);
            //Добавление округление до 5-ти тысяч
            var mainCostWithDiscountWithDeviationRounded = MoneyMethods.MoneyRounder5000(mainCostWithDiscountWithDeviation);

            #region Выставление минимальных цен за доставку, если не добрали до ее (последовательность ВАЖНА!)
            //Если не добрали до минималки - выставляем минималку
            if (city.RegionID != 1 && city.ID != 11 && mainCostWithDiscountWithDeviationRounded < minDeliveryCost)
                mainCostWithDiscountWithDeviationRounded = minDeliveryCost;

            //Если выбраный город доставки из Минской области и насчитали меньше минималки по Минской области - минималка по минской области
            if (city.RegionID == 1 && city.ID != 11 && mainCostWithDiscountWithDeviationRounded < minDeliveryCostMinskRegion)
                mainCostWithDiscountWithDeviationRounded = minDeliveryCostMinskRegion;

            //Если выбраный город доставки Минск и насчитали меньше минималки по Минску - минималка по минску
            if (city.ID == 11 && mainCostWithDiscountWithDeviationRounded < minDeliveryCostMinsk)
                mainCostWithDiscountWithDeviationRounded = minDeliveryCostMinsk;
            #endregion
            

            return mainCostWithDiscountWithDeviationRounded.ToString();
        }
    }
}