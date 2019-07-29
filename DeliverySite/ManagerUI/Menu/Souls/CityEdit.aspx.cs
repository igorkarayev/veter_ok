using Delivery.BLL.Helpers;
using Delivery.BLL.StaticMethods;
using Delivery.DAL.DataBaseObjects;
using DeliverySite.Resources;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI.WebControls;

namespace Delivery.ManagerUI.Menu.Souls
{
    public partial class CityEdit : ManagerBasePage
    {
        protected string ButtonText { get; set; }

        protected string ActionText { get; set; }

        protected void Page_Init(object sender, EventArgs e)
        {
            ButtonText = Page.Request.Params["id"] != null ? "Изменить" : "Добавить";
            ActionText = Page.Request.Params["id"] != null ? "Редактирование" : "Добавление";
            btnCreate.Click += bntCreate_Click;
            DataBind();
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            OtherMethods.ActiveRightMenuStyleChanche("hlCity", this.Page);
            OtherMethods.ActiveRightMenuStyleChanche("hlSouls", this.Page);
            Page.Title = Page.Request.Params["id"] != null ? PagesTitles.ManagerCityEdit + BackendHelper.TagToValue("page_title_part") : PagesTitles.ManagerCityCreate + BackendHelper.TagToValue("page_title_part");

            #region Блок доступа к странице
            var userInSession = (Users)Session["userinsession"];
            var rolesList = Application["RolesList"] as List<Roles>;
            var currentRole = (Roles)rolesList.SingleOrDefault(u => u.Name.ToLower() == userInSession.Role.ToLower());
            if (currentRole.PageCityEdit != 1)
            {
                Response.Redirect("~/Error.aspx?id=1");
            }
            #endregion

            if (Page.Request.Params["id"] != null)
            {
                var city = new City { ID = Convert.ToInt32(Page.Request.Params["id"]) };
                city.GetById();
                if (!IsPostBack)
                {
                    tbName.Text = city.Name;
                    tbDistance.Text = city.Distance.ToString();
                    cbIsMainCity.Checked = city.IsMainCity == 1;
                    tbSOATO.Text = city.SOATO;
                    tbDistanceFromCity.Text = city.DistanceFromCity.ToString();

                    ddlRegion.DataSource = City.Regions.OrderBy(u => u.Value);
                    ddlRegion.DataTextField = "Value";
                    ddlRegion.DataValueField = "Key";
                    ddlRegion.DataBind();
                    ddlRegion.Items.Insert(0, new ListItem("Выберите область...", "0"));

                    ddlDistrict.DataSource = (from t in City.Districts
                                              select new
                                              {
                                                  Key = t.Key,
                                                  Value = t.Value.Name
                                              }).OrderBy(u => u.Value);
                    ddlDistrict.DataTextField = "Value";
                    ddlDistrict.DataValueField = "Key";
                    ddlDistrict.DataBind();
                    ddlDistrict.Items.Insert(0, new ListItem("Выберите район...", "0"));

                    ddlRegion.SelectedValue = city.RegionID.ToString();
                    ddlDistrict.SelectedValue = city.DistrictID.ToString();
                }
            }
            else
            {
                ddlRegion.DataSource = City.Regions;
                ddlRegion.DataTextField = "Value";
                ddlRegion.DataValueField = "Key";
                ddlRegion.DataBind();

                ddlDistrict.DataSource = (from t in City.Districts
                                          select new
                                          {
                                              Key = t.Key,
                                              Value = t.Value.Name
                                          }).OrderBy(u => u.Value);
                ddlDistrict.DataTextField = "Value";
                ddlDistrict.DataValueField = "Key";
                ddlDistrict.DataBind();
            }
        }

        public void bntCreate_Click(Object sender, EventArgs e)
        {
            var id = Page.Request.Params["id"];
            var districtId = Convert.ToInt32(ddlDistrict.SelectedValue);
            var district = new Districts { ID = districtId };
            district.GetById();
            var city = new City
            {
                Name = tbName.Text,
                RegionID = Convert.ToInt32(ddlRegion.SelectedValue),
                TrackID = district.TrackID,
                DistrictID = districtId,
                IsMainCity = cbIsMainCity.Checked ? 1 : 0,
                Distance = Convert.ToInt32(tbDistance.Text),
                SOATO = tbSOATO.Text,
                DistanceFromCity = Convert.ToInt32(tbDistanceFromCity.Text)
            };

            if (id == null)
            {
                city.Create();
            }
            else
            {
                city.ID = Convert.ToInt32(id);
                city.Update();
            }
            //загружаем города в оперативную память
            var cityLoad = new City();
            Application["CityList"] = cityLoad.GetAllItemsToList();
            Page.Response.Redirect("~/ManagerUI/Menu/Souls/CityView.aspx");
        }
    }
}