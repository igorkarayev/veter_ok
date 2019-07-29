using Delivery.BLL.StaticMethods;
using Delivery.DAL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.Services;

namespace Delivery.WebServices.UserAPI
{
    /// <summary>
    /// Summary description for TitlesAPI
    /// </summary>
    [WebService(Namespace = "DeliveryNetWebService")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    // [System.Web.Script.Services.ScriptService]
    public class TitlesAPI : System.Web.Services.WebService
    {

        [WebMethod(Description = "Метод возвращает список всех наименований грузов в формате JSON")]
        public void GetTitlesJSON()
        {
            if (ApiMethods.IsApiAuthRequest())
            {
                var js = new JavaScriptSerializer();
                var userId = Convert.ToInt32(HttpContext.Current.Request.Params["userid"]);
                var dm = new DataManager();
                DataSet avaliableTitlesDs;
                var ifUserHaveAssignSection =
                    dm.QueryWithReturnDataSet(String.Format("SELECT * FROM `userstocategory` WHERE `UserID` = {0}",
                        userId));
                if (ifUserHaveAssignSection.Tables[0].Rows.Count == 0)
                {
                    avaliableTitlesDs = dm.QueryWithReturnDataSet("SELECT * FROM `titles` ORDER BY `Name`");
                }
                else
                {
                    avaliableTitlesDs =
                        dm.QueryWithReturnDataSet(
                            String.Format(
                                "SELECT * FROM `titles` C WHERE C.`CategoryID` IN (SELECT `CategoryID` FROM `userstocategory` WHERE `UserID` = {0})  ORDER BY C.`Name`",
                                userId));
                }
                var allCategoryList = new List<AllTitlesResult>();
                foreach (DataRow row in avaliableTitlesDs.Tables[0].Rows)
                {
                    allCategoryList.Add(new AllTitlesResult() { Name = row["Name"].ToString() });
                }
                Context.Response.ContentType = "application/json; charset=UTF-8";
                var responceBody = js.Serialize(allCategoryList);
                Context.Response.Write(responceBody);
                ApiMethods.LoggingRequest("GetTitlesJSON",
                    "TitlesAPI",
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

        public class AllTitlesResult
        {
            public String Name { get; set; }
        }
    }
}
