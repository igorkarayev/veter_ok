using Delivery.DAL.DataBaseObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DeliverySite.BLL.Helpers
{
    public class SenderProfilesHelper
    {
        public static string GetProfileNameByID(string id)
        {
            if(id != null && id != "")
            {
                var profile = new SenderProfiles { ID = Convert.ToInt32(id) };
                profile.GetById();
                return profile.ProfileName;
            }

            return null;
        }        
    }
}