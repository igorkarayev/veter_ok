using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using Delivery.BLL.Helpers;
using Delivery.BLL.StaticMethods;
using Delivery.DAL.DataBaseObjects;
using Delivery.WWW123Route;

namespace Delivery.BLL
{
    public class Www123RouteMethods
    {
        public static Result Login(APIClient routeClient)
        {
            var result = routeClient.Login("I.kasperovich@Delivery.by", "7799024");
            return result;
        }

        public static Delivery.Www123RouteGeocoder.Result LoginGeo(Delivery.Www123RouteGeocoder.APIClient routeClient)
        {
            var result = routeClient.Login("I.kasperovich@Delivery.by", "7799024");
            return result;
        }

        public static Result SendData()
        {
            var routeClient = new APIClient();
            var result = Login(routeClient);
            if (result.ErrorLevel != "None")
                System.Diagnostics.Debug.WriteLine("login error: " + result.Message);

            var dataPackXml = File.ReadAllText(HttpContext.Current.Server.MapPath("~/SendData.xml"));
            if (String.IsNullOrEmpty(dataPackXml))
                return null;
            var sendResult = routeClient.SetData(dataPackXml, result.AuthToken);
            if (sendResult.ErrorLevel != "None")
                System.Diagnostics.Debug.WriteLine("send error: " + sendResult.Message);
            return sendResult;
        }

        public static void GetByDate(DateTime dateFrom, DateTime dateTo)
        {
            var routeClient = new APIClient();
            var result = Login(routeClient);
            if (result.ErrorLevel != "None")
                System.Diagnostics.Debug.WriteLine("login error: " + result.Message);

            var options = new GetDataOptions
            {
                Variants = true,
                VariantsData = true,
                DateFrom = dateFrom.ToString("yyyy-MM-dd"),
                DateTo = dateFrom.ToString("yyyy-MM-dd"),
                Variant = 1
            };
            var getResult = routeClient.GetData(options, result.AuthToken);
            if (getResult.ErrorLevel != "None")
                System.Diagnostics.Debug.WriteLine("get error: " + getResult.Message);

            File.WriteAllText(HttpContext.Current.Server.MapPath("~/GetData.xml"), getResult.Data);
        }

        public static void GenerateXmlCityFile(string idListString, string data)
        {
            var idList = idListString.Split(';').ToList();
            var output = "<?xml version=\"1.0\"?> \r\n" +
                         "\t<SmartRoutes.Data SchemaVersion=\"3\"> \r\n" +
                         "\t\t<Variants> \r\n" +
                         "\t\t\t<Variant> \r\n" +
                         "\t\t\t\t<OrderCategoryExchangeCode>1891378909</OrderCategoryExchangeCode> \r\n" +
                         "\t\t\t\t<Date>" + Convert.ToDateTime(data).ToString("yyyy-MM-dd") +
                         "T00:00:00+03:00</Date> \r\n" +
                         "\t\t\t\t<N>1</N> \r\n" +
                         "\t\t\t\t<DataReading>Historical</DataReading> \r\n" +

                         "\t\t\t\t<Orders> \r\n";
            if (!String.IsNullOrEmpty(idListString))
            {
                foreach (var id in idList)
                {
                    var city = new City() {ID = Convert.ToInt32(id)};
                    city.GetById();
                    var address = String.Empty;
                    
                    address = String.Format("Беларусь, {0} область, {1} район, {2}",
                        CityHelper.RegionIDToRegionName(city.RegionID), 
                        CityHelper.DistrictIDToDistrictName(city.DistrictID), 
                        CityHelper.CityNameFilter(city.Name));
                    output += "\t\t\t\t\t<Order> \r\n" +
                              "\t\t\t\t\t\t<ExchangeCode>" + city.ID + "</ExchangeCode> \r\n" +
                              "\t\t\t\t\t\t<Title>" + city.Name + "</Title> \r\n" +
                              "\t\t\t\t\t\t<Code>" + city.ID + "</Code> \r\n" +
                              /*
                              "\t\t\t\t\t\t<LoadingAddress> \r\n" +
                              "\t\t\t\t\t\t\t<Location>Беларусь, Минск, Кальварийская улица, 4</Location> \r\n" +
                              "\t\t\t\t\t\t\t<Latitude>0.9408116799</Latitude> \r\n" +
                              "\t\t\t\t\t\t\t<Longitude>0.4806493073</Longitude> \r\n" +
                              "\t\t\t\t\t\t</LoadingAddress> \r\n" +
                               */
                              "\t\t\t\t\t\t<UnloadingAddress> \r\n" +
                              "\t\t\t\t\t\t\t<Location>" + address + "</Location> \r\n" +
                              "\t\t\t\t\t\t</UnloadingAddress> \r\n" +
                              "\t\t\t\t\t</Order> \r\n";
                }
                output += "   \t\t\t\t</Orders> \r\n" +
                          "\t\t\t</Variant> \r\n" +
                          "\t\t</Variants> \r\n" +
                          "\t</SmartRoutes.Data>";
                File.WriteAllText(HttpContext.Current.Server.MapPath("~/SendCityData.xml"), output);
            }
        }

        public static void GenerateXmlFile(string idListString, string data)
        {
            List<string> idList = idListString.Split('-').ToList();
            var output = "<?xml version=\"1.0\"?> \r\n" +
                         "\t<SmartRoutes.Data SchemaVersion=\"3\"> \r\n" +
                         "\t\t<Variants> \r\n" +
                         "\t\t\t<Variant> \r\n" +
                         "\t\t\t\t<OrderCategoryExchangeCode>1891378909</OrderCategoryExchangeCode> \r\n" +
                         "\t\t\t\t<Date>" + Convert.ToDateTime(data).ToString("yyyy-MM-dd") +
                         "T00:00:00+03:00</Date> \r\n" +
                         "\t\t\t\t<N>1</N> \r\n" +
                         "\t\t\t\t<DataReading>Historical</DataReading> \r\n" +

                         "\t\t\t\t<Characteristics> \r\n" +
                         "\t\t\t\t\t<Characteristic> \r\n" +
                         "\t\t\t\t\t\t<ExchangeCode>12345678</ExchangeCode> \r\n" +
                         "\t\t\t\t\t\t<Title>Оцен./Согл. + за доставку</Title> \r\n" +
                         "\t\t\t\t\t\t<Type>Number</Type> \r\n" +
                         "\t\t\t\t\t\t<Notes>стоимость груза + за доставку</Notes> \r\n" +
                         "\t\t\t\t\t</Characteristic> \r\n" +
                         "\t\t\t\t</Characteristics> \r\n" +

                         "\t\t\t\t<Orders> \r\n";
            if (!String.IsNullOrEmpty(idListString))
            {
                foreach (var id in idList)
                {
                    var ticket = new Tickets {ID = Convert.ToInt32(id)};
                    ticket.GetById();
                    var address = String.Empty;
                    if (!String.IsNullOrEmpty(ticket.RecipientKorpus))
                    {
                        address = String.Format("Беларусь, {0} область, {2}, {3} {4}, {5}/{6}",
                            CityHelper.CityIDToRegionName(ticket.CityID.ToString()),
                            CityHelper.CityIDToDistrictName(ticket.CityID.ToString()),
                            CityHelper.CityNameFilter(CityHelper.CityIDToCityNameWithotCustom(ticket.CityID.ToString())),
                            ticket.RecipientStreetPrefix,
                            ticket.RecipientStreet,
                            ticket.RecipientStreetNumber,
                            ticket.RecipientKorpus);
                    }
                    else
                    {
                        address = String.Format("Беларусь, {0} область, {2}, {3} {4}, {5}",
                            CityHelper.CityIDToRegionName(ticket.CityID.ToString()),
                            CityHelper.CityIDToDistrictName(ticket.CityID.ToString()),
                            CityHelper.CityNameFilter(CityHelper.CityIDToCityNameWithotCustom(ticket.CityID.ToString())),
                            ticket.RecipientStreetPrefix,
                            ticket.RecipientStreet,
                            ticket.RecipientStreetNumber);
                    }

                    var goodsObj = new Goods {TicketFullSecureID = ticket.FullSecureID};
                    var goodsDataSet = goodsObj.GetAllItems("ID", "ASC", "TicketFullSecureID");
                    var goodsString =
                        goodsDataSet.Tables[0].Rows.Cast<DataRow>()
                            .Aggregate(String.Empty,
                                (current, goods) => current + String.Format("{0} {1}; ", goods[2], goods[3]))
                            .Replace("&", "");

                    output += "\t\t\t\t\t<Order> \r\n" +
                              "\t\t\t\t\t\t<ExchangeCode>" + ticket.SecureID + "</ExchangeCode> \r\n" +
                              "\t\t\t\t\t\t<Title>" + goodsString + "</Title> \r\n" +
                              "\t\t\t\t\t\t<Phone>" + ticket.RecipientPhone + "</Phone> \r\n" +
                              "\t\t\t\t\t\t<Code>" + ticket.SecureID + "</Code> \r\n" +
                              "\t\t\t\t\t\t<LoadingAddress> \r\n" +
                              "\t\t\t\t\t\t\t<Location>Беларусь, Минск, Кальварийская улица, 4</Location> \r\n" +
                              "\t\t\t\t\t\t\t<Latitude>0.9408116799</Latitude> \r\n" +
                              "\t\t\t\t\t\t\t<Longitude>0.4806493073</Longitude> \r\n" +
                              "\t\t\t\t\t\t</LoadingAddress> \r\n" +
                              "\t\t\t\t\t\t<UnloadingAddress> \r\n" +
                              "\t\t\t\t\t\t\t<Location>" + address + "</Location> \r\n" +
                              "\t\t\t\t\t\t</UnloadingAddress> \r\n" +
                              //время у клиента в xml-формате "PT30M" означает "30 минут"
                              "\t\t\t\t\t\t<UnloadingDuration>" + "PT10M" + "</UnloadingDuration>" +
                              "\t\t\t\t\t\t<CargoProperties>\r\n" +
                              "\t\t\t\t\t\t\t<Property>\r\n" +
                              "\t\t\t\t\t\t\t\t<CharacteristicExchangeCode>12345678</CharacteristicExchangeCode>\r\n" +
                              "\t\t\t\t\t\t\t\t<Value>" +
                              MoneyMethods.MoneySeparator(MoneyMethods.AgreedAssessedDeliveryCosts(ticket.ID.ToString())) +
                              "</Value>\r\n" +
                              "\t\t\t\t\t\t\t</Property>\r\n" +
                              "\t\t\t\t\t\t</CargoProperties>\r\n" +

                              "\t\t\t\t\t</Order> \r\n";
                }
                output += "   \t\t\t\t</Orders> \r\n" +
                          "\t\t\t</Variant> \r\n" +
                          "\t\t</Variants> \r\n" +
                          "\t</SmartRoutes.Data>";
                File.WriteAllText(HttpContext.Current.Server.MapPath("~/SendData.xml"), output);
            }
        }
    }
}
