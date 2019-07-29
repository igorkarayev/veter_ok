using Delivery.BLL;
using Delivery.BLL.Helpers;
using Delivery.BLL.StaticMethods;
using Delivery.DAL;
using Delivery.DAL.DataBaseObjects;
using DeliverySite.Resources;
using Delivery.WebServices.Objects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI.WebControls;

namespace Delivery.ManagerUI.Menu.Souls
{
    public partial class TitlesView : ManagerBasePage
    {
        protected void Page_Init(object sender, EventArgs e)
        {
            btnReload.Click += btnReload_Click;
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            Page.Title = PagesTitles.ManagerTitlesViewTitle + BackendHelper.TagToValue("page_title_part");
            OtherMethods.ActiveRightMenuStyleChanche("hlTitles", this.Page);
            OtherMethods.ActiveRightMenuStyleChanche("hlSouls", this.Page);
            PageAccess();
            if (!IsPostBack)
            {
                var category = new Category();
                var ds = category.GetAllItems("Name", "ASC", null);
                sddlCategory.DataSource = ds;
                sddlCategory.DataTextField = "Name";
                sddlCategory.DataValueField = "ID";
                sddlCategory.DataBind();
                sddlCategory.Items.Insert(0, new ListItem("Все", string.Empty));
                if (!String.IsNullOrEmpty(Page.Request.Params["categoryid"]))
                {
                    sddlCategory.SelectedValue = Page.Request.Params["categoryid"].Trim();
                }
            }
        }

        public void lbDelete_Click(Object sender, EventArgs e)
        {
            DeleteAccess();
            var lb = (LinkButton)sender;
            var category = new Titles();
            category.Delete(Convert.ToInt32(lb.CommandArgument));
            Page.Response.Redirect("~/ManagerUI/Menu/Souls/TitlesView.aspx");
        }

        protected void lvDataPager_PreRender(object sender, EventArgs e)
        {
            var dm = new DataManager();
            var ds = dm.QueryWithReturnDataSet(GetSearchString());
            lblAllResult.Text = ds.Tables[0].Rows.Count.ToString();
            lvAllTracks.DataSource = ds;
            lvAllTracks.DataBind();
            foreach (var item in lvAllTracks.Items)
            {
                var hfCategoryName = (HiddenField)item.FindControl("hfCategoryName");
                var lblDdPrice = (Label)item.FindControl("lblDDPrice");
                var lblSdPrice = (Label)item.FindControl("lblSDPrice");
                lblDdPrice.Text =
                    MoneyMethods.MoneySeparator(Calculator.Calculate(
                        new List<GoodsFromAPI> { new GoodsFromAPI { Description = hfCategoryName.Value, Number = 1 } },
                        13, 1, 239, "2", null, null, true));
                lblSdPrice.Text =
                    MoneyMethods.MoneySeparator(Calculator.Calculate(
                        new List<GoodsFromAPI> { new GoodsFromAPI { Description = hfCategoryName.Value, Number = 1 } },
                        13, 1, 239, "1", null, null));
            }

            #region Редирект на первую страницу при поиске
            if (lvAllTracks.Items.Count == 0 && lvDataPager.TotalRowCount != 0)
            {
                lvDataPager.SetPageProperties(0, lvDataPager.PageSize, false);
                lvAllTracks.DataBind();
            }
            #endregion
        }

        protected void btnReload_Click(object sender, EventArgs e)
        {
            Response.Redirect(Request.RawUrl.Split(new[] { '?' })[0]);
        }

        public String GetSearchString()
        {
            var searchString = String.Empty;
            var seletedCategoryString = String.Empty;
            var searchParametres = new Dictionary<String, String>();

            //формируем cтроку для поиска по Status
            if (!string.IsNullOrEmpty(sddlCategory.SelectedValue))
            {
                seletedCategoryString = "`CategoryID` = '" + sddlCategory.SelectedValue + "'";
            }


            //формируем конечный запро для поиска
            searchParametres.Add("CategoryID", seletedCategoryString);

            foreach (var searchParametre in searchParametres)
            {
                if (!string.IsNullOrEmpty(searchParametre.Value))
                {
                    searchString = searchString + searchParametre.Value + " AND ";
                }
            }

            //формируем строку поика

            searchString = searchString.Length < 4
                ? "SELECT * FROM titles ORDER BY Name ASC"
                : String.Format("SELECT * FROM titles WHERE {0} ORDER BY Name ASC", searchString.Remove(searchString.Length - 4));

            return searchString;
        }

        #region Настройки доступа к странице и действиям
        protected void PageAccess()
        {
            var userInSession = (Users)Session["userinsession"];
            var rolesList = Application["RolesList"] as List<Roles>;
            var currentRole = (Roles)rolesList.SingleOrDefault(u => u.Name.ToLower() == userInSession.Role.ToLower());
            if (currentRole.PageTitlesView != 1)
            {
                Response.Redirect("~/Error.aspx?id=1");
            }
        }

        protected void DeleteAccess()
        {
            var userInSession = (Users)Session["userinsession"];
            var rolesList = Application["RolesList"] as List<Roles>;
            var currentRole = (Roles)rolesList.SingleOrDefault(u => u.Name.ToLower() == userInSession.Role.ToLower());
            if (currentRole.ActionTitlesDelete != 1)
            {
                Response.Redirect("~/Error.aspx?id=1");
            }
        }
        #endregion
    }
}