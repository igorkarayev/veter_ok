using System;
using System.Collections.Generic;
using System.Linq;
using Delivery.BLL.Helpers;
using Delivery.BLL.StaticMethods;
using Delivery.DAL.DataBaseObjects;
using DeliverySite.Resources;

namespace Delivery.ManagerUI.Menu.Content
{
    public partial class EmailNotificatonsView : ManagerBasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Page.Title = PagesTitles.ManagerEmailNotificationView + BackendHelper.TagToValue("page_title_part");
            OtherMethods.ActiveRightMenuStyleChanche("hlEmailNotifications", this.Page);
            OtherMethods.ActiveRightMenuStyleChanche("hlContent", this.Page);

            #region Блок доступа к странице
            var userInSession = (Users)Session["userinsession"];
            var rolesList = Application["RolesList"] as List<Roles>;
            var currentRole = (Roles)rolesList.SingleOrDefault(u => u.Name.ToLower() == userInSession.Role.ToLower());
            if (currentRole.PageEmailNotificationsView != 1)
            {
                Response.Redirect("~/Error.aspx?id=1");
            }
            #endregion
        }

        protected void Page_PreRender(object sender, EventArgs e)
        {
            ListViewDataBind();
        }

        protected void ListViewDataBind()
        {
            var mail = new EmailNotifications();
            lvAllEmailNotifications.DataSource = mail.GetAllItems();
            lvAllEmailNotifications.DataBind();

            #region Редирект на первую страницу при поиске
            if (lvAllEmailNotifications.Items.Count == 0 && lvDataPager.TotalRowCount != 0)
            {
                lvDataPager.SetPageProperties(0, lvDataPager.PageSize, false);
                lvAllEmailNotifications.DataBind();
            }
            #endregion
        }
    }
}