using Delivery.BLL.Helpers;
using Delivery.BLL.StaticMethods;
using Delivery.DAL.DataBaseObjects;
using DeliverySite.Resources;
using System;

namespace Delivery.ManagerUI.Menu.NewsFeed
{
    public partial class NewsFeedView : ManagerBasePage
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
            var news = new News { NewsTypeID = 1 };
            lvAllNews.DataSource = news.GetAllByType();
            lvAllNews.DataBind();
            if (lvAllNews.Items.Count == 0)
            {
                lblPage.Visible = false;
            }
        }
    }
}