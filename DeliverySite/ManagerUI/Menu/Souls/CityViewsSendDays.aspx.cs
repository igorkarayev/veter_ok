using Delivery;
using Delivery.BLL.Helpers;
using Delivery.BLL.StaticMethods;
using Delivery.DAL;
using Delivery.DAL.DataBaseObjects;
using Delivery.ManagerUI;
using DeliverySite.Resources;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI.WebControls;

namespace DeliverySite.ManagerUI.Menu.Souls
{
    public partial class CityViewsSendDays : ManagerBasePage
    {
        public String AppKey { get; set; }
        protected void Page_Init(object sender, EventArgs e)
        {
            btnReload.Click += btnReload_Click;
        }


        protected void Page_Load(object sender, EventArgs e)
        {
            OtherMethods.ActiveRightMenuStyleChanche("hlCity", this.Page);
            OtherMethods.ActiveRightMenuStyleChanche("hlSouls", this.Page);
            Page.Title = PagesTitles.ManagerCityView + BackendHelper.TagToValue("page_title_part");
            PageAccess();
            AppKey = Globals.Settings.AppServiceSecureKey;
            if (!IsPostBack)
            {
                sddlDistricts.DataSource = (from t in City.Districts
                                            select new
                                            {
                                                Key = t.Key,
                                                Value = t.Value.Name
                                            }).OrderBy(u => u.Value);
                sddlDistricts.DataTextField = "Value";
                sddlDistricts.DataValueField = "Key";
                sddlDistricts.DataBind();
                sddlDistricts.Items.Insert(0, new ListItem("Все", ""));
                if (!String.IsNullOrEmpty(Page.Request.Params["district"]))
                {
                    sddlDistricts.SelectedValue = Page.Request.Params["district"].Trim();
                }
            }
        }

        public void lbDelete_Click(Object sender, EventArgs e)
        {
            DeleteAccess();
            var lb = (LinkButton)sender;
            var city = new City();
            city.Delete(Convert.ToInt32(lb.CommandArgument));
            Page.Response.Redirect("~/ManagerUI/Menu/Souls/CityView.aspx");
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
            if (currentRole.PageCityView != 1)
            {
                Response.Redirect("~/Error.aspx?id=1");
            }
        }

        protected void DeleteAccess()
        {
            var userInSession = (Users)Session["userinsession"];
            var rolesList = Application["RolesList"] as List<Roles>;
            var currentRole = (Roles)rolesList.SingleOrDefault(u => u.Name.ToLower() == userInSession.Role.ToLower());
            if (currentRole.ActionCityDelete != 1)
            {
                Response.Redirect("~/Error.aspx?id=1");
            }
        }
        #endregion

        #region Methods

        protected void ListViewDataBind()
        {
            var dm = new DataManager();
            var ds = dm.QueryWithReturnDataSet(GetSearchString());
            lvAllCity.DataSource = ds;
            lvAllCity.DataBind();
            lblAllResult.Text = ds.Tables[0].Rows.Count.ToString();
            #region Редирект на первую страницу при поиске
            if (lvAllCity.Items.Count == 0 && lvDataPager.TotalRowCount != 0)
            {
                lvDataPager.SetPageProperties(0, lvDataPager.PageSize, false);
                lvAllCity.DataBind();
            }
            #endregion

            if (lvAllCity.Items.Count == 0)
            {
                lblPage.Visible = false;
            }
        }

        public String GetSearchString()
        {
            var searchString = String.Empty;
            var seletedNameString = String.Empty;
            var seletedDistrictIDString = String.Empty;

            var searchParametres = new Dictionary<String, String>();

            //формируем cтроку для поиска по Name
            if (!string.IsNullOrEmpty(hfCityID.Value))
            {
                seletedNameString = "`ID` LIKE '" + hfCityID.Value + "'";
            }
            else
            {
                seletedNameString = "`Name` LIKE '%" + stbCityName.Text + "%'";
            }

            //формируем cтроку для поиска по DistrictID
            if (!string.IsNullOrEmpty(sddlDistricts.SelectedValue))
            {
                seletedDistrictIDString = "`DistrictID` = " + sddlDistricts.SelectedValue;
            }

            //формируем конечный запро для поиска
            searchParametres.Add("Name", seletedNameString);
            searchParametres.Add("DistrictID", seletedDistrictIDString);

            foreach (var searchParametre in searchParametres)
            {
                if (!string.IsNullOrEmpty(searchParametre.Value))
                {
                    searchString = searchString + searchParametre.Value + " AND ";
                }
            }

            //формируем строку поика

            searchString = searchString.Length < 4 ? "SELECT * FROM city WHERE `Blocked` = 0 order by Name ASC" : String.Format("SELECT * FROM city WHERE {0} AND `Blocked` = 0 order by Name ASC ", searchString.Remove(searchString.Length - 4));

            return searchString;
        }
        #endregion
    }
}