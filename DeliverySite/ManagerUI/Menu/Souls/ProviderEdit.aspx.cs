using Delivery.BLL.Helpers;
using Delivery.BLL.StaticMethods;
using Delivery.DAL.DataBaseObjects;
using DeliverySite.Resources;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Delivery.ManagerUI.Menu.Souls
{
    public partial class ProviderEdit : ManagerBasePage
    {
        protected string ActionText { get; set; }
        public string AppKey { get; set; }
        protected void Page_Init(object sender, EventArgs e)
        {
            btnCreate.Click += bntCreate_Click;
            ActionText = Page.Request.Params["id"] != null ? "Редактирование" : "Добавление";
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            OtherMethods.ActiveRightMenuStyleChanche("hlSouls", this.Page);
            OtherMethods.ActiveRightMenuStyleChanche("hlProviders", this.Page);
            Page.Title = Page.Request.Params["id"] != null ? PagesTitles.ManagerProviderEdit + BackendHelper.TagToValue("page_title_part") : PagesTitles.ManagerProviderCreate + BackendHelper.TagToValue("page_title_part");
            AppKey = Globals.Settings.AppServiceSecureKey;

            #region Блок доступа к странице
            var userInSession = (Users)Session["userinsession"];
            var rolesList = Application["RolesList"] as List<Roles>;
            var currentRole = (Roles)rolesList.SingleOrDefault(u => u.Name.ToLower() == userInSession.Role.ToLower());
            if (currentRole.PageDistrictEdit != 1)
            {
                Response.Redirect("~/Error.aspx?id=1");
            }
            #endregion

            if (!IsPostBack)
            {
                ddlNamePrefix.DataSource = Providers.NamePrefixes;
                ddlNamePrefix.DataTextField = "Value";
                ddlNamePrefix.DataValueField = "Key";
                ddlNamePrefix.DataBind();
            }

            if (Page.Request.Params["id"] != null)
            {
                var provider = new Providers
                {
                    ID = Convert.ToInt32(Page.Request.Params["id"])
                };
                provider.GetById();
                if (!IsPostBack)
                {
                    ddlNamePrefix.SelectedValue = provider.NamePrefix;
                    tbName.Text = provider.Name;
                    tbContactFIO.Text = provider.ContactFIO;
                    tbContactPhone.Text = provider.ContactPhone;
                    tbTypesOfProducts.Text = provider.TypesOfProducts;
                    tbAddress.Text = provider.Address;
                    tbNote.Text = provider.Note;
                    var allCity = Application["CityList"] as List<City>;
                    if (allCity != null)
                    {
                        tbCity.Text =
                            CityHelper.CityIDToAutocompleteString(allCity.FirstOrDefault(u => u.ID == provider.CityID));
                        hfCityID.Value = provider.CityID.ToString();
                    }
                }
            }
        }

        public void bntCreate_Click(Object sender, EventArgs e)
        {
            var id = Page.Request.Params["id"];
            var provider = new Providers
            {
                Name = tbName.Text,
                NamePrefix = ddlNamePrefix.SelectedValue,
                ContactFIO = tbContactFIO.Text,
                ContactPhone = tbContactPhone.Text,
                CityID = Convert.ToInt32(hfCityID.Value),
                Address = tbAddress.Text,
                Note = tbNote.Text,
                TypesOfProducts = tbTypesOfProducts.Text
            };
            if (!string.IsNullOrEmpty(id))
            {
                provider.ID = Convert.ToInt32(id);
                provider.ChangeDate = DateTime.Now;
                provider.Update();
            }
            else
            {
                provider.CreateDate = DateTime.Now;
                provider.Create();
            }

            Page.Response.Redirect("~/ManagerUI/Menu/Souls/ProvidersView.aspx");
        }
    }
}