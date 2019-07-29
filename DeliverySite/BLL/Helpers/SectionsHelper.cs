using Delivery.DAL.DataBaseObjects;
using System;

namespace Delivery.BLL.Helpers
{
    public class CategoryHelper
    {
        public static string IdToName(string id)
        {
            var category = new Category { ID = Convert.ToInt32(id) };
            category.GetById();
            return category.Name;
        }
    }
}