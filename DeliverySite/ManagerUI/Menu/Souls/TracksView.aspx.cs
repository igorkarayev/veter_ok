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
    public partial class TracksView : ManagerBasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            OtherMethods.ActiveRightMenuStyleChanche("hlTracks", this.Page);
            OtherMethods.ActiveRightMenuStyleChanche("hlSouls", this.Page);
            Page.Title = PagesTitles.ManagerTracksView + BackendHelper.TagToValue("page_title_part");
            PageAccess();
        }

        public void lbDelete_Click(Object sender, EventArgs e)
        {
            DeleteAccess();
            var lb = (LinkButton)sender;
            var track = new Tracks();
            track.Delete(Convert.ToInt32(lb.CommandArgument));
            Page.Response.Redirect("~/ManagerUI/Menu/Souls/TracksView.aspx");
        }

        protected void Page_PreRender(object sender, EventArgs e)
        {
            ListViewDataBind();
        }

        protected void ListViewDataBind()
        {
            var track = new Tracks();
            lvAllTracks.DataSource = track.GetAllItems("Name", "ASC", null);
            lvAllTracks.DataBind();

            #region Редирект на первую страницу при поиске
            if (lvAllTracks.Items.Count == 0 && lvDataPager.TotalRowCount != 0)
            {
                lvDataPager.SetPageProperties(0, lvDataPager.PageSize, false);
                lvAllTracks.DataBind();
            }
            #endregion
        }

        #region Настройки доступа к странице и действиям
        protected void PageAccess()
        {
            var userInSession = (Users)Session["userinsession"];
            var rolesList = Application["RolesList"] as List<Roles>;
            var currentRole = (Roles)rolesList.SingleOrDefault(u => u.Name.ToLower() == userInSession.Role.ToLower());
            if (currentRole.PageTracksView != 1)
            {
                Response.Redirect("~/Error.aspx?id=1");
            }
        }

        protected void DeleteAccess()
        {
            var userInSession = (Users)Session["userinsession"];
            var rolesList = Application["RolesList"] as List<Roles>;
            var currentRole = (Roles)rolesList.SingleOrDefault(u => u.Name.ToLower() == userInSession.Role.ToLower());
            if (currentRole.ActionTracksDelete != 1)
            {
                Response.Redirect("~/Error.aspx?id=1");
            }
        }
        #endregion
    }
}