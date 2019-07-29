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
    public partial class CarsView : ManagerBasePage
    {
        protected string BackLink { get; set; }

        protected void Page_Init(object sender, EventArgs e)
        {
            btnReload.Click += btnReload_Click;
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            Page.Title = PagesTitles.ManagerCarsView + BackendHelper.TagToValue("page_title_part");
            OtherMethods.ActiveRightMenuStyleChanche("hlCars", this.Page);
            OtherMethods.ActiveRightMenuStyleChanche("hlSouls", this.Page);
            PageAccess();

            if (!IsPostBack)
            {
                sddlType.DataSource = Cars.CarType;
                sddlType.DataTextField = "Value";
                sddlType.DataValueField = "Key";
                sddlType.DataBind();
                sddlType.Items.Insert(0, new ListItem("Все", string.Empty));

                if (!string.IsNullOrEmpty(Page.Request.Params["stateSave"]))
                {

                    if (!string.IsNullOrEmpty(Page.Request.Params["aid"]))
                    {
                        stbAID.Text = Page.Request.Params["aid"];
                    }

                    if (!string.IsNullOrEmpty(Page.Request.Params["model"]))
                    {
                        stbModel.Text = Page.Request.Params["model"];
                    }

                    if (!string.IsNullOrEmpty(Page.Request.Params["number"]))
                    {
                        stbNumber.Text = Page.Request.Params["number"];
                    }

                    if (!string.IsNullOrEmpty(Page.Request.Params["typeid"]))
                    {
                        sddlType.SelectedValue = Page.Request.Params["typeid"];
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
            BackLink = CarsHelper.BackCarLinkBuilder(stbAID.Text, stbModel.Text, stbNumber.Text, sddlType.SelectedValue);
            var lb = (LinkButton)sender;
            var driver = new Drivers { CarID = Convert.ToInt32(lb.CommandArgument) };
            var ds = driver.GetAllItems("ID", "ASC", "CarID");
            if (ds.Tables[0].Rows.Count > 0)
            {
                lblError.Text =
                    "К автомобилю привязаны водители. Перед удалением отвяжите всех водителей от удаляемого автомобиля.";
                return;
            }
            var car = new Cars();
            car.Delete(Convert.ToInt32(lb.CommandArgument), userInSession.ID, OtherMethods.GetIPAddress(), "CarsView");
            Page.Response.Redirect("~/ManagerUI/Menu/Souls/CarsView.aspx?" + BackLink);
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
            lvAllCars.DataSource = ds;
            lvAllCars.DataBind();

            #region Редирект на первую страницу при поиске
            if (lvAllCars.Items.Count == 0 && lvDataPager.TotalRowCount != 0)
            {
                lvDataPager.SetPageProperties(0, lvDataPager.PageSize, false);
                lvAllCars.DataBind();
            }
            #endregion
        }

        public String GetSearchString()
        {
            var searchString = String.Empty;
            var searchCarIdString = String.Empty;
            var searchModelrString = String.Empty;
            var searchNumberString = String.Empty;
            var seletedTypeString = String.Empty;
            var searchParametres = new Dictionary<String, String>();

            //формируем cтроку для поиска по ID
            if (!string.IsNullOrEmpty(stbAID.Text))
            {
                searchCarIdString = "`ID` = '" + stbAID.Text + "'";
            }

            //формируем cтроку для поиска по Model
            if (!string.IsNullOrEmpty(stbModel.Text))
            {
                searchModelrString = "`Model` LIKE '%" + stbModel.Text + "%'";
            }

            //формируем cтроку для поиска по Number
            if (!string.IsNullOrEmpty(stbNumber.Text))
            {
                searchNumberString = "`Number` LIKE '%" + stbNumber.Text + "%'";
            }

            //формируем cтроку для поиска по TypeID
            if (!string.IsNullOrEmpty(sddlType.SelectedValue))
            {
                seletedTypeString = "`TypeID` = '" + sddlType.SelectedValue + "'";
            }


            //формируем конечный запро для поиска
            searchParametres.Add("CarID", searchCarIdString);
            searchParametres.Add("Model", searchModelrString);
            searchParametres.Add("Number", searchNumberString);
            searchParametres.Add("TypeID", seletedTypeString);

            foreach (var searchParametre in searchParametres)
            {
                if (!string.IsNullOrEmpty(searchParametre.Value))
                {
                    searchString = searchString + searchParametre.Value + " AND ";
                }
            }

            //формируем строку поика

            searchString = searchString.Length < 4
                ? "SELECT * FROM cars ORDER BY Model ASC, Number ASC"
                : String.Format("SELECT * FROM cars WHERE {0} ORDER BY Model ASC, Number ASC", searchString.Remove(searchString.Length - 4));

            return searchString;
        }
        #endregion

        #region Настройки доступа к странице и действиям
        protected void PageAccess()
        {
            var userInSession = (Users)Session["userinsession"];
            var rolesList = Application["RolesList"] as List<Roles>;
            var currentRole = (Roles)rolesList.SingleOrDefault(u => u.Name.ToLower() == userInSession.Role.ToLower());
            if (currentRole.PageCarsView != 1)
            {
                Response.Redirect("~/Error.aspx?id=1");
            }
        }

        protected void DeleteAccess()
        {
            var userInSession = (Users)Session["userinsession"];
            var rolesList = Application["RolesList"] as List<Roles>;
            var currentRole = (Roles)rolesList.SingleOrDefault(u => u.Name.ToLower() == userInSession.Role.ToLower());
            if (currentRole.ActionCarsDelete != 1)
            {
                Response.Redirect("~/Error.aspx?id=1");
            }
        }
        #endregion
    }
}