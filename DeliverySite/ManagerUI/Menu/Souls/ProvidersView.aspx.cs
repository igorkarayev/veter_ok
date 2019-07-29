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
    public partial class ProvidersView : ManagerBasePage
    {
        protected string BackLink { get; set; }

        protected void Page_Init(object sender, EventArgs e)
        {
            btnReload.Click += btnReload_Click;
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            Page.Title = PagesTitles.ManagerDriversView + BackendHelper.TagToValue("page_title_part");
            OtherMethods.ActiveRightMenuStyleChanche("hlProviders", this.Page);
            OtherMethods.ActiveRightMenuStyleChanche("hlSouls", this.Page);
            PageAccess();

            if (!IsPostBack)
            {
                sddlNamePrefix.DataSource = Providers.NamePrefixes;
                sddlNamePrefix.DataTextField = "Value";
                sddlNamePrefix.DataValueField = "Key";
                sddlNamePrefix.DataBind();
                sddlNamePrefix.Items.Insert(0, new ListItem("Все", string.Empty));
            }
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
            var drivers = new Providers();
            drivers.Delete(Convert.ToInt32(lb.CommandArgument), userInSession.ID, OtherMethods.GetIPAddress(), "ProvidersView");
            Page.Response.Redirect("~/ManagerUI/Menu/Souls/ProvidersView.aspx");
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
            lblAllResult.Text = ds.Tables[0].Rows.Count.ToString();
            lvAllDrivers.DataSource = ds;
            lvAllDrivers.DataBind();

            #region Редирект на первую страницу при поиске
            if (lvAllDrivers.Items.Count == 0 && lvDataPager.TotalRowCount != 0)
            {
                lvDataPager.SetPageProperties(0, lvDataPager.PageSize, false);
                lvAllDrivers.DataBind();
            }
            #endregion
        }

        public string GetSearchString()
        {
            var searchString = string.Empty;
            var seletedNamePrefix = string.Empty;
            var searchIdString = string.Empty;
            var searchPhoneString = string.Empty;
            var searchFirstNameString = string.Empty;
            var searchTypesOfProducts = string.Empty;
            var searchParametres = new Dictionary<string, string>();

            //формируем cтроку для поиска по ID
            if (!string.IsNullOrEmpty(stbDID.Text))
            {
                searchIdString = "`ID` = '" + stbDID.Text + "'";
            }

            //формируем cтроку для поиска по ContactPhone
            if (!string.IsNullOrEmpty(stbContactPhone.Text))
            {
                searchPhoneString = "`ContactPhone` LIKE '%" + stbContactPhone.Text + "%'";
            }

            //формируем cтроку для поиска по Name
            if (!string.IsNullOrEmpty(stbFirstName.Text))
            {
                searchFirstNameString = "`Name` LIKE '%" + stbFirstName.Text + "%'";
            }

            //формируем cтроку для поиска по TypesOfProducts
            if (!string.IsNullOrEmpty(stbTypesOfProducts.Text))
            {
                searchTypesOfProducts = "`TypesOfProducts` LIKE '%" + stbTypesOfProducts.Text + "%'";
            }

            //формируем cтроку для поиска по NamePrefix
            if (!string.IsNullOrEmpty(sddlNamePrefix.SelectedValue))
            {
                seletedNamePrefix = "`NamePrefix` = '" + sddlNamePrefix.SelectedValue + "'";
            }


            //формируем конечный запро для поиска
            searchParametres.Add("ID", searchIdString);
            searchParametres.Add("ContactPhone", searchPhoneString);
            searchParametres.Add("Name", searchFirstNameString);
            searchParametres.Add("NamePrefix", seletedNamePrefix);
            searchParametres.Add("TypesOfProducts", searchTypesOfProducts);

            foreach (var searchParametre in searchParametres)
            {
                if (!string.IsNullOrEmpty(searchParametre.Value))
                {
                    searchString = searchString + searchParametre.Value + " AND ";
                }
            }

            //формируем строку поика

            searchString = searchString.Length < 4
                ? "SELECT * FROM providers ORDER BY CreateDate DESC"
                : string.Format("SELECT * FROM providers WHERE {0} ORDER BY CreateDate DESC", searchString.Remove(searchString.Length - 4));

            return searchString;
        }
        #endregion

        #region Настройки доступа к странице и действиям
        protected void PageAccess()
        {
            var userInSession = (Users)Session["userinsession"];
            var rolesList = Application["RolesList"] as List<Roles>;
            var currentRole = (Roles)rolesList.SingleOrDefault(u => u.Name.ToLower() == userInSession.Role.ToLower());
            if (currentRole.PageDriversView != 1)
            {
                Response.Redirect("~/Error.aspx?id=1");
            }
        }

        protected void DeleteAccess()
        {
            var userInSession = (Users)Session["userinsession"];
            var rolesList = Application["RolesList"] as List<Roles>;
            var currentRole = (Roles)rolesList.SingleOrDefault(u => u.Name.ToLower() == userInSession.Role.ToLower());
            if (currentRole.ActionDriversDelete != 1)
            {
                Response.Redirect("~/Error.aspx?id=1");
            }
        }
        #endregion
    }
}