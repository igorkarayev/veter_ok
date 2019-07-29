using Delivery.BLL.Helpers;
using Delivery.DAL.DataBaseObjects;
using ExcelDataReader;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Net.Mail;
using System.Net.Mime;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Windows.Forms;

namespace Delivery.BLL.StaticMethods
{
    public class ReadXLSMethods
    {
        public static DataSet ReadXLS(Stream stream)
        {
            IExcelDataReader excelReader;            

            try
            {
                excelReader = ExcelReaderFactory.CreateBinaryReader(stream);           
                return excelReader.AsDataSet();
            }
            catch (Exception) { }

            try
            {
                excelReader = ExcelReaderFactory.CreateOpenXmlReader(stream);
                return excelReader.AsDataSet();
            }
            catch (Exception) { }

            return new DataSet();
        }

        public class XLSWrite
        {
            public MemoryStream GetXLSStreamStatistic(DataTable table) 
            {
                ExcelPackage excel = new ExcelPackage();
                var workSheet = excel.Workbook.Worksheets.Add("Sheet1");
                workSheet.Cells[1, 1].LoadFromDataTable(table, true);
                workSheet.Column(5).Style.Numberformat.Format = "yyyy/mm/dd hh:mm:ss";
                workSheet.Column(5).Width = 18;

                var contenttype = new ContentType("application/vnd.ms-excel");
                return new MemoryStream(excel.GetAsByteArray());                
            }
        }
    }
}