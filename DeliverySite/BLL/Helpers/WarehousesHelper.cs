using System;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
using System.Linq;
using System.Net.Mime;
using System.Web;
using System.Web.UI;
using Delivery.DAL.DataBaseObjects;

namespace Delivery.BLL.Helpers
{
    public class WarehousesHelper
    {
        public static string IdToName(int id)
        {
            var warehouse = new Warehouses {ID = id};
            warehouse.GetById();
            return warehouse.Name;
        }

        public static String WarehouseAddress(string warehouseId)
        {
            var warehouse = new Warehouses { ID = Convert.ToInt32(warehouseId) };
            warehouse.GetById();
            var city = new City {ID = Convert.ToInt32(warehouse.CityID)};
            city.GetById();
            var result = city.Name + ", " + warehouse.StreetPrefix + " " +warehouse.StreetName + " " + warehouse.StreetNumber;
            if (!String.IsNullOrEmpty(warehouse.Housing))
            {
                result += "/" + warehouse.Housing;
            }
            if (!String.IsNullOrEmpty(warehouse.ApartmentNumber))
            {
                result += " офис " + warehouse.ApartmentNumber;
            }
            return result;
        }
    }
}