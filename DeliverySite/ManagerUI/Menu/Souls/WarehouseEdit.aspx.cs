using Delivery.BLL.Helpers;
using Delivery.BLL.StaticMethods;
using Delivery.DAL.DataBaseObjects;
using DeliverySite.Resources;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Delivery.ManagerUI.Menu.Souls
{
    public partial class WarehouseEdit : ManagerBasePage
    {
        protected string ButtonText { get; set; }

        protected string ActionText { get; set; }

        public String AppKey { get; set; }

        protected void Page_Init(object sender, EventArgs e)
        {
            ButtonText = Page.Request.Params["id"] != null ? "Изменить" : "Добавить";
            ActionText = Page.Request.Params["id"] != null ? "Редактирование" : "Добавление";
            btnCreate.Click += bntCreate_Click;
            DataBind();
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            OtherMethods.ActiveRightMenuStyleChanche("hlWarehouses", this.Page);
            OtherMethods.ActiveRightMenuStyleChanche("hlSouls", this.Page);
            Page.Title = Page.Request.Params["id"] != null ? PagesTitles.ManagerWarehouseEdit + BackendHelper.TagToValue("page_title_part") : PagesTitles.ManagerWarehouseCreate + BackendHelper.TagToValue("page_title_part");

            #region Блок доступа к странице
            var userInSession = (Users)Session["userinsession"];
            var rolesList = Application["RolesList"] as List<Roles>;
            var currentRole = (Roles)rolesList.SingleOrDefault(u => u.Name.ToLower() == userInSession.Role.ToLower());
            if (currentRole.PageWarehouseEdit != 1)
            {
                Response.Redirect("~/Error.aspx?id=1");
            }
            #endregion

            AppKey = Globals.Settings.AppServiceSecureKey;

            if (Page.Request.Params["id"] != null)
            {
                var warehouse = new Warehouses { ID = Convert.ToInt32(Page.Request.Params["id"]) };
                warehouse.GetById();
                if (!IsPostBack)
                {
                    tbName.Text = warehouse.Name;
                    ddlStreetPrefix.SelectedValue = warehouse.StreetPrefix;
                    tbStreetName.Text = warehouse.StreetName;
                    tbStreetNumber.Text = warehouse.StreetNumber;
                    tbHousing.Text = warehouse.Housing;
                    tbApartmentNumber.Text = warehouse.ApartmentNumber;
                    var allCity = Application["CityList"] as List<City>;
                    if (allCity != null)
                    {
                        tbCity.Text = CityHelper.CityIDToAutocompleteString(allCity.FirstOrDefault(u => u.ID == warehouse.CityID));
                        hfCityID.Value = warehouse.CityID.ToString();
                    }
                }
            }
        }

        public void bntCreate_Click(Object sender, EventArgs e)
        {
            var id = Page.Request.Params["id"];
            var userInSession = (Users)Session["userinsession"];
            var warehouse = new Warehouses()
            {
                Name = tbName.Text.Trim(),
                StreetPrefix = ddlStreetPrefix.SelectedValue,
                StreetName = tbStreetName.Text.Trim(),
                StreetNumber = tbStreetNumber.Text.Trim(),
                Housing = tbHousing.Text.Trim(),
                ApartmentNumber = tbApartmentNumber.Text.Trim(),
                CityID = Convert.ToInt32(hfCityID.Value)

            };

            if (id == null)
            {
                warehouse.CreateDate = DateTime.Now;
                warehouse.Create();
            }
            else
            {
                warehouse.ID = Convert.ToInt32(id);
                warehouse.ChangeDate = DateTime.Now;
                warehouse.Update(userInSession.ID, OtherMethods.GetIPAddress(), "WarehouseEdit");
            }
            Page.Response.Redirect("~/ManagerUI/Menu/Souls/WarehousesView.aspx");
        }
    }
}