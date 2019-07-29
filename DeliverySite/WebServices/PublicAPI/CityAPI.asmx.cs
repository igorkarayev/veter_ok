using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.Services;
using Delivery.BLL;
using Delivery.BLL.Helpers;
using Delivery.BLL.StaticMethods;
using Delivery.DAL.DataBaseObjects;
using MoneyMethods = Delivery.BLL.StaticMethods.MoneyMethods;

namespace Delivery.WebServices.PublicAPI
{
    /// <summary>
    /// Summary description for CityAPI
    /// </summary>
    [WebService(Namespace = "DeliveryNetWebService")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    [System.Web.Script.Services.ScriptService]
    public class CityAPI : System.Web.Services.WebService
    {

        [WebMethod(Description = "Метод возвращает список всех городов и основную информацию о них в формате JSON")]
        public void GetAllCityWithIdJSON()
        {
            if (ApiMethods.IsApiAuthRequest())
            {
                var js = new JavaScriptSerializer { MaxJsonLength = Int32.MaxValue };
                var city = new City();
                var allCityDS = city.GetAllItems();
                var allCityList = new List<AllCityWithIdResult>();
                foreach (DataRow row in allCityDS.Tables[0].Rows)
                {
                    if(row["Blocked"].ToString() == "0")
                        allCityList.Add(new AllCityWithIdResult()
                        {
                            id = row["ID"].ToString(),
                            name = row["Name"].ToString().Trim(),
                            region_id = row["RegionID"].ToString().Trim(),
                            district_id = row["DistrictID"].ToString().Trim(),
                            village_council_name = row["VillageCouncilName"].ToString().Trim()
                        });
                }
                Context.Response.ContentType = "application/json; charset=UTF-8";
                var responceBody = js.Serialize(allCityList);
                Context.Response.Write(responceBody);
                ApiMethods.LoggingRequest("GetAllCityWithIdJSON",
                    "CityAPI",
                    "PublicAPI",
                    null,
                    responceBody.Length,
                    Convert.ToInt32(HttpContext.Current.Request.Params["userid"]),
                    HttpContext.Current.Request.Params["apikey"]);
            }
            else
            {
                ApiMethods.ReturnNotAuth();
            }
        }

        [WebMethod(Description = "Метод возвращает список всех районов и их ID в формате JSON")]
        public void GetAllDistricsWithIdJSON()
        {
            if (ApiMethods.IsApiAuthRequest())
            {
                var js = new JavaScriptSerializer { MaxJsonLength = Int32.MaxValue };
                var district = new Districts();
                var allDistricts = district.GetAllItems();
                var allDistrictList = new List<AllDistrictWithIdResult>();
                foreach (DataRow row in allDistricts.Tables[0].Rows)
                {
                    allDistrictList.Add(new AllDistrictWithIdResult()
                    {
                        id = row["ID"].ToString(),
                        name = row["Name"].ToString().Trim(),
                        monday = row["Monday"].ToString().Trim(),
                        tuesday = row["Tuesday"].ToString().Trim(),
                        wednesday = row["Wednesday"].ToString().Trim(),
                        thursday = row["Thursday"].ToString().Trim(),
                        friday = row["Friday"].ToString().Trim(),
                        saturday = row["Saturday"].ToString().Trim(),
                        sunday = row["Sunday"].ToString().Trim()
                    });                    
                }
                Context.Response.ContentType = "application/json; charset=UTF-8";
                var responceBody = js.Serialize(allDistrictList);
                Context.Response.Write(responceBody);
                ApiMethods.LoggingRequest("GetAllDistrictWithIdJSON",
                    "CityAPI",
                    "PublicAPI",
                    null,
                    responceBody.Length,
                    Convert.ToInt32(HttpContext.Current.Request.Params["userid"]),
                    HttpContext.Current.Request.Params["apikey"]);
            }
            else
            {
                ApiMethods.ReturnNotAuth();
            }
        }

        [WebMethod(Description = "Метод возвращает список всех областей и их ID в формате JSON")]
        public void GetAllTracksWithIdJSON()
        {
            if (ApiMethods.IsApiAuthRequest())
            {
                var js = new JavaScriptSerializer { MaxJsonLength = Int32.MaxValue };
                var track = new Tracks();
                var allTracks = track.GetAllItems();
                var allTrackList = new List<AllTracksWithIdResult>();
                foreach (DataRow row in allTracks.Tables[0].Rows)
                {
                    allTrackList.Add(new AllTracksWithIdResult()
                    {
                        id = row["ID"].ToString(),
                        name = row["Name"].ToString().Trim()
                    });                    
                }
                Context.Response.ContentType = "application/json; charset=UTF-8";
                var responceBody = js.Serialize(allTrackList);
                Context.Response.Write(responceBody);
                ApiMethods.LoggingRequest("GetAllTracksWithIdJSON",
                    "CityAPI",
                    "PublicAPI",
                    null,
                    responceBody.Length,
                    Convert.ToInt32(HttpContext.Current.Request.Params["userid"]),
                    HttpContext.Current.Request.Params["apikey"]);
            }
            else
            {
                ApiMethods.ReturnNotAuth();
            }
        }

        [WebMethod(Description = "Метод возвращает список всех основных городов и основную информацию о них в формате JSON")]
        public void GetCityWithIdJSON()
        {
            if (ApiMethods.IsApiAuthRequest())
            {
                var js = new JavaScriptSerializer { MaxJsonLength = Int32.MaxValue };
                var city = new City();
                var allCityDS = city.GetAllMainItems();
                var allCityList = new List<AllCityWithIdResult>();
                foreach (DataRow row in allCityDS.Tables[0].Rows)
                {
                    allCityList.Add(new AllCityWithIdResult()
                    {
                        id = row["ID"].ToString(),
                        name = row["Name"].ToString().Trim(),
                        region_id = row["RegionID"].ToString().Trim(),
                        district_id = row["DistrictID"].ToString().Trim(),
                        village_council_name = row["VillageCouncilName"].ToString().Trim()
                    });
                }
                Context.Response.ContentType = "application/json; charset=UTF-8";
                var responceBody = js.Serialize(allCityList);
                Context.Response.Write(responceBody);
                ApiMethods.LoggingRequest("GetCityWithIdJSON",
                    "CityAPI",
                    "PublicAPI",
                    null,
                    responceBody.Length,
                    Convert.ToInt32(HttpContext.Current.Request.Params["userid"]),
                    HttpContext.Current.Request.Params["apikey"]);
            }
            else
            {
                ApiMethods.ReturnNotAuth();
            }
        }

        public class AllCityWithIdResult
        {
            public String id { get; set; }

            public String name { get; set; }

            public String region_id { get; set; }

            public String district_id { get; set; }

            public String village_council_name { get; set; }
        }

        public class AllDistrictWithIdResult
        {
            public String id { get; set; }

            public String name { get; set; }

            public String monday { get; set; }

            public String tuesday { get; set; }

            public String wednesday { get; set; }

            public String thursday { get; set; }

            public String friday { get; set; }

            public String saturday { get; set; }

            public String sunday { get; set; }
        }

        public class AllTracksWithIdResult
        {
            public String id { get; set; }

            public String name { get; set; }
        }
    }
}
