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
    /// Summary description for GoodsAPI
    /// </summary>
    [WebService(Namespace = "DeliveryNetWebService")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    [System.Web.Script.Services.ScriptService]
    public class GoodsAPI : System.Web.Services.WebService
    {

        [WebMethod(Description = "Метод возвращает список всех товаров, их ID и ID категории")]
        public void GetAllGoodsWithIdJSON()
        {
            if (ApiMethods.IsApiAuthRequest())
            {
                var js = new JavaScriptSerializer { MaxJsonLength = Int32.MaxValue };
                var good = new Titles();
                var allGoods = good.GetAllItems();
                var allGoodList = new List<AllGoodWithIdResult>();
                foreach (DataRow row in allGoods.Tables[0].Rows)
                {
                    allGoodList.Add(new AllGoodWithIdResult()
                    {
                        id = row["ID"].ToString(),
                        name = row["Name"].ToString().Trim(),
                        category_id = row["CategoryID"].ToString().Trim()
                    });
                    Context.Response.ContentType = "application/json; charset=UTF-8";
                    var responceBody = js.Serialize(allGoodList);
                    Context.Response.Write(responceBody);
                    ApiMethods.LoggingRequest("GetAllGoodsWithIdJSON",
                        "GoodsAPI",
                        "PublicAPI",
                        null,
                        responceBody.Length,
                        Convert.ToInt32(HttpContext.Current.Request.Params["userid"]),
                        HttpContext.Current.Request.Params["apikey"]);
                }
            }
            else
            {
                ApiMethods.ReturnNotAuth();
            }
        }

        [WebMethod(Description = "Метод возвращает список всех категорий товаров и их ID")]
        public void GetAllCategoriesWithIdJSON()
        {
            if (ApiMethods.IsApiAuthRequest())
            {
                var js = new JavaScriptSerializer { MaxJsonLength = Int32.MaxValue };
                var category = new Category();
                var allCategory = category.GetAllItems();
                var allCategoryList = new List<AllCategoryWithIdResult>();
                foreach (DataRow row in allCategory.Tables[0].Rows)
                {
                    allCategoryList.Add(new AllCategoryWithIdResult()
                    {
                        id = row["ID"].ToString(),
                        name = row["Name"].ToString().Trim()
                    });
                    Context.Response.ContentType = "application/json; charset=UTF-8";
                    var responceBody = js.Serialize(allCategoryList);
                    Context.Response.Write(responceBody);
                    ApiMethods.LoggingRequest("GetAllCategoriesWithIdJSON",
                        "GoodsAPI",
                        "PublicAPI",
                        null,
                        responceBody.Length,
                        Convert.ToInt32(HttpContext.Current.Request.Params["userid"]),
                        HttpContext.Current.Request.Params["apikey"]);
                }
            }
            else
            {
                ApiMethods.ReturnNotAuth();
            }
        }

        public class AllGoodWithIdResult
        {
            public String id { get; set; }

            public String name { get; set; }

            public String category_id { get; set; }
        }

        public class AllCategoryWithIdResult
        {
            public String id { get; set; }

            public String name { get; set; }
        }
    }
}

