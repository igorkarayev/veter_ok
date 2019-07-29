using Delivery.BLL.Helpers;
using Delivery.BLL.StaticMethods;
using Delivery.DAL;
using Delivery.DAL.DataBaseObjects;
using DeliverySite.Resources;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Delivery.ManagerUI.Menu.Souls
{
    public partial class DistrictsView : ManagerBasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            OtherMethods.ActiveRightMenuStyleChanche("hlSouls", this.Page);
            OtherMethods.ActiveRightMenuStyleChanche("hlDistricts", this.Page);
            Page.Title = PagesTitles.ManagerDistrictsView + BackendHelper.TagToValue("page_title_part");
            PageAccess();
        }

        protected void btnReload_Click(object sender, EventArgs e)
        {
            Response.Redirect(Request.RawUrl);
        }

        //этот метод перед самой отрисовкой страницы биндит все данные
        protected void Page_PreRender(object sender, EventArgs e)
        {
            ListViewDataBind();
        }

        #region Настройки доступа к странице и действиям
        protected void PageAccess()
        {
            var userInSession = (Users)Session["userinsession"];
            var rolesList = Application["RolesList"] as List<Roles>;
            var currentRole = (Roles)rolesList.SingleOrDefault(u => u.Name.ToLower() == userInSession.Role.ToLower());
            if (currentRole.PageDistrictsView != 1)
            {
                Response.Redirect("~/Error.aspx?id=1");
            }
        }
        #endregion

        #region Methods
        protected void ListViewDataBind()
        {
            var dm = new DataManager();
            lvAllCity.DataSource = dm.QueryWithReturnDataSet(GetSearchString());
            lvAllCity.DataBind();
        }

        public String GetSearchString()
        {
            const string searchString = "SELECT * FROM `districts` ORDER BY `Name` ASC";
            return searchString;
        }
        #endregion
    }
}