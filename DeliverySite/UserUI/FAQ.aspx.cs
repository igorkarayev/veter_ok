using Delivery.BLL.Helpers;
using Delivery.BLL.StaticMethods;
using Delivery.DAL.DataBaseObjects;
using System;

namespace Delivery.UserUI
{
    public partial class FAQ : UserBasePage
    {
        protected void Page_Init(object sender, EventArgs e)
        {
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            OtherMethods.ActiveRightMenuStyleChanche("hlFAQ", this.Page);
            var page = new Pages
            {
                ID = 6
            };
            page.GetById();
            lblJob.Text = page.Content;
            Page.Title = page.PageName + BackendHelper.TagToValue("page_title_part");
        }
    }
}