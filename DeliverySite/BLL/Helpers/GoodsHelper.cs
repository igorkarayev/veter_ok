using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Delivery.DAL.DataBaseObjects;
using System.Data;
using Delivery.WebServices.Objects;

namespace Delivery.BLL.Helpers
{
    public class GoodsHelper
    {
        public static int GoodsCount(string fullSecureID)
        {
            var goods = new Goods { TicketFullSecureID = fullSecureID };
            var ds = goods.GetAllItems("ID", "ASC", "TicketFullSecureID");
            return ds.Tables[0].Rows.Cast<DataRow>().Count(); //мой первый LINQ
        }

        public static int GoodsWeight(List<GoodsFromAPI> goodsList)
        {
            var owerWeight = 0;
            var rnd = new Random();
            foreach (var item in goodsList)
            {
                var category = new Titles { Name = item.Description };
                category.GetByName();
                if (String.IsNullOrEmpty(category.ID.ToString()) || category.ID != 0)
                {
                    owerWeight += (rnd.Next(Convert.ToInt32(Math.Round(Convert.ToDouble(category.WeightMin))),
                        Convert.ToInt32(Math.Round(Convert.ToDouble(category.WeightMax))))) * Convert.ToInt32(item.Number);
                }
                else
                {
                    return 0;
                }
            }
            return owerWeight;
        }

        public static string GoodsToString(string fullSecureID)
        {
            var result = String.Empty;
            var goods = new Goods { TicketFullSecureID = fullSecureID };
            var ds = goods.GetAllItems("ID", "ASC", "TicketFullSecureID");
            foreach (DataRow item in ds.Tables[0].Rows)
            {
                result += String.Format("{0} {1};<br/>", item["Description"], item["Model"]);
            }

            if (result.Length > 5)
            {
                result = result.Remove(result.Length - 5, 5);
            }
            return result;
        }
    }
}