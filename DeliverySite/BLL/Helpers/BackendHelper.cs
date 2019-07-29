using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Delivery.DAL.DataBaseObjects;

namespace Delivery.BLL.Helpers
{
    public class BackendHelper
    {
        public static String TagToValue(string tag)
        {
            var backendList = HttpContext.Current.Application["BackendList"] as List<Backend>;
            return String.IsNullOrEmpty(tag) ? String.Empty : backendList.SingleOrDefault(u => u.Tag.ToLower() == tag.ToLower()).Value;
        }
    }
}