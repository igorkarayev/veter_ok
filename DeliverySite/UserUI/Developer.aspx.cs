using System;
using Delivery.BLL;
using Delivery.BLL.Helpers;
using Delivery.BLL.StaticMethods;
using Delivery.DAL;
using Delivery.DAL.DataBaseObjects;
using DeliverySite.Resources;

namespace Delivery.UserUI
{
    public partial class Developer : UserBasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            OtherMethods.ActiveRightMenuStyleChanche("hlDeveloper", this.Page);
            var page = new Pages
            {
                ID = 7
            };
            page.GetById();
            Page.Title = page.PageName + BackendHelper.TagToValue("page_title_part");

            if (String.IsNullOrEmpty(ActivatedProfilesCount) || Convert.ToInt32(ActivatedProfilesCount) == 0)
            {
                Response.Redirect("~/usernotification/12");
            }
            //lblJob.Text = page.Content;

        }
    }
}