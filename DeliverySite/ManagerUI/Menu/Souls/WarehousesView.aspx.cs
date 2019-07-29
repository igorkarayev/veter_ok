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
    public partial class WarehousesView : ManagerBasePage
    {
        protected string BackLink { get; set; }

        protected void Page_Load(object sender, EventArgs e)
        {
            Page.Title = PagesTitles.ManagerWarehousesView + BackendHelper.TagToValue("page_title_part");
            OtherMethods.ActiveRightMenuStyleChanche("hlWarehouses", this.Page);
            OtherMethods.ActiveRightMenuStyleChanche("hlSouls", this.Page);
            PageAccess();
        }

        protected void btnReload_Click(object sender, EventArgs e)
        {
            Response.Redirect(Request.RawUrl);
        }

        public void lbDelete_Click(Object sender, EventArgs e)
        {
            DeleteAccess();
            var userInSession = (Users)Session["userinsession"];
            var lb = (LinkButton)sender;
            var warehouse = new Warehouses();
            warehouse.Delete(Convert.ToInt32(lb.CommandArgument), userInSession.ID, OtherMethods.GetIPAddress(), "WarehousesView");
            Page.Response.Redirect("~/ManagerUI/Menu/Souls/WarehousesView.aspx");
        }

        //этот метод перед самой отрисовкой страницы биндит все данные
        protected void Page_PreRender(object sender, EventArgs e)
        {
            ListViewDataBind();
        }

        #region Methods
        protected void ListViewDataBind()
        {
            var dm = new DataManager();
            var ds = dm.QueryWithReturnDataSet(GetSearchString());
            lvAllCars.DataSource = ds;
            lvAllCars.DataBind();
            foreach (ListViewDataItem items in lvAllCars.Items)
            {
                var lbDeleteLink = (LinkButton)items.FindControl("lbDelete");
                var hfWarehouseId = (HiddenField)items.FindControl("hfWarehouseId");

                if (hfWarehouseId.Value == "1")
                {
                    lbDeleteLink.Visible = false;
                }
            }
        }

        public String GetSearchString()
        {
            const string searchString = "SELECT * FROM warehouses ORDER BY Name ASC";
            return searchString;
        }

        #endregion

        #region Настройки доступа к странице и действиям
        protected void PageAccess()
        {
            var userInSession = (Users)Session["userinsession"];
            var rolesList = Application["RolesList"] as List<Roles>;
            var currentRole = (Roles)rolesList.SingleOrDefault(u => u.Name.ToLower() == userInSession.Role.ToLower());
            if (currentRole.PageWarehousesView != 1)
            {
                Response.Redirect("~/Error.aspx?id=1");
            }
        }

        protected void DeleteAccess()
        {
            var userInSession = (Users)Session["userinsession"];
            var rolesList = Application["RolesList"] as List<Roles>;
            var currentRole = (Roles)rolesList.SingleOrDefault(u => u.Name.ToLower() == userInSession.Role.ToLower());
            if (currentRole.ActionDeleteWarehouses != 1)
            {
                Response.Redirect("~/Error.aspx?id=1");
            }
        }
        #endregion
    }
}