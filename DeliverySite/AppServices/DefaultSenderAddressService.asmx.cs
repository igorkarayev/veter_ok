using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Services;
using Delivery.BLL;
using Delivery.DAL.DataBaseObjects;
using Delivery.WebServices.Objects;

namespace Delivery.AppServices
{
    /// <summary>
    /// Summary description for DefaultSenderCityService
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    // [System.Web.Script.Services.ScriptService]
    public class DefaultSenderAddressService : System.Web.Services.WebService
    {

        [WebMethod]
        public string SaveDefaultSenderAdress(string userid, string cityid, string appkey, string senderstreetname, string senderstreetprefix, string senderstreetnumber, string senderhousing, string senderapartmentnumber, string senderawharehouse)
        {
            if (appkey != Globals.Settings.AppServiceSecureKey) return "invalid app key";

            var user = new Users
            {
                ID = Convert.ToInt32(userid), 
                SenderCityID = Convert.ToInt32(cityid),
                SenderStreetPrefix = senderstreetprefix,
                SenderStreetName = senderstreetname,
                SenderStreetNumber = senderstreetnumber,
                SenderHousing = senderhousing,
                SenderApartmentNumber = senderapartmentnumber,
                SenderWharehouse = senderawharehouse
            };
            user.Update();
            return "OK";
        }

        [WebMethod]
        public string ClearDefaultSenderAddress(string userid, string appkey)
        {
            if (appkey != Globals.Settings.AppServiceSecureKey) return "invalid app key";

            var user = new Users
            {
                ID = Convert.ToInt32(userid),
                SenderCityID = 0,
                SenderStreetPrefix = String.Empty,
                SenderStreetName = String.Empty,
                SenderStreetNumber = String.Empty,
                SenderHousing = String.Empty,
                SenderApartmentNumber = String.Empty,
                SenderWharehouse = String.Empty
            };
            user.Update();
            return "OK";
        }
    }
}
