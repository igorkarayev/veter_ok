using Delivery.BLL.Helpers;
using Delivery.BLL.StaticMethods;
using Delivery.DAL;
using System;
using System.IO;
using System.Net;
using System.Text;
using System.Web;
using System.Web.Services;

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
    public class SynchronizationApi : System.Web.Services.WebService
    {
        //проксирующий метод
        [WebMethod]
        public string SendSql(string ticketIdList, string appkey)
        {
            if (appkey != Globals.Settings.AppServiceSecureKey) return "invalid app key";
            //проверка на то, введено ли имя сервера для синхронизации
            var syncServerName = BackendHelper.TagToValue("server_to_sync");
            var currentServerName = BackendHelper.TagToValue("server_name");
            if (syncServerName == currentServerName && currentServerName != "local")
                return "sync with current server denied";
            if (String.IsNullOrEmpty(syncServerName)) return "synchronization turn off";
            string syncServerAddress;
            switch (syncServerName)
            {
                case "doc":
                    syncServerAddress = BackendHelper.TagToValue("doc_server_address");
                    break;
                case "test":
                    syncServerAddress = BackendHelper.TagToValue("test_server_address");
                    break;
                case "arch":
                    syncServerAddress = BackendHelper.TagToValue("arch_server_address");
                    break;
                case "primary":
                    syncServerAddress = BackendHelper.TagToValue("primary_server_address");
                    break;
                case "local":
                    syncServerAddress = BackendHelper.TagToValue("local_server_address");
                    break;
                default:
                    syncServerAddress = BackendHelper.TagToValue("doc_server_address");
                    break;
            }
            SynchronizationMethods.SqlBuilder(ticketIdList);
            var responce = SynchronizationMethods.SqlSender(syncServerAddress);
            return responce;
        }

        [WebMethod]
        public string GetSql(string appkey, string resourseServerAddress)
        {
            if (appkey != Globals.Settings.AppServiceSecureKey) return "invalid app key [remote srv]";
            if (BackendHelper.TagToValue("allow_sync") != "true") return "sync disabled [remote srv]";
            var url = "http://" + resourseServerAddress + "/AppServices/SynchronizationApi.asmx/TakeFile";
            var wc = new WebClient { Encoding = Encoding.UTF8 };
            var data = string.Empty;
            try
            {
                data = wc.DownloadString(url);
            }
            catch (Exception)
            {
                return "Can't download file! [remote srv]";
            }
            var dm = new DataManager();
            try
            {
                dm.QueryWithoutReturnData(null, data);
            }
            catch (Exception ex)
            {
                return "Wrong SQL! [remote srv]." + ex.Message;
            }
            return "OK [remote srv]";
        }


        [WebMethod]
        public void TakeFile()
        {
            var buffer = File.ReadAllBytes(HttpContext.Current.Server.MapPath("~/sync.json"));
            HttpContext.Current.Response.ContentType = "text/plain";
            HttpContext.Current.Response.OutputStream.Write(buffer, 0, buffer.Length);
            HttpContext.Current.Response.Charset = Encoding.UTF8.WebName;
            HttpContext.Current.Response.AddHeader("Content-Disposition", "attachment;filename=sync.json");
            //очищаем файл после его получения
            File.WriteAllText(HttpContext.Current.Server.MapPath("~/sync.json"), "", Encoding.UTF8);
        }

    }
}
