using System;
using System.Web.UI.WebControls;
using Delivery.BLL.StaticMethods;
using Delivery.DAL.DataBaseObjects;

namespace Delivery.ManagerUI.Menu.Souls
{
    public partial class RoutesView : ManagerBasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            OtherMethods.ActiveRightMenuStyleChanche("hlRoutes", this.Page);
            OtherMethods.ActiveRightMenuStyleChanche("hlSouls", this.Page);
        }

        public void lbDelete_Click(Object sender, EventArgs e)
        {
            var lb = (LinkButton)sender;
            var route = new Routes();
            route.Delete(Convert.ToInt32(lb.CommandArgument));
            Page.Response.Redirect("~/ManagerUI/RoutesView.aspx");
        }

        protected void Page_PreRender(object sender, EventArgs e)
        {
            ListViewDataBind();
        }

        protected void ListViewDataBind()
        {
            var route = new Routes();
            lvAllTracks.DataSource = route.GetAllItems("Name", "ASC", null);
            lvAllTracks.DataBind();

            #region Редирект на первую страницу при поиске
            if (lvAllTracks.Items.Count == 0 && lvDataPager.TotalRowCount != 0)
            {
                lvDataPager.SetPageProperties(0, lvDataPager.PageSize, false);
                lvAllTracks.DataBind();
            }
            #endregion
        }
    }
}