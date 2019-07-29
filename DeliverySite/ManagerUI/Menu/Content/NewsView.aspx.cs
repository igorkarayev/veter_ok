using Delivery.BLL.Helpers;
using Delivery.BLL.StaticMethods;
using Delivery.DAL.DataBaseObjects;
using DeliverySite.Resources;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI.WebControls;

namespace Delivery.ManagerUI.Menu.Content
{
    public partial class NewsView : ManagerBasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            OtherMethods.ActiveRightMenuStyleChanche("hlNews", this.Page);
            OtherMethods.ActiveRightMenuStyleChanche("hlContent", this.Page);
            Page.Title = PagesTitles.ManagerNewsView + BackendHelper.TagToValue("page_title_part");
            PageAccess();
        }

        public void lbDelete_Click(Object sender, EventArgs e)
        {
            DeleteAccess();
            var lb = (LinkButton)sender;
            var news = new News();
            news.Delete(Convert.ToInt32(lb.CommandArgument));
            Page.Response.Redirect("~/ManagerUI/Menu/Content/NewsView.aspx");
        }

        protected void Page_PreRender(object sender, EventArgs e)
        {
            ListViewDataBind();
        }

        protected void ListViewDataBind()
        {
            var news = new News();
            lvAllNews.DataSource = news.GetAllItems();
            lvAllNews.DataBind();

            #region Редирект на первую страницу при поиске
            if (lvAllNews.Items.Count == 0 && lvDataPager.TotalRowCount != 0)
            {
                lvDataPager.SetPageProperties(0, lvDataPager.PageSize, false);
                lvAllNews.DataBind();
            }
            #endregion
        }

        #region Настройки доступа к странице и действиям
        protected void PageAccess()
        {
            var userInSession = (Users)Session["userinsession"];
            var rolesList = Application["RolesList"] as List<Roles>;
            var currentRole = (Roles)rolesList.SingleOrDefault(u => u.Name.ToLower() == userInSession.Role.ToLower());
            if (currentRole.PageNewsView != 1)
            {
                Response.Redirect("~/Error.aspx?id=1");
            }
        }

        protected void DeleteAccess()
        {
            var userInSession = (Users)Session["userinsession"];
            var rolesList = Application["RolesList"] as List<Roles>;
            var currentRole = (Roles)rolesList.SingleOrDefault(u => u.Name.ToLower() == userInSession.Role.ToLower());
            if (currentRole.ActionNewsDelete != 1)
            {
                Response.Redirect("~/Error.aspx?id=1");
            }
        }
        #endregion
    }
}