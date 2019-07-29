using System;
using System.Linq;
using Delivery.DAL.DataBaseObjects;

namespace Delivery.BLL.Helpers
{
    public class ReportsHelper
    {
        public static string ReportConverter(Int32 reportType)
        {
            return Reports.TypeNames.FirstOrDefault(u => u.Key == Convert.ToInt32(reportType)).Value;
        }

        public static String ColoredReportRows(string id)
        {
            var intId = Convert.ToInt32(id);
            var result = String.Empty;
            if (String.IsNullOrEmpty(id))
            {
                result = "generalRow";
            }

            if (intId == 0) result = "greenRow";
            if (intId == 1) result = "yellowRow";
            if (intId == 2) result = "grayRow";
            if (intId == 3) result = "blueRow";
            if (intId == 4) result = "redRow";
            if (intId == 6) result = "pinkRow";

            return result;
        }
    }
}