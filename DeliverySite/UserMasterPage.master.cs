
using Delivery.DAL.DataBaseObjects;
using System;
using System.Linq;

namespace Delivery
{
    public partial class UserMasterPage : System.Web.UI.MasterPage
    {
        public string HaveUnreadNews { get; set; }

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

