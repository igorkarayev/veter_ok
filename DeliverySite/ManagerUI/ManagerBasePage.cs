using Delivery.DAL.DataBaseObjects;
using System;

namespace Delivery.ManagerUI
{

    public class ManagerBasePage : BasePage
    {
        protected override void OnLoad(EventArgs e)
        {
            var userInSession = (Users)Session["userinsession"];
            if (userInSession == null || userInSession.Role == Users.Roles.User.ToString())
            {
                Response.Redirect("~/");
            }
            base.OnLoad(e);
        }
    }
}