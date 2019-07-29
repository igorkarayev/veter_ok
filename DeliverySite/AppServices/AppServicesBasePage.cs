using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Delivery.BLL;
using Delivery.DAL.DataBaseObjects;

namespace Delivery.AppServices
{
    public class AppServicesBasePage : BasePage
    {
        protected override void OnLoad(EventArgs e)
        {

            var user = (Users)Session["userinsession"];
            if (user == null || user.Role == Users.Roles.User.ToString())
            {
                Response.Redirect("~/");
            }
            base.OnLoad(e);
        }
    }
}