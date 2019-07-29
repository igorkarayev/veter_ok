using Delivery.BLL.Helpers;
using Delivery.BLL.StaticMethods;
using Delivery.DAL.DataBaseObjects;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web.Script.Serialization;
using System.Web.Services;

namespace Delivery.AppServices
{
    /// <summary>
    /// Summary description for CityAPI
    /// </summary>
    [WebService(Namespace = "DeliveryNetWebService")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    // [System.Web.Script.Services.ScriptService]
    public class AutocompliteAPI : WebService
    {

        [WebMethod]
        public void GetAvaliableCompanyJson(string query, string appkey)
        {
            if (appkey != Globals.Settings.AppServiceSecureKey) return;
            var js = new JavaScriptSerializer { MaxJsonLength = Int32.MaxValue };
            var allCOmpanyNamesList = new List<AllCompanyForAutocompliteResult>();
            var allCompanyNames = new UsersProfiles();
            var allCOmpanyNamesDs = allCompanyNames.GetAllItems();
            foreach (
                DataRow row in
                    allCOmpanyNamesDs.Tables[0].AsEnumerable()
                        .Where(c => c.Field<string>("CompanyName") != null && c.Field<string>("CompanyName").ToLower().Trim().Contains(query.ToLower().Trim())).OrderBy(c => c.Field<string>("CompanyName"))
                )
            {
                allCOmpanyNamesList.Add(new AllCompanyForAutocompliteResult()
                {
                    value = string.Format("{0} (UID: {1})", row["CompanyName"], row["UserID"]),
                });
            }
            Context.Response.ContentType = "application/json; charset=UTF-8";
            Context.Response.Write("{\"query\": \"Unit\",\"suggestions\": " + js.Serialize(allCOmpanyNamesList) + "}");
        }


        [WebMethod(Description = "Метод возвращает список всех городов для jQuery autocomplete в формате JSON")]
        public void GetCityForAutocompliteJSON(string query, string appkey)
        {
            if (appkey == Globals.Settings.AppServiceSecureKey)
            {
                var js = new JavaScriptSerializer { MaxJsonLength = Int32.MaxValue };
                var allCityFromApp = Application["CityList"] as List<City>;
                var allCityList = new List<AllCityForAutocompliteResult>();
                var coefficientDeviationCost = Convert.ToDouble(BackendHelper.TagToValue("coefficient_deviation_cost")); //множительный коэфициент добавочной стоимости отклонения от основного города
                foreach (
                    City city in
                        allCityFromApp.Where(
                            u =>
                                ((u.Name.ToLower()
                                    .Replace("ё", "е")
                                    .Replace("ъ", "ь")
                                    .Trim()
                                    .Contains(query.ToLower().Replace("ё", "е").Replace("ъ", "ь").Trim())
                                 && u.Blocked == 0)
                                 || u.ID.ToString().Equals(query))
                                ).OrderBy(u => u.Name).Take(250))
                {
                    allCityList.Add(new AllCityForAutocompliteResult()
                    {
                        value = CityHelper.CityIDToAutocompleteString(city),
                        data = city.ID.ToString(),
                        cost = MoneyMethods.MoneySeparatorForCityTableView((city.DistanceFromCity * Convert.ToDecimal(coefficientDeviationCost)).ToString()),
                        deliverydate =
                            DistrictsHelper.DeliveryDateStringToRuss(DistrictsHelper.DeliveryDateString(city.DistrictID)),
                        deliveryterms =
                            DistrictsHelper.DeliveryTermsToRuss(DistrictsHelper.DeliveryTerms(city.DistrictID))
                    });
                }
                Context.Response.ContentType = "application/json; charset=UTF-8";
                Context.Response.Write("{\"query\": \"Unit\",\"suggestions\": " + js.Serialize(allCityList) + "}");
            }
        }

        public class AllCityForAutocompliteResult
        {
            public String value { get; set; }

            public String data { get; set; }

            public String cost { get; set; }

            public String deliverydate { get; set; }

            public String deliveryterms { get; set; }
        }

        public class AllCompanyForAutocompliteResult
        {
            public String value { get; set; }
        }
    }
}
