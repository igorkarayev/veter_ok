using System;
using System.Collections.Generic;
using System.Data;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.Services;
using Delivery.BLL;
using Delivery.BLL.StaticMethods;
using Delivery.DAL.DataBaseObjects;

namespace Delivery.WebServices.UserAPI
{
    /// <summary>
    /// Summary description for UserProileAPI
    /// </summary>
    [WebService(Namespace = "DeliveryNetWebService")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
     [System.Web.Script.Services.ScriptService]
    public class UserProfileAPI : WebService
    {

        [WebMethod(
            Description =
                "Метод возвращает список всех профилей конкретного пользователя в формате JSON."
            )]
        public void GetProfilesJSON()
        {
            if (ApiMethods.IsApiAuthRequest())
            {
                var js = new JavaScriptSerializer();
                var userIdString = HttpContext.Current.Request.Headers["userid"];
                if (String.IsNullOrEmpty(userIdString))
                    ApiMethods.ReturnNotAuth();
                var profiles = new UsersProfiles {UserID = Convert.ToInt32(userIdString)};
                var allProfilesDS = profiles.GetAllItemsByUserID();
                var allCityList = new List<AllProfileResult>();
                foreach (DataRow row in allProfilesDS.Tables[0].Rows)
                {
                    string fioOrCompanyName;
                    if (String.IsNullOrEmpty(row["CompanyName"].ToString()))
                    {
                        fioOrCompanyName = row["FirstName"] + " " + row["LastName"];
                    }
                    else
                    {
                        fioOrCompanyName = row["CompanyName"].ToString();
                    }

                    var iDPlusType = row["TypeID"].ToString() + row["ID"];

                    allCityList.Add(new AllProfileResult {Name = fioOrCompanyName, ID = iDPlusType});
                }
                Context.Response.ContentType = "application/json; charset=UTF-8";
                var responceBody = js.Serialize(allCityList);
                Context.Response.Write(responceBody);
                ApiMethods.LoggingRequest("GetProfilesJSON",
                    "UserProileAPI",
                    "UserAPI",
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


        [WebMethod(Description = "Метод возвращает список всех профилей конкретного пользователя в формате XML. Требует емейл и пароль пользователя.")]
        public List<AllProfileResult> GetProfilesXML(string email, string password)
        {
            if (ApiMethods.IsApiAuthRequest())
            {
                var js = new JavaScriptSerializer { MaxJsonLength = Int32.MaxValue };
                var allCityList = new List<AllProfileResult>();
                var user = new Users {Email = email};
                user.GetByEmail();
                if (user.Password != OtherMethods.HashPassword(password))
                {
                    allCityList.Add(new AllProfileResult
                    {
                        Name = "Ошибка",
                        ID = "Такой комбинации логина и пароля не найдено!"
                    });
                }
                else
                {
                    var profiles = new UsersProfiles {UserID = user.ID};
                    var allProfilesDS = profiles.GetAllItems();

                    foreach (DataRow row in allProfilesDS.Tables[0].Rows)
                    {
                        string fioOrCompanyName;
                        if (String.IsNullOrEmpty(row["CompanyName"].ToString()))
                        {
                            fioOrCompanyName = row["FirstName"] + " " + row["LastName"];
                        }
                        else
                        {
                            fioOrCompanyName = row["CompanyName"].ToString();
                        }

                        var iDPlusType = row["TypeID"].ToString() + row["ID"];
                        allCityList.Add(new AllProfileResult {Name = fioOrCompanyName, ID = iDPlusType});
                    }
                }
                var responceBody = js.Serialize(allCityList);
                ApiMethods.LoggingRequest("GetProfilesXML",
                    "UserProileAPI",
                    "UserAPI",
                    null,
                    responceBody.Length,
                    Convert.ToInt32(HttpContext.Current.Request.Params["userid"]),
                    HttpContext.Current.Request.Params["apikey"]);
                return allCityList;
            }
            return null;
        }

        public class AllProfileResult
        {
            public String Name { get; set; }

            public String ID { get; set; }
        }
    }
}
