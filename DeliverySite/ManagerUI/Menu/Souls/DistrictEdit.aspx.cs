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
    public partial class DistrictEdit : ManagerBasePage
    {
        protected void Page_Init(object sender, EventArgs e)
        {
            btnCreate.Click += bntCreate_Click;
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            OtherMethods.ActiveRightMenuStyleChanche("hlSouls", this.Page);
            OtherMethods.ActiveRightMenuStyleChanche("hlDistricts", this.Page);
            Page.Title = PagesTitles.ManagerDistrictEdit + BackendHelper.TagToValue("page_title_part");

            #region Блок доступа к странице
            var userInSession = (Users)Session["userinsession"];
            var rolesList = Application["RolesList"] as List<Roles>;
            var currentRole = (Roles)rolesList.SingleOrDefault(u => u.Name.ToLower() == userInSession.Role.ToLower());
            if (currentRole.PageDistrictEdit != 1)
            {
                Response.Redirect("~/Error.aspx?id=1");
            }
            #endregion

            if (Page.Request.Params["id"] != null)
            {
                var district = new Districts { ID = Convert.ToInt32(Page.Request.Params["id"]) };
                district.GetById();
                if (!IsPostBack)
                {
                    tbDeliveryTerms.Text = district.DeliveryTerms.ToString();
                    tbName.Text = district.Name;

                    var track = new Tracks();
                    ddlTrack.DataSource = track.GetAllItems();
                    ddlTrack.DataTextField = "Name";
                    ddlTrack.DataValueField = "ID";
                    ddlTrack.DataBind();
                    ddlTrack.Items.Insert(0, new ListItem("Выберите направление...", "0"));
                    ddlTrack.SelectedValue = district.TrackID.ToString();

                    if (district.Monday == 1)
                    {
                        cbMonday.Checked = true;
                    }
                    if (district.Tuesday == 1)
                    {
                        cbTuesday.Checked = true;
                    }
                    if (district.Wednesday == 1)
                    {
                        cbWednesday.Checked = true;
                    }
                    if (district.Thursday == 1)
                    {
                        cbThursday.Checked = true;
                    }
                    if (district.Friday == 1)
                    {
                        cbFriday.Checked = true;
                    }
                    if (district.Saturday == 1)
                    {
                        cbSaturday.Checked = true;
                    }
                    if (district.Sunday == 1)
                    {
                        cbSunday.Checked = true;
                    }
                }
            }
        }

        public void bntCreate_Click(Object sender, EventArgs e)
        {
            var id = Page.Request.Params["id"];
            var trackId = Convert.ToInt32(ddlTrack.SelectedValue);
            var district = new Districts
            {
                Name = tbName.Text,
                DeliveryTerms = Convert.ToInt32(tbDeliveryTerms.Text),
                Monday = cbMonday.Checked ? 1 : 0,
                Tuesday = cbTuesday.Checked ? 1 : 0,
                Wednesday = cbWednesday.Checked ? 1 : 0,
                Thursday = cbThursday.Checked ? 1 : 0,
                Friday = cbFriday.Checked ? 1 : 0,
                Saturday = cbSaturday.Checked ? 1 : 0,
                Sunday = cbSunday.Checked ? 1 : 0,
                ID = Convert.ToInt32(id),
                TrackID = trackId
            };
            district.Update();
            var dm = new DataManager();
            dm.QueryWithoutReturnData(null, "UPDATE city SET TrackID = " + trackId + " WHERE DistrictID = " + district.ID);
            //загружаем backend в оперативную память
            var districts = new DAL.DataBaseObjects.Districts();
            Application["Districts"] = districts.GetAllItemsToList();
            Page.Response.Redirect("~/ManagerUI/Menu/Souls/DistrictsView.aspx");
        }
    }
}