using Delivery.BLL.Helpers;
using Delivery.BLL.StaticMethods;
using Delivery.DAL;
using Delivery.DAL.DataBaseObjects;
using DeliverySite.Resources;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI.WebControls;

namespace Delivery.ManagerUI.Menu.Souls
{
    public partial class SectionsView : ManagerBasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Page.Title = PagesTitles.ManagerCategoryViewTitle + BackendHelper.TagToValue("page_title_part");
            OtherMethods.ActiveRightMenuStyleChanche("hlCategory", this.Page);
            OtherMethods.ActiveRightMenuStyleChanche("hlSouls", this.Page);
            PageAccess();
        }

        protected void lvDataPager_PreRender(object sender, EventArgs e)
        {
            var dm = new DataManager();
            var ds = dm.QueryWithReturnDataSet("SELECT C.*, (SELECT count(*) FROM `titles` WHERE `CategoryID` = C.`ID`) as TitlesCount FROM `category` C ORDER BY `Name` ASC;");
            lvAllTracks.DataSource = ds;
            lvAllTracks.DataBind();

            foreach (ListViewDataItem items in lvAllTracks.Items)
            {
                var lbDeleteLink = (LinkButton)items.FindControl("lbDelete");
                var lblCategoryCount = (Label)items.FindControl("lblTitlesCount");
                var countCategory = Convert.ToInt32(lblCategoryCount.Text);
                if (countCategory > 0)
                    lbDeleteLink.Visible = false;
            }

            #region Редирект на первую страницу при поиске
            if (lvAllTracks.Items.Count == 0 && lvDataPager.TotalRowCount != 0)
            {
                lvDataPager.SetPageProperties(0, lvDataPager.PageSize, false);
                lvAllTracks.DataBind();
            }
            #endregion
        }

        public void lbDelete_Click(Object sender, EventArgs e)
        {
            DeleteAccess();
            var userInSession = (Users)Session["userinsession"];
            var lb = (LinkButton)sender;
            var category = new Category();
            category.Delete(Convert.ToInt32(lb.CommandArgument), userInSession.ID, OtherMethods.GetIPAddress(), "CategoryView");
            Page.Response.Redirect("~/ManagerUI/Menu/Souls/CategoryView.aspx");
        }

        #region Настройки доступа к странице и действиям
        protected void PageAccess()
        {
            var userInSession = (Users)Session["userinsession"];
            var rolesList = Application["RolesList"] as List<Roles>;
            var currentRole = (Roles)rolesList.SingleOrDefault(u => u.Name.ToLower() == userInSession.Role.ToLower());
            if (currentRole.PageCategoryView != 1)
            {
                Response.Redirect("~/Error.aspx?id=1");
            }
        }

        protected void DeleteAccess()
        {
            var userInSession = (Users)Session["userinsession"];
            var rolesList = Application["RolesList"] as List<Roles>;
            var currentRole = (Roles)rolesList.SingleOrDefault(u => u.Name.ToLower() == userInSession.Role.ToLower());
            if (currentRole.ActionCategoryDelete != 1)
            {
                Response.Redirect("~/Error.aspx?id=1");
            }
        }
        #endregion
    }
}