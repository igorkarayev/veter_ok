using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;

namespace DeliverySite.AppServices
{
    /// <summary>
    /// Summary description for CallPrint
    /// </summary>
    [WebService(Namespace = "DeliveryNetWebService")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    // [System.Web.Script.Services.ScriptService]
    public class CallPrint : System.Web.Services.WebService
    {
        [WebMethod]
        public string CallPrintZP2(string cost, string id, string numberBoxes)
        {
            List<string> json1 = JsonConvert.DeserializeObject<List<string>>(cost);
            List<string> json2 = JsonConvert.DeserializeObject<List<string>>(id);
            List<string> json3 = JsonConvert.DeserializeObject<List<string>>(numberBoxes);

            var costList = String.Empty;
            foreach (string s in json1)
            {
                costList += s;
                costList += "-";
            }
            costList = costList.Remove(costList.Length - 1);

            var idList = String.Empty;
            foreach (string s in json2)
            {
                idList += s;
                idList += "-";
            }
            idList = idList.Remove(idList.Length - 1);

            var numberBoxesList = String.Empty;
            foreach (string s in json3)
            {
                numberBoxesList += s;
                numberBoxesList += "-";
            }
            numberBoxesList = numberBoxesList.Remove(numberBoxesList.Length - 1);

            return String.Format("PrintZP2.aspx?id={0}&cost={1}&number={2}", idList, costList, numberBoxesList);
        }
    }
}
