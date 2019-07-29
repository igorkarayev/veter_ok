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
    public partial class DriversView : ManagerBasePage
    {
        protected string BackLink { get; set; }

        protected void Page_Init(object sender, EventArgs e)
        {
            btnReload.Click += btnReload_Click;
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            Page.Title = PagesTitles.ManagerDriversView + BackendHelper.TagToValue("page_title_part");
            OtherMethods.ActiveRightMenuStyleChanche("hlDrivers", this.Page);
            OtherMethods.ActiveRightMenuStyleChanche("hlSouls", this.Page);
            PageAccess();

            if (!IsPostBack)
            {
                sddlStatus.DataSource = Drivers.DriverStatuses;
                sddlStatus.DataTextField = "Value";
                sddlStatus.DataValueField = "Key";
                sddlStatus.DataBind();
                sddlStatus.Items.Insert(0, new ListItem("Все", string.Empty));
                sddlStatus.SelectedValue = "1";

                if (!string.IsNullOrEmpty(Page.Request.Params["stateSave"]))
                {

                    if (!string.IsNullOrEmpty(Page.Request.Params["did"]))
                    {
                        stbDID.Text = Page.Request.Params["did"];
                    }

                    if (!string.IsNullOrEmpty(Page.Request.Params["statusid"]))
                    {
                        sddlStatus.SelectedValue = Page.Request.Params["statusid"];
                    }

                    if (!string.IsNullOrEmpty(Page.Request.Params["phone"]))
                    {
                        stbPhone.Text = Page.Request.Params["phone"];
                    }

                    if (!string.IsNullOrEmpty(Page.Request.Params["firstname"]))
                    {
                        stbFirstName.Text = Page.Request.Params["firstname"];
                    }
                }
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
            BackLink = DriversHelper.BackDriverLinkBuilder(stbDID.Text, stbPhone.Text, sddlStatus.SelectedValue, stbFirstName.Text);
            var lb = (LinkButton)sender;
            var drivers = new Drivers();
            drivers.Delete(Convert.ToInt32(lb.CommandArgument), userInSession.ID, OtherMethods.GetIPAddress(), "DriversView");
            Page.Response.Redirect("~/ManagerUI/Menu/Souls/DriversView.aspx?" + BackLink);
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

        public String GetSearchString()
        {
            var searchString = String.Empty;
            var seletedStatusString = String.Empty;
            var searchDriverIdString = String.Empty;
            var searchPhoneString = String.Empty;
            var searchFirstNameString = String.Empty;
            var searchParametres = new Dictionary<String, String>();

            //формируем cтроку для поиска по UserID
            if (!string.IsNullOrEmpty(stbDID.Text))
            {
                searchDriverIdString = "`ID` = '" + stbDID.Text + "'";
            }

            //формируем cтроку для поиска по RecipientPhone
            if (!string.IsNullOrEmpty(stbPhone.Text))
            {
                searchPhoneString = "(`PhoneOne` LIKE '%" + stbPhone.Text + "%' OR `PhoneTwo` LIKE '%" + stbPhone.Text + "%')";
            }

            //формируем cтроку для поиска по Family
            if (!string.IsNullOrEmpty(stbFirstName.Text))
            {
                searchFirstNameString = "`FirstName` LIKE '%" + stbFirstName.Text + "%'";
            }

            //формируем cтроку для поиска по Status
            if (!string.IsNullOrEmpty(sddlStatus.SelectedValue))
            {
                seletedStatusString = "`StatusID` = '" + sddlStatus.SelectedValue + "'";
            }


            //формируем конечный запро для поиска
            searchParametres.Add("DriverID", searchDriverIdString);
            searchParametres.Add("PhoneNumbers", searchPhoneString);
            searchParametres.Add("FirstName", searchFirstNameString);
            searchParametres.Add("StatusID", seletedStatusString);

            foreach (var searchParametre in searchParametres)
            {
                if (!string.IsNullOrEmpty(searchParametre.Value))
                {
                    searchString = searchString + searchParametre.Value + " AND ";
                }
            }

            //формируем строку поика

            searchString = searchString.Length < 4
                ? "SELECT * FROM drivers ORDER BY FirstName ASC, LastName ASC"
                : String.Format("SELECT * FROM drivers WHERE {0} ORDER BY FirstName ASC, LastName ASC", searchString.Remove(searchString.Length - 4));

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