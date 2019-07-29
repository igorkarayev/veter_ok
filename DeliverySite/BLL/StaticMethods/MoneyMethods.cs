using Delivery.BLL.Helpers;
using Delivery.DAL.DataBaseObjects;
using System;
using System.Globalization;

namespace Delivery.BLL.StaticMethods
{
    public class MoneyMethods
    {

        #region Разделители разрядов

        public static String MoneySeparator(string money)
        {
            money = money.Replace('.',',');
            if (String.IsNullOrEmpty(money) || money == "&nbsp;")
            {
                return money;
            }
            var result = Convert.ToDecimal(money).ToString("n2");
            return result;
        }

        public static String MoneySeparatorForCityTableView(string money)
        {
            if (String.IsNullOrEmpty(money) || money == "&nbsp;" || money == "0")
            {
                return String.Empty;
            }

            if (money == "-1")
            {
                return "По согласованию";
            }

            var result = String.Format("+{0}", Convert.ToDecimal(money).ToString("n2"));
            return result;
        }

        public static String MoneySeparator(decimal? money)
        {
            var result = Convert.ToDecimal(money, CultureInfo.CurrentCulture).ToString("n2");
            return result;
        }

        public static String MoneySeparator(double money)
        {
            var result = Convert.ToDouble(money).ToString("n2");
            return result;
        }

        #endregion

        public static String OveralCostForCheck(string assessedCost, string deliveryCost)
        {
            var overalCost = Convert.ToDecimal(assessedCost);
            if (!String.IsNullOrEmpty(deliveryCost))
            {
                overalCost = Convert.ToDecimal(assessedCost) + Convert.ToDecimal(deliveryCost);
            }

            return overalCost.ToString("n2");
        }


        //оценочная/согласованная + за доставку

        public static String AgreedAssessedDeliveryCosts(string id)
        {
            var result = String.Empty;
            var ticket = new Tickets { ID = Convert.ToInt32(id) };
            ticket.GetById();
            result = (ticket.AgreedCost != 0 || ticket.WithoutMoney == 1 ? ticket.AgreedCost  : ticket.AssessedCost + ticket.DeliveryCost).ToString();
            return result;
        }


        //согласованная + за доставку

        public static String AgreedDeliveryCosts(string id)
        {
            var ticket = new Tickets { ID = Convert.ToInt32(id) };
            ticket.GetById();
            if (String.IsNullOrEmpty(ticket.AgreedCost.ToString()))
            {
                ticket.AgreedCost = 0;
            }

            return (ticket.AgreedCost + ticket.DeliveryCost).ToString();
        }


        //оценочная + за доставку

        public static String AssessedDeliveryCosts(string id)
        {
            var ticket = new Tickets { ID = Convert.ToInt32(id) };
            ticket.GetById();
            if (String.IsNullOrEmpty(ticket.AssessedCost.ToString()))
            {
                ticket.AssessedCost = 0;
            }

            return (ticket.AssessedCost + ticket.DeliveryCost).ToString();
        }

        //оценочная/согласованная

        public static String AgreedAssessedCosts(string id)
        {
            var result = String.Empty;
            var ticket = new Tickets { ID = Convert.ToInt32(id) };
            ticket.GetById();
            result = ticket.AgreedCost != 0 || ticket.WithoutMoney == 1 ? (ticket.AgreedCost).ToString() : (ticket.AssessedCost).ToString();
            return result;
        }

        //скидка клиента

        public static string UserDiscount(string userid)
        {
            var user = new Users { ID = Convert.ToInt32(userid) };
            user.GetById();
            return user.Discount.ToString();
        }

        public static string IfMoneyNull(string ticketId, string moneyType)
        {
            var result = String.Empty;
            var ticket = new Tickets { ID = Convert.ToInt32(ticketId) };
            ticket.GetById();
            if (moneyType == "BLR")
            {
                result = ticket.ReceivedBLR == 0 ? String.Empty : ticket.ReceivedBLR.ToString();
            }

            if (moneyType == "RUR")
            {
                result = ticket.ReceivedRUR == 0 ? String.Empty : ticket.ReceivedRUR.ToString();
            }

            if (moneyType == "USD")
            {
                result = ticket.ReceivedUSD == 0 ? String.Empty : ticket.ReceivedUSD.ToString();
            }

            if (moneyType == "EUR")
            {
                result = ticket.ReceivedEUR == 0 ? String.Empty : ticket.ReceivedEUR.ToString();
            }
            return result;
        }

        public static string IfMoneyNull(string ticketId, string moneyType, bool isCourse)
        {
            var result = String.Empty;
            var ticket = new Tickets { ID = Convert.ToInt32(ticketId) };
            ticket.GetById();

            if (moneyType == "RUR")
            {
                result = ticket.ReceivedRUR == 0 ? String.Empty : ticket.CourseRUR.ToString();
            }

            if (moneyType == "USD")
            {
                result = ticket.ReceivedUSD == 0 ? String.Empty : ticket.CourseUSD.ToString();
            }

            if (moneyType == "EUR")
            {
                result = ticket.ReceivedEUR == 0 ? String.Empty : ticket.CourseEUR.ToString();
            }
            return result;
        }

        public static string MoneyToIssuance(string ticketId)
        {
            var result = String.Empty;
            var ticket = new Tickets { ID = Convert.ToInt32(ticketId) };
            ticket.GetById();
            var blr = ticket.AgreedCost != 0 ? ticket.AgreedCost + ticket.DeliveryCost : ticket.AssessedCost + ticket.DeliveryCost;
            var usd = ticket.ReceivedUSD;
            var eur = ticket.ReceivedEUR;
            var rur = ticket.ReceivedRUR;
            var gruzobozCost = ticket.GruzobozCost;

            if (blr > gruzobozCost)
            {
                if (blr != 0)
                {
                    result += String.Format("BLR::<b>{0}</b> <br/>", MoneySeparator(blr - gruzobozCost));
                }

                if (usd != 0)
                {
                    result += String.Format("USD::<b>{0}</b> <br/>", MoneySeparator(usd));
                }

                if (eur != 0)
                {
                    result += String.Format("EUR::<b>{0}</b> <br/>", MoneySeparator(eur));
                }

                if (rur != 0)
                {
                    result += String.Format("RUR::<b>{0}</b> <br/>", MoneySeparator(rur));
                }
            }
            else
            {
                if (blr != 0)
                {
                    result += String.Format("BLR::<b>{0}</b> <br/>", MoneySeparator(blr));
                }

                if (usd != 0)
                {
                    result += String.Format("USD::<b>{0}</b> <br/>", MoneySeparator(usd));
                }

                if (eur != 0)
                {
                    result += String.Format("EUR::<b>{0}</b> <br/>", MoneySeparator(eur));
                }

                if (rur != 0)
                {
                    result += String.Format("RUR::<b>{0}</b> <br/>", MoneySeparator(rur));
                }

                result += String.Format("<span style=\"color: red\"><b>-{0}</b></span> BLR", MoneySeparator(gruzobozCost));
            }
            return result;
        }

        public static string ReceivedMoneyToIssuance(string ticketId)
        {
            var result = String.Empty;
            var ticket = new Tickets { ID = Convert.ToInt32(ticketId) };
            ticket.GetById();
            var blr = ticket.AgreedCost != 0 ? ticket.AgreedCost + ticket.DeliveryCost : ticket.AssessedCost + ticket.DeliveryCost;
            var usd = ticket.ReceivedUSD;
            var eur = ticket.ReceivedEUR;
            var rur = ticket.ReceivedRUR;
            var usdCourse = Convert.ToDecimal(ticket.CourseUSD);
            var eurCourse = Convert.ToDecimal(ticket.CourseEUR);
            var rurCourse = Convert.ToDecimal(ticket.CourseRUR);

            if (blr != 0)
            {
                result += String.Format("B:<b>{0}</b> <br/>", MoneySeparator(blr));
            }

            if (usd != 0)
            {
                result += String.Format("U:<b>{0}</b><span style=\"font-size: 10px;\">[{1}]</span> <br/>", MoneySeparator(usd), MoneySeparator(usdCourse));
            }

            if (eur != 0)
            {
                result += String.Format("E:<b>{0}</b><span style=\"font-size: 10px;\">[{1}]</span> <br/>", MoneySeparator(eur), MoneySeparator(eurCourse));
            }

            if (rur != 0)
            {
                result += String.Format("R:<b>{0}</b><span style=\"font-size: 10px;\">[{1}]</span> <br/>", MoneySeparator(rur), MoneySeparator(rurCourse));
            }
            return result;
        }

        public static String CalculatorApiReturnValueToLocalString(String status)
        {
            switch (status)
            {
                case "OutOfTitles":
                    return "Введенного наименования у нас пока нет :( Уточнить стоимость доставки можно у наших менеджеров.";
                case "OutOfCity":
                    return "Выберите город из списка!";
                case "ProfileNotSelected":
                    return "Выберите профиль из списка!";
                case "DescriptionIsNull":
                    return "Введите наименования всех грузов!";
                case "GoodsNumberIsNull":
                    return "Введите количество штук всех грузов!";
                case "InnerException":
                    return "Ошибка расчетов. Перепроверьте введенную информацию.";
                case "DeviationIsUnknown":
                    return "Неизвестна доплата за отклонение. Уточняйте стоимость доставки у наших менеджеров.";
                case "NotAuthorized":
                    return "Запрос не прошел серверную авторизацию. Обратитесь в тех. отдел компании";
                case "InternetMarketAssessedCostException":
                    return "Невозможно просчитать оценочную стоимость груза. Для профиля 'Интернет-магазин' это является необходимым условием при расчете стоимости за доставку.";
                case "CityNotInicialized":
                    return "Город доставки не задан!";
                case "TitleNotAvaliable":
                    return "Рассчет для одного из наименований грузов не доступен для вашего аккаунта. Что бы открыть доступ к данному наименованию обратитесь к нашим менеджерам.";
                default:
                    return status;
            }
        }

        public static String GruzobozCostLoweringPercentage(String money)
        {
            if (String.IsNullOrEmpty(money) || money == "&nbsp;")
            {
                return money;
            }
            var notRoundResult = (Convert.ToDecimal(money) * (100 - Convert.ToDecimal(BackendHelper.TagToValue("gruzoboz_cost_lowering_ percentage")))) / 100;
            notRoundResult = MoneyMethods.MoneyRounder10000(notRoundResult);
            return notRoundResult.ToString();
        }

        public static decimal MoneyRounder100(decimal notRoundResult)
        {
            //decimal tmp;
            //if ((tmp = notRoundResult % Convert.ToDecimal(0.1)) != 0)
            //    notRoundResult += notRoundResult > -1 ? (Convert.ToDecimal(0.1) - tmp) : -tmp;
            return notRoundResult;
        }

        public static decimal MoneyRounder10000(decimal notRoundResult)
        {
            //decimal tmp;
            //if ((tmp = notRoundResult % 1) != 0)
            //    notRoundResult += notRoundResult > -1 ? (1 - tmp) : -tmp;
            return notRoundResult;
        }

        public static decimal MoneyRounder5000(decimal notRoundResult)
        {
            return Decimal.Round(notRoundResult, 2);
        }

        public static String AgreedAccessedCostOver100BazVelich(string gruzobozCost)
        {
            var result = string.Empty;
            var bazVelichina = Convert.ToDecimal(BackendHelper.TagToValue("baz_velichina_rb"));
            if (Convert.ToDecimal(gruzobozCost) >= bazVelichina * 100)
            {
                result = "tdAgreedAccessedCostOver100BazVelich";
            }
            return result;
        }
    }
}


