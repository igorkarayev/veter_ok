using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mime;
using System.Web;
using Delivery.DAL.DataBaseObjects;

namespace Delivery.BLL.Helpers
{
    public class CarsHelper
    {
        public static string CarTypeToString(string type)
        {
            var result = type == "1" ? "Физ." : "Юр.";
            return result;
        }

        public static String CarTypeToFullString(int val)
        {
            return val == 0 ? "Не определен" : Cars.CarType.Where(u => u.Key == val).Select(p => p.Value.ToString()).First();
        }

        public static String BackCarLinkBuilder(string aid, string model, string number, string typeid)
        {
            var link = String.Empty;

            if (!String.IsNullOrEmpty(aid))
            {
                link += "aid=" + aid + "&";
            }

            if (!String.IsNullOrEmpty(model))
            {
                link += "model=" + model + "&";
            }

            if (!String.IsNullOrEmpty(number))
            {
                link += "number=" + number + "&";
            }

            if (!String.IsNullOrEmpty(typeid))
            {
                link += "typeid=" + typeid + "&";
            }

            link += "stateSave=true";
            return link;
        }

        public static String CarIdToModelName(string id)
        {
            if (id == "0" || id == "-1" || String.IsNullOrWhiteSpace(id))
                return String.Empty;

            var car = new Cars { ID = Convert.ToInt32(id) };
            car.GetById();
            return String.Format("{0} {1}", car.Model, car.Number);
        }
    }
}