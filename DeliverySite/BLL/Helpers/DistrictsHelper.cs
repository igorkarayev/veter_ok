using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Delivery.DAL.DataBaseObjects;

namespace Delivery.BLL.Helpers
{
    public class DistrictsHelper
    {
        public static String DeliveryDateString(Int32? districtId)
        {
            if (districtId == null)
                return null;
            var districtsList = (List<Districts>) HttpContext.Current.Application["districts"];
            var result = String.Empty;
            var district = districtsList.FirstOrDefault(u => u.ID == districtId);
            if (district == null)
                return null;
            if (district.Monday == 1)
                result += "mo,";
            if (district.Tuesday == 1)
                result += "tu,";
            if (district.Wednesday == 1)
                result += "we,";
            if (district.Thursday == 1)
                result += "th,";
            if (district.Friday == 1)
                result += "fr,";
            if (district.Saturday == 1)
                result += "sa,";
            if (district.Sunday == 1)
                result += "su,";
            return (result.Length == 0)? null: result.Remove(result.Length - 1, 1);
        }

        public static String DeliveryDateStringToRuss(String str)
        {
            if (String.IsNullOrEmpty(str))
                return String.Empty;
            return str
                .Replace("mo", "пн.")
                .Replace("tu", "вт.")
                .Replace("we", "ср.")
                .Replace("th", "чт.")
                .Replace("fr", "пт.")
                .Replace("sa", "сб.")
                .Replace("su", "вс.");
        }

        public static Int32? DeliveryTerms(Int32? districtId)
        {
            if (districtId == null)
                return null;
            var districtsList = (List<Districts>)HttpContext.Current.Application["districts"];
            var district = districtsList.FirstOrDefault(u => u.ID == districtId);
            return district == null ? null : district.DeliveryTerms;
        }

        public static String DeliveryTermsToRuss(Int32? term)
        {
            if (term == 0 || term == null)
                return String.Empty;
            if (term == 1)
                return String.Format("до {0} дня",term);
            return String.Format("до {0} дней", term);
        }
    }
}