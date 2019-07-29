using Delivery.DAL.DataBaseObjects;
using System;
using System.Linq;
using System.Web.UI;

namespace Delivery
{
    public partial class ManagerMasterPage : MasterPage
    {
        public string HaveUnreadNews { get; set; }

        protected void Page_Load(object sender, EventArgs e)
        {
            HaveUnreadNews = "false";
            var user = (Users)Session["userinsession"];
            if (user.NotReadNews.Count > 0)
            {
                HaveUnreadNews = "true";
                lvNotReadNews.DataSource = user.NotReadNews.Take(5);
                lvNotReadNews.DataBind();
                lblUnreadNewsCount.Text = user.NotReadNews.Count.ToString();
            }
        }
    }
}

