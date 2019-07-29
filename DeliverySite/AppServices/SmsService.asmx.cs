using System;
using System.IO;
using System.Web;
using System.Web.Services;
using Delivery.BLL;
using Delivery.DAL.DataBaseObjects;

namespace Delivery.AppServices
{
    /// <summary>
    /// Summary description for ReportService1
    /// </summary>
    [WebService(Namespace = "DeliveryNetWebService")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    // [System.Web.Script.Services.ScriptService]
    public class SmsService : System.Web.Services.WebService
    {
        //проксирующий метод
        [WebMethod]
        public string SendSmsBulk(string ticketIdList, string appkey)
        {
            if (appkey == Globals.Settings.AppServiceSecureKey)
            {
                SmsSender.SendSmsBulk(ticketIdList);
                return "OK";
            }
            return "invalid app key";
        }

    }
}
