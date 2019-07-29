
using System;
using System.Linq;
using System.Web;
using System.Web.UI;
using Delivery.DAL.DataBaseObjects;

namespace Delivery.UserUI
{
    public partial class UserMasterPage : System.Web.UI.MasterPage
    {
        public String HaveUnreadNews { get; set; }

        protected void Page_Load(object sender, EventArgs e)
        {
            HaveUnreadNews = "false";
            var user = (Users)Session["userinsession"];
            if (user.NotReadNews.Count > 0 && user.Role == Users.Roles.User.ToString())
            {
                HaveUnreadNews = "true";
                lvNotReadNews.DataSource = user.NotReadNews.Take(5);
                lvNotReadNews.DataBind();
                lblUnreadNewsCount.Text = user.NotReadNews.Count.ToString();
            }
        }
    }
}

