using System;
using Delivery.BLL.StaticMethods;
using System.Web;
using Delivery.WebServices.Objects;
using Newtonsoft.Json;

namespace DeliveryNet.Services
{
    public class CalculateService
    {
        public string Calculate(string jsonString)
        {
            if (ApiMethods.IsApiAuthRequest())
            {
                try
                {
                    var userIdString = HttpContext.Current.Request.Params["userid"];
                    var _data = JsonConvert.DeserializeObject<CalculatorObject>(jsonString);

                    if (string.IsNullOrEmpty(_data.ProfileType.ToString()))
                    {
                        return "ProfileNotSelected";
                    }

                    if (String.IsNullOrEmpty(_data.CityID.ToString()))
                    {
                        return "OutOfCity";
                    }

                    foreach (var good in _data.Goods)
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
                    var resultCost = Delivery.BLL.Calculator.Calculate(_data.Goods, Convert.ToInt32(_data.CityID),
                        Convert.ToInt32(userIdString), _data.UserProfileID, _data.ProfileType, _data.AssessedCost, _data.UserDiscount);
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
