using Delivery.DAL;
using Delivery.DAL.DataBaseObjects;
using Delivery.UserUI;
using System;
using System.Collections.Generic;
using System.Data;
using System.Net;
using System.Web.Script.Serialization;
using System.Web.Services;

namespace Delivery.AppServices
{
    /// <summary>
    /// Summary description for SaveAjaxService
    /// </summary>
    [WebService(Namespace = "DeliveryNetWebService")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    // [System.Web.Script.Services.ScriptService]
    public class LoadAjaxService : System.Web.Services.WebService
    {
        [WebMethod]
        public void CreateDataAutocompliteSelectedProfiles(string profileID)
        {
            var js = new JavaScriptSerializer();
            if (profileID != "")
            {
                var dm = new DataManager();
                var senderProfilesDataSet = dm.QueryWithReturnDataSet(string.Format("SELECT * FROM `senderprofiles` WHERE `ProfileID` = {0}", profileID));
                var alluserProfiles = new List<SelectProfilesForAutocompliteResult>();
                foreach (DataRow row in senderProfilesDataSet.Tables[0].Rows)
                {
                    alluserProfiles.Add(new SelectProfilesForAutocompliteResult()
                    {
                        ID = Convert.ToInt32(row["ID"]),
                        ProfileID = row["ProfileID"].ToString(),
                        AddressHouseNumber = row["AddressHouseNumber"].ToString(),
                        AddressKorpus = row["AddressKorpus"].ToString(),
                        AddressKvartira = row["AddressKvartira"].ToString(),
                        AddressPrefix = row["AddressPrefix"].ToString(),
                        AddressStreet = row["AddressStreet"].ToString(),
                        CityCost = row["CityCost"].ToString(),
                        CityDeliveryDate = row["CityDeliveryDate"].ToString(),
                        CityDeliveryTerms = row["CityDeliveryTerms"].ToString(),
                        CityID = row["CityID"].ToString(),
                        CitySelectedString = row["CitySelectedString"].ToString(),
                        ProfileName = row["ProfileName"].ToString(),
                        RecipientPhone1 = row["RecipientPhone1"].ToString(),
                        RecipientPhone2 = row["RecipientPhone2"].ToString(),
                        SendDate = row["SendDate"].ToString(),
                        value = row["ProfileName"].ToString() + " ",
                        FirstName = row["FirstName"].ToString(),
                        LastName = row["LastName"].ToString(),
                        ThirdName = row["ThirdName"].ToString()
                    });
                }
                Context.Response.ContentType = "text/json";
                Context.Response.Write(js.Serialize(alluserProfiles));
            }
            else
            {
                Context.Response.ContentType = "text/json";
                Context.Response.Write(js.Serialize(""));
            }            
        }

        [WebMethod]
        public void LoadCommentHistory(string appkey, string ticketid)
        {
            if (appkey != Globals.Settings.AppServiceSecureKey)
                Context.Response.Write("invalid app key");

            var ticket = new Tickets() { ID = Convert.ToInt32(ticketid) };
            ticket.GetById();
            Context.Response.ContentType = "text/plain; charset=UTF-8";
            Context.Response.Write(WebUtility.HtmlDecode(ticket.Comment));
        }


        [WebMethod]
        public void LoadCommentClientHistory(string appkey, string clientId)
        {
            if (appkey != Globals.Settings.AppServiceSecureKey)
                Context.Response.Write("invalid app key");

            var client = new Users() { ID = Convert.ToInt32(clientId) };
            client.GetById();
            Context.Response.ContentType = "text/plain; charset=UTF-8";
            Context.Response.Write(WebUtility.HtmlDecode(client.Note));
        }

    }
}
