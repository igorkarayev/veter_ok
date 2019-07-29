using Delivery.BLL.Helpers;
using Delivery.BLL.StaticMethods;
using Delivery.DAL.DataBaseObjects;
using DeliverySite.Resources;
using System;

namespace Delivery.UserUI
{
    public partial class NewsFeedView : UserBasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            OtherMethods.ActiveRightMenuStyleChanche("hlNewsFeed", this.Page);
            Page.Title = PagesTitles.ManagerNewsFeedView + BackendHelper.TagToValue("page_title_part");
        }

        protected void Page_PreRender(object sender, EventArgs e)
        {
            ListViewDataBind();
        }

        protected void ListViewDataBind()
        {
            var news = new News { NewsTypeID = 2 };
            lvAllNews.DataSource = news.GetAllByType();
            lvAllNews.DataBind();
            if (lvAllNews.Items.Count == 0)
            {
                lblPage.Visible = false;
            }
        }
    }
}