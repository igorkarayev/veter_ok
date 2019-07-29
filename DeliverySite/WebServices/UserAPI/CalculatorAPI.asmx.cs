using Delivery.BLL.StaticMethods;
using Delivery.WebServices.Objects;
using Newtonsoft.Json;
using System;
using System.Web;
using System.Web.Services;
using Delivery.DAL.DataBaseObjects;

namespace Delivery.WebServices.UserAPI
{
    /// <summary>
    /// Summary description for CalculatorAPI
    /// </summary>
    [WebService(Namespace = "DeliveryNetWebService")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    // [System.Web.Script.Services.ScriptService]
    public class CalculatorAPI : System.Web.Services.WebService
    {

        [WebMethod]  
        public string Calculate(string jsonString)
        {
            if (ApiMethods.IsApiAuthRequest())
            {
                try
                {
                    //var userIdString = HttpContext.Current.Request.Params["userid"];
                    var data = JsonConvert.DeserializeObject<CalculatorObject>(jsonString);
                    var userIdString = string.IsNullOrEmpty(data.UserID.ToString()) ? null : data.UserID.ToString();

                    int? userProfileIdVal = null;
                    int userProfileId;
                    if(Int32.TryParse(data.UserProfileID.ToString(), out userProfileId))
                    {
                        userProfileIdVal = userProfileId;
                    }
                    
                    Boolean isWharehouse = data.IsWharehouse.GetValueOrDefault();

                    if (string.IsNullOrEmpty(data.ProfileType.ToString()))
                    {
                        return "ProfileNotSelected";
                    }

                    if (String.IsNullOrEmpty(data.CityID.ToString()))
                    {
                        return "OutOfCity";
                    }

                    foreach (var good in data.Goods)
                    {
                        if (String.IsNullOrEmpty(good.Number.ToString()) || good.Number.ToString() == "0")
                        {
                            return "GoodsNumberIsNull";
                        }

                        if (String.IsNullOrEmpty(good.Description))
                        {
                            return "DescriptionIsNull";
                        }

                    }
                    var resultCost = BLL.Calculator.Calculate(data.Goods, Convert.ToInt32(data.CityID),
                        Convert.ToInt32(userIdString), userProfileIdVal, data.ProfileType, data.AssessedCost, data.UserDiscount, !isWharehouse);
                    ApiMethods.LoggingRequest("Calculate",
                        "CalculatorAPI",
                        "UserAPI",
                        jsonString,
                        resultCost.Length,
                        Convert.ToInt32(HttpContext.Current.Request.Params["userid"]),
                        HttpContext.Current.Request.Params["apikey"]);

                    

                    return resultCost;

                }
                catch (Exception)
                {
                    return "InnerException";
                }
            }
            return "NotAuthorized";
        }
    }
}
