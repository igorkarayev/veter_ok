using Delivery.DAL.DataBaseObjects;
using System;
using System.IO;
using System.Linq;
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
    public class ReportService : System.Web.Services.WebService
    {

        [WebMethod]
        public string SaveReport(string body, string reporttype, string driverid, string drivername, string appkey, string documentdate)
        {
            if (appkey == Globals.Settings.AppServiceSecureKey)
            {
                var alias = Reports.TypeAlias.FirstOrDefault(u => u.Key == Convert.ToInt32(reporttype));
                var fileName = DateTime.Now.ToString("yyyyMMddHHssmm_") + alias.Value + ".txt";
                var report = new Reports
                {
                    FileName = fileName,
                    ReportType = Convert.ToInt16(reporttype),
                    DriverID = Convert.ToInt16(driverid),
                    DriverName = drivername,
                };
                try
                {
                    report.DocumentDate = Convert.ToDateTime(documentdate);
                }
                catch (Exception)
                {
                    report.DocumentDate = Convert.ToDateTime("01-01-2000 00:00");
                }
                report.Create();
                if (!Directory.Exists(Server.MapPath("~/ReportsArch/")))
                {
                    Directory.CreateDirectory(Server.MapPath("~/ReportsArch/"));
                }

                var dataFile = Server.MapPath("~/ReportsArch/" + fileName);
                File.WriteAllText(@dataFile, body);
                return "OK";
            }
            return "invalid app key";
        }

        [WebMethod]
        public string UpdateReport(string body, string reportid, string appkey)
        {
            if (appkey == Globals.Settings.AppServiceSecureKey)
            {
                var report = new Reports { ID = Convert.ToInt32(reportid) };
                report.GetById();
                var dataFile = Server.MapPath("~/ReportsArch/" + report.FileName);
                File.WriteAllText(@dataFile, body);
                return "OK";
            }
            return "invalid app key";
        }

        [WebMethod]
        public void ViewReport(string reportid, string appkey)
        {
            if (appkey == Globals.Settings.AppServiceSecureKey)
            {
                var report = new Reports
                {
                    ID = Convert.ToInt32(reportid)
                };
                report.GetById();

                var fileStream = new FileStream(Server.MapPath("~/ReportsArch/" + report.FileName), FileMode.Open);
                var fileSize = fileStream.Length;

                var buffer = new byte[(int)fileSize];
                fileStream.Read(buffer, 0, (int)fileSize);
                fileStream.Close();
                Context.Response.BinaryWrite(buffer);
            }
            else
            {
                Context.Response.Write("invalid app key");
            }
        }
    }
}
