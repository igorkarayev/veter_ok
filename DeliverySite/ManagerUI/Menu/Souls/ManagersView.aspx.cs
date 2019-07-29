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
    public partial class ManagersView : ManagerBasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Page.Title = PagesTitles.ManagerManagersView + BackendHelper.TagToValue("page_title_part");
            OtherMethods.ActiveRightMenuStyleChanche("hlManagers", this.Page);
            OtherMethods.ActiveRightMenuStyleChanche("hlSouls", this.Page);
            PageAccess();
            if (!IsPostBack)
            {
                var month = DateTime.Now.Month.ToString();
                if (month.Length == 1) month = String.Format("0{0}", month);
                stbDate1.Text = String.Format("01-{0}-{1}", month, DateTime.Now.Year);
                stbDate2.Text = DateTime.Now.AddDays(1).ToString("dd-MM-yyyy");
            }
        }

        public void lbDelete_Click(Object sender, EventArgs e)
        {
            DeleteAccess();
            var userInSession = (Users)Session["userinsession"];
            var lb = (LinkButton)sender;
            var manager = new Users();
            manager.Delete(Convert.ToInt32(lb.CommandArgument), userInSession.ID, OtherMethods.GetIPAddress(), "ManagersView");
            Page.Response.Redirect("~/ManagerUI/Menu/Souls/ManagersView.aspx");
        }

        protected void Page_PreRender(object sender, EventArgs e)
        {
            ListViewDataBind();
        }

        protected void ListViewDataBind()
        {
            var dm = new DataManager();
            var ds = dm.QueryWithReturnDataSet("select * from `users` WHERE Role <> \"User\" ORDER BY Family ASC;");
            lvAllManager.DataSource = ds;
            lvAllManager.DataBind();

            #region Редирект на первую страницу при поиске
            if (lvAllManager.Items.Count == 0 && lvDataPager.TotalRowCount != 0)
            {
                lvDataPager.SetPageProperties(0, lvDataPager.PageSize, false);
                lvAllManager.DataBind();
            }
            #endregion

            var user = (Users)Session["userinsession"];
            var rolesList = Application["RolesList"] as List<Roles>;
            var currentRole = (Roles)rolesList.SingleOrDefault(u => u.Name.ToLower() == user.Role.ToLower());

            if (currentRole.PageManagerEdit != 1)
            {
                hlManagerCreate.Visible = false;
            }

            foreach (ListViewDataItem items in lvAllManager.Items)
            {
                var lbEditLink = (HyperLink)items.FindControl("lbEdit");
                var lbDeleteLink = (LinkButton)items.FindControl("lbDelete");
                var idManager = (Label)items.FindControl("lblID");
                var hfRoleText = (HiddenField)items.FindControl("hfRole");
                var lblLinkedClientsCount = (Label)items.FindControl("lblLinkedClientsCount");
                var lblLinkedUsersTicketCount = (Label)items.FindControl("lblLinkedUsersTicketCount");

                if ((hfRoleText.Value == Users.Roles.Admin.ToString() || hfRoleText.Value == Users.Roles.SuperAdmin.ToString()) && user.Role != Users.Roles.SuperAdmin.ToString())
                {
                    lbEditLink.Visible = false;
                    lbDeleteLink.Visible = false;
                }

                if (hfRoleText.Value == Users.Roles.SuperAdmin.ToString() && user.Role == Users.Roles.SuperAdmin.ToString())
                {
                    lbEditLink.Visible = true;
                    lbDeleteLink.Visible = false;
                }

                if (idManager.Text == user.ID.ToString())
                {
                    lbDeleteLink.Visible = false;
                    lbEditLink.Visible = true;
                }

                if (currentRole.ActionManagersDelete != 1)
                {
                    lbDeleteLink.Visible = false;
                }

                if (currentRole.PageManagerEdit != 1)
                {
                    lbEditLink.Visible = false;
                }

                if (hfRoleText.Value == Users.Roles.Manager.ToString())
                {
                    lblLinkedClientsCount.Text = dm.QueryWithReturnDataSet(
                        String.Format("SELECT COUNT(*) FROM `Users` WHERE `ManagerID` = {0}",
                        idManager.Text)).Tables[0].Rows[0][0]
                        .ToString();

                    lblLinkedUsersTicketCount.Text = MoneyMethods.MoneySeparator(dm.QueryWithReturnDataSet(
                        String.Format("SELECT COUNT(*) FROM Tickets T JOIN Users U ON T.`UserID` = U.`ID` WHERE (U.`ManagerID` = {0}) AND (T.CreateDate BETWEEN '{1}' AND '{2}')",
                        idManager.Text,
                        Convert.ToDateTime(stbDate1.Text).ToString("yyyy-MM-dd"),
                        Convert.ToDateTime(stbDate2.Text).ToString("yyyy-MM-dd")))
                        .Tables[0].Rows[0][0].ToString());
                }

                if (hfRoleText.Value == Users.Roles.SalesManager.ToString())
                {
                    lblLinkedClientsCount.Text = dm.QueryWithReturnDataSet(
                        String.Format("SELECT COUNT(*) FROM `Users` WHERE `SalesManagerID` = {0}",
                        idManager.Text)).Tables[0].Rows[0][0]
                        .ToString();


                    lblLinkedUsersTicketCount.Text = MoneyMethods.MoneySeparator(dm.QueryWithReturnDataSet(
                        String.Format("SELECT COUNT(*) FROM Tickets T JOIN Users U ON T.`UserID` = U.`ID` WHERE `SalesManagerID` = {0} AND (T.CreateDate BETWEEN '{1}' AND '{2}')",
                        idManager.Text,
                        Convert.ToDateTime(stbDate1.Text).ToString("yyyy-MM-dd"),
                        Convert.ToDateTime(stbDate2.Text).ToString("yyyy-MM-dd")))
                        .Tables[0].Rows[0][0].ToString());
                }
            }
        }

        #region Настройки доступа к странице и действиям
        protected void PageAccess()
        {
            var userInSession = (Users)Session["userinsession"];
            var rolesList = Application["RolesList"] as List<Roles>;
            var currentRole = (Roles)rolesList.SingleOrDefault(u => u.Name.ToLower() == userInSession.Role.ToLower());
            if (currentRole.PageManagersView != 1)
            {
                Response.Redirect("~/Error.aspx?id=1");
            }
        }

        protected void DeleteAccess()
        {
            var userInSession = (Users)Session["userinsession"];
            var rolesList = Application["RolesList"] as List<Roles>;
            var currentRole = (Roles)rolesList.SingleOrDefault(u => u.Name.ToLower() == userInSession.Role.ToLower());
            if (currentRole.ActionManagersDelete != 1)
            {
                Response.Redirect("~/Error.aspx?id=1");
            }
        }
        #endregion
    }
}