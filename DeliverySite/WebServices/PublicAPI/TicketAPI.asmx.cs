using Delivery.BLL.StaticMethods;
using Delivery.DAL.DataBaseObjects;
using DeliverySite.BLL.StaticMethods;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.Services;

namespace DeliverySite.WebServices.PublicAPI
{
    /// <summary>
    /// Summary description for TicketAPI
    /// </summary>
    [WebService(Namespace = "DeliveryNetWebService")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    [System.Web.Script.Services.ScriptService]
    public class TicketAPI : System.Web.Services.WebService
    {

        [WebMethod(Description = "Метод возвращает список всех товаров, их ID и ID категории")]
        public void CreateTickets()
        {
            var ticketsString = HttpContext.Current.Request.Headers["tickets"];
            var userIdString = HttpContext.Current.Request.Headers["userid"];
            var apiKey = HttpContext.Current.Request.Headers["apikey"];
            bool apiKeyExists = true;

            int userId;
            if (!Int32.TryParse(userIdString, out userId))
                apiKeyExists = false;
            Users userById = new Users()
            {
                ID = userId
            };
            userById.GetById();

            if (userById.ApiKey != apiKey)
                apiKeyExists = false;

            if (ApiMethods.IsApiAuthRequest() && apiKeyExists == true)
            {
                
                var ticketsResult = new List<TicketsCreateResult>();

                var jsReturn = new JavaScriptSerializer { MaxJsonLength = Int32.MaxValue };
                var jsGet = new JavaScriptSerializer { MaxJsonLength = Int32.MaxValue };

                dynamic c = jsReturn.DeserializeObject(ticketsString);
                var tickets = jsReturn.Deserialize<List<TicketToCreate>>(ticketsString);


                /*foreach (DataRow row in allCategory.Tables[0].Rows)
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
                }*/

                ticketsResult = new TicketsForAPI().CreateTickets(tickets, Convert.ToInt32(userIdString));

                /*int i = 0;
                foreach (string error in errors)
                {                    
                    ticketsResult.Add(new TicketsCreateResult()
                    {
                        status = string.IsNullOrEmpty(error) ? "Заявка создана" : error,
                        number = i.ToString()
                    });
                    i++;
                }          */    

                Context.Response.ContentType = "application/json; charset=UTF-8";
                var responceBody = jsReturn.Serialize(ticketsResult);
                Context.Response.Write(responceBody);
                ApiMethods.LoggingRequest("CreateTickets",
                        "TicketAPI",
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
    }

    public class TicketsCreateResult
    {
        public String number { get; set; }
        public String ID { get; set; }
        public String desc { get; set; }
    }

    public class TicketsToCreate
    {
        public List<TicketToCreate> ticket { get; set; }
    }

    public class TicketToCreate
    {
        public String ProfileName { get; set; }
        public String CityID { get; set; }
        public String StreetPrefix { get; set; }
        public String StreetName { get; set; }
        public String HouseNumber { get; set; }
        public String Korpus { get; set; }
        public String Kvartira { get; set; }

        public String SenderCityID { get; set; }
        public String SenderStreetPrefix { get; set; }
        public String SenderStreetName { get; set; }
        public String SenderHouseNumber { get; set; }
        public String SenderKorpus { get; set; }
        public String SenderKvartira { get; set; }

        public String FirstName { get; set; }
        public String SecondName { get; set; }
        public String ThirdName { get; set; }
        public String FirstTelefonNumber { get; set; }
        public String SecondTelefonNumber { get; set; }
        public String RecieverCost { get; set; }
        public String BoxCount { get; set; }
        public String SendDate { get; set; }
        public String Comments { get; set; }
        public String TTNSeria { get; set; }
        public String TTNNmber { get; set; }
        public String OtherDocuments { get; set; }
        public String PassportSeria { get; set; }
        public String PassportNumber { get; set; }
        public List<TicketGood> Goods { get; set; }

        /*public TicketToCreate()
        {
            Goods = new List<TicketGood>();
        }*/
    }

    public class TicketGood
    {
        public String GoodName { get; set; }
        public String GoodModel { get; set; }
        public String GoodCost { get; set; }
        public String GoodCount { get; set; }
    }
}
