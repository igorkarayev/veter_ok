using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using Delivery.DAL;

namespace Delivery.BLL.StaticMethods
{
    public class OneCMethods
    {
        private const string FileName = "1C_sync.xml";

        public static string GenerateCsv(string idListString, string invoiceNumber, string invoiceSeries, string invoiceDate)
        {
            var result = String.Empty;
            var dm = new DataManager();
            var idListToMySql = idListString.Split('-').ToList().Aggregate(String.Empty, (current, id) => current + "T.ID = " + id + " OR ");

            //читаем старый файл
            if (!Directory.Exists(HttpContext.Current.Server.MapPath("~/OneCSyncArch/")))
            {
                Directory.CreateDirectory(HttpContext.Current.Server.MapPath("~/OneCSyncArch/"));
            }

            var dataFile = HttpContext.Current.Server.MapPath("~/OneCSyncArch/" + FileName);
            var reader = new System.Xml.Serialization.XmlSerializer(typeof(XmlToOneC));
            var xml = new XmlToOneC
            {
                Invoices = new List<Invoice>()
            };
            if (File.Exists(dataFile))
            {
                var fileReaded = new StreamReader(dataFile);
                xml = (XmlToOneC)reader.Deserialize(fileReaded);
                fileReaded.Close();
            }
            //читаем старый файл

            var invoices = new Invoice
            {
                InvoiceNumber = invoiceNumber,
                InvoiceSeries = invoiceSeries,
                InvoiceDate = invoiceDate,
                CustomOrders = new List<CustomOrderToOneC>()
            };
            var fullSqlString = "SELECT * FROM `tickets` as T " +
                                "WHERE ((" + idListToMySql.Remove(idListToMySql.Length - 3) +
                                ") AND T.PrintNakl = '1' AND T.NotPrintInPril2 ='0') ";
            var rows = dm.QueryWithReturnDataSet(fullSqlString).Tables[0].Rows;
            foreach (DataRow row in rows)
            {
                var customOrderCost = row["AssessedCost"].ToString();
                if (!String.IsNullOrEmpty( row["AgreedCost"].ToString()) && Convert.ToInt32(row["AgreedCost"]) != 0)
                {
                    customOrderCost = row["AgreedCost"].ToString();
                }
                if (!String.IsNullOrEmpty(row["Pril2Cost"].ToString()) && Convert.ToInt32(row["Pril2Cost"]) != 0)
                {
                    customOrderCost = row["Pril2Cost"].ToString();
                }
                
                var customOrder = new CustomOrderToOneC
                {
                    CustomOrderSecureId = row["SecureID"].ToString(),
                    CustomOrderCost = Convert.ToDecimal(customOrderCost),
                    CustomOrderDate = OtherMethods.DateConvertWithDot(row["AdmissionDate"].ToString())
                };
                invoices.CustomOrders.Add(customOrder);
            }
            
            xml.Invoices.Add(invoices);

            var writer = new System.Xml.Serialization.XmlSerializer(typeof(XmlToOneC));
            var fileWriter = File.Create(dataFile);
            writer.Serialize(fileWriter, xml);
            fileWriter.Close();

            
            return result;
        }

        public static void StartSession()
        {
            if (!Directory.Exists(HttpContext.Current.Server.MapPath("~/OneCSyncArch/")))
            {
                Directory.CreateDirectory(HttpContext.Current.Server.MapPath("~/OneCSyncArch/"));
            }

            var dataFile = HttpContext.Current.Server.MapPath("~/OneCSyncArch/" + FileName);
            var writer = new System.Xml.Serialization.XmlSerializer(typeof(XmlToOneC));
            var xml = new XmlToOneC();
            var fileWriter = File.Create(dataFile);
            writer.Serialize(fileWriter, xml);
            fileWriter.Close();
        }

        public static void EndSession()
        {
            var dataFile = HttpContext.Current.Server.MapPath("~/OneCSyncArch/" + FileName);
            var dataFileToAbacus = HttpContext.Current.Server.MapPath("~/" + FileName);
            var historyFile = HttpContext.Current.Server.MapPath("~/OneCSyncArch/" + DateTime.Now.ToString("yyyyMMddHHmm") + "_" + FileName);
            File.Copy(dataFile, historyFile); // сохраняем файл в истории
            if (File.Exists(dataFileToAbacus))
            {
                File.Delete(dataFileToAbacus);
            }
            File.Copy(dataFile, dataFileToAbacus); //посылаем файл абакуса куда надо
            File.Delete(dataFile);
        }

        public static bool IfFileExist()
        {
            if (!Directory.Exists(HttpContext.Current.Server.MapPath("~/OneCSyncArch/")))
            {
                Directory.CreateDirectory(HttpContext.Current.Server.MapPath("~/OneCSyncArch/"));
            }

            var dataFile = HttpContext.Current.Server.MapPath("~/OneCSyncArch/" + FileName);
            return File.Exists(dataFile);
        }

    }

    

    public class XmlToOneC
    {
        public List<Invoice> Invoices { get; set; } //коллекция приложений к накладной
    }

    public class CustomOrderToOneC
    {
        public string CustomOrderSecureId { get; set; } //номер заказ-поручения

        public decimal CustomOrderCost { get; set; } //стоимость грузов из заказ-поручения

        public string CustomOrderDate { get; set; } //дата поступления грузов в заказ-поручение
    }

    public class Invoice
    {
        public string InvoiceNumber { get; set; } //номер накладной

        public string InvoiceSeries { get; set; } //серия накладной

        public string InvoiceDate { get; set; } //дата из накладной

        public List<CustomOrderToOneC> CustomOrders { get; set; }
    }  
}