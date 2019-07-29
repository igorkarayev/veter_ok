using Delivery.BLL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;

namespace DeliverySite.AppServices
{
    /// <summary>
    /// Summary description for UpdateFields
    /// </summary>
    [WebService(Namespace = "DeliveryNetWebService")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    // [System.Web.Script.Services.ScriptService]
    public class UpdateFields : System.Web.Services.WebService
    {
        [WebMethod]
        public string UpdateRusCost(string cost)
        {
            decimal overCost = Convert.ToDecimal(cost);
            return MoneyHelper.ToRussianString(overCost);
        }

        [WebMethod]
        public string UpdateRusNumber(string cost)
        {
            decimal overCost = Convert.ToDecimal(cost);
            return NumberToRussianString.NumberToString(
                    Convert.ToInt64(overCost), NumberToRussianString.WordGender.Masculine);
        }
    }
}
