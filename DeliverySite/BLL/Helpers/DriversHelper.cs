using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net.Mime;
using System.Web;
using Delivery.DAL;
using Delivery.DAL.DataBaseObjects;

namespace Delivery.BLL.Helpers
{
    public class DriversHelper
    {
        public static String DriverStatusToText(int val)
        {
            if (String.IsNullOrEmpty(val.ToString())) return String.Empty;
            return Drivers.DriverStatuses.Where(u => u.Key == val).Select(p => p.Value.ToString()).First();
        }

        public static String DriverColoredStatusRows(string id)
        {
            var result = String.Empty;
            if (String.IsNullOrEmpty(id))
            {
                result = "generalRow";
            }

            if (id == "1") result = "greenRow"; 
            if (id == "2") result = "redRow";

            return result;
        }

        public static String DriverIDToFioToPrint(string id)
        {
            string result;
            if (id == "0")
            {
                result = "Не назначен";
            }
            else
            {
                var driver = new Drivers { ID = Convert.ToInt32(id) };
                driver.GetById();
                result = String.Format("{0} {1} {2}", driver.FirstName, driver.LastName, driver.ThirdName);
            }

            return result;
        }

        public static String DriverIDToNameZP(string id, bool initials = false)
        {
            string result;
            if (id == "0" || id == "-1" || String.IsNullOrWhiteSpace(id))
            {
                result = "__________________________________";
            }
            else
            {
                var driver = new Drivers { ID = Convert.ToInt32(id) };
                driver.GetById();
                result = String.Format("{0} {1} {2}", driver.FirstName, initials ? driver.LastName[0] + "." : driver.LastName, initials ? driver.ThirdName[0] + "." : driver.ThirdName);
            }

            return result;
        }

        public static String DriverIdToName(string id)
        {
            if (id == "0" || id == "-1" || String.IsNullOrWhiteSpace(id))
                return String.Empty;

            var driver = new Drivers { ID = Convert.ToInt32(id) };
            driver.GetById();
            return String.Format("{0} {1} {2}", driver.FirstName, driver.LastName, driver.ThirdName);
        }

        public static String DriverIDToPhone(string id)
        {
            string result;
            if (id == "0" || id == "-1" || String.IsNullOrWhiteSpace(id))
            {
                result = "";
            }
            else
            {
                var driver = new Drivers { ID = Convert.ToInt32(id) };
                driver.GetById();
                result = String.Format("{0}; {1}", driver.PhoneOne, driver.PhoneTwo);
            }

            return result;
        }

        public static String DriverIDToCarZP(string id)
        {
            if (id == "0" || id == "-1" || String.IsNullOrWhiteSpace(id))
                return "__________________________________";
            var driver = new Drivers { ID = Convert.ToInt32(id) };
            driver.GetById();
            if (driver.CarID == 0)
                return "__________________________________";
            var car = new Cars {ID = Convert.ToInt32(driver.CarID)};
            car.GetById();
            if (String.IsNullOrEmpty(car.Model))
                return "__________________________________";
            return car.Model + " " + car.Number;
        }

        public static String DriverIDConvert(string numb)
        {
            string result = numb == "-1" ? "0" : numb;
            return result;
        }

        public static String BackDriverLinkBuilder(string did, string phone, string statusid, string firstname)
        {
            var link = String.Empty;

            if (!String.IsNullOrEmpty(did))
            {
                link += "did=" + did + "&";
            }

            if (!String.IsNullOrEmpty(statusid))
            {
                link += "statusid=" + statusid + "&";
            }

            if (!String.IsNullOrEmpty(phone))
            {
                link += "phone=" + phone + "&";
            }

            if (!String.IsNullOrEmpty(firstname))
            {
                link += "firstname=" + firstname + "&";
            }

            link += "stateSave=true";
            return link;
        }
    }
}